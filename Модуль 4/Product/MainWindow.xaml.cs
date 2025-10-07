using System;
using System.Windows;
using System.Windows.Controls;

namespace ShapeWPF
{
    public partial class MainWindow : Window
    {
        private IProduct currentProduct;

        public MainWindow()
        {
            InitializeComponent();
            InputPanel.Visibility = Visibility.Collapsed; // Скрываем поля ввода до выбора типа
        }

        private void ProductTypeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            InputPanel.Visibility = Visibility.Visible;
            ExtraTextBox.Text = ""; // Сбрасываем дополнительное поле
            InstructionTextBlock.Text = "Выберите тип товара и заполните поля";
            if (ProductTypeComboBox.SelectedItem is ComboBoxItem item)
            {
                switch (item.Content.ToString())
                {
                    case "Продукты питания":
                        InstructionTextBlock.Text = "Введите название, цену и остаток в поля";
                        break;
                    case "Электроника":
                        InstructionTextBlock.Text = "Введите название, цену, остаток и гарантию в поля";
                        break;
                    case "Одежда":
                        InstructionTextBlock.Text = "Введите название, цену, остаток и размер в поля";
                        break;
                }
            }
        }

        private void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                if (double.TryParse(PriceTextBox.Text, out double price) && int.TryParse(QuantityTextBox.Text, out int quantity))
                {
                    if (ProductTypeComboBox.SelectedItem is ComboBoxItem item)
                    {
                        switch (item.Content.ToString())
                        {
                            case "Продукты питания":
                                currentProduct = new FoodProduct(name, price, quantity);
                                ResultTextBlock.Text = $"Создан товар: {((FoodProduct)currentProduct).GetName()}, Стоимость: {currentProduct.GetCost():F2}, Остаток: {currentProduct.GetStockQuantity()}";
                                break;
                            case "Электроника":
                                if (double.TryParse(ExtraTextBox.Text, out double warranty))
                                {
                                    currentProduct = new ElectronicsProduct(name, price, quantity, warranty);
                                    ResultTextBlock.Text = $"Создан товар: {((ElectronicsProduct)currentProduct).GetName()}, Стоимость: {currentProduct.GetCost():F2}, Остаток: {currentProduct.GetStockQuantity()}, Гарантия: {((ElectronicsProduct)currentProduct).GetWarrantyMonths()} месяцев";
                                }
                                break;
                            case "Одежда":
                                string size = ExtraTextBox.Text;
                                if (!string.IsNullOrEmpty(size))
                                {
                                    currentProduct = new ClothingProduct(name, price, quantity, size);
                                    ResultTextBlock.Text = $"Создан товар: {((ClothingProduct)currentProduct).GetName()}, Стоимость: {currentProduct.GetCost():F2}, Остаток: {currentProduct.GetStockQuantity()}, Размер: {((ClothingProduct)currentProduct).GetSize()}";
                                }
                                break;
                        }
                    }
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: введите корректные значения цены и остатка!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void UpdateStockButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentProduct != null && int.TryParse(QuantityTextBox.Text, out int newQuantity))
                {
                    // Приводим к конкретному типу для вызова UpdateStock
                    if (currentProduct is FoodProduct food)
                    {
                        food.UpdateStock(newQuantity);
                        ResultTextBlock.Text = $"Остаток обновлён. Новый остаток: {food.GetStockQuantity()}, Стоимость: {food.GetCost():F2}";
                    }
                    else if (currentProduct is ElectronicsProduct elec)
                    {
                        elec.UpdateStock(newQuantity);
                        ResultTextBlock.Text = $"Остаток обновлён. Новый остаток: {elec.GetStockQuantity()}, Стоимость: {elec.GetCost():F2}";
                    }
                    else if (currentProduct is ClothingProduct cloth)
                    {
                        cloth.UpdateStock(newQuantity);
                        ResultTextBlock.Text = $"Остаток обновлён. Новый остаток: {cloth.GetStockQuantity()}, Стоимость: {cloth.GetCost():F2}";
                    }
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка: сначала создайте товар и введите корректный остаток!";
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}