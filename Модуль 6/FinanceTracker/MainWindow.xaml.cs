using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FinanceTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FirebaseService? _firebaseService;
        private List<FinancialRecord> _records = new List<FinancialRecord>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeFirebase();
            LoadRecordsAsync();
        }

        private void InitializeFirebase()
        {
            try
            {
                // Используем настройки из AppSettings
                string firebaseUrl = AppSettings.FirebaseUrl;

                if (string.IsNullOrEmpty(firebaseUrl))
                {
                    ShowSettingsDialog();
                    firebaseUrl = AppSettings.FirebaseUrl;
                }

                if (!string.IsNullOrEmpty(firebaseUrl))
                {
                    _firebaseService = new FirebaseService(firebaseUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации Firebase: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadRecordsAsync()
        {
            try
            {
                if (_firebaseService == null) return;

                _records = await _firebaseService.GetAllRecordsAsync();
                dgRecords.ItemsSource = _records;
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateStatistics()
        {
            if (_records == null || !_records.Any())
            {
                txtTotalIncome.Text = "Доходы: 0.00";
                txtTotalExpense.Text = "Расходы: 0.00";
                txtBalance.Text = "Баланс: 0.00";
                txtBalance.Foreground = Brushes.Green;
                return;
            }

            decimal totalIncome = _records.Where(r => r.Type == RecordType.Income).Sum(r => r.Amount);
            decimal totalExpense = _records.Where(r => r.Type == RecordType.Expense).Sum(r => r.Amount);
            decimal balance = totalIncome - totalExpense;

            txtTotalIncome.Text = $"Доходы: {totalIncome:N2}";
            txtTotalExpense.Text = $"Расходы: {totalExpense:N2}";
            txtBalance.Text = $"Баланс: {balance:N2}";
            txtBalance.Foreground = balance >= 0 ? Brushes.Green : Brushes.Red;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new RecordEditDialog();
            if (dialog.ShowDialog() == true)
            {
                var newRecord = dialog.Record;
                AddRecordAsync(newRecord);
            }
        }

        private async void AddRecordAsync(FinancialRecord record)
        {
            try
            {
                if (_firebaseService == null)
                {
                    MessageBox.Show("Firebase не инициализирован", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _firebaseService.AddRecordAsync(record);
                LoadRecordsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecord = dgRecords.SelectedItem as FinancialRecord;
            if (selectedRecord != null)
            {
                var dialog = new RecordEditDialog(selectedRecord);
                if (dialog.ShowDialog() == true)
                {
                    var updatedRecord = dialog.Record;
                    updatedRecord.Id = selectedRecord.Id;
                    UpdateRecordAsync(updatedRecord);
                }
            }
        }

        private async void UpdateRecordAsync(FinancialRecord record)
        {
            try
            {
                if (_firebaseService == null)
                {
                    MessageBox.Show("Firebase не инициализирован", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _firebaseService.UpdateRecordAsync(record);
                LoadRecordsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecord = dgRecords.SelectedItem as FinancialRecord;
            if (selectedRecord != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                                           "Подтверждение удаления",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (_firebaseService == null)
                        {
                            MessageBox.Show("Firebase не инициализирован", "Ошибка",
                                          MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        await _firebaseService.DeleteRecordAsync(selectedRecord.Id);
                        LoadRecordsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRecordsAsync();
        }

        private void DgRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection = dgRecords.SelectedItem != null;
            btnEdit.IsEnabled = hasSelection;
            btnDelete.IsEnabled = hasSelection;
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            ShowSettingsDialog();
        }

        private void ShowSettingsDialog()
        {
            var dialog = new SettingsDialog();
            if (dialog.ShowDialog() == true)
            {
                InitializeFirebase();
                LoadRecordsAsync();
            }
        }
    }
}
