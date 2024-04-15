using Newtonsoft.Json;
public class PresenceData
{
    [JsonProperty("date_time")]
    public string DateTime { get; set; }

    [JsonProperty("presence")]
    public int Presence { get; set; }
}