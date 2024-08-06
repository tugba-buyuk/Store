﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IOrderRepository Order { get; }
        IColorRepository Color { get; }
        ICouponCodeRepository CouponCode { get; }
        ICountryRepository Country { get; }
        ICityRepository City { get; }
        ICommentRepository Comment { get; }
        void Save();
    }
}
