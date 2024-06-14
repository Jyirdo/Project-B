namespace ProjectB;

public static class Add_Remove
{
    public static void Remove(Visitor visitor, int tourNumber)
    {
        BaseLogic logic = new BaseLogic();
        List<TourModel> tours = logic.GetAllTours();

        List<Visitor> visitorsToRemove = new List<Visitor>();

        foreach (TourModel tour in tours)
            if (tour.tourId == tourNumber)
            {
                foreach (Visitor existingVisitor in tour.tourVisitorList)
                if (existingVisitor.barcode == visitor.barcode)
                    visitorsToRemove.Add(existingVisitor);
            }

        foreach (TourModel tour in tours)
        {
            tour.tourVisitorList.RemoveAll(visitorsToRemove.Contains);
            tour.parttakers = tour.tourVisitorList.Count();
        }

        BaseAccess.WriteAll(tours);
    }

    public static void Add(Visitor visitor, int tourNumber)
    {
        BaseLogic logic = new();
        List<TourModel> items = logic.GetAllTours();
        foreach (TourModel item in items)
        {
            if (item.tourId == tourNumber)
            {
                item.tourVisitorList.Add(visitor);
                item.parttakers = item.tourVisitorList.Count();
                BaseAccess.WriteAll(items);
            }
        }
    }
}