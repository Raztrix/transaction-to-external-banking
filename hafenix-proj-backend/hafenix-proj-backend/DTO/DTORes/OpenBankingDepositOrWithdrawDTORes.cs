using hafenix_proj_backend.DTO.DTOHelpers;

namespace hafenix_proj_backend.DTO.DTORes
{
    public class OpenBankingDepositOrWithdrawDTORes : BaseDTORes
    {
        public string Amount { get; set; }
        public string AccountNumber { get; set; }
    }
}
