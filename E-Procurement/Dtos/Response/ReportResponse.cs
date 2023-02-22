namespace E_Procurement.Dtos.Response;

public class ReportResponse
{
    public string ProductCode { get; set; }
    public string Date { get; set; }
    public string VendorName { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public long Price { get; set; }
    public int Qty { get; set; }
    public long TotalPrice { get; set; }
}