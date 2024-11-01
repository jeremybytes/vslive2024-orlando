namespace HouseControl.Library.Test;

[TestFixture]
public class ScheduleTests
{
    readonly string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\ScheduleData";

    [Test]
    public void ScheduleItems_OnCreation_IsPopulated()
    {
        // Arrange / Act
        var schedule = new Schedule(fileName, new FakeSunsetProvider());

        // Assert
        Assert.That(schedule.Count, Is.GreaterThan(0));
    }

    [Test]
    public void ScheduleItems_OnCreation_AreInFuture()
    {
        // Arrange / Act
        var schedule = new Schedule(fileName, new FakeSunsetProvider());
        var currentTime = DateTimeOffset.Now;

        // Assert
        foreach (var item in schedule)
        {
            Assert.That(item.Info.EventTime, Is.GreaterThan(currentTime));
        }
    }

    [Test]
    public void ScheduleItems_AfterRoll_AreInFuture()
    {
        // Arrange
        var schedule = new Schedule(fileName, new FakeSunsetProvider());
        var currentTime = DateTimeOffset.Now;
        foreach(var item in schedule)
        {
            item.Info.EventTime = item.Info.EventTime - TimeSpan.FromDays(30);
            Assert.That(item.Info.EventTime, Is.LessThan(currentTime),
                "Invalid Arrangement");
        }

        // Act
        schedule.RollSchedule();

        // Assert
        foreach (var item in schedule)
        {
            Assert.That(item.Info.EventTime, Is.GreaterThan(currentTime));
        }
    }

    [Test]
    public void OneTimeItemInPast_AfterRoll_IsRemoved()
    {
        // Arrange
        var schedule = new Schedule(fileName, new FakeSunsetProvider());
        var originalCount = schedule.Count;

        var newItem = new ScheduleItem(
            1,
            DeviceCommand.On,
            new ScheduleInfo()
            {
                EventTime = DateTimeOffset.Now.AddMinutes(-2),
                Type = ScheduleType.Once,
            },
            true,
            ""
        );
        schedule.Add(newItem);

        var newCount = schedule.Count;
        Assert.That(newCount, Is.EqualTo(originalCount + 1),
            "Invalid Arrangement");

        // Act
        schedule.RollSchedule();

        // Assert
        Assert.That(schedule.Count, Is.EqualTo(originalCount));
    }

    [Test]
    public void OneTimeItemInFuture_AfterRoll_IsStillThere()
    {
        // Arrange
        var schedule = new Schedule(fileName, new FakeSunsetProvider());
        var originalCount = schedule.Count;

        var newItem = new ScheduleItem(
            1,
            DeviceCommand.On,
            new ScheduleInfo()
            {
                EventTime = DateTimeOffset.Now.AddMinutes(+2),
                Type = ScheduleType.Once,
            },
            true,
            ""
        );
        schedule.Add(newItem);

        var newCount = schedule.Count;
        Assert.That(newCount, Is.EqualTo(originalCount + 1),
            "Invalid Arrangement");

        // Act
        schedule.RollSchedule();

        // Assert
        Assert.That(schedule.Count, Is.EqualTo(newCount));
    }
}
