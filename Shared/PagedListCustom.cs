using X.PagedList;

namespace IstimAPI.Shared
{
    public class PagedListCustom<T> where T : class
    {
        public int TotalCount { get; set; }

        public IPagedList<T> Data { get; set; }
    }
}
