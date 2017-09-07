using NUnit.Framework;
using System;
using VendingMachine.Implementations;

namespace VendingMachine.Test
{
    [TestFixture]
    public class BankAccountTest
    {
        private BankAccount bankAccount;

        [Test]
        public void Deposit_200_Fund_To_Account_Shoud_Return_Account_With_200_Balance()
        {
            // Arrange
            bankAccount = new BankAccount();
            //Act
            bankAccount.Deposit(200);
            //Assert
            Assert.AreEqual(200, bankAccount.Balance);
        }

        [Test]
        public void Deposit_200_Fund_To_Account_And_Withdraw_100_Shoud_Return_Account_With_100_Balance()
        {
            // Arrange
            bankAccount = new BankAccount();
            //Act
            bankAccount.Deposit(200);
            bankAccount.Withdraw(100);
            //Assert
            Assert.AreEqual(100, bankAccount.Balance);
        }

        [Test]
        public void Get_Initial_Balance_Should_Return_0()
        {
            // Arrange
            //Act
            bankAccount = new BankAccount();
            //Assert
            Assert.AreEqual(0, bankAccount.Balance);
        }

        [Test]
        public void Withdraw_Empty_Account_Should_Return_Exception()
        {
            //Arrange
            bankAccount = new BankAccount();

            Assert.Throws<Exception>(() => bankAccount.Withdraw(10));
        }
    }
}