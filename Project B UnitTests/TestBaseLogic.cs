// using System;
// using System.Collections.Generic;
// using Project.Logic;
// using Project.Models;

// namespace Project.Tests
// {
//     public class TestBaseLogic : IBaseLogic
//     {
//         public List<TourModel> GetAllTours()
//         {
//             return new List<TourModel>
//             {
//                 new TourModel
//                 {
//                     tourId = 1,
//                     dateTime = new DateTime(2023, 6, 1, 10, 0, 0),
//                     tourVisitorList = new List<Visitor>
//                     {
//                         new Visitor(1234567890),
//                         new Visitor(9876543210)
//                     }
//                 },
//                 new TourModel
//                 {
//                     tourId = 2,
//                     dateTime = new DateTime(2023, 6, 2, 14, 0, 0),
//                     tourVisitorList = new List<Visitor>
//                     {
//                         new Visitor(5555555555)
//                     }
//                 }
//             };
//         }
//     }
// }
