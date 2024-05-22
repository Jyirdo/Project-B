// BaseAccess baseaccess = new();
// List<TourModel> list = baseaccess.loadAll();

// foreach (TourModel model in list)
// {
//     if (model.tourId == 2)
//     {
//         Console.WriteLine(model.tourVisitorList[0].tourTime);
//         break;
//     }
// }

Visitor visitor = new(12345, DateTime.Parse("2024-04-10T11:00:00"), 3);
Visitor visitor2 = new(789, DateTime.Parse("2024-04-10T11:00:00"), 4);
// BaseLogic.AddVisitorsToTour(visitor2);
// BaseLogic.RemoveVisitorsFromTour(visitor);
new TourModel() { };
List<GuideModel> guides = new() { new GuideModel("123", "Jan", new List<(DateTime, DateTime)>() { (DateTime.Now, DateTime.UtcNow), (DateTime.Parse("11-2-2024 13:13"), DateTime.Parse("11-2-2024 14:13")), new List<TourModel>() { } }) };
BaseAccess.WriteGuides(guides);




// Menu.ShowNotRegisteredMenu();
