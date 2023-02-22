using E_Procurement.Entities;

namespace E_Procurement.Security;

public interface IJwtUtils
{
    string GenerateToken(User user);
}