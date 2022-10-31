using AutoMapper;
using Domain.Models;

namespace Database.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        this.CreateMap<User, Entities.User>()
            .ForMember(x => x.Players, options => options.Ignore());
        
        this.CreateMap<Entities.User, User>();
    }
}