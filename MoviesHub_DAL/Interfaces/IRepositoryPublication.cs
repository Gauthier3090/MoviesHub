using MoviesHub_DAL.Entities;

namespace MoviesHub_DAL.Interfaces;

public interface IRepositoryPublication : IRepository<int, PublicationEntity>
{
    public List<PublicationEntity> GetPublicationByUser(int creator);
}