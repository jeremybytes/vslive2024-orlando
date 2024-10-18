namespace HouseControl.Library.Test;

[TestFixture]
public class ScheduleTests
{
    readonly string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\ScheduleData";

    [Test]
    public void ScheduleItems_OnCreation_IsPopulated()
    {
        // Arrange / Act

        // Assert
        Assert.Inconclusive();
    }

    [Test]
    public void ScheduleItems_OnCreation_AreInFuture()
    {
        // Arrange / Act

        // Assert
        Assert.Inconclusive();
    }

    [Test]
    public void ScheduleItems_AfterRoll_AreInFuture()
    {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();
    }

    [Test]
    public void OneTimeItemInPast_AfterRoll_IsRemoved()
    {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();
    }

    [Test]
    public void OneTimeItemInFuture_AfterRoll_IsStillThere()
    {
        // Arrange

        // Act

        // Assert
        Assert.Inconclusive();
    }
}
