using Newtonsoft.Json;

public class BaseAccess
{
    private string path;
    public BaseAccess(string givenPath)
    {
        path = givenPath;
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