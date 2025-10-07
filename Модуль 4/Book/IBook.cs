using System;

namespace LibraryApp
{
    public interface IBook
    {
        string Title { get; }
        bool IsAvailable { get; }
        void IssueBook();
        string GetStatus();
    }
}