using System;
using System.Collections.Generic;

namespace PassportApp
{
    public class ForeignPassport
    {
        public const int MaxValidityPeriodYears = 10;
        public const int PassportNumberLength = 9;

        private static List<string> allowedCountryCodes;
        public static int CreatedObjectsCount;

        static ForeignPassport()
        {
            allowedCountryCodes = new List<string>()
            {
                "UKR", "USA", "DEU", "FRA", "GBR", "POL", "CAN"
            };

            if (allowedCountryCodes.Count == 0)
            {
                throw new Exception("Список кодов стран пуст.");
            }

            CreatedObjectsCount = 0;
        }

        public readonly string Id;

        private string passportNumber;
        private string fullName;
        private DateTime birthDate;
        private DateTime issueDate;
        private DateTime expiryDate;
        private string issuingAuthority;
        private string countryCode;

        public string PassportNumber
        {
            get { return passportNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(PassportNumber), "Номер паспорта не может быть пустым.");
                }

                if (value.Length != PassportNumberLength)
                {
                    throw new ArgumentException("Номер паспорта должен содержать ровно 9 символов.", nameof(PassportNumber));
                }

                passportNumber = value;
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(FullName), "ФИО не может быть пустым.");
                }

                fullName = value;
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException(nameof(BirthDate), "Дата рождения не может быть в будущем.");
                }

                birthDate = value;
            }
        }

        public DateTime IssueDate
        {
            get { return issueDate; }
            set
            {
                if (value < birthDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(IssueDate), "Дата выдачи не может быть раньше даты рождения.");
                }

                if (value > DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException(nameof(IssueDate), "Дата выдачи не может быть в будущем.");
                }

                issueDate = value;
            }
        }

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                if (value <= issueDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(ExpiryDate), "Дата окончания не может быть раньше даты выдачи.");
                }

                if (value > issueDate.AddYears(MaxValidityPeriodYears))
                {
                    throw new ArgumentOutOfRangeException(nameof(ExpiryDate), "Срок действия паспорта не может превышать 10 лет.");
                }

                expiryDate = value;
            }
        }

        public string IssuingAuthority
        {
            get { return issuingAuthority; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(IssuingAuthority), "Орган выдачи не может быть пустым.");
                }

                issuingAuthority = value;
            }
        }

        public string CountryCode
        {
            get { return countryCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(CountryCode), "Код страны не может быть пустым.");
                }

                if (!allowedCountryCodes.Contains(value.ToUpper()))
                {
                    throw new ArgumentException("Недопустимый код страны.", nameof(CountryCode));
                }

                countryCode = value.ToUpper();
            }
        }

        public ForeignPassport()
        {
            Id = Guid.NewGuid().ToString();

            birthDate = new DateTime(2000, 1, 1);
            issueDate = DateTime.Now.AddYears(-1);
            expiryDate = issueDate.AddYears(10);

            PassportNumber = "123456789";
            FullName = "Иванов Иван Иванович";
            IssuingAuthority = "орган 1234";
            CountryCode = "UKR";

            CreatedObjectsCount++;
        }

        public ForeignPassport(string passportNumber, string fullName, DateTime birthDate, DateTime issueDate, DateTime expiryDate, string issuingAuthority, string countryCode)
        {
            Id = Guid.NewGuid().ToString();

            BirthDate = birthDate;
            PassportNumber = passportNumber;
            FullName = fullName;
            IssuingAuthority = issuingAuthority;
            CountryCode = countryCode;
            IssueDate = issueDate;
            ExpiryDate = expiryDate;

            CreatedObjectsCount++;
        }

        public void PrintInfo()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Паспорт: " + PassportNumber + " (" + CountryCode + ")");
            Console.WriteLine("ФИО: " + FullName);
            Console.WriteLine("Дата рождения: " + BirthDate.ToShortDateString());
            Console.WriteLine("Дата выдачи: " + IssueDate.ToShortDateString());
            Console.WriteLine("Действителен до: " + ExpiryDate.ToShortDateString());
            Console.WriteLine("Выдал: " + IssuingAuthority);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ForeignPassport passport1 = new ForeignPassport();
                passport1.PrintInfo();

                ForeignPassport passport2 = new ForeignPassport("987654321", "Петров Петр Петрович", new DateTime(1995, 5, 15), new DateTime(2020, 1, 10), new DateTime(2030, 1, 10), "орган 5678", "UKR");
                passport2.PrintInfo();

                Console.WriteLine("Всего создано паспортов: " + ForeignPassport.CreatedObjectsCount);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            try
            {
                ForeignPassport badPassport = new ForeignPassport("123", "", new DateTime(1990, 1, 1), new DateTime(2020, 1, 1), new DateTime(2035, 1, 1), "орган", "ABC");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Ошибка с пустым значением: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Ошибка с датой или сроком: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка в данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Другая ошибка: " + ex.Message);
            }
        }
    }
}