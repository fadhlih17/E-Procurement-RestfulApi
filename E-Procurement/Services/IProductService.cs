using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;

namespace E_Procurement.Services;

public interface IProductService
{
    Task<ProductResponse> CreateNewProduct(ProductRequest request, string userId);
    Task<ProductResponse> GetById(string id);
    Task<IEnumerable<ProductResponse>> GetAll(string? name);
    Task<ProductResponse> Update(UpdateProductRequest payload);
}