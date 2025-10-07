using System;

namespace ShapeWPF
{
    public interface IProduct
    {
        double GetCost();
        int GetStockQuantity();
    }
}