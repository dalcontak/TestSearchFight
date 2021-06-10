using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Items
{
    /// <summary>
    /// Define an search item.
    /// </summary>
    public class SearchItem
    {
        /// <summary>
        /// Programming language name to be searched.
        /// </summary>
        public string LanguageName { get; }

        /// <summary>
        /// Initialize a SearchItem with the programming language name.
        /// </summary>
        /// <param name="languageName">Name of programming language.</param>
        /// <exception cref="ArgumentException">Exception for a invalid value of languageName.</exception>
        public SearchItem(string languageName)
        {
            if (languageName == null || string.IsNullOrEmpty(languageName))
            {
                throw new ArgumentException("Language name is not valid.");
            }

            LanguageName = languageName;
        }

        /// <summary>
        /// Get a list of SearchItem from string list.
        /// </summary>
        /// <param name="items">String list.</param>
        /// <returns>List of search items.</returns>
        public static IList<SearchItem> Parse(string[] items)
        {
            return items.Select(item => new SearchItem(item)).ToList();
        }

        protected bool Equals(SearchItem other)
        {
            return LanguageName == other.LanguageName;
        }
        
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            
            return Equals((SearchItem) obj);
        }
        
        public override int GetHashCode()
        {
            return (LanguageName != null ? LanguageName.GetHashCode() : 0);
        }
    }
}