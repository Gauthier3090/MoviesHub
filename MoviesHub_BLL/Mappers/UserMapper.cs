using MoviesWorld_DAL.Entities;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld_BLL.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Email = entity.Email,
            Password = entity.Password,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Birthdate = entity.Birthdate,
            Image = entity.Image
        };
    }

    public static UserEntity ToEntity(this UserDto dto)
    {
        return new UserEntity
        {
            Id = dto.Id,
            Email = dto.Email,
            Password = dto.Password,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Birthdate = dto.Birthdate,
            Image = dto.Image
        };
    }
}