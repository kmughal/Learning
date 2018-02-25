namespace writing_functional_code.Models
{
    using System;

    public class Age
    {
        public int Value {get;}

        private Age(int value) {
            Value = value;
        }

        private static bool IsValid(int value) => 
                value > 1 && value <= 80;


        public static bool operator > (Age @object1,Age @object2) => @object1.Value > @object2.Value;
        
        public static bool operator < (Age @object1,Age @object2) => @object1.Value < @object2.Value;

        public static bool operator > (Age @object1,int value) => @object1 > new Age(value);
        
        public static bool operator < (Age @object1,int value) => @object1 < new Age(value);


        public static Age Create(int value) => IsValid(value) ? new Age(value) : throw new ArgumentException(nameof(value));
    }
}