namespace Dice100.src;

[TestFixture]
public class DieTest
{
    private const int MAX_VALUE = 6;
    private const int MIN_VALUE = 1;
    private List<int> _numbers;

    [SetUp]
    public void Setup()
    {
        this._numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
    }

    [Test]
    public void TestCreateDieNoArgument()
    {
        Die die = new Die();
        Assert.That(die.GetValue(), Is.LessThanOrEqualTo(MAX_VALUE), "The value of the Die should be 1 <= value <= 6.");
        Assert.That(die.GetValue(), Is.GreaterThanOrEqualTo(MIN_VALUE), "The value of the Die should be 1 <= value <= 6.");

        TestContext.Out.WriteLine("✅ Konstruktorn i Die utan argument ok");
    }

    [TestCase(5)]
    [TestCase(6)]
    public void TestCreateDieWithArgument(int value)
    {
        Die die = new Die(value);
        Assert.That(die.GetValue(), Is.EqualTo(value), "The value of the Die should be " + value + ".");

        TestContext.Out.WriteLine("✅ Konstruktorn i Die med argument ok");
    }

    [Test]
    public void TestCreateDieWithArgumentTooHigh()
    {
        Die die = new Die(555);
        Assert.That(die.GetValue(), Is.EqualTo(6), "The value of the Die should be 6.");

        TestContext.Out.WriteLine("✅ Konstruktorn i Die med för högt argument ok");
    }

    [Test]
    public void TestCreateDieWithArgumentTooLow0()
    {
        Die die = new Die(0);
        Assert.That(die.GetValue(), Is.EqualTo(1), "The value of the Die should be 1.");

        TestContext.Out.WriteLine("✅ Konstruktorn i Die med för lågt argument ok");
    }

    [Test]
    public void TestCreateDieWithArgumentTooLowNegative()
    {
        Die die = new Die(-5);
        Assert.That(die.GetValue(), Is.EqualTo(1), "The value of the Die should be 1.");

        TestContext.Out.WriteLine("✅ Konstruktorn i Die med negativt argument ok");
    }

    [Test]
    public void TestRollDieTwice()
    {
        Die die = new Die();
        Assert.That(this._numbers.Contains(die.GetValue()), Is.True, "The value of the Die should be 1 <= value <= 6 after Roll.");
        die.Roll();
        Assert.That(this._numbers.Contains(die.GetValue()), Is.True, "The value of the Die should be 1 <= value <= 6 after Roll.");
        die.Roll();
        Assert.That(this._numbers.Contains(die.GetValue()), Is.True, "The value of the Die should be 1 <= value <= 6 after Roll.");

        TestContext.Out.WriteLine("✅ Die: Metoden Roll genererar slumptal ok");
    }

    [TestCase(5)]
    [TestCase(6)]
    public void TestGetString(int value)
    {
        Die die = new Die(value);
        Assert.That(die.GetString(), Is.EqualTo(value.ToString()), "The value of the Die should be " + value + ".");

        TestContext.Out.WriteLine("✅ Die: Metoden GetString funkar ok");
    }
}
