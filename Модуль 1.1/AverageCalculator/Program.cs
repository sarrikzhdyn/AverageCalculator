using System;

namespace AverageCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите три числа для вычисления среднего арифметического.");

            // Запрашиваем первое число
            Console.Write("Введите первое число: ");
            string input1 = Console.ReadLine();
            if (!double.TryParse(input1, out double number1))
            {
                Console.WriteLine("Ошибка: введите корректное число!");
                return; // Завершаем программу при ошибке
            }

            // Запрашиваем второе число
            Console.Write("Введите второе число: ");
            string input2 = Console.ReadLine();
            if (!double.TryParse(input2, out double number2))
            {
                Console.WriteLine("Ошибка: введите корректное число!");
                return;
            }

            // Запрашиваем третье число
            Console.Write("Введите третье число: ");
            string input3 = Console.ReadLine();
            if (!double.TryParse(input3, out double number3))
            {
                Console.WriteLine("Ошибка: введите корректное число!");
                return;
            }

            // Вычисляем среднее арифметическое
            double average = (number1 + number2 + number3) / 3;

            // Выводим результат
            Console.WriteLine($"Среднее арифметическое чисел {number1}, {number2}, {number3} равно {average:F2}");
        }
    }
}