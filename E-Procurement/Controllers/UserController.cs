using System.Net;
using E_Procurement.Dtos.Response;
using E_Procurement.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Procurement.Controllers;

[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("vendor")]
    public async Task<IActionResult> GetAllVendors()
    {
        var allVendors = await _userService.GetAllVendors();
        CommonResponse<List<UserResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Vendor",
            Data = allVendors
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetVendorWithProducts()
    {
        var vendorResponses = await _userService.GetVendorWithProducts();
        CommonResponse<IEnumerable<VendorProductResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Vendor With Products",
            Data = vendorResponses
        };
        return Ok(response);
    }
}