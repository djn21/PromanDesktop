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
    class ActivityDAO
    {

        public static Activity[] getAllActivities()
        {
            XElement response = WebService.callFunction("getAllActivities");
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList activities = new ArrayList();
            if (dictionary.ContainsKey("id"))
            {
                for (int i = 0; i < dictionary["id"].Count; i++)
                {
                    int id = Int32.Parse(dictionary["id"][i]);
                    string name = dictionary["name"][i];
                    string note = dictionary["note"][i];
                    Activity acivity = new Activity(id, name, "", 0, note);
                    activities.Add(acivity);
                }
            }
            return (Activity[])activities.ToArray(typeof(Activity));
        }

        public static Activity[] getAllActivitiesOnTask(int taskId)
        {
            XElement response = WebService.callFunction("getAllActivitiesOnTask",taskId);
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList activities = new ArrayList();
            if (dictionary.ContainsKey("id"))
            {
                for (int i = 0; i < dictionary["id"].Count; i++)
                {
                    int id = Int32.Parse(dictionary["id"][i]);
                    string name = dictionary["name"][i];
                    string user = dictionary["user"][i];
                    double time = Double.Parse(dictionary["time"][i]);
                    string note = dictionary["note"][i];
                    Activity acivity = new Activity(id, name, user, time, note);
                    activities.Add(acivity);
                }
            }
            return (Activity[])activities.ToArray(typeof(Activity));
        }

    }
}
