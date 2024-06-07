using Newtonsoft.Json;

public class BaseAccess
{
    public static List<TourModel> LoadAll()
    {
        if (File.Exists("DataSources/Tours.json"))
        {
            string json = File.ReadAllText("DataSources/Tours.json");
            return JsonConvert.DeserializeObject<List<TourModel>>(json);
        }
        else return null;
    }

    public static void WriteAll(List<TourModel> tour)
    {
        string jsonString = JsonConvert.SerializeObject(tour, Formatting.Indented);
        using (StreamWriter writer = new("DataSources/Tours.json"))
        {
            writer.WriteLine(jsonString);
        }
    }
    public static List<string[]> LoadAllCSV(string filepath)
    {
        string[] lines = File.ReadAllLines(filepath);
        List<string[]> csvData = new List<string[]>();

        foreach (string line in lines)
        {
            string[] values = line.Split(';');
            csvData.Add(values);
        }
        return csvData;
    }

    public static List<string> loadAllStaffCodes()
    {
        List<string> staffCodes = new();

        using (StreamReader reader = new StreamReader("DataSources/staff_codes.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                staffCodes.Add(line);
            }
        }

        return staffCodes;
    }

    public static List<string> loadAllVisitorCodes()
    {
        List<string> visitorCodes = new();

        if (!File.Exists("DataSources/visitor_codes.txt"))
            Console.WriteLine("visitor_codes.txt ontbreekt. Graag assistentie zoeken.");
        else
        {
            using (StreamReader reader = new StreamReader("DataSources/visitor_codes.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    visitorCodes.Add(line);
                }
            }
        }

        return visitorCodes;
    }

    public static bool GetVisitorInTour(int selectedTour, string barcode)
    {
        if (File.Exists("DataSources/Tours.json"))
        {
            string json = File.ReadAllText("DataSources/Tours.json");
            var convertedJson = JsonConvert.DeserializeObject<List<TourModel>>(json);
            foreach (TourModel model in convertedJson)
            {
                if (model.tourId == selectedTour)
                    foreach (Visitor visitor in model.tourVisitorList)
                        if (visitor.barcode == barcode)
                            return true;
            }
        }
        return false;
    }
}

