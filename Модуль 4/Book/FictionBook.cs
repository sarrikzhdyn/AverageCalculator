using System;

namespace LibraryApp
{
    public class FictionBook : IBook
    {
        public string Title { get; }
        public bool IsAvailable { get; private set; }

        public FictionBook(string title)
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
            return $"{Title} (Художественная), Доступна: {IsAvailable}";
        }
    }
}