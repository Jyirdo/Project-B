using Newtonsoft.Json;

class Tour
{
    public void AddVisitorsToTour(bool add, Visitor tourvisitor)
    {
        // Removes visitors if false, adds visitor if true.

        if (add == false)
        {
            if (parttakers > 0)
            {
                parttakers--;
                tourVisitorList.Remove(tourvisitor);
            }
        }
        else if (add == true)
        {
            if (parttakers < limit)
            {
                parttakers++;
                tourVisitorList.Add(tourvisitor);
            }
        }

    }

    private void CreateTourJson()
    {
        if (File.Exists($"Tour_{tour_id}"))
        {

        }
        else
        {
            File.Create($"Tour_{tour_id}");

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

    [JsonProperty("tour_id")]
    public int tour_id;

    [JsonProperty("tourStartTime")]
    public DateTime tourStartTime;

    [JsonProperty("parttakers")]
    public int parttakers = 0;

    [JsonProperty("opentourspots")]
    public bool opentourspots = true;

    [JsonProperty("limit")]
    public int limit = 13;

    [JsonProperty("tourVisitorList")]
    public List<Visitor> tourVisitorList = new();


    public Tour(int id, DateTime time)
    {
        tour_id = id;
        tourStartTime = time;

    }
}