using hafenix_proj_backend.Models;

namespace hafenix_proj_backend.DTO.DTORes
{
    public class TransactionsDTORes : BaseDTORes
    {
        public List<Transaction> Transactions_List { get; set; }
        public int Total_Transactions { get; set; }
    }
}
