namespace ProjectB;
using Newtonsoft.Json;

public class GuideModel
{
    [JsonProperty("guide_id")]
    public string GuideId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("shift")]
    public Tuple<DateTime, DateTime> Shift { get; set; }
    // Tuple item1 is starttime and item2 is endtime

    public GuideModel(string guideid, string name, Tuple<DateTime, DateTime> shift)
    {
        GuideId = guideid;
        Name = name;
        Shift = shift;
    }

}