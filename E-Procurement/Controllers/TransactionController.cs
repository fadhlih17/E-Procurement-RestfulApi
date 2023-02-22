using System.Net;
using System.Security.Claims;
using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Procurement.Controllers;

[Route("api/transactions")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public TransactionController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> CreateTransaction([FromBody] ICollection<PurchaseRequest> requests)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var purchaseResponse = await _purchaseService.CreateNewTransaction(requests, userId);
        CommonResponse<PurchaseResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Transaction Successfully",
            Data = purchaseResponse
        };
        return Created("api/transactions", response);
    }

    [HttpGet]
    [Route("daily-report")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetReportDaily()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var reportResponse = await _purchaseService.ReportDaily(userId);
        CommonResponse<IEnumerable<List<ReportResponse>>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Daily Report",
            Data = reportResponse
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("monthly-report/{date}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetMonthlyReport([FromQuery] int date)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var reportMonthly = await _purchaseService.ReportMonthly(userId, date);
        CommonResponse<IEnumerable<List<ReportResponse>>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Monthly Report",
            Data = reportMonthly
        };
        return Ok(response);
    }
}