using System;
namespace MarketSatis
{
	public class Product
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public int? BuyerId { get; set; }
        public Buyer Buyer { get; set; }
    }
}

