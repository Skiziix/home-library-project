using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Library.EntityModels;

public static class LibraryContextExtensions
{
    public static IServiceCollection AddLibraryContext(
        this IServiceCollection services,
        string relativePath = "../../dev-db-build",
        string databaseName = "Library.db")
    {
        string path = Path.Combine(relativePath, databaseName);
        path = Path.GetFullPath(path);
        LibraryContextLogger.WriteLine($"Database path: {path}");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(message: $"{path} not found.", fileName:path);
        }
        services.AddDbContext<LibraryContext>(options => 
        {
            options.UseSqlite($"Data Source={path}");
            options.LogTo(LibraryContextLogger.WriteLine, 
            new[] {Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting});
        },
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Transient);

        return services;
    }
}
