using SimpleBankApp.Core.Entities;
using System;
using System.Collections.Generic;

namespace SimpleBankApp.Infrastructure.Models
{
    public class User : Core.Entities.User
    {

        //// includes database, so not applicable
        //public bool Authenticate()
        //{
        //    return true;
        //}

        public User(string loginName, string password)
        {
            Id = Guid.NewGuid();
            LoginName = loginName;
            Password = password;
        }

        public void LoadAccounts(IEnumerable<Guid> accounts)
        {
            Accounts = new AccountCollection();
            Accounts.Add(accounts);
        }
    }
}
