using Newtonsoft.Json;
using System.Text.Json;

public class BaseAccess
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Tours.json"));
    public BaseAccess()
    {
    }

    public List<TourModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<TourModel>>(json);
    }

    // public void WriteAll(List<TourModel> tours)
    // {
    //     string json = File.ReadAllText(path);
    //     List<TourModel> addTourModel = JsonConvert.DeserializeObject<List<TourModel>>(json);
    //     foreach(TourModel tour in tours)
    //     {
    //         addTourModel.Add(tour);
    //     }
    //     File.WriteAllText(path, JsonConvert.SerializeObject(addTourModel));
    // }
    public void WriteAll(List<TourModel> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = System.Text.Json.JsonSerializer.Serialize(items, options);
        File.WriteAllText(path, json);
    }
}