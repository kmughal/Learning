namespace TfL.Ruc.Common.Functional
{
    public abstract class Option<T>
    {
        public static implicit operator Option<T>(T content)
        {
            return new Some<T>(content);
        }

        public static implicit operator Option<T>(None value)
        {
            return new None<T>();
        }
    }

    public class Some<T> : Option<T>
    {
        public T Content { get; }

        public Some(T content)
        {
            Content = content;
        }

        public static implicit operator T(Some<T> @object)
        {
            return @object.Content;
        }
    }

    public class None
    {
        private None() { }

        public static None Value = new None();
    }

    public class None<T> : Option<T>
    {

    }
}
