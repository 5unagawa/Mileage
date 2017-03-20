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
            Boolean loop = true;
            Car newCar;
            string input; //Used to read user inputs
///
            Console.WriteLine("----5unagawa's Fuel Logger----");
            while (loop)
            {
                Console.WriteLine("OPEN a profile (O) / CREATE a profile (C)");
                input = Console.ReadLine();
                if (input.Equals("O", StringComparison.CurrentCultureIgnoreCase))
                {
                    //open the profile
                    string[] profile = GetProfile();
                    newCar = new Car(profile);
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

                    newCar = new Car(details);
                    Console.WriteLine("Car created.");
                    newCar.GetInfo();
                    loop = false;
                }

                //Press any key to continue...
                Console.ReadKey();
            }
        }
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
            foreach (string a in filePaths)
            {
                Console.WriteLine(filePaths);
            }

            //Open selected file and insert information into Car object
            while (loop)
            {
                input = Console.ReadLine();
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(input);
                    System.Console.WriteLine($"File opened: {input}");
                    info = new string[6];

                    for (int i = 0; i <= 5; i++)
                    {
                        info[i] = file.ReadLine();
                        i++;
                    }
                    file.Close();
                    return info;
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine($"Was unable to open file at '{input}'");
                }
            }
            //Should never get to this point in method.
            info = new string[1];
            info[1] = "ERROR: Broke out of loop."];
            return info;
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

        public Car()
        {
            make = "Unknown";
            model = "Unkown";
            year = 0;
            odometer = 0;
            capacity = 0;
            serviceInt = 0;
            fuelLevel = 0;
        }

        public Car(string[] info)
        {
            make = info[0];
            model = info[1];
            year = Convert.ToInt32(info[2]);
            odometer = Convert.ToInt32(info[3]);
            capacity = Convert.ToInt32(info[4]);
            serviceInt = Convert.ToInt32(info[5]);
            fuelLevel = 0;
        }

        //Methods
        public void GetInfo()
        {
            Console.WriteLine("----Car Details----");
            Console.WriteLine($"Make: " + "{0}" + "\nModel: '{1}'" + "\nYear: {2}" + "\nOdometer: {3}km" + "\nFuel Capacity: {4}L" + "\nService Interval: {5}km", make, model, year, odometer, capacity, serviceInt);
        }
    }
}
