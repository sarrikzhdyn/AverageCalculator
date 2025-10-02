using System; 

namespace TemperatureControl 
{
    // Класс TemperatureSensor, представляющий датчик температуры
    class TemperatureSensor
    {
        // Поле для хранения текущей температуры
        private double currentTemperature; // Текущая температура в градусах Цельсия

        // Делегат для определения сигнатуры события
        public delegate void TemperatureChangedHandler(double newTemperature); // Делегат для обработки события

        // Событие, срабатывающее при изменении температуры
        public event TemperatureChangedHandler TemperatureChanged; // Событие для уведомления об изменении температуры

        // Свойство для доступа и изменения температуры
        public double CurrentTemperature
        {
            get { return currentTemperature; } // Возвращаем текущую температуру
            set
            {
                if (currentTemperature != value) // Проверяем, изменилась ли температура
                {
                    currentTemperature = value; // Обновляем значение температуры
                    OnTemperatureChanged(currentTemperature); // Вызываем метод для срабатывания события
                }
            }
        }

        // Защищённый метод для вызова события
        protected virtual void OnTemperatureChanged(double newTemperature) // Вызывает событие TemperatureChanged
        {
            TemperatureChanged?.Invoke(newTemperature); // Вызываем событие, если есть подписчики
        }
    }

    // Класс Thermostat, управляющий отоплением на основе температуры
    class Thermostat
    {
        // Поле для отслеживания состояния отопления
        private bool isHeatingOn; // Состояние отопления (включено/выключено)

        // Конструктор класса Thermostat
        public Thermostat()
        {
            isHeatingOn = false; // Изначально отопление выключено
        }

        // Метод для обработки события изменения температуры
        public void OnTemperatureChanged(double newTemperature) // Реагирует на изменение температуры
        {
            if (newTemperature < 18) // Если температура ниже 18°C
            {
                if (!isHeatingOn) // Если отопление выключено
                {
                    isHeatingOn = true; // Включаем отопление
                    Console.WriteLine($"Температура {newTemperature}°C: Отопление включено."); // Сообщаем о включении
                }
            }
            else if (newTemperature > 22) // Если температура выше 22°C
            {
                if (isHeatingOn) // Если отопление включено
                {
                    isHeatingOn = false; // Выключаем отопление
                    Console.WriteLine($"Температура {newTemperature}°C: Отопление выключено."); // Сообщаем о выключении
                }
            }
            else
            {
                if (isHeatingOn) // Если температура между 18°C и 22°C, и отопление включено
                {
                    Console.WriteLine($"Температура {newTemperature}°C: Отопление остаётся включённым."); // Сообщаем о сохранении состояния
                }
                else
                {
                    Console.WriteLine($"Температура {newTemperature}°C: Отопление остаётся выключенным."); // Сообщаем о сохранении состояния
                }
            }
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа управления температурой.");

            // Создаём экземпляр датчика температуры
            TemperatureSensor sensor = new TemperatureSensor(); // Создаём объект датчика

            // Создаём экземпляр термостата
            Thermostat thermostat = new Thermostat(); // Создаём объект термостата

            // Подписываемся на событие TemperatureChanged
            sensor.TemperatureChanged += thermostat.OnTemperatureChanged; // Привязываем обработчик события

            try
            {
                // Ввод данных пользователем
                Console.Write("Введите начальную температуру (в °C): "); // Запрашиваем начальную температуру
                double initialTemp = double.Parse(Console.ReadLine()); // Считываем и преобразуем в double
                sensor.CurrentTemperature = initialTemp; // Устанавливаем начальную температуру

                while (true) // Бесконечный цикл для ввода новых температур
                {
                    Console.Write("Введите новую температуру (или 'exit' для завершения): "); // Запрашиваем новую температуру
                    string input = Console.ReadLine(); // Считываем введённое значение

                    if (input.ToLower() == "exit") // Проверяем условие выхода
                    {
                        break; // Выходим из цикла
                    }

                    double newTemp;
                    if (double.TryParse(input, out newTemp)) // Проверяем, можно ли преобразовать строку в число
                    {
                        sensor.CurrentTemperature = newTemp; // Устанавливаем новую температуру
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введите корректное числовое значение или 'exit' для завершения!"); // Сообщаем об ошибке
                    }
                }
            }
            catch (FormatException) // Обрабатываем ошибки преобразования строки в число
            {
                Console.WriteLine("Ошибка: введите корректное числовое значение!"); // Выводим сообщение об ошибке
            }
            catch (Exception ex) // Обрабатываем другие возможные исключения
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}"); // Выводим общее сообщение об ошибке
            }

            Console.WriteLine("Программа завершена. Нажмите Enter для выхода."); // Сообщаем о завершении
            Console.ReadLine(); // Ждём ввода для завершения
        }
    }
}