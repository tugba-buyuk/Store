using Entities.Models;

namespace StoreApp.Models
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
