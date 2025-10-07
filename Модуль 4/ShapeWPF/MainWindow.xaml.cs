using System;
using System.Windows;

namespace ShapeWPF
{
    public delegate double AreaCalculator();

    public partial class MainWindow : Window
    {
        private IFigure currentShape;
        private AreaCalculator calculator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateCircleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(InputTextBox.Text, out double radius) && radius >= 0)
                {
                    currentShape = new Circle(radius);
                    calculator = currentShape.CalculateArea;
                    currentShape.AreaChanged += OnAreaChanged;
                    ResultTextBlock.Text = "Круг создан. Выберите 'Вычислить площадь' или измените размер.";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: введите корректный неотрицательный радиус!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CreateRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] values = InputTextBox.Text.Split(' ');
                if (values.Length == 2 && double.TryParse(values[0], out double width) && double.TryParse(values[1], out double height) && width >= 0 && height >= 0)
                {
                    currentShape = new Rectangle(width, height);
                    calculator = currentShape.CalculateArea;
                    currentShape.AreaChanged += OnAreaChanged;
                    ResultTextBlock.Text = "Прямоугольник создан. Выберите 'Вычислить площадь' или измените размер.";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: введите ширину и высоту через пробел (например, 3 4)!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CreateTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] values = InputTextBox.Text.Split(' ');
                if (values.Length == 3 && double.TryParse(values[0], out double sideA) && double.TryParse(values[1], out double sideB) && double.TryParse(values[2], out double sideC) && sideA > 0 && sideB > 0 && sideC > 0)
                {
                    currentShape = new Triangle(sideA, sideB, sideC);
                    calculator = currentShape.CalculateArea;
                    currentShape.AreaChanged += OnAreaChanged;
                    ResultTextBlock.Text = "Треугольник создан. Выберите 'Вычислить площадь'.";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: введите три стороны через пробел (например, 3 4 5)!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CalculateAreaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (calculator != null)
                {
                    double area = calculator();
                    ResultTextBlock.Text = $"Площадь: {area:F2}";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: сначала создайте фигуру!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CalculatePerimeterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentShape != null)
                {
                    double perimeter = currentShape.CalculatePerimeter();
                    ResultTextBlock.Text = $"Периметр: {perimeter:F2}";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: сначала создайте фигуру!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void OnAreaChanged(double newArea)
        {
            if (Dispatcher.CheckAccess())
            {
                ResultTextBlock.Text = $"Площадь изменена на: {newArea:F2}";
            }
            else
            {
                Dispatcher.Invoke(() => ResultTextBlock.Text = $"Площадь изменена на: {newArea:F2}");
            }
        }

        private void ChangeSizeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentShape == null)
                {
                    ResultTextBlock.Text = "Ошибка: сначала создайте фигуру!";
                    return;
                }

                string[] values = InputTextBox.Text.Split(' ');
                if (currentShape is Circle circle)
                {
                    if (values.Length == 1 && double.TryParse(values[0], out double newRadius) && newRadius >= 0)
                    {
                        circle.ChangeRadius(newRadius);
                        ResultTextBlock.Text = $"Радиус изменён. Площадь: {circle.CalculateArea():F2}, Периметр: {circle.CalculatePerimeter():F2}";
                    }
                    else
                    {
                        ResultTextBlock.Text = "Ошибка: введите новый радиус!";
                    }
                }
                else if (currentShape is Rectangle rectangle)
                {
                    if (values.Length == 2 && double.TryParse(values[0], out double newWidth) && double.TryParse(values[1], out double newHeight) && newWidth >= 0 && newHeight >= 0)
                    {
                        rectangle.ChangeWidth(newWidth);
                        rectangle.ChangeHeight(newHeight);
                        ResultTextBlock.Text = $"Размеры изменены. Площадь: {rectangle.CalculateArea():F2}, Периметр: {rectangle.CalculatePerimeter():F2}";
                    }
                    else
                    {
                        ResultTextBlock.Text = "Ошибка: введите новую ширину и высоту через пробел!";
                    }
                }
                else
                {
                    ResultTextBlock.Text = "Изменение размера недоступно для треугольника!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}