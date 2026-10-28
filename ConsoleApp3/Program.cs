using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayFilterDelegates
{
    public delegate bool NumberFilter(int number);

    class Program
    {
        public static bool IsEven(int n)
        {
            return n % 2 == 0;
        }

        public static bool IsOdd(int n)
        {
            return n % 2 != 0;
        }

        public static bool IsPrime(int n)
        {
            if (n < 2)
            {
                return false;
            }

            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsFibonacci(int n)
        {
            if (n < 0)
            {
                return false;
            }

            if (n == 0 || n == 1)
            {
                return true;
            }

            int a = 0;
            int b = 1;

            while (b < n)
            {
                int temp = a + b;
                a = b;
                b = temp;
            }

            return b == n;
        }

        public static int[] FilterArray(int[] array, NumberFilter filter)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < array.Length; i++)
            {
                if (filter(array[i]))
                {
                    result.Add(array[i]);
                }
            }

            return result.ToArray();
        }

        public static void PrintArray(string title, int[] array)
        {
            Console.Write(title + ": ");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 21, 22 };

            PrintArray("Початковий масив", numbers);
            Console.WriteLine();

            NumberFilter evenDelegate = IsEven;
            NumberFilter oddDelegate = IsOdd;
            NumberFilter primeDelegate = IsPrime;
            NumberFilter fibDelegate = IsFibonacci;

            int[] evenNumbers = FilterArray(numbers, evenDelegate);
            int[] oddNumbers = FilterArray(numbers, oddDelegate);
            int[] primeNumbers = FilterArray(numbers, primeDelegate);
            int[] fibNumbers = FilterArray(numbers, fibDelegate);

            PrintArray("Парні числа", evenNumbers);
            PrintArray("Непарні числа", oddNumbers);
            PrintArray("Прості числа", primeNumbers);
            PrintArray("Числа Фібоначчі", fibNumbers);
        }
    }
}