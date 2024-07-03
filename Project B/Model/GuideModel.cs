namespace ProjectB;
using Newtonsoft.Json;

public class GuideModel
{
    [JsonProperty("guide_id")]
    public string GuideId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    public GuideModel(string guideid, string name)
    {
        GuideId = guideid;
        Name = name;
    }
}