using System;

/*class Website
{
    private string name;
    private string path;
    private string description;
    private string ipAddress;

    public string Name
    {
        get => name;
        set => name = string.IsNullOrWhiteSpace(value) ? "Невідомо" : value;
    }

    public string Path
    {
        get => path;
        set => path = string.IsNullOrWhiteSpace(value) ? "Невідомо" : value;
    }

    public string Description
    {
        get => description;
        set => description = string.IsNullOrWhiteSpace(value) ? "Без опису" : value;
    }

    public string IpAddress
    {
        get => ipAddress;
        set => ipAddress = string.IsNullOrWhiteSpace(value) ? "0.0.0.0" : value;
    }

    public Website() { }

    public Website(string name, string path, string description, string ipAddress)
    {
        Name = name;
        Path = path;
        Description = description;
        IpAddress = ipAddress;
    }

    public void InputData()
    {
        Console.WriteLine("=== Введення даних про Веб-сайт ===");
        Console.Write("Введіть назву сайту: ");
        Name = Console.ReadLine();

        Console.Write("Введіть шлях до сайту (URL): ");
        Path = Console.ReadLine();

        Console.Write("Введіть опис сайту: ");
        Description = Console.ReadLine();

        Console.Write("Введіть IP-адресу сайту: ");
        IpAddress = Console.ReadLine();
    }
    public void PrintInfo()
    {
        Console.WriteLine("\n--- Інформація про Веб-сайт ---");
        Console.WriteLine($"Назва: {Name}");
        Console.WriteLine($"Шлях: {Path}");
        Console.WriteLine($"Опис: {Description}");
        Console.WriteLine($"IP-адреса: {IpAddress}");
    }
    }*/
    
/*    class Journal
{
    private string name;
    private int foundationYear;
    private string description;
    private string phone;
    private string email;

    public string Name
    {
        get => name;
        set => name = string.IsNullOrWhiteSpace(value) ? "Невідомо" : value;
    }

    public int FoundationYear
    {
        get => foundationYear;
        set => foundationYear = (value > 0 && value <= DateTime.Now.Year) ? value : 2000;
    }

    public string Description
    {
        get => description;
        set => description = string.IsNullOrWhiteSpace(value) ? "Без опису" : value;
    }

    public string Phone
    {
        get => phone;
        set => phone = string.IsNullOrWhiteSpace(value) ? "Не вказано" : value;
    }

    public string Email
    {
        get => email;
        set => email = string.IsNullOrWhiteSpace(value) ? "Не вказано" : value;
    }

    public Journal() { }

    public Journal(string name, int foundationYear, string description, string phone, string email)
    {
        Name = name;
        FoundationYear = foundationYear;
        Description = description;
        Phone = phone;
        Email = email;
    }

    public void InputData()
    {
        Console.WriteLine("=== Введення даних про Журнал ===");
        Console.Write("Введіть назву журналу: ");
        Name = Console.ReadLine();

        Console.Write("Введіть рік заснування: ");
        if (int.TryParse(Console.ReadLine(), out int year))
        {
            FoundationYear = year;
        }

        Console.Write("Введіть опис журналу: ");
        Description = Console.ReadLine();

        Console.Write("Введіть контактний телефон: ");
        Phone = Console.ReadLine();

        Console.Write("Введіть email: ");
        Email = Console.ReadLine();
    }

    public void PrintInfo()
    {
        Console.WriteLine("\n--- Інформація про Журнал ---");
        Console.WriteLine($"Назва: {Name}");
        Console.WriteLine($"Рік заснування: {FoundationYear}");
        Console.WriteLine($"Опис: {Description}");
        Console.WriteLine($"Телефон: {Phone}");
        Console.WriteLine($"Email: {Email}");
    }
}*/
class Store
{
    private string name;
    private string address;
    private string profileDescription;
    private string phone;
    private string email;

    public string Name
    {
        get => name;
        set => name = string.IsNullOrWhiteSpace(value) ? "Невідомо" : value;
    }

    public string Address
    {
        get => address;
        set => address = string.IsNullOrWhiteSpace(value) ? "Не вказано" : value;
    }

    public string ProfileDescription
    {
        get => profileDescription;
        set => profileDescription = string.IsNullOrWhiteSpace(value) ? "Без опису" : value;
    }

    public string Phone
    {
        get => phone;
        set => phone = string.IsNullOrWhiteSpace(value) ? "Не вказано" : value;
    }

    public string Email
    {
        get => email;
        set => email = string.IsNullOrWhiteSpace(value) ? "Не вказано" : value;
    }

    public Store() { }

    public Store(string name, string address, string profileDescription, string phone, string email)
    {
        Name = name;
        Address = address;
        ProfileDescription = profileDescription;
        Phone = phone;
        Email = email;
    }

    public void InputData()
    {
        Console.WriteLine("=== Введення даних про Магазин ===");
        Console.Write("Введіть назву магазину: ");
        Name = Console.ReadLine();

        Console.Write("Введіть адресу магазину: ");
        Address = Console.ReadLine();

        Console.Write("Введіть опис профілю магазину: ");
        ProfileDescription = Console.ReadLine();

        Console.Write("Введіть контактний телефон: ");
        Phone = Console.ReadLine();

        Console.Write("Введіть email: ");
        Email = Console.ReadLine();
    }

    public void PrintInfo()
    {
        Console.WriteLine("\n--- Інформація про Магазин ---");
        Console.WriteLine($"Назва: {Name}");
        Console.WriteLine($"Адреса: {Address}");
        Console.WriteLine($"Профіль: {ProfileDescription}");
        Console.WriteLine($"Телефон: {Phone}");
        Console.WriteLine($"Email: {Email}");
    }
}


class Program
{
    static void Main()
    {
        /*Website site = new Website();
        site.InputData();
        site.PrintInfo();

        Console.WriteLine("\n" + new string('=', 40) + "\n");
        
        Journal journal = new Journal("IT Tech World", 2018, "Журнал про сучасний софт", "+380481234567", "info@itworld.ua");
        journal.PrintInfo();

        Console.WriteLine("\n" + new string('=', 40) + "\n");*/
        
        Store store = new Store();
        store.InputData();
        store.PrintInfo();
    }
}