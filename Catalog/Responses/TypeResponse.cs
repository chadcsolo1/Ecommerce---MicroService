namespace Catalog.Responses
{
    public record TypeResponse
    {
        public string Id
        {
            get;
            init;
        } = string.Empty;
        public string Name
        {
            get;
            init;
        } = string.Empty;
    }
}
