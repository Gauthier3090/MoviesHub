using System.Data;
using System.Text.RegularExpressions;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using Tools.Connections;

namespace MoviesWorld_DAL.Repositories;

public class RepositoryUser : Repository<int, UserEntity>, IRepositoryUser
{
    public RepositoryUser(Connection connection) : base(connection, "[User]", "Id")
    {
    }

    protected override UserEntity MapRecordToEntity(IDataRecord record)
    {
        return new UserEntity
        {
            Id = (int)record[TableId],
            Email = (string)record["Email"],
            Password = (string)record["Password"],
            Firstname = (string)record["Firstname"],
            Lastname = (string)record["Lastname"],
            Birthdate = (DateTime)record["Birthdate"],
            Image = (string)record["Image"]
        };
    }

    public override int Insert(UserEntity entity)
    {
        Command cmd = new("INSERT INTO [User] (email, firstname, lastname, [password], birthdate, [image], createdAt)" +
                          $" OUTPUT inserted.{TableId}" +
                          " VALUES (@email, @firstname, @lastname, @password, @birthdate, @image, @createdAt)");

        cmd.AddParameter("@email", entity.Email);
        cmd.AddParameter("@firstname", entity.Firstname);
        cmd.AddParameter("@lastname", entity.Lastname);
        cmd.AddParameter("@password", entity.Password);
        cmd.AddParameter("@birthdate", entity.Birthdate);
        cmd.AddParameter("@image", entity.Image);
        cmd.AddParameter("@createdAt", DateTime.Now);
        object res = Connection.ExecuteScalar(cmd) ?? -1;
        return (int)res;
    }

    public override bool Update(int id, UserEntity entity)
    {
        Command cmd = new("UPDATE [User]" +
                          "SET email=@email, password=@password, firstname=@firstname, lastname=@lastname, birthdate=@birthdate, image=@image, updatedAt=@updatedAt" +
                          $" WHERE {TableId} = @id");

        cmd.AddParameter("@email", entity.Email);
        cmd.AddParameter("@password", entity.Password);
        cmd.AddParameter("@firstname", entity.Firstname);
        cmd.AddParameter("@lastname", entity.Lastname);
        cmd.AddParameter("@birthdate", entity.Birthdate);
        cmd.AddParameter("@image", entity.Image);
        cmd.AddParameter("@updatedAt", entity.UpdatedAt);
        cmd.AddParameter("@id", id);

        return Connection.ExecuteNonQuery(cmd) == 1;
    }

    public bool UpdateImage(int id, string? filenameimage)
    {
        Command cmd = new("UPDATE [User]" +
                          "SET image=@image, updatedAt=@updatedAt" +
                          $" WHERE {TableId} = @id");

        cmd.AddParameter("@image", filenameimage);
        cmd.AddParameter("@updatedAt", DateTime.Now);
        cmd.AddParameter("@id", id);

        return Connection.ExecuteNonQuery(cmd) == 1;
    }

    public string? GetPassword(string? email)
    {
        Command cmd = new("SELECT password FROM [User] WHERE email=@email");
        cmd.AddParameter("@email", email);
        object? res = Connection.ExecuteScalar(cmd);
        return res?.ToString();
    }

    public UserEntity? GetByEmail(string? email)
    {
        Command cmd = new("SELECT * FROM [User] WHERE email=@email");
        cmd.AddParameter("@email", email);
        return Connection.ExecuteReader(cmd, MapRecordToEntity, true).SingleOrDefault();
    }

    public UserEntity? GetByFirstName(string? firstname)
    {
        Command cmd = new("SELECT * FROM [User] WHERE firstname=@firstname");
        cmd.AddParameter("@firstname", firstname);
        return Connection.ExecuteReader(cmd, MapRecordToEntity, true).SingleOrDefault();
    }

    public IEnumerable<UserEntity> Search(string firstname)
    {
        const string regexBarChar = "[%_]";

        string firstnameClean = Regex.Replace(firstname, regexBarChar, String.Empty, RegexOptions.None);
        if (string.IsNullOrWhiteSpace((firstnameClean)))
        {
            return Enumerable.Empty<UserEntity>();
        }


        Command cmd = new("SELECT * FROM [User] WHERE firstname LIKE @firstname");
        cmd.AddParameter("@firstname", firstnameClean + "%");
        DataTable dt = Connection.GetDataTable(cmd);
        EnumerableRowCollection<UserEntity> users = dt.AsEnumerable().Select(row => new UserEntity
        {
            Id = row.Field<int>("Id"),
            Firstname = row.Field<string>("Firstname"),
            Lastname = row.Field<string>("Lastname"),
            Image = row.Field<string>("Image"),
        });
        return users.ToList();
    }
}