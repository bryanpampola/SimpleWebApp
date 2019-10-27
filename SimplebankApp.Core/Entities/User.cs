using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleBankApp.Core.Entities
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public AccountCollection Accounts { get; set; }

        public User()
        {
            Accounts = new AccountCollection();
        }

        public void Register(IAccount account)
        {
            Accounts ??= new AccountCollection();
            Accounts.Add(account.Id);
        }

    }

    public class AccountCollection
    {
        private readonly IList<Guid> _accountIds;

        public AccountCollection()
        {
            _accountIds = new List<Guid>();
        }
        public void Add(IEnumerable<Guid> accounts)
        {
            foreach (var account in accounts)
            {
                Add(account);
            }
        }
        public void Add(Guid accountId)
            => _accountIds.Add(accountId);
        public IReadOnlyCollection<Guid> GetAccountIds()
        {
            IReadOnlyCollection<Guid> accountIds = new ReadOnlyCollection<Guid>(_accountIds);
            return accountIds;
        }
    }

    public interface IUser : IEntity
    {
        AccountCollection Accounts { get; }
        void Register(IAccount account);
    }
}
