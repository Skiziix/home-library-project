using Library.EntityModels;
namespace Library.WebApi.Repositories;

public interface IBookRepository
{
    Task<Book?> CreateAsync(Book b);
    Task<Book[]> RetrieveAllAsync();
    Task<Book?> RetrieveAsync(int id);
    Task<Book?> UpdateAsync(Book b);
    Task<bool?> DeleteAsync(int id);
}