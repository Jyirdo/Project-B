class Visitor
{
    public int ticketID;
    public Tour? visitorTour;
    public DateTime tourTime;

    public Visitor(int ticketid, DateTime tourtime)
    {
        ticketID = ticketid;
        tourTime = tourtime;
    }

    public void CreateTour()
    {
        visitorTour = new(tourTime);
    }

}

