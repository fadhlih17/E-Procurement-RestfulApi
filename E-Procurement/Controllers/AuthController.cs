using System.Net;
using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Procurement.Controllers;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody]RegisterRequest request)
    {
        var registerCustomer = await _authService.RegisterCustomer(request);
        var response = new CommonResponse<RegisterResponse>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully register customer",
            Data = registerCustomer
        };
        return Created("api/auth/register-customer", response);
    }

    [HttpPost]
    [Route("register-vendor")]
    public async Task<IActionResult> RegisterVendor([FromBody]RegisterRequest request)
    {
        var registerVendor = await _authService.RegisterVendor(request);
        var response = new CommonResponse<RegisterResponse>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully register vendor",
            Data = registerVendor
        };
        return Created("api/auth/register-vendor", response);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var loginResponse = await _authService.Login(request);
        var response = new CommonResponse<LoginResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Login",
            Data = loginResponse
        };
        return Ok(response);
    }
}