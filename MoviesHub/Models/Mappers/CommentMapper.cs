using MoviesWorld_BLL.DTO;

namespace MoviesWorld.Models.Mappers;

public static class CommentMapper
{
    public static Comment ToModel(this CommentDto dto)
    {
        return new Comment
        {
            Id = dto.Id,
            Headline = dto.Headline,
            Body = dto.Body,
            User = dto.User!.Id,
            Publication = dto.Publication
        };
    }
}