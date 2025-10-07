using System;

namespace ShapeWPF
{
    public class Triangle : IFigure
    {
        private double sideA;
        private double sideB;
        private double sideC;

        public Triangle(double sideA, double sideB, double sideC)
        {
            if (sideA > 0 && sideB > 0 && sideC > 0 && (sideA + sideB > sideC) && (sideB + sideC > sideA) && (sideA + sideC > sideB))
            {
                this.sideA = sideA;
                this.sideB = sideB;
                this.sideC = sideC;
                OnAreaChanged(CalculateArea());
            }
            else
            {
                throw new ArgumentException("Стороны не образуют треугольник или имеют некорректные значения!");
            }
        }

        public double CalculateArea()
        {
            double s = (sideA + sideB + sideC) / 2;
            return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
        }

        public double CalculatePerimeter()
        {
            return sideA + sideB + sideC;
        }

        public event Action<double> AreaChanged;

        protected virtual void OnAreaChanged(double newArea)
        {
            AreaChanged?.Invoke(newArea);
        }
    }
}