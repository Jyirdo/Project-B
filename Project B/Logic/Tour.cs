public class Tour
{
    public int tourId;
    public DateTime tourStartTime;
    public static int parttakers;
    public static bool opentourspots;
    public static int limit;
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
        int currentTourID = 1;

        foreach (TourModel tour in tours)
        {
            if (tour.dateTime > DateTime.Now)
            {
                Console.WriteLine($"\x1b[34m\x1b[1m{currentTourID}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m");
                currentTourID++;
            }
            else
            {
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

    public static string ChooseTour(long barcode, string input)
    {
        DateTime selectedTime;
        List<TourModel> tours = baseLogic.GetAllTours();
        //Chosentour = id of tour chosen by visitor
        while (true)
        {
            if (int.TryParse(input, out int chosenTourId) && chosenTourId > 0 && chosenTourId < tours.Count())
            {
                foreach (TourModel tour in tours)
                {
                    if (tour.tourId == chosenTourId && tour.parttakers < tour.limit)
                    {
                        selectedTime = Convert.ToDateTime(tour.dateTime);
                        Visitor newClient = new Visitor(barcode);
                        Add_Remove.Add(new Visitor(Convert.ToInt64(barcode)), tour.tourId);
                        return $"Succesvol aangemeld bij de rondleiding van {tour.dateTime.ToString("dd-M-yyyy HH:mm")}\n";
                    }
                }              
                Console.WriteLine("Deze tour is helaas vol, probeer een andere optie.");
                continue;
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
                    return $"Uw tour van {tour.dateTime} is geannuleerd";
                }
            }
        }
        return "U heeft nog geen tour ingepland";
    }
}