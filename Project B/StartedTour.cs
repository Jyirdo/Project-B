using Newtonsoft.Json;
public class StartedTour
{
    [JsonProperty("date_time")]
    public string date_time { get; set; }

    [JsonProperty("presence")]
    public int presence { get; set; }

}