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
        public IDictionary<string, decimal> Checkout(IDictionary<string, decimal> inserted, decimal price)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, decimal> List()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, decimal> Store(IDictionary<string, decimal> inserted)
        {
            throw new NotImplementedException();
        }
    }
}
