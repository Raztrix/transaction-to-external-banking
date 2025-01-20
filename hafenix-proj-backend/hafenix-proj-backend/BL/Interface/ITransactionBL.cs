using hafenix_proj_backend.DTO.DTOReq;
using hafenix_proj_backend.DTO.DTORes;
using hafenix_proj_backend.Models;

namespace hafenix_proj_backend.BL.Interface
{
    public interface ITransactionBL
    {

        Task<OpenBankingDepositOrWithdrawDTORes> Deposit(TransactionDTOReq req);
        Task<OpenBankingDepositOrWithdrawDTORes> Withdraw(TransactionDTOReq req);
        Task<TransactionsDTORes> GetAllTransactions(int userId, int pageSize, int pageNumber);

    }
}
