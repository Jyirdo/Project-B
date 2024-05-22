public class Menu
{
    public static void Start()
    {
        int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
        Console.Write(Greeting.ShowGreeting(currenthour));
        Console.WriteLine("scan de barcode op uw entreebewijs en druk op ENTER.");
        Console.WriteLine("Toets 'H' en druk ENTER voor hulp.");
        Console.WriteLine("Toets 'Q' en druk ENTER om het programma af te sluiten.");

        while (true)
        {
            string input = Console.ReadLine();
            if (input == "z") //CheckInReservationJson(input)
            {
                Console.WriteLine("Toets 'T' en druk ENTER om de starttijd van uw rondleiding te zien.");
                Console.WriteLine("Toets 'A' en druk ENTER om uw rondleiding te annuleren.");
                Console.WriteLine("Toets 'H' en druk ENTER voor hulp.");
                Console.WriteLine("Toets 'Q' en druk ENTER om het programma af te sluiten.");

                string input2 = Console.ReadLine();
                switch (input2.ToLower())
                {
                    case "t":
                        {
                            Console.WriteLine("Scan de barcode op uw entreebewijs en druk op ENTER.");
                            long input3 = Convert.ToInt64(Console.ReadLine());
                            Console.WriteLine(Tour.GetTourTime(input3));
                            break;
                        }
                    case "a":
                        {
                            Console.WriteLine("Enter the id of the Tour you want to edit");
                            int tourNumber = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Type de barcode van de bezoeker die u wilt verwijderen");
                            string barcode = Console.ReadLine(); 
                            Add_Remove.Remove(new Visitor(Convert.ToInt64(barcode)), tourNumber);
                            break;
                        }
                    case "h":
                        {
                            Help.ShowHelp();
                            break;
                        }
                    case "p":
                        {
                            Staff staff = new Staff();  // Fix staff methods to be static
                            staff.StaffMainMenu();
                            break;
                        }
                    case "q":
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                            continue;
                        }
                }
            }
            else if (long.TryParse(input, out long barcode))
            {
                SelectTour.Select_A_Tour(barcode);
            }
            else
            {
                switch (input.ToLower())
                {
                    case "h":
                        {
                            Help.ShowHelp();
                            break;
                        }
                    case "p":
                        {
                            Staff staff = new Staff();  // Fix staff methods to be static
                            staff.StaffMainMenu();
                            break;
                        }
                    case "q":
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                            continue;
                        }
                }
            }
        }
    }
}