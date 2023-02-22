using E_Procurement.Entities;

namespace E_Procurement.Services;

public interface ICategoryService
{
    Task<IEnumerable<ProductCategory>> GetAllCategory();
}