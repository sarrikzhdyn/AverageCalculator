using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ClassNotification; // Для использования Class1

namespace ShapeWPF
{
    // Делегат для выполнения действий с задачей (принимает название задачи и категорию)
    public delegate void TaskAction(string taskName, string category);

    // Класс для представления задачи
    public class TaskItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public TaskAction Action { get; set; }
        public string ActionName { get; set; } // Для отображения в UI

        public override string ToString() => $"Задача: {Name}, Категория: {Category}, Действие: {ActionName}";
    }

    public partial class MainWindow : Window
    {
        private readonly List<TaskItem> tasks = new List<TaskItem>(); // Список задач
        private readonly Class1 logger; // Экземпляр для логирования

        public MainWindow()
        {
            try
            {
                InitializeComponent(); // Инициализация UI-компонентов из XAML

                // Проверяем наличие UI-элементов
                if (TaskNameTextBox == null || CategoryComboBox == null || DelegateComboBox == null || TaskListBox == null || AddTaskButton == null || ExecuteTasksButton == null || ClearResultsButton == null || ResultTextBlock == null)
                {
                    MessageBox.Show("Ошибка: Один или несколько UI-элементов не найдены в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Инициализируем логгер
                try
                {
                    logger = new Class1();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании логгера: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger = null;
                }

                // Устанавливаем начальный выбор в ComboBox
                CategoryComboBox.SelectedIndex = 0;
                DelegateComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации приложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для отправки уведомления через MessageBox и обновления ResultTextBlock
        private void ShowNotification(string taskName, string category)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateResultText($"Задача '{taskName}' (категория: {category}) выполнена (уведомление)");
                MessageBox.Show($"Задача '{taskName}' (категория: {category}) выполнена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        // Метод для записи в журнал и обновления ResultTextBlock
        private void LogToFile(string taskName, string category)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateResultText($"Задача '{taskName}' (категория: {category}) выполнена (записано в журнал)");
            });

            if (logger != null)
            {
                logger.LogNotification($"Задача '{taskName}' (категория: {category}) выполнена");
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    UpdateResultText("Ошибка: Логгер не инициализирован!");
                });
            }
        }

        // Вспомогательный метод для обновления ResultTextBlock
        private void UpdateResultText(string text)
        {
            if (ResultTextBlock != null)
            {
                ResultTextBlock.Text += $"\n{text}";
            }
            else
            {
                MessageBox.Show("Ошибка: Элемент ResultTextBlock не найден в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик клика по кнопке добавления задачи
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string taskName = TaskNameTextBox.Text?.Trim();
                if (string.IsNullOrEmpty(taskName))
                {
                    MessageBox.Show("Ошибка: Введите название задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Определяем категорию
                var selectedCategory = CategoryComboBox.SelectedItem as ComboBoxItem;
                if (selectedCategory == null)
                {
                    MessageBox.Show("Ошибка: Выберите категорию задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string category = selectedCategory.Content.ToString();

                // Определяем выбранное действие
                var selectedAction = DelegateComboBox.SelectedItem as ComboBoxItem;
                if (selectedAction == null)
                {
                    MessageBox.Show("Ошибка: Выберите действие для задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string actionName = selectedAction.Content.ToString();
                TaskAction action;

                // Явное присваивание делегата для исключения ошибок
                if (actionName == "Отправить уведомление")
                {
                    action = ShowNotification;
                }
                else if (actionName == "Записать в журнал")
                {
                    action = LogToFile;
                }
                else
                {
                    MessageBox.Show($"Ошибка: Неизвестное действие '{actionName}'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаём задачу
                var task = new TaskItem
                {
                    Name = taskName,
                    Category = category,
                    Action = action,
                    ActionName = actionName
                };

                // Добавляем задачу в список
                tasks.Add(task);
                TaskListBox.Items.Add(task);

                // Очищаем поле ввода
                TaskNameTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении задачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик клика по кнопке выполнения всех задач
        private void ExecuteTasksButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tasks.Count == 0)
                {
                    MessageBox.Show("Список задач пуст!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Выполняем все задачи
                foreach (var task in tasks)
                {
                    task.Action?.Invoke(task.Name, task.Category);
                }

                // Очищаем список задач
                tasks.Clear();
                TaskListBox.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении задач: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик клика по кнопке очистки результатов
        private void ClearResultsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ResultTextBlock != null)
                {
                    ResultTextBlock.Text = "Результаты выполнения задач появятся здесь";
                }
                else
                {
                    MessageBox.Show("Ошибка: Элемент ResultTextBlock не найден в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при очистке результатов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик удаления задачи через контекстное меню
        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TaskListBox.SelectedItem != null)
                {
                    var selectedTask = TaskListBox.SelectedItem as TaskItem;
                    tasks.Remove(selectedTask);
                    TaskListBox.Items.Remove(selectedTask);
                }
                else
                {
                    MessageBox.Show("Выберите задачу для удаления!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении задачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}