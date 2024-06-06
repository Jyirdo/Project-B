using Newtonsoft.Json;

public class BaseAccess
{
    public static List<TourModel> LoadAll()
    {
        if (File.Exists("DataSources/Tours.json"))
        {
            string json = File.ReadAllText("DataSources/Tours.json");
            return JsonConvert.DeserializeObject<List<TourModel>>(json);
        }
        else return null;
    }

    public static void WriteAll(List<TourModel> tour)
    {
        string jsonString = JsonConvert.SerializeObject(tour, Formatting.Indented);
        using (StreamWriter writer = new("DataSources/Tours.json"))
        {
            writer.WriteLine(jsonString);
        }
    }
    public static List<string[]> LoadAllCSV(string filepath)
    {
        string[] lines = File.ReadAllLines(filepath);
        List<string[]> csvData = new List<string[]>();

        foreach (string line in lines)
        {
            string[] values = line.Split(';');
            csvData.Add(values);
        }
        return csvData;
    }
}