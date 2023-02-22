namespace E_Procurement.Dtos.Response;

public class ProductPriceResponse
{
    public string Id { get; set; }
    public string ProductCode { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
    public string VendorId { get; set; }
}