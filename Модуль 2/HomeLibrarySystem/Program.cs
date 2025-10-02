using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace HomeLibrarySystem // Определяем пространство имён для организации кода
{
    // Класс Book, представляющий книгу в библиотеке
    class Book
    {
        // Поле для хранения названия книги
        public string Title { get; set; }

        // Поле для хранения автора книги
        public string Author { get; set; }

        // Поле для хранения года издания книги
        public int Year { get; set; }

        // Конструктор для инициализации книги
        public Book(string title, string author, int year)
        {
            Title = title; // Устанавливаем название
            Author = author; // Устанавливаем автора
            Year = year; // Устанавливаем год
        }

        // Метод для вывода информации о книге
        public override string ToString()
        {
            return $"Название: {Title}, Автор: {Author}, Год: {Year}"; // Возвращаем строковое представление книги
        }
    }

    // Класс HomeLibrary, представляющий домашнюю библиотеку
    class HomeLibrary
    {
        // Список для хранения книг
        private List<Book> books;

        // Конструктор для инициализации библиотеки
        public HomeLibrary()
        {
            books = new List<Book>(); // Инициализируем список книг
        }

        // Метод для добавления книги в библиотеку
        public void AddBook(Book book)
        {
            books.Add(book); // Добавляем книгу в список
            Console.WriteLine("Книга добавлена.");
        }

        // Метод для удаления книги по индексу
        public void RemoveBook(int index)
        {
            if (index >= 0 && index < books.Count) // Проверяем корректность индекса
            {
                books.RemoveAt(index); // Удаляем книгу по индексу
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный индекс.");
            }
        }

        // Метод для поиска книг по автору
        public List<Book> SearchByAuthor(string author)
        {
            return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList(); // Ищем книги по автору (нечувствительно к регистру)
        }

        // Метод для поиска книг по году издания
        public List<Book> SearchByYear(int year)
        {
            return books.Where(b => b.Year == year).ToList(); // Ищем книги по году
        }

        // Метод для сортировки книг по заданному полю
        public void SortBy(string field)
        {
            if (field == "title") // Сортировка по названию
            {
                books = books.OrderBy(b => b.Title).ToList();
            }
            else if (field == "author") // Сортировка по автору
            {
                books = books.OrderBy(b => b.Author).ToList();
            }
            else if (field == "year") // Сортировка по году
            {
                books = books.OrderBy(b => b.Year).ToList();
            }
            else
            {
                Console.WriteLine("Ошибка: неверное поле для сортировки (title, author, year).");
                return;
            }
            Console.WriteLine("Библиотека отсортирована.");
        }

        // Метод для вывода всех книг в библиотеке
        public void PrintAllBooks()
        {
            if (books.Count == 0) // Проверяем, пуста ли библиотека
            {
                Console.WriteLine("Библиотека пуста.");
            }
            else
            {
                Console.WriteLine("\nСписок книг:");
                for (int i = 0; i < books.Count; i++) // Перебираем и выводим книги с индексами
                {
                    Console.WriteLine($"{i}: {books[i]}");
                }
            }
        }

        // Метод для получения количества книг
        public int GetBookCount()
        {
            return books.Count; // Возвращаем количество книг
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа управления домашней библиотекой.");

            // Создаём объект домашней библиотеки
            HomeLibrary library = new HomeLibrary();

            // Цикл с меню
            while (true) // Бесконечный цикл для работы с меню
            {
                // Выводим меню
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Добавить книгу");
                Console.WriteLine("2 - Удалить книгу");
                Console.WriteLine("3 - Поиск по автору");
                Console.WriteLine("4 - Поиск по году");
                Console.WriteLine("5 - Сортировать библиотеку");
                Console.WriteLine("6 - Просмотреть все книги");
                Console.WriteLine("7 - Выход");
                Console.Write("Ваш выбор (1-7): ");

                string choice = Console.ReadLine(); // Считываем выбор пользователя

                try
                {
                    switch (choice)
                    {
                        case "1": // Добавление книги
                            while (true) // Вложенный цикл для добавления нескольких книг
                            {
                                Console.Write("Введите название книги (или 'exit' для возврата в меню): ");
                                string title = Console.ReadLine(); // Считываем название
                                if (title.ToLower() == "exit") // Проверяем условие выхода
                                {
                                    break; // Выходим из цикла добавления
                                }

                                Console.Write("Введите автора книги: ");
                                string author = Console.ReadLine(); // Считываем автора

                                Console.Write("Введите год издания: ");
                                string inputYear = Console.ReadLine(); // Считываем год
                                if (int.TryParse(inputYear, out int year) && year > 0) // Проверяем корректность года
                                {
                                    Book book = new Book(title, author, year); // Создаём объект книги
                                    library.AddBook(book); // Добавляем книгу в библиотеку
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка: год должен быть положительным целым числом!");
                                }
                            }
                            break;

                        case "2": // Удаление книги
                            library.PrintAllBooks(); // Выводим текущий список
                            Console.Write("Введите индекс книги для удаления: ");
                            string inputIndex = Console.ReadLine(); // Считываем индекс
                            if (int.TryParse(inputIndex, out int index))
                            {
                                library.RemoveBook(index); // Удаляем книгу
                                library.PrintAllBooks(); // Выводим обновленный список
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: введите корректный индекс!");
                            }
                            break;

                        case "3": // Поиск по автору
                            Console.Write("Введите автора для поиска: ");
                            string searchAuthor = Console.ReadLine(); // Считываем автора
                            List<Book> foundByAuthor = library.SearchByAuthor(searchAuthor); // Ищем книги
                            Console.WriteLine("\nКниги автора:");
                            if (foundByAuthor.Count == 0)
                            {
                                Console.WriteLine("Книги не найдены.");
                            }
                            else
                            {
                                foreach (Book b in foundByAuthor) // Выводим найденные книги
                                {
                                    Console.WriteLine(b);
                                }
                            }
                            break;

                        case "4": // Поиск по году
                            Console.Write("Введите год для поиска: ");
                            string inputYearSearch = Console.ReadLine(); // Считываем год
                            if (int.TryParse(inputYearSearch, out int searchYear) && searchYear > 0)
                            {
                                List<Book> foundByYear = library.SearchByYear(searchYear); // Ищем книги
                                Console.WriteLine("\nКниги за год:");
                                if (foundByYear.Count == 0)
                                {
                                    Console.WriteLine("Книги не найдены.");
                                }
                                else
                                {
                                    foreach (Book b in foundByYear) // Выводим найденные книги
                                    {
                                        Console.WriteLine(b);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: введите корректный год!");
                            }
                            break;

                        case "5": // Сортировка
                            Console.Write("Введите поле для сортировки (title, author, year): ");
                            string sortField = Console.ReadLine(); // Считываем поле
                            library.SortBy(sortField.ToLower()); // Сортируем библиотеку
                            library.PrintAllBooks(); // Выводим отсортированный список
                            break;

                        case "6": // Просмотр всех книг
                            library.PrintAllBooks(); // Выводим текущий список
                            break;

                        case "7": // Выход
                            Console.WriteLine("Программа завершена.");
                            return; // Выходим из программы

                        default:
                            Console.WriteLine("Ошибка: введите число от 1 до 7!");
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