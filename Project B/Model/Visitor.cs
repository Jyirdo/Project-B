namespace ProjectB;

public class Visitor : Barcodable
{
    public Visitor(string barcode)
    {
        Barcode = barcode;
    }

    public bool CorrectVisitorCode()
    {
        List<string> VisitorCodes = BaseAccess.loadAllVisitorCodes();

        if (VisitorCodes.Contains(Barcode))
        {
            return true;
        }
        return false;
    }
}