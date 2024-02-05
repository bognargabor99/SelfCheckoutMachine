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

        /// <inheritdoc />
        public IDictionary<string, uint> Checkout(IDictionary<string, uint> inserted, uint price)
        {
            // Check if inserted money is of valid denominations
            if (!CheckInsertedDenominations(inserted.Keys, out var message))
                throw new ArgumentException(message);
            
            var insertedAmount = inserted.Sum(x => uint.Parse(x.Key) * x.Value);

            // Check if price if enough
            if (!CheckEnoughMoneyInserted(insertedAmount, price, out message))
                throw new ArgumentException(message);

            foreach (var denomination in inserted)
                _currencies[denomination.Key] += denomination.Value;

            // Calculate change if it can be provided
            if (!TryProvideChange(insertedAmount, price, out message, out IDictionary<string, uint> change))
            {
                foreach (var denomination in inserted)
                    _currencies[denomination.Key] -= denomination.Value;

                throw new ArgumentException(message);
            }
            else
            {
                foreach (var denomination in change)
                    _currencies[denomination.Key] -= denomination.Value;

                return change;
            }
        }

        /// <inheritdoc />
        public IDictionary<string, uint> List()
        {
            return _currencies;
        }

        /// <inheritdoc />
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
        private static bool CheckEnoughMoneyInserted(long insertedAmount, uint price, out string message)
        {
            if (insertedAmount < price)
            {
                message = $"The amount of inserted money ({insertedAmount}) is less than the price ({price}). Operation aborted.";
                return false;
            }
            message = string.Empty;
            return true;
        }
        
        /// <summary>
        /// Checks and calculates the exact change to be given back to the customer based on:
        ///     The inserted money
        ///     The price of the purchase and
        ///     The currently holded money in the machine
        /// </summary>
        /// <param name="insertedAmount">The inserted money by the user</param>
        /// <param name="price">Price of the purchase</param>
        /// <param name="message">An error message if the price if the change cannot be provided</param>
        /// <param name="change">The exact amounts of denominations for the change to be provided</param>
        /// <returns>True if the change can be provided, false otherwise</returns>
        private bool TryProvideChange(long insertedAmount, uint price, out string? message, out IDictionary<string, uint> change)
        {
            // Calculate the amount of change
            var changeAmount = insertedAmount - price;
            change = new Dictionary<string, uint>();

            if (changeAmount > 0)
            {
                var indexOfDenomination = IndexOfMaxDenomination(changeAmount);

                while (changeAmount > 0 && indexOfDenomination >= 0)
                {
                    var denomination = uint.Parse(this.AcceptedDenominations[indexOfDenomination]);

                    // Calculate how many bills/coins can we use of the current denomination
                    var countOfDenominationInChange = Math.Min(changeAmount / denomination, this._currencies[denomination.ToString()]);

                    // Decrease the changeAmount, set used amount of bills/coins
                    changeAmount -= denomination * countOfDenominationInChange;
                    change[this.AcceptedDenominations[indexOfDenomination]] = (uint)countOfDenominationInChange;

                    if (indexOfDenomination == 0)
                        break;

                    indexOfDenomination = IndexOfMaxDenomination(changeAmount);
                }

                if (changeAmount != 0)
                {
                    message = "Exact/precise change cannot be provided.";
                    return false;
                }
            }
            message = string.Empty;
            return true;
        }

        /// <summary>
        /// Calculates the highest denomination that can be used for the change
        /// </summary>
        /// <returns>
        /// Index of the denomination that:<br />
        ///     1. Is not higher than the amount of change to be provided<br />
        ///     2. The machine has at least one (bill/coin) of</returns>
        private int IndexOfMaxDenomination(long changeAmount)
        {
            var denominationOfChange = this.AcceptedDenominations.Where(x => this._currencies[x] != 0).Select(x => uint.Parse(x)).Where(x => x <= changeAmount).Max();
            var indexOfDenomination = this.AcceptedDenominations.IndexOf(denominationOfChange.ToString());

            return indexOfDenomination;
        }
    }
}
