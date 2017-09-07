using VendingMachine.Interfaces;

namespace VendingMachine.Implementations
{
    public class User : IUser
    {
        public User(ICashCard cashCardA, ICashCard cashCardB)
        {
            CashCardA = cashCardA;
            CashCardB = cashCardB;
        }

        public ICashCard CashCardA { get; private set; }
        public ICashCard CashCardB { get; private set; }
    }
}
