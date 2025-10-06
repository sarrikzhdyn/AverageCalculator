using System;

namespace SortingApp
{
    public class SortingLogic
    {
        private readonly Random _random = new Random();

        // Генерация случайных чисел
        public int[] GenerateRandomNumbers(int count)
        {
            if (count < 1 || count > 100)
                throw new ArgumentException("Количество чисел должно быть от 1 до 100.");

            int[] randomNumbers = new int[count];
            for (int i = 0; i < count; i++)
            {
                randomNumbers[i] = _random.Next(1, 101);
            }
            return randomNumbers;
        }

        // Пузырьковая сортировка
        public int[] BubbleSort(int[] numbers)
        {
            int[] result = (int[])numbers.Clone();
            int n = result.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (result[j] > result[j + 1])
                    {
                        int temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }
            return result;
        }

        // Быстрая сортировка
        public int[] QuickSort(int[] numbers)
        {
            int[] result = (int[])numbers.Clone();
            QuickSortRecursive(result, 0, result.Length - 1);
            return result;
        }

        private void QuickSortRecursive(int[] numbers, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(numbers, low, high);
                QuickSortRecursive(numbers, low, pi - 1);
                QuickSortRecursive(numbers, pi + 1, high);
            }
        }

        private int Partition(int[] numbers, int low, int high)
        {
            int pivot = numbers[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (numbers[j] <= pivot)
                {
                    i++;
                    int temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }
            int temp1 = numbers[i + 1];
            numbers[i + 1] = numbers[high];
            numbers[high] = temp1;
            return i + 1;
        }
    }
}