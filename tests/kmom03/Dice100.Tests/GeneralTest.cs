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
    public void RequiredFiles()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var requiredFiles = new[]
        {
            "Program.cs",
            "toplist.txt",
            "src/AsciiArt.cs",
            "src/Die.cs",
            "src/FileHandler.cs",
            "src/Game.cs",
            "src/Menu.cs",
            "src/NumberOfDieException.cs",
            "src/Round.cs",
        };

        foreach (var file in requiredFiles)
        {
            var fullPath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(fullPath), Is.True, $"Filen saknas: {file}");
        }
        TestContext.Out.WriteLine("✅ Alla filer för kmom03 finns");
    }

    [Test]
    public void TextFileContainingAtLeastThreeStrings()
    {
        var filePath = Path.Combine(this._targetDir, "toplist.txt");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var lines = File.ReadAllLines(filePath);
        var nonEmptyLines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

        Assert.That(nonEmptyLines, Has.Count.GreaterThanOrEqualTo(3),
            $"Filen måste innehålla minst 3 strängar, hittade {nonEmptyLines.Count}.");
        TestContext.Out.WriteLine("✅ Filen toplist.txt innehåller minst 3 rader");
    }


    [Test]
    public void FileContainingNumberOfDieException()
    {
        var filePath = Path.Combine(this._targetDir, "src/NumberOfDieException.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("public class NumberOfDieException : Exception"),
                    $"Filen måste innehålla klassen 'NumberOfDieException'.");
            Assert.That(content, Does.Contain("public NumberOfDieException("),
                $"Filen måste innehålla metoden 'NumberOfDieException'.");
        });

        TestContext.Out.WriteLine("✅ NumberOfDieException är deklarerad ok");
    }

    [Test]
    public void FilesNotContainingIO()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var requiredFiles = new[]
        {
            "src/Die.cs",
            "src/Game.cs",
            "src/NumberOfDieException.cs",
            "src/Round.cs",
        };

        foreach (var file in requiredFiles)
        {
            var filePath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {file}");
            var content = File.ReadAllText(filePath) ?? "";
            bool hasForbiddenWords = content.Contains("Console.Write") || content.Contains("Console.Read");
            Assert.That(hasForbiddenWords, Is.False,
            $"Filen {file} får INTE innehålla in- och utmatning.");
        }
        TestContext.Out.WriteLine("✅ Bara Menu och AsciArt innehåller I/O");
    }

    [Test]
    public void FilesContainingIO()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var requiredFiles = new[]
        {
            "src/AsciiArt.cs",
            "src/Menu.cs"
        };

        foreach (var file in requiredFiles)
        {
            var filePath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {file}");
            var content = File.ReadAllText(filePath) ?? "";
            Assert.That(content, Does.Contain("Console.Write"),
            $"Filen {file} får innehålla in- och utmatning.");
        }
        TestContext.Out.WriteLine("✅ Menu och AsciArt innehåller I/O");
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

        TestContext.Out.WriteLine("✅ Klassdiagram finns");
    }
}
