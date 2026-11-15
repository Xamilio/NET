using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace GarbageCollectorAndDisposableTasks
{
    public enum ShopType
    {
        Grocery,
        Household,
        Clothing,
        Footwear
    }

    public class Play : IDisposable
    {
        private string title;
        private string authorFullName;
        private string genre;
        private int releaseYear;
        private bool isDisposed;

        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва п'єси не може бути порожньою.");
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

        public string Genre
        {
            get { return genre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Жанр не може бути порожнім.");
                }
                genre = value;
            }
        }

        public int ReleaseYear
        {
            get { return releaseYear; }
            set
            {
                if (value <= 0 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Некоректний рік випуску п'єси.");
                }
                releaseYear = value;
            }
        }

        public Play(string title, string authorFullName, string genre, int releaseYear)
        {
            this.title = string.Empty;
            this.authorFullName = string.Empty;
            this.genre = string.Empty;

            Title = title;
            AuthorFullName = authorFullName;
            Genre = genre;
            ReleaseYear = releaseYear;
            isDisposed = false;
        }

        public void DisplayInfo()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(nameof(Play), "Об'єкт п'єси вже звільнено.");
            }

            Console.WriteLine("П'єса: «" + Title + "»");
            Console.WriteLine("Автор: " + AuthorFullName);
            Console.WriteLine("Жанр: " + Genre);
            Console.WriteLine("Рік випуску: " + ReleaseYear);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Console.WriteLine("[Dispose] Ресурси п'єси «" + Title + "» звільнено через метод Dispose().");
                }

                isDisposed = true;
            }
        }

        ~Play()
        {
            Dispose(false);
            Console.WriteLine("[Деструктор] Об'єкт п'єси «" + Title + "» видалено збирачем сміття.");
        }
    }

    public class Shop : IDisposable
    {
        private string name;
        private string address;
        private ShopType shopType;
        private bool isDisposed;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва магазину не може бути порожньою.");
                }
                name = value;
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Адреса магазину не може бути порожньою.");
                }
                address = value;
            }
        }

        public ShopType Type
        {
            get { return shopType; }
            set { shopType = value; }
        }

        public Shop(string name, string address, ShopType shopType)
        {
            this.name = string.Empty;
            this.address = string.Empty;

            Name = name;
            Address = address;
            Type = shopType;
            isDisposed = false;
        }

        public static string GetShopTypeName(ShopType type)
        {
            switch (type)
            {
                case ShopType.Grocery:
                    return "Продовольчий";
                case ShopType.Household:
                    return "Господарський";
                case ShopType.Clothing:
                    return "Одяг";
                case ShopType.Footwear:
                    return "Взуття";
                default:
                    return type.ToString();
            }
        }

        public void DisplayInfo()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(nameof(Shop), "Об'єкт магазину вже звільнено.");
            }

            Console.WriteLine("Магазин: «" + Name + "»");
            Console.WriteLine("Адреса: " + Address);
            Console.WriteLine("Тип магазину: " + GetShopTypeName(Type));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Console.WriteLine("[Dispose] Ресурси магазину «" + Name + "» звільнено через метод Dispose().");
                }

                isDisposed = true;
            }
        }

        ~Shop()
        {
            Dispose(false);
            Console.WriteLine("[Деструктор] Об'єкт магазину «" + Name + "» видалено збирачем сміття.");
        }
    }

    class Program
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CreatePlayWithoutDispose()
        {
            Play tempPlay = new Play("Лісова пісня", "Косач Лариса Петрівна (Леся Українка)", "Драма-феєрія", 1911);
            tempPlay.DisplayInfo();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CreateShopWithoutDispose()
        {
            Shop tempShop = new Shop("Епіцентр", "м. Київ, просп. Степана Бандери, 11-А", ShopType.Household);
            tempShop.DisplayInfo();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.WriteLine("--- ЗАВДАННЯ 1: Клас «П'єса» та тестування деструктора ---");
                CreatePlayWithoutDispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 2: Клас «Магазин» та виклик Dispose() ---");
                Shop groceryShop = new Shop("Сільпо", "м. Київ, вул. Хрещатик, 44", ShopType.Grocery);
                groceryShop.DisplayInfo();
                groceryShop.Dispose();
                Console.WriteLine();

                using (Shop footwearShop = new Shop("Intertop", "м. Львів, пл. Міцкевича, 8", ShopType.Footwear))
                {
                    footwearShop.DisplayInfo();
                }
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 3: IDisposable для «П'єси» та деструктор для «Магазину» ---");

                using (Play playWithDispose = new Play("Наталка Полтавка", "Котляревський Іван Петрович", "Соціально-побутова драма", 1819))
                {
                    playWithDispose.DisplayInfo();
                }
                Console.WriteLine();

                CreateShopWithoutDispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
