using Newtonsoft.Json;

class Program
{
    List<Visitor> allLoggedClients = new();
    List<Tour> listoftours = new();
    BarcodeGenerator generator = new();
    string? clientCode = null;

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
            string universalClientCode = Console.ReadLine();

            if (long.TryParse(universalClientCode, out long universalClientCodeInt))
            {
                Choose_Tour(universalClientCodeInt);
            }
            else
                Console.WriteLine("Incorrecte barcode");

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

        Console.WriteLine("scan uw barcode op om verder te gaan.");
    }

    public void Check_Tour_Time(long tourTimeInt)
    {
        GetTourTime(tourTimeInt);
    }

    public string? Menu1()
    {
        Console.WriteLine("Voor hulp, toets 'H' \nToets 'Q' om terug te gaan.");
        string clientChoice = Console.ReadLine();
        if (long.TryParse(clientChoice, out long ClientChoiceInt))
        {
            return clientChoice;
        }
        else
        {
            switch (clientChoice.ToLower())
            {
                case "h":
                    {
                        Help();
                        break;
                    }
                case "p":
                    {
                        Personeel();
                        break;
                    }
                case "q":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
                        break;
                    }
            }
            return null;
        }
    }

    public void Menu2()
    {
        Console.WriteLine("Toets 'T' om de tijd van uw rondleiding in te zien. \nAls u uw rondleiding wilt annuleren, toets 'A'. \nVoor hulp, toets 'H' \nToets 'Q' om terug te gaan.");
        while (true)
        {
            clientCode = Console.ReadLine();
            switch (clientCode.ToLower())
            {
                case "t":
                    {
                        Check_Tour_Time(Convert.ToInt64(clientCode));
                        break;
                    }
                case "a":
                    {
                        Cancel();
                        break;
                    }
                case "h":
                    {
                        Help();
                        break;
                    }
                case "p":
                    {
                        Personeel();
                        break;
                    }
                case "q":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
                        continue;
                    }
            }
        }
    }

    public void Help()
    {
        Console.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld. \nToets 'Q' om terug te gaan.");
        if (Console.ReadLine().ToLower() == "q")
            return;
    }

    public void Personeel()
    {
        while (true)
        {
            Console.WriteLine("Geef uw personeelscode op: \nToets 'Q' om terug te gaan."); // Receive input and check if it's valid
            string staffCode = Console.ReadLine().ToLower();
            if (int.TryParse(staffCode, out int staffCodeInt))
            {
                if (staffCodeInt == 456)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
                }
            }
        }
    }

    public void Load_Tours()
    {
        List<Dictionary<string, string>> tourTimes;

        tourTimes = readFromTourJson("../../../tour_times.json");
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
            Console.WriteLine($"{tour.tour_id}; Rondleiding van {tour.tourStartTime}");
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
            string chosenTour = Menu1();
            if (chosenTour == null)
            {
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
            }
            else if (chosenTourInt <= 0 || chosenTourInt > tourAmount)
            {
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
            }
            else
            {
                int.TryParse(chosenTour, out chosenTourInt);
            }
        }

        // add visitor to their chosen tour
        foreach (Tour tour in listoftours)
        {
            if (tour.tour_id == chosenTourInt)
            {
                // check if tour is full
                if (tour.CheckTourFullness() == false)
                {
                    selectedTime = Convert.ToDateTime(tour.tourStartTime);
                    Visitor newClient = new Visitor(clientCodeInt, selectedTime, chosenTourInt);
                    writeToReservationJson(newClient);

                    // add visitor to the list in their tour
                    tour.AddVisitorsToTour(true);
                    Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {(newClient.tourTime).ToString("dd-M-yyyy HH:mm")}\n");
                }
                else
                {
                    Console.WriteLine("This tour is full\n");
                }
            }
        }
    }

    public void Cancel()
    {
        Console.WriteLine("Geef uw code op om uw reservering te annuleren");
        string cancelCode = Console.ReadLine();

        if (CheckInReservationJson(cancelCode))
        {
            // removeFromReservationJson();
        }

        Console.WriteLine("Succesvol afgemeld bij uw rondleiding. Prettige dag verder!");
    }

    public void GetTourTime(long tourTimeRequest)
    {
        var reservations = readReservationJson("../../../reservations.json");
        foreach (Reservation timeRequest in reservations)
        {
            Console.WriteLine(timeRequest);
            if (timeRequest.ReservationId == tourTimeRequest)
            {
                Console.WriteLine($"{timeRequest.Time}\n");
                break;
            }
        }
    }

    public bool CheckInReservationJson(string reservationID)
    {
        string reservationJson = File.ReadAllText("../../../reservations.json");
        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationJson);
        foreach (Reservation booking in reservations)
        {
            if (Convert.ToString(booking.ReservationId) == reservationID)
            {
                return true;
            }
        }

        return false;
    }
    public List<Reservation> readReservationJson(string filepath)
    {
        string reservationJson = File.ReadAllText(filepath);
        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationJson);
        return reservations;
    }

    public List<Dictionary<string, string>> readFromTourJson(string filepath)
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

    public void removeFromReservationJson(Visitor visitor)
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

        reservations.Remove(reservationObj);
        string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText("../../../reservations.json", updatedJson);
    }
}