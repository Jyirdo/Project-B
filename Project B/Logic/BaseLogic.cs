using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class BaseLogic
{

    private static string filepath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Tours.json"));

    protected static List<TourModel> _items;
    public List<TourModel> GetAllTours() => _items;

    public BaseLogic()
    {
        _items = BaseAccess.LoadAll();
    }
    
    public static void AssignGuide()
    {
        
    }
}