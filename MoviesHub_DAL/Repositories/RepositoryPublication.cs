using System.Data;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using Tools.Connections;


namespace MoviesWorld_DAL.Repositories;

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
        return Connection.ExecuteNonQuery(cmd) == 1;
    }

    public IEnumerable<PublicationEntity> GetPublicationByUser(int creator)
    {
        Command cmd = new("SELECT * FROM [Publication] WHERE Creator = @Creator");
        cmd.AddParameter("@Creator", creator);
        DataTable dt = Connection.GetDataTable(cmd);
        EnumerableRowCollection<PublicationEntity> publications = dt.AsEnumerable().Select(row => new PublicationEntity
        {
            Id = row.Field<int>("Id"),
            Title = row.Field<string>("Title"),
            Description = row.Field<string>("Description"),
            Image = row.Field<string>("Image"),
            Creator = row.Field<int>("Creator"),
            CreatedAt = row.Field<DateTime>("CreatedAt")
        });
        return publications.ToList();
    }

    public string? PublicationExist(int id)
    {
        Command cmd = new("SELECT Title FROM [Publication] WHERE Id=@id");
        cmd.AddParameter("@id", id);
        object? res = Connection.ExecuteScalar(cmd);
        return res?.ToString();
    }
}