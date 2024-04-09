class Tour
{
    // List of all visitors in this tour
    public List<Visitor> visitorsintour = new();
    public int tour_id;
    public DateTime tourStartTime;

    public Tour(int id, DateTime time)
    {
        tour_id = id;
        tourStartTime = time;
    }
}