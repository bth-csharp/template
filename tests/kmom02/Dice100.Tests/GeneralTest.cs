using System.Text.RegularExpressions;

namespace Dice100.Tests;

public class GeneralTest
{
    private string _targetDir = "";

    [SetUp]
    public void Setup()
    {
        var workflowDir = Environment.GetEnvironmentVariable("PROJECT_ROOT");

        if (!string.IsNullOrEmpty(workflowDir))
        {
            // Körs i GitHub Actions
            _targetDir = Path.Combine(workflowDir, "kmom02", "Dice100");
        }
        else
        
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var rootDir = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", ".."));
            this._targetDir = Path.Combine(rootDir, "Dice100") ?? "";
        }
    }

    [Test]
    public void RequiredFiles()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var requiredFiles = new[]
        {
            "Program.cs",
            "src/Die.cs",
            "src/Menu.cs",
            "src/Player.cs",
        };

        foreach (var file in requiredFiles)
        {
            var fullPath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(fullPath), Is.True, $"Filen saknas: {file}");
        }
        TestContext.Out.WriteLine("✅ Alla filer för kmom02 finns");
    }
    
    [Test]
    public void RequiredFlowchart()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var fullPath1 = Path.Combine(_targetDir, "..", "flowchart.pdf");
        var fullPath2 = Path.Combine(_targetDir, "..", "flowchart.png");

        bool exists = File.Exists(fullPath1) || File.Exists(fullPath2);

        Assert.That(exists, Is.True, "Flödesdiagram saknas! Har du rätt filnamn?");
        Assert.That(
            (File.Exists(fullPath1) && !string.IsNullOrWhiteSpace(File.ReadAllText(fullPath1))) ||
            (File.Exists(fullPath2) && !string.IsNullOrWhiteSpace(File.ReadAllText(fullPath2))),
            Is.True, "Aj då, tom fil.");

        TestContext.Out.WriteLine("✅ Flödesdiagram finns");
    }

    [Test]
    public void DieContainingThis()
    {
        var filePath = Path.Combine(this._targetDir, "src/Die.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var totalValueCount = Regex.Matches(content, "_value").Count;
        var thisValueCount = Regex.Matches(content, "this\\._value").Count;

        Assert.That(totalValueCount, Is.EqualTo(thisValueCount + 1),
        "_value ska endast användas direkt vid deklarationen — alla andra användningar ska vara this._value.");

        TestContext.Out.WriteLine("✅ Använd this för att referera till attribut _value i klassen Die ok");
    }

    [TestCase("_name")]
    [TestCase("_score")]
    [TestCase("_noOfRounds")]
    public void PlayerContainingThis(string attribut)
    {
        var filePath = Path.Combine(this._targetDir, "src/Player.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var totalCount = Regex.Matches(content, attribut).Count;
        var thisCount = Regex.Matches(content, "this\\." + attribut).Count;

        Assert.That(totalCount, Is.EqualTo(thisCount + 1),
        $"{attribut} ska endast användas direkt vid deklarationen — alla andra användningar ska vara this.{attribut}.");

        TestContext.Out.WriteLine($"✅ Använd this för att referera till {attribut} i klassen Player ok");
    }
}
