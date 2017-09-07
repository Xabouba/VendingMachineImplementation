using System;
using VendingMachine.Interfaces;

namespace VendingMachine.Implementations
{
    public class VendingMachineObj : IVendingMachine
    {
        private const double CAN_PRICE = 0.50;
        private const int INIT_CANS_INVENTORY = 25;
        private int _inventory;

        public VendingMachineObj()
        {
            _inventory = INIT_CANS_INVENTORY;
        }

        public void BuyCan(ICashCard cashCard, int pin)
        {
            if(pin == cashCard.PinCode)
            {
                lock (this)
                {
                    if (_inventory > 0)
                    {
                        cashCard.WithdrawFromAccount(CAN_PRICE);
                        _inventory--;
                    }
                }

                Console.WriteLine("Correct pin");
            }
            else
            {
                Console.WriteLine("Wrong pin");
            }
            Console.WriteLine(string.Format("Cans remaining: {0}", CansInventory));
        }

        public int CansInventory
        {
            get
            {
                lock (this)
                {
                    return _inventory;
                }
            }
        }
    }
}