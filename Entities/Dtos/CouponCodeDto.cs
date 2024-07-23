using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record CouponCodeDto
    {
        public int CouponCodeId { get; init; }
        public string CouponCodeName { get; init; } = string.Empty;
        public int CouponCodeDiscount { get; init; }
        public bool IsActive { get; init; }
    }
}
