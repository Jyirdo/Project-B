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

    public static List<Visitor> CheckPresence(List<Visitor> reservations, DateTime tourStartTime, int tourID)
    {
        List<Visitor> reservationsCopy = new(reservations);
        while (true)
        {

            Console.WriteLine($"\x1b[36;1mScan de barcodes op de kaartjes van de bezoekers die nu aanwezig zijn.\nAls u de rondleiding start worden de afwezige bezoekers automatisch verwijderd uit deze rondleiding.\x1b[0m\n");

            Console.WriteLine($"\x1b[1mNog te controleren bezoeker barcode(s) voor de rondleiding van \x1b[35m{tourStartTime.ToString("HH:mm")}\x1b[0m:");
            foreach (Visitor visitor in reservationsCopy)
                Console.WriteLine($"> {visitor.barcode}");

            Console.WriteLine("\nScan een barcode. Druk op \x1b[31m'Q'\x1b[0m om terug te gaan of op \x1b[32m'K'\x1b[0m als u klaar bent:");

            string checkPresence = Console.ReadLine().Trim();
            if (checkPresence.ToLower() == "q")
            {
                Menu.StaffMenu();
            }
            if (checkPresence.ToLower() == "k")
            {
                Console.Clear();
                return reservationsCopy;
            }
            else
            {
                if (long.TryParse(checkPresence, out _))
                {
                    if (BaseAccess.GetVisitorInTour(tourID, checkPresence.Trim()))
                    {
                        scannedIDS.Add(checkPresence);
                        foreach (Visitor visitor in reservations)
                            if (visitor.barcode == checkPresence)
                                reservationsCopy.Remove(visitor);
                        Console.Clear();
                        Console.WriteLine($"\x1b[32;1m{checkPresence} is gecontroleerd.\x1b[0m");
                        continue;
                    }
                    else
                    {
                        string tourTime = Tour.GetTourTime(checkPresence, true);
                        if (tourTime != "U heeft nog geen rondleiding geboekt\n")
                        {
                            Console.Clear();
                            Console.WriteLine($"\x1b[33;1mDeze bezoeker ({checkPresence}) heeft zich aangemeld bij een \x1b[33;1;4mandere rondleiding\x1b[0m\x1b[33;1m (van {tourTime})\x1b[0m");
                            Console.WriteLine("\x1b[31;1mDus dat is een incorrecte barcode.\x1b[0m\n");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\x1b[31;1mDat is een incorrecte barcode.\x1b[0m");
                        }

                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Dat is geen correcte barcode.");
                    continue;
                }
            }
        }
    }


    public static void SelectTourAndCheckTour(int tourId)
    {
        scannedIDS.Clear();
        // Controleer of de ingevoerde tour ID geldig is
        List<TourModel> tours = BaseAccess.LoadAll();


        foreach (TourModel tour in tours)
        {
            if (tour.tourId == tourId)
            {
            // Hier worden reservation ID's voor de geselecteerde tour geprint

            CheckThePresence:
                List<Visitor> tempVisitorList = new(tour.tourVisitorList);
                List<Visitor> presenceList = CheckPresence(tempVisitorList, tour.dateTime, tourId);

                foreach (string scanned in scannedIDS)
                    foreach (Visitor visitor in tempVisitorList)
                        if (visitor.barcode == scanned)
                            presenceList.Remove(visitor);

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
                    foreach (Visitor notpresent in presenceList)
                        Console.WriteLine($"> {notpresent.barcode}");
                    Console.WriteLine($"Aantal mensen afwezig: {presenceList.Count}.\n");

                }
                Console.WriteLine("Druk op \x1b[32m'S'\x1b[0m om de tour te starten.");
                Console.WriteLine("Druk op \x1b[31m'Q'\x1b[0m om terug te gaan en verder te scannen.");
                switch (Console.ReadLine().ToLower())
                {
                    case "s":
                        {
                            foreach (Visitor notpresent in presenceList)
                                Add_Remove.Remove(notpresent, tourId);

                            List<TourModel> tours2 = BaseAccess.LoadAll();
                            TourModel tourToUpdate = tours2.FirstOrDefault(t => t.tourId == tour.tourId);

                            if (tourToUpdate != null)
                                tourToUpdate.tourStarted = true;

                            BaseAccess.WriteAll(tours2);

                            Console.WriteLine("\x1b[32;1mDe rondleiding is succesvol gestart.\x1b[0m\n");

                            Menu.MainMenu();
                            break;

                        }
                    case "q":
                        {
                            goto CheckThePresence;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("\x1b[31;1mU heeft een incorrecte invoer opgegeven, probeer opnieuw.\x1b[0m");
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
                            Console.Clear();
                            return $"\x1b[32;1m{input.Trim()} is succesvol aangemeld bij de rondleiding van {(tour.dateTime).ToString("dd-M-yyyy HH:mm")}\x1b[0m\n";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Deze tour is helaas vol, probeer een andere tour.\n");
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("U heeft een ongeldig rondleidingsnummer ingevoerd");
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
                Console.Clear();
                Console.WriteLine("U heeft een ongeldige barcode ingevoerd");
                continue;
            }
        }
    }
}


