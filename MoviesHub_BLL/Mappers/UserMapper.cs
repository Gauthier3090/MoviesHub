using MoviesHub_BLL.DTO;
using MoviesHub_DAL.Entities;

namespace MoviesHub_BLL.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto
        {
            IdUser = entity.IdUser,
            Email = entity.Email,
            Password = entity.Password,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Old = entity.Old
        };
    }

    public static UserEntity ToEntity(this UserDto dto)
    {
        return new UserEntity
        {
            IdUser = dto.IdUser,
            Email = dto.Email,
            Password = dto.Password,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Old = dto.Old
        };
    }
}