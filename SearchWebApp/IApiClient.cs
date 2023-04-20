using Common.Shared;

namespace SearchWebApp
{
    public interface IApiClient
    {
        public Task<SearchWord> GetSearchData(string input);
        public Task<string> GetFormattedData(string formatType);
    }
}
