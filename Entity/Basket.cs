using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            if (Items.All(Items => Items.Product.Id != product.Id))
            {
                Items.Add(new BasketItem { Product = product, Quantity = quantity });
                return;
            }
            var existingBasketItems = Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingBasketItems != null)
            {
                existingBasketItems.Quantity += quantity;
            }
        }
        public void RemoveItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(Items => Items.Product.Id == productId);
            if (item == null)
                return;

            item.Quantity -= quantity;
            if (item.Quantity == 0)
            {
                Items.Remove(item);
            }
        }
    }
}