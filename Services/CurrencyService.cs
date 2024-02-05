using System.Collections.Concurrent;

namespace SelfCheckoutMachine.Services
{
    /// <summary>
    /// Responsible for:
    ///     Storing bills and coins
    ///     Handling checkouts
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly List<string> AcceptedDenominations = ["5", "10", "20", "50", "100", "200", "500", "1000", "2000", "5000", "10000", "20000"];

        private readonly ConcurrentDictionary<string, decimal> _currencies = new();

        public CurrencyService()
        {
            // Initializing stored amounts to zero (no money is stored in the beginning)
            foreach (var currency in AcceptedDenominations)
                _currencies[currency] = 0;
        }

        public IDictionary<string, decimal> Checkout(IDictionary<string, decimal> inserted, decimal price)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, decimal> List()
        {
            return _currencies;
        }

        public IDictionary<string, decimal> Store(IDictionary<string, decimal> inserted)
        {
            throw new NotImplementedException();
        }
    }
}
