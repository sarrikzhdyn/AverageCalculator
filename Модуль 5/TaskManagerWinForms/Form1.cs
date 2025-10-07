using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TaskManagerWinForms
{
    public partial class Form1 : Form
    {
        private List<string> tasks = new List<string>(); // Список для хранения задач

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTask.Text))
            {
                string newTask = txtTask.Text; // Получаем текст из поля ввода
                tasks.Add(newTask); // Добавление задачи в список
                lbTasks.Items.Add(newTask); // Добавление в CheckedListBox
                txtTask.Clear(); // Очистка поля ввода
            }
            else
            {
                MessageBox.Show("Введите текст задачи!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbTasks.SelectedIndex != -1)
            {
                tasks.RemoveAt(lbTasks.SelectedIndex); // Удаление из списка
                lbTasks.Items.RemoveAt(lbTasks.SelectedIndex); // Удаление из CheckedListBox
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMarkDone_Click(object sender, EventArgs e)
        {
            if (lbTasks.SelectedIndex != -1)
            {
                // Переключение состояния чекбокса для выбранной задачи
                lbTasks.SetItemChecked(lbTasks.SelectedIndex, !lbTasks.GetItemChecked(lbTasks.SelectedIndex));
                MessageBox.Show("Состояние задачи обновлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите задачу для отметки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}