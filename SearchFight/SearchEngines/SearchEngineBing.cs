using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SearchFight.Items;

namespace SearchFight.SearchEngines
{
    public class SearchEngineBing : SearchEngine
    {
        private readonly HttpClient _httpClient;
        public override string Name => "Bing";

        private string _url => "https://www.bing.com/search?qs=EP&sp=6&q=";

        public SearchEngineBing(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task<SearchResult> SearchAsync(SearchItem itemSearch)
        {
            var resultSearch = await _httpClient.GetStringAsync(_url + itemSearch.LanguageName);
            
            var patternReg = new Regex(@"(?<=<span class=""sb_count"">)[\w,. ]+(?=<\/span>)", RegexOptions.IgnoreCase);
            var resultMatches = patternReg.Matches(resultSearch);
            var contentFound = resultMatches[0].Value;

            var patternNumbers = new Regex(@"\d+");
            var numbersResult = patternNumbers.Matches(contentFound);
            var numberMatch = numbersResult.Aggregate("", (a, b) => a + b.Value);
            
            if (int.TryParse(numberMatch, out var numberResult))
            {
                return new SearchResult(itemSearch, this, numberResult);
            }
            
            throw new InvalidCastException("result search is not valid.");
        }
    }
}