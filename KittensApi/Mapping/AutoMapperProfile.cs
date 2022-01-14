using AutoMapper;
using KittensApi.Domain;
using KittensApi.Dto.Details;

namespace KittensApi.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDetails>();
        }
    }
}
