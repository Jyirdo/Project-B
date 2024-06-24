namespace ProjectB;
using Newtonsoft.Json;

public class TourModel
{
    [JsonProperty("tour_id")]
    public int tourId { get; set; }

    [JsonProperty("tourStartTime")]
    public DateTime dateTime { get; set; }

    [JsonProperty("parttakers")]
    public int parttakers { get; set; }

    [JsonProperty("limit")]
    public int limit { get; set; } = 13;

    [JsonProperty("guide")]
    public GuideModel guide { get; set; }

    [JsonProperty("tourVisitorList")]
    public List<Visitor> visitorList { get; set; } = new();

    [JsonProperty("reservationsList")]
    public List<Visitor> reservationsList { get; set; } = new();

    [JsonProperty("tourStarted")]
    public bool tourStarted { get; set; } = false;

    public TourModel(int tourid, DateTime datetime)
    {
        tourId = tourid;
        dateTime = datetime;
    }
}