namespace ProjectB;

public class Tour
{
    public int tourId;
    public DateTime tourStartTime;
    public static int parttakers;
    public static bool opentourspots;
    public static int limit;

    public static int currentTourID = 1;
    public static int pastTourCounter;
    private static BaseLogic baseLogic = new BaseLogic();

    public Tour(int id, DateTime time)
    {
        tourId = id;
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
        limit = 13;
    }

    public static void Load_Tours(bool staffLogin)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        currentTourID = 1;
        pastTourCounter = 0;

        foreach (TourModel tour in tours)
        {
            if (staffLogin)
            {
                Console.WriteLine($"\x1b[34m\x1b[1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
            }
            else
            {
                if (tour.dateTime > DateTime.Now && tour.parttakers != tour.limit)
                {
                    if (currentTourID < 10)
                    {
                        Console.WriteLine($"\x1b[34m\x1b[1m{currentTourID}\x1b[0m:  Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
                    }
                    else
                    {
                        Console.WriteLine($"\x1b[34m\x1b[1m{currentTourID}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
                    }
                    currentTourID++;
                }
                else
                {
                    pastTourCounter++;
                    continue;
                }
            }
        }
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
                    else return $"U heeft een rondleiding geboekt om \x1b[32m{tour.dateTime.ToString("dd-M-yyyy HH:mm:ss")}\x1b[0m\n";
                }
            }
        }
        return "U heeft nog geen rondleiding geboekt\n";
    }

    public static string ChooseTour(string barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();

        while (true)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out int chosenTourID) && chosenTourID >= 0 )
            {
                chosenTourID += pastTourCounter;
                foreach (TourModel tour in tours)
                {
                    if (tour.tourId == chosenTourID && tour.parttakers < tour.limit)
                    {
                        for (int i = 1; i <= currentTourID; i++)
                        {
                            if (tour.tourId == chosenTourID && tour.parttakers < tour.limit)
                            {
                                Add_Remove.Add(new Visitor(barcode), tour.tourId);
                                return $"Succesvol aangemeld bij de rondleiding van \x1b[32m{tour.dateTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m\n";
                            }
                        }
                    }
                }
            }
            else if (input.ToLower() == "q")
            {
                Console.Clear();
                return "";
            }
            else
            {
                Console.WriteLine("U heeft een incorrect tournummer opgegeven, probeer het opnieuw.");
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