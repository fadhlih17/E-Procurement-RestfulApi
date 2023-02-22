using System.Net;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Procurement.Controllers;

[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var allCategory = await _service.GetAllCategory();
        CommonResponse<IEnumerable<ProductCategory>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Categories",
            Data = allCategory
        };
        return Ok(response);
    }
}