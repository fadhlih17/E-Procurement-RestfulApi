namespace E_Procurement.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(){}
    
    public UnauthorizedException(string? message) : base(message){}
}