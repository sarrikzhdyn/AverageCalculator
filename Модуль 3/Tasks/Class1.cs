using System;
using System.IO;

namespace ClassNotification
{
    /// <summary>
    /// Класс для логирования уведомлений в консоль и файл
    /// </summary>
    public class Class1
    {
        // Путь к файлу логов, основанный на директории приложения
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "notification_log.txt");

        /// <summary>
        /// Логирует информацию об уведомлении в консоль и файл
        /// </summary>
        /// <param name="message">Сообщение для записи в лог</param>
        public void LogNotification(string message)
        {
            try
            {
                // Форматируем запись с временной меткой
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

                // Выводим в консоль для отладки
                Console.WriteLine(logEntry);

                // Записываем в файл, добавляя новую строку в конец
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (UnauthorizedAccessException)
            {
                // Обрабатываем отсутствие прав на запись в файл
                Console.WriteLine("Ошибка логирования: Нет прав на запись в файл!");
            }
            catch (IOException ex)
            {
                // Обрабатываем ошибки ввода-вывода (например, файл занят)
                Console.WriteLine($"Ошибка логирования: Проблема с доступом к файлу ({ex.Message})");
            }
            catch (Exception ex)
            {
                // Обрабатываем прочие непредвиденные ошибки
                Console.WriteLine($"Ошибка логирования: {ex.Message}");
            }
        }
    }
}