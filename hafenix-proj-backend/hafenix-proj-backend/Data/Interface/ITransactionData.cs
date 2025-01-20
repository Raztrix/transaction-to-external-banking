using hafenix_proj_backend.Models;

namespace hafenix_proj_backend.Data.Interface
{
    public interface ITransactionData
    {
        Task<int> AddTransactionRecord(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactions(int userId, int pageSize, int pageNumber);
        Task<int> GetTotalTransactionsNumber(int userId);
    }
}
