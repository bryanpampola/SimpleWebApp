using NUnit.Framework;
using SimpleBankApp.Core.Entities;
using SimpleBankApp.Infrastructure.DataAccess;
using System;
using System.Linq;

namespace SimpleBankApp.Tests.Core
{
    [TestFixture]
    public class AccountsTests
    {
        private static EntityFactory _factory = new EntityFactory();
        private IAccount TestAccount()
        {
            var user = _factory.NewUser("bryanpampola", "password");
            return _factory.NewAccount(user);
        }

        [Test]
        public void MakesAccountDeposit_ExpectsCorrectCreditAmount()
        {
            var depositedAmount = 500;
            var account = TestAccount();

            var credit = account.Deposit(_factory, depositedAmount) as Credit;

            Assert.AreEqual(depositedAmount, credit.Amount);
            Assert.AreEqual("Credit", credit.Description);
        }

        [Test]
        public void MakesAccountWithdrawal_ExpectsCorrectDebitAmount()
        {
            var withdrawnAmount = 500;
            var account = TestAccount();
            account.Deposit(_factory, 600);

            var debit = account.Withdraw(_factory, withdrawnAmount) as Debit;

            Assert.AreEqual(withdrawnAmount, debit.Amount);
            Assert.AreEqual("Debit", debit.Description);
        }

        [Test]
        public void MakesAccountWithdrawal_InvalidAmount_ThrowsException()
        {
            var withdrawnAmount = 500;
            var account = TestAccount();
            account.Deposit(_factory, 499);

            var exc = Assert.Throws<Exception>(() => account.Withdraw(_factory, withdrawnAmount));
            Assert.AreEqual("Account has not enough funds.", exc.Message);
        }

        [Test]
        public void CheckCurrentBalance_MultipleTransaction_ExpectsCorrectAmount()
        {
            var depositedAmount = 5000;
            var firstWithdrawnAmount = 500;
            var secondWithdrawnAmount = 300;
            var expectedRunningBalance = depositedAmount - (firstWithdrawnAmount + secondWithdrawnAmount);
            
            var account = TestAccount();
            account.Deposit(_factory, depositedAmount);
            account.Withdraw(_factory, firstWithdrawnAmount);
            account.Withdraw(_factory, secondWithdrawnAmount);

            var debits = (account as Account).Debits;
            var credits = (account as Account).Credits;

            
            Assert.AreEqual(depositedAmount, (credits.GetTransactions().First() as Credit).Amount);
            Assert.AreEqual(1, credits.GetTransactions().Count);
            
            Assert.AreEqual(firstWithdrawnAmount, (debits.GetTransactions().First() as Debit).Amount);
            Assert.AreEqual(secondWithdrawnAmount, (debits.GetTransactions().Skip(1).First() as Debit).Amount);
            Assert.AreEqual(2, debits.GetTransactions().Count);

            Assert.AreEqual(expectedRunningBalance, account.GetCurrentBalance());
        }
    }
}
