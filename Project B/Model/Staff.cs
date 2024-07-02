namespace ProjectB;

public class Staff : Barcodable
{
    public static List<Tour> tours = BaseAccess.LoadTours();
    public static List<string> scannedIDS = new();

    public Staff(string barcode)
    {
        Barcode = barcode;
    }

    public bool CorrectStaffCode()
    {
        List<string> staffBarodes = BaseAccess.loadAllStaffCodes();

        if (staffBarodes.Contains(Barcode))
        {
            return true;
        }
        return false;
    }
    
    public static List<Visitor> CheckPresence(List<Visitor> reservationsList, DateTime tourStartTime, int tourID)
    {
        List<Visitor> reservationsCopy = new(reservationsList);
        List<string> reservationsCopyString = new();

        while (true)
        {
            reservationsCopyString.Clear();
            foreach (Visitor visitor in reservationsCopy)
            {
                reservationsCopyString.Add(visitor.Barcode);
            }
            string[] ReservationsArray = reservationsCopyString.ToArray();

            string checkPresence = StaffCheckPresence.Show(ReservationsArray, tourStartTime);

            if (checkPresence.ToLower() == "q")
            {
                StaffController.SelectionMenu();
            }
            if (checkPresence.ToLower() == "k")
            {
                return reservationsCopy;
            }
            else
            {
                if (long.TryParse(checkPresence, out _))
                {
                    if (BaseAccess.GetVisitorInTour(tourID, checkPresence.Trim()))
                    {
                        scannedIDS.Add(checkPresence);
                        foreach (Visitor visitor in reservationsList)
                            if (visitor.Barcode == checkPresence)
                            {
                                reservationsCopy.Remove(visitor);
                                foreach (Tour tour in tours)
                                {
                                    Tour.RemoveFromReservations(visitor, tour.tourId);
                                }
                                Tour.AddToTourlist(visitor, tourID);
                            }
                        SoundAccess.PlayAccepted();
                        StaffCheckPresenceSucces.Show(checkPresence);
                        continue;
                    }
                    else
                    {
                        Visitor visitor = new Visitor(checkPresence);
                        string tourTime = Tour.GetTourTime(visitor, true);
                        if (tourTime != "U heeft nog geen rondleiding gereserveerd\n")
                        {
                            StaffCheckPresenceDenied.Show(checkPresence, tourTime);
                        }
                        else
                        {
                            WrongInput.Show();
                        }
                        continue;
                    }
                }
                else
                {
                    WrongInput.Show();
                    continue;
                }
            }
        }
    }

    public static void SelectTourAndCheckTour(int tourID)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        scannedIDS.Clear();
        foreach (Tour tour in tours)
        {
            if (tour.tourId == tourID)
            {
                if (tour.tourStarted == false)
                {
                    CheckThePresence:
                    List<Visitor> presenceList = CheckPresence(tour.reservationsList, tour.tourStartTime, tourID);

                    foreach (string scanned in scannedIDS)
                        foreach (Visitor vis in tour.reservationsList)
                            if (vis.Barcode == scanned)
                                presenceList.Remove(vis);

                    if (presenceList == null)
                    {
                        return;
                    }
                    else if (presenceList.Count() == 0)
                    {
                        AllPresent.Show();
                    }
                    else if (presenceList.Count() > 0)
                    {
                        List<string> presenceListString = new();
                        foreach (Visitor notpresent in presenceList)
                            presenceListString.Add(notpresent.Barcode);
                        string[] presenceListArray = presenceListString.ToArray();
                        AllNotPresent.Show(presenceListArray);
                    }

                    TourAboutToStartWithOptionForExtraVisitors:
                    List<Tour> tours2 = BaseAccess.LoadTours();
                    foreach (Tour tour2 in tours2)
                    {
                        if (tour2.tourId == tourID)
                        {
                            if (tour2.limit > tour2.parttakers)
                            {
                                string input = StaffPresenceCheckedTourNotFull.Show(tour2.parttakers, tour2.limit);
                                Visitor visitor = new Visitor(input);
                                if (visitor.CorrectVisitorCode())
                                {
                                    if(AddLastMinuteVisitor(tourID, visitor))
                                    {
                                        SoundAccess.PlayAccepted();
                                        StaffCheckPresenceSucces.Show(visitor.Barcode);
                                        foreach (Tour tour4 in tours)
                                        {
                                            Tour.RemoveFromReservations(visitor, tour4.tourId);
                                        }
                                        Tour.AddToTourlist(visitor, tourID);
                                    }
                                    else
                                    {
                                        StaffCheckPresenceDenied.Show(visitor.Barcode, tour.tourStartTime.ToString("dd-M-yyyy HH:mm"));
                                    }
                                    goto TourAboutToStartWithOptionForExtraVisitors;
                                }
                                else
                                {
                                    switch (input.ToLower())
                                    {
                                        case "s":
                                            {
                                            foreach (Visitor notpresent in presenceList)
                                                Tour.RemoveFromReservations(notpresent, tourID);

                                            List<Tour> tours3 = BaseAccess.LoadTours();
                                            foreach (Tour tourToUpdate in tours3)
                                            {
                                                if (tourToUpdate.tourId == tourID)
                                                {
                                                    tourToUpdate.tourStarted = true;
                                                }
                                            }
                                            BaseAccess.WriteAll(tours3);
                                            StaffTourStarted.Show();
                                            MenuController.Start();
                                            break;
                                            }
                                        case "q":
                                            {
                                                goto CheckThePresence;
                                            }
                                        default:
                                            {
                                                WrongInput.Show();
                                                goto TourAboutToStartWithOptionForExtraVisitors;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                TourAboutToStart:
                                string input = StaffPresenceCheckedTourFull.Show();
                                switch (input.ToLower())
                                {
                                    case "s":
                                        {
                                            foreach (Visitor notpresent in presenceList)
                                                Tour.RemoveFromReservations(notpresent, tourID);

                                            List<Tour> tours3 = BaseAccess.LoadTours();
                                            foreach (Tour tourToUpdate in tours3)
                                            {
                                                if (tourToUpdate.tourId == tourID)
                                                {
                                                    tourToUpdate.tourStarted = true;
                                                }
                                            }
                                            BaseAccess.WriteAll(tours3);
                                            StaffTourStarted.Show();
                                            MenuController.Start();
                                            break;
                                        }
                                    case "q":
                                        {
                                            StaffController.SelectionMenu();
                                            break;
                                        }
                                    default:
                                        {
                                            WrongInput.Show();
                                            goto TourAboutToStart;
                                        }
                                }
                            }
                        }
                    }
                }
                else if (tour.tourStarted == true)
                {
                    TourStartedCantBeSelected.Show();
                    StaffController.SelectionMenu();
                }
            }
        }
    }

    public static bool AddLastMinuteVisitor(int tourID, Visitor visitor)
    {
        List<Tour> tours = BaseAccess.LoadTours();
        while (true)
        {
            foreach (Tour tour in tours)
            {
                if (tour.tourId == tourID)
                {
                    foreach (Visitor vis in tour.tourVisitorList)
                    {
                        if (vis.Barcode == visitor.Barcode)
                        {
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return true;
                }
            }
        }
    }
}