using Microsoft.EntityFrameworkCore.ChangeTracking;
using Library.EntityModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new ()
    {
        SlidingExpiration = TimeSpan.FromMinutes(30)
    };

    private LibraryContext _db;
    public BookRepository(LibraryContext db, IMemoryCache memoryCache)
    {
        _db = db;
        _memoryCache = memoryCache;
    }

    public async Task<Book?> CreateAsync(Book b)
    {
        EntityEntry<Book> added = await _db.Books.AddAsync(b);

        int affected = await _db.SaveChangesAsync();

        if (affected == 1)
        {
            _memoryCache.Set(b.BookId, b, _cacheEntryOptions);
            return b;
        }
        return null;
    }

    public Task<Book[]> RetrieveAllAsync()
    {
        return _db.Books.ToArrayAsync();
    }

    public Task<Book?> RetrieveAsync(int BookId)
    {
        if (_memoryCache.TryGetValue(BookId, out Book? fromCache))
            return Task.FromResult(fromCache);

        Book? fromDb = _db.Books.FirstOrDefault(b => b.BookId == BookId);

        if (fromDb is null) return Task.FromResult(fromDb);

        _memoryCache.Set(fromDb.BookId, fromDb, _cacheEntryOptions);

        return Task.FromResult(fromDb)!;
    }

    public async Task<Book?> UpdateAsync(Book b)
    {
        _db.Books.Update(b);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            _memoryCache.Set(b.BookId, b, _cacheEntryOptions);
            return b;
        }
        return null;
    }

    public async Task<bool?> DeleteAsync(int BookId)
    {
        Book? b = await _db.Books.FindAsync(BookId);
        if (b is null) return null;

        _db.Books.Remove(b);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            _memoryCache.Remove(b.BookId);
            return true;
        }
        return null;
    }
}