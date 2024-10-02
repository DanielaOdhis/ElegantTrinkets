using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElegantTrinkets
{
    internal class ShoppingCart<T> where T : Products<string>
    {
        private List<(T product, int quantityAdded)> items = new List<(T product, int quantityAdded)>();

        public async Task AddProductAsync(T product, int quantityToAdd)
        {
            await Task.Run(() => {
                var item = items.FirstOrDefault(i => i.product.Id == product.Id);
                if (item.product != null)
                {
                    // Update the quantity if the item is already in the cart
                    item.quantityAdded += quantityToAdd;
                }
                else
                {
                    // Add the item with the initial quantity
                    items.Add((product, quantityToAdd));
                }
            });
        }

        public async Task RemoveProductAsync(T product)
        {
            await Task.Run(() => items.RemoveAll(i => i.product.Id == product.Id));
        }

        public List<(T product, int quantityAdded)> GetItems()
        {
            return items;
        }

        public double GetPrice()
        {
            return items.Sum(i => i.product.Price * i.quantityAdded);
        }
    }
}
