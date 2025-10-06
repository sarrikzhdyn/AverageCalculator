using System; 
using System.Windows; 
namespace ShapeWPF 
{
    // Делегат для вызова метода вычисления площади
    public delegate double AreaCalculator(); // Делегат без параметров, соответствующий CalculateArea

    public partial class MainWindow : Window // Основной класс окна WPF
    {
        private Shape currentShape; // Переменная для хранения текущей фигуры
        private AreaCalculator calculator; // Делегат для вычисления площади

        public MainWindow() // Конструктор окна
        {
            InitializeComponent(); // Инициализируем компоненты из XAML
        }

        // Обработчик события для создания круга
        private void CreateCircleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(InputTextBox.Text, out double radius) && radius >= 0)
                {
                    currentShape = new Circle(radius); // Создаём круг
                    calculator = currentShape.CalculateArea; // Привязываем делегат к методу экземпляра
                    currentShape.AreaChanged += OnAreaChanged; // Подписываемся на событие
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

        // Обработчик события для создания прямоугольника
        private void CreateRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] values = InputTextBox.Text.Split(' ');
                if (values.Length == 2 && double.TryParse(values[0], out double width) && double.TryParse(values[1], out double height) && width >= 0 && height >= 0)
                {
                    currentShape = new Rectangle(width, height); // Создаём прямоугольник
                    calculator = currentShape.CalculateArea; // Привязываем делегат к методу экземпляра
                    currentShape.AreaChanged += OnAreaChanged; // Подписываемся на событие
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

        // Обработчик события для создания треугольника
        private void CreateTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] values = InputTextBox.Text.Split(' ');
                if (values.Length == 3 && double.TryParse(values[0], out double sideA) && double.TryParse(values[1], out double sideB) && double.TryParse(values[2], out double sideC) && sideA > 0 && sideB > 0 && sideC > 0)
                {
                    currentShape = new Triangle(sideA, sideB, sideC); // Создаём треугольник
                    calculator = currentShape.CalculateArea; // Привязываем делегат к методу экземпляра
                    currentShape.AreaChanged += OnAreaChanged; // Подписываемся на событие
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

        // Обработчик события для вычисления площади
        private void CalculateAreaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (calculator != null)
                {
                    double area = calculator(); // Вызываем метод через делегат без параметров
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

        // Обработчик события изменения площади
        private void OnAreaChanged(double newArea) // Обновляет текстовое поле при изменении площади
        {
            if (Dispatcher.CheckAccess()) // Проверяем доступ к UI потоку
            {
                ResultTextBlock.Text = $"Площадь изменена на: {newArea:F2}";
            }
            else
            {
                Dispatcher.Invoke(() => ResultTextBlock.Text = $"Площадь изменена на: {newArea:F2}");
            }
        }

        // Обработчик события для изменения размера фигуры
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
                        circle.ChangeRadius(newRadius); // Изменяем радиус
                        ResultTextBlock.Text = $"Радиус изменён. Площадь: {circle.CalculateArea():F2}";
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
                        rectangle.ChangeWidth(newWidth); // Изменяем ширину
                        rectangle.ChangeHeight(newHeight); // Изменяем высоту
                        ResultTextBlock.Text = $"Размеры изменены. Площадь: {rectangle.CalculateArea():F2}";
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