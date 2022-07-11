using MoviesWorld_DAL.Entities;

namespace MoviesWorld_DAL.Interfaces;

public interface IRepositoryPublication : IRepository<int, PublicationEntity>
{
    public List<PublicationEntity> GetPublicationByUser(int creator);
}