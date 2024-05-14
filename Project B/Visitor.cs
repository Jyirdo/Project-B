public class Visitor
{
    public long barcode;
    public DateTime tourTime;
    public int tourNumber;

    public Visitor(long barcode1, DateTime tourtime, int tournumber)
    {
        barcode = barcode1;
        tourTime = tourtime;
        tourNumber = tournumber;
    }
}