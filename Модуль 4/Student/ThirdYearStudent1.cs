using System;
using System.Linq;

namespace StudentApp
{
    public class ThirdYearStudent : IStudent
    {
        private string name;
        private int[] grades;

        public ThirdYearStudent(string name, int[] grades)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Имя не может быть пустым!");
            if (grades == null || grades.Length == 0) throw new ArgumentException("Баллы не могут быть пустыми!");

            this.name = name;
            this.grades = grades;
        }

        public double GetAverageGrade()
        {
            return grades.Average();
        }

        public string GetCourseInfo()
        {
            return $"{name} - 3-й курс, Средний балл: {GetAverageGrade():F2}";
        }
    }
}