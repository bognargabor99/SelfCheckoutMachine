using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelfCheckoutMachine.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockController(IStockService _currencyService) : ControllerBase
    {
        private readonly IStockService currencyService = _currencyService;

        /// <summary>
        /// Returns stored bills and coins.
        /// </summary>
        /// <returns>The stored bills and coins with the amount of each denomination</returns>
        [HttpGet]
        public IActionResult ListStock()
        {
            Console.WriteLine("Listing available denominations with amount of bills/coins of them...");
            return this.Ok(this.currencyService.List());
        }

        /// <summary>
        /// Loads the given amount of bills and coins into the machine
        /// </summary>
        /// <param name="loaded"></param>
        [HttpPost]
        public IActionResult Load([FromBody] IDictionary<string, uint> loaded)
        {
            Console.WriteLine("Loading money into the machine...");
            try
            {
                return this.Ok(this.currencyService.Store(loaded));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Something happened while loading money into machine. See the message below.\n\t{e.Message}");
                return this.BadRequest(e.Message);
            }
        }
    }
}
