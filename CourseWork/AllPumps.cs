using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    public class AllPumps
    {
        public List<GasPump> pumps = new List<GasPump>();
        public AllPumps(int numberOfPumps)
        {
            for (int i = 0; i < numberOfPumps; i++)
            {
                pumps.Add(new GasPump());  // For the number entered as numberOfPumps the loop cycles round creating one new pump each time
            }
        }

        public int CalculateTotalServices()  ///Calculates the total number of services across the entirety of the applications run time
        {
            int sum = 0;
            foreach (GasPump pump in pumps)
            {
                sum += pump.NumberOfTimesOccupied; // Tallys the total services of each pump
            }

            return sum;
        }
        public string WriteOutAllFuelDispensed()// Purely used for printing the data to the screen. It runs through each gas pump, takes the total fuel dispensed and prints it
        {

            int Diesel = 0; int LPG = 0; int Unleaded = 0;
            foreach (GasPump pump in pumps)
            {
                Diesel += pump.LitresOfDieselFuelDispensed; // This runs through each pump and tallys all the litres of fuel dispensed
                LPG += pump.LitresOfLPGFuelDispensed;
                Unleaded += pump.LitresOfUnleadedFuelDispensed;
            }
            return ($"Litres of fuel as follows: Diesel {Diesel} Litres, LPG {LPG} Litres, Unleaded {Unleaded} Litres");
        }

        public int TotalofAllFuelsLitresDispensed()
        {
            int Diesel = 0; int LPG = 0; int Unleaded = 0;
            foreach (GasPump pump in pumps)
            {
                Diesel += pump.LitresOfDieselFuelDispensed; // We cycle through each pump and add the total litres of fuel dispensed to the Diesel variable
                LPG += pump.LitresOfLPGFuelDispensed;
                Unleaded += pump.LitresOfUnleadedFuelDispensed;
            }
            int total = Diesel + LPG + Unleaded;
            return total; // This returns how many litres of fuel in total has been dispensed

        }

        public bool LaneAvailable(int KeyPressed)
        {
            //This is to ensure cars can't be be moved if a certain location is occupied or in intermission
            if( KeyPressed == 0 && (pumps[2].IsOccupied || pumps[2].InIntermission))// Checks if third pump is blocked when you try to move to the second
            {
                return false; // If so it doesn't let the person make a vehicle
            }
            if(KeyPressed == 0 && (pumps[1].IsOccupied || pumps[1].InIntermission))
            {
                return false;
            }
            if(KeyPressed == 1 && (pumps[2].IsOccupied || pumps[2].InIntermission))
            {
                return false;
            }
            if (KeyPressed == 3 && (pumps[5].IsOccupied || pumps[5].InIntermission))
            {
                return false;
            }
            if (KeyPressed == 3 && (pumps[4].IsOccupied || pumps[4].InIntermission)) 
            {
                return false;
            }
            if (KeyPressed == 4 && (pumps[5].IsOccupied || pumps[5].InIntermission))
            {
                return false;
            }
            if (KeyPressed == 6 && (pumps[8].IsOccupied || pumps[8].InIntermission))
            {
                return false;
            }
            if (KeyPressed == 6 && (pumps[7].IsOccupied || pumps[7].InIntermission))
            {
                return false;
            }
            if (KeyPressed == 7 && (pumps[8].IsOccupied || pumps[8].InIntermission))
            {
                return false;
            }
            return true;

        }


        public void UpdateTimer()
            /// Update timer goes through each gas pump on each run through of the program, checking all time related comparions and when the pumps need to be distributing fuel/when they need to finish
        {
            foreach (GasPump pump in pumps)
            {
                int intermissionUp = (pump.GetIntermissionStartTime.AddMilliseconds(pump.GetIntermissionFinishTime)).CompareTo(DateTime.Now); // We want to check if the time has passed
                if (intermissionUp <= 0 && pump.InIntermission) // If the time has passed we want to make it so the pump isn't in intermission, but is now occupied and start its timer
                {
                    
                    pump.ChangeIntermission();
                    pump.ChangeOccupancy();
                    pump.InitiateTimer();
                }
                if (pump.CheckTimer && !pump.InIntermission) // This is to check if the pump is in progress of distributing fuel and we're not in intermission
                {
                    int result = (pump.GetStartTime.AddMilliseconds(CalculateTimeToFuel(pump.GetDockedVehicleMaxCapacity,pump.VehicleFuelStartingCapacity))).CompareTo(DateTime.Now);
                    if (result <= 0 )// We want to check if the time taken for it to fuel up has passed or not, if so we want to do the following
                    {

                        pump.ChangeOccupancy(); // Make it no longer occupied
                        if(pump.GetFuelToDispense == "Unleaded") // Here we are adding the amount of fuel that was dispensed, depending on the fuel we set the pump to when we transferred the vehicles data to the pump
                        {
                            pump.AddUnleadedLitresDispensed = pump.LitresOfUnleadedFuelDispensed + CalculateLitresDispensed(pump.GetDockedVehicleMaxCapacity, pump.VehicleFuelStartingCapacity);
                        }
                        else if (pump.GetFuelToDispense == "Diesel")
                        {
                            pump.AddDieselLitresDispensed = pump.LitresOfDieselFuelDispensed + CalculateLitresDispensed(pump.GetDockedVehicleMaxCapacity, pump.VehicleFuelStartingCapacity);
                        }
                        else if (pump.GetFuelToDispense == "LPG")
                        {
                            pump.AddLPGLitresDispensed = pump.LitresOfLPGFuelDispensed + CalculateLitresDispensed(pump.GetDockedVehicleMaxCapacity, pump.VehicleFuelStartingCapacity);
                        }
                        pump.SetTimer = false; // Make sure when we next call updatetimer, that we dont run through this again
                    }
                }
                
            }

        }

        public int CalculateTimeToFuel(int MaxCapacity, int StartCapacity)
        {
            double AmountToRefuel = MaxCapacity - StartCapacity;
            double TimeinSeconds = AmountToRefuel / 1.5;
            int Seconds = Convert.ToInt32(TimeinSeconds); 
            return Seconds*1000; // Converting it to milliseconds
            
        }
        public int CalculateLitresDispensed(int MaxCapacity, int StartCapacity)
        {
            double AmountToRefuel = MaxCapacity - StartCapacity;
            int Litres = Convert.ToInt32(AmountToRefuel);
            return Litres;
        }


    }
}
