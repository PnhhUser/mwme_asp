namespace WMUI.Utils;

public static class UploadFile
{
    public static async Task<string> File(IFormFile file, string folder)
    {
        var directory = Path.Combine("wwwroot", "Images", folder);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(directory, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }
}
