using System;
using System.Collections.Generic; 

namespace RussianConsonants 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа создаёт массив из K случайных букв русского алфавита и выделяет согласные.");

            // Запрашиваем размер массива K
            Console.Write("Введите размер массива K: ");
            string inputK = Console.ReadLine(); // Считываем ввод
            int k;
            if (!int.TryParse(inputK, out k) || k <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число для K!"); // Сообщение об ошибке
                return; // Завершаем программу при некорректном вводе
            }

            // Создаём объект для генерации случайных чисел
            Random random = new Random();

            // Определяем русский алфавит (33 буквы)
            char[] russianAlphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й',
                                       'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф',
                                       'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

            // Создаём массив из K символов
            char[] letters = new char[k];

            // Заполняем массив случайными буквами русского алфавита
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = russianAlphabet[random.Next(0, russianAlphabet.Length)]; // Выбираем случайную букву
            }

            // Выводим исходный массив
            Console.WriteLine("\nИсходный массив букв:");
            for (int i = 0; i < letters.Length; i++)
            {
                Console.Write($"{letters[i]} ");
            }
            Console.WriteLine(); // Переходим на новую строку

            // Создаём список для согласных букв
            List<char> consonants = new List<char>();

            // Определяем гласные буквы для фильтрации
            char[] vowels = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };

            // Фильтруем согласные буквы
            for (int i = 0; i < letters.Length; i++)
            {
                bool isVowel = false;
                for (int j = 0; j < vowels.Length; j++)
                {
                    if (letters[i] == vowels[j])
                    {
                        isVowel = true; // Если буква гласная, помечаем
                        break;
                    }
                }
                if (!isVowel && letters[i] != 'ь' && letters[i] != 'ъ' && letters[i] != 'й') // Исключаем мягкий/твёрдый знак и 'й'
                {
                    consonants.Add(letters[i]); // Добавляем согласную в список
                }
            }

            // Выводим массив согласных
            Console.WriteLine("\nМассив согласных букв:");
            if (consonants.Count == 0)
            {
                Console.WriteLine("Согласные буквы отсутствуют.");
            }
            else
            {
                for (int i = 0; i < consonants.Count; i++)
                {
                    Console.Write($"{consonants[i]} ");
                }
                Console.WriteLine(); // Переходим на новую строку
            }
        }
    }
}