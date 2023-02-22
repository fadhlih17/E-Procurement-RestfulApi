using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Procurement.Entities;

[Table(name:"m_product")]
public class Product
{
    [Key, Column(name:"id")]
    public Guid Id { get; set; }

    [Column(name:"name", TypeName = "NVarchar(100)")]
    public string Name { get; set; }

    [Column(name:"product_category_id")]
    public Guid ProductCategoryId { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }
    public virtual ICollection<ProductPrice>? ProductPrices { get; set; }
}