namespace writing_functional_code {

    using System;
    using static System.Console;

    class Program {
        static void Main(string[] args) {
            // WriteLine("Hello World!");
            // // Make your code more funcational.
            // NullReferenceExample("null");

            // Currency cur1 = Currency.Gbp, cur2 = Currency.USD;
            // Write($"{cur1} is {(cur1 == cur2 ? "equal" : "not equal")}  to {cur2}");

            try {
                var book = BookStore.Models.Book.Create(null, DateTime.Now);
                Write($"{book.Name} was published in {book.PublishDate.ToString()}");
            } catch (ArgumentNullException nullError) {
                WriteLine(nullError.Message);
            }
            Read();
        }

        static string NullReferenceExample(string value) =>
            value ??
            throw new NullReferenceException();

        static int ToInteger(string value) =>
            int.TryParse(value, out int result) ?
            result :
            throw new InvalidProgramException(nameof(value));
    }

    public class Movie {

        public string Name { get; }
        public int ReleaseYear { get; }

        private Movie(string name, int releaseYear) {
            Name = name;
            ReleaseYear = releaseYear;
        }

        public Movie CreateMovieInstance(string name, int releaseYear) =>
            new Movie(name, releaseYear);

    }

    public sealed class Currency : IEquatable<Currency> {
        public string Symbol { get; }
        private Currency(string symbol) {
            Symbol = symbol;
        }
        public static Currency USD { get; } = new Currency("UDS");
        public static Currency Gbp { get; } = new Currency("£");
        public static Currency Euro { get; } = new Currency("Euro");

        public override bool Equals(Object obj) =>
        this.Equals(obj);

        public static bool operator ==(Currency a, Currency b) =>
        object.ReferenceEquals(a, null) ? object.ReferenceEquals(b, null) : a.Equals(b);

        public static bool operator !=(Currency a, Currency b) =>
        !a.Equals(b);

        public override int GetHashCode() =>
        Symbol.GetHashCode();

        public override string ToString() => Symbol;

        public bool Equals(Currency other) =>
        other != null && other.Symbol == Symbol;
    }

    public static class ExtensionMethods {
        public static TResult Map<T, TResult>(this T input, Func<T, TResult> map) =>
            map(input);
    }
}

/* 
   Pure Functions


 */