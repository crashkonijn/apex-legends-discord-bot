using AutoMapper;

namespace Database.Profiles;

public class StatsProfiles : Profile
{
    public StatsProfiles()
    {
        this.CreateMap<Domain.Models.Stat, Entities.Stat>()
            .ForMember(x => x.Id, options => options.Ignore());
        
        this.CreateMap<Entities.Stat, Domain.Models.Stat>()
            .ForMember(x => x.Username, options => options.MapFrom(y => y.Player.Username));
    }
}