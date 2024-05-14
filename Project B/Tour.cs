using Newtonsoft.Json;

public class Tour
{
    public static List<Tour> listoftours = new();
    public int tour_id;
    public DateTime tourStartTime;
    public int parttakers;
    public bool opentourspots;
    public int limit;
    public static int tourAmount = 0;
    private static BaseLogic baseLogic = new BaseLogic();

    public Tour(int id, DateTime time)
    {
        tour_id = id;
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
        limit = 13;
    }

    public static void Load_Tours()
    {
        List<TourModel> tours = baseLogic.GetAllTours();
        // List<Dictionary<string, string>> tourTimes;

        // tourTimes = readFromTourJson("tour_times.json");
        foreach (TourModel tour in tours)
        {
            Console.WriteLine($"{tour.tourId}: Rondleiding van {tour.dateTime}");
        }
    }

    public static void Print_Tours()
    {
        int tour_id = 1;

        Console.WriteLine($"Bij deze rondleidingen kunt u zich aanmelden:\n");
        foreach (Tour tour in listoftours)
        {
            if (tour.tourStartTime > DateTime.Now)
            {
                Console.WriteLine($"{tour_id}: Rondleiding van {tour.tourStartTime}");
                tour_id++;
                if (Convert.ToInt32(tour.tour_id) > tourAmount)
                {
                    tourAmount = Convert.ToInt32(tour.tour_id);
                }
            }
        }
    }

    public static void Choose_Tour(long barcode)
    {
        DateTime selectedTime;

        //Chosentour = id of tour chosen by visitor
        
        Console.WriteLine("Toets het nummer van de rondleiding in waarvoor u zich wilt aanmelden:");
        int chosenTourInt = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(tourAmount);
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
                        Visitor newClient = new Visitor(barcode, selectedTime, chosenTourInt);
                        Console.WriteLine(newClient.barcode);
                        //WriteToReservationJson(newClient);

                        //Add visitor to the list in their tour
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

    public static List<Dictionary<string, string>> readFromTourJson(string filepath)
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

    
    public void AddVisitorsToTour(bool add)
    {
        // Removes visitors if false, adds visitor if true.

        if (add == false)
        {
            if (parttakers > 0)
                parttakers--;
        }
        else if (add == true)
        {
            if (parttakers < limit)
                parttakers++;
        }

    }

    public bool CheckTourFullness()
    {
        // Returs true if tour is full and otherwise false.

        if (parttakers == limit)
        {
            return true;
        }
        return false;
    }
}