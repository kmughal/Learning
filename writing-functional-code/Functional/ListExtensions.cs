namespace TfL.Ruc.Common.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtensions
    {
        public static IEnumerable<TResult> Flatten<T,TResult>(this IEnumerable<T> sequence, Func<T, Option<TResult>> map)
        {
            return sequence.Select(map)
                .OfType<Some<TResult>>()
                .Select(x => (TResult) x);
        }

        public static Option<T> DefaultIfNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence.Where(predicate)
                .Select<T, Option<T>>(x => x)
                .DefaultIfEmpty(None.Value)
                .First();
        }
    } 
}
