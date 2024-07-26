using Entities.Models;

namespace StoreApp.Models
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; } = new Order();
        public Cart Cart { get; set; }
        public IEnumerable<Country> Countries { get; set; } = Enumerable.Empty<Country>();
        public IEnumerable<City> Cities { get; set; } = Enumerable.Empty<City>();
        
    }
}
