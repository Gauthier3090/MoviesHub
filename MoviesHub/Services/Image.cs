namespace MoviesHub.Services;

public class Image
{
    private static readonly string FolderName = Path.Combine("wwwroot", "images");
    private IFormFile? ImageFile { get; set; }
    public string? FileName { get; set; }

    public Image(IFormFile formFile)
    {
        ImageFile = formFile;
        FileName = new Guid() + ImageFile.FileName;
    }

    private async Task<byte[]> GetBytes()
    {
        await using MemoryStream memoryStream = new();
        if (ImageFile != null) await ImageFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async void SaveImage()
    {
        string? imagePath = FileName;
        if (imagePath == null) return;
        await using FileStream imageFile = new(imagePath, FileMode.Create);
        byte[] bytes = await GetBytes();
        imageFile.Write(bytes, 0, bytes.Length);
        imageFile.Flush();
    }
}