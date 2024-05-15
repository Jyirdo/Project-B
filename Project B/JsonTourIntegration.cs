using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

static class JsonTourIntegration
{
    static string filepath = "../../../StorageFiles/Tours.json";
    public static List<Tour> FromJson()
    {
        List<Tour> deserializedTours = JsonConvert.DeserializeObject<List<Tour>>(File.ReadAllText(filepath));
        return deserializedTours;
    }

    public static void ToJson(Tour tour)
    {
        JArray existingArray = File.Exists(filepath) ? JArray.Parse(File.ReadAllText(filepath)) : new JArray();
        existingArray.Add(JObject.Parse(JsonConvert.SerializeObject(tour)));
        File.WriteAllText(filepath, existingArray.ToString(Formatting.Indented));
    }

    public static void AddVisitorsToTourJson(Visitor visitor, int tourId)
    {
        JArray existingArray = File.Exists(filepath) ? JArray.Parse(File.ReadAllText(filepath)) : new JArray();
        var tourToModify = existingArray.FirstOrDefault(obj => obj["tour_id"].Value<int>() == tourId);
        if (tourToModify != null)
        {
            JObject visitorObject = JObject.Parse(JsonConvert.SerializeObject(visitor));
            JArray visitorList = (JArray)tourToModify["tourVisitorList"];
            visitorList.Add(visitorObject);
            tourToModify["parttakers"] = visitorList.Count;
            File.WriteAllText(filepath, existingArray.ToString(Formatting.Indented));
        }
        else
            Console.WriteLine("Geen rondleiding gevonden.");

    }

    public static void RemoveVisitorsFromTourJson(Visitor visitor)
    {
        JArray existingArray = File.Exists(filepath) ? JArray.Parse(File.ReadAllText(filepath)) : new JArray();
        var tourToModify = existingArray.FirstOrDefault(obj => obj["tour_id"].Value<string>() == Convert.ToString(Convert.ToInt32(visitor.barcode)));
        if (tourToModify != null)
        {
            JArray visitorList = (JArray)tourToModify["tourVisitorList"];
            var visitorToRemove = visitorList.FirstOrDefault(obj => obj["visitor_id"].Value<string>() == Convert.ToString(Convert.ToInt32(visitor.barcode)));
            if (visitorToRemove != null)
            {
                visitorList.Remove(visitorToRemove);
                tourToModify["parttakers"] = visitorList.Count;
                File.WriteAllText(filepath, existingArray.ToString(Formatting.Indented));
            }
            else
                Console.WriteLine("Bezoeker niet gevonden.");
        }
        else
            Console.WriteLine("Geen rondleiding gevonden.");

    }



}