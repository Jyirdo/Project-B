using Newtonsoft.Json;
public class Reservation
{
    [JsonProperty("reservation_id")]
    public string ReservationId { get; set; }

    [JsonProperty("date_time")]
    public string DateTime { get; set; }

    [JsonProperty("tour_number")]
    public string TourNumber { get; set; }
}
