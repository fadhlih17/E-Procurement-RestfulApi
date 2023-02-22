namespace E_Procurement.Dtos.Request;

public class PurchaseRequest
{
    public int Qty { get; set; }
    public Guid ProductPriceId { get; set; }
}