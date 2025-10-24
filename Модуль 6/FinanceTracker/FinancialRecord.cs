using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker
{
    public class FinancialRecord
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public RecordType Type { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
    }

    public enum RecordType
    {
        Income,
        Expense
    }
}
