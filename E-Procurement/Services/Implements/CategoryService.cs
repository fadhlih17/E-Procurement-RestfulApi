using E_Procurement.Entities;
using E_Procurement.Repositories;

namespace E_Procurement.Services.Implements;

public class CategoryService : ICategoryService
{
    private readonly IRepository<ProductCategory> _repository;

    public CategoryService(IRepository<ProductCategory> repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ProductCategory>> GetAllCategory()
    {
        var findAll = await _repository.FindAll(p => true);
        return findAll;
    }
}