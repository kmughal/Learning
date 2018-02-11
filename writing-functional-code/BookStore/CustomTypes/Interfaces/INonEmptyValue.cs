namespace writing_functional_code.BookStore.CustomTypes.Interfaces
{
       public interface INonEmptyValue<T> {
            T Content { get; }
        }
}