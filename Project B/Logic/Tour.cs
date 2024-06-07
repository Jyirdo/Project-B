public class Tour
{
    public int tourId;
    public DateTime tourStartTime;
    public static int parttakers;
    public static bool opentourspots;
    public static int limit;

    public static int currentTourID = 1;
    private static BaseLogic baseLogic = new BaseLogic();

    public Tour(int id, DateTime time)
    {
        tourId = id;
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
        limit = 13;
    }

    public static int Load_Tours(bool StaffMenuEdition)
    {
        List<TourModel> tours = BaseAccess.LoadAll();
        int lowestTourId = 1;
        bool firstLoop = true;

        foreach (TourModel tour in tours)
        {
            if (StaffMenuEdition == false)
            {
                if (tour.dateTime > DateTime.Now && tour.parttakers != tour.limit && tour.tourStarted == false)
                {
                    if (firstLoop == true)
                    {
                        lowestTourId = tour.tourId;
                        firstLoop = false;
                    }

                    Console.WriteLine($"\x1b[34m\x1b[1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
                }
                else if (DateTime.Now.Hour >= 16)
                {
                    lowestTourId = 420;
                    Console.WriteLine("\n\x1b[36;1mU kunt op dit tijdstip geen rondleidingen meer volgen, excuses voor het ongemak.\x1b[0m\n");

                    Console.Write("4.\r");
                    Thread.Sleep(1000);
                    Console.Write("3.\r");
                    Thread.Sleep(1000);
                    Console.Write("2.\r");
                    Thread.Sleep(1000);
                    Console.Write("1.\r");
                    Thread.Sleep(1000);

                    Console.Clear();
                    Menu.MainMenu();
                }
            }

            else if (StaffMenuEdition == true)
            {
                string tourStartedMessage;
                if (tour.tourStarted == true)
                    tourStartedMessage = "\x1b[32;1m || Deze rondleiding is gestart.\x1b[0m";
                else
                    tourStartedMessage = "";
                Console.WriteLine($"\x1b[34m\x1b[1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers}) {tourStartedMessage}");
            }

        }

        return lowestTourId;
    }

    public static string CheckIfReservation(string barcode)
    {
        List<TourModel> tours = BaseAccess.LoadAll();

        foreach (TourModel tour in tours)
        {
            foreach (Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    if (tour.tourStarted == true)
                    {
                        return $"U heeft de rondleiding van {tour.dateTime} al gevolgd.";
                    }
                    return "true";
                }
            }
        }
        return "false";
    }

    public static string GetTourTime(string barcode, bool staffEdition)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    if (staffEdition == true)
                    {
                        return tour.dateTime.ToString("dd-M-yyyy HH:mm");
                    }
                    else return $"U heeft een rondleiding geboekt om \x1b[32m{tour.dateTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m\n";
                }
            }
        }
        return "U heeft nog geen rondleiding geboekt\n";
    }

    public static string ChooseTour(string barcode, int lowestTourId)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        while (true)
        {
            string input = Console.ReadLine();
            Console.Clear();

            if (int.TryParse(input, out int chosenTourID) && chosenTourID >= lowestTourId && chosenTourID <= tours[tours.Count - 1].tourId)
            {
                foreach (TourModel tour in tours)
                {
                    if (tour.tourId == chosenTourID)
                    {
                        if (tour.parttakers < tour.limit && tour.tourStarted == false)
                        {
                            Add_Remove.Add(new Visitor(barcode), tour.tourId);
                            return $"Succesvol aangemeld bij rondleiding {tour.tourId} van \x1b[32m{tour.dateTime.ToString("HH:mm")}\x1b[0m\n";
                        }
                        else
                        {
                            Console.WriteLine("U heeft een incorrect of ongeldig rondleidingsnummer opgegeven, probeer het opnieuw.\n");
                            return SelectTour.SelectATour(barcode);
                        }
                    }
                }
            }
            else if (input.ToLower() == "q")
            {
                Console.Clear();
                Menu.MainMenu();
            }
            else
            {
                Console.WriteLine("U heeft een incorrect rondleidingsnummer opgegeven, probeer het opnieuw.\n");
                return SelectTour.SelectATour(barcode);
            }
        }
    }


    public static string CancelReservation(string barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    Add_Remove.Remove(new Visitor(barcode), tour.tourId);
                    return $"Uw tour van \x1b[32m{tour.dateTime}\x1b[0m is geannuleerd. Nog een prettige dag verder!";
                }
            }
        }
        return "U heeft nog geen tour ingepland\n";
    }

    public static string CheckIfTourIsStarted(int tourid)
    {
        List<TourModel> tours = BaseAccess.LoadAll();
        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourid && tour.tourStarted == true)
            {
                Console.Clear();
                return $"\x1b[31;1mDeze rondleiding is al gestart en kan niet worden aangepast.\x1b[0m";
            }
        }
        return null;
    }
}