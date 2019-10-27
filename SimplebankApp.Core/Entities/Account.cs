using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleBankApp.Core.Entities
{
    public class Account : IAccount
    {
        public Guid Id { get; set; }

        public CreditsCollection Credits { get; set; }
        public DebitsCollection Debits { get; set; }

        protected Account()
        {
            Credits = new CreditsCollection();
            Debits = new DebitsCollection();
        }

        public ICredit Deposit(IEntityFactory entityFactory, decimal amount)
        {
            var credit = entityFactory.NewCredit(this, amount, DateTime.UtcNow);
            Credits.Add(credit);
            return credit;
        }

        public IDebit Withdraw(IEntityFactory entityFactory, decimal amount)
        {
            if (amount > GetCurrentBalance())
                throw new Exception("Account has not enough funds.");

            var debit = entityFactory.NewDebit(this, amount, DateTime.UtcNow);
            Debits.Add(debit);
            return debit;
        }

        public decimal GetCurrentBalance()
        {
            var totalCredits = Credits.GetTotal();
            var totalDebits = Debits.GetTotal();
            return totalCredits - totalDebits;
        }
    }

    public interface IAccount : IEntity
    {
        public ICredit Deposit(IEntityFactory entityFactory, decimal amount);
        public IDebit Withdraw(IEntityFactory entityFactory, decimal amount);
        public decimal GetCurrentBalance();
    }

    #region collections

    public class CreditsCollection
    {
        private readonly IList<ICredit> _credits;

        public CreditsCollection()
        {
            _credits = new List<ICredit>();
        }

        public void Add<T>(IEnumerable<T> credits)
        where T : ICredit
        {
            foreach (var credit in credits)
            {
                Add(credit);
            }
        }

        public void Add(ICredit credit)
            => _credits.Add(credit);

        public IReadOnlyCollection<ICredit> GetTransactions()
        {
            var transactions = new ReadOnlyCollection<ICredit>(_credits);
            return transactions;
        }

        public decimal GetTotal()
        {
            var total = new decimal(0);

            foreach (ICredit credit in _credits)
            {
                total = credit.Sum(total);
            }

            return total;
        }
    }

    public class DebitsCollection
    {
        private readonly IList<IDebit> _debits;

        public DebitsCollection()
        {
            _debits = new List<IDebit>();
        }

        public void Add<T>(IEnumerable<T> debits)
        where T : IDebit
        {
            foreach (var debit in debits)
            {
                Add(debit);
            }
        }

        public void Add(IDebit debit)
            => _debits.Add(debit);

        public IReadOnlyCollection<IDebit> GetTransactions()
        {
            IReadOnlyCollection<IDebit> transactions = new ReadOnlyCollection<IDebit>(_debits);
            return transactions;
        }

        public decimal GetTotal()
        {
            var total = new decimal(0);

            foreach (IDebit debit in _debits)
            {
                total = debit.Sum(total);
            }

            return total;
        }
    }

    #endregion

}
