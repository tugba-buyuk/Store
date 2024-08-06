using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        ICategoryService CategoryService { get; }
        IOrderService OrderService { get; }
        IAuthService AuthService { get; }
        IColorService ColorService { get; }
        ICouponCodeService CouponCodeService { get; }
        ICountryService CountryService { get; }
        ICityService CityService { get; }
        IEmailService EmailService { get; }
        ISMSService SMSService { get; }
        ICommentService CommentService { get; }
    }
}
