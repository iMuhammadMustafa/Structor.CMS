#nullable disable
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PostsService.Testing.Setup;
using Xunit;

namespace PostsService.Infrastructure.Repositories;

public class Repository
{
    private TestRepository _repo { get; set; }
    private RepositoryTestEntity _newEntity = new()
    {
        Name = "John",
        RelationTestEntity = new RelationTestEntity()
        {
            Name = "John's Relation"
        }
    };
    public Repository()
    {
        _repo = new TestRepository();
    }

    [Fact]
    public async Task Should_Insert()
    {
        var res = await _repo.Add(_newEntity, true);
        res.Should().NotBeNull();
        res.Id.Should().NotBe(0);
        res.Guid.Should().Be(_newEntity.Guid);
        res.RelationTestEntity.Should().NotBeNull();
        res.RelationTestEntity.Guid.Should().Be(_newEntity.RelationTestEntity.Guid);
        res.RelationTestEntity.Id.Should().NotBe(0);
    }


    [Fact]
    public async Task Should_Return_CorrectObject()
    {
        await _repo.Add(_newEntity, true);
        var res = await _repo.FindFirstWhere(x => x.Name == _newEntity.Name);
        res.Should().NotBeNull();
        res.Name.Should().Be(_newEntity.Name);
        res.Guid.Should().Be(_newEntity.Guid);

        res = await _repo.FindByGuid(_newEntity.Guid);
        res.Should().NotBeNull();
        res.Name.Should().Be(_newEntity.Name);
        res.Guid.Should().Be(_newEntity.Guid);

    }

    [Fact]
    public async Task Should_Return_CorrectCount()
    {
        await _repo.Add(_newEntity, true);
        var res = await _repo.FindFirstWhere(x => x.Name == _newEntity.Name);
        res.Should().NotBeNull();
        res.Name.Should().Be(_newEntity.Name);
        res.Guid.Should().Be(_newEntity.Guid);
    }

    [Fact]
    public async Task Should_Update()
    {
        await _repo.Add(_newEntity, true);
        var res = await _repo.FindFirstWhere(x => x.Name == _newEntity.Name);
        res.Name = "New Name";

        await _repo.Update(res, true);

        var updated = await _repo.FindById(res.Id);

        updated.Should().NotBeNull();
        updated.Name.Should().Be("New Name");
    }

    [Fact]
    public async Task Should_Return_CorrectCountWhere()
    {

        var countToAdd = 5;
        var name = "Add";
        for (int i = 0; i < countToAdd; i++)
        {
            await _repo.Add(new RepositoryTestEntity() { Name = name });
        }
        await _repo.SaveChanges();

        var res = await _repo.FindAllWhere(x => x.Name == name);
        var count = await _repo.CountWhere(x => x.Name == name);

        res.Should().NotBeNull();
        res.Should().HaveCount(countToAdd);
        count.Should().Be(countToAdd);
    }
    [Fact]
    public async Task Should_Return_RelationEntity()
    {
        await _repo.Add(_newEntity, true);
        var res = await _repo.GetAllIncluding(x => x.RelationTestEntity).FirstOrDefaultAsync();

        res.Should().NotBeNull();
        res.RelationTestEntity.Should().NotBeNull();
        res.RelationTestEntity.Name.Should().Be(_newEntity.RelationTestEntity.Name);
    }

    [Fact]
    public async Task Should_Remove()
    {
        var addedEntity = await _repo.Add(_newEntity, true);


        var addedEntityFromDb = await _repo.FindById(addedEntity.Id);

        addedEntityFromDb.Should().NotBeNull();


        await _repo.Delete(addedEntityFromDb, true);


        addedEntityFromDb = await _repo.FindById(addedEntity.Id);

        addedEntityFromDb.Should().BeNull();
    }
}
