using MoviesWorld_DAL.Entities;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld_BLL.Mappers;

public static class PublicationMapper
{
    public static PublicationDto ToDto(this PublicationEntity entity)
    {
        return new PublicationDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Image = entity.Image,
            Creator = entity.Creator,
            CreatedAt = entity.CreatedAt,
        };
    }

    public static PublicationEntity ToEntity(this PublicationDto dto)
    {
        return new PublicationEntity
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Image = dto.Image,
            Creator = dto.Creator,
            CreatedAt = dto.CreatedAt
        };
    }
}