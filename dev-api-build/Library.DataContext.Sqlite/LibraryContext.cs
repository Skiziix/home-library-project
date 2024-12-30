using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityModels;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<VRecentBook> VRecentBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string database = "Library.db";
            string dir = Environment.CurrentDirectory;
            string path = string.Empty;
            if (dir.EndsWith("net8.0"))
            {
                path = Path.Combine("..", "..", "..", "..", database);
            }
            else
            {
                path = Path.Combine("..", database);
            }
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message: $"{path} not found.", fileName: path);
            }
            optionsBuilder.UseSqlite($"Data Source={path}");
            optionsBuilder.LogTo(LibraryContextLogger.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<VRecentBook>(entity =>
        {
            entity.ToView("v_recent_books");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
