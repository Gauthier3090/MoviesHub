using MoviesWorld_BLL.DTO;

namespace MoviesWorld.Models.Mappers;

public static class UserMapper
{
    public static User ToModel(this UserDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Email = dto.Email,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Birthdate = dto.Birthdate,
        };
    }
}