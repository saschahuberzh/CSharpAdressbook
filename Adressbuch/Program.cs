namespace Adressbuch
{
    public class App
    {

        static void Main()
        {
            App app = new App();
            app.StartProgram();
        }

        void StartProgram()
        {
            Console.WriteLine("Willkommen zum Adressbuch!");
            bool running = true;
            while (running)
            {
                GetCommandFromUI();
            }
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
            return (Command)GetValidNumber();
        }

        private static int GetValidNumber()
        {
            int potentialValidNumber = GetIntFromConsole();
            bool validNumber = false;
            while (!validNumber)
            {
                if (IsNumberValid(potentialValidNumber))
                {
                    validNumber = true;
                }
                else
                {
                    return GetValidNumber();
                }
            }
            return potentialValidNumber;
        }

        private static bool IsNumberValid(int numberToCheck)
        {
            if (numberToCheck > 0 && numberToCheck <= Enum.GetNames(typeof(Command)).Length)
            {
                return true;
            }
            return false;
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



