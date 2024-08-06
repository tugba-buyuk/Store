using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertion,Product>();
            CreateMap<ProductDtoForUpdate,Product>().ReverseMap();
            CreateMap<CategoryDtoForCreate, Category>();
            CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
            CreateMap<ColorDtoForCreate, Color>();
            CreateMap<ColorDtoForUpdate, Color>().ReverseMap();
            CreateMap<UserDtoForCreation,IdentityUser>();
            CreateMap<UserDtoUpdate,IdentityUser>().ReverseMap();
            CreateMap<CouponCodeDtoForCreate , CouponCode>();
            CreateMap<CouponCodeDtoForUpdate , CouponCode>().ReverseMap();
            CreateMap<CommentDtoForCreate , Comment>();
            CreateMap<CommentDtoForUpdate , Comment>().ReverseMap();
        }
    }
}
