class Visitor
{
    public long ticketID;
    public Tour? visitorTour;
    public DateTime tourTime;

    public Visitor(long ticketid, DateTime tourtime)
    {
        ticketID = ticketid;
        tourTime = tourtime;
    }

    public void CreateTour()
    {
        visitorTour = new(tourTime);
    }

}

