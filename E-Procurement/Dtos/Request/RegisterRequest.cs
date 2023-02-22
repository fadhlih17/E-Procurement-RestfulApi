namespace E_Procurement.Dtos.Request;

public class RegisterRequest
{
    public string Username { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}