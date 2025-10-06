using System; 
namespace ShapeWPF 
{
    // Базовый класс Shape, представляющий геометрическую фигуру
    public abstract class Shape
    {
        // Делегат для обработки события изменения площади
        public delegate void AreaChangedHandler(double newArea); // Делегат для события

        // Событие изменения площади
        public event AreaChangedHandler AreaChanged; // Событие, срабатывающее при изменении площади

        // Виртуальный метод для вычисления площади
        public abstract double CalculateArea(); // Абстрактный метод для переопределения

        // Защищённый метод для вызова события
        protected virtual void OnAreaChanged(double newArea) // Вызывает событие с новой площадью
        {
            AreaChanged?.Invoke(newArea); // Вызываем событие, если есть подписчики
        }
    }

    // Класс Circle, наследующийся от Shape, представляющий круг
    public class Circle : Shape
    {
        private double radius; // Радиус круга

        public Circle(double radius)
        {
            if (radius >= 0) // Проверяем, что радиус неотрицательный
            {
                this.radius = radius; // Устанавливаем радиус
                OnAreaChanged(CalculateArea()); // Вызываем событие при создании
            }
            else
            {
                throw new ArgumentException("Радиус не может быть отрицательным!");
            }
        }

        public void ChangeRadius(double newRadius) // Изменяет радиус и вызывает событие
        {
            if (newRadius >= 0) // Проверяем, что новый радиус неотрицательный
            {
                radius = newRadius; // Обновляем радиус
                OnAreaChanged(CalculateArea()); // Вызываем событие при изменении
            }
            else
            {
                throw new ArgumentException("Новый радиус не может быть отрицательным!");
            }
        }

        public override double CalculateArea() // Вычисляет площадь круга
        {
            return Math.PI * radius * radius; // Возвращаем площадь по формуле πr²
        }
    }

    // Класс Rectangle, наследующийся от Shape, представляющий прямоугольник
    public class Rectangle : Shape
    {
        private double width; // Ширина прямоугольника
        private double height; // Высота прямоугольника

        public Rectangle(double width, double height)
        {
            if (width >= 0 && height >= 0) // Проверяем, что размеры неотрицательные
            {
                this.width = width; // Устанавливаем ширину
                this.height = height; // Устанавливаем высоту
                OnAreaChanged(CalculateArea()); // Вызываем событие при создании
            }
            else
            {
                throw new ArgumentException("Ширина и высота не могут быть отрицательными!");
            }
        }

        public void ChangeWidth(double newWidth) // Изменяет ширину и вызывает событие
        {
            if (newWidth >= 0) // Проверяем, что новая ширина неотрицательная
            {
                width = newWidth; // Обновляем ширину
                OnAreaChanged(CalculateArea()); // Вызываем событие при изменении
            }
            else
            {
                throw new ArgumentException("Новая ширина не может быть отрицательной!");
            }
        }

        public void ChangeHeight(double newHeight) // Изменяет высоту и вызывает событие
        {
            if (newHeight >= 0) // Проверяем, что новая высота неотрицательная
            {
                height = newHeight; // Обновляем высоту
                OnAreaChanged(CalculateArea()); // Вызываем событие при изменении
            }
            else
            {
                throw new ArgumentException("Новая высота не может быть отрицательной!");
            }
        }

        public override double CalculateArea() // Вычисляет площадь прямоугольника
        {
            return width * height; // Возвращаем площадь как произведение ширины и высоты
        }
    }

    // Класс Triangle, наследующийся от Shape, представляющий треугольник
    public class Triangle : Shape
    {
        private double sideA; // Первая сторона
        private double sideB; // Вторая сторона
        private double sideC; // Третья сторона

        public Triangle(double sideA, double sideB, double sideC)
        {
            if (sideA > 0 && sideB > 0 && sideC > 0 && (sideA + sideB > sideC) && (sideB + sideC > sideA) && (sideA + sideC > sideB)) // Проверяем существование треугольника
            {
                this.sideA = sideA; // Устанавливаем первую сторону
                this.sideB = sideB; // Устанавливаем вторую сторону
                this.sideC = sideC; // Устанавливаем третью сторону
                OnAreaChanged(CalculateArea()); // Вызываем событие при создании
            }
            else
            {
                throw new ArgumentException("Стороны не образуют треугольник или имеют некорректные значения!");
            }
        }

        public override double CalculateArea() // Вычисляет площадь треугольника по формуле Герона
        {
            double s = (sideA + sideB + sideC) / 2; // Вычисляем полупериметр
            return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC)); // Возвращаем площадь
        }
    }
}