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
        Console.WriteLine($"Bij deze rondleidingen kunt u zich aanmelden:\n");
        foreach (TourModel tour in tours)
        {
            Console.WriteLine($"{tour.tourId}: Rondleiding van {tour.dateTime}");
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
        DateTime selectedTime;
        List<TourModel> tours = baseLogic.GetAllTours();
        //Chosentour = id of tour chosen by visitor
        while (true)
        {
            string input = Console.ReadLine();
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
                    else
                    {
                        Console.WriteLine("Deze tour is helaas vol, probeer een andere optie.\n");
                        continue;
                    }
                }
            }
            else
            {
                Console.WriteLine("U heeft een incorrect tournummer opgegeven, probeer het opnieuw.");
                continue;
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