using System;
namespace MarketSatis
{
	public class Buyer
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> PurchasedProducts { get; set; } = new List<Product>();
    }
}

