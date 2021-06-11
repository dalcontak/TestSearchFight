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

        public SearchEngineBing()
        {
            _httpClient = new HttpClient();
        }

        protected override async Task<SearchResult> SearchAsync(SearchItem itemSearch)
        {
            var resultSearch = await _httpClient.GetStringAsync(_url + itemSearch.LanguageName);
            
            var numberMatch = NumberMatch(resultSearch);

            return new SearchResult(itemSearch, this, numberMatch);
        }

        private long NumberMatch(string resultSearch)
        {
            var patternReg =
                new Regex(@"(?<=<span class=""sb_count"">)[\w,. ]+(?=<\/span>)", RegexOptions.IgnoreCase);
            long numberResult = 0;
            
            try
            {
                var resultMatches = patternReg.Matches(resultSearch);
                var contentFound = resultMatches[0].Value;

                var patternNumbers = new Regex(@"\d+");
                var numbersResult = patternNumbers.Matches(contentFound);
                var numberMatch = numbersResult.Aggregate("", (a, b) => a + b.Value);

                if (!long.TryParse(numberMatch, out numberResult))
                {
                    numberResult = -1;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error in Bing.NumberMatch: " + ex.Message);
                numberResult = -1;
            }

            return numberResult;
        }
    }
}