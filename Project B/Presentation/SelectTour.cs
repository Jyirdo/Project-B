public class SelectTour
{
    public static void Select_A_Tour(long barcode)
    {
        Tour.Load_Tours();
        Tour.Choose_Tour(barcode);
        Menu.MainMenu();
    }
}