using System;

internal class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public Employee()
    {
        Name = "Unknown";
        Salary = 0;
    }

    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary < 0 ? 0 : salary;
    }

    public static Employee operator +(Employee emp, decimal value)
    {
        return new Employee(emp.Name, emp.Salary + value);
    }

    public static Employee operator -(Employee emp, decimal value)
    {
        decimal newSalary = emp.Salary - value;
        return new Employee(emp.Name, newSalary < 0 ? 0 : newSalary);
    }

    public static Employee operator ++(Employee emp)
    {
        emp.Salary += 500;
        return emp;
    }

    public static Employee operator --(Employee emp)
    {
        emp.Salary = emp.Salary - 500 < 0 ? 0 : emp.Salary - 500;
        return emp;
    }

    public static bool operator <(Employee left, Employee right)
    {
        return left.Salary < right.Salary;
    }

    public static bool operator >(Employee left, Employee right)
    {
        return left.Salary > right.Salary;
    }

    public static bool operator ==(Employee left, Employee right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Salary == right.Salary;
    }

    public static bool operator !=(Employee left, Employee right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        if (obj is Employee other)
        {
            return this.Salary == other.Salary;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Salary.GetHashCode();
    }

    public override string ToString()
    {
        return $"Співробітник: {Name}, Зарплата: {Salary}";
    }
}

internal class Matrix
{
    private int[,] _data;

    public int Rows { get; }
    public int Cols { get; }

    public Matrix(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        _data = new int[rows, cols];
    }

    public int this[int row, int col]
    {
        get
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
                return _data[row, col];
            throw new IndexOutOfRangeException("Некоректний індекс матриці!");
        }
        set
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
                _data[row, col] = value;
            else
                throw new IndexOutOfRangeException("Некоректний індекс матриці!");
        }
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new ArgumentException("Матриці повинні бути одного розміру!");

        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new ArgumentException("Матриці повинні бути одного розміру!");

        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    public static Matrix operator *(Matrix a, int number)
    {
        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                result[i, j] = a[i, j] * number;
            }
        }
        return result;
    }

    public static Matrix operator *(int number, Matrix a)
    {
        return a * number;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Cols != b.Rows)
            throw new ArgumentException("Неможливо помножити дані матриці!");

        Matrix result = new Matrix(a.Rows, b.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < b.Cols; j++)
            {
                for (int k = 0; k < a.Cols; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }

    public static bool operator ==(Matrix a, Matrix b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;

        if (a.Rows != b.Rows || a.Cols != b.Cols) return false;

        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                if (a[i, j] != b[i, j]) return false;
            }
        }
        return true;
    }

    public static bool operator !=(Matrix a, Matrix b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Matrix other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Rows, Cols, _data);
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                result += $"{_data[i, j]}\t";
            }
            result += "\n";
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        Employee emp1 = new Employee("Іван", 15000);
        Employee emp2 = new Employee("Олег", 12000);

        emp1 += 2000;
        Console.WriteLine(emp1);

        emp2++;
        Console.WriteLine(emp2);

        emp2--;
        Console.WriteLine(emp2);

        Console.WriteLine($"emp1 > emp2: {emp1 > emp2}");
        Console.WriteLine($"emp1 == emp2: {emp1 == emp2}");

        Matrix m1 = new Matrix(2, 2);
        m1[0, 0] = 1; m1[0, 1] = 2;
        m1[1, 0] = 3; m1[1, 1] = 4;

        Matrix m2 = new Matrix(2, 2);
        m2[0, 0] = 5; m2[0, 1] = 6;
        m2[1, 0] = 7; m2[1, 1] = 8;

        Console.WriteLine("\nMatrix 1:");
        Console.WriteLine(m1);

        Console.WriteLine("Matrix 2:");
        Console.WriteLine(m2);

        Console.WriteLine("m1 + m2:");
        Console.WriteLine(m1 + m2);

        Console.WriteLine("m1 * 2:");
        Console.WriteLine(m1 * 2);

        Console.WriteLine("m1 * m2:");
        Console.WriteLine(m1 * m2);

        Console.WriteLine($"m1 == m2: {m1 == m2}");
    }
}