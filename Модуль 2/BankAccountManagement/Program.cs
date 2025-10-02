using System; 
using System.Collections.Generic; 

namespace BankAccountManagement 
{
    // Класс BankAccount, представляющий банковский счет
    class BankAccount
    {
        // Приватное поле для хранения номера счета
        private string accountNumber; // Уникальный идентификатор счета

        // Приватное поле для хранения имени владельца
        private string owner; // Имя владельца счета

        // Приватное поле для хранения баланса
        private double balance; // Текущий баланс счета

        // Конструктор класса BankAccount с параметрами
        public BankAccount(string accountNumber, string owner, double initialBalance)
        {
            if (!string.IsNullOrWhiteSpace(accountNumber) && !string.IsNullOrWhiteSpace(owner) && initialBalance >= 0) // Проверяем корректность входных данных
            {
                this.accountNumber = accountNumber; // Устанавливаем номер счета
                this.owner = owner; // Устанавливаем имя владельца
                this.balance = initialBalance; // Устанавливаем начальный баланс
            }
            else
            {
                throw new ArgumentException("Номер счета, имя владельца и начальный баланс должны быть корректными (баланс не отрицательный)!"); // Выбрасываем исключение при некорректных данных
            }
        }

        // Метод для получения номера счета
        public string GetAccountNumber() // Возвращает номер счета
        {
            return accountNumber; // Возвращаем значение номера счета
        }

        // Метод для получения имени владельца
        public string GetOwner() // Возвращает имя владельца
        {
            return owner; // Возвращаем значение имени владельца
        }

        // Метод для получения баланса
        public double GetBalance() // Возвращает текущий баланс
        {
            return balance; // Возвращаем значение баланса
        }

        // Метод для установки нового владельца
        public void SetOwner(string newOwner) // Устанавливает нового владельца
        {
            if (!string.IsNullOrWhiteSpace(newOwner)) // Проверяем, что имя не пустое
            {
                owner = newOwner; // Обновляем имя владельца
            }
            else
            {
                throw new ArgumentException("Имя владельца не может быть пустым!"); // Выбрасываем исключение при некорректном вводе
            }
        }

        // Метод для пополнения счета
        public void Deposit(double amount) // Выполняет пополнение счета
        {
            if (amount > 0) // Проверяем, что сумма положительная
            {
                balance += amount; // Увеличиваем баланс
                Console.WriteLine($"Пополнение на {amount} успешно. Новый баланс: {balance}"); // Выводим сообщение об успехе
            }
            else
            {
                throw new ArgumentException("Сумма пополнения должна быть положительной!"); // Выбрасываем исключение при некорректной сумме
            }
        }

        // Метод для снятия средств со счета
        public void Withdraw(double amount) // Выполняет снятие средств со счета
        {
            if (amount > 0) // Проверяем, что сумма положительная
            {
                if (amount <= balance) // Проверяем, достаточно ли средств
                {
                    balance -= amount; // Уменьшаем баланс
                    Console.WriteLine($"Снятие {amount} успешно. Новый баланс: {balance}"); // Выводим сообщение об успехе
                }
                else
                {
                    throw new ArgumentException("Недостаточно средств на счете!"); // Выбрасываем исключение при недостатке средств
                }
            }
            else
            {
                throw new ArgumentException("Сумма снятия должна быть положительной!"); // Выбрасываем исключение при некорректной сумме
            }
        }
    }

    class Program // Основной класс программы, содержащий точку входа
    {
        static void Main(string[] args) // Точка входа в программу, где начинается выполнение
        {
            // Выводим приветственное сообщение
            Console.WriteLine("Программа управления банковскими счетами.");

            // Создаём список для хранения банковских счетов
            List<BankAccount> accounts = new List<BankAccount>(); // Список для хранения всех счетов

            try
            {
                while (true) // Бесконечный цикл для добавления новых счетов
                {
                    Console.Write("\nВведите номер счета (или 'exit' для завершения добавления): "); // Запрашиваем номер счета
                    string accountNumber = Console.ReadLine(); // Считываем номер счета

                    if (accountNumber.ToLower() == "exit") // Проверяем условие выхода
                    {
                        break; // Выходим из цикла
                    }

                    Console.Write("Введите имя владельца: "); // Запрашиваем имя владельца
                    string owner = Console.ReadLine(); // Считываем имя владельца

                    Console.Write("Введите начальный баланс: "); // Запрашиваем начальный баланс
                    double initialBalance = double.Parse(Console.ReadLine()); // Считываем и преобразуем в double

                    // Создаём новый объект банковского счета
                    BankAccount account = new BankAccount(accountNumber, owner, initialBalance); // Создаём новый счет
                    accounts.Add(account); // Добавляем счет в список

                    Console.WriteLine($"Счет создан. Номер: {account.GetAccountNumber()}, Владелец: {account.GetOwner()}, Баланс: {account.GetBalance()}");
                }

                if (accounts.Count == 0) // Проверяем, есть ли счета
                {
                    Console.WriteLine("Нет созданных счетов.");
                }
                else
                {
                    // Цикл для выполнения операций с существующими счетами
                    while (true) // Бесконечный цикл для операций
                    {
                        Console.WriteLine("\nСписок счетов:");
                        for (int i = 0; i < accounts.Count; i++) // Выводим все счета
                        {
                            Console.WriteLine($"{i + 1}. Номер: {accounts[i].GetAccountNumber()}, Владелец: {accounts[i].GetOwner()}, Баланс: {accounts[i].GetBalance()}");
                        }

                        Console.Write("Выберите номер счета для операции (1-" + accounts.Count + ") или 'exit' для завершения: "); // Запрашиваем номер счета для операции
                        string input = Console.ReadLine(); // Считываем выбор

                        if (input.ToLower() == "exit") // Проверяем условие выхода
                        {
                            break; // Выходим из цикла
                        }

                        int accountIndex;
                        if (int.TryParse(input, out accountIndex) && accountIndex > 0 && accountIndex <= accounts.Count) // Проверяем корректность выбора
                        {
                            BankAccount selectedAccount = accounts[accountIndex - 1]; // Выбираем счет по индексу

                            Console.Write("Введите сумму для пополнения: "); // Запрашиваем сумму для пополнения
                            double depositAmount = double.Parse(Console.ReadLine()); // Считываем и преобразуем в double
                            try
                            {
                                selectedAccount.Deposit(depositAmount); // Выполняем пополнение
                            }
                            catch (ArgumentException ex) // Обрабатываем исключения
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}"); // Выводим сообщение об ошибке
                            }

                            Console.Write("Введите сумму для снятия (или 0 для пропуска): "); // Запрашиваем сумму для снятия
                            double withdrawAmount = double.Parse(Console.ReadLine()); // Считываем и преобразуем в double
                            if (withdrawAmount > 0)
                            {
                                try
                                {
                                    selectedAccount.Withdraw(withdrawAmount); // Выполняем снятие
                                }
                                catch (ArgumentException ex) // Обрабатываем исключения
                                {
                                    Console.WriteLine($"Ошибка: {ex.Message}"); // Выводим сообщение об ошибке
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите корректный номер счета или 'exit' для завершения!"); // Сообщаем об ошибке
                        }
                    }
                }
            }
            catch (ArgumentException ex) // Обрабатываем исключения из конструктора
            {
                Console.WriteLine($"Ошибка при создании счета: {ex.Message}"); // Выводим сообщение об ошибке
            }
            catch (FormatException) // Обрабатываем ошибки преобразования строки в число
            {
                Console.WriteLine("Ошибка: введите корректное числовое значение!"); // Выводим сообщение об ошибке
            }
            catch (Exception ex) // Обрабатываем другие возможные исключения
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}"); // Выводим общее сообщение об ошибке
            }

            Console.WriteLine("Программа завершена. Нажмите Enter для выхода."); // Сообщаем о завершении
            Console.ReadLine(); // Ждём ввода для завершения
        }
    }
}