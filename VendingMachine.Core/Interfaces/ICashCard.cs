
namespace VendingMachine.Interfaces
{
    public interface ICashCard
    {
        void WithdrawFromAccount(double amount);

        int PinCode { get; }
        IBankAccount BankAccount { get; }
    }
}
