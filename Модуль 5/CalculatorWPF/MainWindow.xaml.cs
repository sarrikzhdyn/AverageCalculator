using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorWPF
{
    public partial class MainWindow : Window
    {
        private double? firstNumber = null; // Хранение первого числа операции (nullable, чтобы обнулить при сбросе)
        private string operation = ""; // Хранение текущей арифметической операции (+, -, *, /)
        private bool isNewNumber = true; // Флаг, указывающий, начинается ли ввод нового числа

        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов интерфейса из XAML
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия на кнопку с цифрой или точкой
            Button button = (Button)sender; // Получаем кнопку, вызвавшую событие
            if (isNewNumber)
            {
                // Если начинается новый ввод, заменяем текущее значение в текстовом поле
                txtResult.Text = button.Content.ToString();
                isNewNumber = false; // Сбрасываем флаг нового числа
            }
            else
            {
                // Добавляем новую цифру или точку к существующему числу
                txtResult.Text += button.Content.ToString();
            }
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия на кнопку с операцией
            Button button = (Button)sender; // Получаем кнопку с операцией
            if (!string.IsNullOrEmpty(txtResult.Text) && firstNumber.HasValue)
            {
                // Если есть текущее число и первое число уже введено, выполняем предыдущую операцию
                Equal_Click(sender, e);
            }
            // Сохраняем текущее число как первое и операцию для следующего вычисления
            firstNumber = double.Parse(txtResult.Text); // Преобразуем текст в число
            operation = button.Content.ToString(); // Сохраняем операцию
            isNewNumber = true; // Устанавливаем флаг нового числа для следующего ввода
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия на кнопку равно для выполнения вычисления
            if (firstNumber.HasValue && !string.IsNullOrEmpty(operation) && !isNewNumber)
            {
                // Проверяем, что есть оба числа и операция для вычисления
                double secondNumber = double.Parse(txtResult.Text); // Получаем второе число
                double result = 0; // Инициализируем переменную для результата
                switch (operation)
                {
                    case "+":
                        result = firstNumber.Value + secondNumber; // Сложение
                        break;
                    case "-":
                        result = firstNumber.Value - secondNumber; // Вычитание
                        break;
                    case "*":
                        result = firstNumber.Value * secondNumber; // Умножение
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber.Value / secondNumber; // Деление
                        }
                        else
                        {
                            // Обработка деления на ноль
                            MessageBox.Show("Деление на ноль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return; // Прерываем выполнение метода
                        }
                        break;
                }
                // Выводим результат и обновляем состояние
                txtResult.Text = result.ToString();
                firstNumber = result; // Сохраняем результат как новое первое число
                isNewNumber = true; // Готовимся к новому вводу
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия на кнопку стирания (сброс)
            txtResult.Text = ""; // Очищаем текстовое поле
            firstNumber = null; // Сбрасываем первое число
            operation = ""; // Сбрасываем операцию
            isNewNumber = true; // Устанавливаем флаг нового числа
        }
    }
}