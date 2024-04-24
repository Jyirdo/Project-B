using Newtonsoft.Json;
class Visitor
{
    [JsonProperty("reservation_id")]
    public string ticketID;

    [JsonProperty("date_time")]
    public DateTime tourTime;

    [JsonProperty("tour_number")]
    public int tourNumber;

    public Visitor(string ticketid, DateTime tourtime, int tournumber)
    {
        ticketID = ticketid;
        tourTime = tourtime;
        tourNumber = tournumber;
    }
}