using System;

class Program
{
    static void Main()
    {
        int[] arr1 = { 1, 2, 4, 5, 7, 2, 8 };
        int[] arr2 = { 2, 5, 8, 9, 2, 10 };
        int[] result = arr1.Intersect(arr2).ToArray();

        Console.WriteLine("Спільні елементи без повторень: " + string.Join(" ", result));
    }
}

/*class Program
{
    static void Main()
    {
        Console.Write("Введіть речення: ");
        string text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text)) return;

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            char[] charArray = words[i].ToCharArray();
            Array.Reverse(charArray);
            words[i] = new string(charArray);
        }

        string result = string.Join(" ", words);
        Console.WriteLine("Результат перевороту: " + result);
    }
}*/

/*class Program
{
    static void Main()
    {
        Console.Write("Введіть речення: ");
        string text = Console.ReadLine();

        string vowels = "aeiouyаеєиіїоуюяёыэюAEIOUYАЕЄИІЇОУЮЯЁЫЭЮ";
        int count = 0;

        foreach (char ch in text)
        {
            if (vowels.Contains(ch))
            {
                count++;
            }
        }

        Console.WriteLine($"Кількість голосних літер: {count}");
    }
} */