using System;

namespace md.Services.ViewModels
{
    public class PagedResultBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalResults / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
