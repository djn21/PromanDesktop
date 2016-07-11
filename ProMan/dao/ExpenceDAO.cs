using ProjectManager.dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectManager.dao
{
    class ExpenceDAO
    {

        public static Expence[] getAllIncomesOnProject(int projectId)
        {
            XElement response = WebService.callFunction("getAllExpencesOnProject", projectId);
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList expences = new ArrayList();
            if (dictionary.ContainsKey("id"))
            {
                for (int i = 0; i < dictionary["id"].Count; i++)
                {
                    int id = Int32.Parse(dictionary["id"][i]);
                    string description = dictionary["description"][i];
                    double amount = Double.Parse(dictionary["amount"][i]);
                    string date = dictionary["date"][i];
                    Expence expence = new Expence(id, description, amount, date);
                    expences.Add(expence);
                }
            }
            return (Expence[])expences.ToArray(typeof(Expence));
        }

    }
}
