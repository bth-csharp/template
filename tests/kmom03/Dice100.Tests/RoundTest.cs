namespace Dice100.src;

public class RoundTest
{
    private List<int> _numbers;

    [SetUp]
    public void Setup()
    {
        this._numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
    }

    [Test]
    public void TestCreateRoundCheck5Dice()
    {
        Round round = new Round();
        Assert.That(round.HandToList().Count, Is.EqualTo(5), "The number of dice in a Round object should be 5.");
    }

    [Test]
    public void TestCreateRound()
    {
        Round round = new Round();
        foreach (var die in round.HandToList())
        {
            Assert.That(this._numbers.Contains(die), Is.True, "The values of the dice in a Round object should be 1 <= value <= 6.");
        }

        TestContext.Out.WriteLine("✅ Konstruktorn i Round utan argument ok");
    }

    [Test]
    public void TestCreateRoundCheckWithArguments()
    {
        List<int> values = new List<int> { 6, 6, 6, 6, 5 };
        Round round = new Round(values);
        List<int> roundValues = round.HandToList();

        for (int i = 0; i < roundValues.Count; i++)
        {
            Assert.That(values[i], Is.EqualTo(roundValues[i]), "The values of the dice in a Round object should be { 6, 6, 6, 6, 5 } when using HandToList.");
        }

        TestContext.Out.WriteLine("✅ Konstruktorn i Round med argument ok");
    }

    [Test]
    public void TestRollRound()
    {
        Round round = new Round();

        round.Roll();
        foreach (var die in round.HandToList())
        {
            Assert.That(this._numbers.Contains(die), Is.True, "The values of the dice in a Round object should be 1 <= value <= 6 after Roll.");
        }

        TestContext.Out.WriteLine("✅ Round: Metoden Roll utan argument funkar ok");
    }

    [Test]
    public void TestRollRoundWithArguments()
    {
        List<int> values = new List<int> { 6, 6, 6, 6, 5 };
        List<int> diceToRoll = new List<int> { 0, 2, 4 };
        Round round = new Round(values);
        foreach (var die in round.HandToList())
        {
            Assert.That(this._numbers.Contains(die), Is.True);
        }
        List<int> roundValues = round.HandToList();
        Assert.That(values[1], Is.EqualTo(roundValues[1]), "The value of the dice should be 6 after Roll.");
        Assert.That(values[3], Is.EqualTo(roundValues[3]), "The value of the dice should be 6 after Roll.");

        TestContext.Out.WriteLine("✅ Round: Metoden Roll med argument funkar ok");
    }

    [Test]
    public void TestGetPoints1()
    {
        List<int> values = new List<int> { 6, 6, 6, 6, 5 };
        Round round = new Round(values);
        Assert.That(round.GetPoints(), Is.EqualTo(29), "The sum of the dice should be 29.");

        TestContext.Out.WriteLine("✅ Round: Metoden GetPoints med argument utan en 1:a funkar ok");
    }

    [Test]
    public void TestGetPoints2()
    {
        List<int> values = new List<int> { 0, 1, 1, 1, 10 };
        Round round = new Round(values);
        Assert.That(round.GetPoints(), Is.EqualTo(0), "The sum of the dice should be 0 since one value is a 1.");

        TestContext.Out.WriteLine("✅ Round: Metoden GetPoints med argument med en 1:a funkar ok");
    }

    [Test]
    public void TestClearPoints()
    {
        List<int> values = new List<int> { 6, 6, 6, 6, 5 };
        Round round = new Round(values);
        Assert.That(round.GetPoints(), Is.EqualTo(29), "The sum of the dice should be 29.");
        round.ClearPoints();
        Assert.That(round.GetPoints(), Is.EqualTo(0), "The sum of the dice should be 0.");

        TestContext.Out.WriteLine("✅ Round: Metoden ClearPoints funkar ok");
    }

    [Test]
    public void TestHandToList()
    {
        Round round = new Round();
        List<int> roundInts = round.HandToList();

        foreach (var no in roundInts)
        {
            Assert.That(no, Is.TypeOf<int>());
            Assert.That(this._numbers.Contains(no), Is.True, "The values of the dice in a Round object should be 1 <= value <= 6 when using HandToList.");
        }

        TestContext.Out.WriteLine("✅ Round: Metoden HandToList utan argument funkar ok");
    }

    [Test]
    public void TestHandToListWithArguments()
    {
        List<int> values = new List<int> { 0, 1, 1, 1, 10 };
        List<int> correctValues = new List<int> { 1, 1, 1, 1, 6 };
        Round round = new Round(values);
        List<int> roundInts = round.HandToList();

        for (int i = 0; i < values.Count; i++)
        {
            Assert.That(correctValues[i], Is.EqualTo(roundInts[i]), "The value of the dice should be 1 <= value <= 6. Correct value is " + correctValues[i] + ".");
        }

        TestContext.Out.WriteLine("✅ Round: Metoden HandToList med argument funkar ok");
    }
}
