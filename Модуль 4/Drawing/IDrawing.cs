using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace DrawingApp
{
    public interface IDrawing
    {
        void DrawLine(Canvas canvas, Point start, Point end, Brush color, double thickness);
        void DrawCircle(Canvas canvas, Point center, double radius, Brush color, double thickness);
        void DrawRectangle(Canvas canvas, Point topLeft, double width, double height, Brush color, double thickness);
    }
}