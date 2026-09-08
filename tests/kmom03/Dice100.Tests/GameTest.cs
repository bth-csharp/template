namespace Dice100.Tests;
using Dice100.src;

public class GameTest
{
    Game _game;

    [SetUp]
    public void SetUp()
    {
        this._game = new Game();
    }

    [Test]
    public void TestInputValues()
    {
        string name = "Marie";
        int noOfDice = 4;

        _game.SetGameValues(noOfDice, name);

        Assert.That(_game.SetGameValues(noOfDice, name), Is.True, $"SetGameValue ska vara en sträng och ett 2<= tal <=10.");
        TestContext.Out.WriteLine("✅ Game: SetGameValue, inputs är ok");
    }

    [Test]
    public void TestInputNoOfDiesTooHigh()
    {
        int noOfDiceTooHigh = 14;
        string name = "Marie";

        Assert.Throws<NumberOfDieException>(() => _game.SetGameValues(noOfDiceTooHigh, name));
        TestContext.Out.WriteLine("✅ Game: SetGameValue, NumberOfDieException kastas när antalet tärningar är större än 10");
    }

    [Test]
    public void TestInputNoOfDiesTooLow()
    {
        string name = "Marie";
        int noOfDiceTooLow = 1;

        Assert.Throws<NumberOfDieException>(() => _game.SetGameValues(noOfDiceTooLow, name));
        TestContext.Out.WriteLine("✅ Game: SetGameValue, NumberOfDieException kastas när antalet tärningar är mindre än 2");
    }
}
