using NUnit.Framework;
using SimpleBankApp.Infrastructure.DataAccess;
using System.Linq;

namespace SimpleBankApp.Tests.Core
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void RegisterAccount_AddSingleAccount_ExpectSingleAccount()
        {
            var factory = new EntityFactory();
            var user = factory.NewUser("bryanpampola", "password");
            var account = factory.NewAccount(user);
            
            user.Register(account);

            Assert.AreEqual(1, user.Accounts.GetAccountIds().Count);
            Assert.AreEqual(account.Id, user.Accounts.GetAccountIds().First());
        }

    }
}
