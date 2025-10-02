using System; 

namespace MatrixSortBySum 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа формирует прямоугольную матрицу и сортирует её строки по возрастанию сумм элементов.");

            // Запрашиваем количество строк
            Console.Write("Введите количество строк (положительное число): ");
            string inputRows = Console.ReadLine();
            int rows;
            if (!int.TryParse(inputRows, out rows) || rows <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число для строк!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Запрашиваем количество столбцов
            Console.Write("Введите количество столбцов (положительное число): ");
            string inputCols = Console.ReadLine();
            int cols;
            if (!int.TryParse(inputCols, out cols) || cols <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число для столбцов!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Создаём матрицу
            int[,] matrix = MatrixHelper.CreateMatrix(rows, cols);

            // Выводим исходную матрицу
            Console.WriteLine("\nИсходная матрица:");
            MatrixHelper.PrintMatrix(matrix);

            // Сортируем строки по суммам
            MatrixHelper.SortRowsBySum(matrix);

            // Выводим отсортированную матрицу
            Console.WriteLine("\nМатрица после сортировки строк по суммам:");
            MatrixHelper.PrintMatrix(matrix);
        }
    }

    // Класс для статических методов обработки матриц
    static class MatrixHelper
    {
        // Статический метод для создания матрицы со случайными значениями
        public static int[,] CreateMatrix(int rows, int cols)
        {
            Random random = new Random(); // Создаём объект Random
            int[,] matrix = new int[rows, cols]; // Создаём матрицу с заданными размерами
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = random.Next(-50, 51); // Генерируем число от -50 до 50
                }
            }
            return matrix; // Возвращаем заполненную матрицу
        }

        // Статический метод для вывода матрицы
        public static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0); // Количество строк
            int cols = matrix.GetLength(1); // Количество столбцов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],4}"); // Выводим с выравниванием (4 символа)
                }
                Console.WriteLine(); // Переходим на новую строку
            }
        }

        // Статический метод для вычисления суммы строки
        private static int CalculateRowSum(int[,] matrix, int row)
        {
            int sum = 0;
            int cols = matrix.GetLength(1); // Количество столбцов
            for (int j = 0; j < cols; j++)
            {
                sum += matrix[row, j]; // Суммируем элементы строки
            }
            return sum; // Возвращаем сумму
        }

        // Статический метод для сортировки строк по суммам
        public static void SortRowsBySum(int[,] matrix)
        {
            int rows = matrix.GetLength(0); // Количество строк
            int cols = matrix.GetLength(1); // Количество столбцов
            int[] sums = new int[rows]; // Массив для хранения сумм строк
            for (int i = 0; i < rows; i++)
            {
                sums[i] = CalculateRowSum(matrix, i); // Вычисляем сумму для каждой строки
            }

            // Сортируем строки с помощью пузырьковой сортировки
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < rows - 1 - i; j++)
                {
                    if (sums[j] > sums[j + 1])
                    {
                        // Меняем суммы
                        int tempSum = sums[j];
                        sums[j] = sums[j + 1];
                        sums[j + 1] = tempSum;

                        // Меняем строки матрицы
                        for (int k = 0; k < cols; k++)
                        {
                            int tempValue = matrix[j, k];
                            matrix[j, k] = matrix[j + 1, k];
                            matrix[j + 1, k] = tempValue;
                        }
                    }
                }
            }
        }
    }
}