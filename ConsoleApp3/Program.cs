using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IteratorsTasks
{
    public class Alphabet : IEnumerable<char>
    {
        private char[] letters;

        public Alphabet()
        {
            letters = new char[26];
            for (int i = 0; i < 26; i++)
            {
                letters[i] = (char)('A' + i);
            }
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = 0; i < letters.Length; i++)
            {
                yield return letters[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Resident
    {
        private string fullName;
        private int age;

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ПІБ мешканця не може бути порожнім.");
                }
                fullName = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Вік мешканця не може бути від'ємним.");
                }
                age = value;
            }
        }

        public Resident(string fullName, int age)
        {
            this.fullName = string.Empty;
            FullName = fullName;
            Age = age;
        }

        public string GetInfo()
        {
            return FullName + " (" + Age + " р.)";
        }
    }

    public class Apartment : IEnumerable<Resident>
    {
        private int apartmentNumber;
        private int roomsCount;
        private List<Resident> residents;

        public int ApartmentNumber
        {
            get { return apartmentNumber; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Номер квартири повинен бути більшим за нуль.");
                }
                apartmentNumber = value;
            }
        }

        public int RoomsCount
        {
            get { return roomsCount; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Кількість кімнат повинна бути більшою за нуль.");
                }
                roomsCount = value;
            }
        }

        public Apartment(int apartmentNumber, int roomsCount)
        {
            ApartmentNumber = apartmentNumber;
            RoomsCount = roomsCount;
            residents = new List<Resident>();
        }

        public void AddResident(Resident resident)
        {
            if (resident == null)
            {
                throw new ArgumentNullException(nameof(resident), "Мешканець не може бути null.");
            }

            residents.Add(resident);
        }

        public string GetInfo()
        {
            return "Квартира №" + ApartmentNumber + " (" + RoomsCount + "-кімнатна, мешканців: " + residents.Count + ")";
        }

        public IEnumerator<Resident> GetEnumerator()
        {
            for (int i = 0; i < residents.Count; i++)
            {
                yield return residents[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class House : IEnumerable<Apartment>
    {
        private string address;
        private List<Apartment> apartments;

        public string Address
        {
            get { return address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Адреса будинку не може бути порожньою.");
                }
                address = value;
            }
        }

        public House(string address)
        {
            this.address = string.Empty;
            Address = address;
            apartments = new List<Apartment>();
        }

        public void AddApartment(Apartment apartment)
        {
            if (apartment == null)
            {
                throw new ArgumentNullException(nameof(apartment), "Квартира не може бути null.");
            }

            apartments.Add(apartment);
        }

        public IEnumerator<Apartment> GetEnumerator()
        {
            for (int i = 0; i < apartments.Count; i++)
            {
                yield return apartments[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Car
    {
        private string brand;
        private string model;
        private int year;
        private string licensePlate;

        public string Brand
        {
            get { return brand; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Марка автомобіля не може бути порожньою.");
                }
                brand = value;
            }
        }

        public string Model
        {
            get { return model; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Модель автомобіля не може бути порожньою.");
                }
                model = value;
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (value < 1886 || value > DateTime.Now.Year + 1)
                {
                    throw new ArgumentException("Некоректний рік випуску автомобіля.");
                }
                year = value;
            }
        }

        public string LicensePlate
        {
            get { return licensePlate; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Номерний знак не може бути порожнім.");
                }
                licensePlate = value;
            }
        }

        public Car(string brand, string model, int year, string licensePlate)
        {
            this.brand = string.Empty;
            this.model = string.Empty;
            this.licensePlate = string.Empty;

            Brand = brand;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
        }

        public string GetInfo()
        {
            return Brand + " " + Model + " (" + Year + " р., номер: " + LicensePlate + ")";
        }
    }

    public class Garage : IEnumerable<Car>
    {
        private string name;
        private List<Car> cars;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва гаража не може бути порожньою.");
                }
                name = value;
            }
        }

        public Garage(string name)
        {
            this.name = string.Empty;
            Name = name;
            cars = new List<Car>();
        }

        public void AddCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Автомобіль не може бути null.");
            }

            cars.Add(car);
        }

        public IEnumerator<Car> GetEnumerator()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                yield return cars[i];
            }
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
                Console.WriteLine("--- ЗАВДАННЯ 1: Клас «Алфавіт» (foreach) ---");

                Alphabet alphabet = new Alphabet();
                Console.Write("Літери англійського алфавіту: ");

                foreach (char letter in alphabet)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("--- ЗАВДАННЯ 2: Класи «Будинок» і «Квартира» (foreach) ---");

                House house = new House("м. Київ, вул. Хрещатик, 25");

                Apartment apt1 = new Apartment(10, 2);
                apt1.AddResident(new Resident("Мельник Олександр Петрович", 34));
                apt1.AddResident(new Resident("Мельник Ірина Сергіївна", 31));

                Apartment apt2 = new Apartment(11, 3);
                apt2.AddResident(new Resident("Бондаренко Максим Ігорович", 42));
                apt2.AddResident(new Resident("Бондаренко Наталія Олександрівна", 39));
                apt2.AddResident(new Resident("Бондаренко Софія Максимівна", 12));

                Apartment apt3 = new Apartment(12, 1);
                apt3.AddResident(new Resident("Ткаченко Дмитро Вікторович", 27));

                house.AddApartment(apt1);
                house.AddApartment(apt2);
                house.AddApartment(apt3);

                Console.WriteLine("Будинок за адресою: " + house.Address);

                foreach (Apartment apartment in house)
                {
                    Console.WriteLine("  " + apartment.GetInfo() + ":");

                    foreach (Resident resident in apartment)
                    {
                        Console.WriteLine("    - " + resident.GetInfo());
                    }
                }

                Console.WriteLine();
                Console.WriteLine("--- ЗАВДАННЯ 3: Класи «Гараж» та «Автомобіль» (foreach) ---");

                Garage garage = new Garage("Центральний автопарк");
                garage.AddCar(new Car("Toyota", "Camry", 2022, "AA 7788 КІ"));
                garage.AddCar(new Car("BMW", "X5", 2021, "КА 4500 НВ"));
                garage.AddCar(new Car("Volkswagen", "Passat", 2020, "ВС 1290 АТ"));

                Console.WriteLine("Гараж: «" + garage.Name + "»");

                foreach (Car car in garage)
                {
                    Console.WriteLine("  - " + car.GetInfo());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
