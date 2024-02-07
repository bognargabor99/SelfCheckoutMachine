using System.ComponentModel.DataAnnotations;

namespace SelfCheckoutMachine.Model
{
    public class Denomination
    {
        [Key]
        public required string Value { get; set; }

        public uint Amount { get; set; }
    }
}
