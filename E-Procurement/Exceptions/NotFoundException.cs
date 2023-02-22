namespace E_Procurement.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(){}
    
    public NotFoundException(string? message) : base(message){}
}