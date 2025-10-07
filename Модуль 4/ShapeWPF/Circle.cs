using System;

namespace ShapeWPF
{
    public class Circle : IFigure
    {
        private double radius;

        public Circle(double radius)
        {
            if (radius >= 0)
            {
                this.radius = radius;
                OnAreaChanged(CalculateArea());
            }
            else
            {
                throw new ArgumentException("Радиус не может быть отрицательным!");
            }
        }

        public void ChangeRadius(double newRadius)
        {
            if (newRadius >= 0)
            {
                radius = newRadius;
                OnAreaChanged(CalculateArea());
            }
            else
            {
                throw new ArgumentException("Новый радиус не может быть отрицательным!");
            }
        }

        public double CalculateArea()
        {
            return Math.PI * radius * radius;
        }

        public double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }

        public event Action<double> AreaChanged;

        protected virtual void OnAreaChanged(double newArea)
        {
            AreaChanged?.Invoke(newArea);
        }
    }
}