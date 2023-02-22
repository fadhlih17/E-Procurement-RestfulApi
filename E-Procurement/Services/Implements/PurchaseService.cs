using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Exceptions;
using E_Procurement.Repositories;

namespace E_Procurement.Services.Implements;

public class PurchaseService : IPurchaseService
{
    private readonly IRepository<Purchase> _repository;
    private readonly IPersistence _persistence;
    private readonly IProductPriceService _productPriceService;
    private readonly IProductService _productService;

    public PurchaseService(IRepository<Purchase> repository, IPersistence persistence,
        IProductPriceService productPriceService, IProductService productService)
    {
        _repository = repository;
        _persistence = persistence;
        _productPriceService = productPriceService;
        _productService = productService;
    }
    
    public async Task<PurchaseResponse> CreateNewTransaction(ICollection<PurchaseRequest> request, string userId)
    {
        ICollection<PurchaseDetail> purchaseDetails = new List<PurchaseDetail>();
        foreach (var req in request)
        {
            purchaseDetails.Add(new PurchaseDetail{Qty = req.Qty, ProductPriceId = req.ProductPriceId});
        }

        var purchaseResponse = await _persistence.ExecuteTransactionAsync(async () =>
        {
            Purchase purchase = new Purchase
            {
                TransDate = DateTime.Now,
                UserId = Guid.Parse(userId),
                PurchaseDetails = purchaseDetails
            };

            foreach (var purchaseDetail in purchase.PurchaseDetails)
            {
                var productPrice = await _productPriceService.GetById(purchaseDetail.ProductPriceId.ToString());
                purchaseDetail.ProductPrice = productPrice;
            }

            var savePurchase = await _repository.Save(purchase);
            await _persistence.SaveChangeAsync();

            foreach (var purchaseDetail in savePurchase.PurchaseDetails)
            {
                purchaseDetail.ProductPrice.Stock -= purchaseDetail.Qty;
            }

            await _persistence.SaveChangeAsync();
            long totalPrice = 0;
            
            return new PurchaseResponse
            {
                Id = purchase.Id.ToString(),
                TransDate = purchase.TransDate,
                PurchaseDetailResponses = purchase.PurchaseDetails.Select(detail =>
                {
                    totalPrice += detail.ProductPrice.Price;
                    return new PurchaseDetailResponse
                    {
                        Id = detail.Id.ToString(),
                        Qty = detail.Qty,
                        ProductPriceId = detail.ProductPrice.Id.ToString()
                    };
                }).ToList(),
                TotalPrice = totalPrice
                
            };
        });
        return purchaseResponse;
    }

    public async Task<IEnumerable<List<ReportResponse>>> ReportDaily(string userId)
    {
        var purchases = await _repository.FindAll(purchase =>
                purchase.UserId.Equals(Guid.Parse(userId)) && purchase.TransDate.Day.Equals(DateTime.Now.Day),
            new[] { "PurchaseDetails.ProductPrice.Product.ProductCategory", "PurchaseDetails.ProductPrice.User" });

        if (purchases is null) throw new NotFoundException("Belum ada transaksi hari ini");
        var reportResponses = purchases.Select(purchase =>
        {
            var purchaseDetail = purchase.PurchaseDetails.Select(detail => new ReportResponse
            {
                ProductCode = detail.ProductPrice.ProductCode,
                Date = purchase.TransDate.Date.ToString("dd/MM/yyyy"),
                VendorName = detail.ProductPrice.User.Username,
                ProductName = detail.ProductPrice.Product.Name,
                Category = detail.ProductPrice.Product.ProductCategory.Name,
                Price = detail.ProductPrice.Price,
                Qty = detail.Qty,
                TotalPrice = detail.ProductPrice.Price * detail.Qty
            }).ToList();

            return purchaseDetail;
        });
        return reportResponses;
    }
    
    public async Task<IEnumerable<List<ReportResponse>>> ReportMonthly(string userId, int date)
    {
        var purchases = await _repository.FindAll(purchase =>
                purchase.UserId.Equals(Guid.Parse(userId)) && purchase.TransDate.Month.Equals(date),
            new[] { "PurchaseDetails.ProductPrice.Product.ProductCategory", "PurchaseDetails.ProductPrice.User" });

        if (purchases is null) throw new NotFoundException("Anda belum melakukan transaksi bulan ini");
        var reportResponses = purchases.Select(purchase =>
        {
            var purchaseDetail = purchase.PurchaseDetails.Select(detail => new ReportResponse
            {
                ProductCode = detail.ProductPrice.ProductCode,
                Date = purchase.TransDate.Date.ToString("dd/MM/yyyy"),
                VendorName = detail.ProductPrice.User.Username,
                ProductName = detail.ProductPrice.Product.Name,
                Category = detail.ProductPrice.Product.ProductCategory.Name,
                Price = detail.ProductPrice.Price,
                Qty = detail.Qty,
                TotalPrice = detail.ProductPrice.Price * detail.Qty
            }).ToList();

            return purchaseDetail;
        });
        return reportResponses;
    }

}