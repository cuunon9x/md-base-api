using System;

namespace md.Data.Entites
{
    public class ProductInShop
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }
    }
}
