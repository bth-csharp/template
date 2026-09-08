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
            "src/Game.cs",
            "src/Menu.cs",
            "src/Round.cs",
        };

        foreach (var file in requiredFiles)
        {
            var fullPath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(fullPath), Is.True, $"Filen saknas: {file}");
        }
        TestContext.Out.WriteLine("✅ Alla filer för kmom02 finns");
    }

    [Test]
    public void CheckIfProgramIsOk()
    {
        var filePath = Path.Combine(this._targetDir, "Program.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();


        Assert.That(content, Does.Contain("Menu menu = "), $"Ett meny objekt ska skapas");
        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("menu."), $"Menyn ska köras igång");
            Assert.That(nonEmptyLines, Has.Count.GreaterThanOrEqualTo(2),
                $"Filen måste innehålla minst 2 strängar, hittade {nonEmptyLines.Count}.");
            Assert.That(nonEmptyLines, Has.Count.LessThanOrEqualTo(4),
                $"Filen måste innehålla max 4 strängar, hittade {nonEmptyLines.Count}.");
        });

        TestContext.Out.WriteLine("✅ Ett objekt av klassen Menu skapas och menyn körs igång");
    }

    [Test]
    public void RequiredClassDiagram()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var fullPath1 = Path.Combine(_targetDir, "..", "classdiagram.pdf");
        var fullPath2 = Path.Combine(_targetDir, "..", "classdiagram.png");

        bool exists = File.Exists(fullPath1) || File.Exists(fullPath2);

        Assert.That(exists, Is.True, "Klassdiagram saknas! Har du rätt filnamn?");
        Assert.That(
            (File.Exists(fullPath1) && !string.IsNullOrWhiteSpace(File.ReadAllText(fullPath1))) ||
            (File.Exists(fullPath2) && !string.IsNullOrWhiteSpace(File.ReadAllText(fullPath2))),
            Is.True, "Aj då, tom fil.");

        TestContext.Out.WriteLine("✅ Klassdiagram finns");
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

        TestContext.Out.WriteLine("✅ Använd this för att referera till attribut i klassen Die ok");
    }

    [Test]
    public void RoundContainingThis()
    {
        var filePath = Path.Combine(this._targetDir, "src/Round.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var totalHandCount = Regex.Matches(content, "_hand").Count;
        var thisHandCount = Regex.Matches(content, "this\\._hand").Count;
        var totalPointCount = Regex.Matches(content, "_points").Count;
        var thisPointCount = Regex.Matches(content, "this\\._points").Count;

        Assert.That(totalHandCount, Is.EqualTo(thisHandCount + 1),
        "_hand ska endast användas direkt vid deklarationen — alla andra användningar ska vara this._hand.");
        Assert.That(totalPointCount, Is.EqualTo(thisPointCount + 1),
        "_points ska endast användas direkt vid deklarationen — alla andra användningar ska vara this._points.");

        TestContext.Out.WriteLine("✅ Använd this för att referera till attribut i klassen Round ok");
    }
}
