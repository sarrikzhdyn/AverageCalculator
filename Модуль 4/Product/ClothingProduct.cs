using System;

namespace ShapeWPF
{
    public class ClothingProduct : IProduct
    {
        private string name;
        private double pricePerUnit;
        private int stockQuantity;
        private string size;

        public ClothingProduct(string name, double pricePerUnit, int stockQuantity, string size)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Название не может быть пустым!");
            if (pricePerUnit < 0) throw new ArgumentException("Цена не может быть отрицательной!");
            if (stockQuantity < 0) throw new ArgumentException("Остаток не может быть отрицательным!");
            if (string.IsNullOrEmpty(size)) throw new ArgumentException("Размер не может быть пустым!");

            this.name = name;
            this.pricePerUnit = pricePerUnit;
            this.stockQuantity = stockQuantity;
            this.size = size;
        }

        public double GetCost()
        {
            return pricePerUnit * stockQuantity;
        }

        public int GetStockQuantity()
        {
            return stockQuantity;
        }

        public void UpdateStock(int newQuantity)
        {
            if (newQuantity >= 0)
            {
                stockQuantity = newQuantity;
            }
            else
            {
                throw new ArgumentException("Новый остаток не может быть отрицательным!");
            }
        }

        public string GetName()
        {
            return name;
        }

        public string GetSize()
        {
            return size;
        }
    }
}