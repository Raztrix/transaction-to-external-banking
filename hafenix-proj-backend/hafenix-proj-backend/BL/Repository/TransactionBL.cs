using hafenix_proj_backend.BL.Interface;
using hafenix_proj_backend.Data.Interface;
using hafenix_proj_backend.DTO.DTOHelpers;
using hafenix_proj_backend.DTO.DTOReq;
using hafenix_proj_backend.DTO.DTORes;
using hafenix_proj_backend.Models;
using hafenix_proj_backend.Utils;

namespace hafenix_proj_backend.BL.Repository
{
    public class TransactionBL : ITransactionBL
    {
        private readonly HttpClientService _httpClientService;
        private readonly ITransactionData _transactionData;

        public TransactionBL(HttpClientService httpClientService, ITransactionData transactionData)
        {
            _httpClientService = httpClientService;
            _transactionData = transactionData;
        }


        public async Task<OpenBankingDepositOrWithdrawDTORes> Deposit(TransactionDTOReq req)
        {
            OpenBankingDepositOrWithdrawDTORes openBankingDepositOrWithdrawDTORes = new OpenBankingDepositOrWithdrawDTORes();

            OpenBankingGetTokenDTO openBankingGetTokenDTO = new OpenBankingGetTokenDTO() { URL = "https://openBanking/createdeposit", UserId = req.TransactionData.UserId };
            OpenBankingGetTokenDTORes openBankingGetTokenDTORes = await _httpClientService.OpenBankingGetToken(openBankingGetTokenDTO);


            if (openBankingGetTokenDTORes.StatusCode == StatusCode.Failed)
            {
                openBankingDepositOrWithdrawDTORes.StatusCode = StatusCode.Failed;
                return openBankingDepositOrWithdrawDTORes;
            }

            // case of success need to update the db.

            int insert = await _transactionData.AddTransactionRecord(req.TransactionData);
            if (insert > 0)
            {
                openBankingDepositOrWithdrawDTORes.StatusCode = StatusCode.Success;
                openBankingDepositOrWithdrawDTORes.AccountNumber = req.TransactionData.AccountNumber;
                openBankingDepositOrWithdrawDTORes.Amount = req.TransactionData.Amount;
                return openBankingDepositOrWithdrawDTORes;
            }
            else
            {
                openBankingDepositOrWithdrawDTORes.StatusCode = StatusCode.Failed;
                return openBankingDepositOrWithdrawDTORes;
            }


        }

        public async Task<OpenBankingDepositOrWithdrawDTORes> Withdraw(TransactionDTOReq req)
        {
            OpenBankingDepositOrWithdrawDTORes openBankingDepositOrWithdrawDTORes = new OpenBankingDepositOrWithdrawDTORes();

            OpenBankingGetTokenDTO openBankingGetTokenDTO = new OpenBankingGetTokenDTO() { URL = "https://openBanking/createWithdrawal", UserId = req.TransactionData.UserId };
            OpenBankingGetTokenDTORes openBankingGetTokenDTORes = await _httpClientService.OpenBankingGetToken(openBankingGetTokenDTO);


            if (openBankingGetTokenDTORes.StatusCode == StatusCode.Failed)
            {
                openBankingDepositOrWithdrawDTORes.StatusCode = StatusCode.Failed;
                return openBankingDepositOrWithdrawDTORes;
            }

            // case of success need to update the db.

            int insert = await _transactionData.AddTransactionRecord(req.TransactionData);
            if (insert > 0)
            {
                openBankingDepositOrWithdrawDTORes.StatusCode = StatusCode.Success;
                openBankingDepositOrWithdrawDTORes.AccountNumber = req.TransactionData.AccountNumber;
                openBankingDepositOrWithdrawDTORes.Amount = req.TransactionData.Amount;
                return openBankingDepositOrWithdrawDTORes;
            }
            else
            {
                openBankingDepositOrWithdrawDTORes.StatusCode = StatusCode.Failed;
                return openBankingDepositOrWithdrawDTORes;
            }
        }

        public async Task<TransactionsDTORes> GetAllTransactions(int userId, int pageSize, int pageNumber)
        {
            TransactionsDTORes res = new TransactionsDTORes();
            IEnumerable<Transaction> transactions = await _transactionData.GetAllTransactions(userId, pageSize, pageNumber);
            res.Transactions_List = transactions.ToList();
            res.Total_Transactions = await _transactionData.GetTotalTransactionsNumber(userId);
            return res;
            
        }

    }
}
