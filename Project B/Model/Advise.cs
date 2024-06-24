namespace ProjectB;

public static class Advise
{
    public static void CreateAdvise()
    {
        Dictionary<string, int> timeDictLarge = new();
        Dictionary<string, int> timeDictSmall = new();
        List<string[]> present = BaseAccess.LoadAllCSV();

        if (present == null || present.Count == 0)
        {
            MissingFile.ShowCSV();
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
        BaseAccess.WriteAdvice(timeDictLarge, timeDictSmall);
    }
}
