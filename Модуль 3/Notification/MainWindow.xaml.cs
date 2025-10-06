using System;
using System.Windows;
using System.Windows.Threading;


namespace ShapeWPF
{
    // Класс для обработки уведомлений (сообщения, звонки, email)
    public class Notification
    {
        // Делегаты для событий уведомлений
        public delegate void MessageSentHandler(string message);
        public delegate void CallMadeHandler(string phoneNumber);
        public delegate void EmailSentHandler(string email, string subject);

        // События для уведомлений
        public event MessageSentHandler MessageSent;
        public event CallMadeHandler CallMade;
        public event EmailSentHandler EmailSent;

        // Метод для отправки текстового сообщения
        public void SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Сообщение не может быть пустым!");
            MessageSent?.Invoke(message); // Вызываем событие
        }

        // Метод для совершения звонка
        public void MakeCall(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Номер телефона не может быть пустым!");
            CallMade?.Invoke(phoneNumber); // Вызываем событие
        }

        // Метод для отправки email
        public void SendEmail(string email, string subject)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Email и тема не могут быть пустыми!");
            EmailSent?.Invoke(email, subject); // Вызываем событие
        }
    }

    public partial class MainWindow : Window
    {
        private readonly Notification notifier; // Экземпляр для отправки уведомлений
        private readonly ClassNotification.Class1 logger; // Экземпляр для логирования
        private readonly DispatcherTimer timer; // Таймер для автоматических уведомлений

        public MainWindow()
        {
            try
            {
                InitializeComponent(); // Инициализация UI-компонентов из XAML

                notifier = new Notification(); // Создаём объект для уведомлений

                // Проверяем, что Class1 доступен, и создаём объект для логирования
                try
                {
                    logger = new ClassNotification.Class1();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании логгера: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger = null; // Устанавливаем logger в null, чтобы избежать дальнейших ошибок
                }

                // Подписываемся на события уведомлений
                notifier.MessageSent += OnMessageSent;
                notifier.CallMade += OnCallMade;
                notifier.EmailSent += OnEmailSent;

                // Проверяем наличие ResultTextBlock
                if (ResultTextBlock == null)
                {
                    MessageBox.Show("Ошибка: Элемент ResultTextBlock не найден в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Прерываем инициализацию, если UI не загружен
                }
                ResultTextBlock.Text = "Результаты появятся здесь"; // Устанавливаем начальный текст

                // Настройка таймера для автоматических уведомлений
                timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(10) // Интервал 10 секунд
                };
                timer.Tick += Timer_Tick; // Привязываем обработчик
                timer.Start(); // Запускаем таймер
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации приложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик события тика таймера
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                notifier.SendMessage("Автоматическое уведомление!");
                if (logger != null)
                    logger.LogNotification("Автоматическое уведомление отправлено");
                else
                    UpdateResultText("Ошибка: Логгер не инициализирован!");
            }
            catch (Exception ex)
            {
                UpdateResultText($"Ошибка в таймере: {ex.Message}");
            }
        }

        // Обработчик события отправки сообщения
        private void OnMessageSent(string message)
        {
            if (Dispatcher.CheckAccess()) // Проверяем доступ к UI-потоку
            {
                UpdateResultText($"Сообщение отправлено: {message}");
                if (logger != null)
                    logger.LogNotification($"Сообщение отправлено: {message}");
                else
                    UpdateResultText("Ошибка: Логгер не инициализирован!");
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    UpdateResultText($"Сообщение отправлено: {message}");
                    if (logger != null)
                        logger.LogNotification($"Сообщение отправлено: {message}");
                    else
                        UpdateResultText("Ошибка: Логгер не инициализирован!");
                });
            }
        }

        // Обработчик события совершения звонка
        private void OnCallMade(string phoneNumber)
        {
            if (Dispatcher.CheckAccess())
            {
                UpdateResultText($"Звонок выполнен на: {phoneNumber}");
                if (logger != null)
                    logger.LogNotification($"Звонок выполнен на: {phoneNumber}");
                else
                    UpdateResultText("Ошибка: Логгер не инициализирован!");
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    UpdateResultText($"Звонок выполнен на: {phoneNumber}");
                    if (logger != null)
                        logger.LogNotification($"Звонок выполнен на: {phoneNumber}");
                    else
                        UpdateResultText("Ошибка: Логгер не инициализирован!");
                });
            }
        }

        // Обработчик события отправки email
        private void OnEmailSent(string email, string subject)
        {
            if (Dispatcher.CheckAccess())
            {
                UpdateResultText($"Email отправлен на {email} с темой: {subject}");
                if (logger != null)
                    logger.LogNotification($"Email отправлен на {email} с темой: {subject}");
                else
                    UpdateResultText("Ошибка: Логгер не инициализирован!");
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    UpdateResultText($"Email отправлен на {email} с темой: {subject}");
                    if (logger != null)
                        logger.LogNotification($"Email отправлен на {email} с темой: {subject}");
                    else
                        UpdateResultText("Ошибка: Логгер не инициализирован!");
                });
            }
        }

        // Вспомогательный метод для обновления ResultTextBlock с проверкой на null
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

        // Обработчик клика по кнопке отправки сообщения
        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageTextBox != null)
                {
                    string message = MessageTextBox.Text?.Trim();
                    if (!string.IsNullOrEmpty(message))
                    {
                        notifier.SendMessage(message);
                        MessageTextBox.Text = "";
                    }
                    else
                    {
                        UpdateResultText("Ошибка: Введите сообщение для отправки!");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Элемент MessageTextBox не найден в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                UpdateResultText($"Ошибка: {ex.Message}");
            }
        }

        // Обработчик клика по кнопке совершения звонка
        private void MakeCallButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PhoneTextBox != null)
                {
                    string phone = PhoneTextBox.Text?.Trim();
                    if (!string.IsNullOrEmpty(phone))
                    {
                        notifier.MakeCall(phone);
                        PhoneTextBox.Text = "";
                    }
                    else
                    {
                        UpdateResultText("Ошибка: Введите номер телефона для звонка!");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Элемент PhoneTextBox не найден в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                UpdateResultText($"Ошибка: {ex.Message}");
            }
        }

        // Обработчик клика по кнопке отправки email
        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EmailTextBox != null && SubjectTextBox != null)
                {
                    string email = EmailTextBox.Text?.Trim();
                    string subject = SubjectTextBox.Text?.Trim();
                    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(subject))
                    {
                        notifier.SendEmail(email, subject);
                        EmailTextBox.Text = "";
                        SubjectTextBox.Text = "";
                    }
                    else
                    {
                        UpdateResultText("Ошибка: Введите email и тему для отправки!");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Элементы EmailTextBox или SubjectTextBox не найдены в XAML!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                UpdateResultText($"Ошибка: {ex.Message}");
            }
        }
    }
}