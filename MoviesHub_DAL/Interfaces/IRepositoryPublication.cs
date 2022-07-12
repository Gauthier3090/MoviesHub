using MoviesWorld_DAL.Entities;

namespace MoviesWorld_DAL.Interfaces;

public interface IRepositoryPublication : IRepository<int, PublicationEntity>
{
    public IEnumerable<PublicationEntity> GetPublicationByUser(int creator);
    public string? PublicationExist(int id);
}