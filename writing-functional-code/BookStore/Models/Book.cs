namespace writing_functional_code.BookStore.Models {

    using System;
    using TfL.Ruc.Common.Functional;
    using writing_functional_code.BookStore.CustomTypes.Erros;
    using writing_functional_code.BookStore.CustomTypes.Interfaces;
    using writing_functional_code.BookStore.CustomTypes;

    public class Book {
        public string Id { get; }
        public string Name { get; }
        public DateTime PublishDate { get; }

        private Book(string name, DateTime publishDate) {
            Id = Guid.NewGuid().ToString();
            Name = name;
            PublishDate = publishDate;
        }

        public static Book Create(string name, DateTime publishDate) {
            name = NonEmptyStringValue.Create(name)
                .Map(y => y.Content)
                .Reduce(z =>
                    throw z);
            return new Book(name, publishDate);

        }
    }
}