namespace writing_functional_code.BookStore.CustomTypes {

    using System;
    using Interfaces;
    using TfL.Ruc.Common.Functional;
    using writing_functional_code.BookStore.CustomTypes.Erros;

    public class NonEmptyStringValue : INonEmptyValue<string> {

                public string Content { get; }

                private NonEmptyStringValue(string content) {
                    Content = content;
                }

                public static Either<Exception,NonEmptyStringValue> Create(string content)  {

                    if (string.IsNullOrEmpty(content)) {
                        throw new ArgumentNullException("content is null, so can't intialize parameter.");
                    }
                    return  new NonEmptyStringValue(content);
                }

                public static implicit operator string(NonEmptyStringValue @object) => @object.Content;
}
}