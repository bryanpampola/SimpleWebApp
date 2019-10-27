using SimpleBankApp.Core.Entities;
using System;

namespace SimpleBankApp.Infrastructure.DataAccess
{
    public class EntityFactory : IEntityFactory
    {
        public IAccount NewAccount(IUser user) 
            => new Models.Account(user);

        public ICredit NewCredit(IAccount account, decimal amountToDeposit, DateTime transactionDate)
            => new Models.Credit(account, amountToDeposit, transactionDate);

        public IUser NewUser(string loginName, string password) 
            => new Models.User(loginName, password);

        public IDebit NewDebit(IAccount account, decimal amountToWithdraw, DateTime transactionDate)
            => new Models.Debit(account, amountToWithdraw, transactionDate);
    }
}
