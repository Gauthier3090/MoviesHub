using MoviesWorld_DAL.Entities;

namespace MoviesWorld_DAL.Interfaces;

public interface IRepositoryUser : IRepository<int, UserEntity>
{
    UserEntity? GetByEmail(string? email);
    string? GetPassword(string? email);
}