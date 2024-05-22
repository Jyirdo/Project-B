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

    public static void Choose_Tour(long barcode)
    {
        DateTime selectedTime;
        List<TourModel> tours = baseLogic.GetAllTours();
        //Chosentour = id of tour chosen by visitor
        while (true)
        {
            Console.WriteLine("Toets het nummer van de rondleiding in waarvoor u zich wilt aanmelden:");
            int chosenTourId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(tours.Count());
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
                            //BaseLogic.AddVisitorsToTourJson(newClient, chosenTourId);

                            Console.WriteLine($"Succesvol aangemeld bij de rondleiding van {(tour.dateTime).ToString("dd-M-yyyy HH:mm")}\n");
                        }
                        else
                        {
                            Console.WriteLine("Deze tour is helaas vol, probeer een andere optie.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer het opnieuw.");
                    }
                }

            }
        }

    }

    // public void Cancel()
    // {
    //     if (CheckInReservationJson(universalClientCode))
    //     {
    //         Reservation cancelReservation = GetReservationFromJson(universalClientCode);
    //         if (cancelReservation != null)
    //         {
    //             removeFromReservationJson(new Visitor(Convert.ToInt64(cancelReservation.ReservationId), Convert.ToDateTime(cancelReservation.DateTime), Convert.ToInt32(cancelReservation.TourNumber)));
    //             Console.WriteLine("Succesvol afgemeld bij uw rondleiding. Prettige dag verder!");
    //             return;
    //         }
    //         else
    //             Console.WriteLine("Uw reservering is helaas niet gevonden. Probeer het opnieuw.");
    //         return;
    //     }
    // }
}