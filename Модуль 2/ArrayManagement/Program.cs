using System; 
using System.Collections.Generic;
using System.Linq; 

namespace ArrayManagement 
{
    // Класс StringArray, представляющий одномерный массив строк фиксированной длины
    class StringArray
    {
        // Приватное поле для хранения массива строк
        private string[] array;

        // Свойство для получения длины массива
        public int Length { get { return array.Length; } } // Возвращает длину массива

        // Конструктор для инициализации массива заданной длины
        public StringArray(int length)
        {
            if (length > 0) // Проверяем, что длина положительная
            {
                array = new string[length]; // Инициализируем массив
            }
            else
            {
                throw new ArgumentException("Длина массива должна быть положительной!"); // Выбрасываем исключение при некорректной длине
            }
        }

        // Метод для установки значения элемента по индексу
        public void SetElement(int index, string value)
        {
            if (index >= 0 && index < array.Length) // Проверяем, что индекс в пределах массива
            {
                array[index] = value; // Устанавливаем значение
            }
            else
            {
                throw new IndexOutOfRangeException("Индекс выходит за пределы массива!"); // Выбрасываем исключение при выходе за пределы
            }
        }

        // Метод для получения значения элемента по индексу
        public string GetElement(int index)
        {
            if (index >= 0 && index < array.Length) // Проверяем, что индекс в пределах массива
            {
                return array[index]; // Возвращаем значение
            }
            else
            {
                throw new IndexOutOfRangeException("Индекс выходит за пределы массива!"); // Выбрасываем исключение при выходе за пределы
            }
        }

        // Метод для поэлементного сцепления двух массивов
        public static StringArray Concatenate(StringArray arr1, StringArray arr2)
        {
            if (arr1 == null || arr2 == null) // Проверяем, что массивы не null
            {
                throw new ArgumentNullException("Один из массивов пустой!");
            }
            StringArray result = new StringArray(arr1.Length + arr2.Length); // Создаём новый массив с суммарной длиной
            for (int i = 0; i < arr1.Length; i++)
            {
                result.SetElement(i, arr1.GetElement(i)); // Копируем элементы первого массива
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                result.SetElement(arr1.Length + i, arr2.GetElement(i)); // Копируем элементы второго массива
            }
            return result; // Возвращаем новый массив
        }

        // Метод для слияния двух массивов с исключением повторяющихся элементов
        public static StringArray Merge(StringArray arr1, StringArray arr2)
        {
            if (arr1 == null || arr2 == null) // Проверяем, что массивы не null
            {
                throw new ArgumentNullException("Один из массивов пустой!");
            }
            var uniqueElements = new HashSet<string>(arr1.array.Concat(arr2.array).Where(s => s != null)); // Собираем уникальные элементы
            StringArray result = new StringArray(uniqueElements.Count); // Создаём новый массив с длиной уникальных элементов
            int i = 0;
            foreach (var item in uniqueElements) // Заполняем массив уникальными значениями
            {
                result.SetElement(i++, item);
            }
            return result; // Возвращаем новый массив
        }

        // Метод для вывода всего массива
        public void PrintArray()
        {
            if (array.Length == 0) // Проверяем, пуст ли массив
            {
                Console.WriteLine("Массив пуст.");
            }
            else
            {
                Console.WriteLine("\nМассив:");
                for (int i = 0; i < array.Length; i++) // Перебираем и выводим элементы
                {
                    Console.WriteLine($"[{i}]: {array[i] ?? "null"}");
                }
            }
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа управления одномерным массивом строк.");

            // Создаём первый массив
            Console.Write("Введите длину первого массива: ");
            string inputLength1 = Console.ReadLine(); // Считываем длину
            StringArray array1 = null;
            if (int.TryParse(inputLength1, out int length1) && length1 > 0)
            {
                array1 = new StringArray(length1); // Инициализируем первый массив
                for (int i = 0; i < length1; i++) // Заполняем первый массив
                {
                    Console.Write($"Введите строку для индекса {i}: ");
                    array1.SetElement(i, Console.ReadLine()); // Устанавливаем значение
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректную длину!");
                return;
            }

            // Создаём второй массив
            Console.Write("Введите длину второго массива: ");
            string inputLength2 = Console.ReadLine(); // Считываем длину
            StringArray array2 = null;
            if (int.TryParse(inputLength2, out int length2) && length2 > 0)
            {
                array2 = new StringArray(length2); // Инициализируем второй массив
                for (int i = 0; i < length2; i++) // Заполняем второй массив
                {
                    Console.Write($"Введите строку для индекса {i}: ");
                    array2.SetElement(i, Console.ReadLine()); // Устанавливаем значение
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректную длину!");
                return;
            }

            // Цикл с меню
            while (true) // Бесконечный цикл для работы с меню
            {
                // Выводим меню
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Вывести элемент по индексу");
                Console.WriteLine("2 - Вывести первый массив");
                Console.WriteLine("3 - Вывести второй массив");
                Console.WriteLine("4 - Сцепить массивы");
                Console.WriteLine("5 - Слить массивы (без повторов)");
                Console.WriteLine("6 - Выход");
                Console.Write("Ваш выбор (1-6): ");

                string choice = Console.ReadLine(); // Считываем выбор пользователя

                try
                {
                    switch (choice)
                    {
                        case "1": // Вывод элемента по индексу
                            Console.Write("Введите индекс для первого массива: ");
                            string inputIndex = Console.ReadLine(); // Считываем индекс
                            if (int.TryParse(inputIndex, out int index) && index >= 0 && index < array1.Length)
                            {
                                Console.WriteLine($"Элемент: {array1.GetElement(index)}");
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: неверный индекс!");
                            }
                            break;

                        case "2": // Вывод первого массива
                            array1.PrintArray(); // Выводим первый массив
                            break;

                        case "3": // Вывод второго массива
                            array2.PrintArray(); // Выводим второй массив
                            break;

                        case "4": // Сцепление массивов
                            StringArray concatenated = StringArray.Concatenate(array1, array2); // Сцепляем массивы
                            concatenated.PrintArray(); // Выводим результат
                            break;

                        case "5": // Слияние массивов
                            StringArray merged = StringArray.Merge(array1, array2); // Сливаем массивы
                            merged.PrintArray(); // Выводим результат
                            break;

                        case "6": // Выход
                            Console.WriteLine("Программа завершена.");
                            return; // Выходим из программы

                        default:
                            Console.WriteLine("Ошибка: введите число от 1 до 6!");
                            break;
                    }
                }
                catch (Exception ex) // Обрабатываем возможные исключения
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}"); // Выводим сообщение об ошибке
                }
            }
        }
    }
}