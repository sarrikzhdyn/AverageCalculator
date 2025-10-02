using System; 

namespace DrawbleShapes
{
    // Интерфейс IDrawable, определяющий метод для отрисовки объектов
    interface IDrawable
    {
        // Метод Draw() для вывода информации о рисуемом объекте
        void Draw();
    }

    // Класс Circle, реализующий интерфейс IDrawable, представляющий круг
    class Circle : IDrawable
    {
        // Публичное свойство для хранения радиуса с возможностью чтения и записи
        public double Radius { get; set; }

        // Конструктор класса Circle, принимающий радиус как параметр
        public Circle(double radius)
        {
            Radius = radius; // Устанавливаем значение радиуса
        }

        // Реализация метода Draw() для вывода информации о круге
        public void Draw()
        {
            Console.WriteLine($"Рисуем круг с радиусом: {Radius}"); // Выводим сообщение с радиусом круга
        }
    }

    // Класс Rectangle, реализующий интерфейс IDrawable, представляющий прямоугольник
    class Rectangle : IDrawable
    {
        // Публичное свойство для хранения ширины с возможностью чтения и записи
        public double Width { get; set; }

        // Публичное свойство для хранения высоты с возможностью чтения и записи
        public double Height { get; set; }

        // Конструктор класса Rectangle, принимающий ширину и высоту как параметры
        public Rectangle(double width, double height)
        {
            Width = width; // Устанавливаем значение ширины
            Height = height; // Устанавливаем значение высоты
        }

        // Реализация метода Draw() для вывода информации о прямоугольнике
        public void Draw()
        {
            Console.WriteLine($"Рисуем прямоугольник с шириной: {Width} и высотой: {Height}"); // Выводим сообщение с шириной и высотой
        }
    }

    // Класс Triangle, реализующий интерфейс IDrawable, представляющий треугольник
    class Triangle : IDrawable
    {
        // Публичное свойство для хранения первой стороны с возможностью чтения и записи
        public double SideA { get; set; }

        // Публичное свойство для хранения второй стороны с возможностью чтения и записи
        public double SideB { get; set; }

        // Публичное свойство для хранения третьей стороны с возможностью чтения и записи
        public double SideC { get; set; }

        // Конструктор класса Triangle, принимающий три стороны как параметры
        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = sideA; // Устанавливаем значение первой стороны
            SideB = sideB; // Устанавливаем значение второй стороны
            SideC = sideC; // Устанавливаем значение третьей стороны
        }

        // Реализация метода Draw() для вывода информации о треугольнике
        public void Draw()
        {
            Console.WriteLine($"Рисуем треугольник со сторонами: {SideA}, {SideB}, {SideC}"); // Выводим сообщение с длинами сторон
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Создание массива объектов, реализующих интерфейс IDrawable, для хранения трёх фигур
            IDrawable[] shapes = new IDrawable[3];

            // Ввод данных для круга
            Console.Write("Введите радиус круга:"); // Запрашиваем радиус у пользователя
            double radius = double.Parse(Console.ReadLine()); // Считываем введённое значение и преобразуем в double
            shapes[0] = new Circle(radius); // Создаём объект круга и добавляем в массив

            // Ввод данных для прямоугольника
            Console.Write("Введите ширину прямоугольника:"); // Запрашиваем ширину у пользователя
            double width = double.Parse(Console.ReadLine()); // Считываем введённое значение и преобразуем в double
            Console.Write("Введите высоту прямоугольника:"); // Запрашиваем высоту у пользователя
            double height = double.Parse(Console.ReadLine()); // Считываем введённое значение и преобразуем в double
            shapes[1] = new Rectangle(width, height); // Создаём объект прямоугольника и добавляем в массив

            // Ввод данных для треугольника
            Console.Write("Введите длину первой стороны треугольника:"); // Запрашиваем первую сторону у пользователя
            double sideA = double.Parse(Console.ReadLine()); // Считываем введённое значение и преобразуем в double
            Console.Write("Введите длину второй стороны треугольника:"); // Запрашиваем вторую сторону у пользователя
            double sideB = double.Parse(Console.ReadLine()); // Считываем введённое значение и преобразуем в double
            Console.Write("Введите длину третьей стороны треугольника:"); // Запрашиваем третью сторону у пользователя
            double sideC = double.Parse(Console.ReadLine()); // Считываем введённое значение и преобразуем в double
            shapes[2] = new Triangle(sideA, sideB, sideC); // Создаём объект треугольника и добавляем в массив

            // Вызов метода Draw для каждого объекта в массиве
            Console.WriteLine("\nРисуем фигуры:"); // Выводим заголовок перед отрисовкой
            foreach (IDrawable shape in shapes) // Проходим по всем объектам в массиве
            {
                shape.Draw(); // Вызываем метод Draw для текущего объекта
            }
        }
    }
}