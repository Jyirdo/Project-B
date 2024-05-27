public class Menu
{
    public static void MainMenu()
    {
        int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
        Console.Write(Greeting.ShowGreeting(currenthour));
        Console.WriteLine("scan de barcode op uw entreebewijs en druk op ENTER.");
        Console.WriteLine("Toets 'H' en druk ENTER voor hulp.");
        Console.WriteLine("Toets 'Q' en druk ENTER om het programma af te sluiten.");

        while (true)
        {
            string input = Console.ReadLine();
            long.TryParse(input, out long barcode);
            if (Tour.CheckIfReservation(barcode) == true) 
            {   
                SubMenu(barcode);
            }
            else if (long.TryParse(input, out long barcode2))
            {
                SelectTour.Select_A_Tour(barcode2);
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
                            StaffMenu();
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

    public static void SubMenu(long barcode)
    {
        Console.WriteLine("Toets 'T' en druk ENTER om de starttijd van uw rondleiding te zien.");
        Console.WriteLine("Toets 'A' en druk ENTER om uw rondleiding te annuleren.");
        Console.WriteLine("Toets 'H' en druk ENTER voor hulp.");
        Console.WriteLine("Toets 'Q' en druk ENTER om het programma af te sluiten.");
        while (true)
        {
            string input2 = Console.ReadLine();
            switch (input2.ToLower())
            {
                case "t":
                    {
                        Console.WriteLine(Tour.GetTourTime(barcode));
                        break;
                    }
                case "a":
                    {
                        Console.WriteLine("Enter the id of the Tour you want to edit");
                        int tourNumber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Type de barcode van de bezoeker die u wilt verwijderen");
                        string barcode1 = Console.ReadLine(); 
                        Add_Remove.Remove(new Visitor(Convert.ToInt64(barcode1)), tourNumber);
                        break;
                    }
                case "h":
                    {
                        Help.ShowHelp();
                        break;
                    }
                case "p":
                    {
                        StaffMenu();
                        break;
                    }
                case "q":
                    {
                        MainMenu();
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

    public static void StaffMenu()
    {
        while (true)
        {
            Console.WriteLine("Geef uw personeelscode op:"); 
            Console.WriteLine("Toets 'Q' om terug te gaan.");
            string staffcode = Console.ReadLine();

            if (staffcode.ToLower() == "q")
            {
                MainMenu();
            }
            else if (Staff.CorrectStaffCode(staffcode) == true)
            {
                Tour.Load_Tours();
                Console.WriteLine("Voer de ID in van de tour die u wilt selecteren.");
                Console.WriteLine("Voor advies over rondleidingen, toets A.");
                Console.WriteLine("Druk op 'Q' om terug te gaan naar het hoofdmenu.");

                string tourId = Console.ReadLine();
                if (int.TryParse(tourId, out int tourIdInt))
                {
                    while (true)
                    {
                        Console.WriteLine("Om een bezoeker toe te voegen druk op L.");
                        Console.WriteLine("Om de bezoekers in de tour te checken druk op 'C'.");
                        Console.WriteLine("Toets 'Q' om terug te gaan.");
                        string input = Console.ReadLine();
                        switch (input.ToLower())
                        {
                            case "q":
                                {
                                    StaffMenu();
                                    break;
                                }
                            case "l":
                                {
                                    Console.WriteLine("Scan de barcode van de bezoeker die u wilt toevoegen aan de rondleiding");
                                    long barcode = Convert.ToInt64(Console.ReadLine());
                                    Console.WriteLine(Staff.AddLastMinuteVisitor(barcode, tourIdInt));
                                    break;
                                }
                            case "c":
                                {
                                    Staff.SelectTourAndCheckTour(tourIdInt);
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
                else
                {
                    switch (tourId)
                    {
                        case "q":
                            {
                                MainMenu();
                                break;
                            }
                        case "a":
                            {
                                //Advise.CreateAdvise();
                                break;
                            }
                    }
                }
            }
            else
            {
                Console.WriteLine("Dat is geen correcte code.");
                continue;
            }
        }
    }
}