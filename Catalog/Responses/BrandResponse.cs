namespace Catalog.Responses
{
    public record BrandResponse
    {
        public string Id
        {
            get;
            init;
        } = string.Empty;

        public string Name
        {
            get;
            set;
        } = string.Empty;
    }
}
