using MoviesHub_BLL.DTO;
using MoviesHub_BLL.Mappers;
using MoviesHub_DAL.Entities;
using MoviesHub_DAL.Interfaces;

namespace MoviesHub_BLL.Services;

public class UserService
{
    private readonly IRepositoryUser _repositoryUser;

    public UserService(IRepositoryUser repositoryUser)
    {
        _repositoryUser = repositoryUser;
    }

    public UserDto? Insert(string email, string firstName, string lastName, string password, DateTime birthdate, string image)
    {
        int id = _repositoryUser.Insert(new UserEntity
        {
            Email = email,
            Password = password,
            Firstname = firstName,
            Lastname = lastName,
            Birthdate = birthdate,
            Image = image,
            CreatedAt = DateTime.Now
        });
        return _repositoryUser.GetById(id)?.ToDto();
    }

    public bool Update(int id, UserEntity entity)
    {
        return _repositoryUser.Update(id, entity);
    }

    public UserDto? GetByEmail(string email)
    {
        return _repositoryUser.GetByEmail(email)?.ToDto();
    }

    public string? GetPassword(string? email)
    {
        return _repositoryUser.GetPassword(email);
    }

    public UserDto? GetById(int id)
    {
        return _repositoryUser.GetById(id)?.ToDto();
    }

    public bool Delete(int id)
    {
        return _repositoryUser.Delete(id);
    }
}