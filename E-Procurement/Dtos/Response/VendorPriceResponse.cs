namespace E_Procurement.Dtos.Response;

public class VendorPriceResponse
{
    public string Id { get; set; }
    public string ProductCode { get; set; }
    public string NameProduct { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}