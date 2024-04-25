using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace FinalProject
{
    [DataContract]
    public abstract class Crew
    {
        [DataMember]
        private string fullName;
        [DataMember]
        private double salary;
        [DataMember]
        private string position;
        public Crew(string fullName, double salary, string position)
        {
            this.fullName = fullName;
            this.salary = salary;
            this.position = position;
        }
        public string FullName { get { return fullName; } }
        public double Salary { get { return salary; } }
        public string Position { get { return position; } }
        public abstract double CalculateSalary();
        public abstract void Print();
    }
}