using System;

namespace SimpleBankApp.Core.Entities
{
    public class Debit : IDebit
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get => "Debit"; }
        public DateTime TransactionDate { get; set; }

        public decimal Sum(decimal amount)
        {
            return Amount += amount;
        }
    }

    public interface IDebit
    {
        public decimal Sum(decimal amount);
    }
}
