using System;
using System.Collections.Generic;

public class TestBaseLogic : IBaseLogic
{
    public List<TourModel> GetAllTours()
    {
        return new List<TourModel>
        {
            new TourModel { tourId = 1, dateTime = new DateTime(2023, 6, 1, 10, 0, 0) },
            new TourModel { tourId = 2, dateTime = new DateTime(2023, 6, 2, 14, 0, 0) }
        };
    }
}
