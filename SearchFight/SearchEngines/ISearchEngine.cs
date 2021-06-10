using System.Threading.Tasks;
using SearchFight.Items;

namespace SearchFight.SearchEngines
{
    /// <summary>
    /// Interface for a search engine, define a name and a function for search hits of item search.
    /// </summary>
    public interface ISearchEngine
    {
        string Name { get; }
        
        Task<SearchResult> SearchNumberHitsAsync(SearchItem searchItem);
    }
}