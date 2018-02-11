namespace writing_functional_code.BookStore.CustomTypes.Erros {
    public class InvalidParameterException : Error<string> {

        private InvalidParameterException(string message) : base(message) {

        }

        public static InvalidParameterException Create(string message) => new InvalidParameterException(message);

    }
}