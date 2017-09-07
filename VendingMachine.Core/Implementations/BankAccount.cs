using System;
using VendingMachine.Interfaces;

namespace VendingMachine.Implementations
{
    public class BankAccount : IBankAccount
    {
        private double _balance;
        
        public void Deposit(double amount)
        {
            lock (this)
            {
                _balance += amount;
            }
        }
        public void Withdraw(double amount)
        {
            lock (this)
            {
                if (amount > _balance)
                {
                    throw new Exception("Insufficient funds");
                }
                _balance -= amount;
            }
        }
        public double Balance
        {
            get
            {
                lock (this)
                {
                    return _balance;
                }
            }
        }
    }
}
