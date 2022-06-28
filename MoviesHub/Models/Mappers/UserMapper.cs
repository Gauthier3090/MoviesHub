using MoviesHub_BLL.DTO;

namespace MoviesHub.Models.Mappers;

public static class UserMapper
{
    public static User ToModel(this UserDto dto)
    {
        return new User
        {
            IdUser = dto.IdUser,
            Email = dto.Email,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Old = dto.Old
        };
    }
}