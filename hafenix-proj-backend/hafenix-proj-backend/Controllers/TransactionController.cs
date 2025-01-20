using hafenix_proj_backend.BL.Interface;
using hafenix_proj_backend.DTO.DTOReq;
using hafenix_proj_backend.DTO.DTORes;
using hafenix_proj_backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace hafenix_proj_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionBL _transactionBL;

        public TransactionController(ITransactionBL transactionBL)
        {
            _transactionBL = transactionBL;
        }

        [HttpPost]

        public async Task<IActionResult> Deposit([FromBody] TransactionDTOReq req)
        {
            bool accountNumberIsValid = Validations.ValidateAccountNumber(req.TransactionData.AccountNumber);
            bool amountIsValid = Validations.ValidateAmount(req.TransactionData.Amount);
            bool userIdIsValid = Validations.ValidateUserId(req.TransactionData.UserId);
            if (!accountNumberIsValid)
            {
                return Ok(new BaseDTORes() { StatusCode = DTO.DTORes.StatusCode.Failed, InfoText = "יש להזין מספר חשבון תקין", ErrorType = ErrorType.EmptyAccountNumber});
            }

            if (!amountIsValid)
            {
                return Ok(new BaseDTORes() { StatusCode = DTO.DTORes.StatusCode.Failed, InfoText = "יש להזין סכום תקין להפקדה", ErrorType = ErrorType.EmptyAmountOrZero });
            }

            if (!userIdIsValid)
            {
                return Ok(new BaseDTORes() { StatusCode = DTO.DTORes.StatusCode.Failed, InfoText = "מספר תעודת זהות לא תקין", ErrorType = ErrorType.WrongUserId });
            }

            OpenBankingDepositOrWithdrawDTORes res = await _transactionBL.Deposit(req);
            return Ok(res);

            
        }

        [HttpPost]
        public async Task<IActionResult> Withdrawal([FromBody] TransactionDTOReq req)
        {
            bool accountNumberIsValid = Validations.ValidateAccountNumber(req.TransactionData.AccountNumber);
            bool amountIsValid = Validations.ValidateAmount(req.TransactionData.Amount);
            bool userIdIsValid = Validations.ValidateUserId(req.TransactionData.UserId);
            if (!accountNumberIsValid)
            {
                return Ok(new BaseDTORes() { StatusCode = DTO.DTORes.StatusCode.Failed, InfoText = "יש להזין מספר חשבון תקין", ErrorType = ErrorType.EmptyAccountNumber });
            }

            if (!amountIsValid)
            {
                return Ok(new BaseDTORes() { StatusCode = DTO.DTORes.StatusCode.Failed, InfoText = "יש להזין סכום תקין למשיכה", ErrorType = ErrorType.EmptyAmountOrZero });
            }

            if (!userIdIsValid)
            {
                return Ok(new BaseDTORes() { StatusCode = DTO.DTORes.StatusCode.Failed, InfoText = "מספר תעודת זהות לא תקין", ErrorType = ErrorType.WrongUserId });
            }

            OpenBankingDepositOrWithdrawDTORes res = await _transactionBL.Withdraw(req);
            return Ok(res);

        }

        [HttpPost]

        public async Task<IActionResult> GetAllTransactions([FromBody] GetAllTransactionsDTOReq req)
        {
            TransactionsDTORes res = new TransactionsDTORes();
            res = await _transactionBL.GetAllTransactions(req.UserId, req.pageSize, req.pageNumber);
            res.StatusCode = DTO.DTORes.StatusCode.Success;
            return Ok(res);
        }

    }
}
