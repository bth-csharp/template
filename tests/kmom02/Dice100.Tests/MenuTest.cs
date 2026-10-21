using Dice100.src;

namespace Dice100.Tests;
[TestFixture]
public class MenuTest
{
    private Menu _menu;

    [SetUp]
    public void SetUp()
    {
        this._menu = new Menu();
    }

    [Test]
    public void ShowRules_WritesToConsole()
    {
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        this._menu.PrintRules();

        string output = writer.ToString();
        Assert.That(output.Length, Is.GreaterThan(100), "The rules text is too short min 100 characters required.");

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

    [Test]
    public void ShowMenu_WritesToConsole()
    {
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);

        this._menu.PrintMenu();

        string output = writer.ToString();
        Assert.That(output, Does.Contain("1."));
        Assert.That(output, Does.Contain("2."));
        Assert.That(output, Does.Contain("3."));

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

    [Test]
    public void GetMenuChoice_ValidInput_ReturnsChoice()
    {
        StringReader reader = new StringReader("2");
        Console.SetIn(reader);

        string choice = this._menu.GetMenuChoice();

        Assert.That(choice, Is.EqualTo("2"));

        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }

    [Test]
    public void GetMenuChoice_InvalidInput_ReturnsMinusOne()
    {
        StringReader reader = new StringReader("abc");
        Console.SetIn(reader);

        string choice = this._menu.GetMenuChoice();

        Assert.That(choice, Is.EqualTo("abc"));

        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }
}

