using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileageLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean loop = true;                    //Program will loop while TRUE
            Car newCar = new MileageLog.Car();      //New Car object
            string input;                           //Used to read user inputs

            Console.WriteLine("----5unagawa's Fuel Logger----");
            while (loop)
            {
                Console.WriteLine("OPEN a profile (O) / CREATE a profile (C)");
                input = Console.ReadLine();
                if (input.Equals("O", StringComparison.CurrentCultureIgnoreCase))
                {
                    //open the profile
                    string[] profile = GetProfile();
                    newCar.Update(profile);
                    Console.WriteLine("Profile loaded.");
                    newCar.GetInfo();
                    loop = false;
                }
                else if (input.Equals("C", StringComparison.CurrentCultureIgnoreCase))
                {
                    string[] details = new string[6];

                    Console.Write("Enter make: ");
                    details[0] = Console.ReadLine();
                    Console.Write("Enter model: ");
                    details[1] = Console.ReadLine();
                    Console.Write("Enter year: ");
                    details[2] = Console.ReadLine();
                    Console.Write("Enter odometer: ");
                    details[3] = Console.ReadLine();
                    Console.Write("Enter fuel capacity: ");
                    details[4] = Console.ReadLine();
                    Console.Write("Enter service interval: ");
                    details[5] = Console.ReadLine();

                    newCar.Update(details);
                    SaveProfile(newCar);
                    newCar.GetInfo();
                    loop = false;
                }
            }
            loop = true;
            while (loop)
            {
                Console.WriteLine("FILL (F)");
                input = Console.ReadLine();
                if (input.Equals("F", StringComparison.CurrentCultureIgnoreCase))
                {
                    newCar.Fill();
                    SaveProfile(newCar);
                }
            }
                //Press any key to continue...
                Console.ReadKey();
        }

        /// <summary>
        /// Opens the selected profile.
        /// </summary>
        static string[] GetProfile()
        {
            Boolean loop = true;
            string directory;   //Current working directory
            string input;       //String containing user input
            string[] info;      //String array containing information about Car object
            string[] filePaths; //String array of profile files stored in current working directory

            //Display profiles in directory
            Console.WriteLine("Select a profile to open:");
            directory = System.IO.Directory.GetCurrentDirectory();
            filePaths = System.IO.Directory.GetFiles(directory, "*.txt");
            
            foreach (string entry in filePaths)
            {
                Console.WriteLine(entry);
            }

            //Open selected file and insert information into Car object
            while (loop)
            {
                input = Console.ReadLine();
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(input);
                    info = new string[8];

                    for (int i = 0; i <= 7; i++)
                    {
                        info[i] = file.ReadLine();
                    }
                    file.Close();
                    System.Console.WriteLine($"File opened: {input}");
                    return info;
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine($"Was unable to open file at '{input}. Please try again.'");
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    Console.WriteLine($"Was unable to open file at '{input}. Please try again.'");
                }
            }
            //Should never get to this point in method.
            info = new string[1];
            info[1] = "ERROR: Broke out of loop.";
            return info;
        }
        
        /// <summary>
        /// Writes information to a file.
        /// </summary>
        static void SaveProfile(Car myCar)
        {
            string filename;
            string[] info = new string[8];

            info[0] = myCar.make;
            info[1] = myCar.model;
            info[2] = myCar.year.ToString();
            info[3] = myCar.odometer.ToString();
            info[4] = myCar.capacity.ToString();
            info[5] = myCar.serviceInt.ToString();
            info[6] = myCar.fuelLevel.ToString();
            info[7] = myCar.getCarID();

            filename = info[0] + info[1] + info[2] + ".txt";
            System.IO.File.WriteAllLines(filename, info);
            Console.WriteLine($"Profile updated: {filename}");
        }
    }

    public class Car
    {
        //Fields
        public decimal fuelLevel;   //Current amount of fuel in car
        
        public int capacity;        //Fuel tank capcity
        public int odometer;        //Current odometer reading
        public int serviceInt;      //Service interval
        public int year;            //Year of manufacture
            
        public string make;
        public string model;
        private string uID;   //Unique identifier

        public Car()
        {
            make = "Unknown";
            model = "Unkown";
            year = 0;
            odometer = 0;
            capacity = 0;
            serviceInt = 0;
            fuelLevel = 0;
            uID = GenerateID();
        }

        //Methods

        /// <summary>
        /// User inputs odometer reading, amount of fuel purchased and price per litre.
        /// Fuel economy is calculated and Car's odometer is updated.
        /// fillAmount and price passed to Log method for recording.
        /// </summary>
        public void Fill()  //assuming complete fill for now 
        {
            int newOdo;             //Odometer reading at time of refill
            decimal fillAmount;     //Quantity of fuel purchased
            decimal price;          //Price per litre
            decimal mileage;
            //get odometer
            //get amount purchased
            //calculate mileage
            //update values

            Console.WriteLine("odometer");
            newOdo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("amount");
            fillAmount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("price per litre");
            price = Convert.ToDecimal(Console.ReadLine());

            mileage = (fillAmount / (newOdo - odometer)) * 100;
            Console.WriteLine($"You are using {Decimal.Round(mileage, 2)}L/100KM");

            //update mileage
            odometer = newOdo;
            Log(fillAmount, price);
        }
                
        /// <summary>
        /// Retrieves a Car object's unique identifier.
        /// </summary>                   
        public string getCarID() {
            string carID = uID;
            return carID;
        }
            
        /// <summary>
        /// Prints information about a Car object.
        /// </summary>
        public void GetInfo()
        {
            Console.WriteLine("----Car Details----");
            Console.WriteLine($"Make: {make}\nModel: {model}\nYear: {year}\nOdometer: {odometer}km\nFuel Capacity: {capacity}L\nService Interval: {serviceInt}km");
            Console.WriteLine($"Car ID: {uID}");
        }

        /// <summary>
        /// Updates information about a Car object using user inputs.
        /// </summary
        public void Update(string[] info)
        {
            make = info[0];
            model = info[1];
            year = Convert.ToInt32(info[2]);
            odometer = Convert.ToInt32(info[3]);
            capacity = Convert.ToInt32(info[4]);
            serviceInt = Convert.ToInt32(info[5]);

            //add fuel level and Car ID if profile is being loaded
            if (info.Length == 8)
            {
                fuelLevel = Convert.ToDecimal(info[6]);
                uID = info[7];
            }
        }
        
        /// <summary>
        /// Generates a unique identifier for a Car object.
        /// </summary
        private string GenerateID()
        {
            string id;
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            id = string.Format("{0:X}", i - DateTime.Now.Ticks);
            Console.WriteLine(id);
            return id;
        }

        /// <summary>
        /// Saves the details of the refuel to a unique log file.
        /// If the file does not exist, one will be created.
        /// </summary>
        private void Log(decimal fA, decimal ppl) {
            decimal fuelAmt = fA;                   //Amount of fuel purchased
            decimal price = ppl;                    //Price per litre
            string path = $"logs/{uID}.txt";        //Path using Car's uID
            DateTime currdate = DateTime.Today;     //Today's date
            
            //Append info to file
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine(currdate.ToString("d") + " " + odometer + " " + fuelAmt + " " + price);
                }   
            }

            else
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                {
                    sw.WriteLine(currdate.ToString("d") + " " + odometer + " " + fuelAmt + " " + price);
                }    
            }
            Console.WriteLine("Refuell logged successfully.");
        }
    }
}
