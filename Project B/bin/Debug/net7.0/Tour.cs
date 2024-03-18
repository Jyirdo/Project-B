class Tour
{
    // Get time from file with all the tour times?

    public DateTime tourStartTime;
    public int parttakers;

    public Tour(DateTime time)
    {
        tourStartTime = time;
        parttakers = 0;
    }

    public void AddVisitor()
    {
        parttakers++;
    }

    public void RemoveVisitor()
    {
        parttakers--;
    }


}