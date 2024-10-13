using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Backend.Contexts;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

public class IntervalServiceTests
{
    private readonly Mock<AppDbContext> _mockContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<IntervalService>> _mockLogger;
    private readonly IntervalService _service;
    

    public IntervalServiceTests()
    {
        _mockContext = new Mock<AppDbContext>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<IntervalService>>();
        _service = new IntervalService(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetIntervals_ReturnsIntervals()
    {
        // Arrange
        var pagination = new PaginationDTO { skip = 0, limit = 10 };
        var intervals = new List<Interval> { new Interval { IntervalNo = 1, IntervalName = "Test" } };
        var dbSetMock = new Mock<DbSet<Interval>>();
        dbSetMock.As<IQueryable<Interval>>().Setup(m => m.Provider).Returns(intervals.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Interval>>().Setup(m => m.Expression).Returns(intervals.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Interval>>().Setup(m => m.ElementType).Returns(intervals.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Interval>>().Setup(m => m.GetEnumerator()).Returns(intervals.AsQueryable().GetEnumerator());
        _mockContext.Setup(c => c.Intervals).Returns(dbSetMock.Object);

        // Act
        var result = await _service.GetIntervals(pagination);

        // Assert
        Assert.NotNull(result);
//        Assert.(result);
    }

    [Fact]
    public async Task FindIntervalByID_ReturnsIntervalDTO()
    {
        // Arrange
        var interval = new Interval { IntervalNo = 1, IntervalName = "Test" };
        _mockContext.Setup(c => c.Intervals.Find(It.IsAny<int>())).Returns(interval);
        _mockMapper.Setup(m => m.Map<IntervalDTO>(It.IsAny<Interval>())).Returns(new IntervalDTO { IntervalNo = 1, IntervalName = "Test" });

        // Act
        var result = await _service.findIntervalByID(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equals(1, result.IntervalNo);
    }

    [Fact]
    public void AddInterval_AddsInterval()
    {
        // Arrange
        var intervalDTO = new IntervalDTO { IntervalNo = 1, IntervalName = "Test" };
        var interval = new Interval { IntervalNo = 1, IntervalName = "Test" };
        _mockMapper.Setup(m => m.Map<Interval>(It.IsAny<IntervalDTO>())).Returns(interval);

        // Act
        _service.AddInterval(intervalDTO);

        // Assert
        _mockContext.Verify(c => c.Intervals.Add(It.IsAny<Interval>()), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }


    [Fact]
    public void UpdateInterval_UpdatesInterval()
    {
        // Arrange
        var intervalDTO = new IntervalDTO { IntervalNo = 1, IntervalName = "Test" };
        var interval = new Interval { IntervalNo = 1, IntervalName = "Test" };
        _mockMapper.Setup(m => m.Map<Interval>(It.IsAny<IntervalDTO>())).Returns(interval);

        // Act
        _service.UpdateInterval(intervalDTO);

        // Assert
        _mockContext.Verify(c => c.Intervals.Update(It.IsAny<Interval>()), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void DeleteInterval_DeletesInterval()
    {
        // Arrange
        var interval = new Interval { IntervalNo = 1, IntervalName = "Test" };
        _mockContext.Setup(c => c.Intervals.Find(It.IsAny<int>())).Returns(interval);

        // Act
        _service.DeleteInterval(1);

        // Assert
        _mockContext.Verify(c => c.Intervals.Remove(It.IsAny<Interval>()), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }
    
    [Fact]
    public void GetIntervalOccurrences_ReturnsIntervalDTOs()
    {
        // Arrange
        var pagination = new PaginationDTO { skip = 0, limit = 10 };
        var intervals = new List<Interval> { new Interval { IntervalNo = 1, IntervalName = "Test" } };
        var occurrences = new List<Occurrence> { new Occurrence { EarlyIntervalNo = 1 } };
        var intervalOccurrence = intervals.Join(
            occurrences,
            interval => interval.IntervalNo,
            occurrence => occurrence.EarlyIntervalNo,
            (interval, occurrence) => new { Interval = interval, Occurrence = occurrence }
        ).ToList();

        var dbSetMockIntervals = new Mock<DbSet<Interval>>();
        dbSetMockIntervals.As<IQueryable<Interval>>().Setup(m => m.Provider).Returns(intervals.AsQueryable().Provider);
        dbSetMockIntervals.As<IQueryable<Interval>>().Setup(m => m.Expression).Returns(intervals.AsQueryable().Expression);
        dbSetMockIntervals.As<IQueryable<Interval>>().Setup(m => m.ElementType).Returns(intervals.AsQueryable().ElementType);
        dbSetMockIntervals.As<IQueryable<Interval>>().Setup(m => m.GetEnumerator()).Returns(intervals.AsQueryable().GetEnumerator());

        var dbSetMockOccurrences = new Mock<DbSet<Occurrence>>();
        dbSetMockOccurrences.As<IQueryable<Occurrence>>().Setup(m => m.Provider).Returns(occurrences.AsQueryable().Provider);
        dbSetMockOccurrences.As<IQueryable<Occurrence>>().Setup(m => m.Expression).Returns(occurrences.AsQueryable().Expression);
        dbSetMockOccurrences.As<IQueryable<Occurrence>>().Setup(m => m.ElementType).Returns(occurrences.AsQueryable().ElementType);
        dbSetMockOccurrences.As<IQueryable<Occurrence>>().Setup(m => m.GetEnumerator()).Returns(occurrences.AsQueryable().GetEnumerator());

        _mockContext.Setup(c => c.Intervals).Returns(dbSetMockIntervals.Object);
        _mockContext.Setup(c => c.Occurrences).Returns(dbSetMockOccurrences.Object);
        _mockMapper.Setup(m => m.Map<List<IntervalDTO>>(It.IsAny<List<object>>())).Returns(new List<IntervalDTO> { new IntervalDTO { IntervalNo = 1, IntervalName = "Test" } });

        // Act
        var result = _service.getIntervalOccurrences("Test", pagination);

        // Assert
        Assert.NotNull(result);
        Assert.GreaterOrEqual(result.Count > 0, true);
        Assert.Equals(1, result.First().IntervalNo);
    }
}