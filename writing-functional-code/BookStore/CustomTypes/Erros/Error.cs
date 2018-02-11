namespace writing_functional_code.BookStore.CustomTypes.Erros
{
    public abstract class Error<T>
    {
        public T Content {get;}

        protected Error(T content) {
            Content = content;
        }
        
    }
}