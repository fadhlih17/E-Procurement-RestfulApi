namespace E_Procurement.Dtos.Response;

public class PurchaseResponse
{
    public string Id { get; set; }
    public DateTime TransDate { get; set; }
    public ICollection<PurchaseDetailResponse> PurchaseDetailResponses { get; set; }
    public long TotalPrice { get; set; }
}