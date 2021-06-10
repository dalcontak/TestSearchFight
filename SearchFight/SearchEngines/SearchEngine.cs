using System;
using System.Threading.Tasks;
using SearchFight.Items;

namespace SearchFight.SearchEngines
{
    public abstract class SearchEngine : ISearchEngine
    {
        public abstract string Name { get; }
        
        public async Task<SearchResult> SearchNumberHitsAsync(SearchItem itemSearch)
        {
            if (itemSearch == null)
            {
                throw new ArgumentException("Item search null", nameof(itemSearch));
            }

            return await SearchAsync(itemSearch);
        }

        protected abstract Task<SearchResult> SearchAsync(SearchItem itemSearch);
    }
}