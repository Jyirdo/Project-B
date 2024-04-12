using Newtonsoft.Json;

public static class Advise
{
    private static List<Dictionary<DateTime, int>> presenceList = new List<Dictionary<DateTime, int>>();
    private static void GetPresenceData()
    {
        string json = File.ReadAllText("../../../started_tours.json");
        List<PresenceData> dataList = JsonConvert.DeserializeObject<List<PresenceData>>(json);

        foreach (var data in dataList)
        {
            var dict = new Dictionary<DateTime, int>();
            dict.Add(Convert.ToDateTime(data.DateTime), data.Presence);
            presenceList.Add(dict);
        }

    }


    public static void CreateAdvise()
    {
        List<string> timeListLarge = new List<string>();
        List<string> timeListSmall = new List<string>();
        GetPresenceData();

        // Add day(s) and time(s) where there are more then 10 people or less then 5 to list.
        foreach (Dictionary<DateTime, int> item in presenceList)
        {
            foreach (KeyValuePair<DateTime, int> kvp in item)
            {
                if (kvp.Value > 10)
                {
                    timeListLarge.Add(kvp.Key.ToString($"dddd 'om' HH:mm"));
                }
                else if (kvp.Value < 5)
                {
                    timeListSmall.Add(kvp.Key.ToString($"dddd 'om' HH:mm"));
                }
            }
        }

        Dictionary<string, int> timeDictLarge = new Dictionary<string, int>();
        Dictionary<string, int> timeDictSmall = new Dictionary<string, int>();

        // Add to a dict the day and time and the amount of times that day and time has more then 10 visitors or less then 5.
        foreach (string time in timeListLarge.Distinct().ToList())
        {
            int count = timeListLarge.Count(item => item == time);
            timeDictLarge.Add(time, count);
        }

        timeListLarge.Clear();

        foreach (string time in timeListSmall.Distinct().ToList())
        {
            int count = timeListSmall.Count(item => item == time);
            timeDictSmall.Add(time, count);
        }

        timeListSmall.Clear();

        // If the amount of times a certain day and time has too little or too much visitors is greater than 4, suggest extra or less tours.
        using (StreamWriter writer = new StreamWriter("../../../Advise.txt", false))
        {
            foreach (KeyValuePair<string, int> kvp in timeDictLarge)
            {
                if (kvp.Value > 4)
                {
                    Console.WriteLine($"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.");
                    writer.WriteLine($"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.");
                }
            }

            Console.WriteLine("-------------------------------------------------------------------------");
            writer.WriteLine("-------------------------------------------------------------------------");

            foreach (KeyValuePair<string, int> kvp in timeDictSmall)
            {
                if (kvp.Value > 4)
                {
                    Console.WriteLine($"Er wordt aangeraden om minder rondleidingen te geven op {kvp.Key}.");
                    writer.WriteLine($"Er wordt aangeraden om minder rondleidingen te geven op {kvp.Key}.");
                }
            }
        }
    }
}