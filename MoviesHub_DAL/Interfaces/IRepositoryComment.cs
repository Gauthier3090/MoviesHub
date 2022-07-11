using MoviesWorld_DAL.Entities;

namespace MoviesWorld_DAL.Interfaces;

public interface IRepositoryComment : IRepository<int, CommentEntity>
{
    public IEnumerable<CommentEntity> GetCommentsByPublication(int id);
}