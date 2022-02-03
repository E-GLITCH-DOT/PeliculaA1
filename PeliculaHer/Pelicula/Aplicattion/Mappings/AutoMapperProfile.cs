using System.Reflection.Metadata.Ecma335;
using System;
using AutoMapper;
using Pelicula.Domain.dto.Responses;
using Pelicula.Domain.dto.Requests;
using Pelicula.Domain.dto;
using Pelicula.Domain.Entities;

namespace Pelicula.Application.Mappings
{
      public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Peliculas, PeliculasResponse>()
            .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director))
            .ForMember(dest => dest.Género, opt => opt.MapFrom(src => src.Género))
            .ForMember(dest => dest.Puntuacion, opt => opt.MapFrom(src => src.Puntuacion))
            .ForMember(dest => dest.Publicacion, opt => opt.MapFrom(src => src.Publicacion));


            CreateMap<PeliculasCreateRequest, Peliculas>()
            .ForPath(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
            .ForPath(dest => dest.Director, opt => opt.MapFrom(src => src.Director))
            .ForPath(dest => dest.Género, opt => opt.MapFrom(src => src.Género))
            .ForPath(dest => dest.Puntuacion, opt => opt.MapFrom(src => src.Puntuacion))    
            .ForPath(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForPath(dest => dest.Publicacion, opt => opt.MapFrom(src => src.Publicacion));
            
        }
    }
}