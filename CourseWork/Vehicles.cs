using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    public class Vehicles
    {

        string carType;
        string fuelType;
        int agitationNumber;
        DateTime startTime;
        int fuelTankCapacity;
        


        public string GetCarType
        {
            get { return carType; }
        }

        public string GetFuelType
        {
            get { return fuelType; }
        }

        public int GetAgitiationNumber
        {
            get { return agitationNumber; }
        }



        //Agitation related
        public DateTime GetStartTime
        {
            get { return startTime; }
        }

        //Fuel Tank capacity related

        public int GetTankCapacity
        {
            get { return fuelTankCapacity; }
        }

        public Vehicles() // This is for on instancing the vehicle
        {
            carType = RandomCarType();
            fuelType = RandomFuelType();
            agitationNumber = 4500;
            startTime = DateTime.Now;
            fuelTankCapacity = RandomFuelCapacity();
        }


        public int RandomFuelCapacity()
        {

            if (GetCarType == "HGV")
            {
                return GetRandomNumberInRange(0,37); // Setting the starting fuel to be at a quarter of their max tank
            }
            else if (GetCarType == "Van")
            {
                return GetRandomNumberInRange(0, 20);
            }
            else if (GetCarType == "Car")
            {
                return GetRandomNumberInRange(0, 12);
            }
            return 0;
        }
        public string RandomCarType() // Randomly select a vehicle type
        {
            string[] ArrayOfcarTypes = { "Car", "Van", "HGV" };
            return ArrayOfcarTypes[GetRandomNumberInRange(0, 2)]; // Upper integer -1 is the max
        }

        public string RandomFuelType() // Randomly selected a fuel type dependent on the vehicle type
        {
            string[] ArrayOffuelTypes = { "Unleaded", "Diesel", "LPG" };
            if (GetCarType == "HGV")
            {
                return "Diesel";
            }
            else if(GetCarType == "Van")
            {
                return ArrayOffuelTypes[GetRandomNumberInRange(1, 2)]; // Vans can only be Diesel or LPG
            }
            else if(GetCarType == "Car")
            {
                return ArrayOffuelTypes[GetRandomNumberInRange(0, 2)]; // Cars can be any fuel type
            }
            return "Error";
            
            
        }
        public int GetRandomNumberInRange(int min, int max) // Random number generator
        {
            Random rnd = new Random();
            int value = rnd.Next(min, max+1);
            return value;
        }

    }
}
