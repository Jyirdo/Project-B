namespace ProjectB;

class NoMoreTours : View
{
    public static void Show()
    {
        WriteLine("De laatste rondleiding is al geweest, u kunt zich niet meer aanmelden bij een rondleiding. Excuses voor het ongemak.\n");
        WriteLine("Toets 'ENTER' om terug te gaan naar het hoofdmenu.");
        ReadLine();
    }
}
