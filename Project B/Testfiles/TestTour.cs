namespace ProjectB;

public class TestTour
{
    private static BaseLogic baseLogic = new BaseLogic();
    public int currentTourID = 1;
    public int pastTourCounter;
    public readonly IWorld World;
    public TestTour(IWorld world)
    {
        World = world;
    }
    public void Load_Tours(bool _)
    {
        List<TourModel> tours = baseLogic.GetAllTours();

        foreach (TourModel tour in tours)
        {
            World.WriteLine($"\x1b[34m\x1b[1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
        }
    }

    public void TestLoad_Tours()
    {
        List<TourModel> tours = BaseAccess.LoadAll();
        currentTourID = 1;
        pastTourCounter = 0;

        foreach (TourModel tour in tours)
        {
            tours = BaseAccess.LoadAll();
            // if (tour.dateTime > DateTime.Now && tour.parttakers != tour.limit && tour.tourStarted == false)
            if (tour.parttakers != tour.limit && tour.tourStarted == false)
            {
                if (currentTourID < 10)
                {
                    World.WriteLine($"\x1b[34;1m{currentTourID}\x1b[0m:  Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
                }
                else
                {
                    World.WriteLine($"\x1b[34;1m{currentTourID}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
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

    public string ChooseTour(string barcode)
    {
        List<TourModel> tours = baseLogic.GetAllTours();

        string input = World.ReadLine();

        if (int.TryParse(input, out int chosenTourID) && chosenTourID >= 0 )
        {
            foreach (TourModel tour in tours)
            {
                if (tour.tourId == chosenTourID && tour.parttakers < tour.limit)
                {
                    Add_Remove.Add(new Visitor(barcode), tour.tourId);
                    return $"Succesvol aangemeld bij de rondleiding van \x1b[32m{tour.dateTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m\n";
                }
            }
        }
        else if (input.ToLower() == "q")
        {
            Console.Clear();
            return "";
        }
        Console.WriteLine("U heeft een incorrect tournummer opgegeven, probeer het opnieuw.");
        return SelectTour.SelectATour(barcode);        
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