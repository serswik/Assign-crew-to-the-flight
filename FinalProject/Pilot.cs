using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FinalProject
{
    [DataContract]
    public class Pilot : Crew
    {
        [DataMember]
        private string tier;
        public Pilot(string fullName, double salary, string position,string tier) : base(fullName,salary, position)
        {
            this.tier = tier;
        }
        public string Tier { get { return tier; } }
        public override double CalculateSalary()
        {
            double tierCoefficient = GetTierCoefficient(tier);
            double positionCoefficient = 600;
            if (Position.ToLower() == "first")
            {
                positionCoefficient += 500;
            }
            double additionalAmount = tierCoefficient * positionCoefficient;
            double totalSalary = Salary + additionalAmount;
            return totalSalary;
        }
        private double GetTierCoefficient(string tier)
        {
            switch(tier.ToLower())
            {
                case "top":
                    return 1.6;
                case "mid":
                    return 1.3;
                case "low":
                    return 1.2;
                default:
                    return 1;
            }
        }
        public override void Print()
        {
            double totalSalary = CalculateSalary();
            Console.WriteLine("{0} being {1} pilot on the plane with {2} tier, has salary: {3}$", FullName,Position,Tier, Salary);
            Console.WriteLine($"Total salary with bonuses: {totalSalary}$");
        }
    }
}
