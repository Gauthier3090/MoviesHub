using MoviesWorld_DAL.Entities;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld_BLL.Mappers;

public static class FollowMapper
{
    public static FollowDto ToDto(this FollowEntity entity, UserDto userTarget, UserDto userFollow)
    {
        return new FollowDto
        {
            Target = userTarget,
            Follow = userFollow
        };
    }

    public static FollowEntity ToEntity(this FollowDto dto)
    {
        return new FollowEntity
        {
            Target = dto.Target!.Id,
            Follower = dto.Follow!.Id
        };
    }
}