namespace ProjectB;

public static class AddRemove
{
    public static void RemoveFromReservations(Visitor visitor, int tourNumber)
    {
        List<TourModel> tours = BaseAccess.LoadTours();

        List<Visitor> visitorsToRemove = new List<Visitor>();

        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                foreach (Visitor existingVisitor in tour.reservationsList)
                if (existingVisitor.barcode == visitor.barcode)
                    visitorsToRemove.Add(existingVisitor);
            }
            tour.reservationsList.RemoveAll(visitorsToRemove.Contains);
            tour.parttakers = tour.reservationsList.Count();
        }

        BaseAccess.WriteAll(tours);
    }

    public static void RemoveFromTourlist(Visitor visitor, int tourNumber)
    {
        List<TourModel> tours = BaseAccess.LoadTours();

        List<Visitor> visitorsToRemove = new List<Visitor>();

        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                foreach (Visitor existingVisitor in tour.visitorList)
                if (existingVisitor.barcode == visitor.barcode)
                    visitorsToRemove.Add(existingVisitor);
            }
            tour.visitorList.RemoveAll(visitorsToRemove.Contains);
            tour.parttakers = tour.visitorList.Count();
        }

        BaseAccess.WriteAll(tours);
    }

    public static void AddToReservations(Visitor visitor, int tourNumber)
    {
        List<TourModel> tours = BaseAccess.LoadTours();

        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                tour.reservationsList.Add(visitor);
                tour.parttakers = tour.reservationsList.Count();
                BaseAccess.WriteAll(tours);
            }
        }
    }

    public static void AddToTourlist(Visitor visitor, int tourNumber)
    {
        List<TourModel> tours = BaseAccess.LoadTours();

        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourNumber)
            {
                tour.visitorList.Add(visitor);
                tour.parttakers = tour.visitorList.Count();
                BaseAccess.WriteAll(tours);
            }
        }
    }
}