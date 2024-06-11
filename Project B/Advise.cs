using Newtonsoft.Json;

public static class Advise
{
    public static void CreateAdvise()
    {
        List<string[]> present = BaseAccess.LoadAllCSV("DataSources/parttakers.csv");
        if (present == null || present.Count == 0)
        {
            Console.WriteLine("CSV niet gevonden.");
            return;
        }

        string[] headers = present[0];
        Dictionary<string, List<int>[]> parttakers = new();

        foreach (string[] row in present.Skip(1)) // Skip header
        {
            if (row.Length == 0) continue;

            string dateStr = row[0];

            if (DateTime.TryParse(dateStr, out DateTime date))
            {
                string dayOfWeek = date.DayOfWeek.ToString();

                if (!parttakers.ContainsKey(dayOfWeek))
                {
                    parttakers[dayOfWeek] = new List<int>[headers.Length - 1];
                    for (int i = 0; i < headers.Length - 1; i++)
                    {
                        parttakers[dayOfWeek][i] = new List<int>();
                    }
                }

                for (int i = 1; i < row.Length && i < headers.Length; i++)
                {
                    if (int.TryParse(row[i], out int participants))
                    {
                        parttakers[dayOfWeek][i - 1].Add(participants);
                    }
                }
            }
        }

        Dictionary<string, int> timeDictLarge = new();
        Dictionary<string, int> timeDictSmall = new();

        foreach (var day in parttakers)
        {
            for (int i = 0; i < day.Value.Length; i++)
            {
                List<int> participantsList = day.Value[i];
                participantsList.Sort();
                int count = participantsList.Count;
                int median = count % 2 == 0
                    ? (participantsList[count / 2 - 1] + participantsList[count / 2]) / 2
                    : participantsList[count / 2];

                string timeKey = $"{day.Key} {headers[i + 1]}";
                if (median < 5)
                {
                    if (!timeDictSmall.ContainsKey(timeKey))
                        timeDictSmall[timeKey] = 0;
                    timeDictSmall[timeKey]++;
                }
                else if (median > 10)
                {
                    if (!timeDictLarge.ContainsKey(timeKey))
                        timeDictLarge[timeKey] = 0;
                    timeDictLarge[timeKey]++;
                }
            }
        }

        using (StreamWriter writer = new StreamWriter("DataSources/Advise.txt", false))
        {
            foreach (KeyValuePair<string, int> kvp in timeDictLarge)
            {
                if (kvp.Value >= 9)
                {
                    string message = $"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.";
                    Console.WriteLine(message);
                    writer.WriteLine(message);
                }
            }

            Console.WriteLine("-------------------------------------------------------------------------");
            writer.WriteLine("-------------------------------------------------------------------------");

            foreach (KeyValuePair<string, int> kvp in timeDictSmall)
            {
                if (kvp.Value <= 5)
                {
                    string message = $"Er wordt aangeraden om minder rondleidingen te geven op {kvp.Key}.";
                    Console.WriteLine(message);
                    writer.WriteLine(message);
                }
            }

            writer.WriteLine("\nAdvise created on:");
            writer.WriteLine($"{DateTime.Now.ToString("dd-MM-yyyy")}");
            Console.WriteLine("Alleen een streep? Dan is er momenteel geen advies\n");
        }
    }
}
