using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using log4net;

namespace FinalProject
{
    public class Program
    {
        public static void Main()
        {
            int flightHours;
            try
            {
                flightHours = GetValidFlightHours();
            }
            catch(FormatException ex)
            {
                Console.WriteLine("Invalid input format. Please enter an integer.");
                flightHours = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                flightHours = 0;
            }

            string pilotTier = GetPilotTier(flightHours);

            List<Pilot> pilotList = new List<Pilot>();
            AddPilotToList(pilotList,"John Doe", 7000, "second", pilotTier);
            AddPilotToList(pilotList,"Adam Brooks", 8000, "first", pilotTier);

            List<FlightAttendant> flightAttendantList = new List<FlightAttendant>();
            AddAttendantToList(flightAttendantList, "Andrew Grey", 2500, "first", new List<string> { "English", "Spanish", "Ukrainian" });
            AddAttendantToList(flightAttendantList, "Benjamin Aveo", 3500, "second", new List<string> { "English", "Ukrainian" });

            foreach (var p in pilotList)
            {
                p.Print();
            }
            Console.WriteLine("");
            
            foreach(var f in flightAttendantList)
            {
                f.Print();
            }
            Console.WriteLine("");

            double totalPrice = CalculateTotalPrice(pilotList, flightAttendantList, flightHours);
            Console.WriteLine("Total price of the flight(salaries,fuel): {0}$", totalPrice);

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            
            SerializeToJsonPilots(pilotList, "pilots.json");
            SerializeToJsonAttendants(flightAttendantList, "flightattendants.json");

            List<Pilot> deserializedPilots = DeserializeFromJsonPilots("pilots.json");
            foreach (var p in deserializedPilots)
            {
                Console.WriteLine("Deserialized pilot: {0}, Salary: {1}$, Position: {2}, Tier: {3}",
                    p.FullName, p.Salary, p.Position, p.Tier);
            }
            List<FlightAttendant> deserializedAttendants = DeserializeFromJsonAttendants("flightattendants.json");
            foreach (var f in deserializedAttendants)
            {
                Console.WriteLine("Deserialized attendant: {0}, Salary: {1}$, Position: {2}, Languages Spoken: {3}",
                f.FullName, f.Salary, f.Position, string.Join(", ", f.LanguagesSpoken));
            }
        }
        public static string GetPilotTier(int flightHours)
        {
            if (flightHours <= 2)
            {
                return "low";
            }
            else if (flightHours > 2 && flightHours <= 5)
            {
                return "mid";
            }
            else
            {
                return "top";
            }    
        }
        public static int GetValidFlightHours()
        {
            Console.Write("Enter duration of the flight (in hours): ");
            string inputHoursCheck = Console.ReadLine();
            if (Regex.IsMatch(inputHoursCheck, @"^\d+$"))
            {
                int flightHours = Convert.ToInt32(inputHoursCheck);
                return flightHours;
            }
            else
            {
                throw new Exception("Invalid input. Please enter an integer.");
            }
        }
        public static double CalculateFuelCost(int flightHours)
        {
            double fuelCostPerHour = 3000;
            double totalFuelCost = flightHours * fuelCostPerHour;
            return totalFuelCost;
        }
        public static double CalculateTotalPrice(List<Pilot> pilotList, List<FlightAttendant> flightAttendantList, int flightHours)
        {
            double totalPrice = 0;
            foreach(var p in pilotList)
            {
                totalPrice += p.CalculateSalary();
            }
            foreach(var f in flightAttendantList)
            {
                totalPrice += f.CalculateSalary();
            }
            totalPrice += CalculateFuelCost(flightHours);
            return totalPrice;
        }
        public static void AddPilotToList(List<Pilot> pilotList, string fullName, double salary, string position, string tier)
        {
            Pilot pilot = new Pilot(fullName, salary, position, tier);
            pilotList.Add(pilot);
            pilot.CalculateSalary();
        }
        public static void AddAttendantToList(List<FlightAttendant> flightAttendantList, string fullName, double salary, string position, List<string> languagesSpoken)
        {
            FlightAttendant flightAttendant = new FlightAttendant(fullName, salary, position, languagesSpoken);
            flightAttendantList.Add(flightAttendant);
            flightAttendant.CalculateSalary();
        }
        public static void SerializeToJsonPilots(List<Pilot> dataList, string filePath)
        {
            filePath = "pilots.json";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Pilot>));
                serializer.WriteObject(fileStream, dataList);
            }
        }
        public static List<Pilot> DeserializeFromJsonPilots(string filePath)
        {
            List<Pilot> pilotList = new List<Pilot>();
            filePath = "pilots.json";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Pilot>));
                pilotList = (List<Pilot>)serializer.ReadObject(fileStream);
            }
            return pilotList;
        }
        public static void SerializeToJsonAttendants(List<FlightAttendant> dataList, string filePath)
        {
            filePath = "flightAttendant.json";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<FlightAttendant>));
                serializer.WriteObject(fileStream, dataList);
            }
        }
        public static List<FlightAttendant> DeserializeFromJsonAttendants(string filePath)
        {
            List<FlightAttendant> flightAttendantList = new List<FlightAttendant>();
            filePath = "flightAttendant.json";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<FlightAttendant>));
                flightAttendantList = (List<FlightAttendant>)serializer.ReadObject(fileStream);
            }
            return flightAttendantList;
        }
    }
}
//	Створити додатковий проєкт для юніт тестування.