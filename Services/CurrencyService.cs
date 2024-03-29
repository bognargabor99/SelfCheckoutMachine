﻿using SelfCheckoutMachine.Model;

namespace SelfCheckoutMachine.Services
{
    /// <summary>
    /// Responsible for:
    ///     Storing bills and coins
    ///     Handling checkouts
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly List<string> AcceptedDenominations = [];

        private CurrencyContext CurrencyContext { get; set; }

        public CurrencyService(CurrencyContext context)
        {
            this.CurrencyContext = context;
            this.AcceptedDenominations = this.CurrencyContext.Denominations.Select(x => x.Value).ToList();
        }

        /// <inheritdoc />
        public IDictionary<string, uint> Checkout(IDictionary<string, uint> inserted, uint price)
        {
            // Check if inserted money is of valid denominations
            if (!CheckInsertedDenominations(inserted.Keys, out var message))
            {
                Console.WriteLine(message);
                throw new ArgumentException(message);
            }

            var insertedAmount = inserted.Sum(x => uint.Parse(x.Key) * x.Value);

            // Check if price if enough
            if (!CheckEnoughMoneyInserted(insertedAmount, price, out message))
            {
                Console.WriteLine(message);
                throw new ArgumentException(message);
            }
            Console.WriteLine("The customer provided valid denominations and enough money to pay for the purchase.");

            foreach (var denomination in inserted)
                this.CurrencyContext.Denominations.Single(x => x.Value == denomination.Key).Amount += denomination.Value;
            
            this.CurrencyContext.SaveChanges();

            // Calculate change if it can be provided
            Console.WriteLine("The change is being calculated...");
            if (!TryProvideChange(insertedAmount, price, out message, out IDictionary<string, uint> change))
            {
                Console.WriteLine("Something happened while calculating change.");
                Console.WriteLine($"See the error message: {message}");

                foreach (var denomination in inserted)
                    this.CurrencyContext.Denominations.Single(x => x.Value == denomination.Key).Amount -= denomination.Value;

                this.CurrencyContext.SaveChanges();

                throw new ArgumentException(message);
            }
            else
            {
                Console.WriteLine("The exact change can be provided.");
                foreach (var denomination in change)
                    this.CurrencyContext.Denominations.Single(x => x.Value == denomination.Key).Amount -= denomination.Value;

                this.CurrencyContext.SaveChanges();

                Console.WriteLine("The inserted money is stored in the machine and the change is given back.");
                Console.WriteLine($"The change is ({string.Join(", ", change.Select(x => $"\"{x.Key}\": {x.Value}"))})");

                return change;
            }
        }

        /// <inheritdoc />
        public IDictionary<string, uint> List()
        {
            var dict = new Dictionary<string, uint>();
            foreach (var denomination in this.CurrencyContext.Denominations)
                dict.Add(denomination.Value, denomination.Amount);

            return dict;
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
                this.CurrencyContext.Denominations.Single(x => x.Value == denomination.Key).Amount += denomination.Value;

            this.CurrencyContext.SaveChanges();

            Console.WriteLine("The denominations to be stored are valid and saved.");

            return this.List();
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
                var maxDenominationInChange = 20000;
                TryFindIndexOfMaxDenomination(changeAmount, maxDenominationInChange, out var indexOfDenomination);

                while (changeAmount > 0 && indexOfDenomination >= 0)
                {
                    var denomination = uint.Parse(this.AcceptedDenominations[indexOfDenomination]);

                    // Calculate how many bills/coins can we use of the current denomination
                    var countOfDenominationInChange = Math.Min(changeAmount / denomination, this.CurrencyContext.Denominations.Single(x => x.Value == denomination.ToString()).Amount);
                    
                    // Decrease the changeAmount, set used amount of bills/coins
                    changeAmount -= denomination * countOfDenominationInChange;
                    change[this.AcceptedDenominations[indexOfDenomination]] = (uint)countOfDenominationInChange;

                    // If the denomination cannot lowered, break the loop
                    if (indexOfDenomination == 0 || changeAmount == 0)
                        break;

                    // We cannot use the current denomination again
                    // Therefore, we change the maximum denomination to a smaller one
                    // For example, we used 1000 -> in the next cycle we can only use 500 or lower
                    maxDenominationInChange = int.Parse(this.AcceptedDenominations[this.AcceptedDenominations.IndexOf(denomination.ToString()) - 1]);

                    while (!TryFindIndexOfMaxDenomination(changeAmount, maxDenominationInChange, out indexOfDenomination) && maxDenominationInChange > 5)
                    {
                        // If the change cannot be provided using the largest kinds of bills
                        // then I try to use smaller and smaller kinds of bills/coins

                        if (!change.Any())
                        {
                            maxDenominationInChange = 5;
                            continue;
                        }

                        var indexOfNewDenomination = this.AcceptedDenominations.IndexOf(change.Keys.Select(x => int.Parse(x)).Max().ToString()) - 1;
                        maxDenominationInChange = int.Parse(this.AcceptedDenominations[indexOfNewDenomination]);
                        
                        changeAmount += change.Select(x => int.Parse(x.Key) * x.Value).Sum();
                        change.Clear();
                    }
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
        private bool TryFindIndexOfMaxDenomination(long changeAmount, int maxDenomination, out int indexOfDenomination)
        {
            var denominationsOfChange = this.AcceptedDenominations
                .Where(x => this.CurrencyContext.Denominations.Single(d => d.Value == x).Amount != 0)
                .Select(x => uint.Parse(x))
                .Where(x => x <= changeAmount && x <= maxDenomination);

            if (!denominationsOfChange.Any())
            {
                indexOfDenomination = -1;
                return false;
            }
            
            indexOfDenomination = this.AcceptedDenominations.IndexOf(denominationsOfChange.Max().ToString());

            return true;
        }
    }
}
