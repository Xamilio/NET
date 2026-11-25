using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqFirmsTasks
{
    public class Employee
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }

        public Employee(string fullName, string position, string phone, string email, decimal salary)
        {
            FullName = fullName;
            Position = position;
            Phone = phone;
            Email = email;
            Salary = salary;
        }

        public string GetFirstName()
        {
            string[] parts = FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0] : FullName;
        }

        public string GetInfo()
        {
            return FullName + " | Посада: " + Position + " | Тел.: " + Phone + " | Email: " + Email + " | Зарплата: " + Salary + " $";
        }
    }

    public class Firm
    {
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public string BusinessProfile { get; set; }
        public string DirectorFullName { get; set; }
        public int EmployeesCount { get; set; }
        public string Address { get; set; }
        public Employee[] Employees { get; set; }

        public Firm(
            string name,
            DateTime foundationDate,
            string businessProfile,
            string directorFullName,
            int employeesCount,
            string address,
            Employee[] employees)
        {
            Name = name;
            FoundationDate = foundationDate;
            BusinessProfile = businessProfile;
            DirectorFullName = directorFullName;
            EmployeesCount = employeesCount;
            Address = address;
            Employees = employees;
        }

        public string GetDirectorLastName()
        {
            string[] parts = DirectorFullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[parts.Length - 1] : DirectorFullName;
        }

        public string GetInfo()
        {
            return "«" + Name + "» | Профіль: " + BusinessProfile +
                   " | Заснована: " + FoundationDate.ToString("dd.MM.yyyy") +
                   " | Директор: " + DirectorFullName +
                   " | Працівників: " + EmployeesCount +
                   " | Адреса: " + Address;
        }
    }

    class Program
    {
        public static void PrintFirms(string title, IEnumerable<Firm> firms)
        {
            Console.WriteLine("► " + title + ":");
            List<Firm> list = firms.ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("  (не знайдено)");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("  " + (i + 1) + ". " + list[i].GetInfo());
                }
            }
            Console.WriteLine();
        }

        public static void PrintEmployees(string title, IEnumerable<Employee> employees)
        {
            Console.WriteLine("► " + title + ":");
            List<Employee> list = employees.ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("  (не знайдено)");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("  " + (i + 1) + ". " + list[i].GetInfo());
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            DateTime today = DateTime.Now.Date;

            Firm[] firms = new Firm[]
            {
                new Firm(
                    "White Food Market",
                    today.AddYears(-5),
                    "Харчова промисловість",
                    "John White",
                    150,
                    "Лондон, Baker Street 221B",
                    new Employee[]
                    {
                        new Employee("Lionel Carter", "Менеджер", "234-55-11", "diana.carter@whitefood.com", 3200m),
                        new Employee("Sarah Connor", "Технолог", "441-10-22", "sarah@whitefood.com", 2400m),
                        new Employee("David Brown", "Менеджер", "239-88-99", "david@whitefood.com", 2900m)
                    }
                ),
                new Firm(
                    "Bright Marketing Agency",
                    today.AddDays(-123),
                    "Маркетинг",
                    "Emily Stone",
                    85,
                    "Київ, вул. Хрещатик 22",
                    new Employee[]
                    {
                        new Employee("Олена Коваль", "Маркетолог", "050-123-45-67", "olena@bright.ua", 1900m),
                        new Employee("Lionel Kovalenko", "Менеджер", "231-00-44", "dimon@bright.ua", 2700m)
                    }
                ),
                new Firm(
                    "White Star Solutions",
                    today.AddYears(-3),
                    "IT",
                    "Robert Black",
                    240,
                    "Лондон, Oxford Street 15",
                    new Employee[]
                    {
                        new Employee("James Wilson", "Senior Developer", "237-77-01", "dirk.wilson@whitestar.uk", 5200m),
                        new Employee("Lionel Andres", "Team Lead", "555-12-34", "lionel@whitestar.uk", 5800m),
                        new Employee("Anna Taylor", "Менеджер", "449-01-02", "anna@whitestar.uk", 3500m)
                    }
                ),
                new Firm(
                    "Healthy Food Express",
                    today.AddYears(-1),
                    "Маркетинг",
                    "Michael White",
                    420,
                    "Львів, пл. Ринок 10",
                    new Employee[]
                    {
                        new Employee("Ігор Мельник", "Аналітик", "230-99-88", "igor@healthyfood.ua", 2100m),
                        new Employee("Марія Бойко", "Дизайнер", "067-999-11-22", "diana.boyko@healthyfood.ua", 2300m)
                    }
                )
            };

            Console.WriteLine("=====================================================================");
            Console.WriteLine("ЗАВДАННЯ 1: Запити LINQ (синтаксис запитів)");
            Console.WriteLine("=====================================================================\n");

            var q1All = from f in firms
                        select f;
            PrintFirms("1. Усі фірми", q1All);

            var q1Food = from f in firms
                         where f.Name.Contains("Food")
                         select f;
            PrintFirms("2. Фірми зі словом «Food» у назві", q1Food);

            var q1Marketing = from f in firms
                              where f.BusinessProfile == "Маркетинг"
                              select f;
            PrintFirms("3. Фірми у галузі маркетингу", q1Marketing);

            var q1MarketingOrIt = from f in firms
                                  where f.BusinessProfile == "Маркетинг" || f.BusinessProfile == "IT"
                                  select f;
            PrintFirms("4. Фірми у галузі маркетингу або IT", q1MarketingOrIt);

            var q1MoreThan100 = from f in firms
                                where f.EmployeesCount > 100
                                select f;
            PrintFirms("5. Фірми з кількістю працівників більше 100", q1MoreThan100);

            var q1Range100To300 = from f in firms
                                  where f.EmployeesCount >= 100 && f.EmployeesCount <= 300
                                  select f;
            PrintFirms("6. Фірми з кількістю працівників від 100 до 300", q1Range100To300);

            var q1London = from f in firms
                           where f.Address.Contains("Лондон")
                           select f;
            PrintFirms("7. Фірми, які знаходяться в Лондоні", q1London);

            var q1DirectorWhite = from f in firms
                                  where f.GetDirectorLastName() == "White"
                                  select f;
            PrintFirms("8. Фірми, в яких прізвище директора White", q1DirectorWhite);

            var q1OlderThan2Years = from f in firms
                                    where f.FoundationDate < today.AddYears(-2)
                                    select f;
            PrintFirms("9. Фірми, засновані більше двох років тому", q1OlderThan2Years);

            var q1Exactly123Days = from f in firms
                                   where (today - f.FoundationDate.Date).Days == 123
                                   select f;
            PrintFirms("10. Фірми, з дня заснування яких минуло 123 дні", q1Exactly123Days);

            var q1BlackAndWhite = from f in firms
                                  where f.GetDirectorLastName() == "Black" && f.Name.Contains("White")
                                  select f;
            PrintFirms("11. Фірми: прізвище директора Black і назва містить «White»", q1BlackAndWhite);

            Console.WriteLine("=====================================================================");
            Console.WriteLine("ЗАВДАННЯ 2: Ті самі запити через методи розширень LINQ");
            Console.WriteLine("=====================================================================\n");

            PrintFirms("1. Усі фірми (Select)", firms.Select(f => f));
            PrintFirms("2. Назва містить «Food» (Where)", firms.Where(f => f.Name.Contains("Food")));
            PrintFirms("3. Галузь маркетингу (Where)", firms.Where(f => f.BusinessProfile == "Маркетинг"));
            PrintFirms("4. Галузь маркетингу або IT (Where)", firms.Where(f => f.BusinessProfile == "Маркетинг" || f.BusinessProfile == "IT"));
            PrintFirms("5. Кількість працівників > 100 (Where)", firms.Where(f => f.EmployeesCount > 100));
            PrintFirms("6. Кількість працівників 100..300 (Where)", firms.Where(f => f.EmployeesCount >= 100 && f.EmployeesCount <= 300));
            PrintFirms("7. Знаходяться в Лондоні (Where)", firms.Where(f => f.Address.Contains("Лондон")));
            PrintFirms("8. Прізвище директора White (Where)", firms.Where(f => f.GetDirectorLastName() == "White"));
            PrintFirms("9. Засновані більше 2 років тому (Where)", firms.Where(f => f.FoundationDate < today.AddYears(-2)));
            PrintFirms("10. З дня заснування минуло 123 дні (Where)", firms.Where(f => (today - f.FoundationDate.Date).Days == 123));
            PrintFirms("11. Директор Black і назва містить «White» (Where)", firms.Where(f => f.GetDirectorLastName() == "Black" && f.Name.Contains("White")));

            Console.WriteLine("=====================================================================");
            Console.WriteLine("ЗАВДАННЯ 3: Запити по працівниках фірм");
            Console.WriteLine("=====================================================================\n");

            string targetFirmName = "White Star Solutions";
            decimal minSalary = 3000m;

            var empOfFirm = from f in firms
                            where f.Name == targetFirmName
                            from e in f.Employees
                            select e;
            PrintEmployees("1. Усі працівники фірми «" + targetFirmName + "»", empOfFirm);

            var empSalaryGreater = from f in firms
                                   where f.Name == targetFirmName
                                   from e in f.Employees
                                   where e.Salary > minSalary
                                   select e;
            PrintEmployees("2. Працівники фірми «" + targetFirmName + "» із зарплатою > " + minSalary + " $", empSalaryGreater);

            var empManagers = from f in firms
                              from e in f.Employees
                              where e.Position == "Менеджер"
                              select e;
            PrintEmployees("3. Працівники усіх фірм на посаді «Менеджер»", empManagers);

            var empPhone23 = from f in firms
                             from e in f.Employees
                             where e.Phone.StartsWith("23")
                             select e;
            PrintEmployees("4. Працівники, у яких телефон починається з «23»", empPhone23);

            var empEmailDi = from f in firms
                             from e in f.Employees
                             where e.Email.StartsWith("di", StringComparison.OrdinalIgnoreCase)
                             select e;
            PrintEmployees("5. Працівники, у яких Email починається з «di»", empEmailDi);

            var empLionel = from f in firms
                            from e in f.Employees
                            where e.GetFirstName() == "Lionel"
                            select e;
            PrintEmployees("6. Працівники з ім'ям Lionel", empLionel);
        }
    }
}
