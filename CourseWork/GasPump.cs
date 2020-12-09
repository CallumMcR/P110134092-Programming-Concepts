using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CourseWork
{
    public class GasPump
    {
        bool isOccupied;
        int numberOfTimesOccupied;
        DateTime startTime; // Time stamp for when the gas pump received the vehicle
        bool timerBool; // Whether the timer is active
        /// This three are the litres of fuel dispensed of the specific fuel type

        int litresUnleadedDispensed;
        int litresDieselDispensed;
        int litresLPGDispensed;


        string fuelToDispense; // Type of fuel to dispense
        bool inIntermission; // Whether or not the vehicle is in intermission
        DateTime intermissionStartTime; // Timestamp for when the intermission began
        int intermissionFinishTime; //  when the intermission time should end
        int vehicleStartCapacity; // Vehicles current fuel capacity
        int dockedVehicleMaxCap; // Docked vehicles max fuel capacity




        public string FuelToDispense
        {
            set { fuelToDispense = value; }
        }
        public string GetFuelToDispense
        {
            get { return fuelToDispense; }
        }

        // Unleaded Fuel
        public int AddUnleadedLitresDispensed
        {
            set { litresUnleadedDispensed = value; }
        }
        public int LitresOfUnleadedFuelDispensed
        {
            get { return litresUnleadedDispensed; }
        }
        // Diesel
        public int AddDieselLitresDispensed
        {
            set { litresDieselDispensed = value; }
        }
        public int LitresOfDieselFuelDispensed
        {
            get { return litresDieselDispensed; }
        }
        //LPG
        public int AddLPGLitresDispensed
        {
            set { litresLPGDispensed = value; }
        }
        public int LitresOfLPGFuelDispensed
        {
            get { return litresLPGDispensed; }
        }


        public bool InIntermission
        {
            set { inIntermission = value; }
            get { return inIntermission; }
        }
        public bool IsOccupied
        {
            set { isOccupied = value; }
            get { return isOccupied; }
        }



        public int NumberOfTimesOccupied
        {
            get { return numberOfTimesOccupied; }
        }

        public DateTime GetStartTime
        {
            get { return startTime; }
        }


        //Intermission related
        public DateTime GetIntermissionStartTime
        {
            get { return intermissionStartTime; }
        }
        public DateTime SetIntermissionStartTime
        {
            set { intermissionStartTime = value; }
        }
        public int SetIntermissionFinishTime
        {
            set { intermissionFinishTime = value; }
        }
        public int GetIntermissionFinishTime
        {
            get { return intermissionFinishTime; }
        }
        public bool SetTimer
        {
            set { timerBool = value; }
        }
        public bool CheckTimer // Boolean for if timer is active or not
        {
            get { return timerBool; }
        }

        // Tank on its starting capacity

        public int VehicleFuelStartingCapacity
        {
            get { return vehicleStartCapacity; }
        }
        public int SetVehicleStartCapacity
        {
            set { vehicleStartCapacity = value; }
        }


        // Docked Vehicle Max Capacity
        public int GetDockedVehicleMaxCapacity
        {
            get { return dockedVehicleMaxCap; }
        }
        public int SetDockedVehicleMaxCapacity
        {
            set { dockedVehicleMaxCap = value; }
        }


        


        public GasPump()
        {
            isOccupied = false;
            startTime = DateTime.Now;
            timerBool = false;
            numberOfTimesOccupied = 0;
            litresDieselDispensed = 0;
            litresLPGDispensed = 0;
            litresUnleadedDispensed = 0;
            inIntermission = false;
        }


        public void ChangeOccupancy() // Changes the state of the gas pump to either Unavailable of Available
        {
            if (!isOccupied)
            {
                isOccupied = true;
                numberOfTimesOccupied++;


            }
            else
            {
                isOccupied = false;
            }

        }
        public void ChangeIntermission()  // Changes the state of the gas pump from Available to In intermission
        {
            if (!inIntermission)
            {
                inIntermission = true;

            }
            else
            {
                inIntermission = false;
            }

        }
        public void InitiateTimer() // Take a time stamp for when the pump started fuelling
        {
            if (!timerBool)
            {
                timerBool = true;
                startTime = DateTime.Now;

            }

        }

    }
}
