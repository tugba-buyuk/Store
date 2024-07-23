using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CouponCodeManager : ICouponCodeService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CouponCodeManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        
        public void CreateCouponCode(CouponCodeDtoForCreate couponCodeDto)
        {
            CouponCode couponCode= _mapper.Map<CouponCode>(couponCodeDto);
            _manager.CouponCode.CreateOneCouponCode(couponCode);
            _manager.Save();
        }

        public void DeleteOneCouponCode(int id)
        {
            var couponCode = _manager.CouponCode.GetOneCouponCode(id, false);
            if(couponCode is null)
            {
                throw new Exception("The coupon code is not found.");
            }
            _manager.CouponCode.DeleteOneCoupon(couponCode);
            _manager.Save();
        }

        public IEnumerable<CouponCode> GetAllCouponCodes(bool trackChanges)
        {
            return _manager.CouponCode.FindAll(trackChanges);
        }

        public CouponCode? GetOneCouponCode(int id, bool trackChanges)
        {
            var couponcode =  _manager.CouponCode.GetOneCouponCode(id, trackChanges);
            if(couponcode is null)
            {
                throw new Exception("The coupon code is njot found");
            }
            return couponcode;
        }

        public CouponCodeDtoForUpdate GetOneCouponCodeForUpdate(int id, bool trackChanges)
        {
            var couponCode=_manager.CouponCode.GetOneCouponCode(id,trackChanges);
            var couponCodeDto=_mapper.Map<CouponCodeDtoForUpdate>(couponCode);
            return couponCodeDto;
        }

        public void UpdateOneCouponCode(CouponCodeDtoForUpdate couponCodeDto)
        {
            var couponCode=_mapper.Map<CouponCode>(couponCodeDto);
            _manager.CouponCode.UpdateOneCouponCode(couponCode);
            _manager.Save();
        }
    }
}
