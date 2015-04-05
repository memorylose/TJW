using System;

namespace TJW.Model
{
    public class NetIncome
    {
        public int IncomeId { get; set; }

        public string OrderNumber { get; set; }

        public double Price { get; set; }

        public bool IsSuc { get; set; }

        public int Grade { get; set; }

        public string TradeNumber { get; set; }

        public DateTime IncomeTime { get; set; }

        public DateTime IncomeSucTime { get; set; }
    }
}
