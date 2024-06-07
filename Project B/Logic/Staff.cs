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
            Console.WriteLine("Scan een barcode. Druk op 'Q' om terug te gaan of op 'K' als u klaar bent:");
            string checkPresence = Console.ReadLine();
            if (checkPresence.ToLower() == "q")
            {
                return null;
            }
            if (checkPresence.ToLower() == "k")
            {
                Console.Clear();
                List<string> reservations2 = reservations;
                return reservations2;
            }
            else
            {
                if (long.TryParse(checkPresence, out _))
                {
                    if (BaseAccess.GetVisitorInTour(tourID, checkPresence.Trim()))
                    {
                        scannedIDS.Add(checkPresence);
                        reservations.Remove(Convert.ToString(checkPresence.Trim()));
                        Console.WriteLine($"\x1b[32;1m{checkPresence} is gecontroleerd.\x1b[0m");
                        continue;
                    }
                    else
                    {
                        string tourTime = Tour.GetTourTime(checkPresence, true);
                        if (tourTime != "U heeft nog geen rondleiding geboekt\n")
                        {
                            Console.WriteLine($"\x1b[36;1mDeze bezoeker heeft zich aangemeld bij een \x1b[36;1;4mandere rondleiding\x1b[0m\x1b[36;1m (van {tourTime})\x1b[0m");
                            Console.WriteLine("\x1b[31;1mDus dat is een incorrecte barcode.\x1b[0m\n");
                        }
                        else
                            Console.WriteLine("\x1b[31;1mDat is een incorrecte barcode.\x1b[0m");

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
        List<TourModel> tours = BaseAccess.LoadAll();

        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourId)
            {
                // Hier worden reservation ID's voor de geselecteerde tour geprint
                Console.WriteLine($"Bezoeker barcode(s) voor tour met ID \x1b[35m\x1b[1m{tourId}\x1b[0m:");
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
                    Console.WriteLine("\x1b[1mDeze ID's zijn afwezig:\x1b[0m\n");
                    foreach (string notpresent in presenceList)
                        Console.WriteLine($"{notpresent}");
                    Console.WriteLine($"Aantal mensen afwezig: {presenceList.Count}.\n");

                }

                Console.WriteLine("Druk op \x1b[33m'S'\x1b[0m om de tour te starten.");
                Console.WriteLine("Druk op \x1b[31m'Q'\x1b[0m om terug te gaan en verder te scannen.");
                switch (Console.ReadLine().ToLower())
                {
                    case "s":
                        {
                            foreach (string notpresent in presenceList)
                                Add_Remove.Remove(new Visitor(notpresent), tourId);

                            List<TourModel> tours2 = BaseAccess.LoadAll();
                            TourModel tourToUpdate = tours2.FirstOrDefault(t => t.tourId == tour.tourId);

                            if (tourToUpdate != null)
                                tourToUpdate.tourStarted = true;

                            BaseAccess.WriteAll(tours2);

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


