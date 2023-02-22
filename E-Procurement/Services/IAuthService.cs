using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;

namespace E_Procurement.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterCustomer(RegisterRequest request);
    Task<RegisterResponse> RegisterVendor(RegisterRequest request);
    Task<LoginResponse> Login(LoginRequest request);
}