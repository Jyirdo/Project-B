using Newtonsoft.Json;

class Program
{
    List<Visitor> allLoggedClients = new();
    List<Tour> listoftours = new();
    BarcodeGenerator generator = new();
    string? clientCode = null;
    List<long> staffcodes = new()
    {
        0313800960323
    };

    public static void Main()
    {
        Program test = new Program();
        test.Robot();
    }

    public void Robot()
    {
        Load_Tours();
        while (true)
        {
            Greeting();
            Console.WriteLine("geef uw code op om u aan te melden bij een rondleiding. \nOm de tijd van uw rondleidng te zien of om deze te annuleren, toets 'T'. \nVoor hulp, toets 'H'.");
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
            Console.WriteLine("Geef uw code op om de tijd van uw rondleiding in te zien. Als u uw rondleiding wilt annuleren, toets 'A'.\nToets 'Q' om terug te gaan."); // Receive input and check if it's valid
            clientCode = Console.ReadLine();

            if (int.TryParse(clientCode, out int clientCodeInt))
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

            else if (clientCode.ToLower() == "a")
            {
                Console.WriteLine("Geef uw code op om uw reservering te annuleren");
                clientCode = Console.ReadLine();
                foreach(Visitor visitor in allLoggedClients)
                {
                    if (clientCode == Convert.ToString(visitor.ticketID))
                    {
                        allLoggedClients.Remove(visitor);
                        foreach (Tour tour in listoftours)
                        {
                            tour.visitorsintour.Remove(visitor);
                        }
                        Console.WriteLine("Succesvol afgemeld bij uw rondleiding. Prettige dag verder!");
                        break;
                    }
                }
            }

            else if (clientCode.ToLower() == "q")
            {
                break;
            }

            else
            {
                Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
                continue;
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
            Console.WriteLine("Geef uw personeelscode op: \nToets 'Q' om terug te gaan."); // Ontvang invoer en controleer of deze geldig is
            clientCode = Console.ReadLine();
            int tourAmount = 0;
            if (clientCode.ToLower() == "q")
            {
                break;
            }
            else if (long.TryParse(clientCode, out long clientCodeLong))
            {   // Uit te voeren code
                if (staffcodes.Contains(clientCodeLong))
                {
                    foreach (Tour tour in listoftours)
                    {
                        Console.WriteLine($"{tour.tour_id}; Rondleiding van {tour.tourStartTime}");
                        if (Convert.ToInt32(tour.tour_id) > tourAmount)
                        {
                            tourAmount = Convert.ToInt32(tour.tour_id);
                        }
                    }
                    Console.WriteLine("Voer de ID in van de tour die u wilt selecteren of druk op 'Q' om terug te gaan naar het hoofdmenu:");
                    string selectedTourId = Console.ReadLine();
                    if (selectedTourId.ToLower() == "q")
                    {
                        break;
                    }
                    else
                    {
                        int selectedTourIdInt;
                        if (int.TryParse(selectedTourId, out selectedTourIdInt))
                        {
                            // Controleer of de ingevoerde tour ID geldig is
                            if (selectedTourIdInt > 0 && selectedTourIdInt <= tourAmount)
                            {
                                // Hier worden reservation ID's voor de geselecteerde tour geprint
                                string jsonFilePath = "reservations.json";
                                string jsonText = File.ReadAllText(jsonFilePath);
                                dynamic reservations = JsonConvert.DeserializeObject(jsonText);
                                Console.WriteLine($"Reservation IDs voor tour met ID {selectedTourId}:");
                                foreach (var reservation in reservations)
                                {
                                    if (reservation.tour_number == selectedTourId)
                                    {
                                        Console.WriteLine(reservation.reservation_id);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ongeldige tour ID. Probeer opnieuw.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ongeldige invoer. Voer a.u.b. een numerieke waarde in.");
                        }
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

    public void Load_Tours()
    {
        List<Dictionary<string, string>> tourTimes;

        tourTimes = readFromJson("../../../tour_times.json");
        foreach (var tourTime in tourTimes)
        {
            foreach (var entry in tourTime)
            {
                //Make a tour for every tourtime and id
                Tour tour = new Tour(Convert.ToInt32(entry.Key), Convert.ToDateTime(entry.Value));
                listoftours.Add(tour);
            }
        }
    }

    public void Choose_Tour(long clientCodeInt)
    {
        int tourAmount = 0;
        DateTime selectedTime;

        foreach (Tour tour in listoftours)
        {
            Console.WriteLine($"{tour.tour_id}; Rondleiding van {tour.tourStartTime}\n");
            if (Convert.ToInt32(tour.tour_id) > tourAmount)
            {
                tourAmount = Convert.ToInt32(tour.tour_id);
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

        // add visitor to their chosen tour
        foreach (Tour tour in listoftours)
        {
            if (tour.tour_id == chosenTourInt)
            {
                // check if tour is full
                if (tour.visitorsintour.Count < 3)
                {
                    selectedTime = Convert.ToDateTime(tour.tourStartTime);
                    Visitor newClient = new Visitor(clientCodeInt, selectedTime, chosenTourInt);
                    writeToReservationJson(newClient);

                    // add visitor to the list in their tour
                    tour.visitorsintour.Add(newClient);
                    Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {(newClient.tourTime).ToString("dd-M-yyyy HH:mm")}\n");
                }
                else
                {
                    Console.WriteLine("This tour is full\n");
                }
            }
        }
    }

    public List<Dictionary<string, string>> readFromJson(string filepath)
    {
        List<Dictionary<string, string>> tourTimes;
        using (StreamReader reader = new StreamReader(filepath))
        {
            var json = reader.ReadToEnd();
            tourTimes = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
        }

        if (tourTimes == null)
            return new List<Dictionary<string, string>>();
        else
            return tourTimes;
    }

    public void writeToReservationJson(Visitor visitor)
    {
        var reservationObj = new
        {
            reservation_id = $"{visitor.ticketID}",
            date_time = $"{visitor.tourTime}",
            tour_number = $"{visitor.tourNumber}"
        };

        List<dynamic> reservations = new List<dynamic>();
        if (File.Exists("../../../reservations.json"))
        {
            string existingJson = File.ReadAllText("../../../reservations.json");
            reservations = JsonConvert.DeserializeObject<List<dynamic>>(existingJson) ?? new List<dynamic>();
        }

        reservations.Add(reservationObj);
        string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText("../../../reservations.json", updatedJson);
    }
}