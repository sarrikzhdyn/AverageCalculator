using System; 

namespace MinMaxIndices 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа создаёт массив из K элементов, заполняет случайными числами из диапазона [A, B) и выводит элементы между минимальным и максимальным.");

            // Запрашиваем размер массива K
            Console.Write("Введите размер массива K: ");
            string inputK = Console.ReadLine(); // Считываем ввод
            int k;
            if (!int.TryParse(inputK, out k) || k <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число для K!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Запрашиваем начало диапазона A
            Console.Write("Введите начало диапазона A: ");
            string inputA = Console.ReadLine(); // Считываем ввод
            int a;
            if (!int.TryParse(inputA, out a))
            {
                Console.WriteLine("Ошибка: введите целое число для A!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Запрашиваем конец диапазона B
            Console.Write("Введите конец диапазона B: ");
            string inputB = Console.ReadLine(); // Считываем ввод
            int b;
            if (!int.TryParse(inputB, out b) || b <= a)
            {
                Console.WriteLine("Ошибка: B должно быть целым числом и больше A!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Создаём объект для генерации случайных чисел
            Random random = new Random();

            // Создаём массив из K целых чисел
            int[] array = new int[k];

            // Заполняем массив случайными числами из диапазона [A, B)
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(a, b); // Генерируем число от A до B-1
            }

            // Выводим исходный массив
            Console.WriteLine("\nИсходный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine(); // Переходим на новую строку

            // Находим индексы минимального и максимального элементов
            int minIndex = 0; // Предполагаем, что минимум — первый элемент
            int maxIndex = 0; // Предполагаем, что максимум — первый элемент
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[minIndex])
                {
                    minIndex = i; // Обновляем индекс минимума
                }
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i; // Обновляем индекс максимума
                }
            }

            // Выводим минимальный и максимальный элементы с их индексами
            Console.WriteLine($"\nМинимальный элемент: {array[minIndex]} (индекс {minIndex})");
            Console.WriteLine($"Максимальный элемент: {array[maxIndex]} (индекс {maxIndex})");

            // Определяем границы для вывода (от меньшего индекса к большему)
            int startIndex = Math.Min(minIndex, maxIndex); // Начало диапазона
            int endIndex = Math.Max(minIndex, maxIndex); // Конец диапазона

            // Выводим элементы между минимальным и максимальным (включая их)
            Console.WriteLine("\nЭлементы между минимальным и максимальным (включая их):");
            if (startIndex == endIndex)
            {
                Console.WriteLine(array[startIndex]); // Если индексы совпадают, выводим только один элемент
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Console.Write($"{array[i]} ");
                }
                Console.WriteLine(); // Переходим на новую строку
            }
        }
    }
}