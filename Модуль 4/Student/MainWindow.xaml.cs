using System;
using System.Windows;
using System.Windows.Controls;

namespace StudentApp
{
    public partial class MainWindow : Window
    {
        private IStudent currentStudent;

        public MainWindow()
        {
            InitializeComponent();
            InputPanel.Visibility = Visibility.Collapsed; // Скрываем поля ввода до выбора типа
        }

        private void CourseComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            InputPanel.Visibility = Visibility.Visible;
        }

        private void CreateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                string[] gradeStrings = GradesTextBox.Text.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                int[] grades = Array.ConvertAll(gradeStrings, int.Parse);

                if (CourseComboBox.SelectedItem is ComboBoxItem item)
                {
                    switch (item.Content.ToString())
                    {
                        case "1-й курс":
                            currentStudent = new FirstYearStudent(name, grades);
                            ResultTextBlock.Text = $"Создан студент: {name}, 1-й курс, Средний балл: {currentStudent.GetAverageGrade():F2}";
                            break;
                        case "2-й курс":
                            currentStudent = new SecondYearStudent(name, grades);
                            ResultTextBlock.Text = $"Создан студент: {name}, 2-й курс, Средний балл: {currentStudent.GetAverageGrade():F2}";
                            break;
                        case "3-й курс":
                            currentStudent = new ThirdYearStudent(name, grades);
                            ResultTextBlock.Text = $"Создан студент: {name}, 3-й курс, Средний балл: {currentStudent.GetAverageGrade():F2}";
                            break;
                        case "4-й курс":
                            currentStudent = new FourthYearStudent(name, grades);
                            ResultTextBlock.Text = $"Создан студент: {name}, 4-й курс, Средний балл: {currentStudent.GetAverageGrade():F2}";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CalculateGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentStudent != null)
                {
                    ResultTextBlock.Text = $"Средний балл: {currentStudent.GetAverageGrade():F2}";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: сначала создайте студента!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void GetCourseInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentStudent != null)
                {
                    ResultTextBlock.Text = currentStudent.GetCourseInfo();
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: сначала создайте студента!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}