using Library.EntityModels;
namespace Library.UnitTests;

public class EntityModelTests
{
    [Fact]
    public void DatabaseConnectTest()
    {
        using LibraryContext db = new();
        Assert.True(db.Database.CanConnect());
    }
}