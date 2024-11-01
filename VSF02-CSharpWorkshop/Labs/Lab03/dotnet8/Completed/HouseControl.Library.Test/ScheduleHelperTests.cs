using Moq;

namespace HouseControl.Library.Test;

[TestFixture]
public class ScheduleHelperTests
{
    TimeSpan timeZoneOffset = new(-2, 0, 0);

    private void SetCurrentTime(DateTimeOffset currentTime)
    {
        var mockTime = new Mock<ITimeProvider>();
        mockTime.Setup(t => t.Now()).Returns(currentTime);
        ScheduleHelper.TimeProvider = mockTime.Object;
    }

    [TearDown]
    public void Teardown()
    {
        // Reset TimeProvider to the default that uses the real time
        ScheduleHelper.TimeProvider = new CurrentTimeProvider();
    }

    [Test]
    public void MondayItemInPast_OnRolLDay_IsTomorrow()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);

        DateTimeOffset monday = new(2024, 02, 26, 15, 52, 00, timeZoneOffset);
        DateTimeOffset expected = new(thursday.Date.AddDays(1) + monday.TimeOfDay, timeZoneOffset);
        ScheduleInfo info = new()
        {
            EventTime = monday,
            TimeType = ScheduleTimeType.Standard,
        };
        ScheduleHelper helper = new(new FakeSunsetProvider());

        // Act
        DateTimeOffset actual = helper.RollForwardToNextDay(info);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MondayItemInFuture_OnRollDay_IsUnchanged()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);

        DateTimeOffset monday = new(2024, 03, 04, 15, 52, 00, timeZoneOffset);
        DateTimeOffset expected = monday;
        ScheduleInfo info = new()
        {
            EventTime = monday,
            TimeType = ScheduleTimeType.Standard,
        };
        ScheduleHelper helper = new(new FakeSunsetProvider());

        // Act
        DateTimeOffset actual = helper.RollForwardToNextDay(info);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MondayItemInPastAndTodayFriday_OnRollWeekdayDay_IsNextMonday()
    {
        // Arrange
        DateTimeOffset friday = new(2024, 03, 01, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(friday);

        DateTimeOffset monday = new(2024, 02, 26, 15, 52, 00, timeZoneOffset);
        DateTimeOffset expected = new(friday.Date.AddDays(3) + monday.TimeOfDay, timeZoneOffset);
        ScheduleInfo info = new()
        {
            EventTime = monday,
            TimeType = ScheduleTimeType.Standard,
        };
        ScheduleHelper helper = new(new FakeSunsetProvider());

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekdayDay(info);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MondayItemInFutureAndTodayFriday_OnRollWeekdayDay_IsUnchanged()
    {
        // Arrange
        DateTimeOffset friday = new(2024, 03, 01, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(friday);

        DateTimeOffset monday = new(2024, 03, 04, 15, 52, 00, timeZoneOffset);
        DateTimeOffset expected = monday;
        ScheduleInfo info = new()
        {
            EventTime = monday,
            TimeType = ScheduleTimeType.Standard,
        };
        ScheduleHelper helper = new(new FakeSunsetProvider());

        // Act
        DateTimeOffset actual = helper.RollForwardToNextDay(info);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SundayItemInPastAndTodayThursday_OnRollWeekendDay_IsSaturday()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);

        DateTimeOffset sunday = new(2024, 02, 25, 15, 52, 00, timeZoneOffset);
        DateTimeOffset expected = new(thursday.Date.AddDays(2) + sunday.TimeOfDay, timeZoneOffset);
        ScheduleInfo info = new()
        {
            EventTime = sunday,
            TimeType = ScheduleTimeType.Standard,
        };
        ScheduleHelper helper = new(new FakeSunsetProvider());

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekendDay(info);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SaturdayItemInFutureAndTodayFriday_OnRollWeekendDay_IsUnchanged()
    {
        // Arrange
        DateTimeOffset friday = new(2024, 03, 01, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(friday);

        DateTimeOffset saturday = new(2024, 03, 02, 15, 52, 00, timeZoneOffset);
        DateTimeOffset expected = saturday;
        ScheduleInfo info = new()
        {
            EventTime = saturday,
            TimeType = ScheduleTimeType.Standard,
        };
        ScheduleHelper helper = new(new FakeSunsetProvider());

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekendDay(info);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    // Tests for DateTime Helper Methods:
    //    Now
    //    Today
    //    Tomorrow
    //    DurationFromNow
    //    IsInPast
    //    IsInFuture

    [Test]
    public void Now_ReturnsConfiguredTime()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset expected = thursday;

        // Act
        var actual = ScheduleHelper.Now();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Tomorrow_ReturnsConfiguredDatePlusOne()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset expected = new(thursday.Date.AddDays(1), timeZoneOffset);

        // Act
        var actual = ScheduleHelper.Tomorrow();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Today_ReturnsConfiguredDate()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset expected = new(thursday.Date, timeZoneOffset);

        // Act
        var actual = ScheduleHelper.Today();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PastDate_IsInFuture_ReturnsFalse()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset monday = new(2024, 02, 26, 15, 52, 00, timeZoneOffset);

        // Act
        var actual = ScheduleHelper.IsInFuture(monday);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void FutureDate_IsInFuture_ReturnsTrue()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset monday = new(2024, 03, 04, 15, 52, 00, timeZoneOffset);

        // Act
        var actual = ScheduleHelper.IsInFuture(monday);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void PastDate_IsInPast_ReturnsTrue()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset monday = new(2024, 02, 26, 15, 52, 00, timeZoneOffset);

        // Act
        var actual = ScheduleHelper.IsInPast(monday);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void FutureDate_IsInPast_ReturnsFalse()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset monday = new(2024, 03, 04, 15, 52, 00, timeZoneOffset);

        // Act
        var actual = ScheduleHelper.IsInPast(monday);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void Time10MintuesInPast_DurationFromNow_Returns10Minutes()
    {
        // Arrange
        DateTimeOffset thursday = new(2024, 02, 29, 16, 35, 22, timeZoneOffset);
        SetCurrentTime(thursday);
        DateTimeOffset testTime = thursday.AddMinutes(-10);
        TimeSpan expected = new(0, 10, 0);

        // Act
        var actual = ScheduleHelper.DurationFromNow(testTime);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
