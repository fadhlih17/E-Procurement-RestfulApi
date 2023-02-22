namespace E_Procurement.Dtos.Response;

public class CommonResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = String.Empty;
    public T Data { get; set; }
}