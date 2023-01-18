using AutoMapper;
using Domain.Models;

namespace Database.Profiles;

public class SeasonProfiles : Profile
{
    public SeasonProfiles()
    {
        this.CreateMap<Season, Entities.Season>();
        this.CreateMap<Entities.Season, Season>();
    }
}