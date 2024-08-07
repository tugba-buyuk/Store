﻿using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ICouponCodeRepository _couponCodeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICommentRepository _commentRepository;


        public RepositoryManager(IProductRepository productRepository, RepositoryContext context, ICategoryRepository categoryRepository,
            IOrderRepository orderRepository, IColorRepository colorRepository, ICouponCodeRepository couponCodeRepository,ICountryRepository countryRepository,
            ICityRepository cityRepository, ICommentRepository commentRepository )
        {
            _productRepository = productRepository;
            _context = context;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _colorRepository = colorRepository;
            _couponCodeRepository = couponCodeRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _commentRepository = commentRepository;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public IOrderRepository Order => _orderRepository;
        public IColorRepository Color => _colorRepository;
        public ICouponCodeRepository CouponCode => _couponCodeRepository;

        public ICountryRepository Country => _countryRepository;
        public ICityRepository City => _cityRepository;

        public ICommentRepository Comment => _commentRepository;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
