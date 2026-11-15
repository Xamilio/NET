using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace PoemsCollectionApp
{
    public class Poem
    {
        private string title;
        private string authorFullName;
        private int year;
        private string text;
        private string theme;

        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва вірша не може бути порожньою.");
                }
                title = value;
            }
        }

        public string AuthorFullName
        {
            get { return authorFullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("П.І.Б. автора не може бути порожнім.");
                }
                authorFullName = value;
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (value <= 0 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Некоректний рік написання вірша.");
                }
                year = value;
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Текст вірша не може бути порожнім.");
                }
                text = value;
            }
        }

        public string Theme
        {
            get { return theme; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Тема вірша не може бути порожньою.");
                }
                theme = value;
            }
        }

        public int Length
        {
            get { return text.Length; }
        }

        public Poem()
        {
            title = "Без назви";
            authorFullName = "Невідомий автор";
            year = 2000;
            text = "-";
            theme = "Загальна";
        }

        public Poem(string title, string authorFullName, int year, string text, string theme)
        {
            this.title = string.Empty;
            this.authorFullName = string.Empty;
            this.text = string.Empty;
            this.theme = string.Empty;

            Title = title;
            AuthorFullName = authorFullName;
            Year = year;
            Text = text;
            Theme = theme;
        }

        public bool ContainsWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '—', '\n', '\r', '\t', '"', '«', '»' };
            string[] words = Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                if (string.Equals(words[i], word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Назва: «" + Title + "»");
            sb.AppendLine("Автор: " + AuthorFullName);
            sb.AppendLine("Рік написання: " + Year);
            sb.AppendLine("Тема: " + Theme);
            sb.AppendLine("Довжина тексту: " + Length + " символів");
            sb.AppendLine("Текст:\n" + Text);
            return sb.ToString();
        }
    }

    public class PoemCollection
    {
        private List<Poem> poems;

        public int Count
        {
            get { return poems.Count; }
        }

        public PoemCollection()
        {
            poems = new List<Poem>();
        }

        public void AddPoem(Poem poem)
        {
            if (poem == null)
            {
                throw new ArgumentNullException(nameof(poem), "Вірш не може бути null.");
            }

            poems.Add(poem);
        }

        public bool RemovePoemByTitle(string title)
        {
            for (int i = 0; i < poems.Count; i++)
            {
                if (string.Equals(poems[i].Title, title, StringComparison.OrdinalIgnoreCase))
                {
                    poems.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool UpdatePoem(string currentTitle, string newTitle, string newAuthor, int newYear, string newText, string newTheme)
        {
            for (int i = 0; i < poems.Count; i++)
            {
                if (string.Equals(poems[i].Title, currentTitle, StringComparison.OrdinalIgnoreCase))
                {
                    poems[i].Title = newTitle;
                    poems[i].AuthorFullName = newAuthor;
                    poems[i].Year = newYear;
                    poems[i].Text = newText;
                    poems[i].Theme = newTheme;
                    return true;
                }
            }

            return false;
        }

        public List<Poem> SearchByTitle(string titlePart)
        {
            List<Poem> result = new List<Poem>();
            for (int i = 0; i < poems.Count; i++)
            {
                if (poems[i].Title.IndexOf(titlePart, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(poems[i]);
                }
            }
            return result;
        }

        public List<Poem> SearchByAuthor(string authorPart)
        {
            List<Poem> result = new List<Poem>();
            for (int i = 0; i < poems.Count; i++)
            {
                if (poems[i].AuthorFullName.IndexOf(authorPart, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(poems[i]);
                }
            }
            return result;
        }

        public List<Poem> SearchByTheme(string theme)
        {
            List<Poem> result = new List<Poem>();
            for (int i = 0; i < poems.Count; i++)
            {
                if (string.Equals(poems[i].Theme, theme, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(poems[i]);
                }
            }
            return result;
        }

        public List<Poem> SearchByWord(string word)
        {
            List<Poem> result = new List<Poem>();
            for (int i = 0; i < poems.Count; i++)
            {
                if (poems[i].ContainsWord(word))
                {
                    result.Add(poems[i]);
                }
            }
            return result;
        }

        public List<Poem> SearchByYear(int year)
        {
            List<Poem> result = new List<Poem>();
            for (int i = 0; i < poems.Count; i++)
            {
                if (poems[i].Year == year)
                {
                    result.Add(poems[i]);
                }
            }
            return result;
        }

        public List<Poem> SearchByLengthRange(int minLength, int maxLength)
        {
            List<Poem> result = new List<Poem>();
            for (int i = 0; i < poems.Count; i++)
            {
                if (poems[i].Length >= minLength && poems[i].Length <= maxLength)
                {
                    result.Add(poems[i]);
                }
            }
            return result;
        }

        public void SaveToFile(string filePath)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };

            string json = JsonSerializer.Serialize(poems, options);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл колекції віршів не знайдено.", filePath);
            }

            string json = File.ReadAllText(filePath, Encoding.UTF8);
            List<Poem>? loadedPoems = JsonSerializer.Deserialize<List<Poem>>(json);

            if (loadedPoems != null)
            {
                poems = loadedPoems;
            }
        }

        private string FormatReport(string reportTitle, List<Poem> items)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== " + reportTitle + " ===");
            sb.AppendLine("Знайдено віршів: " + items.Count);
            sb.AppendLine(new string('-', 45));

            if (items.Count == 0)
            {
                sb.AppendLine("Віршів за заданим критерієм не знайдено.");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    sb.AppendLine((i + 1) + ". " + items[i].GetInfo());
                }
            }

            return sb.ToString();
        }

        public string GenerateReportByTitle(string titleQuery)
        {
            return FormatReport("Звіт за назвою вірша («" + titleQuery + "»)", SearchByTitle(titleQuery));
        }

        public string GenerateReportByAuthor(string authorQuery)
        {
            return FormatReport("Звіт за П.І.Б. автора («" + authorQuery + "»)", SearchByAuthor(authorQuery));
        }

        public string GenerateReportByTheme(string themeQuery)
        {
            return FormatReport("Звіт за темою вірша («" + themeQuery + "»)", SearchByTheme(themeQuery));
        }

        public string GenerateReportByWord(string wordQuery)
        {
            return FormatReport("Звіт за словом у тексті («" + wordQuery + "»)", SearchByWord(wordQuery));
        }

        public string GenerateReportByYear(int yearQuery)
        {
            return FormatReport("Звіт за роком написання (" + yearQuery + ")", SearchByYear(yearQuery));
        }

        public string GenerateReportByLength(int minLength, int maxLength)
        {
            return FormatReport("Звіт за довжиною вірша (" + minLength + " - " + maxLength + " символів)", SearchByLengthRange(minLength, maxLength));
        }

        public void OutputReport(string reportContent, bool saveToFile, string? reportFilePath = null)
        {
            Console.WriteLine(reportContent);

            if (saveToFile && !string.IsNullOrWhiteSpace(reportFilePath))
            {
                File.WriteAllText(reportFilePath, reportContent, Encoding.UTF8);
                Console.WriteLine("Звіт збережено у файл: " + reportFilePath);
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.WriteLine("--- ЗАВДАННЯ 1: Робота з колекцією віршів ---");

                PoemCollection collection = new PoemCollection();

                collection.AddPoem(new Poem(
                    "Заповіт",
                    "Шевченко Тарас Григорович",
                    1845,
                    "Як умру, то поховайте\nМене на могилі\nСеред степу широкого\nНа Вкраїні милій...",
                    "Патріотична лірика"
                ));

                collection.AddPoem(new Poem(
                    "Садок вишневий коло хати",
                    "Шевченко Тарас Григорович",
                    1847,
                    "Садок вишневий коло хати,\nХрущі над вишнями гудуть,\nПлугатарі з плугами йдуть...",
                    "Пейзажна лірика"
                ));

                collection.AddPoem(new Poem(
                    "Contra spem spero!",
                    "Косач Лариса Петрівна (Леся Українка)",
                    1890,
                    "Гетьте, думи, ви хмари осінні!\nТо ж тепера весна золота!",
                    "Філософська лірика"
                ));

                collection.AddPoem(new Poem(
                    "Чого являєшся мені у сні?",
                    "Франко Іван Якович",
                    1896,
                    "Чого являєшся мені у сні?\nЧого звертаєш ти до мене чудові очі ті ясні...",
                    "Інтимна лірика"
                ));

                Console.WriteLine("Додано віршів у колекцію: " + collection.Count);

                collection.UpdatePoem(
                    "Contra spem spero!",
                    "Contra spem spero!",
                    "Косач Лариса Петрівна (Леся Українка)",
                    1890,
                    "Гетьте, думи, ви хмари осінні!\nТо ж тепера весна золота!\nЧи то так у жалю, в голосінні\nПроминуть молодії літа?",
                    "Філософська лірика"
                );
                Console.WriteLine("Оновлено інформацію про вірш «Contra spem spero!»");

                collection.RemovePoemByTitle("Чого являєшся мені у сні?");
                Console.WriteLine("Після видалення вірша залишилось у колекції: " + collection.Count);

                string storageFile = "poems_collection.json";
                collection.SaveToFile(storageFile);
                Console.WriteLine("Колекцію збережено у файл: " + storageFile);

                PoemCollection loadedCollection = new PoemCollection();
                loadedCollection.LoadFromFile(storageFile);
                Console.WriteLine("Колекцію завантажено з файлу (віршів: " + loadedCollection.Count + ").");
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 2: Звіти (за назвою, автором, темою) ---");

                string reportByTitle = loadedCollection.GenerateReportByTitle("Заповіт");
                loadedCollection.OutputReport(reportByTitle, true, "report_by_title.txt");

                string reportByAuthor = loadedCollection.GenerateReportByAuthor("Шевченко");
                loadedCollection.OutputReport(reportByAuthor, true, "report_by_author.txt");

                string reportByTheme = loadedCollection.GenerateReportByTheme("Філософська лірика");
                loadedCollection.OutputReport(reportByTheme, true, "report_by_theme.txt");

                Console.WriteLine("--- ЗАВДАННЯ 3: Додаткові звіти (за словом, роком, довжиною) ---");

                string reportByWord = loadedCollection.GenerateReportByWord("весна");
                loadedCollection.OutputReport(reportByWord, true, "report_by_word.txt");

                string reportByYear = loadedCollection.GenerateReportByYear(1847);
                loadedCollection.OutputReport(reportByYear, true, "report_by_year.txt");

                string reportByLength = loadedCollection.GenerateReportByLength(70, 120);
                loadedCollection.OutputReport(reportByLength, true, "report_by_length.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
