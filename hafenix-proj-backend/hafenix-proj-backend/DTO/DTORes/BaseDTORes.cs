namespace hafenix_proj_backend.DTO.DTORes
{
    public class BaseDTORes
    {
        public StatusCode StatusCode { get; set; }
        public string InfoText { get; set; }
        public ErrorType ErrorType { get; set; }
    }

    public enum StatusCode
    {
        Success,
        Failed
    }

    public enum ErrorType
    {
        EmptyAccountNumber,
        EmptyAmountOrZero,
        Unknown,
        WrongUserId,
        WrongToken,
        WrongURL
    }
}
