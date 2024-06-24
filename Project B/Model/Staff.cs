namespace ProjectB;

public class Staff
{
    public List<string> scannedIDS = new();
    public string StaffBarode;

    public Staff(string staffBarcode)
    {
        StaffBarode = staffBarcode;
    }

    public bool CorrectStaffCode()
    {
        List<string> staffBarodes = BaseAccess.loadAllStaffCodes();

        if (staffBarodes.Contains(StaffBarode))
        {
            return true;
        }
        return false;
    }
}