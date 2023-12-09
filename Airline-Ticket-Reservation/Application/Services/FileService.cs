using Application.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace Application.Services;

public class FileService
{
    private readonly IWebHostEnvironment host;
    private static string ImagesPath => "Images";
    private static string RepositoriesPath => "Data";
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
        var fileDirectory = GetRelativeFileDirectory(fileCategory);
        var absoluteDirectory = GetAbsoluteDirectory(fileCategory);
        
        if (!Directory.Exists(absoluteDirectory))
            Directory.CreateDirectory(Path.Combine(host.WebRootPath, fileDirectory));
        
        var relativePath = Path.Combine(fileDirectory, fileName);
        var filePath = Path.Combine(host.WebRootPath, relativePath);
        
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        file.CopyTo(fileStream);
        
        return PrependRelativePath(relativePath);
    }
    
    /// <summary>
    /// Gets the relative file directory for the specified file category.
    /// </summary>
    /// <param name="fileCategory"></param>
    /// <returns>The directory location for the specified file category</returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetRelativeFileDirectory(FileCategory fileCategory)
    {
        return fileCategory switch
        {
            FileCategory.Passport => PassportPath,
            FileCategory.Repository => RepositoriesPath,
            _ => throw new ArgumentException("Invalid file category.", nameof(fileCategory))
        };
    }

    /// <summary>
    /// Gets the absolute file directory for the specified file category
    /// </summary>
    /// <param name="fileCategory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetAbsoluteDirectory(FileCategory fileCategory)
    {
        return fileCategory switch
        {
            FileCategory.Passport => Path.Combine(host.WebRootPath, PassportPath),
            FileCategory.Repository => Path.Combine(host.WebRootPath, RepositoriesPath),
            _ => throw new ArgumentException("Invalid file category.", nameof(fileCategory))
        };
    }

    /// <summary>
    /// Deletes a file which is located in the given location.
    /// </summary>
    /// <param name="path">The relative path within the application</param>
    public void DeleteFile(string path)
    {
        var absolute = Path.Combine(host.WebRootPath, path);
        File.Delete(absolute);
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
           return $"{Path.DirectorySeparatorChar}{relativePath}";
        
        return relativePath;
    }
}