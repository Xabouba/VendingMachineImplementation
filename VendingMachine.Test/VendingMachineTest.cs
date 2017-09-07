using NUnit.Framework;
using System;
using System.Threading;
using VendingMachine.Implementations;
using VendingMachine.Interfaces;

namespace VendingMachine.Test
{
    [TestFixture]
    public class VendingMachineTest
    {
        private IUser _user;
        private ICashCard _cashCardA;
        private ICashCard _cashCardB;
        private IBankAccount _bankAccount;
        private IVendingMachine _vendingMachine;

        [SetUp]
        public void Init()
        {
            _vendingMachine = new VendingMachineObj();
            _bankAccount = new BankAccount();
            _cashCardA = new CashCard(_bankAccount);
            _cashCardB = new CashCard(_bankAccount);
        }

        [Test]
        public void Buy_One_Can_With_Sufficient_Fund_With_Correct_Pin_Should_Return_24_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _cashCardA.BankAccount.Deposit(100);
            _vendingMachine.BuyCan(_cashCardA, _cashCardA.PinCode);

            // Assert
            Assert.AreEqual(24, _vendingMachine.CansInventory);
        }

        [Test]
        public void Buy_One_Can_With_Empty_Fund_With_Correct_Pin_Should_Return_Exception()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);


            // Act

            // Assert
            Assert.Throws<Exception>(() => _vendingMachine.BuyCan(_user.CashCardA, _user.CashCardA.PinCode));

        }
        [Test]
        public void Buy_3_Cans_With_UnSufficient_Fund_With_Correct_Pin_Should_Return_Exception()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _user.CashCardA.BankAccount.Deposit(1);
            // Assert
            Assert.Throws<Exception>(() => BuyCansWithCorrectPin(3, _user.CashCardA));
        }

        [Test]
        public void Buy_One_Can_With_Sufficient_Fund_With_Incorrect_Pin_Should_Return_25_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);
            // Act
            var differentPin = _user.CashCardA.PinCode == 1000 ? 1001 : 1000;
            _user.CashCardA.BankAccount.Deposit(100);
            BuyCansWithInCorrectPin(1, _user.CashCardA);

            // Assert
            Assert.AreEqual(25, _vendingMachine.CansInventory);
        }

        [Test]
        public void Buy_10_Cans_With_Sufficient_Fund_With_Correct_Pin_Should_Return_15_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _cashCardA.BankAccount.Deposit(100);
            BuyCansWithCorrectPin(10, _user.CashCardA);

            // Assert
            Assert.AreEqual(15, _vendingMachine.CansInventory);
        }

        [Test]
        public void Buy_2_Cans_With_Sufficient_Fund_With_Correct_Pin_And_MultiTHreaded_Should_Return_23_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _user.CashCardA.BankAccount.Deposit(5);

            // Person 1
            Thread withdraw1 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(1, cashCard);
            });

            // Person 2
            Thread withdraw2 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(1, cashCard);
            });

            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);
            withdraw1.Start(_user.CashCardA);
            withdraw2.Start(_user.CashCardB);

            withdraw1.Join();
            withdraw2.Join();
            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);

            // Assert
            Assert.AreEqual(23, _vendingMachine.CansInventory);
        }

        [Test]
        public void Buy_Too_Many_Cans_With_Sufficient_Fund_With_Correct_Pin_And_MultiTHreaded_Should_Return_23_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _user.CashCardA.BankAccount.Deposit(25);

            // Person 1
            Thread withdraw1 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(15, cashCard);
            });

            // Person 2
            Thread withdraw2 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(15, cashCard);
            });

            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);
            withdraw1.Start(_user.CashCardA);
            withdraw2.Start(_user.CashCardB);

            withdraw1.Join();
            withdraw2.Join();
            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);

            // Assert
            Assert.AreEqual(0, _vendingMachine.CansInventory);
        }

        [Test]
        public void Buy_Cans_With_Sufficient_Fund_With_Correct_And_Incorrect_Pin_And_MultiTHreaded_Should_Return_23_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _user.CashCardA.BankAccount.Deposit(25);

            // Person 1
            Thread withdraw1 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithInCorrectPin(15, cashCard);
            });

            // Person 2
            Thread withdraw2 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(15, cashCard);
            });

            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);
            withdraw1.Start(_user.CashCardA);
            withdraw2.Start(_user.CashCardB);

            withdraw1.Join();
            withdraw2.Join();
            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);

            // Assert
            Assert.AreEqual(10, _vendingMachine.CansInventory);
        }

        [Test]
        public void Buy_Too_Many_Cans_With_UnSufficient_Fund_With_Correct_Pin_And_MultiTHreaded_Should_Return_23_Cans_Vending_Machine()
        {
            // Arrange
            _user = new User(_cashCardA, _cashCardB);

            // Act
            _user.CashCardA.BankAccount.Deposit(5);
            // Person 1
            Thread withdraw1 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(15, cashCard);
            });

            // Person 2
            Thread withdraw2 = new Thread(param =>
            {
                CashCard cashCard = param as CashCard;
                BuyCansWithCorrectPin(15, cashCard);
            });

            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);
            withdraw1.Start(_user.CashCardA);
            withdraw2.Start(_user.CashCardB);

            withdraw1.Join();
            withdraw2.Join();
            Console.WriteLine("Balance = {0:c}", _user.CashCardA.BankAccount.Balance);
            // Assert
            Assert.AreEqual(15, _vendingMachine.CansInventory);
        }

        private void BuyCansWithInCorrectPin(int cansnumber, ICashCard card)
        {
            var differentPin = card.PinCode == 1000 ? 1001 : 1000;
            BuyCans(cansnumber, card, differentPin);
        }

        private void BuyCansWithCorrectPin(int cansnumber, ICashCard card)
        {
            BuyCans(cansnumber, card, card.PinCode);
        }

        private void BuyCans(int cansnumber, ICashCard card, int pinCode)
        {
            while (cansnumber > 0)
            {
                _vendingMachine.BuyCan(card, pinCode);
                cansnumber--;
            }
        }
    }
}