using System;

namespace md.Data.Entites
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ConsumerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime DatedCreated { get; set; }
    }
}
