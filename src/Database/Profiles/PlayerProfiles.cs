using AutoMapper;
using Domain.Models;

namespace Database.Profiles;

public class PlayerProfiles : Profile
{
    public PlayerProfiles()
    {
        this.CreateMap<Player, Entities.Player>()
            .ForMember(x => x.Stats, options => options.Ignore());
        
        this.CreateMap<Entities.Player, Player>();
    }
}