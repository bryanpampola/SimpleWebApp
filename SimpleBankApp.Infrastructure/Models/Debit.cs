using SimpleBankApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBankApp.Infrastructure.Models
{
    public class Debit : Core.Entities.Debit
    {
        public Guid AccountId { get; set; }

        public Debit(IAccount account, decimal amount, DateTime transactionDate)
        {
            AccountId = account.Id;
            Amount = amount;
            TransactionDate = transactionDate;
        }
    }
}
