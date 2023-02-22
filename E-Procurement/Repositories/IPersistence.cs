namespace E_Procurement.Repositories;

public interface IPersistence
{
    Task SaveChangeAsync();
    Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
}