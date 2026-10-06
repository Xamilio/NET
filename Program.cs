internal class Program
{
    static void Main(string[] args)
    {
        //Console.Write("Введите 4 числа: ");
        //ulong x1 = Convert.ToUInt64(Console.ReadLine());
        //ulong x2 = Convert.ToUInt64(Console.ReadLine());
        //ulong x3 = Convert.ToUInt64(Console.ReadLine());
        //ulong x4 = Convert.ToUInt64(Console.ReadLine());

        //ulong y = x1 * 1000 + x2 * 100 + x3 * 10 + x4;

        //Console.WriteLine(y);

        Console.Write("Введите число: ");
        double x1 = Convert.ToUInt64(Console.ReadLine());
        Console.Write("Введите процент: ");
        double x2 = Convert.ToUInt64(Console.ReadLine());

        double y = (x1 / 100.0) * x2;
        Console.Write($"Процент от числа {y}");
    }
}   
