using System; 

namespace ShapeInheritance 
{
    // Базовый класс Shape, представляющий геометрическую фигуру
    class Shape
    {
        // Виртуальный метод для вычисления площади, базовая реализация
        public virtual double Area()
        {
            return 0; // Возвращаем 0 как значение по умолчанию
        }

        // Виртуальный метод для вычисления периметра, базовая реализация
        public virtual double Perimeter()
        {
            return 0; // Возвращаем 0 как значение по умолчанию
        }
    }

    // Класс Circle, наследующий Shape, представляющий круг
    class Circle : Shape
    {
        // Приватное поле для хранения радиуса
        private double radius; // Радиус круга

        // Конструктор для инициализации радиуса
        public Circle(double r)
        {
            if (r > 0) // Проверяем, что радиус положительный
            {
                radius = r; // Устанавливаем радиус
            }
            else
            {
                throw new ArgumentException("Радиус должен быть положительным числом!"); // Выбрасываем исключение при некорректном вводе
            }
        }

        // Переопределённый метод для вычисления площади круга
        public override double Area()
        {
            return Math.PI * radius * radius; // Площадь = π * r²
        }

        // Переопределённый метод для вычисления периметра круга
        public override double Perimeter()
        {
            return 2 * Math.PI * radius; // Периметр (окружность) = 2 * π * r
        }
    }

    // Класс Rectangle, наследующий Shape, представляющий прямоугольник
    class Rectangle : Shape
    {
        // Приватные поля для хранения длины и ширины
        private double length; // Длина прямоугольника
        private double width; // Ширина прямоугольника

        // Конструктор для инициализации длины и ширины
        public Rectangle(double l, double w)
        {
            if (l > 0 && w > 0) // Проверяем, что длина и ширина положительные
            {
                length = l; // Устанавливаем длину
                width = w; // Устанавливаем ширину
            }
            else
            {
                throw new ArgumentException("Длина и ширина должны быть положительными числами!"); // Выбрасываем исключение при некорректном вводе
            }
        }

        // Переопределённый метод для вычисления площади прямоугольника
        public override double Area()
        {
            return length * width; // Площадь = длина * ширина
        }

        // Переопределённый метод для вычисления периметра прямоугольника
        public override double Perimeter()
        {
            return 2 * (length + width); // Периметр = 2 * (длина + ширина)
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа для вычисления площади и периметра геометрических фигур.");

            // Переменные для хранения данных пользователя
            double radius, length, width;

            try
            {
                // Запрашиваем данные для круга
                Console.Write("Введите радиус круга: ");
                string inputRadius = Console.ReadLine(); // Считываем радиус
                if (double.TryParse(inputRadius, out radius) && radius > 0)
                {
                    // Создаём объект круга
                    Circle circle = new Circle(radius);

                    // Выводим информацию о круге
                    Console.WriteLine("\nИнформация о круге:");
                    Console.WriteLine($"Площадь: {circle.Area():F2}"); // Выводим площадь с 2 знаками после запятой
                    Console.WriteLine($"Периметр: {circle.Perimeter():F2}"); // Выводим периметр с 2 знаками после запятой
                }
                else
                {
                    Console.WriteLine("Ошибка: радиус должен быть положительным числом!");
                    return; // Завершаем программу при некорректном вводе
                }

                // Запрашиваем данные для прямоугольника
                Console.Write("Введите длину прямоугольника: ");
                string inputLength = Console.ReadLine(); // Считываем длину
                Console.Write("Введите ширину прямоугольника: ");
                string inputWidth = Console.ReadLine(); // Считываем ширину
                if (double.TryParse(inputLength, out length) && double.TryParse(inputWidth, out width) && length > 0 && width > 0)
                {
                    // Создаём объект прямоугольника
                    Rectangle rectangle = new Rectangle(length, width);

                    // Выводим информацию о прямоугольнике
                    Console.WriteLine("\nИнформация о прямоугольнике:");
                    Console.WriteLine($"Площадь: {rectangle.Area():F2}"); // Выводим площадь с 2 знаками после запятой
                    Console.WriteLine($"Периметр: {rectangle.Perimeter():F2}"); // Выводим периметр с 2 знаками после запятой
                }
                else
                {
                    Console.WriteLine("Ошибка: длина и ширина должны быть положительными числами!");
                    return; // Завершаем программу при некорректном вводе
                }
            }
            catch (ArgumentException ex) // Обрабатываем исключения из конструкторов
            {
                Console.WriteLine($"Ошибка: {ex.Message}"); // Выводим сообщение об ошибке
            }
            catch (Exception ex) // Обрабатываем другие возможные исключения
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}"); // Выводим общее сообщение об ошибке
            }

            Console.WriteLine("\nПрограмма завершена. Нажмите Enter для выхода."); // Сообщаем о завершении
            Console.ReadLine(); // Ждём ввода для завершения
        }
    }
}