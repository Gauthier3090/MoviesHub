using System.Data;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using Tools.Connections;

namespace MoviesWorld_DAL.Repositories;

public class RepositoryComment: Repository<int, CommentEntity>, IRepositoryComment
{
    public RepositoryComment(Connection connection) : base(connection, "[Comment]", "Id")
    {

    }

    protected override CommentEntity MapRecordToEntity(IDataRecord record)
    {
        return new CommentEntity
        {
            Id = (int)record[TableId],
            Headline = (string)record["Headline"],
            Body = (string)record["Body"],
            CreatedAt = (DateTime)record["CreatedAt"],
            User = (int)record["UserId"],
            Publication = (int)record["PublicationId"],
            IsActive = (bool)record["IsActive"]
        };
    }

    public override int Insert(CommentEntity entity)
    {
        Command cmd = new("INSERT INTO [Comment] (Headline, Body, CreatedAt, UserId, PublicationId)" +
                          " VALUES (@Headline, @Body, @CreatedAt, @User, @Publication)");
        cmd.AddParameter("@Headline", entity.Headline);
        cmd.AddParameter("@Body", entity.Body);
        cmd.AddParameter("@CreatedAt", DateTime.Now);
        cmd.AddParameter("@User", entity.User);
        cmd.AddParameter("@Publication", entity.Publication);
        return Connection.ExecuteNonQuery(cmd);
    }

    public override bool Update(int id, CommentEntity entity)
    {
        Command cmd = new("UPDATE [Comment] SET Headline=@Headline, Body=@Body, UpdatedAt=@UpdatedAt)" +
                          $" WHERE {TableId} = @Id");
        cmd.AddParameter("@Headline", entity.Headline);
        cmd.AddParameter("@Body", entity.Body);
        cmd.AddParameter("@UpdatedAt", DateTime.Now);
        return Connection.ExecuteNonQuery(cmd) == 1;
    }

    public IEnumerable<CommentEntity> GetCommentsByPublication(int id)
    {
        Command cmd = new("SELECT * FROM [Comment] WHERE PublicationId = @id");
        cmd.AddParameter("@id", id);
        DataTable dt = Connection.GetDataTable(cmd);
        EnumerableRowCollection<CommentEntity> publications = dt.AsEnumerable().Select(row => new CommentEntity
        {
            Id = row.Field<int>("Id"),
            Headline = row.Field<string>("Headline"),
            Body = row.Field<string>("Body"),
            User = row.Field<int>("UserId"),
            Publication = row.Field<int>("PublicationId"),
            CreatedAt = row.Field<DateTime>("CreatedAt")
        });
        return publications;
    }
}