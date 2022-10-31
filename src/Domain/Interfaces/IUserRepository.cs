using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task Upsert(User user);
    Task<User?> GetByDiscordId(ulong userId);
}