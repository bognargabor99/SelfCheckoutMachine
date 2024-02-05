using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.Model.DTO;
using SelfCheckoutMachine.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelfCheckoutMachine.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CheckoutController(ICheckoutService _checkoutService) : ControllerBase
    {
        private readonly ICheckoutService checkoutService = _checkoutService;

        [HttpPost]
        public IActionResult Checkout([FromBody] CheckoutDTO checkoutDetails)
        {
            try
            {
                return this.Ok(this.checkoutService.Checkout(checkoutDetails.Inserted, checkoutDetails.Price));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
