using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.dto
{
    class Activity
    {
        private int id;
        private string name;
        private string user;
        private double time;
        private string note;

        public Activity()
        {
            this.id = 0;
            this.name = "";
            this.user = "";
            this.time = 0;
            this.note = "";
        }

        public Activity(int id, string name, string user, double time, string note)
        {
            this.id = id;
            this.name = name;
            this.user = user;
            this.time = time;
            this.note = note;
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

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public double Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

    }
}
