class Tour
{

    public void AddVisitorsToTour(bool add)
    {
        // Removes visitors if false, adds visitor if true.

        if (add == false)
        {
            if (parttakers > 0)
                parttakers--;
        }
        else if (add == true)
        {
            if (parttakers < limit)
                parttakers++;
        }

    }

    public bool CheckTourFullness()
    {
        // Returs true if tour is full and otherwise false.

        if (parttakers == limit)
        {
            return true;
        }
        return false;
    }

    public int tour_id;
    public DateTime tourStartTime;
    public int parttakers;
    public bool opentourspots;
    public int limit;


    public Tour(int id, DateTime time)
    {
        tour_id = id;
        tourStartTime = time;
        parttakers = 0;
        opentourspots = true;
        limit = 13;
    }
}