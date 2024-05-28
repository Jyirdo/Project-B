public class SelectTour
{
    public static string SelectATour(long barcode)
    {
        Tour.Load_Tours();
        return Tour.ChooseTour(barcode);
    }
}