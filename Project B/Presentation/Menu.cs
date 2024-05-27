public class Menu
{
    public static void MainMenu()
    {
        while (true)
        {
            int currenthour = Convert.ToInt16(DateTime.Now.ToString("HH"));
            Console.Write(Greeting.ShowGreeting(currenthour));
            Console.WriteLine("scan de barcode op uw entreebewijs en druk op ENTER.");
            Console.WriteLine("Toets 'H' en druk ENTER voor hulp.");
            Console.WriteLine("Toets 'Q' en druk ENTER om het programma af te sluiten.");

            string input = Console.ReadLine();
            long.TryParse(input, out long barcode);
            if (Tour.CheckIfReservation(barcode) == true) 
            {   
                SubMenu(barcode);
            }
            else if (long.TryParse(input, out long barcode2))
            {
                Console.WriteLine("Toets het nummer van de rondleiding in waarvoor u zich wilt aanmelden:");
                Console.WriteLine(SelectTour.SelectATour(barcode2));
                continue;
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
        while (true)
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
                        Console.WriteLine(Tour.GetTourTime(barcode));
                        break;
                    }
                case "a":
                    {
                        Console.WriteLine(Tour.CancelReservation(barcode));
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
            Console.WriteLine("Toets 'Q' en druk ENTER om terug te gaan.");
            string staffcode = Console.ReadLine();

            if (staffcode.ToLower() == "q")
            {
                MainMenu();
            }
            else if (Staff.CorrectStaffCode(staffcode) == true)
            {
                Tour.Load_Tours();
                Console.WriteLine("Voer de ID in van de tour die u wilt selecteren en druk ENTER.");
                Console.WriteLine("Toets 'A' en druk ENTER voor advies over rondleidingen.");
                Console.WriteLine("Toets 'Q' en druk ENTER om terug te gaan.");

                string tourId = Console.ReadLine();
                if (int.TryParse(tourId, out int tourIdInt))
                {
                    while (true)
                    {
                        Console.WriteLine("Toets 'L' en druk ENTER om een bezoeker toe te voegen aan een rondleiding.");
                        Console.WriteLine("Toets 'C' en druk ENTER om de bezoekers in de tour te checken.");
                        Console.WriteLine("Toets 'Q' en druk ENTER om terug te gaan.");
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
                                    Console.WriteLine("Toets 'Q' en druk ENTER om terug te gaan.");
                                    Console.WriteLine(Staff.AddLastMinuteVisitor(tourIdInt));
                                    break;
                                }
                            case "c":
                                {
                                    Console.WriteLine("Begin met barcodes scannen om te controleren of iedereen er is.");
                                    Console.WriteLine("Toets 'K' en druk ENTER wanneer u klaar bent.");
                                    Console.WriteLine("Toets 'Q' en druk ENTER om terug te gaan.");
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