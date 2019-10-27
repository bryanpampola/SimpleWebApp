using SimpleBankApp.Core.Entities;
using System;
using System.Collections.Generic;

namespace SimpleBankApp.Infrastructure.Models
{
    public class Account : Core.Entities.Account
    {
        public Guid UserId { get; set; }

        public Account(IUser user)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
        }

        public void Load(IList<Core.Entities.Credit> credits, IList<Debit> debits)
        {
            Credits = new CreditsCollection();
            Credits.Add(credits);

            Debits = new DebitsCollection();
            Debits.Add(debits);
        }
    }
}
