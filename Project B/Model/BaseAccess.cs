namespace ProjectB;
using Newtonsoft.Json;

public static class BaseAccess
{
    public static List<Tour> LoadTours()
    {
        if (File.Exists("Data/Tours.json"))
        {
            string json = Program.World.ReadAllText("Data/Tours.json");
            return JsonConvert.DeserializeObject<List<Tour>>(json);
        }
        else return null;
    }

    public static void WriteAll(List<Tour> tour)
    {
        string jsonString = JsonConvert.SerializeObject(tour, Formatting.Indented);
        using (StreamWriter writer = new("Data/Tours.json"))
        {
            writer.WriteLine(jsonString);
        }
    }

    public static List<string[]> LoadAllCSV()
    {
        string[] lines = File.ReadAllLines("Data/parttakers.csv");
        List<string[]> csvData = new List<string[]>();

        foreach (string line in lines)
        {
            string[] values = line.Split(';');
            csvData.Add(values);
        }
        return csvData;
    }

    public static List<string> loadAllVisitorCodes()
    {
        List<string> visitorCodes = new();
        List<string> exceptionCodes = new();

        try
        {
            StreamReader reader = new StreamReader("Data/visitor_codes.txt");
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    visitorCodes.Add(line);
                }
            }
            return visitorCodes;
        }
        catch (Exception ex)
        {
            exceptionCodes.Add(ex.Message);
            return exceptionCodes;
        }
    }

    public static bool GetVisitorInTour(int selectedTour, string barcode)
    {
        if (File.Exists("Data/Tours.json"))
        {
            string json = Program.World.ReadAllText("Data/Tours.json");
            var convertedJson = JsonConvert.DeserializeObject<List<Tour>>(json);
            foreach (Tour model in convertedJson)
            {
                if (model.tourId == selectedTour)
                    foreach (Visitor visitor in model.reservationsList)
                        if (visitor.Barcode == barcode)
                            return true;
            }
        }
        return false;
    }

    public static void WriteAdvice(Dictionary<string, int> timeDictLarge, Dictionary<string, int> timeDictSmall)
    {
        using (StreamWriter writer = new StreamWriter("Data/Advise.txt", false))
        {
            foreach (KeyValuePair<string, int> kvp in timeDictLarge)
            {
                if (kvp.Value >= 9)
                {
                    string message = $"Er wordt aangeraden om extra rondleidingen te geven op {kvp.Key}.";
                    writer.WriteLine(message);
                }
            }
            writer.WriteLine("-------------------------------------------------------------------------");

            foreach (KeyValuePair<string, int> kvp in timeDictSmall)
            {
                if (kvp.Value <= 5)
                {
                    string message = $"Er wordt aangeraden om minder rondleidingen te geven op {kvp.Key}.";
                    writer.WriteLine(message);
                }
            }

            writer.WriteLine("\nAdvise created on:");
            writer.WriteLine($"{DateTime.Now.ToString("dd-MM-yyyy")}");
        }
    }

    public static List<string> loadAllGuideInfo()
    {
        List<string> visitorCodes = new();
        List<string> exceptionCodes = new();

        try
        {
            StreamReader reader = new StreamReader("Data/GuideInfo.txt");
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    visitorCodes.Add(line);
                }
            }
            return visitorCodes;
        }
        catch (Exception ex)
        {
            exceptionCodes.Add(ex.Message);
            return exceptionCodes;
        }
    }
}

