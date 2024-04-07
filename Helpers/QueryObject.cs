namespace StocksApp.Helpers
{
    public class QueryObject
    {
        //Filtering
        public string? Symbol { get; set; } = null;

        public string? CompanyName { get; set; } = null;

        //Sorting
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;

        //Pagination
        public int  PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 1000000000;
    }
}
