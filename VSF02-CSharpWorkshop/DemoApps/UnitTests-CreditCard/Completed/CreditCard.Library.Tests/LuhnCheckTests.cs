namespace CreditCard.Library.Tests;

public class LuhnCheckTests
{
    [TestCase("378282246310005")]
    [TestCase("371449635398431")]
    [TestCase("5555555555554444")]
    [TestCase("5105105105105100")]
    [TestCase("4111111111111111")]
    [TestCase("4012888888881881")]
    [TestCase("6011111111111117")]
    [TestCase("6011000990139424")]
    [TestCase("3530111333300000")]
    [TestCase("3566002020360505")]
    public void PassesLuhnCheck_OnValidNumber_ReturnsTrue(string testNumber)
    {
        // Arrange / Act
        bool actual = LuhnCheck.PassesLuhnCheck(testNumber);

        // Assert
        Assert.That(actual, Is.True);
    }

    [TestCase("9876543210987654")]
    [TestCase("abc")]
    [TestCase("-01233454567")]
    [TestCase("7145551212")]
    [TestCase("123")]
    public void PassesLuhnCheck_OnInvalidNumber_ReturnsFalse(string testNumber)
    {
        bool actual = LuhnCheck.PassesLuhnCheck(testNumber);
        Assert.That(actual, Is.False);
    }
}