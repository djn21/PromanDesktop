using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;


namespace ProjectManager.soap
{
    /// <summary>
    /// Soap client
    /// </summary>
    public class SoapClient
    {
        /// <summary>
        /// Wsdl url
        /// </summary>
        public string WSDLUrl { get; set; }
        /// <summary>
        /// True if save wsdl definition
        /// </summary>
        public bool UseLocal { get; set; }

        private XDocument xDocument;
        private readonly CustomWebClient client = new CustomWebClient();

        private WSDL wsdl;

        public SoapHeader Header { get; set; }
               

        public WSDL Wsdl
        {
            get { return this.wsdl; }
            set { this.wsdl = value; }
        }

        public SoapClient()
        {
            //client.Timeout = 2800;
        }

        /// <summary>
        /// Load Soapclient with wsdl url
        /// </summary>
        /// <param name="wsdlUrl"></param>
        public SoapClient(string wsdlUrl)
        {
            this.WSDLUrl = wsdlUrl;            
        }

        /// <summary>
        /// Load and parse WSDL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="saveLocally"></param>
        public void LoadWSDL(string url, bool saveLocally)
        {   
            this.WSDLUrl = url;            
            this.UseLocal = saveLocally;

            //try to load local definition 
            if (File.Exists("wsdl.xml"))
                this.wsdl = WSDL.Deserialize();
            else
               ParseWSDL();               

            if (saveLocally)
                this.wsdl.Serialize();                
        }

        private void ParseWSDL()
        {

            if (this.xDocument == null)
                xDocument = XDocument.Load(this.WSDLUrl);

            // declaring wsdl definition
            this.wsdl = new WSDL();
            this.wsdl.TargetNS = xDocument.Root.Attribute("targetNamespace").Value;

            // get the soap portType   
            // if ports types count > 1 get soap one. Else get first one
            XElement soapContainer = null, binding = null;
            string portType = string.Empty;
            if (xDocument.Root.Elements(XName.Get(string.Format("{{{0}}}portType", xDocument.Root.Name.NamespaceName))).Count() > 1)
            {
                soapContainer = xDocument.Root.Elements(XName.Get(string.Format("{{{0}}}portType", xDocument.Root.Name.NamespaceName))).FirstOrDefault(c => c.Attribute("name").Value.ToLower().Contains("soap"));
                portType = soapContainer.Attribute("name").Value;
                binding = xDocument.Root.Elements(XName.Get(string.Format("{{{0}}}binding", xDocument.Root.Name.NamespaceName))).FirstOrDefault(c => c.Attribute("name").Value == portType);
            }
            else
            {
                soapContainer = xDocument.Root.Element(XName.Get(string.Format("{{{0}}}portType", xDocument.Root.Name.NamespaceName)));
                portType = soapContainer.Attribute("name").Value;
                binding = xDocument.Root.Element(XName.Get(string.Format("{{{0}}}binding", xDocument.Root.Name.NamespaceName)));
            }
            

            if (soapContainer == null)
                throw new ArgumentNullException("soap port type cannot be found");

            // creating operations
            this.wsdl.Operations = soapContainer.Elements(XName.Get(string.Format("{{{0}}}operation",soapContainer.Name.NamespaceName))).Select(c => new WsdlOperation(c)).ToList();

            // getting parameters 
            XElement soapTypes = xDocument.Root.Element(XName.Get(string.Format("{{{0}}}types",xDocument.Root.Name.NamespaceName)));
            IEnumerable<XElement> soapMessages = xDocument.Root.Elements(XName.Get(string.Format("{{{0}}}message", xDocument.Root.Name.NamespaceName)));
            if (soapTypes == null)
                throw new ArgumentNullException("soap types cannot be found");

            // associating operations and types
            XElement parameter = null;
            
            foreach (WsdlOperation operation in wsdl.Operations)
            {
                
                // parameter is defined in type 
                parameter = soapTypes.Descendants().FirstOrDefault(c => c.Name.LocalName == "element" && c.Attribute("name").Value == operation.Name);
                if (parameter != null)
                {
                    parameter = parameter.Descendants().FirstOrDefault(c => c.Name.LocalName == "sequence");
                    if (parameter != null)
                        operation.Parameters = parameter.Elements().Select(c => new WsdlParameter(c)).ToList();
                }
                else // parameter is in messages
                {
                    parameter = soapMessages.SingleOrDefault(c => c.Attribute("name").Value == operation.InputMessage);
                    if (parameter == null)
                        continue;
                    operation.Parameters = parameter.Descendants(XName.Get(string.Format("{{{0}}}part", xDocument.Root.Name.NamespaceName))).Select(c => new WsdlParameter(c)).ToList();
                }

                parameter = binding.Descendants().FirstOrDefault(c => c.Name.LocalName == "operation" && c.Attribute("name") != null && c.Attribute("name").Value == operation.Name);
                if (parameter != null)
                    operation.SoapAction = ((XElement)parameter.FirstNode).Attribute("soapAction").Value;   //this.wsdl.TargetNS + operation.Name;
            }

            // getting address
            XElement xAddress = xDocument.Root.Descendants(XName.Get(string.Format("{{{0}}}port", xDocument.Root.Name.NamespaceName))).FirstOrDefault(c => c.Attribute("name").Value == portType);
            if(xAddress != null)
                this.wsdl.Address = ((XElement)xAddress.FirstNode).Attribute("location").Value;
        }

        /// <summary>
        /// Invoke wsdl operation and return results
        /// </summary>
        /// <param name="operationName">Operation Name</param>
        /// <param name="args">ordered parameters</param>
        /// <returns></returns>
        public XElement Invoke(string operationName, params object[] args)
        {            
            if (this.wsdl== null)
                ParseWSDL();
            
            // creating input message
            SoapBuilder message = new SoapBuilder();

            // enveloppe 
            message.WriteStartElement("Envelope", "soap", this.wsdl.SoapNS);

            // header 
            if (this.Header != null && !string.IsNullOrEmpty(this.Header.Name))
            {
                message.WriteStartElement("Header", "soap",null);
                message.WriteStartElement(this.Header.Name, null, this.wsdl.TargetNS);
                foreach (KeyValue kvp in this.Header.Values)
                {
                    message.WriteElement(kvp.Key, kvp.Value);
                    
                }
                message.WriteEndElement();//header
                message.WriteEndElement();//header name
            }

            message.WriteStartElement("Body", "soap", null);

            message.WriteStartElement(operationName, null, this.wsdl.TargetNS);
            
            WsdlOperation operation = this.wsdl.Operations.SingleOrDefault(c => c.Name == operationName);
            for (int i = 0; i < args.Length; i++)
            {
                message.WriteElement(operation.Parameters[i].Name, Convert.ToString(args[i]));
            }
            message.WriteEndElement();//operation
            message.WriteEndElement();//body

            message.WriteEndElement();//envelope
                        
            
            // send command               
            // setting client header            
            client.Headers = new WebHeaderCollection();
            client.Headers.Add("Content-Type", "text/xml; charset=utf-8");

            // get soapAction            
            client.Headers.Add("SOAPAction", operation.SoapAction);
            
            string result = client.UploadString(this.wsdl.Address,"POST", message.ToString());

            //parsing result

            xDocument = XDocument.Parse(result);
            XElement xResponse = xDocument.Descendants().Where(c => c.Name.LocalName.ToLower() == "body").FirstOrDefault();

            return xResponse;
        }


    }
}

