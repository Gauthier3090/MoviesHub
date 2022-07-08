using System.Data;
using MoviesHub_DAL.Entities;
using MoviesHub_DAL.Interfaces;
using Tools.Connections;


namespace MoviesHub_DAL.Repositories;

public class RepositoryPublication : Repository<int, PublicationEntity>, IRepositoryPublication
{
    public RepositoryPublication(Connection connection) : base(connection, "[Publication]", "Id")
    {
    }

    protected override PublicationEntity MapRecordToEntity(IDataRecord record)
    {
        return new PublicationEntity
        {
            Id = (int)record[TableId],
            Creator = (int)record["Creator"],
            Description = (string)record["Description"],
            Image = (string)record["Image"],
            Title = (string)record["Title"],
            CreatedAt = (DateTime)record["CreatedAt"]
        };
    }

    public override int Insert(PublicationEntity entity)
    {
        Command cmd = new("INSERT INTO [Publication] (Title, Description, Image, CreatedAt, Creator)" +
                          " VALUES (@Title, @Description, @Image, @CreatedAt, @Creator)");
        cmd.AddParameter("@Title", entity.Title);
        cmd.AddParameter("@Description", entity.Description);
        cmd.AddParameter("@Image", entity.Image);
        cmd.AddParameter("@CreatedAt", DateTime.Now);
        cmd.AddParameter("@Creator", entity.Creator);
        return Connection.ExecuteNonQuery(cmd);
    }

    public override bool Update(int id, PublicationEntity entity)
    {
        Command cmd = new("UPDATE [Publication] SET Title=@Title, Description=@Description, Image=@Image)" +
                          $" WHERE {TableId} = @Id");
        cmd.AddParameter("@Title", entity.Title);
        cmd.AddParameter("@Description", entity.Description);
        cmd.AddParameter("@Image", entity.Image);

        object res = Connection.ExecuteScalar(cmd) ?? false;
        return (bool)res;
    }

    public PublicationEntity? GetPublicationByUser(int creator)
    {
        Command cmd = new("SELECT * FROM [Publication] WHERE Creator = @Creator");
        cmd.AddParameter("@Creator", creator);
        return Connection.ExecuteReader(cmd, MapRecordToEntity, true).SingleOrDefault();
    }
}