class ClientID
{
    public int ticketID;
    public Tour? thisClientTour;

    public ClientID(int ticketid)
    {
        ticketID = ticketid;
    }

    public void CreateTour()
    {
        thisClientTour = new(ticketID);
    }



}

