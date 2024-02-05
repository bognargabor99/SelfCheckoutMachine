using System.Text.Json.Serialization;

namespace SelfCheckoutMachine.Model.DTO
{
    public class CheckoutDTO
    {
        [JsonPropertyName("inserted")]
        public IDictionary<string, uint> Inserted { get; set; } = null!;

        [JsonPropertyName("price")]
        public uint Price { get; set; }
    }
}
