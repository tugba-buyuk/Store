using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CouponCodeRepository : RepositoryBase<CouponCode>, ICouponCodeRepository
    {
        private readonly RepositoryContext _context;
        public CouponCodeRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeActivity(int id)
        {
            var couponCode = GetOneCouponCode(id, true);
            if (couponCode != null)
            {
                couponCode.IsActive = !couponCode.IsActive;
                _context.SaveChanges();
            }

        }

        public void CreateOneCouponCode(CouponCode couponCode) => Create(couponCode);
        

        public void DeleteOneCoupon(CouponCode couponCode) => Remove(couponCode);

        public CouponCode? FindByCouponName(string couponCode, bool trackChanges)
        {
            return FindByCondition(p => p.CouponCodeName.Equals(couponCode), trackChanges);
        }

        public CouponCode? GetOneCouponCode(int id, bool trackChanges)
        {
            return FindByCondition(p => p.CouponCodeId.Equals(id), trackChanges);
        }

        public void UpdateOneCouponCode(CouponCode couponCode) => Update(couponCode);
        
    }


}
