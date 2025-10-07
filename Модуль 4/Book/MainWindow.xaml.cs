using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LibraryApp
{
    public partial class MainWindow : Window
    {
        private List<IBook> books = new List<IBook>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BookTypeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Ничего не делаем, просто инициализация
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleTextBox.Text.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    ResultTextBlock.Text = "Ошибка: введите название книги!";
                    return;
                }

                IBook newBook;
                if (BookTypeComboBox.SelectedItem is ComboBoxItem item && item.Content.ToString() == "Художественная")
                {
                    newBook = new FictionBook(title);
                }
                else
                {
                    newBook = new NonFictionBook(title);
                }

                books.Add(newBook);
                ResultTextBlock.Text = $"Книга добавлена: {newBook.GetStatus()}";
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void IssueBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleTextBox.Text.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    ResultTextBlock.Text = "Ошибка: введите название книги!";
                    return;
                }

                IBook book = books.Find(b => b.Title == title);
                if (book != null)
                {
                    book.IssueBook();
                    ResultTextBlock.Text = $"Книга выдана: {book.GetStatus()}";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: книга не найдена!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleTextBox.Text.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    ResultTextBlock.Text = "Ошибка: введите название книги!";
                    return;
                }

                IBook book = books.Find(b => b.Title == title);
                if (book != null)
                {
                    ResultTextBlock.Text = book.GetStatus();
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: книга не найдена!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}