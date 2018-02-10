namespace TfL.Ruc.Common.Functional
{
    public class Either<TLeft, TRight>
    {
        public static implicit operator Either<TLeft, TRight>(TLeft @object)
        {
            return new Left<TLeft, TRight>(@object);
        }

        public static implicit operator Either<TLeft, TRight>(TRight @object)
        {
            return new Right<TLeft, TRight>(@object);
        }
    }

    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        public TLeft Content { get; }

        public Left(TLeft content)
        {
            Content = content;
        }

        public static implicit operator TLeft(Left<TLeft, TRight> @object)
        {
            return @object.Content;
        }
    }

    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        public TRight Content { get; }
        public Right(TRight content)
        {
            Content = content;
        }

        public static implicit operator TRight(Right<TLeft, TRight> @object)
        {
            return @object.Content;
        }
    }
}
