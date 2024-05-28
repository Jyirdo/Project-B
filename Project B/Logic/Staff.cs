using System.Data;
using Newtonsoft.Json;
// Sometime in the future make these methods static somehow
public class Staff
{
    public static List<string> staffCodes = new List<string>();
    public static List<string> scannedIDS = new();
    private static BaseLogic baseLogic = new BaseLogic();

    public Staff()
    {
    }

    public static bool CorrectStaffCode(string staffcode)
    {
        using (StreamReader reader = new StreamReader("DataSources/staff_codes.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                staffCodes.Add(line);
            }
        }
        if (staffCodes.Contains(staffcode))
        {
            return true;
        }
        return false;
    }

    public static List<string> CheckPresence(List<string> reservations)
    {
        while (true)
        {
            string checkPresence = Console.ReadLine();
            if (checkPresence.ToLower() == "q")
            {
                return null;
            }
            if (checkPresence.ToLower() == "k")
            {
                return reservations;
            }
            else
            {
                if (long.TryParse(checkPresence, out long barcode))
                {
                    if (reservations.Contains(Convert.ToString(barcode)))
                    {
                        scannedIDS.Add(checkPresence);
                        reservations.Remove(Convert.ToString(barcode));
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Dat is geen correcte barcode.");
                    continue;
                }
            }
        }
    }


    public static void SelectTourAndCheckTour(int tourId)
    {
        scannedIDS.Clear();
        List<string> reservationIDS = new List<string>();
        // Controleer of de ingevoerde tour ID geldig is
        List<TourModel> tours = baseLogic.GetAllTours();

        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourId)
            {
                // Hier worden reservation ID's voor de geselecteerde tour geprint
                Console.WriteLine($"Bezoeker barcode(s) voor tour met ID {tourId}:");
                foreach (Visitor visitor in tour.tourVisitorList)
                {
                    Console.WriteLine(visitor.barcode);
                    reservationIDS.Add(Convert.ToString(visitor.barcode));
                }


            CheckThePresence:
                List<string> presenceList = CheckPresence(reservationIDS);

                foreach (string scanned in scannedIDS)
                    presenceList.Remove(scanned);

                if (presenceList == null)
                {
                    return;
                }
                else if (presenceList.Count() == 0)
                {
                    Console.WriteLine("Iedereen is aanwezig.");
                }
                else if (presenceList.Count() > 0)
                {
                    Console.WriteLine("Deze ID's zijn afwezig:\n");
                    foreach (string notpresent in presenceList)
                    {
                        int enumerator = 1;
                        Console.WriteLine($"{enumerator}. ({notpresent})");
                        enumerator++;
                    }
                }

                Console.WriteLine("Druk op 'S' om de tour te starten.");
                Console.WriteLine("Druk op 'Q' om terug te gaan.");
                switch (Console.ReadLine().ToLower())
                {
                    case "s":
                        {
                            Console.WriteLine("De tour is succesvol gestart\n");
                            Menu.MainMenu();
                            break;
                        }
                    case "q":
                        {
                            goto CheckThePresence;
                        }
                    default:
                        {
                            Console.WriteLine("U heeft een incorrecte invoer opgegeven, probeer opnieuw.");
                            break;
                        }
                }
            }
            else
                Console.WriteLine("Ongeldige tour ID. Probeer opnieuw.");
        }
    }

    public static string AddLastMinuteVisitor(int tourId, string input)
    {
        List<TourModel> tours = baseLogic.GetAllTours();

        while (true)
        {
            if (long.TryParse(input, out long barcode))
            {
                foreach (TourModel tour in tours)
                {
                    if (tour.tourId == tourId)
                    {
                        // check if tour is full
                        if (tour.parttakers < tour.limit)
                        {
                            Add_Remove.Add(new Visitor(barcode), tourId);
                            return $"{barcode} succesvol aangemeld bij de rondleiding van {(tour.dateTime).ToString("dd-M-yyyy HH:mm")}\n";
                        }
                        else
                        {
                            Console.WriteLine("Deze tour is helaas vol, probeer een andere tour.\n");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("U heeft een ongeldig tournummer ingevoerd");
                        continue;
                    }
                }
            }
            else if(input.ToLower() == "q")
            {
                return"";
            }
            else
            {
               Console.WriteLine("U heeft een ongeldige barcode ingevoerd");
                continue; 
            }
        }
    }
}


