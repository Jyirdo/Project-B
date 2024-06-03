using Newtonsoft.Json;
// Sometime in the future make these methods static somehow
// Implement 3 layer architecture here
class Staff
{
    List<string> staffCodes = new List<string>();
    List<string> scannedIDS = new();
    List<TourModel> listoftours = new();
    private static BaseLogic baseLogic = new BaseLogic();
    int tourAmount = 0;
    private string staffCode;

    private static List<TourModel> dataList;

    public Staff()
    {
        using (StreamReader reader = new StreamReader("DataSources/staff_codes.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                staffCodes.Add(line);
            }
        }
    }

    public void StaffMainMenu()
    {
        Console.WriteLine("Geef uw personeelscode op: \nToets 'Q' om terug te gaan."); // Ontvang invoer en controleer of deze geldig is
        staffCode = Console.ReadLine();
        if (staffCode.ToLower() == "q")
            return;
        else if (staffCodes.Contains(staffCode))
        {
            StaffSubMenu(staffCode);
        }
        else if (!staffCodes.Contains(staffCode))
        {
            Console.WriteLine("Dat is geen correcte code.");
        }
    }

    public void PopulateListOfTours(string staffCode)
    {
        if (staffCodes.Contains(staffCode))
        {
            if (File.Exists("DataSources/Tours.json"))
            {
                using (StreamReader reader = new StreamReader("DataSources/Tours.json"))
                {
                    var json = reader.ReadToEnd();
                    dataList = JsonConvert.DeserializeObject<List<TourModel>>(json);
                    foreach (TourModel tour in dataList)
                        listoftours.Add(tour);
                }
            }
        }
    }

    private void StaffSubMenu(string staffCode)
    {
        PopulateListOfTours(staffCode);

        foreach (TourModel tour in listoftours)
        {
            Console.WriteLine($"{tour.tourId}; Rondleiding van {tour.dateTime}");
            if (Convert.ToInt32(tour.tourId) > tourAmount)
            {
                tourAmount = Convert.ToInt32(tour.tourId);
            }
        }

        scannedIDS.Clear();
        Console.WriteLine("Voer de ID in van de tour die u wilt selecteren. \nVoor advies over rondleidingen, toets 'A'. \nOm een bezoeker toe te voegen druk op 'l'. \nDruk op 'Q' om terug te gaan naar het hoofdmenu.");
        string selectedTourId = Console.ReadLine();
        if (int.TryParse(selectedTourId, out int selectedTourIdInt))
        {
            SelectTourAndCheckTour(selectedTourIdInt);
        }
        else
        {
            switch (selectedTourId)
            {
                case "q":
                    {
                        break;
                    }
                case "a":
                    {
                        Advise.CreateAdvise();
                        break;
                    }
                case "l":
                    {
                        long barcode = 12345678;
                        AddLastMinuteVisitor(barcode);
                        break;
                    }
            }
        }
    }

    public List<string> CheckPresence(List<string> reservations)
    {
        while (true)
        {
            Console.WriteLine("Begin met barcodes scannen om te controleren of iedereen er is. \nDruk op 'K' wanneer u klaar bent. \nDruk op 'Q' om terug te gaan.");
            string checkPresence = Console.ReadLine();
            if (checkPresence.ToLower() == "q")
            {
                return null;
            }
            if (checkPresence.ToLower() == "k")
            {
                return reservations;
            }
            else
            {
                if (long.TryParse(checkPresence, out long id))
                {
                    if (reservations.Contains(Convert.ToString(id)))
                    {
                        scannedIDS.Add(checkPresence);
                        reservations.Remove(Convert.ToString(id));
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Dat is geen correcte id.");
                    continue;
                }
            }
        }

    }


    private void SelectTourAndCheckTour(int selectedTourIdInt)
    {
        List<string> reservationIDS = new List<string>();
        // Controleer of de ingevoerde tour ID geldig is
        if (selectedTourIdInt > 0 && selectedTourIdInt <= tourAmount)
        {
            // Hier worden reservation ID's voor de geselecteerde tour geprint
            string json = File.ReadAllText("DataSources/Tours.json");
            dataList = JsonConvert.DeserializeObject<List<TourModel>>(json);
            Console.WriteLine($"Reservation IDs voor tour met ID {selectedTourIdInt}:");


            foreach (var tour in dataList)
            {
                if (tour.tourId == selectedTourIdInt)
                {
                    Console.WriteLine(Convert.ToString(tour.tourId));
                    reservationIDS.Add(Convert.ToString(tour.tourId));
                }
            }


        CheckThePresence:
            List<string> presenceList = CheckPresence(reservationIDS);

            foreach (string scanned in scannedIDS)
                presenceList.Remove(scanned);

            if (presenceList == null)
            {
                return;
            }
            else if (presenceList.Count() == 0)
            {
                Console.WriteLine("Iedereen is aanwezig.");
            }
            else if (presenceList.Count() > 0)
            {
                Console.WriteLine("Deze ID's zijn afwezig:\n");
                foreach (string notpresent in presenceList)
                {
                    int enumerator = 1;
                    Console.WriteLine($"{enumerator}. ({notpresent})");
                    enumerator++;
                }
            }
            Console.WriteLine("Druk op 'S' om de tour te starten. \nDruk op 'Q' om terug te gaan.");
            switch (Console.ReadLine().ToLower())
            {
                case "s":
                    {
                        foreach (TourModel tour in listoftours)
                        {
                            if (tour.tourId == selectedTourIdInt)
                            {
                                // Program program = new Program();
                                // program.writeToStartedToursJson(tour.parttakers, Convert.ToString(tour.tourStartTime));
                                // break;
                            }
                        }
                        Console.WriteLine("De tour is succesvol gestart");
                        break;
                    }
                case "q":
                    {
                        goto CheckThePresence;
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
                        break;
                    }
            }
        }
        else
            Console.WriteLine("Ongeldige tour ID. Probeer opnieuw.");
    }

    public static void Choose_Tour(long barcode)
    {
        DateTime selectedTime;
        List<TourModel> tours = baseLogic.GetAllTours();
        //Chosentour = id of tour chosen by visitor

        Console.WriteLine("Kies de tour waar u een bezoeker aan toe wilt voegen");
        int tourId = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine(tours.Count());
        if (tourId <= 0 || tourId > tours.Count())
        {
            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
        }
        else
        {
            foreach (TourModel tour in tours)
            {

                if (tour.tourId == tourId)
                {
                    // check if tour is full
                    if (tour.parttakers < tour.limit)
                    {
                        selectedTime = Convert.ToDateTime(tour.dateTime);
                        Visitor newClient = new Visitor(barcode, selectedTime, tourId);

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

    private void AddLastMinuteVisitor(long barcode)
    {
        DateTime selectedTime;
        List<TourModel> tours = baseLogic.GetAllTours();

        Console.WriteLine("Kies de tour waar u een bezoeker aan toe wilt voegen");
        int tourId = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine(tours.Count());
        if (tourId <= 0 || tourId > tours.Count())
        {
            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
        }
        else
        {
            foreach (TourModel tour in tours)
            {

                if (tour.tourId == tourId)
                {
                    // check if tour is full
                    if (tour.parttakers < tour.limit)
                    {
                        selectedTime = Convert.ToDateTime(tour.dateTime);
                        Visitor newClient = new Visitor(barcode, selectedTime, tourId);

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


