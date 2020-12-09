using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    public class VehicleList
    {
        DateTime vehicleGenTime;
        int totalLeaves;
        int CreateCarTime;
        public List<Vehicles> VehicleQueue = new List<Vehicles>();

        public VehicleList(int numberOfVehicles) 
        {

            VehicleQueue.Add(new Vehicles());

        }
        
        public void AddVehicleToQueue()
        {
            int result = (vehicleGenTime.AddMilliseconds(CreateCarTime)).CompareTo(DateTime.Now); // Compare the time of when the car needs to finish to the current time
            if (result <= 0 & NumberOfVehiclesInList() < 5) // Prevents us exceeding 5 vehicles
            {
                vehicleGenTime = DateTime.Now; // Take a time stamp of when the vehicle was instanced
                VehicleQueue.Add(new Vehicles()); // Create a new instance of a vehicle and add it to the vehicle queue
                SetCarGenTimer();

            }
        }
        public int CalculateTotalEarlyLeaves() //Return the number of total leaves
        {
            int x = totalLeaves;
            return x;
        }

        public void RemoveVehicleFromQueue() // To remove the first vehicle from the queue
        {
            VehicleQueue.RemoveAt(0);
        }

        public int NumberOfVehiclesInList()  // Goes through the vehicle list and counts the number of vehicles
        {
            int sum = 0;
            foreach (Vehicles vehicle in VehicleQueue)
            {
                sum += 1;
            }

            return sum;
        }
        public void TypeOfVehicles()  // Cycles through each vehicles and gets the type of the car
        {
            Console.WriteLine($"Queueing Vehicles:");
            foreach (Vehicles vehicle in VehicleQueue)
            {
                Console.Write($"{vehicle.GetCarType} ");
            }
            Console.WriteLine();

        }
        public void SetCarGenTimer() // Used to set a random time for when a car will be created
        {
            Random rnd = new Random();
            int value = rnd.Next(1500, 2200);
            CreateCarTime = value;
        }
        public void UpdateAgitiation()
            // This method checks whether a vehicle has exceeded the 4.5 second time it can be in a queue for before it leaves.
        {
            if(VehicleQueue.Count > 0) // Want to make sure we have a vehicle to compare against, so we need the queue to be greater than 0
            {
                List<int> TempListForRemoval = new List<int>(); // Temp list to remove vehicles that leave
                for (int i = 0; i < VehicleQueue.Count; i++) // Cycle through the vehicle list for how many vehicles are in it
                {
                    int result = (VehicleQueue[i].GetStartTime.AddMilliseconds(VehicleQueue[i].GetAgitiationNumber)).CompareTo(DateTime.Now);
                    if (result <= 0) // if the car exceeds the agitiation timer
                    {
                        TempListForRemoval.Add(i);  // Add the car to the temporary list for removal
                    }

                }
                TempListForRemoval.Reverse(); // We want to reverse the list so we dont change the queue
                foreach(int i in TempListForRemoval)
                {
                    VehicleQueue.RemoveAt(i); // Now we remove the cars from the main queue
                    totalLeaves += 1; // Tally the leaves
                }

            }
            
        }


        public int getFirstVehicleFuelCapacity()  /// Gets the fuel capacity of the first vehicle in the vehicle queue and returns it
        {

            for (int i = 0; i < 1;)
            {
                return VehicleQueue[i].GetTankCapacity;
            }
            return 0;
        }
        public string getFirstVehicleFuelType() /// Gets the fuel type of the first vehicle in the vehicle queue list
        {

            for (int i = 0; i < 1;)
            {
                return VehicleQueue[i].GetFuelType;
            }
            return "Unleaded";
        }

        public int getFirstVehicleMaxfuelCap() /// Get the first vehicles max fuel capacity.
        {
            for (int i = 0; i < 1;)
            {
                if (VehicleQueue[i].GetCarType == "Car")
                    return 50;
                else if (VehicleQueue[i].GetCarType == "HGV")
                    return 150;
                else if (VehicleQueue[i].GetCarType == "Van")
                    return 80;
            }
            return 50;
        }
    }
}
