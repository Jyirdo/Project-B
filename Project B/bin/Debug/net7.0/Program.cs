string? clientCode = null;
List<Visitor> allLoggedClients = new List<Visitor>();

int parttakers = 0;
bool opentourspots = true;

void MaxVisitor()
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

while (true)
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

    Console.WriteLine("geef uw code op om u aan te melden bij een rondleiding. \nOm de tijd van uw rondleidng te zien, toets 'T'. \nVoor hulp, toets 'H'.");

    // Receive input and check if it's valid

    clientCode = Console.ReadLine();
    if (clientCode.ToLower() == "t") // Check tour time option
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
                        Console.WriteLine(timeRequestVisitor.tourTime);
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

    else if (clientCode.ToLower() == "h") // Assistance option
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

    else if (clientCode.ToLower() == "p") // Access for staff
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

    else if (long.TryParse(clientCode, out long clientCodeInt)) // Add a visitor to a tour
    {   // Code to run
        DateTime selectedTime;
        DateTime test = DateTime.Now;

        while (true)
        {
            Console.WriteLine("Bij welke rondleiding wilt u u aanmelden?");

            string jsonFilePath = @"tour_times.json";

            foreach (string line in File.ReadLines(jsonFilePath))
            {
                Console.WriteLine($"{line}");
            }

            string chosenTour = Console.ReadLine();
            if (chosenTour == "1")
            {
                selectedTime = test;
                break;
            }
            else
            {
                Console.WriteLine("Incorrecte invoer, probeer opnieuw.");
            }
        }

        if (opentourspots == true)
        {
            parttakers++;
            MaxVisitor();
            Console.WriteLine(parttakers);
            Console.WriteLine(opentourspots);
            Visitor newClient = new Visitor(clientCodeInt, selectedTime);

            newClient.CreateTour();
            allLoggedClients.Add(newClient);
            Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {newClient.tourTime}");
        }
        else
        {
            Console.WriteLine("Maximum tourcapacity reached");
        }
    }

    else // Capture wrong inputs
    {
        Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
    }

}
