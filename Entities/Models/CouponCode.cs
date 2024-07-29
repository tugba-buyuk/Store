using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CouponCode
    {
        public int CouponCodeId { get; set; }
        public string CouponCodeName { get; set; } = string.Empty;
        public int CouponCodeDiscount { get; set; }
        public bool IsActive { get; set; }
        public string StripeCouponId { get; set; }
    }
}
