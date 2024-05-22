using Newtonsoft.Json;

public class BaseAccess
{
    private static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Tours.json"));
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
}