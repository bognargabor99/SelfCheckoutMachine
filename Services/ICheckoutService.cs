namespace SelfCheckoutMachine.Services
{
    public interface ICheckoutService
    {
        /// <summary>
        /// Handles checkouts by storing the given money, calculating and returning the change to be provided to the customer.<br />
        /// The transaction fails if one of the following statements is true:<br />
        /// 1. At least one of the inserted denominations is invalid<br />
        /// 2. Not enough money is inserted by the customer<br />
        /// 3. Exact amount of change cannot be provided<br />
        /// </summary>
        /// <param name="inserted">The inserted money by the user</param>
        /// <param name="price">The price of the purchase</param>
        /// <returns>The calculated change</returns>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, uint> Checkout(IDictionary<string, uint> inserted, uint price);
    }
}
