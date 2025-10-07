using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp
{
    public class CanvasDrawing : IDrawing
    {
        public void DrawLine(Canvas canvas, System.Windows.Point start, System.Windows.Point end, Brush color, double thickness)
        {
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = color,
                StrokeThickness = thickness
            };
            canvas.Children.Add(line);
        }

        public void DrawCircle(Canvas canvas, System.Windows.Point center, double radius, Brush color, double thickness)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = color,
                StrokeThickness = thickness
            };
            Canvas.SetLeft(ellipse, center.X - radius);
            Canvas.SetTop(ellipse, center.Y - radius);
            canvas.Children.Add(ellipse);
        }

        public void DrawRectangle(Canvas canvas, System.Windows.Point topLeft, double width, double height, Brush color, double thickness)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Stroke = color,
                StrokeThickness = thickness
            };
            Canvas.SetLeft(rectangle, topLeft.X);
            Canvas.SetTop(rectangle, topLeft.Y);
            canvas.Children.Add(rectangle);
        }
    }
}