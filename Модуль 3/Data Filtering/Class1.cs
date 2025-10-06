using System;

namespace Задание_4
{
    public class DataItem
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public DataItem(string title, DateTime date)
        {
            Title = title;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Title} - {Date.ToShortDateString()}";
        }
    }
}
