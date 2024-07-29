using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public ICollection<CartLine> Lines { get; set; }=new List<CartLine>();
        [Required(ErrorMessage="Full Name is required")]
        public String? FullName {  get; set; }
        public String? City {  get; set; }
        public String? Country { get; set; }
        [Required(ErrorMessage ="Full Address is requuired")]
        public String FullAddress { get; set; }=String.Empty;
        public bool GiftWrap { get; set; }
        public bool Shipped { get; set; }
        public String OrderStatus { get; set; } = String.Empty;
        public DateTime OrderAt { get; set; }=DateTime.Now;
        [Required(ErrorMessage ="Email is required")]
        public String? Email { get; set; }
        [Required(ErrorMessage ="Phone Number is required")]
        public String? PhoneNumber { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal DiscountAmount { get; set; }
        public string CouponCode { get; set; }=string.Empty;

    }
}
