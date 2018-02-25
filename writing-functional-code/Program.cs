using System.Collections.Generic;

namespace writing_functional_code {

    using System;
    using writing_functional_code.Extensions;
    using writing_functional_code.Models;
    using static System.Console;
    using Unit = System.ValueTuple;
    using System.Linq;
    using FuntionalApproach.OptionalValues;

    class Author {
        public NonNullValue<string> Name { get; }
        public DateTime DateOfPublish { get; }

        private Author(NonNullValue<string> name, DateTime dateOfPublish) {
            Name = name;
            DateOfPublish = dateOfPublish;
        }

        public static Author Create(NonNullValue<string> name, DateTime dateOfPublish) => new Author(name, dateOfPublish);
    }

    class NonNullValue<T> {
        public T Content { get; }

        private NonNullValue(T content) {
            if (content == null) {
                throw new NullReferenceException(nameof(content));
            }
            Content = content;
        }

        public static NonNullValue<T> Create(T content) => new NonNullValue<T>(content);

    }

    class Program {
        static void Main(string[] args) {
            var authors = new List<Option<Author>>();
            for (var i = 1; i < 100; i++)
                authors.Add(new Some<Author>(Author.Create(NonNullValue<string>.Create($"Author {i}"), DateTime.Now.AddDays(i))));

            // search example
            var item = authors
                .Where(z => z.Content.Name.Content == "Khurram")
                .DefaultIfEmpty(new Option<Author>())
                .Single();

            try {
                var addedAuthor = item.Match(z => z, () => {
                    var newAuthor = Author.Create(NonNullValue<string>.Create("Khurram"), DateTime.Now);
                    authors.Add(new Some<Author>(newAuthor));
                    return newAuthor;
                });
                WriteLine($"author:{addedAuthor.Name.Content},Date of publish:{addedAuthor.DateOfPublish}");
            } catch (Exception err) {
                WriteLine(err.Message);
            }

            // Map , Bind examples

            foreach (int v in new [] { 3, 4, 5, 6, 7, 8, 9 }.Map1(i => i * i)) {
                WriteLine($"Square value:{v}");
            }

            var name = new Option<string>("Khurram Shahzad");
            Func<string, string> message = m => $"welcome {m}";
            WriteLine(name.Map1(message).Content);

            var _ = None.Instance;
            WriteLine(_.Map1(message).Content);

            // For Each example
            new [] { 3, 4, 5, 6, 7, 8, 9 }
                .Map1(i => i * i)
                .ForEach(WriteLine);

            // Bind example
            WriteLine($"Your age is : {getAge().Content}");
            Age getAge() =>
                "Input age:"
                .Prompt()
                .Parse()
                .Bind(Age.Of)
                .Match(x => x, () => getAge());

        }

        static void FunctionalProgrammingExamples() {
            try {
                var book = BookStore.Models.Book.Create(null, DateTime.Now);
                Write($"{book.Name} was published in {book.PublishDate.ToString()}");
            } catch (ArgumentNullException nullError) {
                WriteLine(nullError.Message);
            }

            "".ToTime(() => {
                var age1 = Age.Create(12);
                var age2 = Age.Create(23);

                WriteLine($"{age1}>{age2}");
                WriteLine($"{age2} >14");
                return Unit.Create();
            });
            getMessage();
            string getMessage() {
                return "hello";
            }

            try {
                var r = TestOption().Match(
                    (name) => $"Greetings from {name}",
                    () =>
                    throw new System.NullReferenceException("REPLY IS NULL OK")
                );
                WriteLine("r:" + r);

            } catch (Exception e) {
                WriteLine(e.Message);
            }

            try {

                // Parse value example
                WriteLine("232".Parse().Match((i) => i, () => 0));
                // Parse example with throw exception
                WriteLine("45".Parse().Match(i => i, () =>
                    throw new InvalidCastException("fail to type cast ...")));
                // Map example
                // foreach (var item in new [] { "One", "Two", "Three", "Double Fees" }.Map(x => $"{x} has {x.Length} characters.")) {
                //     WriteLine(item);
                // }
            } catch (Exception err) {
                WriteLine(err.Message);
            }

            // Map example

            var listOfOptions = new List<Option<string>>();
            listOfOptions.Add(None.Instance);
            listOfOptions.Add(new Some<string>("Good boy 1"));
            listOfOptions.Add(None.Instance);
            listOfOptions.Add(new Some<string>("Good boy 2"));
            listOfOptions.Add(None.Instance);
            listOfOptions.Add(new Some<string>("Good boy 3"));

            foreach (var item in listOfOptions.Map(y => y.Content.Replace(" ", "___"))) {
                WriteLine(item);
            }

        }
        static Option<string> TestOption() {
            return None.Instance;
        }

        static string NullReferenceExample(string value) =>
            value ??
            throw new NullReferenceException();

        static int ToInteger(string value) =>
            int.TryParse(value, out int result) ?
            result :
            throw new InvalidProgramException(nameof(value));
    }

    public class Age {
        public int Content { get; }
        private Age(int content) {
            if (!IsAgeValid(content)) {
                throw new ArgumentException(nameof(content));
            }
            Content = content;
        }

        private bool IsAgeValid(int age) =>
            true;

        public static Age Create(int age) => new Age(age);

        public static Option<Age> Of(int age) => new Some<Age>(Age.Create(age));

    }

}

namespace FuntionalApproach {
    namespace OptionalValues {
        using System.Linq;
        using System;
        using Unit = System.ValueTuple;
        using static System.Console;

        public class Some<T> {
            public T Content { get; }
            public Some(T content) {
                if (content == null) {
                    throw new System.NullReferenceException();
                }
                Content = content;
            }
        }

        public class None {
            public static None Instance = new None();

        }

        public class Option<T> {
            public T Content { get; }
            private readonly bool hasValue;

            public Option(T content) {
                hasValue = true;
                Content = content;
            }

            public Option() {

            }

            public static implicit operator Option<T>(Some<T> value) => new Option<T>(value.Content);

            public static implicit operator Option<T>(None _) => new Option<T>();

            public R Match<R>(System.Func<T, R> some, System.Func<R> none) =>
            hasValue ? some(Content) : none();

        }

        /*
             Option = Some<T> | None;

             Map: a function that takes a structure and a function which then is applied to the inner structure each item.
             public static IEnumerable<R> Map<T,R>(this IEnumerable<T> sequence , System.Func<T,R> func) {
                 foreach(var item in sequence) {
                     yield return func(item);
                 }
             }
         */

        public static class OptionExtensions {
            public static Option<int> Parse(this string value) {
                int result;
                if (int.TryParse(value, out result)) {
                    return new Some<int>(result);
                }
                return None.Instance;
            }

            public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<Option<T>> sequence, System.Func<Option<T>, TResult> func) {
                foreach (var item in sequence) {
                    yield return item.Match(v => func(item), () => default(TResult));
                }
            }

            public static R Map<T, R>(this Option<T> item, System.Func<T, R> func) {
                // func(item.Content)
                return item.Match(v => func(v), () => default(R));
            }

            public static System.Func<Unit> ToFunction(this System.Action action) {
                return () => { action(); return Unit.Create(); };
            }

            public static IEnumerable<TResult> Map1<TSource, TResult>(this IEnumerable<TSource> sequence, Func<TSource, TResult> func) {
                foreach (TSource item in sequence) {
                    yield return func(item);
                }
            }

            public static Option<TResult> Map1<TSource, TResult>(this Option<TSource> some, Func<TSource, TResult> func) =>
                new Some<TResult>(func(some.Content));

            public static Option<TResult> Map1<TSource, TResult>(this None _, Func<TSource, TResult> func) =>
                None.Instance;

            public static IEnumerable<Unit> ForEach<TSource>(this IEnumerable<TSource> sequence, Action<TSource> action) =>
                sequence.Map1((_) => {
                    action(_);
                    return Unit.Create();
                }).ToList();

            public static Option<TResult> Bind<TSource, TResult>(this Option<TSource> opt, Func<TSource, Option<TResult>> func) =>
                opt.Match(v =>(func(v)), () => None.Instance);

            public static string Prompt(this string message) {
                WriteLine(message);
                return ReadLine();
            }
        }
    }
}