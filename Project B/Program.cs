using Newtonsoft.Json;
using System.Media;


class Program
{
    List<Visitor> allLoggedClients = new();
    List<Tour> listoftours = new();
    BarcodeGenerator generator = new();
    string clientCode = null;

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

                if (menu2 == "a" || menu2 == "q")
                {
                    continue;
                }
            }
            else if (long.TryParse(universalClientCode, out long universalClientCodeInt))
            {
                Choose_Tour(universalClientCodeInt);
            }
            else if (universalClientCode.ToLower() == "p")
            {
                Staff staff = new Staff();  // Fix staff methods to be static
                staff.StaffMainMenu();
            }
            else
                Console.WriteLine("U heeft een incorrecte barcode gescand, probeer het opnieuw.");

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

        Console.WriteLine("scan uw barcode om verder te gaan.");
    }

    public string Menu1()
    {
        Console.WriteLine("Voor hulp, toets 'H' \nOm terug te gaan, toets 'Q'");
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
                        Staff staff = new Staff();  // Fix staff methods to be static
                        staff.StaffMainMenu();
                        break;
                    }
                case "q":
                    {
                        return "q";
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                        break;
                    }
            }
            return null;
        }
    }

    public string Menu2()
    {

        while (true)
        {
            Console.WriteLine("Om de tijd van uw rondleiding in te zien, toets 'T' \nAls u uw rondleiding wilt annuleren, toets 'A' \nVoor hulp, toets 'H' \nOm terug te gaan, toets 'Q'");
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
                        Staff staff = new Staff();  // Fix staff methods to be static
                        staff.StaffMainMenu();
                        break;
                    }
                case "q":
                    {
                        return "q";
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                        continue;
                    }
            }
        }
    }

    public void Help()
    {
        while (true)
        {
            Console.WriteLine("Er komt iemand aan om u te helpen, een ogenblik geduld alstublieft. \nOm terug te gaan, toets 'Q'.");
            PlayJingle();
            string helpInput = Console.ReadLine().ToLower();
            if (helpInput == "q")
                return;
            else
            {
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                continue;
            }
        }
    }



    public void Load_Tours()
    {
        List<Dictionary<string, string>> tourTimes;

        tourTimes = readFromTourJson("../../../StorageFiles/tour_times.json");
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
            Console.WriteLine("Toets het nummer van de rondleiding in waarvoor u zich wilt aanmelden:");
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
                Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
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
                            Console.WriteLine("Deze tour is helaas vol, probeer een andere optie.\n");
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
            Reservation cancelReservation = GetReservationFromJson(universalClientCode);
            if (cancelReservation != null)
            {
                removeFromReservationJson(new Visitor(Convert.ToInt64(cancelReservation.ReservationId), Convert.ToDateTime(cancelReservation.DateTime), Convert.ToInt32(cancelReservation.TourNumber)));
                Console.WriteLine("Succesvol afgemeld bij uw rondleiding. Prettige dag verder!");
                return;
            }
            else
                Console.WriteLine("Uw reservering is helaas niet gevonden. Probeer het opnieuw.");
            return;
        }
    }

    public string GetTourTime(long tourTimeRequest)
    {
        var reservations = readReservationJson("../../../StorageFiles/reservations.json");
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
        string reservationJson = File.ReadAllText("../../../StorageFiles/reservations.json");
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

    public Reservation GetReservationFromJson(string reservationID)
    {
        string reservationJson = File.ReadAllText("../../../StorageFiles/reservations.json");
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
        if (File.Exists("../../../StorageFiles/reservations.json"))
        {
            string existingJson = File.ReadAllText("../../../StorageFiles/reservations.json");
            reservations = JsonConvert.DeserializeObject<List<dynamic>>(existingJson) ?? new List<dynamic>();
        }

        reservations.Add(reservationObj);
        string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText("../../../StorageFiles/reservations.json", updatedJson);
    }

    public void writeToStartedToursJson(int parttakers, string time)
    {
        var reservationObj = new
        {
            date_time = time,
            presence = parttakers
        };

        List<dynamic> startedTours = new List<dynamic>();
        if (File.Exists("../../../StorageFiles/started_tours.json"))
        {
            string existingJson = File.ReadAllText("../../../StorageFiles/started_tours.json");
            startedTours = JsonConvert.DeserializeObject<List<dynamic>>(existingJson) ?? new List<dynamic>();
        }

        startedTours.Add(reservationObj);
        string updatedJson = JsonConvert.SerializeObject(startedTours, Formatting.Indented);
        File.WriteAllText("../../../StorageFiles/started_tours.json", updatedJson);
    }

    public void removeFromReservationJson(Visitor visitor)
    {
        string existingJson = File.ReadAllText("../../../StorageFiles/reservations.json");
        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(existingJson) ?? new List<Reservation>();
        var reservationToRemove = reservations.FirstOrDefault(r => r.ReservationId == visitor.ticketID.ToString());
        if (reservationToRemove != null)
        {
            reservations.Remove(reservationToRemove);
            string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText("../../../StorageFiles/reservations.json", updatedJson);
        }
    }

    public void PlayJingle()  // Make check for os, only works on windows.
    {
        using (SoundPlayer soundPlayer = new SoundPlayer("../../../Audio/jingle.wav"))
            soundPlayer.Play();
    }

}