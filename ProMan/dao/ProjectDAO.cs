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
    class ProjectDAO
    {

        public static Project[] getAllProjects(){
            XElement response=WebService.callFunction("getAllProjects");
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList projects=new ArrayList();
            if (dictionary.ContainsKey("id"))
            {
                for (int i = 0; i < dictionary["id"].Count; i++)
                {
                    int id = Int32.Parse(dictionary["id"][i]);
                    string name = dictionary["name"][i];
                    string startDate = dictionary["start_date"][i];
                    string endDate = dictionary["end_date"][i];
                    string deadLine = dictionary["dead_line"][i];
                    string status = dictionary["status"][i];
                    string note = dictionary["note"][i];
                    Project project = new Project(id, name, startDate, endDate, deadLine, status, note);
                    projects.Add(project);
                }
            }
            return (Project[])projects.ToArray(typeof(Project));
        }

    }
}
