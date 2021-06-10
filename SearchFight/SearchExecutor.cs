using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SearchFight.Items;
using SearchFight.SearchEngines;

namespace SearchFight
{
    /// <summary>
    /// Run the search for each item in each search engine.
    /// </summary>
    public class SearchExecutor
    {
        private readonly IList<ISearchEngine> _searchEngines;

        /// <summary>
        /// Initialize with search engines.
        /// </summary>
        /// <param name="searchEngines">Search engines where items are searched.</param>
        /// <exception cref="ConstraintException">At least two search engines.</exception>
        public SearchExecutor(params ISearchEngine[] searchEngines)
        {
            if (searchEngines.Length < 2)
            {
                throw new ConstraintException("You need at least two search engines.");
            }

            _searchEngines = searchEngines.ToList();
        }

        /// <summary>
        /// Run all searches in all search engines.
        /// </summary>
        /// <param name="searchItems">Items to search.</param>
        /// <returns>Return search results.</returns>
        public async Task<IList<SearchResult>> RunSearchesAsync(IList<SearchItem> searchItems)
        {
            var searchResults = new List<SearchResult>();
            var searches = new List<Task<SearchResult>>();
            
            foreach (var searchItem in searchItems)
            {
                foreach (var searchEngine in _searchEngines)
                {
                    searches.Add(searchEngine.SearchNumberHitsAsync(searchItem));
                }
            }
            
            return await Task.WhenAll(searches);
        }
    }
}