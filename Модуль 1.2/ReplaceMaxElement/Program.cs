using System; 

namespace ReplaceMaxElement 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа создаёт массив из 10 целых чисел, показывает максимальный элемент и заменяет его на введённое число.");

            // Создаём объект для генерации случайных чисел
            Random random = new Random();

            // Создаём массив из 10 целых чисел
            int[] array = new int[10];

            // Заполняем массив случайными числами от 1 до 100
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 101); // Генерируем число от 1 до 100 (101 не включается)
            }

            // Выводим исходный массив
            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine(); // Переходим на новую строку

            // Находим максимальный элемент и его индекс
            int maxIndex = 0; // Предполагаем, что максимум — первый элемент
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i; // Обновляем индекс, если найден больший элемент
                }
            }

            // Выводим максимальный элемент
            Console.WriteLine($"Максимальный элемент: {array[maxIndex]}");

            // Запрашиваем целое число для замены максимального элемента
            Console.Write("Введите целое число для замены максимального элемента: ");
            string input = Console.ReadLine(); // Считываем ввод
            int newValue;

            // Проверяем, является ли ввод корректным целым числом
            if (!int.TryParse(input, out newValue))
            {
                Console.WriteLine("Ошибка: введите корректное целое число!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Заменяем максимальный элемент на введённое число
            array[maxIndex] = newValue; // Устанавливаем новое значение

            // Выводим изменённый массив
            Console.WriteLine("Изменённый массив:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine(); // Переходим на новую строку
        }
    }
}