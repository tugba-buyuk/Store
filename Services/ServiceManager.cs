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
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IEmailService _emailService;
        private readonly ISMSService _smsService;

        public ServiceManager(IProductService productService, ICategoryService categoryService, IOrderService orderService,
            IAuthService authService, IColorService colorService, ICouponCodeService couponCodeService, ICountryService countryService,
            ICityService cityService, IEmailService emailService, ISMSService smsService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _authService = authService;
            _colorService = colorService;
            _couponCodeService = couponCodeService;
            _countryService = countryService;
            _cityService = cityService;
            _emailService = emailService;
            _smsService = smsService;
        }

        public IProductService ProductService => _productService;

        public ICategoryService CategoryService => _categoryService;

        public IOrderService OrderService => _orderService;

        public IAuthService AuthService => _authService;
        public IColorService ColorService => _colorService;
        public ICouponCodeService CouponCodeService => _couponCodeService;
        public ICountryService CountryService => _countryService;
        public ICityService CityService => _cityService;    
        public IEmailService EmailService => _emailService;
        public ISMSService SMSService => _smsService;
    }
}
