using Newtonsoft.Json;
class Staff
{
    string? clientCode;
    List<long> staffCodes = new List<long>();
    List<Tour> listoftours = new();
    int tourAmount = 0;

    public Staff()
    {
        using (StreamReader reader = new StreamReader("../../../staff_codes.txt"))
        {
            while (reader.ReadLine() != null)
                staffCodes.Add(Convert.ToInt64(reader.ReadLine()));
        }
        Console.WriteLine(staffCodes[0]);
    }

    public void StaffMainMenu()
    {
        Console.WriteLine("Geef uw personeelscode op: \nToets 'Q' om terug te gaan."); // Ontvang invoer en controleer of deze geldig is
        clientCode = Console.ReadLine();
        if (long.TryParse(clientCode, out long clientCodeLong))
        {
            StaffSubMenu(clientCodeLong);
        }
        else

            switch (clientCode.ToLower())
            {
                case "q":
                    {
                        return;
                    }
            }
    }
    public void PopulateListOfTours(long staffCodeLong)
    {
        if (staffCodes.Contains(staffCodeLong))
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
    private void StaffSubMenu(long staffCodeLong)
    {
        PopulateListOfTours(staffCodeLong);

        foreach (Tour tour in listoftours)
        {
            Console.WriteLine($"{tour.tour_id}; Rondleiding van {tour.tourStartTime}");
            if (Convert.ToInt32(tour.tour_id) > tourAmount)
            {
                tourAmount = Convert.ToInt32(tour.tour_id);
            }
        }

        Console.WriteLine("Voer de ID in van de tour die u wilt selecteren. \nVoor advies over rondleidingen, toets 'A'. \nDruk op 'Q' om terug te gaan naar het hoofdmenu.");
        string selectedTourId = Console.ReadLine();
        int selectedTourIdInt;
        if (int.TryParse(selectedTourId, out selectedTourIdInt))
        {

        }
        else
        {
            switch (selectedTourId)
            {
                case "q":
                    {
                        return;
                    }
                case "a":
                    {
                        Advise.CreateAdvise();
                        break;
                    }
            }
        }
    }

    private void SelectTourAndCheckTour(int selectedTourIdInt, int selectedTourId)
    {
        // Controleer of de ingevoerde tour ID geldig is
        if (selectedTourIdInt > 0 && selectedTourIdInt <= tourAmount)
        {
            // Hier worden reservation ID's voor de geselecteerde tour geprint
            string jsonFilePath = "../../../reservations.json";
            string jsonText = File.ReadAllText(jsonFilePath);
            dynamic reservations = JsonConvert.DeserializeObject(jsonText);
            Console.WriteLine($"Reservation IDs voor tour met ID {selectedTourId}:");

            List<long> reservationIDS = new List<long>();
            foreach (var reservation in reservations)
            {
                if (reservation.tour_number == selectedTourId)
                {
                    Console.WriteLine(reservation.reservation_id);
                    reservationIDS.Add(Convert.ToInt64(reservation.reservation_id));
                }
            }


            List<long> presenceList = CheckPresence(reservationIDS);
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
                foreach (long notpresent in presenceList)
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
                                writeToStartedToursJson(tour.parttakers, Convert.ToString(tour.tourStartTime));
                        }
                        Console.WriteLine("De tour is succesvol gestart");
                        break;
                    }
                case "q":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
                        break;
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
        else
{
    Console.WriteLine("U heeft een incorrecte code opgegeven, probeer opnieuw.");
}

public List<long>? CheckPresence(List<long> reservations)
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
            break;
        }
        else
        {
            if (long.TryParse(checkPresence, out long id))
            {
                if (reservations.Contains(id))
                {
                    reservations.Remove(id);

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
    return reservations;
}
