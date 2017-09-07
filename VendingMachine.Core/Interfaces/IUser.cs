
namespace VendingMachine.Interfaces
{
    public interface IUser
    {
        ICashCard CashCardA { get; }
        ICashCard CashCardB { get; }
    }
}
