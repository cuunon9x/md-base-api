using System.Collections.Generic;

namespace md.Services.ViewModels
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Data { set; get; }
    }
}
