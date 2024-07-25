using Entities.Models;
using StoreApp.Infrastructure.Extensions;
using System.Text.Json.Serialization;

namespace StoreApp.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession? Session { get; set; }

        public static Cart GetCart(IServiceProvider service)
        {
            ISession? session = service.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            SessionCart cart = session.GetJson<SessionCart>("cart") ?? new SessionCart();

            cart.Session = session;

            return cart;
        }

        public override void AddItem(Product product, int quantity, string color, string size)
        {
            base.AddItem(product, quantity, color, size);
            Session?.SetJson<SessionCart>("cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("cart");
        }

        public void ApplyDiscount(decimal discount)
        {
            base.ApplyDiscount(discount);
            Session?.SetJson<SessionCart>("cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session?.SetJson<SessionCart>("cart", this);
        }
    }
}

