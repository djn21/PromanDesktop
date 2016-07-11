using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectManager.soap;

namespace ProjectManager.dao
{
    class WebService
    {
        public static XElement callFunction(string function)
        {
            SoapClient client = new SoapClient("http://pisio.etfbl.net/~dejand/proman/soap-service/service");
            XElement xmlResponse = client.Invoke(function);
            return xmlResponse;
        }

        public static XElement callFunction(string function, int parameter)
        {
            SoapClient client = new SoapClient("http://pisio.etfbl.net/~dejand/proman/soap-service/service");
            XElement xmlResponse = client.Invoke(function,parameter);
            return xmlResponse;
        }

    }
}
