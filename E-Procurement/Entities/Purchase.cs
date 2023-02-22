using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Procurement.Entities;

[Table(name: "t_purchase")]
public class Purchase
{
    [Key, Column(name: "id")] 
    public Guid Id { get; set; }
    
    [Column(name: "trans_date")] 
    public DateTime TransDate { get; set; }
    
    [Column(name: "user_id")] 
    public Guid UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
}