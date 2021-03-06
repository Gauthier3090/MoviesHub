using MoviesWorld_DAL.Entities;

namespace MoviesWorld_DAL.Interfaces;

public interface IRepositoryUser : IRepository<int, UserEntity>
{
    public UserEntity? GetByEmail(string? email);
    public string? GetPassword(string? email);
    public UserEntity? GetByFirstName(string? firstname);
    public bool UpdateImage(int id, string? filenameimage);
    public IEnumerable<UserEntity> Search(string firstname);
}