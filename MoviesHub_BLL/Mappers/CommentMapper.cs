using MoviesWorld_DAL.Entities;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld_BLL.Mappers;

public static class CommentMapper
{
    public static CommentDto ToDto(this CommentEntity entity)
    {
        return new CommentDto
        {
            Id = entity.Id,
            Headline = entity.Headline,
            Body = entity.Body,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            User = entity.User,
            Publication = entity.Publication
        };
    }

    public static CommentEntity ToEntity(this CommentDto dto)
    {
        return new CommentEntity
        {
            Id = dto.Id,
            Headline = dto.Headline,
            Body = dto.Body,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
            User = dto.User,
            Publication = dto.Publication
        };
    }
}