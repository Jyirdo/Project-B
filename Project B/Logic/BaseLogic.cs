using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class BaseLogic
{
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