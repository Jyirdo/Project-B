static class Add_Remove
{
    static private BaseLogic baseLogic = new BaseLogic();
    private static List<Visitor> fakeTourVisitorList = new List<Visitor>();

    public static void Remove()
    {
        Console.WriteLine("Enter the id of the Tour you want to edit");
        int id = Convert.ToInt32(Console.ReadLine());
        TourModel tour = baseLogic.GetById(id);
        if (tour != null)
        {
            // Just a quick example on a few things to update
            // Properly code this in the project
            Console.WriteLine("We are editing the Tour with the following visitors:");
            foreach (Visitor visitor in tour.tourVisitorList)
            {
                Console.WriteLine(visitor.barcode);
                fakeTourVisitorList.Add(visitor);
            }

            Console.WriteLine("Type de barcode van de bezoeker die u wilt verwijderen");
            long barcode = Convert.ToInt64(Console.ReadLine());
            foreach (Visitor visitor in fakeTourVisitorList)
            {
                if (visitor.barcode == barcode)
                {
                    tour.tourVisitorList.Remove(visitor);
                }
            }
            baseLogic.UpdateList(tour);
            Console.WriteLine("tour updated");
        }
        else
        {
            Console.WriteLine("No tour found with that id");
        }

    }
}