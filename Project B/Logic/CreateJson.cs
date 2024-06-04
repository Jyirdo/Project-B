using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public static class CreateJson
{
    public static string CheckTours()
    {
        List<TourModel> jsonList = BaseAccess.LoadAll();

        List<string> tourTimes = new List<string>
        {
            "10:20", "10:40", "11:00", "11:20", "12:00", "12:20", "12:40",
            "13:00", "13:20", "13:40", "14:00", "14:20", "14:40", "15:00",
            "15:20", "15:40", "16:00"
        };

        DateTime currentDate = DateTime.Now.Date;
        bool isNewDay = false;

        foreach (TourModel tour in jsonList)
        {
            if (tour.dateTime.Date < currentDate)
            {
                isNewDay = true;
                break;
            }
        }

        if (isNewDay)
        {
            try
            {
                string sourceFile = "DataSources/Tours.json";
                if (File.Exists(sourceFile))
                {
                    string destinationDirectory = "DataSources/PreviousJsons";
                    string destinationFileName = $"Tours-on-{jsonList[0].dateTime:yyyy-MM-dd}.json";
                    string destinationFile = Path.Combine(destinationDirectory, destinationFileName);

                    if (!Directory.Exists(destinationDirectory))
                        Directory.CreateDirectory(destinationDirectory);

                    File.Move(sourceFile, destinationFile);

                    // Create new tours for the current day
                    List<TourModel> newTourList = new List<TourModel>();
                    int tourNumber = 1;
                    foreach (string time in tourTimes)
                    {
                        DateTime tourDateTime = DateTime.ParseExact(currentDate.ToString("yyyy-MM-dd") + " " + time, "yyyy-MM-dd HH:mm", null);
                        TourModel newTour = new TourModel(tourNumber, tourDateTime);
                        newTourList.Add(newTour);
                        tourNumber++;
                    }

                    BaseAccess.WriteAll(newTourList);

                    return "New file successfully created";
                }
                else
                {
                    return "File not found!";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        return "No new day detected or something went wrong";
    }
}
