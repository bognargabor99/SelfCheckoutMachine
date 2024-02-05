namespace SelfCheckoutMachine.Services
{
    public interface ICurrencyService
    {
        // Responsible for storing the inserted bills and coins in the machine
        IDictionary<string, decimal> Store(IDictionary<string, decimal> inserted);

        // Returns available bills and coins stored in the machine
        IDictionary<string, decimal> List();

        // Handles checkouts, updates available denominations
        IDictionary<string, decimal> Checkout(IDictionary<string, decimal> inserted, decimal price);
    }
}
