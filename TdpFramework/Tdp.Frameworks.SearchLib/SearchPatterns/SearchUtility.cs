using System;
using System.Linq;
using System.Linq.Expressions;

namespace Tdp.Frameworks.SearchLib.SearchPatterns
{
    using Tdp.Common;
    public class SearchUtility
    {
        public static IQueryable<T> SearchContain<T>(IQueryable<T> source, string propertyName, string phrase)
        {
            Expression<Func<T, bool>> express = PredicateBuilder.BuildContains<T>(propertyName, phrase);

            return source.Where(express.Compile()).AsQueryable();
        }
    }
}
