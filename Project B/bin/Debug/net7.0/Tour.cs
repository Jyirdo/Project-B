class Tour
{
    // Get time from file with all the tour times?

    public DateTime tourStartTime;
    public int parttakers;
    public bool opentourspots;

    public Tour(DateTime time)
    {
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
    }

    public void AddVisitor()
    {
        if (opentourspots == true)
        {
            parttakers++;
            MaxVisitor();
        }
        else
        {
            Console.WriteLine("Maximum tourcapacity reached");
        }
    }

    public void RemoveVisitor()
    {
        if (parttakers == 13)
        {
            parttakers--;
            MaxVisitor();
        }
        else
        {
            parttakers--;
            MaxVisitor();
        }
    }

    public void MaxVisitor()
    {
        if (parttakers < 3)
        {
            opentourspots = true;
        }
        else 
        {
            opentourspots = false;
        }
    }
}