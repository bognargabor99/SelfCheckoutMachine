namespace SelfCheckoutMachine.Services
{
    public interface IStockService
    {
        /// <summary>
        /// Stores the given amount of money in the machine
        /// </summary>
        /// <param name="inserted">Tha amount of money to be stored</param>
        /// <returns>The new state of the machine</returns>
        /// <exception cref="ArgumentException"></exception>
        IDictionary<string, uint> Store(IDictionary<string, uint> inserted);

        /// <summary>
        /// Returns the state of the machine
        /// </summary>
        /// <returns></returns>
        IDictionary<string, uint> List();
    }
}
