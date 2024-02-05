using System.Text.Json.Serialization;

namespace SelfCheckoutMachine.Model.DTO
{
    /// <summary>
    /// Represents the object that is placed in the body of HTTP POST message when the user tries to checkout.
    /// </summary>
    public class CheckoutDTO
    {
        [JsonPropertyName("inserted")]
        public IDictionary<string, uint> Inserted { get; set; } = null!;

        [JsonPropertyName("price")]
        public uint Price { get; set; }
    }
}
