using System.Data;
using MoviesHub_DAL.Entities;
using MoviesHub_DAL.Interfaces;
using Tools.Connections;

namespace MoviesHub_DAL.Repositories;

public class RepositoryUser : Repository<int, UserEntity>, IRepositoryUser
{
    public RepositoryUser(Connection connection) : base(connection, "[User]", "Id")
    {
    }

    protected override UserEntity MapRecordToEntity(IDataRecord record)
    {
        return new UserEntity
        {
            IdUser = (int)record[TableId],
            Email = (string)record["Email"],
            Password = (string)record["Password"],
            Firstname = (string)record["Firstname"],
            Lastname = (string)record["Lastname"],
            Old = (int)record["Old"]
        };
    }

    public override int Insert(UserEntity entity)
    {
        Command cmd = new("INSERT INTO [User] (email, password, firstname, lastname, old)" +
                          $" OUTPUT inserted.{TableId}" +
                          " VALUES (@email, @password, @firstname, @lastname, @old)");

        cmd.AddParameter("@email", entity.Email);
        cmd.AddParameter("@password", entity.Password);
        cmd.AddParameter("@firstname", entity.Firstname);
        cmd.AddParameter("@lastname", entity.Lastname);
        cmd.AddParameter("@old", entity.Old);

        object res = Connection.ExecuteScalar(cmd) ?? -1;
        return (int)res;
    }

    public override bool Update(int id, UserEntity entity)
    {
        Command cmd = new("UPDATE [User]" +
                          "SET email=email, password=password, firstname=firstname, lastname=lastname, age=age" +
                          $" WHERE {TableId}={id}");

        cmd.AddParameter("email", entity.Email);
        cmd.AddParameter("password", entity.Password);
        cmd.AddParameter("firstname", entity.Firstname);
        cmd.AddParameter("lastname", entity.Lastname);
        cmd.AddParameter("age", entity.Old);

        object res = Connection.ExecuteScalar(cmd) ?? 0;
        return (bool)res;
    }
}