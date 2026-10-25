using System;
using System.Collections.Generic;

namespace OOPTasks
{
    public class Journal
    {
        public string Title { get; set; }
        private int employeesCount;

        public int EmployeesCount
        {
            get { return employeesCount; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Кількість працівників не може бути від'ємною.");
                }
                employeesCount = value;
            }
        }

        public Journal(string title, int employeesCount)
        {
            Title = title;
            EmployeesCount = employeesCount;
        }

        public static Journal operator +(Journal journal, int amount)
        {
            return new Journal(journal.Title, journal.EmployeesCount + amount);
        }

        public static Journal operator -(Journal journal, int amount)
        {
            return new Journal(journal.Title, journal.EmployeesCount - amount);
        }

        public static bool operator ==(Journal j1, Journal j2)
        {
            if (ReferenceEquals(j1, j2)) return true;
            if (ReferenceEquals(j1, null) || ReferenceEquals(j2, null)) return false;
            return j1.EmployeesCount == j2.EmployeesCount;
        }

        public static bool operator !=(Journal j1, Journal j2)
        {
            return !(j1 == j2);
        }

        public static bool operator <(Journal j1, Journal j2)
        {
            return j1.EmployeesCount < j2.EmployeesCount;
        }

        public static bool operator >(Journal j1, Journal j2)
        {
            return j1.EmployeesCount > j2.EmployeesCount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Journal journal)
            {
                return this.EmployeesCount == journal.EmployeesCount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return EmployeesCount.GetHashCode();
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Журнал: " + Title + " | Працівників: " + EmployeesCount);
        }
    }

    public class Shop
    {
        public string Name { get; set; }
        private double area;

        public double Area
        {
            get { return area; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Площа магазину не може бути від'ємною.");
                }
                area = value;
            }
        }

        public Shop(string name, double area)
        {
            Name = name;
            Area = area;
        }

        public static Shop operator +(Shop shop, double amount)
        {
            return new Shop(shop.Name, shop.Area + amount);
        }

        public static Shop operator -(Shop shop, double amount)
        {
            return new Shop(shop.Name, shop.Area - amount);
        }

        public static bool operator ==(Shop s1, Shop s2)
        {
            if (ReferenceEquals(s1, s2)) return true;
            if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null)) return false;
            return s1.Area == s2.Area;
        }

        public static bool operator !=(Shop s1, Shop s2)
        {
            return !(s1 == s2);
        }

        public static bool operator <(Shop s1, Shop s2)
        {
            return s1.Area < s2.Area;
        }

        public static bool operator >(Shop s1, Shop s2)
        {
            return s1.Area > s2.Area;
        }

        public override bool Equals(object obj)
        {
            if (obj is Shop shop)
            {
                return this.Area == shop.Area;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Area.GetHashCode();
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Магазин: " + Name + " | Площа: " + Area + " кв.м.");
        }
    }

    public class BookList
    {
        private List<string> books;

        public int Count
        {
            get { return books.Count; }
        }

        public BookList()
        {
            books = new List<string>();
        }

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= books.Count)
                {
                    throw new IndexOutOfRangeException("Некоректний індекс книги.");
                }
                return books[index];
            }
            set
            {
                if (index < 0 || index >= books.Count)
                {
                    throw new IndexOutOfRangeException("Некоректний індекс книги.");
                }
                books[index] = value;
            }
        }

        public static BookList operator +(BookList list, string book)
        {
            if (!string.IsNullOrEmpty(book) && !list.Contains(book))
            {
                list.books.Add(book);
            }
            return list;
        }

        public static BookList operator -(BookList list, string book)
        {
            list.books.Remove(book);
            return list;
        }

        public bool Contains(string book)
        {
            return books.Contains(book);
        }

        public void DisplayBooks()
        {
            Console.WriteLine("=== Список книг до прочитання ===");
            if (books.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + books[i]);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                Console.WriteLine("--- ЗАВДАННЯ 1: Журнал ---");
                Journal j1 = new Journal("IT World", 10);
                Journal j2 = new Journal("Science", 15);

                j1 = j1 + 5;
                j1.DisplayInfo();

                Console.WriteLine("j1 == j2: " + (j1 == j2));
                Console.WriteLine("j1 > j2: " + (j1 > j2));
                Console.WriteLine("Equals: " + j1.Equals(j2));
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 2: Магазин ---");
                Shop s1 = new Shop("Продукти", 150.5);
                Shop s2 = new Shop("Одяг", 200.0);

                s1 = s1 + 49.5;
                s1.DisplayInfo();

                Console.WriteLine("s1 == s2: " + (s1 == s2));
                Console.WriteLine("s1 < s2: " + (s1 < s2));
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 3: Список книг ---");
                BookList readingList = new BookList();

                readingList = readingList + "Кобзар";
                readingList = readingList + "1984";
                readingList = readingList + "Майстер і Маргарита";

                readingList.DisplayBooks();

                Console.WriteLine("\nЧи є книга '1984' у списку? " + readingList.Contains("1984"));

                Console.WriteLine("\nКнига за індексом 0: " + readingList[0]);

                readingList = readingList - "1984";
                Console.WriteLine("\nПісля видалення '1984':");
                readingList.DisplayBooks();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}