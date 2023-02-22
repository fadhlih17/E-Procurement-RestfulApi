using E_Procurement.Entities;

namespace E_Procurement.Dtos.Response;

public class VendorProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public IEnumerable<VendorPriceResponse> VendorPriceResponses { get; set; }
}