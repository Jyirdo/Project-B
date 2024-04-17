class BarcodeGenerator
{
    public string barcode;
    private string line;

    // Amount is the amount of barcodes it generates.
    public void GenerateBarcodes(int amount)
    {
        for (int x = 0; x < amount; x++)
        {
            Random rnd = new Random();

            for (int i = 0; i < 13; i++)
            {
                barcode += rnd.Next(10).ToString();
            }

            using (StreamReader sr = new StreamReader("../../../barcodes/GeneratedBarcodes.csv"))
            {
                line = sr.ReadLine();
                if (line == barcode)
                {
                    sr.Close();
                    GenerateBarcodes(amount);
                }
                else
                {
                    sr.Close();
                    using (StreamWriter sw = File.AppendText("../../../barcodes/GeneratedBarcodes.csv"))
                    {
                        sw.WriteLine(barcode);
                        Console.WriteLine(barcode);
                        barcode = "";
                    }
                }
            }
        }

    }
}
