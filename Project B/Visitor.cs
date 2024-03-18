class Visitor
{
    public int ticketID;
    public Tour? visitorTour;

    public Visitor(int ticketid)
    {
        ticketID = ticketid;
    }

    public void CreateTour()
    {
        visitorTour = new(ticketID);
    }



}

