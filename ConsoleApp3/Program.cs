using System;
using System.Collections.Generic;

namespace UniversitySystem
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }

        public Address(string street, string city)
        {
            if (string.IsNullOrEmpty(street))
            {
                throw new ArgumentNullException(nameof(street));
            }
            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            Street = street;
            City = city;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Адреса: м. " + City + ", вул. " + Street);
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Student(int id, string name)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Id = id;
            Name = name;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Студент [ID: " + Id + "] - " + Name);
        }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Teacher(int id, string name)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Id = id;
            Name = name;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Викладач [ID: " + Id + "] - " + Name);
        }
    }

    public class CourseModule
    {
        public string Title { get; set; }

        public CourseModule(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            Title = title;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("  - Модуль: " + Title);
        }
    }

    public class Course
    {
        public static int CreatedCoursesCount = 0;

        public string Title { get; set; }
        private List<CourseModule> modules;
        private List<Student> students;

        public Course(string title, List<string> moduleTitles)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            Title = title;
            modules = new List<CourseModule>();
            students = new List<Student>();

            if (moduleTitles != null)
            {
                foreach (string mTitle in moduleTitles)
                {
                    modules.Add(new CourseModule(mTitle));
                }
            }

            CreatedCoursesCount++;
        }

        public void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            foreach (Student s in students)
            {
                if (s.Id == student.Id)
                {
                    throw new InvalidOperationException("Студент вже зарахований.");
                }
            }

            students.Add(student);
            Console.WriteLine("Студента " + student.Name + " додано на курс " + Title + ".");
        }

        public void RemoveStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            if (students.Remove(student))
            {
                Console.WriteLine("Студента " + student.Name + " видалено з курсу " + Title + ".");
            }
            else
            {
                Console.WriteLine("Студента " + student.Name + " не знайдено.");
            }
        }

        public Student FindStudentByName(string name)
        {
            foreach (Student s in students)
            {
                if (s.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return s;
                }
            }
            return null;
        }

        public Student FindStudentById(int id)
        {
            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    return s;
                }
            }
            return null;
        }

        public void DisplayStudents()
        {
            Console.WriteLine("Студенти курсу \"" + Title + "\":");
            if (students.Count == 0)
            {
                Console.WriteLine("  На курсі немає студентів.");
                return;
            }

            foreach (Student s in students)
            {
                Console.Write("  ");
                s.DisplayInfo();
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Курс: " + Title);
            Console.WriteLine("Навчальні модулі курсу:");
            foreach (CourseModule m in modules)
            {
                m.DisplayInfo();
            }
            DisplayStudents();
        }
    }

    public class University
    {
        public string Name { get; set; }
        public Address Address { get; private set; }
        private List<Teacher> teachers;

        public University(string name, string street, string city)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Address = new Address(street, city);
            teachers = new List<Teacher>();
        }

        public void AddTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }

            teachers.Add(teacher);
            Console.WriteLine("Викладача " + teacher.Name + " додано до університету " + Name + ".");
        }

        public void DisplayTeachers()
        {
            Console.WriteLine("Викладачі університету " + Name + ":");
            if (teachers.Count == 0)
            {
                Console.WriteLine("  Викладачів немає.");
                return;
            }

            foreach (Teacher t in teachers)
            {
                Console.Write("  ");
                t.DisplayInfo();
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine("=== УНІВЕРСИТЕТ: " + Name + " ===");
            Address.DisplayInfo();
            DisplayTeachers();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                Student student1 = new Student(1, "Олексій Іваненко");
                Student student2 = new Student(2, "Марія Петренко");
                Student student3 = new Student(3, "Іван Сидоренко");

                Teacher teacher1 = new Teacher(101, "проф. Шевченко Т. Г.");
                Teacher teacher2 = new Teacher(102, "доц. Бондаренко О. В.");

                University uni = new University("КНУ ім. Тараса Шевченка", "Володимирська 60", "Київ");

                uni.AddTeacher(teacher1);
                uni.AddTeacher(teacher2);

                Console.WriteLine();
                uni.DisplayInfo();
                Console.WriteLine();

                List<string> csharpModules = new List<string>() 
                { 
                    "Основи ООП", 
                    "Винятки та колекції", 
                    "Агрегація і композиція" 
                };
                
                Course course1 = new Course("Програмування C#", csharpModules);

                List<string> mathModules = new List<string>() 
                { 
                    "Матриці", 
                    "Векторний аналіз" 
                };

                Course course2 = new Course("Вища математика", mathModules);

                Console.WriteLine("Створено курсів: " + Course.CreatedCoursesCount);
                Console.WriteLine();

                course1.AddStudent(student1);
                course1.AddStudent(student2);

                course2.AddStudent(student2);
                course2.AddStudent(student3);

                Console.WriteLine();
                course1.DisplayInfo();
                Console.WriteLine();

                Student foundByName = course1.FindStudentByName("Марія Петренко");
                if (foundByName != null)
                {
                    Console.WriteLine("Знайдено за ім'ям: " + foundByName.Name);
                }

                Student foundById = course1.FindStudentById(1);
                if (foundById != null)
                {
                    Console.WriteLine("Знайдено за ID: " + foundById.Name);
                }

                Console.WriteLine();

                try
                {
                    course1.AddStudent(student1);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Помилка повторного додавання: " + ex.Message);
                }

                Console.WriteLine();

                course1 = null;

                Console.WriteLine("Студенти після видалення курсу:");
                student1.DisplayInfo();
                student2.DisplayInfo();

                Console.WriteLine();
                teacher1.DisplayInfo();

                uni = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}