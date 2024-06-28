namespace ProjectB;
using Newtonsoft.Json;

public class GuideModel
{
    [JsonProperty("guide_id")]
    public string GuideId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("roster")]
    public List<(DateTime, DateTime)> Roster { get; set; }
    // Tuple item1 is starttime and item2 is endtime

    [JsonProperty("tours_assigned")]
    public List<TourModel> toursAssigned { get; set; }

    public GuideModel(string guideid, string name, List<(DateTime, DateTime)> roster, List<TourModel> toursassigned)
    {
        GuideId = guideid;
        Name = name;
        Roster = roster;
        toursAssigned = toursassigned;
    }

}