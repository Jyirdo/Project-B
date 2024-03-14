string? clientCode = null;
List<ClientID> allLoggedClients = new List<ClientID>();

while (true)
{
    Console.WriteLine("Wat wilt u doen? \n1. Aanmelden bij een rondleiding \n2. Tijd van een rondleiding opvragen \n3. Toegang voor personeel");
    while (true)
    {
        string? userInput = Console.ReadLine();
        if (int.TryParse(userInput, out int userInt))
        {
            if (userInt == 1)
            {

                while (true)
                {
                    Console.WriteLine("Geef uw barcode op:"); // Receive input and check if it's valid
                    clientCode = Console.ReadLine();
                    if (int.TryParse(clientCode, out int clientCodeInt))
                    {   // Code to run
                        ClientID newClient = new ClientID(clientCodeInt);

                        newClient.CreateTour();
                        allLoggedClients.Add(newClient);

                        Console.WriteLine("Succesvol aangemeld.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
                    }

                }
                break;
            }

            if (userInt == 2)
            {
                while (true)
                {
                    Console.WriteLine("Geef uw barcode op:"); // Receive input and check if it's valid
                    clientCode = Console.ReadLine();
                    if (int.TryParse(clientCode, out int clientCodeInt))
                    {   // Code to run
                        foreach (ClientID timeRequest in allLoggedClients)
                        {
                            if (timeRequest.ticketID == clientCodeInt)
                            {
                                Console.WriteLine(timeRequest.thisClientTour.tourTime);
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

            if (userInt == 3)
            {
                while (true)
                {
                    Console.WriteLine("Geef uw barcode op:"); // Receive input and check if it's valid
                    clientCode = Console.ReadLine();
                    if (int.TryParse(clientCode, out int clientCodeInt))
                    {   // Code to run

                    }
                    else
                    {
                        Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
                    }

                }
                break;
            }

            else
            {
                Console.WriteLine("Incorrecte invoer");
                break;
            }
        }
    }



}


