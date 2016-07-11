using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.dto
{
    class Income
    {
        private int id;
        private string description;
        private double amount;
        private string date;

        public Income()
        {
            this.id = 0;
            this.description = "";
            this.amount = 0;
            this.date = "";
        }

        public Income(int id, string description, double amount, string date)
        {
            this.id = id;
            this.description = description;
            this.amount = amount;
            this.date = date;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

    }
}
