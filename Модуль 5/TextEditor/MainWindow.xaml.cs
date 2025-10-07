using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace TextEditorWPF
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события клика по кнопке "Открыть"
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра диалога открытия файла
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                // Установка фильтра для отображения только текстовых файлов или всех файлов
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                // Установка заголовка диалога
                Title = "Выберите файл для открытия"
            };
            // Проверка, был ли выбран файл через диалог
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Чтение всего содержимого выбранного файла в строку
                    string fileContent = File.ReadAllText(openFileDialog.FileName);
                    // Установка содержимого файла в текстовое поле
                    txtEditor.Text = fileContent;
                    // Обновление заголовка окна с именем открытого файла
                    Title = $"Текстовый редактор - {openFileDialog.SafeFileName}";
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Обработка исключения, если нет прав доступа к файлу
                    MessageBox.Show("Нет прав доступа к файлу: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (FileNotFoundException ex)
                {
                    // Обработка исключения, если файл не найден
                    MessageBox.Show("Файл не найден: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    // Обработка всех остальных неожиданных ошибок при открытии файла
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Обработчик события клика по кнопке "Сохранить"
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра диалога сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                // Установка фильтра для сохранения только текстовых файлов или всех файлов
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                // Установка заголовка диалога
                Title = "Сохранить файл как"
            };
            // Проверка, был ли выбран путь для сохранения через диалог
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Запись содержимого текстового поля в файл
                    File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
                    // Обновление заголовка окна с именем сохранённого файла
                    Title = $"Текстовый редактор - {saveFileDialog.SafeFileName}";
                    // Сообщение об успешном сохранении
                    MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Обработка всех ошибок, возникающих при сохранении файла
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}