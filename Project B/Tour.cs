class Tour
{
    // List of all visitors in this tour
    public List<Visitor> visitorsintour = new();
    public int tour_id;
    public DateTime tourStartTime;
    public int parttakers;
    public bool opentourspots;

    public Tour(int id, DateTime time)
    {
        tour_id = id;
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
    }
}