using System.Text.Json;
public class BaseLogic
{

    private static string filepath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Tours.json"));
    protected BaseAccess AccessLayer = new BaseAccess();

    protected static List<TourModel> _items;
    public List<TourModel> GetAllTours() => _items;

    public BaseLogic()
    {
        _items = AccessLayer.LoadAll();
    }

    public void UpdateList(TourModel item)
    {
        //Find if there is already an model with the same id
        int index = _items.FindIndex(s => (s as TourWithID).tourId == (item as TourWithID).tourId);

        if (index != -1)
        {
            //update existing model
            _items[index] = item;
        }
        else
        {
            //add new model
            _items.Add(item);
        }
        AccessLayer.WriteAll(_items);
    }

    public void Delete(int id)
    {
        _items.RemoveAll(i => (i as TourWithID).tourId == id);
        AccessLayer.WriteAll(_items);
    }

    public void Delete(TourModel item)
    {
        _items.Remove(item);
        AccessLayer.WriteAll(_items);
    }

    public TourModel GetById(int id)
    {
        return _items.Find(i => (i as TourWithID).tourId == id);
    }

    // public static void AddVisitorsToTourJson(Visitor visitor, int tourId)
    // {
    //     JArray existingArray = File.Exists(filepath) ? JArray.Parse(File.ReadAllText(filepath)) : new JArray();
    //     var tourToModify = existingArray.FirstOrDefault(obj => obj["tour_id"].Value<int>() == tourId);
    //     if (tourToModify != null)
    //     {
    //         JObject visitorObject = JObject.Parse(JsonConvert.SerializeObject(visitor));
    //         JArray visitorList = (JArray)tourToModify["tourVisitorList"];
    //         visitorList.Add(visitorObject);
    //         tourToModify["parttakers"] = visitorList.Count;
    //         File.WriteAllText(filepath, existingArray.ToString(Formatting.Indented));
    //     }
    //     else
    //         return;
    // }

    // public static void RemoveVisitorsFromTourJson(Visitor visitor)
    // {
    //     JArray existingArray = File.Exists(filepath) ? JArray.Parse(File.ReadAllText(filepath)) : new JArray();
    //     var tourToModify = existingArray.FirstOrDefault(obj => obj["tour_id"].Value<string>() == Convert.ToString(Convert.ToInt32(visitor.barcode)));
    //     if (tourToModify != null)
    //     {
    //         JArray visitorList = (JArray)tourToModify["tourVisitorList"];
    //         var visitorToRemove = visitorList.FirstOrDefault(obj => obj["visitor_id"].Value<string>() == Convert.ToString(Convert.ToInt32(visitor.barcode)));
    //         if (visitorToRemove != null)
    //         {
    //             visitorList.Remove(visitorToRemove);
    //             tourToModify["parttakers"] = visitorList.Count;
    //             File.WriteAllText(filepath, existingArray.ToString(Formatting.Indented));
    //         }
    //         else
    //             return;
    //     }
    //     else
    //         return;
    // }
}