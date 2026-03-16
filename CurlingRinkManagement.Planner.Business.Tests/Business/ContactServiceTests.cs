using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.Enums;
using CurlingRinkManagement.Planner.Business.Services;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using Moq;

namespace CurlingRinkManagement.Planner.Tests.Business;

[TestClass]
public class ContactServiceFilterTests
{
    private ContactService _service;

    [TestInitialize]
    public void Setup()
    {
        var repoMock = new Mock<IClubRepository<Contact>>();
        _service = new ContactService(repoMock.Object);
    }

    [TestMethod]
    public void AddFilter_Generic_ShouldFilterByEmail()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Email = "john@test.com", PhoneNumber = "123", FirstName="John", Prefix="", LastName="Doe" },
            new Contact { Email = "mary@test.com", PhoneNumber = "456", FirstName="Mary", Prefix="", LastName="Smith" }
        }.AsQueryable();

        var filters = new[] { GenericFilters.Generic.ToString() };
        var values = new[] { "john" };

        // Act
        var result = _service.AddFilter(contacts, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
        Assert.AreEqual("john@test.com", result[0].Email);
    }

    [TestMethod]
    public void AddFilter_Generic_ShouldFilterByPhoneNumber()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Email="a@test.com", PhoneNumber="12345", FirstName="John", Prefix="", LastName="Doe"},
            new Contact { Email="b@test.com", PhoneNumber="99999", FirstName="Jane", Prefix="", LastName="Smith"}
        }.AsQueryable();

        var filters = new[] { GenericFilters.Generic.ToString() };
        var values = new[] { "123" };

        // Act
        var result = _service.AddFilter(contacts, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
        Assert.AreEqual("12345", result[0].PhoneNumber);
    }

    [TestMethod]
    public void AddFilter_Generic_ShouldFilterByFullName()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Email="a@test.com", PhoneNumber="123", FirstName="John", Prefix="van", LastName="Dijk"},
            new Contact { Email="b@test.com", PhoneNumber="456", FirstName="Jane", Prefix="", LastName="Smith"}
        }.AsQueryable();

        var filters = new[] { GenericFilters.Generic.ToString() };
        var values = new[] { "johnvandijk".Replace(" ", "") };

        // Act
        var result = _service.AddFilter(contacts, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
        Assert.AreEqual("John", result[0].FirstName);
    }

    [TestMethod]
    public void AddFilter_ShouldReturnUnfilteredQuery_WhenFilterLengthsMismatch()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Email="a@test.com", PhoneNumber="123", FirstName="John", Prefix="", LastName="Doe"},
            new Contact { Email="b@test.com", PhoneNumber="456", FirstName="Jane", Prefix="", LastName="Smith"}
        }.AsQueryable();

        var filters = new[] { GenericFilters.Generic.ToString() };
        var values = Array.Empty<string>();

        // Act
        var result = _service.AddFilter(contacts, filters, values).ToList();

        // Assert
        Assert.HasCount(2, result);
    }

    [TestMethod]
    public void AddFilter_ShouldIgnoreUnknownFilters()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Email="a@test.com", PhoneNumber="123", FirstName="John", Prefix="", LastName="Doe"},
            new Contact { Email="b@test.com", PhoneNumber="456", FirstName="Jane", Prefix="", LastName="Smith"}
        }.AsQueryable();

        var filters = new[] { "UnknownFilter" };
        var values = new[] { "john" };

        // Act
        var result = _service.AddFilter(contacts, filters, values).ToList();

        // Assert
        Assert.HasCount(2, result);
    }
}