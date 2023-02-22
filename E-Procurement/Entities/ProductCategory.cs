using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Procurement.Entities;

[Table(name:"m_product_category")]
public class ProductCategory
{
    [Key, Column(name:"id")]
    public Guid Id { get; set; }

    [Column(name:"name", TypeName = "Varchar(50)")]
    public string Name { get; set; }
}