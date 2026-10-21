namespace Dice100.src;

public class PlayerTest
{
    private Player _player;

    [SetUp]
    public void SetUp()
    {
        this._player = new Player("Anna");
    }

    [Test]
    public void TestCreatePlayerWithCorrectName()
    {
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        this._player.PrintInformation();
        string result = writer.ToString();

        Assert.That(result, Does.Contain("Anna"), "The name should be Anna");

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

    [Test]
    public void GetScore_Initially_ReturnsZero()
    {
        Assert.That(this._player.GetScore(), Is.EqualTo(0), "The score should be 0");
    }

    [Test]
    public void GetRounds_Initially_ReturnsZero()
    {
        Assert.That(this._player.GetRounds(), Is.EqualTo(0), "The number of rounds should be 0");
    }

    [Test]
    public void AddScore_AddsPoints()
    {
        this._player.AddScore(15);
        Assert.That(this._player.GetScore(), Is.EqualTo(15), "The score should be 15");
    }

    [Test]
    public void AddScore_MultipleTimes_Accumulates()
    {
        this._player.AddScore(10);
        this._player.AddScore(20);
        Assert.That(this._player.GetScore(), Is.EqualTo(30), "The score should be 30");
    }

    [Test]
    public void AddRound_IncrementsRounds()
    {
        this._player.AddRound();
        Assert.That(this._player.GetRounds(), Is.EqualTo(1), "The number of rounds should be 1");
    }

    [Test]
    public void AddRound_MultipleTimes_Accumulates()
    {
        this._player.AddRound();
        this._player.AddRound();
        this._player.AddRound();
        Assert.That(this._player.GetRounds(), Is.EqualTo(3), "The number of rounds should be 3");
    }
}
