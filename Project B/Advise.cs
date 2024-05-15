// using Newtonsoft.Json;

<<<<<<< HEAD
// public static class Advise
// {
//     private static List<StartedTour> dataList;
//     static string latestStartedTour = "1-1-1970";
//     private static string GetPresenceData()
//     {
//         if (File.Exists("started_tours.json"))
//         {
//             // Use only information from the past month of tours, to keep information up to date.
//             KeepStartedToursUpToDate();

//             string json = File.ReadAllText("started_tours.json");
//             dataList = JsonConvert.DeserializeObject<List<StartedTour>>(json);
=======
public static class Advise
{
    private static List<StartedTour> dataList;
    static string latestStartedTour = "1-1-1970";
    private static string GetPresenceData()
    {
        if (File.Exists("started_tours.json"))
        {
            // Use only information from the past month of tours, to keep information up to date.
            KeepStartedToursUpToDate();

            string json = File.ReadAllText("started_tours.json");
            dataList = JsonConvert.DeserializeObject<List<StartedTour>>(json);
>>>>>>> main

//             if (dataList == null)
//             {
//                 Console.WriteLine("The file 'started_tours.json' is empty.");
//                 return null;
//             }

//             return "";
//         }

<<<<<<< HEAD
//         else
//         {
//             Console.WriteLine("The file 'started_tours.json' could not be found.");
//             return null;
//         }
//     }
//     private static void KeepStartedToursUpToDate()
//     {
//         string json = File.ReadAllText("started_tours.json");
//         List<StartedTour> tempDataList = JsonConvert.DeserializeObject<List<StartedTour>>(json);
=======
        else
        {
            Console.WriteLine("The file 'started_tours.json' could not be found.");
            return null;
        }
    }
    private static void KeepStartedToursUpToDate()
    {
        string json = File.ReadAllText("started_tours.json");
        List<StartedTour> tempDataList = JsonConvert.DeserializeObject<List<StartedTour>>(json);
>>>>>>> main

//         foreach (var data in tempDataList)
//             if (DateTime.Parse(data.date_time) > DateTime.Parse(latestStartedTour))
//                 latestStartedTour = data.date_time;

<<<<<<< HEAD
//         List<StartedTour> startedList = JsonConvert.DeserializeObject<List<StartedTour>>(File.ReadAllText("started_tours.json"));
//         startedList = startedList.Where(start => DateTime.Parse(start.date_time) > DateTime.Parse(latestStartedTour).AddDays(-30)).ToList();
//         string updatedStartedTours = JsonConvert.SerializeObject(startedList, Formatting.Indented);
//         File.WriteAllText("started_tours.json", updatedStartedTours);
//     }
=======
        List<StartedTour> startedList = JsonConvert.DeserializeObject<List<StartedTour>>(File.ReadAllText("started_tours.json"));
        startedList = startedList.Where(start => DateTime.Parse(start.date_time) > DateTime.Parse(latestStartedTour).AddDays(-30)).ToList();
        string updatedStartedTours = JsonConvert.SerializeObject(startedList, Formatting.Indented);
        File.WriteAllText("started_tours.json", updatedStartedTours);
    }
>>>>>>> main

//     public static void CreateAdvise()
//     {
//         List<string> timeListLarge = new List<string>();
//         List<string> timeListSmall = new List<string>();
//         string present = GetPresenceData();
//         if (present == null)
//             return;

//         // Add day(s) and time(s) where there are more then 10 people or less then 5 to list.
//         // This program checks by day (e.g. "Thursday") and not by date.
//         foreach (StartedTour item in dataList)
//         {
//             if (item.presence > 10)
//                 timeListLarge.Add(DateTime.Parse(item.date_time).ToString($"dddd 'om' HH:mm"));

//             else if (item.presence < 5)
//                 timeListSmall.Add(DateTime.Parse(item.date_time).ToString($"dddd 'om' HH:mm"));
//         }

//         Dictionary<string, int> timeDictLarge = new Dictionary<string, int>();
//         Dictionary<string, int> timeDictSmall = new Dictionary<string, int>();

//         // Add to a dict the day and time and the amount of times that day and time has more then 10 visitors or less then 5.
//         foreach (string time in timeListLarge.Distinct().ToList())
//         {
//             int count = timeListLarge.Count(item => item == time);
//             timeDictLarge.Add(time, count);
//         }

//         foreach (string time in timeListSmall.Distinct().ToList())
//         {
//             int count = timeListSmall.Count(item => item == time);
//             timeDictSmall.Add(time, count);
//         }

<<<<<<< HEAD
//         // If the amount of times a certain day and time has too little or too much visitors exceeds twice in a month, suggest extra tour.
//         using (StreamWriter writer = new StreamWriter("Advise.txt", false))
//         {
//             foreach (KeyValuePair<string, int> kvp in timeDictLarge)
//             {
//                 if (kvp.Value >= 2)
//                 {
//                     Console.WriteLine($"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.");
//                     writer.WriteLine($"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.");
//                 }
//             }
=======
        // If the amount of times a certain day and time has too little or too much visitors exceeds twice in a month, suggest extra tour.
        using (StreamWriter writer = new StreamWriter("Advise.txt", false))
        {
            foreach (KeyValuePair<string, int> kvp in timeDictLarge)
            {
                if (kvp.Value >= 2)
                {
                    Console.WriteLine($"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.");
                    writer.WriteLine($"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.");
                }
            }
>>>>>>> main

//             Console.WriteLine("-------------------------------------------------------------------------");
//             writer.WriteLine("-------------------------------------------------------------------------");

//             foreach (KeyValuePair<string, int> kvp in timeDictSmall)
//             {
//                 if (kvp.Value >= 2)
//                 {
//                     Console.WriteLine($"Er wordt aangeraden om minder rondleidingen te geven op {kvp.Key}.");
//                     writer.WriteLine($"Er wordt aangeraden om minder rondleidingen te geven op {kvp.Key}.");
//                 }
//             }

//             writer.WriteLine("\nAdvise created on:");
//             writer.WriteLine($"{DateTime.Now.ToString("dd-MM-yyyy")}");
//         }



//     }
// }