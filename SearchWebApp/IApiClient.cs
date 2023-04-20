using Common.Shared;

namespace SearchWebApp
{
    public interface IApiClient
    {
        public SearchWord GetSearchData(string input);
        public string GetFormattedData(string formatType);
    }
}
