namespace Duc.Splitt.Common.Dtos.Responses
{
    public class PagedResponseDto<T> : ResponseDto<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

    }

}
