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
                    foreach (Visitor visitor in tour.reservationsList)
                        if (visitor.barcode == scanned)
                            presenceList.Remove(visitor);

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
                string input = StaffPresenceChecked.Show();
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
                            break;
                        }
                }
            }
        }
    }

    public static string AddLastMinuteVisitor(int tourId, string input)
    {
        while (true)
        {
            if (long.TryParse(input, out _))
            {
                foreach (TourModel tour in tours)
                {
                    if (tour.tourId == tourId)
                    {
                        // check if tour is full
                        if (tour.parttakers < tour.limit)
                        {
                            AddRemove.AddToTourlist(new Visitor(input.Trim()), tourId);
                            return $"{input.Trim()} succesvol aangemeld bij de rondleiding van {(tour.dateTime).ToString("dd-M-yyyy HH:mm")}\n";
                        }
                        else
                        {
                            TourFull.Show();
                            StaffController.SelectionMenu();
                        }
                    }
                    else
                    {
                        WrongInput.Show();
                        continue;
                    }
                }
            }
            else if (input.ToLower() == "q")
            {
                return "";
            }
            else
            {
                WrongInput.Show();
                continue;
            }
        }
    }
}