using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.dto
{
    class TaskUser
    {
        private int id;
        private string name;
        private string role;

        public TaskUser()
        {
            this.id = 0;
            this.name = "";
            this.role = "";
        }

        public TaskUser(int id, string name, string role)
        {
            this.id = id;
            this.name = name;
            this.role = role;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}
