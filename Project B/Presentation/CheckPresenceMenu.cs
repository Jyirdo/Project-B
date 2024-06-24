namespace ProjectB;

public class CheckPresenceMenu : View
{
    public static string Show(string[] reservations, DateTime TourStartTime)
    {
        WriteLine($"\x1b[36;1mScan de barcodes op de kaartjes van de bezoekers die nu aanwezig zijn.\nAls u de rondleiding start worden de afwezige bezoekers automatisch verwijderd uit deze rondleiding.\x1b[0m\n");
        WriteLine($"\x1b[1mNog te controleren bezoeker barcode(s) voor de rondleiding van \x1b[35m{TourStartTime.ToString("HH:mm")}\x1b[0m:");
        WriteLine($"{string.Join("\n", reservations)}");
        WriteLine("\nScan een barcode. Druk op \x1b[31m'Q'\x1b[0m om terug te gaan of op \x1b[32m'K'\x1b[0m als u klaar bent:");
        return ReadLine();
    }
}