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
    public class FlightAttendant : Crew
    {
        [DataMember]
        public List<string> LanguagesSpoken { get; set; }
        public FlightAttendant(string fullName, double salary, string position,List<string> languagesSpoken) : base(fullName, salary, position)
        {
            LanguagesSpoken = languagesSpoken;
        }
        public override double CalculateSalary()
        {
            double languageCoeffiecient = 200;
            double additionalAmount = LanguagesSpoken.Count * languageCoeffiecient;
            if(Position.ToLower() == "first")
            {
                additionalAmount += 200;
            }
            if(LanguagesSpoken.Count > 3)
            {
                additionalAmount += 500;
            }
            double totalSalary = Salary + additionalAmount;
            return totalSalary;
        }
        public override void Print()
        {
            string languagesString = string.Join(", ", LanguagesSpoken);
            double totalSalary = CalculateSalary();
            Console.WriteLine("{0} being {1} flight attendant on the plane speaks {2} languages, has salary: {3}$", FullName, Position, languagesString, Salary);
            Console.WriteLine($"Total salary with bonuses: {totalSalary}$");
        }
    }
}
