using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SortingApp
{
    public partial class MainWindow : Window
    {
        private readonly SortingLogic _sortingLogic = new SortingLogic();

        public MainWindow()
        {
            InitializeComponent();
            if (NumbersTextBox == null || RandomCountTextBox == null || SortMethodSelector == null ||
                SortedNumbersListBox == null || GenerateRandomButton == null || SortButton == null || ClearButton == null)
            {
                MessageBox.Show("Ошибка инициализации интерфейса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            SortMethodSelector.SelectedIndex = 0; // Установить начальный выбор сортировки
        }

        private void GenerateRandomButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RandomCountTextBox == null || NumbersTextBox == null)
                {
                    MessageBox.Show("Поле для количества или ввода чисел не найдено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string input = RandomCountTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Введите количество чисел!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(input, out int count) || count < 1)
                {
                    MessageBox.Show("Введите целое число больше 0!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int[] randomNumbers = _sortingLogic.GenerateRandomNumbers(count);
                NumbersTextBox.Text = string.Join(" ", randomNumbers);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NumbersTextBox == null || SortMethodSelector == null || SortedNumbersListBox == null)
                {
                    MessageBox.Show("Ошибка инициализации интерфейса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string input = NumbersTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Введите числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int[] numbers = input.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(x => int.Parse(x.Trim()))
                                     .ToArray();

                if (numbers.Length == 0)
                {
                    MessageBox.Show("Введите хотя бы одно число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int[] sortedNumbers;
                switch (SortMethodSelector.SelectedIndex)
                {
                    case 0:
                        sortedNumbers = _sortingLogic.BubbleSort(numbers);
                        break;
                    case 1:
                        sortedNumbers = _sortingLogic.QuickSort(numbers);
                        break;
                    default:
                        sortedNumbers = _sortingLogic.BubbleSort(numbers);
                        break;
                }

                SortedNumbersListBox.Items.Clear();
                foreach (int number in sortedNumbers)
                {
                    SortedNumbersListBox.Items.Add(number.ToString());
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите только целые числа, разделённые пробелами или запятыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NumbersTextBox != null) NumbersTextBox.Text = string.Empty;
                if (RandomCountTextBox != null) RandomCountTextBox.Text = string.Empty;
                if (SortedNumbersListBox != null) SortedNumbersListBox.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при очистке: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}