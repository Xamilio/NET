using System;
using System.Text;

namespace DelegatesAndEventsTasks
{
    public delegate void CardEventHandler(string message);

    public class CreditCard
    {
        private string cardNumber;
        private string ownerFullName;
        private DateTime expiryDate;
        private string pin;
        private decimal creditLimit;
        private decimal balance;
        private decimal targetLimit;

        public event CardEventHandler? AccountReplenished;
        public event CardEventHandler? MoneySpent;
        public event CardEventHandler? CreditStarted;
        public event CardEventHandler? TargetLimitReached;
        public event CardEventHandler? PinChanged;

        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Номер картки не може бути порожнім.");
                }
                cardNumber = value;
            }
        }

        public string OwnerFullName
        {
            get { return ownerFullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ПІБ власника не може бути порожнім.");
                }
                ownerFullName = value;
            }
        }

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public string Pin
        {
            get { return pin; }
            private set { pin = value; }
        }

        public decimal CreditLimit
        {
            get { return creditLimit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Кредитний ліміт не може бути від'ємним.");
                }
                creditLimit = value;
            }
        }

        public decimal Balance
        {
            get { return balance; }
            private set { balance = value; }
        }

        public decimal TargetLimit
        {
            get { return targetLimit; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Заданий ліміт суми повинен бути більшим за нуль.");
                }
                targetLimit = value;
            }
        }

        public CreditCard(
            string cardNumber,
            string ownerFullName,
            DateTime expiryDate,
            string pin,
            decimal creditLimit,
            decimal initialBalance,
            decimal targetLimit)
        {
            if (!IsValidPin(pin))
            {
                throw new ArgumentException("PIN повинен складатися з 4 цифр.");
            }

            this.cardNumber = string.Empty;
            this.ownerFullName = string.Empty;
            this.pin = pin;

            CardNumber = cardNumber;
            OwnerFullName = ownerFullName;
            ExpiryDate = expiryDate;
            CreditLimit = creditLimit;
            Balance = initialBalance;
            TargetLimit = targetLimit;
        }

        private bool IsValidPin(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool VerifyPin(string inputPin)
        {
            return Pin == inputPin;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сума поповнення повинна бути більшою за нуль.");
            }

            decimal previousBalance = Balance;
            Balance += amount;

            if (AccountReplenished != null)
            {
                AccountReplenished("Рахунок поповнено на " + amount + " грн. Поточний баланс: " + Balance + " грн.");
            }

            if (previousBalance < TargetLimit && Balance >= TargetLimit)
            {
                if (TargetLimitReached != null)
                {
                    TargetLimitReached("Досягнуто ліміту заданої суми (" + TargetLimit + " грн)! На рахунку: " + Balance + " грн.");
                }
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сума списання повинна бути більшою за нуль.");
            }

            if (DateTime.Now.Date > ExpiryDate.Date)
            {
                throw new InvalidOperationException("Термін дії картки закінчився.");
            }

            decimal availableFunds = Balance + CreditLimit;
            if (amount > availableFunds)
            {
                throw new InvalidOperationException("Недостатньо коштів (перевищено кредитний ліміт). Доступно: " + availableFunds + " грн.");
            }

            decimal previousBalance = Balance;
            Balance -= amount;

            if (MoneySpent != null)
            {
                MoneySpent("З рахунку витрачено " + amount + " грн. Поточний баланс: " + Balance + " грн.");
            }

            if (previousBalance >= 0 && Balance < 0)
            {
                decimal usedCredit = Math.Abs(Balance);
                decimal remainingCredit = CreditLimit - usedCredit;

                if (CreditStarted != null)
                {
                    CreditStarted("Розпочато використання кредитних коштів! Використано кредиту: " + usedCredit + " грн. Залишок кредитного ліміту: " + remainingCredit + " грн.");
                }
            }
        }

        public void ChangePin(string oldPin, string newPin)
        {
            if (!VerifyPin(oldPin))
            {
                throw new InvalidOperationException("Невірний поточний PIN-код.");
            }

            if (!IsValidPin(newPin))
            {
                throw new ArgumentException("Новий PIN повинен складатися з 4 цифр.");
            }

            if (oldPin == newPin)
            {
                throw new ArgumentException("Новий PIN повинен відрізнятися від старого.");
            }

            Pin = newPin;

            if (PinChanged != null)
            {
                PinChanged("PIN-код картки успішно змінено.");
            }
        }

        public void DisplayInfo()
        {
            decimal ownMoney = Balance > 0 ? Balance : 0;
            decimal usedCredit = Balance < 0 ? Math.Abs(Balance) : 0;

            Console.WriteLine("Номер картки: " + CardNumber);
            Console.WriteLine("Власник: " + OwnerFullName);
            Console.WriteLine("Термін дії: " + ExpiryDate.ToString("MM'/'yyyy"));
            Console.WriteLine("Власні кошти: " + ownMoney + " грн");
            Console.WriteLine("Використано кредитних коштів: " + usedCredit + " грн");
            Console.WriteLine("Кредитний ліміт: " + CreditLimit + " грн");
            Console.WriteLine("Заданий ліміт суми: " + TargetLimit + " грн");
        }
    }

    class Program
    {
        public static void ShowCurrentTime()
        {
            Console.WriteLine("Поточний час: " + DateTime.Now.ToString("HH:mm:ss"));
        }

        public static void ShowCurrentDate()
        {
            Console.WriteLine("Поточна дата: " + DateTime.Now.ToString("dd.MM.yyyy"));
        }

        public static void ShowCurrentDayOfWeek()
        {
            string dayName = GetUkrainianDayOfWeek(DateTime.Now.DayOfWeek);
            Console.WriteLine("Поточний день тижня: " + dayName);
        }

        public static string GetUkrainianDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Понеділок";
                case DayOfWeek.Tuesday:
                    return "Вівторок";
                case DayOfWeek.Wednesday:
                    return "Середа";
                case DayOfWeek.Thursday:
                    return "Четвер";
                case DayOfWeek.Friday:
                    return "П'ятниця";
                case DayOfWeek.Saturday:
                    return "Субота";
                case DayOfWeek.Sunday:
                    return "Неділя";
                default:
                    return day.ToString();
            }
        }

        public static bool IsPositive(double value)
        {
            return value > 0;
        }

        public static bool IsWeekend(DayOfWeek day)
        {
            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
        }

        public static double CalculateTriangleArea(double triangleBase, double height)
        {
            return 0.5 * triangleBase * height;
        }

        public static double CalculateRectangleArea(double width, double height)
        {
            return width * height;
        }

        public static void HandleAccountReplenished(string message)
        {
            Console.WriteLine("[Подія - Поповнення] " + message);
        }

        public static void HandleMoneySpent(string message)
        {
            Console.WriteLine("[Подія - Витрата] " + message);
        }

        public static void HandleCreditStarted(string message)
        {
            Console.WriteLine("[Подія - Кредит] " + message);
        }

        public static void HandleTargetLimitReached(string message)
        {
            Console.WriteLine("[Подія - Ліміт суми] " + message);
        }

        public static void HandlePinChanged(string message)
        {
            Console.WriteLine("[Подія - Зміна PIN] " + message);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.WriteLine("--- ЗАВДАННЯ 1: Методи з делегатами Action, Predicate, Func ---");

                Action timeAction = ShowCurrentTime;
                Action dateAction = ShowCurrentDate;
                Action dayAction = ShowCurrentDayOfWeek;

                timeAction();
                dateAction();
                dayAction();

                Predicate<DayOfWeek> isWeekendPredicate = IsWeekend;
                if (isWeekendPredicate(DateTime.Now.DayOfWeek))
                {
                    Console.WriteLine("Статус дня: Вихідний день");
                }
                else
                {
                    Console.WriteLine("Статус дня: Робочий день");
                }

                Console.WriteLine();

                Predicate<double> isPositivePredicate = IsPositive;
                Func<double, double, double> triangleAreaFunc = CalculateTriangleArea;
                Func<double, double, double> rectangleAreaFunc = CalculateRectangleArea;

                double triangleBase = 10.0;
                double triangleHeight = 6.0;

                if (isPositivePredicate(triangleBase) && isPositivePredicate(triangleHeight))
                {
                    double triangleArea = triangleAreaFunc(triangleBase, triangleHeight);
                    Console.WriteLine("Площа трикутника (основа = " + triangleBase + ", висота = " + triangleHeight + "): " + triangleArea);
                }
                else
                {
                    Console.WriteLine("Некоректні розміри трикутника.");
                }

                double rectWidth = 8.0;
                double rectHeight = 5.0;

                if (isPositivePredicate(rectWidth) && isPositivePredicate(rectHeight))
                {
                    double rectArea = rectangleAreaFunc(rectWidth, rectHeight);
                    Console.WriteLine("Площа прямокутника (ширина = " + rectWidth + ", висота = " + rectHeight + "): " + rectArea);
                }
                else
                {
                    Console.WriteLine("Некоректні розміри прямокутника.");
                }

                Console.WriteLine();
                Console.WriteLine("--- ЗАВДАННЯ 2: Клас «Кредитна картка» та події ---");

                CreditCard card = new CreditCard(
                    "4149 4991 1234 5678",
                    "Шевченко Тарас Григорович",
                    new DateTime(2029, 8, 31),
                    "1234",
                    5000m,
                    2000m,
                    4000m
                );

                card.AccountReplenished += HandleAccountReplenished;
                card.MoneySpent += HandleMoneySpent;
                card.CreditStarted += HandleCreditStarted;
                card.TargetLimitReached += HandleTargetLimitReached;
                card.PinChanged += HandlePinChanged;

                Console.WriteLine("Початкова інформація про картку:");
                card.DisplayInfo();
                Console.WriteLine();

                card.Deposit(2500m);
                Console.WriteLine();

                card.Withdraw(1500m);
                Console.WriteLine();

                card.Withdraw(4200m);
                Console.WriteLine();

                card.ChangePin("1234", "9876");
                Console.WriteLine();

                Console.WriteLine("Оновлена інформація про картку:");
                card.DisplayInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
