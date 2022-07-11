namespace MoviesWorld.Services;

public class ImageService
{
    private readonly string _folderName = Path.Combine("wwwroot", "images");
    private IFormFile? ImageFile { get; }
    public string? FileName { get; }

    public ImageService(IFormFile formFile)
    {
        ImageFile = formFile;
        FileName = Guid.NewGuid() + ImageFile.FileName;
    }

    private async Task<byte[]> GetBytes()
    {
        await using MemoryStream memoryStream = new();
        if (ImageFile != null) await ImageFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async void SaveImage()
    {
        if (FileName == null) return;
        string imagePath = Path.Combine(_folderName, FileName);
        await using FileStream imageFile = new(imagePath, FileMode.Create);
        byte[] bytes = await GetBytes();
        imageFile.Write(bytes, 0, bytes.Length);
        imageFile.Flush();
    }

    public void DeleteImage(string fileImage)
    {
        string path = Path.Combine(_folderName, fileImage);
        File.Delete(path);
    }

}