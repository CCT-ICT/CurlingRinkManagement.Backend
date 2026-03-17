using Moq;

using CurlingRinkManagement.Planner.Business.Services;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Tests.Business;

[TestClass]
public class ActivityServiceTests
{
    private Mock<IClubRepository<Activity>> _activityRepositoryMock;
    private Mock<IClubRepository<SheetActivity>> _sheetActivityRepositoryMock;
    private ActivityService _service;

    private Guid _clubId = Guid.NewGuid();

    [TestInitialize]
    public void Setup()
    {
        _activityRepositoryMock = new Mock<IClubRepository<Activity>>();
        _sheetActivityRepositoryMock = new Mock<IClubRepository<SheetActivity>>();

        _activityRepositoryMock.Setup(r => r.GetClubId()).Returns(_clubId);

        _service = new ActivityService(
            _activityRepositoryMock.Object,
            _sheetActivityRepositoryMock.Object
        );
    }

    [TestMethod]
    public void Create_ShouldAssignClubIds_ToSheetActivities()
    {
        // Arrange
        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    ActivityTime = new DateTimeRange()
                }
            }
        };

        _activityRepositoryMock
            .Setup(r => r.Create(It.IsAny<Activity>()))
            .Returns((Activity a) => a);

        // Act
        var result = _service.Create(activity);

        // Assert
        Assert.AreEqual(_clubId, result.SheetActivities.First().ClubId);
        Assert.AreEqual(_clubId, result.SheetActivities.First().ActivityTime.ClubId);
    }

    [TestMethod]
    public void AddInstructor_ShouldAddInstructor_WhenNotAlreadyPresent()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var sheetActivityId = Guid.NewGuid();

        var sheetActivity = new SheetActivity
        {
            Id = sheetActivityId,
            ClubId = _clubId,
            LinkedInstructors = new List<LinkedInstructor>()
        };

        var activity = new Activity
        {
            Id = activityId,
            SheetActivities = new List<SheetActivity> { sheetActivity }
        };

        var activities = new List<Activity> { activity }.AsQueryable();

        _activityRepositoryMock.Setup(r => r.GetAll()).Returns(activities);
        _sheetActivityRepositoryMock.Setup(r => r.Update(It.IsAny<SheetActivity>()));

        // Act
        _service.AddInstructor("user1", activityId, sheetActivityId);

        // Assert
        Assert.HasCount(1, sheetActivity.LinkedInstructors);
        Assert.AreEqual("user1", sheetActivity.LinkedInstructors.First().UserIdentity);
    }

    [TestMethod]
    public void AddInstructor_ShouldThrow_WhenInstructorAlreadyExists()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var sheetActivityId = Guid.NewGuid();

        var sheetActivity = new SheetActivity
        {
            Id = sheetActivityId,
            ClubId = _clubId,
            LinkedInstructors = new List<LinkedInstructor>
            {
                new LinkedInstructor { UserIdentity = "user1" }
            }
        };

        var activity = new Activity
        {
            Id = activityId,
            SheetActivities = new List<SheetActivity> { sheetActivity }
        };

        _activityRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(new List<Activity> { activity }.AsQueryable());

        // Act
        Assert.ThrowsExactly<Exception>(() => _service.AddInstructor("user1", activityId, sheetActivityId));

    }

    [TestMethod]
    public void AddInstructor_ShouldThrow_WhenSheetActivityNotFound()
    {
        // Arrange
        var activityId = Guid.NewGuid();

        var activity = new Activity
        {
            Id = activityId,
            SheetActivities = new List<SheetActivity>()
        };

        _activityRepositoryMock
            .Setup(r => r.GetAll())
            .Returns(new List<Activity> { activity }.AsQueryable());

        // Act
        Assert.ThrowsExactly<KeyNotFoundException>(() => _service.AddInstructor("user1", activityId, Guid.NewGuid()));
    }

    [TestMethod]
    public void AddFilter_ShouldFilterBySheetId()
    {
        // Arrange
        var sheetId = Guid.NewGuid();

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    SheetId = sheetId
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "SheetId" };
        var values = new[] { sheetId.ToString() };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void AddFilter_ShouldExcludeWithWrongSheetId()
    {
        // Arrange
        var sheetId = Guid.NewGuid();

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    SheetId = Guid.NewGuid()
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "SheetId" };
        var values = new[] { sheetId.ToString() };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(0, result);
    }


    [TestMethod]
    public void AddFilter_ShouldFilterByInstructorId()
    {
        // Arrange
        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    LinkedInstructors = new List<LinkedInstructor>
                    {
                        new LinkedInstructor { UserIdentity = "instructor1" }
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "InstructorId" };
        var values = new[] { "instructor1" };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void AddFilter_ShouldExcludeWithWrongInstructorId()
    {
        // Arrange
        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    LinkedInstructors = new List<LinkedInstructor>
                    {
                        new LinkedInstructor { UserIdentity = "instructor2" }
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "InstructorId" };
        var values = new[] { "instructor1" };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void AddFilter_ShouldFilterByStartDate()
    {
        // Arrange
        var start = DateTime.UtcNow;

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    ActivityTime = new DateTimeRange
                    {
                        Start = start.AddHours(1),
                        End = start.AddHours(2)
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "StartDate" };
        var values = new[] { start.ToString("O") };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void AddFilter_ShouldIncludeWithEndAfterStartDate()
    {
        // Arrange
        var start = DateTime.UtcNow;

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    ActivityTime = new DateTimeRange
                    {
                        Start = start.AddHours(-2),
                        End = start.AddHours(1)
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "StartDate" };
        var values = new[] { start.ToString("O") };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void AddFilter_ShouldExcludeBeforeStartDate()
    {
        // Arrange
        var start = DateTime.UtcNow;

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    ActivityTime = new DateTimeRange
                    {
                        Start = start.AddHours(-2),
                        End = start.AddHours(-1)
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "StartDate" };
        var values = new[] { start.ToString("O") };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(0, result);
    }


    [TestMethod]
    public void AddFilter_ShouldFilterByEndDate()
    {
        // Arrange
        var end = DateTime.UtcNow;

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    ActivityTime = new DateTimeRange
                    {
                        Start = end.AddHours(-2),
                        End = end.AddHours(-1)
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "EndDate" };
        var values = new[] { end.ToString("O") };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void AddFilter_ShouldExcludeAfterEndDate()
    {
        // Arrange
        var end = DateTime.UtcNow;

        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    ActivityTime = new DateTimeRange
                    {
                        Start = end.AddHours(1),
                        End = end.AddHours(2)
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "EndDate" };
        var values = new[] { end.ToString("O") };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(0, result);
    }

    [TestMethod]
    public void AddFilter_ShouldReturnActivitiesWithMissingInstructors()
    {
        // Arrange
        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    AmountOfInstructors = 2,
                    LinkedInstructors = new List<LinkedInstructor>
                    {
                        new LinkedInstructor { UserIdentity = "inst1" }
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "MissingInstructors" };
        var values = new[] { "true" };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void AddFilter_ShouldExcludeActivitiesWithoutMissingInstructors()
    {
        // Arrange
        var activity = new Activity
        {
            SheetActivities = new List<SheetActivity>
            {
                new SheetActivity
                {
                    AmountOfInstructors = 1,
                    LinkedInstructors = new List<LinkedInstructor>
                    {
                        new LinkedInstructor { UserIdentity = "inst1" }
                    }
                }
            }
        };

        var query = new List<Activity> { activity }.AsQueryable();

        var filters = new[] { "MissingInstructors" };
        var values = new[] { "true" };

        // Act
        var result = _service.AddFilter(query, filters, values).ToList();

        // Assert
        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void AddFilter_ShouldReturnUnchanged_WhenFilterLengthMismatch()
    {
        // Arrange
        var query = new List<Activity>().AsQueryable();

        var filters = new[] { "SheetId" };
        var values = new[] { "value", "extra" };

        // Act
        var result = _service.AddFilter(query, filters, values);

        // Assert
        Assert.AreEqual(query, result);
    }
}