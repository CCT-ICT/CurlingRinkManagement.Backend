using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.Enums;
using CurlingRinkManagement.Planner.Business.Services;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Enums;
using Moq;

namespace CurlingRinkManagement.Planner.Business.Tests;

[TestClass]
public class CustomerRequestServiceFilterTests
{
    private CustomerRequestService _service;

    [TestInitialize]
    public void Setup()
    {
        var repoMock = new Mock<IClubRepository<CustomerRequest>>();
        _service = new CustomerRequestService(repoMock.Object);
    }

    [TestMethod]
    public void AddFilter_ShouldFilterByCustomerRequestState()
    {
        // Arrange
        var data = new List<CustomerRequest>
        {
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.InvoiceSent },
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.PaymentReceived },
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.InvoiceSent }
        }.AsQueryable();

        var filters = new[] { nameof(GenericFilters.CustomerRequestState) };
        var values = new[] { nameof(CustomerRequestState.InvoiceSent) };

        // Act
        var result = _service.AddFilter(data, filters, values).ToList();

        // Assert
        Assert.HasCount(2, result);
        Assert.IsTrue(result.All(r => r.CustomerRequestState == CustomerRequestState.InvoiceSent));
    }

    [TestMethod]
    public void AddFilter_ShouldReturnUnfilteredQuery_WhenStateInvalid()
    {
        // Arrange
        var data = new List<CustomerRequest>
        {
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.InvoiceSent },
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.PaymentReceived }
        }.AsQueryable();

        var filters = new[] { nameof(GenericFilters.CustomerRequestState) };
        var values = new[] { "invalidstate" };

        // Act
        var result = _service.AddFilter(data, filters, values).ToList();

        // Assert
        Assert.HasCount(2, result);
    }

    [TestMethod]
    public void AddFilter_ShouldReturnUnfilteredQuery_WhenFilterLengthsMismatch()
    {
        // Arrange
        var data = new List<CustomerRequest>
        {
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.InvoiceSent },
            new CustomerRequest { Id = Guid.NewGuid(), CustomerRequestState = CustomerRequestState.PaymentReceived }
        }.AsQueryable();

        var filters = new[] { GenericFilters.CustomerRequestState.ToString() };
        var values = Array.Empty<string>();

        // Act
        var result = _service.AddFilter(data, filters, values).ToList();

        // Assert
        Assert.HasCount(2, result);
    }

}