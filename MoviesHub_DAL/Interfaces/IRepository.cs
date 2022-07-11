namespace MoviesWorld_DAL.Interfaces;

public interface IRepository<TKey, TEntity>
{
    TKey Insert(TEntity entity);
    TEntity? GetById(TKey id);
    IEnumerable<TEntity> GetAll();
    bool Update(TKey id, TEntity entity);
    bool Delete(TKey id);
}