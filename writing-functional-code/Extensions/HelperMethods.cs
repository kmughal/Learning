namespace writing_functional_code.Extensions {
    using System;
    using System.Diagnostics;
    using Unit = System.ValueTuple;
    using static System.Console;
    public static class HelperMethods {
        public static Func<Unit> ToFunc(this Action action) =>
            () => { action(); return Unit.Create(); };

        public static Func<T, Unit> ToFunc<T>(this Action action) =>
            (t) => { action(); return Unit.Create(); };

        public static T ToTime<T>(this string op , Func<T> func) {
            var sw = new Stopwatch();
            sw.Start();
            WriteLine($"Started : {DateTime.Now.ToString()}");
            var t = func();
            sw.Stop();
            WriteLine($"End : {DateTime.Now.ToString()}");
            return t;
        }
    }
}