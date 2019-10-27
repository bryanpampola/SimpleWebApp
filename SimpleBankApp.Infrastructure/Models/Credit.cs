using SimpleBankApp.Core.Entities;
using System;

namespace SimpleBankApp.Infrastructure.Models
{

    public class Credit : Core.Entities.Credit
    {
        public Guid AccountId { get; set; }

        public Credit(IAccount account, decimal amount, DateTime transactionDate)
        {
            AccountId = account.Id;
            Amount = amount;
            TransactionDate = transactionDate;
        }
    }
}
