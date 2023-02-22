namespace E_Procurement.Dtos.Request;

public class ProductRequest
{
    public string Name { get; set; }
    public string CategoryId { get; set; }
    public string ProductCode { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}