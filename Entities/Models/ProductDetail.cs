using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string Size { get; set; } // S, M, L, XL, XXL
        public string Gender { get; set; } // Women, Men, Unisex
        public string Color { get; set; } // Admin panelinden seçilecek renkler
    }
}
