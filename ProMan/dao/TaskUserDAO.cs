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
    class TaskUserDAO
    {

        public static TaskUser[] getAllUsersOnTask(int taskId)
        {
            XElement response = WebService.callFunction("getAllUsersOnTask", Login.username, Login.password, taskId);
            Dictionary<string, List<string>> dictionary = response.Descendants("item").Elements("item")
                .GroupBy(x => x.Element("key").Value, y => y.Element("value").Value)
                .ToDictionary(x => x.Key, y => y.ToList());
            ArrayList taskUsers = new ArrayList();
            if (dictionary.ContainsKey("id"))
            {
                for (int i = 0; i < dictionary["id"].Count; i++)
                {
                    int id = Int32.Parse(dictionary["id"][i]);
                    string name = dictionary["name"][i];
                    string role = dictionary["role"][i];
                    TaskUser taskUser = new TaskUser(id, name, role);
                    taskUsers.Add(taskUser);
                }
            }
            return (TaskUser[])taskUsers.ToArray(typeof(TaskUser));
        }

    }
}
