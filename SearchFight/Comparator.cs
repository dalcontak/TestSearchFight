using System.Collections.Generic;
using System.Linq;
using SearchFight.Items;
using SearchFight.SearchEngines;

namespace SearchFight
{
    public class Comparator
    {
        private readonly IList<SearchResult> _results;

        public Comparator(IList<SearchResult> results)
        {
            _results = results;
        }

        public Dictionary<ISearchEngine, long> ResultsOf(SearchItem item)
        {
            var resultsBySearchEngine = new Dictionary<ISearchEngine, long>();
            
            var resultsOfItem = _results.Where(r => r.SearchItem.Equals(item)).ToList();

            var searchEngines = resultsOfItem.Select(r => r.SearchEngine).Distinct();

            foreach (var engine in searchEngines)
            {
                var totalHits = resultsOfItem.Where(r => r.SearchEngine.Equals(engine))
                    .Sum(itemResult => itemResult.NumberResultsFound);
                resultsBySearchEngine.Add(engine, totalHits);
            }

            return resultsBySearchEngine;
        }

        public SearchItem WinnerInSearchEngine(ISearchEngine searchEngine)
        {
            var resultsBySearchEngine = _results.Where(r => r.SearchEngine.Equals(searchEngine)).ToList();

            return resultsBySearchEngine.OrderByDescending(r => r.NumberResultsFound).First().SearchItem;
        }

        public SearchItem WinnerSearchItem()
        {
            var searchItems = _results.Select(r => r.SearchItem).Distinct().ToList();
            var totalResultsBySearchItem = new Dictionary<SearchItem, long>();

            foreach (var searchItem in searchItems)
            {
                totalResultsBySearchItem.Add(searchItem,
                    ResultsOf(searchItem).Sum(r => r.Value));
            }

            return totalResultsBySearchItem.OrderByDescending(r => r.Value).First().Key;
        }
    }
}