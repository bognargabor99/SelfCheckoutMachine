namespace SelfCheckoutMachine.Services
{
    public interface ICheckoutService
    {
        // Handles checkouts and updates available denominations
        IDictionary<string, uint> Checkout(IDictionary<string, uint> inserted, uint price);
    }
}
