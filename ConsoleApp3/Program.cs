using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemTasks
{
    class Program
    {
        public static bool IsPrime(int n)
        {
            if (n < 2)
            {
                return false;
            }

            for (int i = 2; i * i <= n; i++)
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

        public static void RunTask1()
        {
            Console.WriteLine("--- ЗАВДАННЯ 1: Генерація 100 чисел (прості та Фібоначчі у файли) ---");

            Random random = new Random();
            int totalCount = 100;
            int[] numbers = new int[totalCount];

            List<string> primeNumbers = new List<string>();
            List<string> fibonacciNumbers = new List<string>();

            for (int i = 0; i < totalCount; i++)
            {
                numbers[i] = random.Next(0, 200);

                if (IsPrime(numbers[i]))
                {
                    primeNumbers.Add(numbers[i].ToString());
                }

                if (IsFibonacci(numbers[i]))
                {
                    fibonacciNumbers.Add(numbers[i].ToString());
                }
            }

            string primesFilePath = "primes.txt";
            string fibonacciFilePath = "fibonacci.txt";

            File.WriteAllText(primesFilePath, string.Join(" ", primeNumbers), Encoding.UTF8);
            File.WriteAllText(fibonacciFilePath, string.Join(" ", fibonacciNumbers), Encoding.UTF8);

            Console.WriteLine("Статистика роботи додатку:");
            Console.WriteLine("Згенеровано чисел: " + totalCount);
            Console.WriteLine("Знайдено простих чисел: " + primeNumbers.Count + " (збережено у файл '" + primesFilePath + "')");
            Console.WriteLine("Прості числа: " + string.Join(", ", primeNumbers));
            Console.WriteLine("Знайдено чисел Фібоначчі: " + fibonacciNumbers.Count + " (збережено у файл '" + fibonacciFilePath + "')");
            Console.WriteLine("Числа Фібоначчі: " + string.Join(", ", fibonacciNumbers));
            Console.WriteLine();
        }

        public static int CountOccurrences(string text, string searchWord)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(searchWord))
            {
                return 0;
            }

            int count = 0;
            int index = 0;

            while ((index = text.IndexOf(searchWord, index, StringComparison.Ordinal)) != -1)
            {
                count++;
                index += searchWord.Length;
            }

            return count;
        }

        public static void RunTask2()
        {
            Console.WriteLine("--- ЗАВДАННЯ 2: Пошук та заміна слова у файлі ---");

            string filePath = "task2_text.txt";
            string defaultContent = "Ця програма створена на C#. Кожна хороша програма працює швидко, а ця програма ще й зручна.";
            File.WriteAllText(filePath, defaultContent, Encoding.UTF8);

            Console.WriteLine("Початковий вміст файлу '" + filePath + "':");
            Console.WriteLine(defaultContent);

            Console.Write("Введіть слово для пошуку (або натисніть Enter для 'програма'): ");
            string? searchWord = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchWord))
            {
                searchWord = "програма";
            }

            Console.Write("Введіть слово для заміни (або натисніть Enter для 'система'): ");
            string? replaceWord = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(replaceWord))
            {
                replaceWord = "система";
            }

            string content = File.ReadAllText(filePath, Encoding.UTF8);
            int replacementsCount = CountOccurrences(content, searchWord);
            string updatedContent = content.Replace(searchWord, replaceWord);

            File.WriteAllText(filePath, updatedContent, Encoding.UTF8);

            Console.WriteLine("\nСтатистика роботи додатку:");
            Console.WriteLine("Шукане слово: '" + searchWord + "'");
            Console.WriteLine("Слово для заміни: '" + replaceWord + "'");
            Console.WriteLine("Кількість виконаних замін: " + replacementsCount);
            Console.WriteLine("Оновлений вміст файлу:");
            Console.WriteLine(updatedContent);
            Console.WriteLine();
        }

        public static void RunTask3()
        {
            Console.WriteLine("--- ЗАВДАННЯ 3: Додаток «Модератор» ---");

            string defaultTextPath = "moderation_text.txt";
            string defaultWordsPath = "moderation_words.txt";

            File.WriteAllText(defaultTextPath, "test best rest car\nman telephone", Encoding.UTF8);
            File.WriteAllText(defaultWordsPath, "car telephone", Encoding.UTF8);

            Console.Write("Введіть шлях до файлу з текстом (Enter для '" + defaultTextPath + "'): ");
            string? textPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(textPath))
            {
                textPath = defaultTextPath;
            }

            Console.Write("Введіть шлях до файлу зі словами для модерації (Enter для '" + defaultWordsPath + "'): ");
            string? wordsPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(wordsPath))
            {
                wordsPath = defaultWordsPath;
            }

            if (!File.Exists(textPath) || !File.Exists(wordsPath))
            {
                Console.WriteLine("Один із вказаних файлів не знайдено.");
                Console.WriteLine();
                return;
            }

            string textContent = File.ReadAllText(textPath, Encoding.UTF8);
            string wordsContent = File.ReadAllText(wordsPath, Encoding.UTF8);

            Console.WriteLine("\nПочатковий текст:");
            Console.WriteLine(textContent);

            char[] separators = new char[] { ' ', '\t', '\r', '\n', ',', ';' };
            string[] forbiddenWords = wordsContent.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string moderatedText = textContent;
            for (int i = 0; i < forbiddenWords.Length; i++)
            {
                string word = forbiddenWords[i];
                string stars = new string('*', word.Length);
                moderatedText = moderatedText.Replace(word, stars);
            }

            File.WriteAllText(textPath, moderatedText, Encoding.UTF8);

            Console.WriteLine("\nРезультат після модерації:");
            Console.WriteLine(moderatedText);
            Console.WriteLine();
        }

        public static void RunTask4()
        {
            Console.WriteLine("--- ЗАВДАННЯ 4: Перевертання вмісту файлу ---");

            string defaultSourcePath = "source_to_reverse.txt";
            File.WriteAllText(defaultSourcePath, "Привіт, світ! Це тестовий файл для перевертання.", Encoding.UTF8);

            Console.Write("Введіть шлях до файлу (Enter для '" + defaultSourcePath + "'): ");
            string? sourcePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                sourcePath = defaultSourcePath;
            }

            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("Файл не знайдено: " + sourcePath);
                Console.WriteLine();
                return;
            }

            string originalContent = File.ReadAllText(sourcePath, Encoding.UTF8);
            char[] charArray = originalContent.ToCharArray();
            Array.Reverse(charArray);
            string reversedContent = new string(charArray);

            string reversedFilePath = "reversed_" + Path.GetFileName(sourcePath);
            File.WriteAllText(reversedFilePath, reversedContent, Encoding.UTF8);

            Console.WriteLine("Початковий вміст файлу: " + originalContent);
            Console.WriteLine("Перевернутий вміст (збережено у '" + reversedFilePath + "'): " + reversedContent);
            Console.WriteLine();
        }

        public static void RunTask5()
        {
            Console.WriteLine("--- ЗАВДАННЯ 5: Аналіз файлу зі 100000 цілих чисел ---");

            string sourceNumbersFile = "numbers_100000.txt";
            int totalNumbers = 100000;
            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(sourceNumbersFile, false, Encoding.UTF8))
            {
                for (int i = 0; i < totalNumbers; i++)
                {
                    int value = random.Next(-99999, 100000);
                    writer.WriteLine(value);
                }
            }

            string positiveFile = "positive_numbers.txt";
            string negativeFile = "negative_numbers.txt";
            string twoDigitFile = "two_digit_numbers.txt";
            string fiveDigitFile = "five_digit_numbers.txt";

            int positiveCount = 0;
            int negativeCount = 0;
            int twoDigitCount = 0;
            int fiveDigitCount = 0;

            using (StreamReader reader = new StreamReader(sourceNumbersFile, Encoding.UTF8))
            using (StreamWriter posWriter = new StreamWriter(positiveFile, false, Encoding.UTF8))
            using (StreamWriter negWriter = new StreamWriter(negativeFile, false, Encoding.UTF8))
            using (StreamWriter twoDigitWriter = new StreamWriter(twoDigitFile, false, Encoding.UTF8))
            using (StreamWriter fiveDigitWriter = new StreamWriter(fiveDigitFile, false, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out int number))
                    {
                        if (number > 0)
                        {
                            positiveCount++;
                            posWriter.WriteLine(number);
                        }
                        else if (number < 0)
                        {
                            negativeCount++;
                            negWriter.WriteLine(number);
                        }

                        int absNumber = Math.Abs(number);
                        if (absNumber >= 10 && absNumber <= 99)
                        {
                            twoDigitCount++;
                            twoDigitWriter.WriteLine(number);
                        }
                        else if (absNumber >= 10000 && absNumber <= 99999)
                        {
                            fiveDigitCount++;
                            fiveDigitWriter.WriteLine(number);
                        }
                    }
                }
            }

            Console.WriteLine("Статистика аналізу файлу '" + sourceNumbersFile + "' (" + totalNumbers + " чисел):");
            Console.WriteLine("1. Кількість додатних чисел: " + positiveCount + " (файл: '" + positiveFile + "')");
            Console.WriteLine("2. Кількість від'ємних чисел: " + negativeCount + " (файл: '" + negativeFile + "')");
            Console.WriteLine("3. Кількість двозначних чисел: " + twoDigitCount + " (файл: '" + twoDigitFile + "')");
            Console.WriteLine("4. Кількість п'ятизначних чисел: " + fiveDigitCount + " (файл: '" + fiveDigitFile + "')");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            try
            {
                RunTask1();
                RunTask2();
                RunTask3();
                RunTask4();
                RunTask5();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
