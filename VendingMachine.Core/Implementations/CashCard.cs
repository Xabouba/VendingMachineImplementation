using System;
using VendingMachine.Interfaces;

namespace VendingMachine.Implementations
{
    public class CashCard : ICashCard
    {
        public CashCard (int pinCode, IBankAccount bankAccount)
        {
            PinCode = pinCode;
            BankAccount = bankAccount;
        }

        public CashCard(IBankAccount bankAccount)
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            PinCode = _rdm.Next(_min, _max);

            BankAccount = bankAccount;
        }

        public void WithdrawFromAccount(double amount)
        {
            BankAccount.Withdraw(amount);
        }

        public int PinCode { get; private set; }
        public IBankAccount BankAccount { get; private set; }
    }
}