using System.Threading.Tasks;
using SearchFight.Items;
using SearchFight.SearchEngines;

namespace UnitTestSearchFight.Stubs
{
    public class SearchEngineStub : ISearchEngine
    {
        public string Name { get; }

        public SearchEngineStub(string nameEngine)
        {
            Name = nameEngine;
        }
        
        public Task<SearchResult> SearchNumberHitsAsync(SearchItem searchItem)
        {
            throw new System.NotImplementedException();
        }
    }
}