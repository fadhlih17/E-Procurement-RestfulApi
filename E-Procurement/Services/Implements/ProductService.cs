using E_Procurement.Dtos.Request;
using E_Procurement.Dtos.Response;
using E_Procurement.Entities;
using E_Procurement.Exceptions;
using E_Procurement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Procurement.Services.Implements;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IPersistence _persistence;

    public ProductService(IRepository<Product> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<ProductResponse> CreateNewProduct(ProductRequest request, string userId)
    {
        var product = await _repository.Find(p => 
            p.Name.ToLower().Equals(request.Name.ToLower()), new[] { "ProductPrices", "ProductCategory" });

        var productSave = new Product
        {
            Name = request.Name,
            ProductCategoryId = Guid.Parse(request.CategoryId),
            ProductPrices = new List<ProductPrice>
            {
                new ProductPrice
                {
                    Price = request.Price,
                    Stock = request.Stock,
                    ProductCode = request.ProductCode,
                    UserId = Guid.Parse(userId),
                }
            }
        };
        
        if (product is null)
        {
            var result = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var product = await _repository.Save(productSave);
                await _persistence.SaveChangeAsync();
                return product;
            });

            var productPriceResponse = result.ProductPrices.Select(price => new ProductPriceResponse
            {
                Id = price.Id.ToString(),
                ProductCode = price.ProductCode,
                Stock = price.Stock,
                Price = price.Price,
                VendorId = price.UserId.ToString()
            }).ToList();

            var findResult = await _repository.Find(p => p.Name.ToLower().Equals(request.Name.ToLower()), 
                new[] { "ProductPrices", "ProductCategory" });
            
            ProductResponse response = new()
            {
                Id = findResult.Id.ToString(),
                Name = findResult.Name,
                Category = findResult.ProductCategory.Name,
                ProductPrices = productPriceResponse
            };

            return response;
        }

        var productPriceRequest = productSave.ProductPrices.ToList();
        ProductPrice price = new ProductPrice
        {
            Price = productPriceRequest[0].Price,
            Stock = productPriceRequest[0].Stock,
            ProductCode = productPriceRequest[0].ProductCode,
            UserId = productPriceRequest[0].UserId,
        };
        
        product.ProductPrices.Add(price);
        await _persistence.SaveChangeAsync();
        
        ProductResponse productResponse = new()
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Category = product.ProductCategory.Name,
            ProductPrices = new List<ProductPriceResponse>
            {
                new ()
                {
                    Id = price.Id.ToString(),
                    Price = price.Price,
                    ProductCode = price.ProductCode,
                    Stock = price.Stock,
                    VendorId = price.UserId.ToString()
                }
            }
        };

        return productResponse;
    }

    public async Task<ProductResponse> GetById(string id)
    {
        var product = await _repository.Find(product =>
            product.Id.Equals(Guid.Parse(id)), new[] { "ProductPrices" });

        if (product is null) throw new NotFoundException("Product Not Found");

        var productPriceResponse = product.ProductPrices.Select(p => new ProductPriceResponse
        {
            Id = p.Id.ToString(),
            ProductCode = p.ProductCode,
            Stock = p.Stock,
            Price = p.Price,
            VendorId = p.UserId.ToString()
        }).ToList();

        ProductResponse response = new ProductResponse
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            ProductPrices = productPriceResponse
        };

        return response;
    }

    public async Task<IEnumerable<ProductResponse>> GetAll(string? name)
    {
        var products = await _repository.FindAll(
            criteria: p => EF.Functions.Like(p.Name, $"%{name}%"),
            includes: new[] { "ProductPrices", "ProductCategory"}
        );
        
        var productResponses = products.Select(product =>
        {
            var productPriceResponses = product.ProductPrices.Select(productPrice =>
            {
                ProductPriceResponse productPriceResponse = new()
                {
                    Id = productPrice.Id.ToString(),
                    ProductCode = productPrice.ProductCode,
                    Price = productPrice.Price,
                    Stock = productPrice.Stock,
                    VendorId = productPrice.UserId.ToString()
                };
                return productPriceResponse;
            }).ToList();

            return new ProductResponse
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Category = product.ProductCategory.Name,
                ProductPrices = productPriceResponses
            };
        });

        return productResponses;
    }

    public async Task<ProductResponse> Update(UpdateProductRequest request)
    {
        Product payload = new Product
        {
            Id = request.Id,
            Name = request.ProductName,
            ProductCategoryId = request.ProductCategoryId
        };
        
        if (payload.Id == Guid.Empty) throw new NotFoundException("Product Not Found");

        _repository.Update(payload);
        await _persistence.SaveChangeAsync();

        var product = await _repository.Find(p => p.Id.Equals(payload.Id), new[] { "ProductCategory" });
        
        return new ProductResponse
        {
            Id = payload.Id.ToString(),
            Name = payload.Name,
            Category = product.ProductCategory.Name
        };
    }
}