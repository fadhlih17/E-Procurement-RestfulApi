namespace E_Procurement.Dtos.Request;

public class UpdateProductRequest
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public Guid ProductCategoryId { get; set; }
}