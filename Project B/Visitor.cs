class Visitor
{
    public long ticketID;
    public DateTime tourTime;
    public int tourNumber;

    public Visitor(long ticketid, DateTime tourtime, int tournumber)
    {
        ticketID = ticketid;
        tourTime = tourtime;
        tourNumber = tournumber;
    }
}