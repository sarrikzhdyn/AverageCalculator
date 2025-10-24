using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinanceTracker
{
    /// <summary>
    /// Логика взаимодействия для RecordEditDialog.xaml
    /// </summary>
    public partial class RecordEditDialog : Window
    {
        public FinancialRecord Record { get; private set; }

        public RecordEditDialog()
        {
            InitializeComponent();
            Record = new FinancialRecord
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Description = string.Empty,
                Category = string.Empty
            };
            InitializeControls();
        }

        public RecordEditDialog(FinancialRecord record)
        {
            InitializeComponent();
            Record = record;
            InitializeControls();
        }

        private void InitializeControls()
        {
            cmbType.SelectedValue = Record.Type.ToString();
            txtDescription.Text = Record.Description;
            txtAmount.Text = Record.Amount > 0 ? Record.Amount.ToString("F2") : "";
            txtCategory.Text = Record.Category;
            dpDate.SelectedDate = Record.Date;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                Record.Type = cmbType.SelectedValue?.ToString() == "Income" ? RecordType.Income : RecordType.Expense;
                Record.Description = txtDescription.Text ?? string.Empty;
                Record.Amount = decimal.Parse(txtAmount.Text);
                Record.Category = txtCategory.Text ?? string.Empty;
                Record.Date = dpDate.SelectedDate ?? DateTime.Now;

                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Введите описание", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
