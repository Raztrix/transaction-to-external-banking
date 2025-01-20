namespace hafenix_proj_backend.DTO.DTOHelpers
{
    public class OpenBankingDepositOrWithdrawDTO
    {
        public string Token { get; set; }
        public string URL { get; set; }
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
        public ActionType ActionType { get; set; }
    }

    public enum ActionType
    {
        Deposit,
        Withdrawal
    }


}
