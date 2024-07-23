using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICouponCodeRepository :   IRepositoryBase<CouponCode>
    {
        void CreateOneCouponCode(CouponCode couponCode);
        void UpdateOneCouponCode(CouponCode couponCode);
        void DeleteOneCoupon(CouponCode couponCode);
        void ChangeActivity(int id);
        public CouponCode? FindByCouponName(string couponCode, bool trackChanges);
        public CouponCode? GetOneCouponCode(int id, bool trackChanges);

    }
}
