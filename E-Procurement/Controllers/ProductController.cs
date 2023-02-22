using System.Net;
using System.Security.Claims;
using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Procurement.Controllers;

[Route("api/products")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductPriceService _productPriceService;

    public ProductController(IProductService productService, IProductPriceService productPriceService)
    {
        _productService = productService;
        _productPriceService = productPriceService;
    }

    [HttpPost]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> CreateNewProduct([FromBody] ProductRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var productResponse = await _productService.CreateNewProduct(request, userId);

        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully Create Product",
            Data = productResponse
        };
        return Created("api/products", response);
    }

    [HttpPut]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var productResponse = await _productService.Update(request);
        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Update Product",
            Data = productResponse
        };
        return Ok(response);
    }

    [HttpPut]
    [Route("product-update")]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> UpdateProductPrice([FromBody] UpdatePriceRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var productPriceResponse = await _productPriceService.UpdateProductPrice(request, userId);
        CommonResponse<ProductPriceResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Update Product",
            Data = productPriceResponse
        };
        return Ok(response);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("{name}")]
    public async Task<IActionResult> GetAllProducts([FromQuery] string? name = "")
    {
        var products = await _productService.GetAll(name);
        CommonResponse<IEnumerable<ProductResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Products",
            Data = products
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct([FromQuery] string id)
    {
        var vendorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _productPriceService.DeleteById(id, vendorId);
        CommonResponse<ProductPriceResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Delete Product"
        };
        return Ok(response);
    }
}