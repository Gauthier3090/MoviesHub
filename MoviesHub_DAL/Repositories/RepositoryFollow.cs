using System.Data;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using Tools.Connections;


namespace MoviesWorld_DAL.Repositories;

public class RepositoryFollow : Repository<int, FollowEntity>, IRepositoryFollow
{
    public RepositoryFollow(Connection connection) : base(connection, "[Follow]", "Id")
    {

    }

    protected override FollowEntity MapRecordToEntity(IDataRecord record)
    {
        return new FollowEntity
        {
            Target = (int)record["Target"],
            Follower = (int)record["Follower"]
        };
    }

    public override int Insert(FollowEntity entity)
    {
        Command cmd = new("INSERT INTO [Publication] (Target, Follower)" +
                          " VALUES(@Target, @Follower)");
        cmd.AddParameter("@Target", entity.Target);
        cmd.AddParameter("@Follower", entity.Follower);
        return Connection.ExecuteNonQuery(cmd);
    }

    public override bool Update(int id, FollowEntity entity)
    {
        Command cmd = new("UPDATE [Follow] SET Target=@Target, Follow=@Follow)" +
                          $" WHERE {TableId} = @Id");
        cmd.AddParameter("@Target", entity.Target);
        cmd.AddParameter("@Follower", entity.Follower);
        return Connection.ExecuteNonQuery(cmd) == 1;
    }
}