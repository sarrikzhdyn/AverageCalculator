using System; 
namespace SortedIndices 
{
    class Program
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа создаёт массив из 10 вещественных чисел в диапазоне [-10, 10) и формирует массив индексов по возрастанию значений.");
            Console.WriteLine("Результаты будут показаны поэтапно. Нажмите Enter для продолжения.");

            // Ожидаем нажатия Enter для начала
            Console.ReadLine();

            // Создаём объект для генерации случайных чисел
            Random random = new Random();

            // Создаём массив из 10 вещественных чисел
            double[] numbers = new double[10];

            // Заполняем массив случайными числами из диапазона [-10, 10)
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.NextDouble() * 20 - 10; // Генерируем число от -10 до 10
            }

            // Выводим исходный массив
            Console.WriteLine("\nИсходный массив:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"{numbers[i]:F2} "); // Форматируем до 2 знаков после запятой
            }
            Console.WriteLine("\n\nНажмите Enter, чтобы найти минимальный и максимальный элементы.");
            Console.ReadLine(); // Ожидаем подтверждение пользователя

            // Находим минимальный и максимальный элементы
            int minIndex = 0;
            int maxIndex = 0;
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < numbers[minIndex])
                {
                    minIndex = i; // Обновляем индекс минимума
                }
                if (numbers[i] > numbers[maxIndex])
                {
                    maxIndex = i; // Обновляем индекс максимума
                }
            }

            // Выводим минимальный и максимальный элементы
            Console.WriteLine($"\nМинимальный элемент: {numbers[minIndex]:F2} (индекс {minIndex})");
            Console.WriteLine($"Максимальный элемент: {numbers[maxIndex]:F2} (индекс {maxIndex})");
            Console.WriteLine("\nНажмите Enter, чтобы сформировать массив индексов.");
            Console.ReadLine(); // Ожидаем подтверждение пользователя

            // Создаём массив индексов
            int[] indices = new int[10];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i; // Инициализируем индексы от 0 до 9
            }

            // Сортируем массив индексов по значениям чисел в numbers
            for (int i = 0; i < indices.Length - 1; i++)
            {
                for (int j = 0; j < indices.Length - 1 - i; j++)
                {
                    if (numbers[indices[j]] > numbers[indices[j + 1]])
                    {
                        // Меняем индексы местами
                        int temp = indices[j];
                        indices[j] = indices[j + 1];
                        indices[j + 1] = temp;
                    }
                }
            }

            // Выводим массив индексов
            Console.WriteLine("\nМассив индексов (по возрастанию значений):");
            for (int i = 0; i < indices.Length; i++)
            {
                Console.Write($"{indices[i]} ");
            }
            Console.WriteLine("\n\nНажмите Enter, чтобы показать элементы в порядке возрастания.");
            Console.ReadLine(); // Ожидаем подтверждение пользователя

            // Выводим элементы в порядке возрастания
            Console.WriteLine("\nЭлементы массива в порядке возрастания:");
            for (int i = 0; i < indices.Length; i++)
            {
                Console.Write($"{numbers[indices[i]]:F2} ");
            }
            Console.WriteLine("\n\nПрограмма завершена. Нажмите Enter для выхода.");
            Console.ReadLine(); // Ожидаем завершение
        }
    }
}