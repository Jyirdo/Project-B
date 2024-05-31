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

    public static void Load_Tours()
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        currentTourID = 1;
        pastTourCounter = 0;

        Console.WriteLine("\nBij deze rondleidingen kunt u zich op dit moment aanmelden:");
        foreach (TourModel tour in tours)
        {
            if (tour.dateTime > DateTime.Now && tour.parttakers != tour.limit)
            {
                Console.WriteLine($"\x1b[34m\x1b[1m{currentTourID}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m");
                currentTourID++;
            }
            else
            {
                pastTourCounter++;
                continue;
            }
        }
    }

    public static bool CheckIfReservation(long barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        foreach (TourModel tour in tours)
        {
            foreach(Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    public static string GetTourTime(long barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    return $"{tour.dateTime.ToString("dd-M-yyyy HH:mm")}";
                }
            }
        }
        return "U heeft geen rondleiding geboekt";
    }

    public static string ChooseTour(long barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        while (true)
        {
            // request user input
            string input = Console.ReadLine();

            if (int.TryParse(input, out int chosenTourID) && chosenTourID > 0 && chosenTourID < currentTourID)
            {
                chosenTourID += pastTourCounter;
                foreach (TourModel tour in tours)
                {
                    for (int i = 1; i <= currentTourID; i++)
                    {
                        if (tour.tourId == chosenTourID && tour.parttakers < tour.limit)
                        {
                            Add_Remove.Add(new Visitor(Convert.ToInt64(barcode)), tour.tourId);
                            return $"Succesvol aangemeld bij de rondleiding van {tour.dateTime.ToString("dd-M-yyyy HH:mm")}\n";
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("U heeft een incorrect tournummer opgegeven, probeer het opnieuw.");
                return SelectTour.SelectATour(barcode);
            }
        }
    }

    public static string CancelReservation(long barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    Add_Remove.Remove(new Visitor(Convert.ToInt64(barcode)), tour.tourId);
                    return $"Uw tour van \x1b[32m{tour.dateTime}\x1b[0m is geannuleerd. Nog een prettige dag verder!";
                }
            }
        }
        return "U heeft nog geen tour ingepland";
    }
}