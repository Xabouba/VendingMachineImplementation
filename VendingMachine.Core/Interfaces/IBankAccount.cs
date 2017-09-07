
namespace VendingMachine.Interfaces
{
    public interface IBankAccount
    {
        void Withdraw(double amount);
        void Deposit(double amount);

        double Balance { get; }
    }
}
