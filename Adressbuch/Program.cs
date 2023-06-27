using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Adressbuch
{
    public class App
    {
        private DataModel dataModel;

        static void Main()
        {
            App app = new App();
            app.StartProgram();
        }

        void StartProgram()
        {
            Console.WriteLine("Willkommen zum Adressbuch!");
            this.dataModel = new DataModel();
            bool running = true;
            while (running)
            {
                Command command = GetCommandFromUI();
                switch (command)
                {
                    case Command.SHOWALL:
                        Console.WriteLine("Show all");
                        HandleShowAll();
                        break;
                    case Command.SHOWBYID: 
                        Console.WriteLine("Show by id");
                        HandleShowOneById();
                        break;
                    case Command.ADD: 
                        Console.WriteLine("Add");
                        break;
                    case Command.REMOVE:
                        Console.WriteLine("Remove");
                        break;
                    case Command.UPDATE: 
                        Console.WriteLine("Update");
                        break;
                    case Command.EXIT:
                        Console.WriteLine("Exit");
                        running = false;
                        break;

                }
            }
        }

        public void HandleShowAll()
        {
            IOInteraction.PrintAllAddressesToConsole(dataModel);
        }

        public void HandleShowOneById()
        {
            //dataModel.AadAddress(new Address(1, "dielsdorf"));
            Predicate<int> predicate = address =>  true;    
            int validId = IOInteraction.GetValidNumber(predicate);
            Address address = dataModel.getAddressById(validId);
            Console.WriteLine($"Id {validId} chosen");
            //print to console
        }

        private Command GetCommandFromUI()
        {
            return IOInteraction.GetCommandFromConsole();
        }

        
    }

    public static class IOInteraction
    {
        private static void PrintEnumToConsole()
        {
            Console.WriteLine("---------------------");
            int index = 1;
            foreach (Command item in Enum.GetValues(typeof(Command)))
            {
                Console.WriteLine(index + " " + item);
                index++;
            }
            Console.WriteLine("---------------------");
        }

        public static Command GetCommandFromConsole()
        {
            Console.WriteLine("Choose what you want.");
            PrintEnumToConsole();
            Predicate<int> validNumbersForCommands = (int numberToCheck) => { return numberToCheck > 0 && numberToCheck <= Enum.GetNames(typeof(Command)).Length; };
            return (Command)GetValidNumber(validNumbersForCommands);
        }

        public static int GetValidNumber(Predicate<int> predicate)
        {
            int potentialValidNumber = GetIntFromConsole();
            bool validNumber = false;
            while (!validNumber)
            {
                if(predicate(potentialValidNumber))
                {
                    validNumber = true;
                }
                else
                {
                    return GetValidNumber(predicate);
                }
            }
            return potentialValidNumber;
        }

        private static int GetIntFromConsole()
        {
            string inputAsString = Console.ReadLine();
            if (Int32.TryParse(inputAsString, out int number))
            {
                return number;
            }
            else
            {
                return GetIntFromConsole();
            }
        }

        public static void PrintAllAddressesToConsole(DataModel data)
        {
            //data.AadAddress(new Address(1, "dielsdorf"));
            data.GetAllAddresses().ForEach(address => PrintAddressToConsole(address));
        }

        public static void PrintAddressToConsole(Address address)
        {
            Console.WriteLine(address.ToString());
        }
    }

    public class DataModel
    {
        List<Address> addressList;

        public DataModel()
        {
            addressList = new List<Address>();
        }

        public List<Address> GetAllAddresses()
        {
            return addressList;
        }

        public void AadAddress(Address address)
        {
            addressList.Add(address);
        }

        public int AmountOfAddresses()
        {
            return addressList.Count;
        }

        public Address getAddressById(int id)
        {
            try
            {
                Console.WriteLine("here");
                return addressList.Where(address => address.Id == id).ToList()[0];
            } catch (Exception ex) {
                throw ex;
            } 
        }
    }

    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }

        public Address(int id, string city)
        {
            this.Id = id;   
            this.City = city;
        }

        public override string? ToString()
        {
            return $"Id: {Id}; City: {City}";
        }
    }

    public enum Command
    {
        SHOWALL=1,
        SHOWBYID=2,
        ADD=3,
        REMOVE=4,
        UPDATE=5,
        EXIT=6
    }
}



