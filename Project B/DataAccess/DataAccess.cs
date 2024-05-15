using Newtonsoft.Json;

public class BaseAccess
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Tours.json"));
    public BaseAccess()
    {
    }

    public List<TourModel> loadAll()
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<TourModel>>(json);
    }

    public void writeAll(TourModel tour)
    {
        string json = File.ReadAllText(path);
        List<TourModel> addTourModel = JsonConvert.DeserializeObject<List<TourModel>>(json);
        addTourModel.Add(tour);
        File.WriteAllText(path, JsonConvert.SerializeObject(addTourModel));
    }
}