using System.Drawing;

namespace ProjectB;

public class Tour
{
    public int tourId;
    public DateTime tourStartTime;
    public int parttakers;
    public bool opentourspots;
    public int limit;
    public static int currentTourID = 1;
    public static int pastTourCounter;

    public Tour(int id, DateTime time)
    {
        tourId = id;
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
        limit = 13;
    }

    public static string[] ShowTours(bool staffLogin)
    {
        currentTourID = 1;
        pastTourCounter = 0;
        List<string> ToursList = new List<string>();
        List<TourModel> tours = BaseAccess.LoadTours();

        foreach (TourModel tour in tours)
        {
            if (staffLogin)
            {
                string startedMessage = "";
                string warningMessage = "";

                if (tour.tourStarted == true)
                    startedMessage = "\x1b[32;1m|| Deze rondleiding is gestart.\x1b[0m";

                else if (tour.tourStarted == false && tour.parttakers == 0 && tour.dateTime < Program.World.Now)
                    warningMessage = "\x1b[33;1m|| Deze rondleiding is niet gestart en de starttijd is al geweest, maar er zijn 0 deelnemers.\x1b[0m";

                else if (tour.tourStarted == false && tour.dateTime < Program.World.Now)
                    warningMessage = $"\x1b[31;1m|| Let op!: De starttijd van deze rondleiding is al geweest, maar deze rondleiding is nog niet gestart. ({tour.parttakers} deelnemer(s) wachten.)\x1b[0m";

                if (tour.guide == null)
                {
                    ToursList.Add($"\x1b[34;1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers}) (Gids: geen) {warningMessage}{startedMessage}");
                }    
                else
                {
                    ToursList.Add($"\x1b[34;1m{tour.tourId}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers}) (Gids: {tour.guide.Name}) {warningMessage}{startedMessage}");
                }
                
            }
            else
            {
                if (Program.World.Now > tours[tours.Count - 1].dateTime)
                {
                    NoMoreTours.Show();
                    MenuController.Start();
                }
                else if (tour.dateTime > Program.World.Now && tour.parttakers != tour.limit && tour.tourStarted == false)
                {
                    if (currentTourID < 10)
                    {
                        ToursList.Add($"\x1b[34;1m{currentTourID}\x1b[0m:  Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
                    }
                    else
                    {
                        ToursList.Add($"\x1b[34;1m{currentTourID}\x1b[0m: Rondleiding van \x1b[32m{tour.dateTime}\x1b[0m (Plaatsen over: {tour.limit - tour.parttakers})");
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
        List<TourModel> tours = BaseAccess.LoadTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor vis in tour.reservationsList)
            {
                if (visitor.barcode == vis.barcode)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool VisitorWithTour(Visitor visitor)
    {
        List<TourModel> tours = BaseAccess.LoadTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor vis in tour.tourVisitorList)
            {
                if (visitor.barcode == vis.barcode)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static string GetTourTime(Visitor visitor, bool staffEdition)
    {
        List<TourModel> tours = BaseAccess.LoadTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor vis in tour.reservationsList)
            {
                if (visitor.barcode == vis.barcode)
                {
                    if (staffEdition)
                    {
                        return tour.dateTime.ToString("dd-M-yyyy HH:mm");
                    }
                    else 
                    {
                        return $"U heeft een rondleiding gereserveerd om \x1b[32m{tour.dateTime.ToString("dd-M-yyyy HH:mm:ss")}\x1b[0m\n";
                    }
                }
            }
        }
        return "U heeft nog geen rondleiding gereserveerd\n";
    }

    public static string MakeReservation(Visitor visitor, int tourID)
    {
        List<TourModel> tours = BaseAccess.LoadTours();
        while (true)
        {
            if (tourID >= 0 && tourID < currentTourID)
            {
                tourID += pastTourCounter;
                foreach (TourModel tour in tours)
                {
                    for (int i = 1; i <= currentTourID; i++)
                    {
                        if (tour.tourId == tourID && tour.parttakers < tour.limit)
                        {
                            AddRemove.AddToReservations(visitor, tour.tourId);
                            return $"Succesvol gereserveerd voor de rondleiding van \x1b[32m{tour.dateTime.ToString("dd-M-yyyy HH:mm")}\x1b[0m\n";
                        }
                    }
                }
            }
            else
            {
                WrongInput.Show();
                VisitorController.Login(visitor.barcode);
            }
        }
    }

    public static string CancelReservation(Visitor visitor)
    {
        List<TourModel> tours = BaseAccess.LoadTours();
        foreach (TourModel tour in tours)
        {
            foreach (Visitor vis in tour.reservationsList)
            {
                if (visitor.barcode == vis.barcode)
                {
                    AddRemove.RemoveFromReservations(visitor, tour.tourId);
                    return $"Uw reservering voor de rondleiding van \x1b[32m{tour.dateTime}\x1b[0m is geannuleerd. Nog een prettige dag verder!";
                }
            }
        }
        return "U heeft nog niet gereserveerd voor een rondleiding.\n";
    }

    public static string CheckIfTourStarted(int tourid)
    {
        List<TourModel> tours = BaseAccess.LoadTours();
        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourid && tour.tourStarted == true)
            {
                return $"\x1b[31;1mDeze rondleiding is al gestart en kan niet worden aangepast.\x1b[0m";
            }
        }
        return null;
    }
}