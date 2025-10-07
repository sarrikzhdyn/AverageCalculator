using System;

namespace LibraryApp
{
    public class NonFictionBook : IBook
    {
        public string Title { get; }
        public bool IsAvailable { get; private set; }

        public NonFictionBook(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Название книги не может быть пустым!");
            this.Title = title;
            this.IsAvailable = true;
        }

        public void IssueBook()
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("Книга уже выдана!");
            }
            IsAvailable = false;
        }

        public string GetStatus()
        {
            return $"{Title} (Нехудожественная), Доступна: {IsAvailable}";
        }
    }
}