using System;
using SearchFight.SearchEngines;

namespace SearchFight.Items
{
    /// <summary>
    /// Represent the result of a search in a search engine.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Searched item.
        /// </summary>
        public SearchItem SearchItem { get; }
        /// <summary>
        /// Used search engine.
        /// </summary>
        public ISearchEngine SearchEngine { get; }
        /// <summary>
        /// Number of items found.
        /// </summary>
        public int NumberResultsFound { get; }

        /// <summary>
        /// Initialize an object SearchResult for search item and search engine.
        /// </summary>
        /// <param name="searchItem">Searched item.</param>
        /// <param name="searchEngine">Used search engine.</param>
        /// <param name="numberItemsFound">Number of item found, if the number is negative then there were
        /// problems in the search.</param>
        /// <exception cref="ArgumentNullException">Exception throw by arguments searchItem and searchEngineç
        /// nulls.</exception>
        public SearchResult(SearchItem searchItem, ISearchEngine searchEngine, int numberItemsFound)
        {
            SearchItem = searchItem ?? throw new ArgumentNullException(nameof(searchItem));
            SearchEngine = searchEngine ?? throw new ArgumentNullException(nameof(searchEngine));
            NumberResultsFound = numberItemsFound;
        }
    }
}