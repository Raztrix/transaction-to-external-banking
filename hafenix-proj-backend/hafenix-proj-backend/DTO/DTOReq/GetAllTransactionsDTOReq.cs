namespace hafenix_proj_backend.DTO.DTOReq
{
    public class GetAllTransactionsDTOReq
    {
        public int UserId { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
