using System;

namespace ShapeWPF
{
    public class Rectangle : IFigure
    {
        private double width;
        private double height;

        public Rectangle(double width, double height)
        {
            if (width >= 0 && height >= 0)
            {
                this.width = width;
                this.height = height;
                OnAreaChanged(CalculateArea());
            }
            else
            {
                throw new ArgumentException("Ширина и высота не могут быть отрицательными!");
            }
        }

        public void ChangeWidth(double newWidth)
        {
            if (newWidth >= 0)
            {
                width = newWidth;
                OnAreaChanged(CalculateArea());
            }
            else
            {
                throw new ArgumentException("Новая ширина не может быть отрицательной!");
            }
        }

        public void ChangeHeight(double newHeight)
        {
            if (newHeight >= 0)
            {
                height = newHeight;
                OnAreaChanged(CalculateArea());
            }
            else
            {
                throw new ArgumentException("Новая высота не может быть отрицательной!");
            }
        }

        public double CalculateArea()
        {
            return width * height;
        }

        public double CalculatePerimeter()
        {
            return 2 * (width + height);
        }

        public event Action<double> AreaChanged;

        protected virtual void OnAreaChanged(double newArea)
        {
            AreaChanged?.Invoke(newArea);
        }
    }
}