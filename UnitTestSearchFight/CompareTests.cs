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
        public void CompareNumberOfResultsByTwoSearchItemsTest()
        {
            var results = new List<SearchResult>();
            results.Add(new SearchResult(_languageOne, _searchEngineOne, 1));
            results.Add(new SearchResult(_languageOne, _searchEngineTwo, 1));
            results.Add(new SearchResult(_languageTwo, _searchEngineOne, 1));
            _comparator = new Comparator(results);
            
            Assert.AreEqual(2, _comparator.ResultsOf(_languageOne).Count);
            Assert.AreEqual(1, _comparator.ResultsOf(_languageTwo).Count);
        }

        [Test]
        public void DetermineWinnerLanguageInSearchEngineTest()
        {
            var results = new List<SearchResult>();
            results.Add(new SearchResult(_languageOne, _searchEngineOne, 5));
            results.Add(new SearchResult(_languageOne, _searchEngineTwo, 8));
            results.Add(new SearchResult(_languageTwo, _searchEngineOne, 10));
            results.Add(new SearchResult(_languageTwo, _searchEngineTwo, 2));
            _comparator = new Comparator(results);
            
            Assert.AreEqual(_languageTwo.LanguageName,
                _comparator.WinnerInSearchEngine(_searchEngineOne).LanguageName);
        }

        [Test]
        public void DetermineWinnerSearchItemInAllSearchEnginesTest()
        {
            var results = new List<SearchResult>();
            results.Add(new SearchResult(_languageOne, _searchEngineOne, 50));
            results.Add(new SearchResult(_languageOne, _searchEngineTwo, 45));
            results.Add(new SearchResult(_languageTwo, _searchEngineOne, 80));
            results.Add(new SearchResult(_languageTwo, _searchEngineTwo, 4));
            _comparator = new Comparator(results);

            Assert.AreEqual(_languageOne.LanguageName, _comparator.WinnerSearchItem().LanguageName);
        }
    }
}