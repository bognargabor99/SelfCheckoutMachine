using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelfCheckoutMachine.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockController(ICurrencyService _currencyService) : ControllerBase
    {
        private ICurrencyService currencyService { get; set; } = _currencyService;

        /// <summary>
        /// Returns stored bills and coins.
        /// </summary>
        /// <returns>The stored bills and coins with the amount of each denomination</returns>
        [HttpGet]
        public IActionResult ListStock()
        {
            return this.Ok(this.currencyService.List());
        }

        /// <summary>
        /// Loads the given amount of bills and coins into the machine
        /// </summary>
        /// <param name="loaded"></param>
        [HttpPost]
        public IActionResult Load([FromBody] IDictionary<string, uint> loaded)
        {
            try
            {
                return this.Ok(this.currencyService.Store(loaded));
            }
            catch (ArgumentException e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
