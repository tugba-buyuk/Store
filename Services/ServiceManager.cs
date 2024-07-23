using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        private readonly IColorService _colorService;
        private readonly ICouponCodeService _couponCodeService;

        public ServiceManager(IProductService productService, ICategoryService categoryService, IOrderService orderService,IAuthService authService, IColorService colorService, ICouponCodeService couponCodeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _authService = authService;
            _colorService = colorService;
            _couponCodeService = couponCodeService;
        }

        public IProductService ProductService => _productService;

        public ICategoryService CategoryService => _categoryService;

        public IOrderService OrderService => _orderService;

        public IAuthService AuthService => _authService;
        public IColorService ColorService => _colorService;
        public ICouponCodeService CouponCodeService => _couponCodeService;

    }
}
