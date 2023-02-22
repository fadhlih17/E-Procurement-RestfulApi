using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Procurement.Entities;

[Table(name:"m_product_price")]
public class ProductPrice
{
    [Key, Column(name:"id")]
    public Guid Id { get; set; }

    [Column(name:"price")]
    public long Price { get; set; }

    [Column(name:"stock")]
    public int Stock { get; set; }
    
    [Column(name:"product_code", TypeName = "NVarchar(6)")]
    public string ProductCode { get; set; }

    [Column(name:"product_id")]
    public Guid ProductId { get; set; }

    [Column(name:"user_id")]
    public Guid UserId { get; set; }

    public virtual Product? Product { get; set; }
    public virtual User? User { get; set; }
}