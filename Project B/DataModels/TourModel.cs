using Newtonsoft.Json;
public class TourModel
{
    [JsonProperty("tourId")]
    public int tourId { get; set; }

    [JsonProperty("tourStartTime")]
    public DateTime dateTime { get; set; }

    [JsonProperty("parttakers")]
    public int parttakers { get; set; }

    [JsonProperty("opentourspots")]
    public bool openTourSpots { get; set; }

    [JsonProperty("limit")]
    public int limit { get; set; }

    [JsonProperty("tourVisitorList")]
    public List<Visitor> tourVisitorList { get; set; }

    public TourModel(int tour_id, DateTime time, int part_takers, bool openSpots, int tour_limit)
    {
        tourId = tour_id;
        dateTime = time;
        parttakers = part_takers;
        openTourSpots = openSpots;
        limit = tour_limit;
    }
}