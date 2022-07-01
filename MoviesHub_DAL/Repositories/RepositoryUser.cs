using System.Data;
using Microsoft.AspNetCore.Http;
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
            Id = (int)record[TableId],
            Email = (string)record["Email"],
            Password = (string)record["Password"],
            Firstname = (string)record["Firstname"],
            Lastname = (string)record["Lastname"],
            Birthdate = (DateTime)record["Birthdate"]
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
        cmd.AddParameter("@image", entity.Image?.FileName);
        cmd.AddParameter("@createdAt", DateTime.Now);
        if (entity.Image != null) SaveImage(entity.Image);
        object res = Connection.ExecuteScalar(cmd) ?? -1;
        return (int)res;
    }

    public static async Task<byte[]> GetBytes(IFormFile formFile)
    {
        await using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    private static async void SaveImage(IFormFile filename)
    {
        string folderName = Path.Combine("wwwroot", "images");
        string imagePath = Path.Combine(folderName, filename.FileName);
        await using FileStream imageFile = new(imagePath, FileMode.Create);
        byte[] bytes = await GetBytes(filename);
        imageFile.Write(bytes, 0, bytes.Length);
        imageFile.Flush();
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
        cmd.AddParameter("age", entity.Birthdate);

        object res = Connection.ExecuteScalar(cmd) ?? 0;
        return (bool)res;
    }

    public int IsAuthenticated(string email, string password)
    {
        Command cmd = new("SELECT id FROM [User] WHERE email=@email AND password=@password");
        cmd.AddParameter("@email", email);
        cmd.AddParameter("@password", password);
        return Connection.ExecuteNonQuery(cmd);
    }

    public string GetPassword(string email)
    {
        Command cmd = new("SELECT password FROM [User] WHERE email=@email");
        cmd.AddParameter("@email", email);
        object res = Connection.ExecuteScalar(cmd) ?? 0;
        return res?.ToString();
    }

    public UserEntity GetByEmail(string email)
    {
        Command cmd = new("SELECT * FROM [User] WHERE email=@email");
        cmd.AddParameter("@email", email);
        return Connection.ExecuteReader(cmd, MapRecordToEntity, true).SingleOrDefault();
    }
}