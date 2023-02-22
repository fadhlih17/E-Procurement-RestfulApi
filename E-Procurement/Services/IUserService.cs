using E_Procurement.Dtos.Response;

namespace E_Procurement.Services;

public interface IUserService
{
    Task<List<UserResponse>> GetAllVendors();
    public Task<IEnumerable<VendorProductResponse>> GetVendorWithProducts();
}