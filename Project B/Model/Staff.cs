namespace ProjectB;

public class Staff
{
    public static List<TourModel> tours = BaseAccess.LoadTours();
    public List<string> scannedIDS = new();
    public string StaffBarode;

    public Staff(string staffBarcode)
    {
        StaffBarode = staffBarcode;
    }

    public bool CorrectStaffCode()
    {
        List<string> staffBarodes = BaseAccess.loadAllStaffCodes();

        if (staffBarodes.Contains(StaffBarode))
        {
            return true;
        }
        return false;
    }

    public static string[] GetVisitorsInTour(List<Visitor> VisitorsWithReservation)
    {
        List<string> VisitorsList = new List<string>();
        foreach (Visitor visitor in VisitorsWithReservation)
            VisitorsList.Add(visitor.barcode);

        string[] Reservations = VisitorsList.ToArray();
        return Reservations;
    }

    public static void CheckPresence(int tourID)
    {
        List<Visitor> VisitorsWithReservation = new List<Visitor>();
        DateTime TourStartTime = Program.World.Now;

        foreach (TourModel tour in tours)
        {
            if (tourID == tour.tourId)
            {
                foreach (Visitor visitor in tour.reservationsList)
                {
                    VisitorsWithReservation.Add(visitor);
                }
                TourStartTime = tour.dateTime;
            }
        }
        string[] Reservations = GetVisitorsInTour(VisitorsWithReservation);

        while(true)
        {
            string PresentVisitorBarcode = StaffCheckPresence.Show(Reservations, TourStartTime);

            switch (PresentVisitorBarcode.ToLower())
            {
                case "q":
                {
                    StaffController.TourSelectedMenu(tourID);
                    break;
                }
                case "k":
                {
                    // Next
                    break;
                }
                default:
                {
                    if (Reservations.Length != 0 && Reservations.Contains(PresentVisitorBarcode))
                    {
                        AddRemove.AddToTourlist(new Visitor(PresentVisitorBarcode), tourID);
                        AddRemove.RemoveFromReservations(new Visitor(PresentVisitorBarcode), tourID);
                        VisitorsWithReservation.Remove(new Visitor(PresentVisitorBarcode));
                        CheckPresence(tourID);
                    }
                    else
                    {
                        Console.WriteLine("Dat was de laatste");
                        StaffTourStarted.Show();
                    }
                }
                break;
            }
        }
    }
}
    
    // public static List<Visitor> CheckPresence(List<Visitor> reservations, DateTime tourStartTime, int tourID)
    // {
    //     while (true)
    //     {
    //         CheckPresenceAgain(tourID);
    //         string checkPresence = CheckPresenceMenu.Show(GetVisitorsInTour(reservations), tourStartTime);
    //         if (checkPresence.ToLower() == "q")
    //         {
    //             SelectionMenu();
    //         }
    //         if (checkPresence.ToLower() == "k")
    //         {
    //             return reservations;
    //         }
    //         else
    //         {
    //             if (long.TryParse(checkPresence, out _))
    //             {
    //                 if (BaseAccess.GetVisitorInTour(tourID, checkPresence.Trim()))
    //                 {
    //                     scannedIDS.Add(checkPresence);
    //                     foreach (Visitor visitor in reservations)
    //                         if (visitor.barcode == checkPresence)
    //                             reservations.Remove(visitor);
    //                     Console.WriteLine($"\x1b[32;1m{checkPresence} is gecontroleerd.\x1b[0m");
    //                     continue;
    //                 }
    //                 else
    //                 {
    //                     Visitor visitor = new Visitor(checkPresence);
    //                     string tourTime = Tour.GetTourTime(visitor, true);
    //                     if (tourTime != "U heeft nog geen rondleiding geboekt\n")
    //                     {
    //                         Console.WriteLine($"\x1b[33;1mDeze bezoeker ({checkPresence}) heeft zich aangemeld bij een \x1b[33;1;4mandere rondleiding\x1b[0m\x1b[33;1m (van {tourTime})\x1b[0m");
    //                         Console.WriteLine("\x1b[31;1mDus dat is een incorrecte barcode.\x1b[0m\n");
    //                     }
    //                     else
    //                     {
    //                         Console.WriteLine("\x1b[31;1mDat is een incorrecte barcode.\x1b[0m");
    //                     }

    //                     continue;
    //                 }
    //             }
    //             else
    //             {
    //                 WrongInput.Show();
    //                 CheckPresenceMenu.Show(GetVisitorsInTour(reservations), tourStartTime);
    //                 continue;
    //             }
    //         }
    //     }
    // }

    // public static void CheckPresenceAgain(int tourID)
    // {
    //     scannedIDS.Clear();

    //     foreach (TourModel tour in tours)
    //     {
    //         if (tour.tourId == tourID)
    //         {
    //             DateTime tourStartTime = tour.dateTime;
    //             List<Visitor> reservations = new(tour.reservationsList);
    //             List<Visitor> presenceList = CheckPresence(reservations, tourStartTime, tourID);

    //             foreach (string scanned in scannedIDS)
    //                 foreach (Visitor visitor in reservations)
    //                     if (visitor.barcode == scanned)
    //                         presenceList.Remove(visitor);

    //             if (presenceList == null)
    //             {
    //                 return;
    //             }
    //             else if (presenceList.Count() == 0)
    //             {
    //                 Console.WriteLine("Iedereen is aanwezig.");
    //             }
    //             else if (presenceList.Count() > 0)
    //             {
    //                 Console.WriteLine("\x1b[1mDeze ID's zijn afwezig:\x1b[0m\n");
    //                 foreach (Visitor notpresent in presenceList)
    //                     Console.WriteLine($"> {notpresent.barcode}");
    //                 Console.WriteLine($"Aantal mensen afwezig: {presenceList.Count}.\n");

    //             }
    //             Console.WriteLine("Druk op \x1b[32m'S'\x1b[0m om de tour te starten.");
    //             Console.WriteLine("Druk op \x1b[31m'Q'\x1b[0m om terug te gaan en verder te scannen.");
    //             switch (Console.ReadLine().ToLower())
    //             {
    //                 case "s":
    //                     {
    //                         foreach (Visitor notpresent in presenceList)
    //                             AddRemove.Remove(notpresent, tourID);

    //                         List<TourModel> tours2 = BaseAccess.LoadTours();
    //                         TourModel tourToUpdate = tours2.FirstOrDefault(t => t.tourId == tour.tourId);

    //                         if (tourToUpdate != null)
    //                             tourToUpdate.tourStarted = true;

    //                         BaseAccess.WriteAll(tours2);
    //                         Console.WriteLine("\x1b[32;1mDe rondleiding is succesvol gestart.\x1b[0m");
    //                         Console.WriteLine("\nToets 'ENTER' om terug te gaan naar het hoofdmenu.");
    //                         Console.ReadLine();
    //                         MenuController.Start();
    //                         break;
    //                     }
    //                 case "q":
    //                     {
    //                         CheckPresenceAgain(tourID);
    //                         break;
    //                     }
    //                 default:
    //                     {
    //                         WrongInput.Show();
    //                         CheckPresenceAgain(tourID);
    //                         break;
    //                     }
    //             }
    //         }
    //     }
    // }
