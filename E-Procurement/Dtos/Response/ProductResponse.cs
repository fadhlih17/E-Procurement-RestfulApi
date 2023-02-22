namespace E_Procurement.Dtos.Response;

public class ProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public ICollection<ProductPriceResponse> ProductPrices { get; set; }
}