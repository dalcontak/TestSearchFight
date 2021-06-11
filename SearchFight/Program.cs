using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchFight.Items;
using SearchFight.SearchEngines;

namespace SearchFight
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var searchItems = SearchItem.Parse(args);

            var searchEngines = new List<ISearchEngine>
            {
                new SearchEngineBing(), 
                new SearchEngineYahooSearch()
            };

            var searchExecutor = new SearchExecutor(searchEngines.ToArray());

            var results = await searchExecutor.RunSearchesAsync(searchItems);

            var comparator = new Comparator(results);
            
            foreach (var item in searchItems)
            {
                Console.Write(item.LanguageName + ": ");
                Console.WriteLine(comparator.ResultsOf(item).Aggregate(
                    "", (a, b) => a + $"{b.Key.Name}: {b.Value} "));
            }

            foreach (var searchEngine in searchEngines)
            {
                Console.WriteLine($"{searchEngine.Name} winner: {comparator.WinnerInSearchEngine(searchEngine).LanguageName}");
            }
            
            Console.WriteLine($"Total winner: {comparator.WinnerSearchItem().LanguageName}");
        }
    }
}