using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;

namespace E_Procurement.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateNewTransaction(ICollection<PurchaseRequest> purchase, string userId);
    Task<IEnumerable<List<ReportResponse>>> ReportDaily(string userId);
    Task<IEnumerable<List<ReportResponse>>> ReportMonthly(string userId, int date);
}