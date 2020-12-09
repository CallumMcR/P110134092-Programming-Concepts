using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace CourseWork
{
    class Application
    {

        
        public void Run()
        {
            
            LogInScreen();
            
            double fuelCost = 5.40;
            Console.Clear();
            AllPumps PumpList = new AllPumps(9); // Creating our 9 gas pumps
            VehicleList VehicleQueue = new VehicleList(1); // We want to generate the vehicle list and add atleast one vehicle to it
            bool priceValidation = true;
            while(priceValidation) // Validation for user input in the price of fuel per litre
            {
                double test;
                Console.WriteLine("Please enter the price per litre of fuel in this format (currency is in pounds): 00.00");
                if (double.TryParse(Console.ReadLine(), out test))  // To check if what is entered is actually a double so we can perform calculations
                {
                    fuelCost = test;  // We set fuelcost to what has been confirmed as a double
                    priceValidation = false; // Finish the loop
                }
                Console.Clear();

            }
            
            DateTime WorkStartTime = DateTime.Now; // Take the time the user started their shift at
            bool program = false;
            while (!program)
            {
                int id = 1;
                int column = 1;
                Console.WriteLine(); Console.WriteLine("User: Callum"); Console.WriteLine();
                foreach (GasPump pump in PumpList.pumps)
                {
                    if (column == 4)
                    {
                        Console.WriteLine(); Console.WriteLine(); column = 1;

                    }
                    if (pump.InIntermission)  // Displays the pump in intermission
                    {
                        Console.Write($"{id} Intermission".PadLeft(25));
                    }
                    else if (pump.IsOccupied)
                    {
                        Console.Write($"{id} Unavailable".PadLeft(25));
                    }
                    else
                    {
                        Console.Write($"{id} Available".PadLeft(25));
                    }
                    id++;
                    column++;
                }
                Console.WriteLine(); Console.WriteLine();
                VehicleQueue.TypeOfVehicles();  // Displays the list of cars in the queue
                Console.WriteLine("Press the according number on your keyboard to let a vehicle go to it or press escape to finish");
                PumpList.UpdateTimer(); // This updates near to all timer related things at the start of the turn
                
                VehicleQueue.AddVehicleToQueue(); // Will add vehicles to queue, has time related checks also
                VehicleQueue.UpdateAgitiation(); // Update agitiation after so we don't remove the first vehicle before we check its fuel type
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo KeyInput = Console.ReadKey();
                    {
                        if (KeyInput.Key != ConsoleKey.Escape && VehicleQueue.NumberOfVehiclesInList() > 0) // Make sure we have atleast one vehicle before we try to assign one to a pump.
                        {

                            int i;
                            if (int.TryParse(KeyInput.KeyChar.ToString(), out i) && i != 0)  // Check that what has been pressed on the keyboard is a number and that its not 0 ( we dont have a pump 0)
                            {
                                int PumpSelect= Convert.ToInt32(KeyInput.KeyChar.ToString()); 
                                PumpSelect -= 1;  // converting it to number in elements
                                if (!PumpList.pumps[PumpSelect].IsOccupied && !PumpList.pumps[PumpSelect].InIntermission && PumpList.LaneAvailable(PumpSelect))
                                    // Making sure the pump isn't occupied in intermission and that its actually available/not blocked
                                {
                                    //This is transfering near to all of the vehicles data type to the pump so we can record how much of each fuel needs to added and documented
                                    PumpList.pumps[PumpSelect].SetIntermissionStartTime = DateTime.Now;  // We take a timestamp of the time when the pump beginings intermission
                                    PumpList.pumps[PumpSelect].SetVehicleStartCapacity = VehicleQueue.getFirstVehicleFuelCapacity(); // We transfer the first vehicles current fuel capacity to the pump
                                    PumpList.pumps[PumpSelect].SetDockedVehicleMaxCapacity = VehicleQueue.getFirstVehicleMaxfuelCap(); // We get the max capacity of the vehicle and tell the pump what it is
                                    PumpList.pumps[PumpSelect].SetIntermissionFinishTime = GetRandomNumberInRange(2000, 3000); // We set a random time for how long the intermission takes
                                    PumpList.pumps[PumpSelect].FuelToDispense = VehicleQueue.getFirstVehicleFuelType(); // Set the gas pumps fuel dispense type
                                    PumpList.pumps[PumpSelect].ChangeIntermission(); // We want to tell the gas pump to change to in intermission after we have set the finish time and the start time stamps
                                    VehicleQueue.RemoveVehicleFromQueue(); // Once the transfer of data to the pump is done, we want take that vehicle out of the queue



                                }
                            }


                            


                        }
                        else if (KeyInput.Key == ConsoleKey.Escape) // End program and display 
                        {

                            Console.Clear();
                            TimeSpan HoursOnDuty = DateTime.Now.Subtract(WorkStartTime); // Calculating the time the user has been on duty for
                            /// We store this as a string in order to be able to use it later down the line for exporting it to a file also
                            string Data =
                            $@"Total Number of services:  {PumpList.CalculateTotalServices()}
                            Total number of early leaves {VehicleQueue.CalculateTotalEarlyLeaves()}
                            Numbers of hours and minutes on the job:  {HoursOnDuty.Hours}hrs {HoursOnDuty.Minutes}mins
                            Your 1% commission: £{String.Format("{0:0.0#}", ((fuelCost * PumpList.TotalofAllFuelsLitresDispensed()) * 0.01))}
                            {PumpList.WriteOutAllFuelDispensed()}
                            Amount of money made from selling fuel: £ {(fuelCost * PumpList.TotalofAllFuelsLitresDispensed())}
                            Employee wage was £{String.Format("{0:0.0#}", 5.9 *HoursOnDuty.Hours)} + 1% commission of £{String.Format("{0:0.0#}", ((fuelCost * PumpList.TotalofAllFuelsLitresDispensed()) * 0.01))}
                            Employee was Callum";
                            
                            

                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine(Data); // Here is where we display the data and counters to just the user
                                Console.WriteLine($"Do you wish to log out, or do another shift? Enter Logout to log out or another to start a new shift");
                                string enteredShift = Console.ReadLine();
                                if (enteredShift.ToLower() == "logout") // Error prevention for accidental caps lock
                                {
                                    Console.WriteLine("You have been logged out and a file has been provided with all your details");
                                    WriteToFile(Data);
                                    //Here is where we export the data to a txt file
                                    Environment.Exit(0);// Stops application
                                }
                                else if (enteredShift.ToLower() == "another")// Re-runs the program for the user to do again
                                {
                                    Run();
                                }
                            }
                            
                        }
                    }
                }
                Thread.Sleep(100);
                Console.Clear();
            }
            

        }

        public int GetRandomNumberInRange(int min, int max) // Simple random number generator for getting random vehicle times
        {
            Random rnd = new Random();
            int value = rnd.Next(min, max+1);
            return value;
        }


        public void LogInScreen()
        {
            string userName = "Callum123";
            string password = "123";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter your username: ");
                string enteredUserName = Console.ReadLine();
                if (enteredUserName == userName)
                {
                    Console.WriteLine("Please enter your password: ");
                    string enteredPassword = Console.ReadLine();
                    if(enteredPassword == password)
                    {
                        Console.WriteLine("Welcome! We hope you have a good shift");
                        Thread.Sleep(500);

                        break;
                    }

                }

            }
        }

        public const string filename = @"G:\My Drive\Programming Concepts\Year 1\Semester 1\CourseWork\GasStationCode\Information.txt";
        public void WriteToFile(string data)
        {
            if (!File.Exists(filename))
            {
                using(StreamWriter sw = File.CreateText(filename)) // Creates a file in the directory provided then writes the data we stored in it
                {
                    sw.WriteLine(data);
                }

            }
            else if (File.Exists(filename))
            {
                using(StreamWriter sw = File.AppendText(filename))  // Adds the data from this run to the already existing file
                {
                    sw.WriteLine(data);
                }


            }
        }

    }

    
}
