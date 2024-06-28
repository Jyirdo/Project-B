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
                    string message = Tour.CancelReservation(visitor);
                    VisitorCancelReservation.Show(message);
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
        // visitor already participated with a tour
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
                string message = Tour.MakeReservation(visitor, tourID);
                VisitorMakeReservationSucces.Show(message);
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