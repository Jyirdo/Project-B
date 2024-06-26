namespace ProjectB;

public class Staff
{
    public static List<TourModel> tours = BaseAccess.LoadTours();
    public static List<string> scannedIDS = new();
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
    
    public static List<Visitor> CheckPresence(List<Visitor> reservationsList, DateTime tourStartTime, int tourID)
    {
        List<Visitor> reservationsCopy = new(reservationsList);
        List<string> reservationsCopyString = new();

        while (true)
        {
            reservationsCopyString.Clear();
            foreach (Visitor visitor in reservationsCopy)
            {
                reservationsCopyString.Add(visitor.barcode);
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
                            if (visitor.barcode == checkPresence)
                            {
                                reservationsCopy.Remove(visitor);
                                AddRemove.RemoveFromReservations(visitor, tourID);
                                AddRemove.AddToTourlist(visitor, tourID);
                            }
                        StaffCheckPresenceSucces.Show(checkPresence);
                        continue;
                    }
                    else
                    {
                        Visitor visitor = new Visitor(checkPresence);
                        string tourTime = Tour.GetTourTime(visitor, true);
                        if (tourTime != "U heeft nog geen rondleiding geboekt\n")
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
        scannedIDS.Clear();
        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourID)
            {
            CheckThePresence:
                List<Visitor> presenceList = CheckPresence(tour.reservationsList, tour.dateTime, tourID);

                foreach (string scanned in scannedIDS)
                    foreach (Visitor vis in tour.reservationsList)
                        if (vis.barcode == scanned)
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
                        presenceListString.Add(notpresent.barcode);
                    string[] presenceListArray = presenceListString.ToArray();
                    AllNotPresent.Show(presenceListArray);
                }
                
                TourAboutToStartWithOptionForExtraVisitors:
                string input = "";
                if (tour.limit - tour.parttakers != 0)
                {
                    input = StaffPresenceCheckedTourNotFull.Show(tour.parttakers, tour.limit);
                    Visitor visitor = new Visitor(input);
                    if (visitor.CorrectVisitorCode())
                    {
                        if (tour.tourVisitorList.Contains(visitor))
                        {
                            Console.WriteLine("Dat mag niet");
                        }
                        else
                        {
                            AddRemove.AddToTourlist(visitor, tourID);
                        }
                        foreach (TourModel item in tours)
                        {
                            AddRemove.RemoveFromReservations(visitor, item.tourId);
                        }
                        StaffCheckPresenceSucces.Show(visitor.barcode);
                        goto TourAboutToStartWithOptionForExtraVisitors;
                    }
                }
                else
                {
                    TourAboutToStart:
                    input = StaffPresenceCheckedTourFull.Show();
                    switch (input.ToLower())
                    {
                        case "s":
                            {
                                foreach (Visitor notpresent in presenceList)
                                    AddRemove.RemoveFromReservations(notpresent, tourID);

                                List<TourModel> tours2 = BaseAccess.LoadTours();
                                TourModel tourToUpdate = tours2.FirstOrDefault(t => t.tourId == tour.tourId);

                                if (tourToUpdate != null)
                                    tourToUpdate.tourStarted = true;

                                BaseAccess.WriteAll(tours2);
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
                                goto TourAboutToStart;
                            }
                    }
                }
            }
        }
    }
}