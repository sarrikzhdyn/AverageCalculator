using System; 

namespace ArraySum 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим сообщение о назначении программы
            Console.WriteLine("Программа создаёт массив из 10 случайных целых чисел и вычисляет их сумму.");

            // Создаём объект для генерации случайных чисел
            Random random = new Random();

            // Создаём массив из 10 целых чисел
            int[] numbers = new int[10];

            // Заполняем массив случайными числами от 1 до 100
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(1, 101); // Генерируем число от 1 до 100 (101 не включается)
            }

            // Выводим элементы массива
            Console.Write("Созданный массив: ");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i]);
                if (i < numbers.Length - 1)
                {
                    Console.Write(", "); // Добавляем запятую между элементами, кроме последнего
                }
            }
            Console.WriteLine(); // Переходим на новую строку после вывода массива

            // Вычисляем сумму элементов массива
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i]; // Добавляем каждый элемент к сумме
            }

            // Выводим сумму элементов
            Console.WriteLine($"Сумма всех элементов массива: {sum}");
        }
    }
}