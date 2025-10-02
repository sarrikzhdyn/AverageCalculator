using System; 

namespace FractionReducer 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа сокращает обыкновенную дробь, используя НОД.");

            // Запрашиваем числитель
            Console.Write("Введите неотрицательный числитель: ");
            string inputNumerator = Console.ReadLine();
            int numerator;
            if (!int.TryParse(inputNumerator, out numerator) || numerator < 0)
            {
                Console.WriteLine("Ошибка: числитель должен быть неотрицательным целым числом!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Запрашиваем знаменатель
            Console.Write("Введите положительный знаменатель: ");
            string inputDenominator = Console.ReadLine();
            int denominator;
            if (!int.TryParse(inputDenominator, out denominator) || denominator <= 0)
            {
                Console.WriteLine("Ошибка: знаменатель должен быть положительным целым числом!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Выводим исходную дробь
            Console.WriteLine($"\nИсходная дробь: {numerator}/{denominator}");

            // Вычисляем НОД числителя и знаменателя
            int gcd = MathHelper.GreatestCommonDivisor(numerator, denominator);

            // Сокращаем дробь
            int reducedNumerator = numerator / gcd;
            int reducedDenominator = denominator / gcd;

            // Выводим сокращённую дробь
            Console.WriteLine($"Сокращённая дробь: {reducedNumerator}/{reducedDenominator}");
        }
    }

    // Класс для статических математических методов
    static class MathHelper
    {
        // Статический метод для вычисления НОД двух чисел (алгоритм Евклида)
        public static int GreatestCommonDivisor(int a, int b)
        {
            // Приводим числа к неотрицательным значениям
            a = Math.Abs(a);
            b = Math.Abs(b);

            // Если одно из чисел равно 0, возвращаем другое (или 1, если оба 0)
            if (a == 0 && b == 0)
            {
                return 1; // Соглашение: НОД(0,0) = 1, чтобы избежать деления на 0
            }
            if (a == 0)
            {
                return b;
            }
            if (b == 0)
            {
                return a;
            }

            // Алгоритм Евклида: НОД(a,b) = НОД(b, a mod b)
            while (b != 0)
            {
                int temp = b;
                b = a % b; // Остаток от деления
                a = temp;
            }

            return a; // Возвращаем НОД
        }
    }
}