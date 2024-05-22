using Newtonsoft.Json;

public class BaseAccess
{
    private static string path = "DataSources/Tours.json";
    public BaseAccess()
    {
    }

    public static List<TourModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<TourModel>>(json);
    }

    public static void WriteAll(List<TourModel> tour)
    {
        string jsonString = JsonConvert.SerializeObject(tour, Formatting.Indented);
        using (StreamWriter writer = new(path))
        {
            writer.WriteLine(jsonString);
        }
    }

    public static void WriteGuides(List<GuideModel> guides)
    {
        string jsonString = JsonConvert.SerializeObject(guides, Formatting.Indented);
        using (StreamWriter writer = new("DataSources/Guides.json"))
        {
            writer.WriteLine(jsonString);
        }
    }
}