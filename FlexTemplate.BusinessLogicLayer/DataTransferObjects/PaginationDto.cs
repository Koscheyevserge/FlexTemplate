namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PaginationDto
    {
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPagesCount { get; set; }
    }
}
