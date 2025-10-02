using System;
using System.Collections.Generic;

namespace MinimalArraySum 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа создаёт массив с минимальным количеством элементов, сумма которых не превышает заданное число.");

            // Запрашиваем максимальную сумму
            Console.Write("Введите максимальную сумму (положительное число): ");
            string inputSum = Console.ReadLine();
            int sumLimit;
            if (!int.TryParse(inputSum, out sumLimit) || sumLimit <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Создаём массив с минимальным количеством элементов
            int[] array = ArrayHelper.CreateMinimalArray(sumLimit);

            // Выводим массив
            Console.WriteLine("\nСозданный массив:");
            ArrayHelper.PrintArray(array);

            // Выводим сумму элементов
            int sum = ArrayHelper.CalculateSum(array);
            Console.WriteLine($"Сумма элементов: {sum}");
        }
    }

    // Класс для статических методов обработки массивов
    static class ArrayHelper
    {
        // Статический метод для создания массива с минимальным количеством элементов
        public static int[] CreateMinimalArray(int sumLimit)
        {
            Random random = new Random(); // Создаём объект Random
            List<int> tempList = new List<int>(); // Временный список для элементов
            int remainingSum = sumLimit; // Остаток суммы для распределения

            // Определяем начальное количество элементов (максимум 9 элементов для лимита)
            int maxElements = Math.Min(9, sumLimit); // Не больше 9, так как числа от 1 до 9
            int elementsCount = 1; // Минимальное количество элементов
            while (elementsCount <= maxElements)
            {
                int targetSumPerElement = remainingSum / elementsCount; // Целевая сумма на элемент
                if (targetSumPerElement >= 1) // Убеждаемся, что элемент может быть хотя бы 1
                {
                    tempList.Clear(); // Очищаем список для нового расчёта
                    int currentSum = 0;
                    for (int i = 0; i < elementsCount; i++)
                    {
                        int maxValue = Math.Min(9, remainingSum - currentSum); // Максимальное возможное значение
                        if (maxValue < 1) break; // Если не можем добавить, прерываем
                        int value = random.Next(1, maxValue + 1); // Случайное число от 1 до maxValue
                        tempList.Add(value);
                        currentSum += value;
                    }
                    if (currentSum <= sumLimit) // Если сумма в пределах лимита, используем этот массив
                    {
                        remainingSum = sumLimit - currentSum;
                        if (remainingSum == 0 || elementsCount == maxElements)
                        {
                            return tempList.ToArray(); // Возвращаем массив, если больше добавить нельзя
                        }
                    }
                }
                elementsCount++; // Увеличиваем количество элементов, если текущий вариант не подошёл
            }

            // Если не удалось заполнить, возвращаем минимальный возможный массив
            if (tempList.Count == 0 && sumLimit >= 1)
            {
                tempList.Add(sumLimit); // Если лимит позволяет, добавляем сам лимит
            }
            return tempList.ToArray();
        }

        // Статический метод для вывода массива
        public static void PrintArray(int[] array)
        {
            if (array.Length == 0)
            {
                Console.WriteLine("Массив пустой.");
                return;
            }
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine(); // Переходим на новую строку
        }

        // Статический метод для вычисления суммы элементов
        public static int CalculateSum(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i]; // Суммируем элементы
            }
            return sum; // Возвращаем сумму
        }
    }
}