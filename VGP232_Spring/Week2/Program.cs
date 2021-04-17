using System;

namespace Week2
{
    class Program
    {
        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Size
        }

        public enum Fruits
        {
            Banana,
            Apple,
            Watermelon,
            Size
        }

        static void RunningSum(int left, ref int total)
        {
            total += left;
        }

        static void OutSum(int left, int right, out int total)
        {
            total = left + right;
        }

        static int Sum(int[] numbers)
        {
            int total = 0;
            for (int i = 0; i < numbers.Length; ++i)
            {
                total += numbers[i];
            }
            return total;
        }

        static int SumParams(params int[] numbers)
        {
            int total = 0;
            for (int i = 0; i < numbers.Length; ++i)
            {
                total += numbers[i];
            }
            return total;
        }

        // Optionals
        static void PrintAName(string name = "Default")
        {
            Console.WriteLine(name);
        }

        // Named Parameters
        static void SetConfiguration(bool fullScreenEnable, ref int width, out int height)
        {
            height = 5;
            Console.WriteLine("Configuration set.");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Fruits myFruit = Fruits.Apple;

            string myStringFruit = "Apple";

            try
            {
                Fruits myFruit2 = (Fruits)Enum.Parse(typeof(Fruits), myStringFruit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot convert this value into a Fruit");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Finishing...");
            }
           

            int total = 3;
            RunningSum(7, ref total);
            Console.WriteLine(total);

            int total2;
            OutSum(4, 6, out total2);
            Console.WriteLine("Out Total:{0}", total2);

            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Sum(Numbers);

            SumParams(1, 2, 3, 4, 5, 6, 7, 8);

            PrintAName();
            PrintAName("Ian");

            SetConfiguration(height: out total2, fullScreenEnable: false, width: ref total);
        }
    }
}
