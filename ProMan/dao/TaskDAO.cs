using ProjectManager.dao;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectManager.dao
{
    class TaskDAO
    {
        public static ProjectManager.dto.Task[] getAllTasks()
        {
            XElement response = WebService.callFunction("getAllTasks");
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList tasks = new ArrayList();
            for (int i = 0; i < dictionary["id"].Count; i++)
            {
                int id = Int32.Parse(dictionary["id"][i]);
                string name = dictionary["name"][i];
                string startDate = dictionary["start_date"][i];
                string endDate = dictionary["end_date"][i];
                string deadLine = dictionary["dead_line"][i];
                double manHours = Double.Parse(dictionary["man_hours"][i]);
                double percentage = Double.Parse(dictionary["percentage"][i]);
                string note = dictionary["note"][i];
                dto.Task task = new dto.Task(id, name, startDate, endDate, deadLine, manHours, percentage, note);
                tasks.Add(task);
            }
            return (dto.Task[])tasks.ToArray(typeof(dto.Task));
        }

        public static dto.Task[] getAllTasksOnProject(int projectId)
        {
            XElement response = WebService.callFunction("getAllTasksOnProject", projectId);
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList tasks = new ArrayList();
            if (dictionary.ContainsKey("id"))
            {
                for (int i = 0; i < dictionary["id"].Count; i++)
                {
                    int id = Int32.Parse(dictionary["id"][i]);
                    string name = dictionary["name"][i];
                    string startDate = dictionary["start_date"][i];
                    string endDate = dictionary["end_date"][i];
                    string deadLine = dictionary["dead_line"][i];
                    double manHours = Double.Parse(dictionary["man_hours"][i]);
                    double percentage = Double.Parse(dictionary["percentage"][i]);
                    string note = dictionary["note"][i];
                    dto.Task task = new dto.Task(id, name, startDate, endDate, deadLine, manHours, percentage, note);
                    tasks.Add(task);
                }
            }
            return (dto.Task[])tasks.ToArray(typeof(dto.Task));
        }
    }
}
