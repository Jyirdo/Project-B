namespace ProjectB;

public static class StaffController
{
    public static void Login(string staffBarcode)
    {
        string StaffBarcode = staffBarcode;
        string password_input = StaffLogin.Show();
        string password = "hetdepot2024!";

        if (password_input == password)
        {
            SelectionMenu();
            MenuController.Start();
        }
        else
        {
            MenuController.Start();
        }
    }

    public static void SelectionMenu()
    {
        string input = StaffMenu.Show();

        if (int.TryParse(input, out int tourID) && tourID > 0 && tourID < 19)
        {
            TourSelectedMenu(tourID);
        }
        else
        {
            switch (input.ToLower())
            {
                case "a":
                {
                    Advise.CreateAdvise();
                    AdviseCreated.Show();
                    SelectionMenu();
                    break;
                }
                case "z":
                {
                    Environment.Exit(0);
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
                    SelectionMenu();
                    break;
                }
            }
        }
    }

    public static void TourSelectedMenu(int tourID)
    {
        string option = StaffTourSelected.Show(tourID);

        switch (option.ToLower())
        {
            case "c":
            {
                Staff.SelectTourAndCheckTour(tourID);
                break;
            }
            case "q":
            {
                SelectionMenu();
                break;
            }
            default:
            {
                WrongInput.Show();
                TourSelectedMenu(tourID);
                break;
            }
        }
    }
}