using System;

namespace ShapeWPF
{
    public interface IFigure
    {
        double CalculateArea();
        double CalculatePerimeter();
        event Action<double> AreaChanged;
    }
}