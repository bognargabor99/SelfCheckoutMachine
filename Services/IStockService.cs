namespace SelfCheckoutMachine.Services
{
    public interface IStockService
    {
        // Responsible for storing the inserted bills and coins in the machine
        IDictionary<string, uint> Store(IDictionary<string, uint> inserted);

        // Returns available bills and coins stored in the machine
        IDictionary<string, uint> List();
    }
}
