namespace DrMeet.Api.Shared.PagedList
{

    public class PagedListInfo
    {
        private int _pageSize = 100;

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int PageNumber { get; set; }

        public int? PageSize
        {
            get => _pageSize;
            set
            {
                if (value != null)
                {
                    _pageSize = value.Value;
                }

            }
        }
    }
}
