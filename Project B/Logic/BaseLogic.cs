using System.Collections.Generic;
using Project.Models;

public class BaseLogic : IBaseLogic
{
    protected static List<TourModel> _items;

    public BaseLogic()
    {
        _items = BaseAccess.LoadAll();
    }

    public List<TourModel> GetAllTours()
    {
        return _items;
    }
}
