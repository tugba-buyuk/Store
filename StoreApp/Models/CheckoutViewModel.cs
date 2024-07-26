using Entities.Models;

namespace StoreApp.Models
{
    public class CheckoutViewModel
    {
        public Cart Cart { get; set; }
        public IEnumerable<Country> Countries { get; set; } = Enumerable.Empty<Country>();
        public IEnumerable<City> Cities { get; set; } = Enumerable.Empty<City>();
        
    }
}
