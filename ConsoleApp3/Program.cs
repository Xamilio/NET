using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CollectionsTasks
{
    public abstract class SeaItem
    {
        private string name;
        private string species;
        private int age;
        private double weight;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ім'я морської істоти не може бути порожнім.");
                }
                name = value;
            }
        }

        public string Species
        {
            get { return species; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Вид морської істоти не може бути порожнім.");
                }
                species = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Вік не може бути від'ємним.");
                }
                age = value;
            }
        }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Вага повинна бути більшою за нуль.");
                }
                weight = value;
            }
        }

        protected SeaItem(string name, string species, int age, double weight)
        {
            this.name = string.Empty;
            this.species = string.Empty;

            Name = name;
            Species = species;
            Age = age;
            Weight = weight;
        }

        public virtual string GetInfo()
        {
            return "Ім'я: " + Name + ", Вид: " + Species + ", Вік: " + Age + " р., Вага: " + Weight + " кг";
        }
    }

    public class Shark : SeaItem
    {
        private double lengthMeters;

        public double LengthMeters
        {
            get { return lengthMeters; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Довжина акули повинна бути більшою за нуль.");
                }
                lengthMeters = value;
            }
        }

        public Shark(string name, string species, int age, double weight, double lengthMeters)
            : base(name, species, age, weight)
        {
            LengthMeters = lengthMeters;
        }

        public override string GetInfo()
        {
            return "[Акула] " + base.GetInfo() + ", Довжина: " + LengthMeters + " м";
        }
    }

    public class Dolphin : SeaItem
    {
        private double maxJumpHeight;

        public double MaxJumpHeight
        {
            get { return maxJumpHeight; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Висота стрибка не може бути від'ємною.");
                }
                maxJumpHeight = value;
            }
        }

        public Dolphin(string name, string species, int age, double weight, double maxJumpHeight)
            : base(name, species, age, weight)
        {
            MaxJumpHeight = maxJumpHeight;
        }

        public override string GetInfo()
        {
            return "[Дельфін] " + base.GetInfo() + ", Висота стрибка: " + MaxJumpHeight + " м";
        }
    }

    public class SeaTurtle : SeaItem
    {
        private double shellDiameterCm;

        public double ShellDiameterCm
        {
            get { return shellDiameterCm; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Діаметр панцира повинен бути більшим за нуль.");
                }
                shellDiameterCm = value;
            }
        }

        public SeaTurtle(string name, string species, int age, double weight, double shellDiameterCm)
            : base(name, species, age, weight)
        {
            ShellDiameterCm = shellDiameterCm;
        }

        public override string GetInfo()
        {
            return "[Морська черепаха] " + base.GetInfo() + ", Діаметр панцира: " + ShellDiameterCm + " см";
        }
    }

    public class Octopus : SeaItem
    {
        private int tentaclesCount;

        public int TentaclesCount
        {
            get { return tentaclesCount; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Кількість щупалець повинна бути більшою за нуль.");
                }
                tentaclesCount = value;
            }
        }

        public Octopus(string name, string species, int age, double weight, int tentaclesCount)
            : base(name, species, age, weight)
        {
            TentaclesCount = tentaclesCount;
        }

        public override string GetInfo()
        {
            return "[Восьминіг] " + base.GetInfo() + ", Кількість щупалець: " + TentaclesCount;
        }
    }

    public class Oceanarium : IEnumerable<SeaItem>
    {
        private string title;
        private List<SeaItem> seaItems;

        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва океанаріуму не може бути порожньою.");
                }
                title = value;
            }
        }

        public List<SeaItem> SeaItems
        {
            get { return seaItems; }
        }

        public Oceanarium(string title)
        {
            this.title = string.Empty;
            Title = title;
            seaItems = new List<SeaItem>();
        }

        public void AddSeaItem(SeaItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Морська істота не може бути null.");
            }

            seaItems.Add(item);
        }

        public void DisplayAll()
        {
            Console.WriteLine("Океанаріум: «" + Title + "» (кількість мешканців: " + seaItems.Count + ")");

            if (seaItems.Count == 0)
            {
                Console.WriteLine("Список мешканців порожній.");
                return;
            }

            for (int i = 0; i < seaItems.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + seaItems[i].GetInfo());
            }
        }

        public IEnumerator<SeaItem> GetEnumerator()
        {
            return seaItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Player
    {
        private string fullName;
        private int number;
        private string position;
        private int age;

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ПІБ гравця не може бути порожнім.");
                }
                fullName = value;
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                if (value <= 0 || value > 99)
                {
                    throw new ArgumentException("Ігровий номер повинен бути в межах від 1 до 99.");
                }
                number = value;
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Позиція гравця не може бути порожньою.");
                }
                position = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 15)
                {
                    throw new ArgumentException("Вік гравця повинен бути не менше 15 років.");
                }
                age = value;
            }
        }

        public Player(string fullName, int number, string position, int age)
        {
            this.fullName = string.Empty;
            this.position = string.Empty;

            FullName = fullName;
            Number = number;
            Position = position;
            Age = age;
        }

        public string GetInfo()
        {
            return "№" + Number + " — " + FullName + " (" + Position + ", " + Age + " р.)";
        }
    }

    public class FootballTeam : IEnumerable<Player>
    {
        private string teamName;
        private string city;
        private List<Player> players;

        public string TeamName
        {
            get { return teamName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва команди не може бути порожньою.");
                }
                teamName = value;
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Місто команди не може бути порожнім.");
                }
                city = value;
            }
        }

        public List<Player> Players
        {
            get { return players; }
        }

        public FootballTeam(string teamName, string city)
        {
            this.teamName = string.Empty;
            this.city = string.Empty;

            TeamName = teamName;
            City = city;
            players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "Гравець не може бути null.");
            }

            players.Add(player);
        }

        public void DisplayAll()
        {
            Console.WriteLine("Футбольна команда: «" + TeamName + "» (м. " + City + "), гравців у складі: " + players.Count);

            if (players.Count == 0)
            {
                Console.WriteLine("Список гравців порожній.");
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + players[i].GetInfo());
            }
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.WriteLine("--- ЗАВДАННЯ 1: Океанаріум ---");

                Oceanarium oceanarium = new Oceanarium("Морська Перлина");

                oceanarium.AddSeaItem(new Shark("Шторм", "Біла акула", 8, 680.5, 4.5));
                oceanarium.AddSeaItem(new Dolphin("Перлина", "Афаліна", 5, 190.0, 3.8));
                oceanarium.AddSeaItem(new SeaTurtle("Матильда", "Зелена черепаха", 42, 135.2, 95.0));
                oceanarium.AddSeaItem(new Octopus("Кракенець", "Гігантський восьминіг", 3, 28.4, 8));

                oceanarium.DisplayAll();

                Console.WriteLine();
                Console.WriteLine("--- ЗАВДАННЯ 2: Футбольна команда ---");

                FootballTeam team = new FootballTeam("Динамо", "Київ");

                team.AddPlayer(new Player("Бущан Георгій Миколайович", 1, "Воротар", 30));
                team.AddPlayer(new Player("Попов Денис Юрійович", 4, "Захисник", 25));
                team.AddPlayer(new Player("Шапаренко Микола Володимирович", 10, "Півзахисник", 26));
                team.AddPlayer(new Player("Ярмоленко Андрій Миколайович", 7, "Півзахисник", 35));
                team.AddPlayer(new Player("Ванат Владислав Андрійович", 11, "Нападник", 23));

                team.DisplayAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
