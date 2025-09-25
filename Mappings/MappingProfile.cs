using AutoMapper;
using Florin_Back.Models.DTOs.Auth;
using Florin_Back.Models.DTOs.Category;
using Florin_Back.Models.DTOs.Transaction;
using Florin_Back.Models.DTOs.User;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;

namespace Florin_Back.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDTO, User>();
        CreateMap<LoginDTO, User>();

        CreateMap<User, UserDTO>();

        CreateMap<Category, CategoryDTO>();
        CreateMap<Pagination<Category>, Pagination<CategoryDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<CreateCategoryDTO, Category>();
        CreateMap<UpdateCategoryDTO, Category>();

        CreateMap<Transaction, TransactionDTO>();
        CreateMap<Pagination<Transaction>, Pagination<TransactionDTO>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<CreateTransactionDTO, Transaction>();
        CreateMap<UpdateTransactionDTO, Transaction>();
    }
}
