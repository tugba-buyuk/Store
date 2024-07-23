using Entities.Dtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICouponCodeService
    {
        IEnumerable<CouponCode> GetAllCouponCodes(bool trackChanges);
        CouponCode? GetOneCouponCode(int id, bool trackChanges);
        void CreateCouponCode(CouponCodeDtoForCreate couponCodeDto);
        CouponCodeDtoForUpdate GetOneCouponCodeForUpdate(int id, bool trackChanges);
        void UpdateOneCouponCode(CouponCodeDtoForUpdate couponCodeDto);
        void DeleteOneCouponCode(int id);
    }
}
