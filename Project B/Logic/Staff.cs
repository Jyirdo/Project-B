using System.Data;
using Newtonsoft.Json;
// Sometime in the future make these methods static somehow
public class Staff
{
    public static List<string> staffCodes = new();
    public static List<string> scannedIDS = new();
    private static BaseLogic baseLogic = new BaseLogic();

    public static bool CorrectStaffCode(string staffcode)
    {
        staffCodes = BaseAccess.loadAllStaffCodes();
        if (staffCodes.Contains(staffcode))
        {
            return true;
        }
        return false;
    }

    public static List<string> CheckPresence(List<string> reservations, int tourID)
    {
        while (true)
        {
            Console.WriteLine("Scan een barcode. Druk op \x1b[31m'Q'\x1b[0m om terug te gaan of op \x1b[32m'K'\x1b[0m als u klaar bent:");
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
                if (long.TryParse(checkPresence, out _))
                {
                    if (BaseAccess.GetVisitorInTour(tourID, checkPresence.Trim()))
                    {
                        scannedIDS.Add(checkPresence);
                        reservations.Remove(Convert.ToString(checkPresence.Trim()));
                        Console.WriteLine($"{checkPresence} is gecontroleerd.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Dat is geen correcte barcode.");
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
                List<string> presenceList = CheckPresence(reservationIDS, tourId);

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
                    Console.WriteLine("Deze ID's zijn afwezig:");
                    foreach (string notpresent in presenceList)
                    {
                        int enumerator = 1;
                        Console.WriteLine($"{enumerator}. ({notpresent})");
                        Add_Remove.Remove(new Visitor(notpresent), tourId);
                        enumerator++;
                    }
                }

                Console.WriteLine("Druk op \x1b[32m'S'\x1b[0m om de tour te starten.");
                Console.WriteLine("Druk op \x1b[31m'Q'\x1b[0m om terug te gaan en verder te scannen.");
                switch (Console.ReadLine().ToLower())
                {
                    case "s":
                        {
                            tour.tourStarted = true;
                            BaseAccess.WriteAll(tours);
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
        }
    }

    public static string AddLastMinuteVisitor(int tourId, string input)
    {
        List<TourModel> tours = baseLogic.GetAllTours();

        while (true)
        {
            if (long.TryParse(input, out _))
            {
                foreach (TourModel tour in tours)
                {
                    if (tour.tourId == tourId)
                    {
                        // check if tour is full
                        if (tour.parttakers < tour.limit)
                        {
                            Add_Remove.Add(new Visitor(input.Trim()), tourId);
                            return $"{input.Trim()} succesvol aangemeld bij de rondleiding van {(tour.dateTime).ToString("dd-M-yyyy HH:mm")}\n";
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
            else if (input.ToLower() == "q")
            {
                return "";
            }
            else
            {
                Console.WriteLine("U heeft een ongeldige barcode ingevoerd");
                continue;
            }
        }
    }
}


