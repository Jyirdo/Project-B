namespace ProjectB;

public class VisitorAlreadyParticipatedInTour : View
{
    public static void Show()
    {
        WriteLine("U heeft vandaag al deelgenomen aan een rondleiding. Wij zien u graag een volgende keer terug!\n");
        WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
        ReadLine();
    }
}