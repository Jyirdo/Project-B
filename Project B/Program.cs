using System.ComponentModel.Design;
using Newtonsoft.Json;

string? clientCode = null;
List<Visitor> allLoggedClients = new();
BarcodeGenerator generator = new();
// generator.GenerateBarcodes(25);
// To generate codes for testing

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
        while (true)
        {
            List<Dictionary<string, string>> tourTimes;
            int tourAmount = 0;
            DateTime selectedTime;

            Console.WriteLine("Bij welke rondleiding wilt u u aanmelden?");

            // Read the json and print appropriately
            using (StreamReader reader = new StreamReader("tour_times.json"))
            {
                var json = reader.ReadToEnd();
                tourTimes = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                foreach (var tourTime in tourTimes)
                {
                    foreach (var entry in tourTime)
                    {
                        Console.WriteLine($"{entry.Key}; Rondleiding van {DateTime.Parse(entry.Value).ToString("dd-M-yyyy HH:mm")}");
                        if (Convert.ToInt32(entry.Key) > tourAmount)
                        {
                            tourAmount = Convert.ToInt32(entry.Key);
                        }

                    }
                }
            }

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
                            newClient.CreateTour();
                            allLoggedClients.Add(newClient);
                            Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {(newClient.tourTime).ToString("dd-M-yyyy HH:mm")}");
                            goto End;
                        }
                        else
                        {
                            Console.WriteLine("This Tour is full");
                        }
                    }
                }
            }
        }
    }

    else // Capture wrong inputs
    {
        Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
    }
End:
    { }

}
