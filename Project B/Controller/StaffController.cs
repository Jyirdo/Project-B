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
            Staff.SelectTourAndCheckTour(tourID);
        }
        else
        {
            switch (input.ToLower())
            {
                case "t":
                {
                    Staff.AddGuideToTour();
                    SelectionMenu();
                    break;
                }
                case "v":
                {
                    Staff.RemoveGuideFromTour();
                    SelectionMenu();
                    break;
                }
                case "a":
                {
                    Advise.CreateAdvise();
                    AdviseCreated.Show();
                    SelectionMenu();
                    break;
                }
                case "z":
                {
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
}