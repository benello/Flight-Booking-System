using Application.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace Application.Services;

public class FileService
{
    private readonly IWebHostEnvironment host;
    private static string ImagesPath => "Images";
    private string PassportPath => Path.Combine(ImagesPath, "Passport");
    
    public FileService(IWebHostEnvironment host)
    {
        this.host = host;
    }
    
    /// <summary>
    /// Saves the file to the file system.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileCategory"></param>
    /// <returns>returns the relative path to the file</returns>
    public string SaveFile(IFormFile file, FileCategory fileCategory)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fileDirectory = GetFileDirectory(fileCategory);
        
        if (!Directory.Exists(Path.Combine(host.WebRootPath, fileDirectory)))
            Directory.CreateDirectory(Path.Combine(host.WebRootPath, fileDirectory));
        
        var relativePath = Path.Combine(fileDirectory, fileName);
        var filePath = Path.Combine(host.WebRootPath, relativePath);
        
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        file.CopyTo(fileStream);
        
        return PrependRelativePath(relativePath);
    }
    
    /// <summary>
    /// Gets the file directory for the specified file category.
    /// </summary>
    /// <param name="fileCategory"></param>
    /// <returns>The directory location for the specified file category</returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetFileDirectory(FileCategory fileCategory)
    {
        return fileCategory switch
        {
            FileCategory.Passport => PassportPath,
            _ => throw new ArgumentException("Invalid file category.", nameof(fileCategory))
        };
    }
    
    /// <summary>
    /// This is to be used when a relative path is needed for the database.
    /// Since the relative path to the application is used according to the current context and not the root of the application.
    /// </summary>
    /// <param name="relativePath"></param>
    /// <returns>The relative path prefixed with a /</returns>
    private static string PrependRelativePath(string relativePath)
    {
        if (!relativePath.StartsWith(Path.PathSeparator))
           return $"{Path.PathSeparator}{relativePath}";
        
        return relativePath;
    }
}