using Domain.Models;

namespace Domain.Interfaces;

public interface IMessageService
{
    Task RankUpdate(Player loaded);
}