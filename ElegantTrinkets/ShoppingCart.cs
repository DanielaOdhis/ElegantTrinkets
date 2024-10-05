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
            await Task.Run(() =>
            {
                var item = items.FirstOrDefault(i => i.product.Id == product.Id);
                if (item.product != null)
                {
                    // Update the quantity if the item is already in the cart
                    var updatedItem = (item.product, item.quantityAdded + quantityToAdd);
                    items[items.IndexOf(item)] = updatedItem;
                }
                else
                {
                    // Add the item with the initial quantity
                    items.Add((product, quantityToAdd));
                }
            });
        }

        public async Task RemoveProductAsync(T product, int quantityToRemove)
        {
            await Task.Run(() =>
            {
                var item = items.FirstOrDefault(i => i.product.Id == product.Id);
                if (item.product != null)
                {
                    if (item.quantityAdded > quantityToRemove)
                    {
                        // Decrease the quantity
                        var updatedItem = (item.product, item.quantityAdded - quantityToRemove);
                        items[items.IndexOf(item)] = updatedItem;
                    }
                    else
                    {
                        // Remove the item if the quantity becomes zero or less
                        items.Remove(item);
                    }
                }
            });
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
