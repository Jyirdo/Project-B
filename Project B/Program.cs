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

    string universalClientCode; // The universalClientCode is the code the user gives at beginning. It makes sure the code only needs to be scanned once. It resets when the program asks for a new barcode.

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
            universalClientCode = "";
            Greeting();
            universalClientCode = Console.ReadLine();
            if (CheckInReservationJson(universalClientCode))
            {
                string menu2 = Menu2();

                if (menu2 == "a" || menu2 == "q") ;
                {
                    continue;
                }
            }
            else if (long.TryParse(universalClientCode, out long universalClientCodeInt))
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
                        return "q";
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
                        break;
                    }
            }
            return null;
        }
    }

    public string? Menu2()
    {

        while (true)
        {
            Console.WriteLine("Toets 'T' om de tijd van uw rondleiding in te zien. \nAls u uw rondleiding wilt annuleren, toets 'A'. \nVoor hulp, toets 'H' \nToets 'Q' om terug te gaan.");
            string clientChoice = Console.ReadLine();
            switch (clientChoice.ToLower())
            {
                case "t":
                    {
                        Console.WriteLine(GetTourTime(Convert.ToInt64(universalClientCode)));
                        break;
                    }
                case "a":
                    {
                        Cancel();
                        return "a";
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
                        return "q";
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
                        continue;
                    }
            }
        }
    }

    public void Help()
    {
        while (true)
        {
            Console.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld. \nToets 'Q' om terug te gaan.");
            string helpInput = Console.ReadLine().ToLower();
            if (helpInput == "q")
                return;
            else
            {
                Console.WriteLine("Deze invoer herken ik niet.");
                continue;
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
        Console.WriteLine($"Bij deze rondleidingen kunt u zich aanmelden:\n");

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
            Console.WriteLine("Toets het nummer van de rondleiding in op u aan te melden.");
            string chosenTour = Menu1();
            if (chosenTour == null)
            {
                continue;
            }
            else if (chosenTour == "q")
            {
                break;
            }

            int.TryParse(chosenTour, out chosenTourInt);
            if (chosenTourInt <= 0 || chosenTourInt > tourAmount)
            {
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
            }
            else
            {
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
        }
    }

    public void Cancel()
    {
        if (CheckInReservationJson(universalClientCode))
        {
            Reservation? cancelReservation = GetReservationFromJson(universalClientCode);
            if (cancelReservation != null)
            {
                removeFromReservationJson(new Visitor(Convert.ToInt64(cancelReservation.ReservationId), Convert.ToDateTime(cancelReservation.DateTime), Convert.ToInt32(cancelReservation.TourNumber)));
                Console.WriteLine("Succesvol afgemeld bij uw rondleiding. Prettige dag verder!");
                return;
            }
            else
                Console.WriteLine("Uw reservering is niet gevonden.");
            return;
        }
    }

    public string GetTourTime(long tourTimeRequest)
    {
        var reservations = readReservationJson("../../../reservations.json");
        foreach (Reservation timeRequest in reservations)
        {
            if (timeRequest.ReservationId == Convert.ToString(tourTimeRequest))
            {
                return $"{timeRequest.DateTime}\n";
            }
        }
        return "Geen tijd gevonden.";
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

    public Reservation? GetReservationFromJson(string reservationID)
    {
        string reservationJson = File.ReadAllText("../../../reservations.json");
        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationJson);
        foreach (Reservation booking in reservations)
        {
            if (Convert.ToString(booking.ReservationId) == reservationID)
            {
                return booking;
            }
        }
        return null;
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
        string existingJson = File.ReadAllText("../../../reservations.json");
        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(existingJson) ?? new List<Reservation>();
        var reservationToRemove = reservations.FirstOrDefault(r => r.ReservationId == visitor.ticketID.ToString());
        if (reservationToRemove != null)
        {
            reservations.Remove(reservationToRemove);
            string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText("../../../reservations.json", updatedJson);
        }
    }

}