namespace ProjectB;

public static class MenuController
{
    public static void Start()
    {
        while (true)
        {
            Tour.CheckTours();
            string input = Menu.Show().Trim();

            Staff staff = new Staff(input);
            Visitor visitor = new Visitor(input);

            if (staff.CorrectStaffCode())
            {
                StaffController.Login(input);
                break;
            }
            if (visitor.CorrectVisitorCode())
            {
                VisitorController.Login(input);
                break;
            }
            else if (input.ToLower() == "h")
            {
                SoundAccess.PlayHelp();
                Help.Show();
            }
            else
            {
                WrongInput.Show();
                continue;
            }
        }
    }
}