using System;

namespace ArrayNormalization 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа нормирует массив, деля его элементы на максимальный по модулю элемент.");

            // Запрашиваем размер массива
            Console.Write("Введите размер массива N: ");
            string inputN = Console.ReadLine(); // Считываем ввод размера
            int n;

            // Проверяем, является ли ввод корректным числом
            if (!int.TryParse(inputN, out n) || n <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Создаём массив из n элементов типа double
            double[] array = new double[n];

            // Заполняем массив значениями от пользователя
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введите элемент {i + 1}: "); // Запрашиваем элемент
                string input = Console.ReadLine(); // Считываем ввод
                if (!double.TryParse(input, out array[i]))
                {
                    Console.WriteLine("Ошибка: введите корректное число!"); // Сообщение об ошибке
                    return; // Завершаем программу при некорректном вводе
                }
            }

            // Выводим исходный массив
            Console.WriteLine("\nИсходный массив:");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine(); // Переходим на новую строку

            // Находим максимальный по модулю элемент
            double maxAbs = Math.Abs(array[0]); // Берем модуль первого элемента как начальное значение
            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(array[i]) > maxAbs)
                {
                    maxAbs = Math.Abs(array[i]); // Обновляем максимум, если найден больший по модулю
                }
            }

            // Проверяем, не равен ли максимум нулю
            if (maxAbs == 0)
            {
                Console.WriteLine("Ошибка: все элементы массива равны нулю, деление невозможно!"); // Сообщение об ошибке
                return; // Завершаем программу
            }

            // Нормируем массив, деля каждый элемент на максимальный по модулю
            for (int i = 0; i < n; i++)
            {
                array[i] /= maxAbs; // Делим элемент на максимальный по модулю
            }

            // Выводим нормированный массив
            Console.WriteLine("\nНормированный массив:");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{array[i]:F4} "); // Форматируем до 4 знаков после запятой
            }
            Console.WriteLine(); // Переходим на новую строку
        }
    }
}