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

        private readonly ConcurrentDictionary<string, uint> _currencies = new();

        public CurrencyService()
        {
            // Initializing stored amounts to zero (no money is stored in the beginning)
            foreach (var currency in AcceptedDenominations)
                _currencies[currency] = 0;
        }

        public IDictionary<string, uint> Checkout(IDictionary<string, uint> inserted, uint price)
        {
            // Check if inserted money is of valid denominations
            if (!CheckInsertedDenominations(inserted.Keys, out var message))
                throw new ArgumentException(message);
            
            // Check if price if enough
            if (!CheckEnoughMoneyInserted(inserted, price, out message))
                throw new ArgumentException(message);
            
            return null;
        }

        public IDictionary<string, uint> List()
        {
            return _currencies;
        }

        public IDictionary<string, uint> Store(IDictionary<string, uint> inserted)
        {
            // Check if inserted money is of valid denominations
            if (!CheckInsertedDenominations(inserted.Keys, out var message))
            {
                throw new ArgumentException(message);
            }
            
            foreach (var denomination in inserted)
                _currencies[denomination.Key] += denomination.Value;

            return this._currencies;
        }

        /// <summary>
        /// Checks if inserted money is of accepted denominations
        /// </summary>
        /// <param name="denominations">Inserted types of denominations</param>
        /// <param name="message">An error message if an inserted denomination is not accepted</param>
        /// <returns>True if all inserted denominations are accepted, false otherwise</returns>
        private bool CheckInsertedDenominations(ICollection<string> denominations, out string message)
        {
            foreach (var denom in denominations)
            {
                if (!this.AcceptedDenominations.Contains(denom))
                {
                    message = $"Unrecognized denomination ({denom}) was inserted. Operation aborted.\n" +
                              $"See accepted denominations: {string.Join(", ", this.AcceptedDenominations)}";
                    return false;
                }
            }
            message = string.Empty;
            return true;
        }

        /// <summary>
        /// Checks if the inserted money is enough for the purchase
        /// </summary>
        /// <param name="inserted">The inserted bills/coins by denominations and the amount of each denomination</param>
        /// <param name="price">The price of the purchase</param>
        /// <param name="message">An error message if the provided money is not enough</param>
        /// <returns>True if the user inserted enough money, false otherwise</returns>
        private static bool CheckEnoughMoneyInserted(IDictionary<string, uint> inserted, uint price, out string message)
        {
            var insertedAmount = inserted.Sum(x => uint.Parse(x.Key) * x.Value);

            if (insertedAmount < price)
            {
                message = $"The amount of inserted money ({insertedAmount}) is less than the price ({price}). Operation aborted.";
                return false;
            }
            message = string.Empty;
            return true;
        }
    }
}
