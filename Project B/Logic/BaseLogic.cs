using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class BaseLogic
{

    private static string filepath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Tours.json"));

    protected static List<TourModel> _items;
    public List<TourModel> GetAllTours() => _items;

    public BaseLogic()
    {
        _items = BaseAccess.LoadAll();
    }

    public static void AddVisitorsToTour(Visitor visitor)
    {
        BaseLogic logic = new();
        List<TourModel> items = logic.GetAllTours();
        foreach (TourModel item in items)
        {
            if (item.tourId == visitor.tourNumber)
            {
                item.tourVisitorList.Add(visitor);
                item.parttakers = item.tourVisitorList.Count();
                BaseAccess.WriteAll(items);
            }
        }
    }


    public static void RemoveVisitorsFromTour(Visitor visitor)
    {
        BaseLogic logic = new BaseLogic();
        List<TourModel> items = logic.GetAllTours();

        List<Visitor> visitorsToRemove = new List<Visitor>();

        foreach (TourModel item in items)
            if (item.tourId == visitor.tourNumber)
                foreach (Visitor existingVisitor in item.tourVisitorList)
                    if (existingVisitor.barcode == visitor.barcode)
                        visitorsToRemove.Add(existingVisitor);

        foreach (TourModel item in items)
        {
            item.tourVisitorList.RemoveAll(visitorsToRemove.Contains);
            item.parttakers = item.tourVisitorList.Count();
        }

        BaseAccess.WriteAll(items);
    }

    // public static void AssignGuide()
    // {
    //     GuideModel.
    // }

}