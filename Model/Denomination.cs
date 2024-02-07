using System.ComponentModel.DataAnnotations;

namespace SelfCheckoutMachine.Model
{
    public class Denomination
    {
        [Key]
        public int Id { get; set; }
        
        public required string Value { get; set; }

        public uint Amount { get; set; }
    }
}
