namespace SelfCheckoutMachine.Services
{
    public interface ICurrencyService
    {
        // Responsible for storing the inserted bills and coins in the machine
        IDictionary<string, uint> Store(IDictionary<string, uint> inserted);

        // Returns available bills and coins stored in the machine
        IDictionary<string, uint> List();

        // Handles checkouts, updates available denominations
        IDictionary<string, uint> Checkout(IDictionary<string, uint> inserted, uint price);
    }
}
