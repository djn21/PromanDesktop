using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.dto
{
    class Task
    {
        private int id;
        private string name;
        private string startDate;
        private string endDate;
        private string deadLine;
        private double manHours;
        private double percentage;
        private string note;

        public Task()
        {
            this.id = 0;
            this.name = "";
            this.startDate = "";
            this.endDate = "";
            this.deadLine = "";
            this.manHours = 0;
            this.percentage = 0;
            this.note = "";
        }

        public Task(int id, string name, string startDate, string endDate, string deadLine, double manHours, double percentage, string note)
        {
            this.id = id;
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            this.deadLine = deadLine;
            this.manHours = manHours;
            this.percentage = percentage;
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

        public double ManHours
        {
            get { return manHours; }
            set { manHours = value; }
        }

        public double Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

    }
}
