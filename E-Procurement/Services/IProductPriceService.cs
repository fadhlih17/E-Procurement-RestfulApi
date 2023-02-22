using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;

namespace E_Procurement.Services;

public interface IProductPriceService
{
    Task<ProductPrice> GetById(string id);
    Task<ProductPriceResponse> UpdateProductPrice(UpdatePriceRequest request, string userId);
    Task DeleteById(string id, string vendorId);
}