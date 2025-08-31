using AutoMapper;
using Florin_Back.DTOs.Auth;
using Florin_Back.DTOs.Category;
using Florin_Back.DTOs.User;
using Florin_Back.Models;

namespace Florin_Back.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDTO, User>();
        CreateMap<LoginDTO, User>();

        CreateMap<User, UserDTO>();

        CreateMap<Category, UserCategoryDTO>();
        CreateMap<CreateUserCategoryDTO, Category>();
        CreateMap<UpdateUserCategoryDTO, Category>();
    }
}
