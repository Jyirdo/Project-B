using Newtonsoft.Json;

namespace ProjectB;

public class Tour
{
    public static DateTime currentDate = Program.World.Now.Date;
    public static int currentTourID = 1;
    public static int pastTourCounter;

    [JsonProperty("tour_id")]
    public int tourId { get; set; }

    [JsonProperty("tourStartTime")]
    public DateTime tourStartTime { get; set; }

    [JsonProperty("parttakers")]
    public int parttakers { get; set; } = 0;

    [JsonProperty("limit")]
    public int limit { get; set; } = 13;

    [JsonProperty("guide")]
    public GuideModel guide { get; set; }

    [JsonProperty("tourVisitorList")]
    public List<Visitor> tourVisitorList { get; set; } = new();

    [JsonProperty("reservationsList")]
    public List<Visitor> reservationsList { get; set; } = new();

    [JsonProperty("tourStarted")]
    public bool tourStarted { get; set; } = false;

    public Tour(int id, DateTime time)
    {
        tourId = id;
        tourStartTime = time;
    }

    static List<Tour> CreateTours()
    {
        List<string> tourTimes = new List<string>
        {
            "10:20", "10:40", "11:00", "11:20", "11:40", "12:00", "12:20",
            "12:40", "13:00", "13:20", "13:40", "14:00", "14:20", "14:40",
            "15:00", "15:20", "15:40", "16:00"
        };
        List<Tour> newTourList = new List<Tour>();
        int tourNumber = 1;

        foreach (string time in tourTimes)
        {
            DateTime tourDateTime = DateTime.ParseExact(currentDate.ToString("yyyy-MM-dd") + " " + time, "yyyy-MM-dd HH:mm", null);
            Tour newTour = new Tour(tourNumber, tourDateTime);
            newTourList.Add(newTour);
            tourNumber++;
        }
        return newTourList;
    }

    public static string CheckTours()
    {
        List<Tour> jsonList;
        if (BaseAccess.LoadTours() == null)
        {
            var file = File.Create("Data/Tours.json");
            file.Close();
            BaseAccess.WriteAll(CreateTours());
            return "New file successfully created";
        }
        else
            jsonList = BaseAccess.LoadTours();

        bool isNewDay = false;

        foreach (Tour tour in jsonList)
        {
            if (tour.tourStartTime.Date < currentDate)
            {
                isNewDay = true;
                break;
            }
        }

        if (isNewDay)
        {
            try
            {
                string sourceFile = "Data/Tours.json";
                if (File.Exists(sourceFile))
                {
                    string destinationDirectory = "Data/PreviousJsons";
                    string destinationFileName = $"Tours-on-{jsonList[0].tourStartTime:yyyy-MM-dd}.json";
                    string destinationFile = Path.Combine(destinationDirectory, destinationFileName);

                    if (!Directory.Exists(destinationDirectory))
                        Directory.CreateDirectory(destinationDirectory);

                    File.Move(sourceFile, destinationFile);

                    BaseAccess.WriteAll(CreateTours());

                    return "File successfully moved and updated";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
        return "No new day detected or something went wrong";
    }

    public static string[] ShowTours(bool staffLogin)
    {
        currentTourID = 1;
        pastTourCounter = 0;
        List<string> ToursList = new List<string>();
        List<Tour> tours = BaseAccess.LoadTours();

        foreach (Tour tour in tours)
        {
            if (staffLogin)
            {
                string startedMessage = "";
                string warningMessage = "";

                if (tour.tourStarted == true)
                    startedMessage = "\x1b[32;1m|| Deze rondleiding is gestart.\x1b[0m";

                else if (tour.tourStarted == false && tour.parttakers == 0 && tour.tourStartTime < Program.World.Now)
                    warningMessage = "\x1b[33;1m|| Deze rondleiding is niet gestart en de starttijd is al geweest, maar er zijn 0 deelnemers.\x1b[0m";

                else if (tour.tourStarted == false && tour.tourStartTime < Program.World.Now)
                    warningMessage = $"\x1b[31;1m|| Let op!: De starttijd van deze rondleiding is al geweest, maar deze rondleiding is nog niet gestart. ({tour.parttakers} deelnemer(s) wachten.)\x1b[0m";

                ToursList.Add($"\x1b[34;1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.tourStartTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers}) {warningMessage}{startedMessage}");
            }
            else
            {
                if (Program.World.Now > tours[tours.Count - 1].tourStartTime)
                {
                    NoMoreTours.Show();
                    MenuController.Start();
                }
                else if (tour.tourStartTime > Program.World.Now && tour.parttakers != tour.limit && tour.tourStarted == false)
                {
                    if (currentTourID < 10)
                    {
                        ToursList.Add($"\x1b[34;1m{currentTourID}\x1b[0m:  Rondleiding van \x1b[32m{tour.tourStartTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
                    }
                    else
                    {
                        ToursList.Add($"\x1b[34;1m{currentTourID}\x1b[0m: Rondleiding van \x1b[32m{tour.tourStartTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
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
        string[] Tours = ToursList.ToArray();
        return Tours;
    }

    public static bool VisitorWithReservation(Visitor visitor)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        foreach (Tour tour in tours)
        {
            foreach (Visitor vis in tour.reservationsList)
            {
                if (visitor.Barcode == vis.Barcode)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool VisitorWithTour(Visitor visitor)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        foreach (Tour tour in tours)
        {
            foreach (Visitor vis in tour.tourVisitorList)
            {
                if (visitor.Barcode == vis.Barcode)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static string GetTourTime(Visitor visitor, bool staffEdition)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        foreach (Tour tour in tours)
        {
            foreach (Visitor vis in tour.reservationsList)
            {
                if (visitor.Barcode == vis.Barcode)
                {
                    if (staffEdition)
                    {
                        return tour.tourStartTime.ToString("dd-M-yyyy HH:mm");
                    }
                    else 
                    {
                        return $"U heeft een rondleiding gereserveerd om \x1b[32m{tour.tourStartTime.ToString("dd-M-yyyy HH:mm:ss")}\x1b[0m\n";
                    }
                }
            }
        }
        return "U heeft nog geen rondleiding gereserveerd\n";
    }

    public static (DateTime, bool) MakeReservation(Visitor visitor, int tourID)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        while (true)
        {
            if (tourID >= 0 && tourID < currentTourID)
            {
                tourID += pastTourCounter;
                foreach (Tour tour in tours)
                {
                    for (int i = 1; i <= currentTourID; i++)
                    {
                        if (tour.tourId == tourID && tour.parttakers < tour.limit)
                        {
                            AddToReservations(visitor, tour.tourId);
                            return (tour.tourStartTime, true);
                        }
                    }
                }
            }
            else
            {
                WrongInput.Show();
                VisitorController.Login(visitor.Barcode);
            }
        }
    }

    public static (DateTime, bool) CancelReservation(Visitor visitor)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        foreach (Tour tour in tours)
        {
            foreach (Visitor vis in tour.reservationsList)
            {
                if (visitor.Barcode == vis.Barcode)
                {
                    RemoveFromReservations(visitor, tour.tourId);
                    return (tour.tourStartTime, true);
                }
            }
        }
        return (Program.World.Now, false);
    }

    public static void RemoveFromReservations(Visitor visitor, int tourNumber)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        List<Visitor> visitorsToRemove = new List<Visitor>();

        foreach (Tour tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                foreach (Visitor existingVisitor in tour.reservationsList)
                if (existingVisitor.Barcode == visitor.Barcode)
                    visitorsToRemove.Add(existingVisitor);
            }
            tour.reservationsList.RemoveAll(visitorsToRemove.Contains);
            tour.parttakers = tour.reservationsList.Count();
        }
        BaseAccess.WriteAll(tours);
    }

    public static void RemoveFromTourlist(Visitor visitor, int tourNumber)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        List<Visitor> visitorsToRemove = new List<Visitor>();

        foreach (Tour tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                foreach (Visitor existingVisitor in tour.tourVisitorList)
                if (existingVisitor.Barcode == visitor.Barcode)
                    visitorsToRemove.Add(existingVisitor);
            }
            tour.tourVisitorList.RemoveAll(visitorsToRemove.Contains);
            tour.parttakers = tour.tourVisitorList.Count();
        }
        BaseAccess.WriteAll(tours);
    }

    public static void AddToReservations(Visitor visitor, int tourNumber)
    {
        List<Tour> tours = BaseAccess.LoadTours();

        foreach (Tour tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                tour.reservationsList.Add(visitor);
                tour.parttakers = tour.reservationsList.Count();
            }
        }
        BaseAccess.WriteAll(tours);
    }

    public static void AddToTourlist(Visitor visitor, int tourNumber)
    {
        List<Tour> tours = BaseAccess.LoadTours();

        foreach (Tour tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                tour.tourVisitorList.Add(visitor);
                tour.parttakers = tour.tourVisitorList.Count();
            }
        }
        BaseAccess.WriteAll(tours);
    }
}