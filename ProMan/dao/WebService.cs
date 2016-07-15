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
        public static XElement callFunction(string function, string username, string password)
        {
            SoapClient client = new SoapClient("http://pisio.etfbl.net/~dejand/proman/soap-service/service");
            XElement xmlResponse = client.Invoke(function, username, password);
            return xmlResponse;
        }

        public static XElement callFunction(string function, string username, string password, int parameter)
        {
            SoapClient client = new SoapClient("http://pisio.etfbl.net/~dejand/proman/soap-service/service");
            XElement xmlResponse = client.Invoke(function, username, password, parameter);
            return xmlResponse;
        }

    }
}
