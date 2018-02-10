namespace TfL.Ruc.Common.Functional
{
    using System;
    using System.Collections.Generic;

    public static class OptionsExtensions
    {
        public static Option<TResult> Map<T, TResult>(this IEnumerable<T> sequence, Func<Option<T>, TResult> map)
        {
            if (sequence is Some<T>)
            {
                var some = (Some<T>)sequence;
                return map(some);
            }

            return None.Value;
        }

        public static Option<T> Reduce<T>(this Option<T> option, T value)
        {
            if (option is Some<T>)
            {
                var some = (Some<T>)option;
                return value;
            }

            return None.Value;
        }

        public static Option<T> Reduce<T>(this Option<T> option, Func<T> func)
        {
            if (option is Some<T>)
            {
                var some = (Some<T>)option;
                return func();
            }

            return None.Value;

        }

        public static Option<T> When<T>(this T value, Func<T, bool> prediciate)
        {
            return prediciate(value) ? (Option<T>)value : None.Value;
        }
    }
}
