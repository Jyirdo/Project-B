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
            foreach(Visitor visitor in tour.tourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    return $"{(tour.dateTime).ToString("dd-M-yyyy HH:mm")}";
                }
            }
        }
        return "U heeft geen rondleiding geboekt";
    }
    
    public static void Choose_Tour(long barcode)
    {
        DateTime selectedTime;
        List<TourModel> tours = baseLogic.GetAllTours();
        //Chosentour = id of tour chosen by visitor
        while (true)
        {
            Console.WriteLine("Toets het nummer van de rondleiding in waarvoor u zich wilt aanmelden:");
            int chosenTourId = Convert.ToInt32(Console.ReadLine());
            foreach (TourModel tour in tours)
            {
                if (chosenTourId > 0 || chosenTourId < tours.Count())
                {
                    if (tour.tourId == chosenTourId)
                    {
                        // check if tour is full
                        if (tour.parttakers < tour.limit)
                        {
                            selectedTime = Convert.ToDateTime(tour.dateTime);
                            Visitor newClient = new Visitor(barcode);
                            Add_Remove.Add(new Visitor(Convert.ToInt64(barcode)), tour.tourId);

                            Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {(tour.dateTime).ToString("dd-M-yyyy HH:mm")}\n");
                            Menu.MainMenu();
                        }
                        else
                        {
                            Console.WriteLine("Deze tour is helaas vol, probeer een andere optie.\n");
                            continue;
                        }
                    }
                }
            }
            break;
        }
    }
}