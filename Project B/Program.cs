using Newtonsoft.Json;

class Program
{
    List<Visitor> allLoggedClients = new();
    BarcodeGenerator generator = new();
    string? clientCode = null;
    int parttakers = 0;
    bool opentourspots = true;

    public static void Main()
    {
        Program test = new Program();
        test.Robot();
    }

    public void Robot()
    {
        while (true)
        {
            Greeting();
            Console.WriteLine("geef uw code op om u aan te melden bij een rondleiding. \nOm de tijd van uw rondleidng te zien, toets 'T'. \nVoor hulp, toets 'H'.");
            clientCode = Console.ReadLine();
            if (clientCode.ToLower() == "t")
            {
                Check_Tour_Time();
            }
            else if (clientCode.ToLower() == "h")
            {
                Help();
            }
            else if (clientCode.ToLower() == "p")
            {
                Personeel();
            }
            else if (long.TryParse(clientCode, out long clientCodeInt))
            {
                Choose_Tour(clientCodeInt);
            }
            else // Capture wrong inputs
            {
                Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
            }
        }     
    }

    public void Greeting()
    {
        // Find out what time it is and greet the user appropriatly
        int currentHour = Convert.ToInt16(DateTime.Now.ToString("HH"));
        if (currentHour < 6)
        {
            Console.Write("Goedennacht, ");
        }
        else if (currentHour < 12)
        {
            Console.Write("Goedemorgen, ");
        }
        else if (currentHour < 18)
        {
            Console.Write("Goedemiddag, ");
        }
        else if (currentHour < 24)
        {
            Console.Write("Goedenavond, ");
        }
        else
        {
            Console.Write("Welkom, ");
        }
    }

    public void Check_Tour_Time()
    {
        while (true)
        {
            Console.WriteLine("Geef uw code op om de tijd van uw rondleiding te zien. \nToets 'Q' om terug te gaan."); // Receive input and check if it's valid
            clientCode = Console.ReadLine();
            if (clientCode.ToLower() == "q")
            {
                break;
            }
            else if (int.TryParse(clientCode, out int clientCodeInt))
            {   // Code to run
                foreach (Visitor timeRequestVisitor in allLoggedClients)
                {
                    if (timeRequestVisitor.ticketID == clientCodeInt)
                    {
                        Console.WriteLine($"{timeRequestVisitor.tourTime}\n");
                    }
                    break;
                }

            }
            else
            {
                Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
            }
            break;
        }
    }

    public void Help()
    {
        while (true)
        {
            Console.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld. \nToets 'Q' om terug te gaan."); // Receive input and check if it's valid
            clientCode = Console.ReadLine();
            if (clientCode.ToLower() == "q")
            {
                break;
            }
        }
    }

    public void Personeel()
    {
        while (true)
        {
            Console.WriteLine("Geef uw personeelscode op: \nToets 'Q' om terug te gaan."); // Receive input and check if it's valid
            clientCode = Console.ReadLine();
            if (clientCode.ToLower() == "q")
            {
                break;
            }
            else if (int.TryParse(clientCode, out int clientCodeInt))
            {   // Code to run
                if (clientCodeInt == 456)
                {

                    Console.WriteLine("Druk op 'Q' om terug te gaan naar het hoofdmenu.");
                    clientCode = Console.ReadLine();
                    if (clientCode.ToLower() == "q")
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrecte personeelscode.");
                }
            }
            else
            {
                Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
            }

        }
    }

    public void Choose_Tour(long clientCodeInt)
    {
        List<Dictionary<string, string>> tourTimes;
        int tourAmount = 0;
        DateTime selectedTime;

        // Read the json
        using (StreamReader reader = new StreamReader("tour_times.json"))
        {
            var json = reader.ReadToEnd();
            tourTimes = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
            Console.WriteLine("Bij welke rondleiding wilt u u aanmelden?");
            foreach (var tourTime in tourTimes)
            {
                foreach (var entry in tourTime)
                {
                    //Make a tour for every time
                    Tour tour = new Tour(Convert.ToInt32(entry.Key), Convert.ToDateTime(entry.Value));
                    //Print the Datetime per tour
                    Console.WriteLine($"{entry.Key}; Rondleiding van {DateTime.Parse(entry.Value).ToString("dd-M-yyyy HH:mm")}");
                    if (Convert.ToInt32(entry.Key) > tourAmount)
                    {
                        tourAmount = Convert.ToInt32(entry.Key);
                    }
                }
            }
        }

        //Chosentour = id of tour chosen by visitor
        int chosenTourInt = 0;
        while (chosenTourInt <= 0 || chosenTourInt > tourAmount)
        {
            Console.WriteLine("Ik wil me aanmelden bij rondleiding:");
            string chosenTour = Console.ReadLine();
            int.TryParse(chosenTour, out chosenTourInt);
            if (chosenTourInt <= 0 || chosenTourInt > tourAmount)
            {
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
            }
        }

        foreach (var tourTime in tourTimes)
        {
            foreach (var entry in tourTime)
            {
                if (Convert.ToInt32(entry.Key) == chosenTourInt)
                {
                    if (opentourspots == true)
                    {
                        selectedTime = Convert.ToDateTime(entry.Value);
                        parttakers++;
                        MaxVisitor();

                        Visitor newClient = new Visitor(clientCodeInt, selectedTime);
                        allLoggedClients.Add(newClient);
                        Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {(newClient.tourTime).ToString("dd-M-yyyy HH:mm")}\n");
                    }
                    else
                    {
                        Console.WriteLine("This Tour is full");
                    }
                }
            }
        }
    }

    public void MaxVisitor()
    {
        if (parttakers < 3)
        {
            opentourspots = true;
        }
        else
        {
            opentourspots = false;
        }
    }
}