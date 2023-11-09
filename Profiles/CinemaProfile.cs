using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCineDto,Cinema>();
            CreateMap<Cinema, ReadCineDto>().ForMember(cinedto => cinedto.ReadAddressDto, opt => opt.MapFrom(cinema => cinema.Address));
            CreateMap<UpdateCineDto, Cinema>();

        }
    }
}
