using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentProcessingSystem
{
    public interface ITextProcessor
    {
        string ProcessText();
        int CountWords();
        bool ContainsWord(string word);
    }

    public record DocumentStatistics(int WordCount, int CharacterCount, string LongestWord, int UniqueWordCount);

    public record DocumentSummary(string Title, string Author, string DocumentType, int WordCount);

    public class Document : ITextProcessor
    {
        private string title;
        private string author;
        private string text;
        private DateTime createdAt;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        public Document(string title, string author, string text)
        {
            this.title = title;
            this.author = author;
            this.text = text;
            this.createdAt = DateTime.Now;
        }

        public virtual string GetInfo()
        {
            return "Назва: " + Title + "\nАвтор: " + Author + "\nДата створення: " + CreatedAt.ToShortDateString() + "\nКількість слів: " + CountWords();
        }

        public string ProcessText()
        {
            if (text == null)
            {
                return "";
            }
            return text.Trim();
        }

        public string[] GetCleanWords()
        {
            string cleaned = ProcessText();
            char[] separators = new char[] { ' ', '.', ',', '!', '?', '-', ':', ';', '(', ')', '"', '\n', '\r' };
            return cleaned.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        public int CountWords()
        {
            string[] words = GetCleanWords();
            return words.Length;
        }

        public bool ContainsWord(string word)
        {
            string[] words = GetCleanWords();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].ToLower() == word.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public DocumentStatistics AnalyzeDocument()
        {
            string[] words = GetCleanWords();
            int wordCount = words.Length;
            int charCount = Text.Length;
            string longestWord = "";
            List<string> uniqueWords = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > longestWord.Length)
                {
                    longestWord = words[i];
                }

                string lowerWord = words[i].ToLower();
                if (!uniqueWords.Contains(lowerWord))
                {
                    uniqueWords.Add(lowerWord);
                }
            }

            return new DocumentStatistics(wordCount, charCount, longestWord, uniqueWords.Count);
        }
    }

    public class Article : Document
    {
        private string category;
        private int readTime;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public int ReadTime
        {
            get { return readTime; }
            set { readTime = value; }
        }

        public Article(string title, string author, string text, string category, int readTime)
            : base(title, author, text)
        {
            this.category = category;
            this.readTime = readTime;
        }

        public override string GetInfo()
        {
            return "[Стаття]\n" + base.GetInfo() + "\nКатегорія: " + Category + "\nЧас читання: " + ReadTime + " хв.";
        }
    }

    public class Report : Document
    {
        private string department;
        private int pageCount;

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }

        public Report(string title, string author, string text, string department, int pageCount)
            : base(title, author, text)
        {
            this.department = department;
            this.pageCount = pageCount;
        }

        public override string GetInfo()
        {
            return "[Звіт]\n" + base.GetInfo() + "\nВідділ: " + Department + "\nКількість сторінок: " + PageCount;
        }
    }

    public class ReportGenerator
    {
        public string GenerateFullReport(List<Document> documents)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== ЗАГАЛЬНИЙ ЗВІТ ПРО ДОКУМЕНТИ ===");
            sb.AppendLine();

            int totalWords = 0;
            int totalChars = 0;

            for (int i = 0; i < documents.Count; i++)
            {
                Document doc = documents[i];
                DocumentStatistics stats = doc.AnalyzeDocument();

                sb.Append("Документ №");
                sb.AppendLine((i + 1).ToString());
                sb.AppendLine(doc.GetInfo());
                sb.AppendLine("--- Статистика тексту ---");
                sb.Append("Кількість слів: ");
                sb.AppendLine(stats.WordCount.ToString());
                sb.Append("Кількість символів: ");
                sb.AppendLine(stats.CharacterCount.ToString());
                sb.Append("Найдовше слово: ");
                sb.AppendLine(stats.LongestWord);
                sb.Append("Унікальних слів: ");
                sb.AppendLine(stats.UniqueWordCount.ToString());
                sb.AppendLine("-------------------------");
                sb.AppendLine();

                totalWords += stats.WordCount;
                totalChars += stats.CharacterCount;
            }

            sb.AppendLine("=== ЗАГАЛЬНИЙ ПІДСУМОК ===");
            sb.Append("Всього документів: ");
            sb.AppendLine(documents.Count.ToString());
            sb.Append("Загальна кількість слів: ");
            sb.AppendLine(totalWords.ToString());
            sb.Append("Загальна кількість символів: ");
            sb.AppendLine(totalChars.ToString());

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            List<Document> documents = new List<Document>();
            ReportGenerator reportGenerator = new ReportGenerator();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nСИСТЕМА ОБРОБКИ ДОКУМЕНТІВ");
                Console.WriteLine("1. Додати статтю");
                Console.WriteLine("2. Додати звіт");
                Console.WriteLine("3. Показати всі документи");
                Console.WriteLine("4. Знайти документи за словом");
                Console.WriteLine("5. Показати статистику документа");
                Console.WriteLine("6. Згенерувати загальний звіт");
                Console.WriteLine("7. Вийти");
                Console.Write("\nОберіть дію: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                {
                    Console.Write("Введіть назву статті: ");
                    string title = Console.ReadLine();

                    Console.Write("Введіть автора: ");
                    string author = Console.ReadLine();

                    Console.Write("Введіть категорію: ");
                    string category = Console.ReadLine();

                    Console.Write("Введіть час читання: ");
                    int readTime = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введіть текст:");
                    string text = Console.ReadLine();

                    Article article = new Article(title, author, text, category, readTime);
                    documents.Add(article);

                    Console.WriteLine("\nДокумент успішно створено!");
                }
                else if (choice == "2")
                {
                    Console.Write("Введіть назву звіту: ");
                    string title = Console.ReadLine();

                    Console.Write("Введіть автора: ");
                    string author = Console.ReadLine();

                    Console.Write("Введіть відділ: ");
                    string department = Console.ReadLine();

                    Console.Write("Введіть кількість сторінок: ");
                    int pageCount = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введіть текст:");
                    string text = Console.ReadLine();

                    Report report = new Report(title, author, text, department, pageCount);
                    documents.Add(report);

                    Console.WriteLine("\nДокумент успішно створено!");
                }
                else if (choice == "3")
                {
                    if (documents.Count == 0)
                    {
                        Console.WriteLine("Список документів порожній.");
                    }
                    else
                    {
                        for (int i = 0; i < documents.Count; i++)
                        {
                            Document doc = documents[i];
                            string docType = "Документ";
                            if (doc is Article)
                            {
                                docType = "Стаття";
                            }
                            else if (doc is Report)
                            {
                                docType = "Звіт";
                            }

                            DocumentSummary summary = new DocumentSummary(doc.Title, doc.Author, docType, doc.CountWords());
                            Console.WriteLine((i + 1) + ". " + summary.Title + " (Автор: " + summary.Author + ", Тип: " + summary.DocumentType + ", Слів: " + summary.WordCount + ")");

                            if (i == 0)
                            {
                                DocumentSummary copySummary = summary with { Title = summary.Title + " (Копія)" };
                                Console.WriteLine("   Приклад копії через with: " + copySummary.Title + ", Автор: " + copySummary.Author);
                            }
                        }
                    }
                }
                else if (choice == "4")
                {
                    Console.Write("Введіть слово для пошуку: ");
                    string word = Console.ReadLine();
                    Console.WriteLine();

                    bool found = false;
                    for (int i = 0; i < documents.Count; i++)
                    {
                        if (documents[i].ContainsWord(word))
                        {
                            Console.WriteLine("Знайдено документ:");
                            Console.WriteLine(documents[i].Title);
                            Console.WriteLine("Автор: " + documents[i].Author);
                            Console.WriteLine("Кількість слів: " + documents[i].CountWords());
                            Console.WriteLine();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Документів із таким словом не знайдено.");
                    }
                }
                else if (choice == "5")
                {
                    if (documents.Count == 0)
                    {
                        Console.WriteLine("Список документів порожній.");
                    }
                    else
                    {
                        Console.Write("Введіть номер документа (від 1 до " + documents.Count + "): ");
                        int index = int.Parse(Console.ReadLine()) - 1;

                        if (index >= 0 && index < documents.Count)
                        {
                            DocumentStatistics stats = documents[index].AnalyzeDocument();
                            Console.WriteLine("\nСтатистика документа '" + documents[index].Title + "':");
                            Console.WriteLine("Кількість слів: " + stats.WordCount);
                            Console.WriteLine("Кількість символів: " + stats.CharacterCount);
                            Console.WriteLine("Найдовше слово: " + stats.LongestWord);
                            Console.WriteLine("Кількість унікальних слів: " + stats.UniqueWordCount);
                        }
                        else
                        {
                            Console.WriteLine("Невірний номер документа.");
                        }
                    }
                }
                else if (choice == "6")
                {
                    if (documents.Count == 0)
                    {
                        Console.WriteLine("Немає документів для генерації звіту.");
                    }
                    else
                    {
                        string fullReport = reportGenerator.GenerateFullReport(documents);
                        Console.WriteLine(fullReport);
                    }
                }
                else if (choice == "7")
                {
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                }
            }
        }
    }
}