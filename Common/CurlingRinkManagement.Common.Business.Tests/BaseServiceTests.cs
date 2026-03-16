using CurlingRinkManagement.Common.Business.Services;
using CurlingRinkManagement.Common.Data.Database;
using Moq;

namespace CurlingRinkManagement.Tests.Services;

[TestClass]
public class BaseServiceTests
{
    private Mock<IClubRepository<TestEntity>> _repoMock;
    private TestService _service;

    [TestInitialize]
    public void Setup()
    {
        _repoMock = new Mock<IClubRepository<TestEntity>>();
        _service = new TestService(_repoMock.Object);
    }

    [TestMethod]
    public void Create_ShouldCallRepositoryCreate()
    {
        var entity = new TestEntity { Id = Guid.NewGuid() };

        _repoMock.Setup(r => r.Create(entity)).Returns(entity);

        var result = _service.Create(entity);

        Assert.AreEqual(entity, result);
        _repoMock.Verify(r => r.Create(entity), Times.Once);
    }

    [TestMethod]
    public void Delete_ShouldCallRepositoryDelete()
    {
        var id = Guid.NewGuid();
        var entity = new TestEntity { Id = id };

        _repoMock.Setup(r => r.GetAll())
            .Returns(new List<TestEntity> { entity }.AsQueryable());

        _service.Delete(id);

        _repoMock.Verify(r => r.Delete(entity), Times.Once);
    }

    [TestMethod]
    public void GetById_ShouldReturnEntity_WhenExists()
    {
        var id = Guid.NewGuid();
        var entity = new TestEntity { Id = id };

        _repoMock.Setup(r => r.GetAll())
            .Returns(new List<TestEntity> { entity }.AsQueryable());

        var result = _service.GetById(id);

        Assert.AreEqual(entity, result);
    }

    [TestMethod]
    public void GetById_ShouldThrow_WhenEntityDoesNotExist()
    {
        _repoMock.Setup(r => r.GetAll())
            .Returns(new List<TestEntity>().AsQueryable());

        Assert.ThrowsExactly<KeyNotFoundException>(() => _service.GetById(Guid.NewGuid()));
    }

    [TestMethod]
    public void GetAll_ShouldReturnLimitedResults()
    {
        var data = Enumerable.Range(1, 10)
            .Select(i => new TestEntity { Id = Guid.NewGuid() })
            .ToList();

        _repoMock.Setup(r => r.GetAll())
            .Returns(data.AsQueryable());

        var result = _service.GetAll(null, 5, null, null);

        Assert.HasCount(5, result);
    }

    [TestMethod]
    public void GetAmount_ShouldReturnCorrectCount()
    {
        var data = Enumerable.Range(1, 10)
            .Select(i => new TestEntity { Id = Guid.NewGuid() })
            .ToList();

        _repoMock.Setup(r => r.GetAll())
            .Returns(data.AsQueryable());

        var result = _service.GetAmount(null, null);

        Assert.AreEqual(10, result);
    }
}

// Test Entity
public class TestEntity : IClubEntity
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
}

// Concrete service implementation for testing
public class TestService : BaseService<TestEntity>
{
    public TestService(IClubRepository<TestEntity> repo) : base(repo)
    {
    }

    public override TestEntity Update(TestEntity entity)
    {
        return entity;
    }
}