namespace Dice100.Tests;
using Dice100.src;

public class MenuTest
{
    private string _targetDir = "";

    [SetUp]
    public void SetUp()
    {
        var workflowDir = Environment.GetEnvironmentVariable("PROJECT_ROOT");

        if (!string.IsNullOrEmpty(workflowDir))
        {
            // Körs i GitHub Actions
            _targetDir = Path.Combine(workflowDir, "kmom03", "Dice100");
        }
        else
        
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var rootDir = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", ".."));
            this._targetDir = Path.Combine(rootDir, "Dice100") ?? "";
        }
    }

    [Test]
    [Ignore("Menu kan implementeras på olika sätt, test funkar inte med alla lösningar")]
    public void TestInputValues()
    {
        string name = "Marie";
        string noOfDice = "4";
        string title = "Dice100";
        Menu menu = new Menu();

        var input = new StringReader(name + "\n" + noOfDice +"\n");
        Console.SetIn(input);
        var output = new StringWriter();
        Console.SetOut(output);

        menu.InitGame();
        var content = output.ToString() ?? "";

        Assert.That(content, Does.Contain(title), $"Välkomstmeddelandet ska innehålla titeln: {title}.");
        Assert.That(content, Does.Contain(name), $"Välkomstmeddelandet ska innehålla namnet: {name}.");
        Assert.That(content, Does.Contain(noOfDice), $"Välkomstmeddelandet ska innehålla antalet tärningar {noOfDice}.");
        TestContext.Out.WriteLine("✅ Menu: Godkänt valkomstmeddelande med rubrik, namn och antal tärningar");
    }

    [Test]
    [Ignore("Menu kan implementeras på olika sätt, test funkar inte med alla lösningar")]
    public void TestInputNoOfDiesTooHigh()
    {
        string name = "Marie";
        string noOfDice = "4";
        string noOfDiceTooHigh = "14";
        string errorMessage = "vara 10 eller färre";
        Menu menu = new Menu();

        var input = new StringReader(name + "\n" + noOfDiceTooHigh +"\n" + noOfDice + "\n");
        Console.SetIn(input);
        var output = new StringWriter();
        Console.SetOut(output);
        menu.InitGame();

        var content = output.ToString() ?? "";

        Assert.That(content, Does.Contain(errorMessage), $"Felmeddelandet ska innehålla delsträngen: {errorMessage}.");
        Assert.That(content, Does.Contain(noOfDice), $"Antalet tärningar {noOfDice}.");
        TestContext.Out.WriteLine("✅ Menu: Inmatning av antalet tärningar över 10 kastar NumberOfDieException");
    }

    [Test]
    [Ignore("Menu kan implementeras på olika sätt, test funkar inte med alla lösningar")]
    public void TestInputNoOfDiesTooLow()
    {
        string name = "Marie";
        string noOfDice = "5";
        string noOfDiceTooLow = "1";
        string errorMessage = "vara 2 eller fler";
        Menu menu = new Menu();

        var input = new StringReader(name + "\n" + noOfDiceTooLow + "\n" + noOfDice + "\n");
        Console.SetIn(input);
        var output = new StringWriter();
        Console.SetOut(output);
        menu.InitGame();

        var content = output.ToString() ?? "";

        Assert.That(content, Does.Contain(errorMessage), $"Felmeddelandet ska innehålla delsträngen: {errorMessage}.");
        Assert.That(content, Does.Contain(noOfDice), $"Antalet tärningar {noOfDice}.");
        TestContext.Out.WriteLine("✅ Menu: Inmatning av antalet tärningar mindre än 2 kastar NumberOfDieException");
    }

    [Test]
    public void MenuContainingWelcome()
    {
        var filePath = Path.Combine(this._targetDir, "src/Menu.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Match("Välkom|Welcom"),
                    $"Välkomsthälsning saknas");
            Assert.That(content, Does.Match("tärning|Tärning|Di|di"),
                    $"Information om antal tärningar saknas");
        });

        TestContext.Out.WriteLine("✅ Menu: Välkomsthälsning ok");
    }

    [Test]
    public void MenuContainingGame()
    {
        var filePath = Path.Combine(this._targetDir, "src/Menu.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        Assert.That(content, Does.Match(@"\bGame\b"), $"Game objektet saknas");


        TestContext.Out.WriteLine("✅ Menu: innehåller Game ok");
    }

    [Test]
    public void MenuNotContainingRoundNorDie()
    {
        var filePath = Path.Combine(this._targetDir, "src/Menu.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        Assert.That(content, Does.Not.Match(@"\bRound\b"), "Menu får inte referera till klassen Round.");
        Assert.That(content, Does.Not.Match(@"\bDie\b"), "Menu får inte referera till klassen Die.");


        TestContext.Out.WriteLine("✅ Menu: innehåller inte Round och Die ok");
    }
}
