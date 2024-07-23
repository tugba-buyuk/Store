using Entities.Models;
using StoreApp.Infrastructure.Extensions;
using System.Text.Json.Serialization;

namespace StoreApp.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        // ISession türünde bir oturum nesnesi, nullable olarak tanımlandı.
        public ISession? Session { get; set; }

        // IServiceProvider türünde bir servis sağlayıcı parametresi alan statik bir metod.
        public static Cart GetCart(IServiceProvider service)
        {
            // IHttpContextAccessor servisinden HttpContext ve oturum (Session) nesnesini alır.
            ISession? session = service.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            // Session içindeki "cart" anahtarıyla saklanan veriyi alır ve SessionCart nesnesine dönüştürür.
            // Eğer böyle bir veri yoksa yeni bir SessionCart nesnesi oluşturur.
            SessionCart cart = session.GetJson<SessionCart>("cart") ?? new SessionCart();

            // Oturum nesnesini SessionCart nesnesinin Session özelliğine atar.
            cart.Session = session;

            // Oluşturulan veya alınan cart nesnesini döner.
            return cart;
        }

        public override void AddItem(Product product, int quantity,string color,string size)
        {
            // Cart sınıfının AddItem metodunu çağırır.
            base.AddItem(product, quantity,color,size);

            // Güncellenmiş cart nesnesini oturumda "cart" anahtarıyla saklar.
            Session?.SetJson<SessionCart>("cart", this);
        }

        public override void Clear()
        {
            // Cart sınıfının Clear metodunu çağırır.
            base.Clear();

            // Oturumdaki "cart" anahtarıyla saklanan veriyi kaldırır.
            Session?.Remove("cart");
        }

        public override void RemoveLine(Product product)
        {
            // Cart sınıfının RemoveLine metodunu çağırır.
            base.RemoveLine(product);

            // Güncellenmiş cart nesnesini oturumda "cart" anahtarıyla saklar.
            Session?.SetJson<SessionCart>("cart", this);
        }
    }


}

