namespace ProjectB;

public class Visitor
{
    public string barcode;

    public Visitor(string barcode1)
    {
        barcode = barcode1;
    }

    public bool CorrectVisitorCode()
    {
        List<string> VisitorCodes = BaseAccess.loadAllVisitorCodes();

        if (VisitorCodes.Contains(barcode))
        {
            return true;
        }
        return false;
    }
}