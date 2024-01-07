#region

using AutoMapper;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Handlers;

#endregion

namespace MyMovies.MappingProfiles;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, AddMovieCommand>();
        CreateMap<AddMovieCommand, Movie>();

        CreateMap<Movie, MovieDto>();
        CreateMap<MovieDto, Movie>();
    }
}