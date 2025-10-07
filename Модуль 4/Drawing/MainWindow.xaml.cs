using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp
{
    public partial class MainWindow : Window
    {
        private IDrawing drawingTool = new CanvasDrawing();
        private string selectedTool = "";
        private double thickness = 2;
        private Point? startPoint = null; // Для хранения начальной точки линии
        private Line tempLine = null; // Временная линия для предпросмотра

        public MainWindow()
        {
            InitializeComponent();
            DrawingCanvas.MouseDown += DrawingCanvas_MouseDown;
            DrawingCanvas.MouseMove += DrawingCanvas_MouseMove;
            DrawingCanvas.MouseUp += DrawingCanvas_MouseUp;
            ThicknessSlider.ValueChanged += ThicknessSlider_ValueChanged;
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            thickness = e.NewValue;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            selectedTool = "Line";
            startPoint = null; // Сбрасываем начальную точку
            if (tempLine != null)
            {
                DrawingCanvas.Children.Remove(tempLine);
                tempLine = null;
            }
        }

        private void CircleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedTool = "Circle";
            startPoint = null;
            if (tempLine != null)
            {
                DrawingCanvas.Children.Remove(tempLine);
                tempLine = null;
            }
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedTool = "Rectangle";
            startPoint = null;
            if (tempLine != null)
            {
                DrawingCanvas.Children.Remove(tempLine);
                tempLine = null;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
            startPoint = null;
            if (tempLine != null)
            {
                DrawingCanvas.Children.Remove(tempLine);
                tempLine = null;
            }
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedTool == "")
            {
                MessageBox.Show("Выберите инструмент!");
                return;
            }

            Point clickPoint = e.GetPosition(DrawingCanvas);
            // Проверяем, находится ли точка в пределах холста
            if (clickPoint.X < 0 || clickPoint.X > DrawingCanvas.ActualWidth || clickPoint.Y < 0 || clickPoint.Y > DrawingCanvas.ActualHeight)
            {
                MessageBox.Show("Рисование возможно только внутри холста!");
                return;
            }

            Brush color = Brushes.Black;

            if (selectedTool == "Line" && e.LeftButton == MouseButtonState.Pressed)
            {
                startPoint = clickPoint;
                tempLine = new Line
                {
                    Stroke = color,
                    StrokeThickness = thickness,
                    X1 = clickPoint.X,
                    Y1 = clickPoint.Y
                };
                DrawingCanvas.Children.Add(tempLine);
            }
            else if (selectedTool == "Circle")
            {
                drawingTool.DrawCircle(DrawingCanvas, clickPoint, 50, color, thickness);
            }
            else if (selectedTool == "Rectangle")
            {
                drawingTool.DrawRectangle(DrawingCanvas, clickPoint, 100, 80, color, thickness);
            }
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedTool == "Line" && startPoint.HasValue && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(DrawingCanvas);
                if (currentPoint.X >= 0 && currentPoint.X <= DrawingCanvas.ActualWidth && currentPoint.Y >= 0 && currentPoint.Y <= DrawingCanvas.ActualHeight)
                {
                    if (tempLine != null)
                    {
                        tempLine.X2 = currentPoint.X;
                        tempLine.Y2 = currentPoint.Y;
                    }
                }
            }
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedTool == "Line" && startPoint.HasValue)
            {
                Point endPoint = e.GetPosition(DrawingCanvas);
                if (endPoint.X >= 0 && endPoint.X <= DrawingCanvas.ActualWidth && endPoint.Y >= 0 && endPoint.Y <= DrawingCanvas.ActualHeight)
                {
                    if (tempLine != null)
                    {
                        drawingTool.DrawLine(DrawingCanvas, startPoint.Value, endPoint, Brushes.Black, thickness);
                        DrawingCanvas.Children.Remove(tempLine);
                        tempLine = null;
                    }
                }
                startPoint = null;
            }
        }
    }
}