namespace ProjectB;

public static class VisitorController
{
    public static void Login(string barcode)
    {
        Visitor visitor = new Visitor(barcode);
        // visitor already made a reservation for a tour
        if (Tour.VisitorWithReservation(visitor))
        {
            string input = VisitorMenu.Show(visitor);

            switch (input.ToLower())
            {
                case "a":
                {
                    (DateTime tourStartTime, bool success) = Tour.CancelReservation(visitor);

                    if (success)
                        VisitorCancelReservation.Show(tourStartTime);
                    else
                        NoReservationYet.Show();
                    MenuController.Start();
                    break;
                }
                case "h":
                {
                    SoundAccess.PlayHelp();
                    Help.Show();
                    Login(barcode);
                    break;
                }
                case "q":
                {
                    MenuController.Start();
                    break;
                }
                default:
                {
                    WrongInput.Show();
                    Login(barcode);
                    break;
                }
            }
        }
        // visitor already participated in a tour
        else if (Tour.VisitorWithTour(visitor))
        {
            VisitorAlreadyParticipatedInTour.Show();
            MenuController.Start();
        }
        // visitor did not yet make a reservation for a tour
        else
        {
            string input = VisitorMakeReservation.Show();

            if (int.TryParse(input, out int tourID))
            {
                (DateTime tourStartTime, bool success) = Tour.MakeReservation(visitor, tourID);

                if (success)
                    VisitorMakeReservationSucces.Show(tourStartTime);
                else
                    NoReservationYet.Show();
                MenuController.Start();
            }

            else if (input.ToLower() == "q")
            {
                MenuController.Start();
            }

            else
            {
                WrongInput.Show();
                Login(barcode);
            }
        }
    }
}