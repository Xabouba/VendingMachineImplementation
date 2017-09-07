
namespace VendingMachine.Interfaces
{
    public interface IVendingMachine
    {
        void BuyCan(ICashCard cashCard, int pinCode);

        int CansInventory { get; }
    }
}
