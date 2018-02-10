namespace TfL.Ruc.Common.Functional
{
    using System;

    public static class EitherExtensions
    {
        public static Either<TLeft, TRightNew> Map<TLeft, TRight, TRightNew>(
            this Either<TLeft, TRight> either, Func<TRight, TRightNew> map)
        {
            if (either is Right<TLeft, TRight>)
            {
                var right = (Right<TLeft, TRight>) either;
                return map(right);
            }
            return (TLeft)(Left<TLeft, TRight>)either;
        }


        public static Either<TLeft, TRightNew> Map<TLeft, TRight, TRightNew>(
            this Either<TLeft, TRight> either, Func<TRight, Either<TLeft, TRightNew>> map)
        {
            if (either is Right<TLeft, TRight>)
            {
                var right = (Right<TLeft, TRight>)either;
                return map(right);
            }
            return (TLeft)(Left<TLeft, TRight>)either;
        }


        public static TRight Reduce<TLeft, TRight>(this Either<TLeft, TRight> either,Func<TLeft,TRight> map)
        {
            if (either is Right<TLeft, TRight>)
            {
                return (Right<TLeft, TRight>) either;
            }

            return map((TLeft)(Left<TLeft,TRight>)either);
        }


        public static Either<TLeft, TRight> Reduce<TLeft, TRight>(this Either<TLeft, TRight> either,
            Func<TLeft, TRight> map,Func<TLeft,bool> when)
        {
            if (either is Left<TLeft, TRight>)
            {
                var left = (Left<TLeft, TRight>) either;
                if (when(left))
                {
                    return map(left);
                }
            }

            return either;
        }
    }
}
