namespace Cumulocity.SDK.Client.Rest.Representation
{
    public class PageStatisticsRepresentation
    {
        private int pageSize;

        public PageStatisticsRepresentation()
        {
        }

        public PageStatisticsRepresentation(int pageSize)
        {
            this.pageSize = pageSize;
        }

        public virtual int? TotalPages { set; get; }


        public virtual int PageSize
        {
            set => pageSize = value;
            get => pageSize;
        }


        public virtual int CurrentPage { set; get; }
    }
}