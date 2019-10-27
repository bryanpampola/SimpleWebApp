using System;

namespace SimpleBankApp.Core.Entities
{
    public interface IEntity
    {
        public Guid Id { get; }
    }

    public interface IEntityFactory
    {
        public IAccount NewAccount(IUser user);
        public ICredit NewCredit(IAccount account, decimal amountToDeposit, DateTime transactionDate);
        public IUser NewUser(string loginName, string password);
        public IDebit NewDebit(IAccount account, decimal amountToWithdraw, DateTime transactionDate);
    }
}
