using MoviesWorld_BLL.DTO;

namespace MoviesWorld.Models.Mappers;

public static class PublicationMapper
{
    public static Publication ToModel(this PublicationDto dto)
    {
        return new Publication
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Image = dto.Image
        };
    }
}