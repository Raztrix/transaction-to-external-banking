namespace hafenix_proj_backend.Models
{
    public class Transaction
    {
        public int Transaction_ID { get; set; }
        public string FullNameHeb { get; set; }
        public string FullNameEng { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserId { get; set; } // id can start with 0
        public int ActionType { get; set; }
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
    }
}
