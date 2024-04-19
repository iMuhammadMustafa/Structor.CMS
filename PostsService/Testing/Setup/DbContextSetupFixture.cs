using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PostsService.Infrastructure.Entities;
using PostsService.Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsService.Testing.Setup;

public class DbContextSetupFixture : IDisposable
{

    private static string DefaultSqLiteMemoryConnectionString = $"Data Source={Guid.NewGuid()};Mode=Memory;";
    private static SqliteConnection _connection { get; set; } = null!;
    public static TestDbContext CreateContextForSQLiteInMemory(string? connectionString = null)
    {
        if (string.IsNullOrWhiteSpace(connectionString)) { connectionString = DefaultSqLiteMemoryConnectionString; }
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
        var _options = new DbContextOptionsBuilder()
                .UseSqlite(_connection)
                .Options;
        var context = new TestDbContext(_options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
        }
    }
}
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options) { }
    public DbSet<RepositoryTestEntity> RepositoryTestEntities { get; set; }
}

public class RepositoryTestEntity : IEntity
{
    public required string Name { get; set; }

    [ForeignKey(nameof(RelationTestEntity))]
    public int? RelationTestEntityId { get; set; }
    public virtual RelationTestEntity? RelationTestEntity { get; set; }
}
public class RelationTestEntity : IEntity
{
    public required string Name { get; set; }
}
public class TestRepository : Repository<RepositoryTestEntity, TestDbContext>
{
    public TestRepository() : base(DbContextSetupFixture.CreateContextForSQLiteInMemory())
    {
    }

}