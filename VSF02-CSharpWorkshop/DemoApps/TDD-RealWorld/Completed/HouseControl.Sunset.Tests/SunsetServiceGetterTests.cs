using Moq;

namespace HouseControl.Sunset.Tests;

public class SunsetServiceGetterTests
{
    string goodData = "{\"results\":{\"sunrise\":\"7:52:28 AM\",\"sunset\":\"6:53:14 PM\",\"solar_noon\":\"1:22:51 PM\",\"day_length\":\"11:00:46.4065871\"},\"status\":\"OK\"}";
    string badData = "{\"results\":null,\"status\":\"ERROR\"}";

    [Test]
    public void SunsetGetter_ImplementsInterface()
    {
        var getter = new ServiceSunsetGetter();
        Assert.That(getter, Is.InstanceOf<ISunsetGetter>());
    }

    [Test]
    public void ToLocalTime_OnValidTimeString_ReturnsExpectedDateTime()
    {
        // Arrange
        var getter = new ServiceSunsetGetter();
        string timeString = "6:53:14 PM";
        DateOnly date = new DateOnly(2022, 10, 20);
        DateTimeOffset expected = new DateTime(2022, 10, 20, 18, 53, 14);

        // Act
        DateTimeOffset result = getter.ToLocalTime(date, timeString);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunsetTime_OnValidData_ReturnsExpectedTimeString()
    {
        var getter = new ServiceSunsetGetter();
        string expected = "6:53:14 PM";

        string result = getter.ParseSunsetTime(goodData);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunsetTime_OnErrorData_ThrowsArgumentException()
    {
        var getter = new ServiceSunsetGetter();

        try
        {
            getter.ParseSunsetTime(badData);
            Assert.Fail("ArgumentException not thrown.");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception other than ArgumentException thrown: {ex.GetType()}");
        }
    }

    [Test]
    public void CheckStatus_OnValidStatus_ReturnsTrue()
    {
        var getter = new ServiceSunsetGetter();
        bool result = getter.CheckStatus(goodData);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckStatus_OnErrorStatus_ReturnsFalse()
    {
        var getter = new ServiceSunsetGetter();
        bool result = getter.CheckStatus(badData);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetSunset_OnValidDate_ReturnsExpectedDateTime()
    {
        Mock<ISolarService> serviceMock = new();
        serviceMock.Setup(s => s.GetServiceData(It.IsAny<DateOnly>()))
            .Returns(Task.FromResult(goodData));

        var getter = new ServiceSunsetGetter();
        getter.Service = serviceMock.Object;

        DateOnly date = new DateOnly(2022, 10, 20);
        DateTimeOffset expected = new DateTime(2022, 10, 20, 18, 53, 14);

        DateTimeOffset result = await getter.GetSunset(date);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunriseTime_OnValidData_ReturnsExpectedTimeString()
    {
        var getter = new ServiceSunsetGetter();
        string expected = "7:52:28 AM";

        string result = getter.ParseSunriseTime(goodData);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunriseTime_OnErrorData_ThrowsArgumentException()
    {
        var getter = new ServiceSunsetGetter();

        try
        {
            getter.ParseSunriseTime(badData);
            Assert.Fail("ArgumentException not thrown.");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception other than ArgumentException thrown: {ex.GetType()}");
        }
    }

    [Test]
    public async Task GetSunrise_OnValidDate_ReturnsExpectedDateTime()
    {
        Mock<ISolarService> serviceMock = new();
        serviceMock.Setup(s => s.GetServiceData(It.IsAny<DateOnly>()))
            .Returns(Task.FromResult(goodData));

        var getter = new ServiceSunsetGetter();
        getter.Service = serviceMock.Object;

        DateOnly date = new DateOnly(2022, 10, 20);
        DateTimeOffset expected = new DateTime(2022, 10, 20, 07, 52, 28);

        DateTimeOffset result = await getter.GetSunrise(date);

        Assert.That(result, Is.EqualTo(expected));
    }
}