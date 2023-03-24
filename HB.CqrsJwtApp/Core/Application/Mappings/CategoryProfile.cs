using AutoMapper;
using HB.CqrsJwtApp.Core.Application.Dto;
using HB.CqrsJwtApp.Core.Domain;

namespace HB.CqrsJwtApp.Core.Application.Mappings
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
        }
    }
}
