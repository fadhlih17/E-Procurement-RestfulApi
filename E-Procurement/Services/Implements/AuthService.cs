using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Exceptions;
using E_Procurement.Repositories;
using E_Procurement.Security;

namespace E_Procurement.Services.Implements;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _repository;
    private readonly IPersistence _persistence;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IRepository<User> repository, IPersistence persistence, IJwtUtils jwtUtils)
    {
        _repository = repository;
        _persistence = persistence;
        _jwtUtils = jwtUtils;
    }

    // Check Email for register
    private async Task<string> LoadRegisterEmail(string email)
    {
        var emailUser = await _repository.Find(user => user.Email.Equals(email));
        if (emailUser is not null) throw new UnauthorizedException("Email already registered !");
        return email;
    }

    // Check email for login
    private async Task<User> LoadLoginEmail(string email)
    {
        var user = await _repository.Find(user => user.Email.Equals(email));
        if (user is null) throw new NotFoundException("Email not registered yet !");
        return user;
    }

    // Register for customer
    public async Task<RegisterResponse> RegisterCustomer(RegisterRequest request)
    {
        var emailUser = await LoadRegisterEmail(request.Email);
        // make register transaction
        var registerResponse = await _persistence.ExecuteTransactionAsync(async () =>
        {
            var user = new User
            {
                Username = request.Username,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = emailUser,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                ERole = ERole.Customer
            };

            var save = await _repository.Save(user);
            await _persistence.SaveChangeAsync();

            return new RegisterResponse
            {
                Email = save.Email,
                Role = save.ERole.ToString(),
            };
        });

        return registerResponse;
    }

    public async Task<RegisterResponse> RegisterVendor(RegisterRequest request)
    {
        var emailUser = await LoadRegisterEmail(request.Email);
        var registerResponse = await _persistence.ExecuteTransactionAsync(async () =>
        {
            var user = new User
            {
                Username = request.Username,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = emailUser,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                ERole = ERole.Vendor
            };

            var save = await _repository.Save(user);
            await _persistence.SaveChangeAsync();

            return new RegisterResponse
            {
                Email = save.Email,
                Role = save.ERole.ToString(),
            };
        });

        return registerResponse;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var findUser = await LoadLoginEmail(request.Email);
        var verify = BCrypt.Net.BCrypt.Verify(request.Password, findUser.Password);
        if (!verify) throw new UnauthorizedException("Unauthorized");

        var loginResponse = new LoginResponse
        {
            Email = findUser.Email,
            Role = findUser.ERole.ToString(),
            Token = _jwtUtils.GenerateToken(findUser)
        };

        return loginResponse;
    }
}