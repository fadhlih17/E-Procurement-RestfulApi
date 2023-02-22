namespace E_Procurement.Dtos.Request;

public class UpdatePriceRequest
{
    public Guid Id { get; set; }
    public long Price { get; set; }
    public int Stock { get; set; }
    public string ProductCode { get; set; }
    //public Guid ProductId { get; set; }
}