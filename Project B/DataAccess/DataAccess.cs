using Newtonsoft.Json;

public class BaseAccess
{
    public static List<TourModel> LoadAll()
    {
        string json = File.ReadAllText("Tours.json");
        return JsonConvert.DeserializeObject<List<TourModel>>(json);
    }

    public static void WriteAll(List<TourModel> tour)
    {
        string jsonString = JsonConvert.SerializeObject(tour, Formatting.Indented);
        using (StreamWriter writer = new("Tours.json"))
        {
            writer.WriteLine(jsonString);
        }
    }
}