using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Задание_4
{
    public partial class MainWindow : Window
    {
        private List<DataItem> _dataItems;
        private List<DataItem> _filteredDataItems;
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            // Инициализация данных
            _dataItems = new List<DataItem>
            {
                new DataItem("Событие A", new DateTime(2024, 10, 1)),
                new DataItem("Событие B", new DateTime(2024, 10, 5)),
                new DataItem("Событие C", new DateTime(2024, 10, 8)),
                new DataItem("Событие D", new DateTime(2024, 9, 25)),
                new DataItem("Конференция", new DateTime(2024, 11, 12))
            };

            _filteredDataItems = new List<DataItem>(_dataItems);
            UpdateDataList();
        }

        private void UpdateDataList()
        {
            DataListBox.Items.Clear();
            foreach (var item in _dataItems)
            {
                DataListBox.Items.Add(item);
            }

            FilteredDataListBox.Items.Clear();
            foreach (var item in _filteredDataItems)
            {
                FilteredDataListBox.Items.Add(item);
            }
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterSelector.SelectedItem is ComboBoxItem selectedFilter && !string.IsNullOrWhiteSpace(FilterValueTextBox.Text))
            {
                Func<DataItem, bool> filter = null;

                switch (selectedFilter.Content.ToString())
                {
                    case "Фильтр по дате (после)":
                        if (DateTime.TryParse(FilterValueTextBox.Text, out DateTime filterDate))
                        {
                            filter = item => item.Date > filterDate;
                        }
                        else
                        {
                            MessageBox.Show("Введите корректную дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        break;

                    case "Фильтр по ключевому слову":
                        string keyword = FilterValueTextBox.Text.ToLower();
                        filter = item => item.Title.ToLower().Contains(keyword);
                        break;
                }

                if (filter != null)
                {
                    _filteredDataItems = _dataItems.Where(filter).ToList();
                    UpdateDataList();
                }
            }
            else
            {
                MessageBox.Show("Выберите фильтр и введите значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
