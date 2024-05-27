public class SelectTour
{
    public static string Select_A_Tour(long barcode)
    {
        Tour.Load_Tours();
        return Tour.Choose_Tour(barcode);
    }
}