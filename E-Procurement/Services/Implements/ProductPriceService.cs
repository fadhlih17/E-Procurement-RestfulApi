using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Exceptions;
using E_Procurement.Repositories;

namespace E_Procurement.Services.Implements;

public class ProductPriceService : IProductPriceService
{
    private readonly IRepository<ProductPrice> _repository;
    private readonly IPersistence _persistence;

    public ProductPriceService(IRepository<ProductPrice> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<ProductPrice> GetById(string id)
    {
        var findById = await _repository.FindById(Guid.Parse(id));
        if (findById is null) throw new NotFoundException("Product Not Found");
        return findById;
    }

    public async Task<ProductPriceResponse> UpdateProductPrice(UpdatePriceRequest request, string userId)
    {
        var newProduct = await GetById(request.Id.ToString());
        
        var verify = newProduct.UserId.Equals(Guid.Parse(userId));
        if (!verify) throw new UnauthorizedException("can't Update Product");

        newProduct.Price = request.Price;
        newProduct.Stock = request.Stock;
        newProduct.ProductCode = request.ProductCode;

        _repository.Update(newProduct);
        await _persistence.SaveChangeAsync();

        return new ProductPriceResponse
        {
            Id = newProduct.Id.ToString(),
            ProductCode = newProduct.ProductCode,
            Stock = newProduct.Stock,
            Price = newProduct.Price,
            VendorId = newProduct.UserId.ToString()
        };
    }

    public async Task DeleteById(string id, string vendorId)
    {
        var productPrice = await _repository.Find(price => 
            price.Id.Equals(Guid.Parse(id)) && price.UserId.Equals(Guid.Parse(vendorId)));
        if (productPrice is null) throw new NotFoundException("Product Not Found");
        _repository.Delete(productPrice);
        await _persistence.SaveChangeAsync();
    }
}