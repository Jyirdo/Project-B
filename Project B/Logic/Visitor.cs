public class Visitor
{
    public string barcode;

    public Visitor(string barcode1)
    {
        barcode = barcode1;
    }

    public static bool HasTicket(string visitorTicket)
    {
        List<string> tickets = BaseAccess.loadAllVisitorCodes();
        if (tickets.Contains(visitorTicket))
            return true;
        return false;
    }
}