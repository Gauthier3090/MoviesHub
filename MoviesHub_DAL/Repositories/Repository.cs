using System.Collections.Generic;
using System.Data;
using System.Linq;
using MoviesHub_DAL.Interfaces;
using Tools.Connections;

namespace MoviesHub_DAL.Repositories;

public abstract class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
{
    protected Connection Connection { get; set; }
    protected string TableName { get; set; }
    protected string TableId { get; set; }

    protected Repository(Connection connection, string tableName, string tableId)
    {
        Connection = connection;
        TableName = tableName;
        TableId = tableId;
    }

    protected abstract TEntity MapRecordToEntity(IDataRecord record);

    public virtual IEnumerable<TEntity> GetAll()
    {
        Command cmd = new($"SELECT * FROM {TableName}");

        return Connection.ExecuteReader(cmd, MapRecordToEntity, false);
    }

    public virtual TEntity GetById(TKey id)
    {
        Command cmd = new($"SELECT * FROM {TableName} WHERE {TableId} = @Id");
        cmd.AddParameter("Id", id);

        return Connection.ExecuteReader(cmd, MapRecordToEntity, false).SingleOrDefault();
    }

    public abstract TKey Insert(TEntity entity);

    public abstract bool Update(TKey id, TEntity entity);

    public virtual bool Delete(TKey id)
    {
        Command cmd = new($"DELETE FROM {TableName} WHERE {TableId} = @Id");
        cmd.AddParameter("Id", id);

        return Connection.ExecuteNonQuery(cmd) == 1;
    }
}