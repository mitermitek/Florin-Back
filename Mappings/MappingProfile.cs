using AutoMapper;
using Florin_Back.DTOs.Auth;
using Florin_Back.DTOs.Category;
using Florin_Back.DTOs.User;
using Florin_Back.DTOs.UserTransaction;
using Florin_Back.DTOs.Utility;
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
        CreateMap<Pagination<Category>, PaginationDTO<UserCategoryDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<CreateUserCategoryDTO, Category>();
        CreateMap<UpdateUserCategoryDTO, Category>();

        CreateMap<Transaction, UserTransactionDTO>();
        CreateMap<Pagination<Transaction>, PaginationDTO<UserTransactionDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<CreateUserTransactionDTO, Transaction>();
        CreateMap<UpdateUserTransactionDTO, Transaction>();
    }
}
