using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedTotalPrice { get; set; } = 0;// New property

        public Cart()
        {
            Lines = new List<CartLine>();
            UpdateDiscountedTotalPrice(); // Initialize DiscountedTotalPrice
        }

        public virtual void AddItem(Product product, int quantity, string color, string size)
        {
            CartLine? line = Lines.FirstOrDefault(l =>
                l.Product.Id == product.Id &&
                l.Color == color &&
                l.Size == size);

            if (line is null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    Color = color,
                    Size = size
                });
            }
            else
            {
                line.Quantity += quantity;
            }

            UpdateDiscountedTotalPrice(); // Update the discounted total price after adding an item
        }

        public virtual void RemoveLine(Product product)
        {
            Lines.RemoveAll(l => l.Product.Id.Equals(product.Id));
            UpdateDiscountedTotalPrice(); // Update the discounted total price after removing an item
        }

        public decimal ComputeTotalValue() =>
            Lines.Sum(e => e.Product.Price * e.Quantity) - Discount;

        public decimal ComputeTotalWithoutDescount() => Lines.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear()
        {
            Lines.Clear();
            UpdateDiscountedTotalPrice(); // Update the discounted total price after clearing
        }

        public void ApplyDiscount(decimal discount)
        {
            Discount = discount;
            UpdateDiscountedTotalPrice(); // Update the discounted total price after applying discount
        }

        private void UpdateDiscountedTotalPrice()
        {
            DiscountedTotalPrice = ComputeTotalValue(); // Calculate the discounted total price
        }
    }
}
