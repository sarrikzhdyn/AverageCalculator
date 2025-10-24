using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace TextEditorWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Попытка открыть диалог...");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл для открытия";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Старт с рабочего стола
                MessageBox.Show("Диалог инициализирован, нажмите OK для открытия...");
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Файл выбран: " + openFileDialog.FileName);
                    try
                    {
                        string fileContent = File.ReadAllText(openFileDialog.FileName);
                        txtEditor.Text = fileContent;
                        MessageBox.Show("Файл успешно загружен!");
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show("Нет прав доступа к файлу: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("Файл не найден: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при открытии файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Диалог закрыт или отменён пользователем.");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить файл";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
                        MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}