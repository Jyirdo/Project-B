using Newtonsoft.Json;
// Sometime in the future make these methods static somehow
class Staff
{
    string clientCode;
    List<string> staffCodes = new List<string>();
    List<Tour> listoftours = new();
    int tourAmount = 0;
    private string staffCode;

    List<string> scannedIDS = new();

    public Staff()
    {
        using (StreamReader reader = new StreamReader("../../../staff_codes.txt"))
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
            if (File.Exists("../../../tour_times.json"))
            {
                List<Dictionary<string, string>> tourTimes;
                using (StreamReader reader = new StreamReader("../../../tour_times.json"))
                {
                    var json = reader.ReadToEnd();
                    tourTimes = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                    foreach (Dictionary<string, string> tour in tourTimes)
                        foreach (KeyValuePair<string, string> kvp in tour)
                            listoftours.Add(new Tour(Convert.ToInt32(kvp.Key), Convert.ToDateTime(kvp.Value)));
                }
            }
        }
    }

    private void StaffSubMenu(string staffCode)
    {
        PopulateListOfTours(staffCode);

        foreach (Tour tour in listoftours)
        {
            Console.WriteLine($"{tour.tour_id}; Rondleiding van {tour.tourStartTime}");
            if (Convert.ToInt32(tour.tour_id) > tourAmount)
            {
                tourAmount = Convert.ToInt32(tour.tour_id);
            }
        }

        scannedIDS.Clear();
        Console.WriteLine("Voer de ID in van de tour die u wilt selecteren. \nVoor advies over rondleidingen, toets 'A'. \nDruk op 'Q' om terug te gaan naar het hoofdmenu.");
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
            string jsonFilePath = "../../../reservations.json";
            string jsonText = File.ReadAllText(jsonFilePath);
            dynamic reservations = JsonConvert.DeserializeObject(jsonText);
            Console.WriteLine($"Reservation IDs voor tour met ID {selectedTourIdInt}:");


            foreach (var reservation in reservations)
            {
                if (reservation.tour_number == selectedTourIdInt)
                {
                    Console.WriteLine(Convert.ToString(reservation.reservation_id));
                    reservationIDS.Add(Convert.ToString(reservation.reservation_id));
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
                        foreach (Tour tour in listoftours)
                        {
                            if (tour.tour_id == selectedTourIdInt)
                            {
                                Program program = new Program();
                                program.writeToStartedToursJson(tour.parttakers, Convert.ToString(tour.tourStartTime));
                                break;
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
}


