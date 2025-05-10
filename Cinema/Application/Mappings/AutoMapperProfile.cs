using AutoMapper;
using Cinema.Entities;
using Cinema.Application.DTOs;

namespace Cinema.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<Sala, SalaDto>().ReverseMap();
            CreateMap<Butaca, ButacaDto>().ReverseMap();
            CreateMap<Cartelera, CarteleraDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();
        }
    }
}
