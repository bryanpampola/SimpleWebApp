using System;

namespace SimpleBankApp.Core.Entities
{
    public class Credit : ICredit
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get => "Credit"; }
        public DateTime TransactionDate { get; set; }

        public decimal Sum(decimal amount)
        {
            return Amount += amount;
        }

    }

    public interface ICredit
    {
        public decimal Sum(decimal amount);
    }
}
