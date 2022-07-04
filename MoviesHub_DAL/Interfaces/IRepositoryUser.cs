using MoviesHub_DAL.Entities;

namespace MoviesHub_DAL.Interfaces;

public interface IRepositoryUser : IRepository<int, UserEntity>
{
    UserEntity? GetByEmail(string? email);
    string? GetPassword(string? email);
}