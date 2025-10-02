using System; 
using System.Collections.Generic; 

namespace AuthorBook 
{
    // Класс Author, представляющий автора
    class Author
    {
        // Приватное поле для хранения имени автора
        private string name; // Имя автора

        // Приватное поле для хранения года рождения автора
        private int birthYear; // Год рождения автора

        // Конструктор для инициализации имени и года рождения
        public Author(string authorName, int year)
        {
            if (!string.IsNullOrWhiteSpace(authorName)) // Проверяем, что имя не пустое
            {
                name = authorName; // Устанавливаем имя
            }
            else
            {
                throw new ArgumentException("Имя автора не может быть пустым!"); // Выбрасываем исключение при некорректном вводе
            }
            if (year > 0) // Проверяем, что год положительный
            {
                birthYear = year; // Устанавливаем год рождения
            }
            else
            {
                throw new ArgumentException("Год рождения должен быть положительным!"); // Выбрасываем исключение при некорректном вводе
            }
        }

        // Метод для получения имени автора
        public string GetName()
        {
            return name; // Возвращаем имя
        }

        // Метод для получения года рождения автора
        public int GetBirthYear()
        {
            return birthYear; // Возвращаем год рождения
        }

        // Метод для вывода информации об авторе
        public void PrintInfo()
        {
            Console.WriteLine($"Автор: {name}, Год рождения: {birthYear}"); // Выводим имя и год рождения
        }
    }

    // Класс Book, представляющий книгу с композицией объекта Author
    class Book
    {
        // Приватное поле для хранения названия книги
        private string title; // Название книги

        // Приватное поле для хранения года выпуска книги
        private int publishYear; // Год выпуска книги

        // Приватное поле для хранения объекта автора
        private Author author; // Объект автора, связанный с книгой

        // Конструктор для инициализации названия, года выпуска и автора
        public Book(string bookTitle, int year, Author bookAuthor)
        {
            if (!string.IsNullOrWhiteSpace(bookTitle)) // Проверяем, что название не пустое
            {
                title = bookTitle; // Устанавливаем название
            }
            else
            {
                throw new ArgumentException("Название книги не может быть пустым!"); // Выбрасываем исключение при некорректном вводе
            }
            if (year > 0) // Проверяем, что год положительный
            {
                publishYear = year; // Устанавливаем год выпуска
            }
            else
            {
                throw new ArgumentException("Год выпуска должен быть положительным!"); // Выбрасываем исключение при некорректном вводе
            }
            author = bookAuthor; // Устанавливаем автора
        }

        // Метод для вывода информации о книге и её авторе
        public void PrintInfo()
        {
            Console.WriteLine($"Книга: {title}, Год выпуска: {publishYear}"); // Выводим название и год выпуска
            author.PrintInfo(); // Вызываем метод автора для вывода его данных
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа для управления информацией об авторах и книгах.");

            // Создаём списки для хранения объектов Author и Book
            List<Author> authors = new List<Author>(); // Список авторов
            List<Book> books = new List<Book>(); // Список книг

            // Переменная для хранения выбора пользователя
            string choice;

            do
            {
                // Показываем меню с доступными действиями
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Добавить нового автора");
                Console.WriteLine("2 - Добавить новую книгу");
                Console.WriteLine("3 - Показать всех авторов");
                Console.WriteLine("4 - Показать все книги");
                Console.WriteLine("5 - Выйти");
                Console.Write("Ваш выбор (1-5): ");
                choice = Console.ReadLine(); // Считываем выбор пользователя

                try
                {
                    if (choice == "1")
                    {
                        // Запрашиваем данные для нового автора
                        Console.Write("Введите имя автора: ");
                        string authorName = Console.ReadLine(); // Считываем имя

                        Console.Write("Введите год рождения: ");
                        string inputBirthYear = Console.ReadLine(); // Считываем год рождения
                        int birthYear;
                        if (int.TryParse(inputBirthYear, out birthYear))
                        {
                            // Создаём нового автора
                            Author author = new Author(authorName, birthYear);
                            authors.Add(author); // Добавляем автора в список
                            Console.WriteLine("Автор добавлен!");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: год рождения должен быть целым числом!");
                        }
                    }
                    else if (choice == "2")
                    {
                        // Проверяем, есть ли авторы для выбора
                        if (authors.Count == 0)
                        {
                            Console.WriteLine("Ошибка: сначала добавьте автора!");
                            continue;
                        }

                        // Запрашиваем данные для новой книги
                        Console.Write("Введите название книги: ");
                        string bookTitle = Console.ReadLine(); // Считываем название

                        Console.Write("Введите год выпуска: ");
                        string inputPublishYear = Console.ReadLine(); // Считываем год выпуска
                        int publishYear;
                        if (int.TryParse(inputPublishYear, out publishYear))
                        {
                            // Показываем список авторов для выбора
                            Console.WriteLine("\nВыберите автора по номеру:");
                            for (int i = 0; i < authors.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {authors[i].GetName()} ({authors[i].GetBirthYear()})");
                            }
                            Console.Write("Номер автора: ");
                            string inputAuthorIndex = Console.ReadLine(); // Считываем номер автора
                            int authorIndex;
                            if (int.TryParse(inputAuthorIndex, out authorIndex) && authorIndex > 0 && authorIndex <= authors.Count)
                            {
                                // Создаём новую книгу с выбранным автором
                                Book book = new Book(bookTitle, publishYear, authors[authorIndex - 1]);
                                books.Add(book); // Добавляем книгу в список
                                Console.WriteLine("Книга добавлена!");
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: неверный номер автора!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: год выпуска должен быть целым числом!");
                        }
                    }
                    else if (choice == "3")
                    {
                        // Показываем всех авторов
                        if (authors.Count == 0)
                        {
                            Console.WriteLine("Список авторов пуст!");
                        }
                        else
                        {
                            Console.WriteLine("\nСписок авторов:");
                            for (int i = 0; i < authors.Count; i++)
                            {
                                Console.Write($"{i + 1}. ");
                                authors[i].PrintInfo();
                            }
                        }
                    }
                    else if (choice == "4")
                    {
                        // Показываем все книги
                        if (books.Count == 0)
                        {
                            Console.WriteLine("Список книг пуст!");
                        }
                        else
                        {
                            Console.WriteLine("\nСписок книг:");
                            for (int i = 0; i < books.Count; i++)
                            {
                                Console.Write($"{i + 1}. ");
                                books[i].PrintInfo();
                            }
                        }
                    }
                    else if (choice != "5")
                    {
                        Console.WriteLine("Неверный выбор! Введите 1, 2, 3, 4 или 5.");
                    }
                }
                catch (ArgumentException ex) // Обрабатываем исключения из конструкторов
                {
                    Console.WriteLine($"Ошибка: {ex.Message}"); // Выводим сообщение об ошибке
                }
                catch (Exception ex) // Обрабатываем другие возможные исключения
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}"); // Выводим общее сообщение об ошибке
                }

                // Спрашиваем, хочет ли пользователь продолжить, если не выбрал выход
                if (choice != "5")
                {
                    Console.Write("Продолжить? (да/нет): ");
                    choice = Console.ReadLine().ToLower(); // Считываем ответ и приводим к нижнему регистру
                }

            } while (choice == "да" || choice == "д" || choice == "yes" || choice == "y"); // Продолжаем цикл при положительном ответе

            Console.WriteLine("Программа завершена."); // Сообщаем о завершении программы
        }
    }
}