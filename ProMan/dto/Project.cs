using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.dto
{
    class Project
    {
        private int id;
        private string name;
        private string startDate;
        private string endDate;
        private string deadLine;
        private string status;
        private string note;

        public Project()
        {
            this.id = 0;
            this.name = "";
            this.startDate = "";
            this.endDate = "";
            this.deadLine = "";
            this.status = "";
            this.note = "";
        }

        public Project(int id, string name, string startDate, string endDate, string deadLine, string status, string note)
        {
            this.id = id;
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            this.deadLine = deadLine;
            this.status = status;
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

        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string DeadLine
        {
            get { return deadLine; }
            set { deadLine = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

    }
}
