using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Procurement.Entities;

[Table(name:"m_user")]
public class User
{
    [Key, Column(name:"id")] public Guid Id { get; set; }

    [Column(name:"username", TypeName = "NVarchar(50)")]
    public string Username { get; set; }

    [Column(name:"address", TypeName = "NVarchar(150)")]
    public string Address { get; set; }

    [Column(name:"phone_number", TypeName = "NVarchar(14)")]
    public string PhoneNumber { get; set; }

    [Column(name:"email"), Required, EmailAddress]
    public string Email { get; set; }

    [Column(name:"password"),Required, StringLength(maximumLength:int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; }

    [Column(name:"role")]
    public ERole ERole { get; set; }
    
    public virtual ICollection<ProductPrice>? ProductPrices { get; set; }
}