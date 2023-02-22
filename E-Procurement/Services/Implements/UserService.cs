using System.Collections;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Repositories;

namespace E_Procurement.Services.Implements;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository, IProductService productService)
    {
        _repository = repository;
    }
    
    public async Task<List<UserResponse>> GetAllVendors()
    {
        var vendors = await _repository.FindAll(p => p.ERole.Equals(ERole.Vendor));

        var vendorResponse = vendors.Select(p =>
        {
            return new UserResponse
            {
                Id = p.Id.ToString(),
                Name = p.Username,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                Role = p.ERole.ToString()
            };
        }).ToList();
        return vendorResponse;
    }

    public async Task<IEnumerable<VendorProductResponse>> GetVendorWithProducts()
    {
        var vendors = await _repository.FindAll(p => 
            p.ERole.Equals(ERole.Vendor), new[] { "ProductPrices.Product" });

        var vendorResponse = vendors.Select(p =>
        {
            var productPrices = p.ProductPrices.Select(price =>
            {
                VendorPriceResponse priceResponse = new VendorPriceResponse()
                {
                    Id = price.Id.ToString(),
                    ProductCode = price.ProductCode,
                    NameProduct = price.Product.Name,
                    Stock = price.Stock,
                    Price = price.Price,
                };
                return priceResponse;
            }).ToList();

            return new VendorProductResponse
            {
                Id = p.Id.ToString(),
                Name = p.Username,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                VendorPriceResponses = productPrices
            };
        });
        return vendorResponse;
    }
}