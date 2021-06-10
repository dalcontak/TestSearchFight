using System.Collections.Generic;
using NUnit.Framework;
using SearchFight;
using SearchFight.Items;
using SearchFight.SearchEngines;
using UnitTestSearchFight.Stubs;

namespace UnitTestSearchFight
{
    public class CompareTests
    {
        private SearchItem _languageOne;
        private SearchItem _languageTwo;
        private SearchItem _languageThree;
        private ISearchEngine _searchEngineOne;
        private ISearchEngine _searchEngineTwo;
        private ISearchEngine _searchEngineThree;

        private Comparator _comparator;

        [SetUp]
        public void Setup()
        {
            _languageOne = new SearchItem("languageOne");
            _languageTwo = new SearchItem("languageTwo");
            _languageThree = new SearchItem("languageThree");
            _searchEngineOne = new SearchEngineStub("EngineOne");
            _searchEngineTwo = new SearchEngineStub("EngineTwo");
            _searchEngineThree = new SearchEngineStub("EngineThree");
        }

        [Test]
        public void CompareNumberOfResultsBySearchItem()
        {
            var results = new List<SearchResult>();
            results.Add(new SearchResult(_languageOne, _searchEngineOne, 1));
            results.Add(new SearchResult(_languageOne, _searchEngineTwo, 1));
            results.Add(new SearchResult(_languageTwo, _searchEngineOne, 1));
            _comparator = new Comparator(results);
            
            Assert.AreEqual(2, _comparator.ResultsOf(_languageOne).Count);
        }
    }
}