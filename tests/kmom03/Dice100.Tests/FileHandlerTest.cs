namespace Die100.Tests;
using Dice100.src;
using System.Reflection;

[TestFixture]
public class FileHandlerTest
{
    private const string READ_FILE1 = @"TestData/testReadFile1.txt";
    private const string READ_FILE2 = @"TestData/testReadFile2.txt";
    private const string APPEND_FILE = @"TestData/testAppendFile.txt";
    private const string WRITE_FILE = @"TestData/testWriteFile.txt";
    private const string CORRECT_LINE1 = "5, Testperson1, 103, 15";
    private const string CORRECT_LINE2 = "3, Testperson2, 108, 25";
    private const string CORRECT_LINE3 = "3, Testperson3, 103, 16";
    private string _testDirectory = "";
    private string _targetDir = "";

    [OneTimeSetUp]
    public void SetupBeforeTests()
    {
        var workflowDir = Environment.GetEnvironmentVariable("PROJECT_ROOT");

        this._testDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "./";
        var filePath = Path.Combine(_testDirectory, WRITE_FILE);
        File.Delete(filePath);
        if (!string.IsNullOrEmpty(workflowDir))
        {
            // Körs i GitHub Actions
            _targetDir = Path.Combine(workflowDir, "kmom03", "Dice100") ?? "";
        }
        else
        
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var rootDir = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", ".."));
            this._targetDir = Path.Combine(rootDir, "Dice100") ?? "";
        }
    }

    [OneTimeTearDown]
    public void TearDownAfterTests()
    {
        var directoryPath = Path.Combine(_testDirectory, "TestData");
        Directory.Delete(directoryPath, true);
    }

    [Test]
    public void ReadAllTextIsStatic()
    {
        var filePath = Path.Combine(this._targetDir, "src", "FileHandler.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        Assert.That(content, Does.Contain("public static string[] ReadFromFile("),
                    $"ReadFromFile ska vara statisk.");
        TestContext.Out.WriteLine("✅ FileHandler: ReadFromFile är statisk");
    }

    [Test]
    public void SaveToFileIsStatic()
    {
        var filePath = Path.Combine(this._targetDir, "src", "FileHandler.cs");

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        Assert.That(content, Does.Contain("public static void SaveToFile("),
                    $"SaveToFile ska vara statisk.");
        TestContext.Out.WriteLine("✅ FileHandler: SaveToFile är statisk");
    }

    [Test]
    public void TestReadLineCorrect()
    {
        string filePath = Path.Combine(_testDirectory, READ_FILE1);
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), READ_FILE1, SearchOption.AllDirectories);
        string[] fileLines = FileHandler.ReadFromFile(files[0]);
        string[] content = CORRECT_LINE1.Split(",");
        string message = $"Test that your FileHandler can read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";

        Assert.That(fileLines, Has.Length.EqualTo(1));
        Assert.That(fileLines[0], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        TestContext.Out.WriteLine("✅ FileHandler: ReadFromFile funkar bra med en rad");
    }

    [Test]
    public void TestReadLinesCorrect()
    {
        var filePath = Path.Combine(this._testDirectory, READ_FILE2);
        string[] fileLines = FileHandler.ReadFromFile(filePath);
        string[] content = CORRECT_LINE1.Split(",");
        string message = $"Test that your FileHandler can read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";

        Assert.That(fileLines, Has.Length.EqualTo(3));
        Assert.That(fileLines[0], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        content = CORRECT_LINE2.Split(",");
        message = $"Test that your FileHandler can read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";
        Assert.That(fileLines[1], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        content = CORRECT_LINE3.Split(",");
        message = $"Test that your FileHandler can read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";
        Assert.That(fileLines[2], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        TestContext.Out.WriteLine("✅ FileHandler: ReadFromFile funkar bra med flera rader");
    }

    [Test]
    public void TestWriteLineCorrect()
    {
        var filePath = Path.Combine(this._testDirectory, WRITE_FILE);
        string[] fileLines = [CORRECT_LINE1];
        FileHandler.SaveToFile(filePath, fileLines);

        string result = File.ReadAllText(filePath).Trim();
        string[] content = CORRECT_LINE1.Split(",");
        string message = $"Test that your FileHandler can write and read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";

        Assert.That(result.Length, Is.EqualTo(23));
        Assert.That(result, Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        TestContext.Out.WriteLine("✅ FileHandler: SaveToFile funkar bra med en rad");
    }

    [Test]
    [Ignore("FileHandler ska kunna antingen append eller write, inte båda just nu")]
    public void TestAppendLinesCorrect()
    {
        var filePath = Path.Combine(this._testDirectory, APPEND_FILE);
        string[] fileLines = [CORRECT_LINE2, CORRECT_LINE3];
        FileHandler.SaveToFile(filePath, fileLines);

        string[] resultLines = File.ReadAllLines(filePath);
        string[] content = CORRECT_LINE1.Split(",");
        string message = $"Test that your FileHandler can append and read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";

        Assert.That(resultLines, Has.Length.EqualTo(3));
        Assert.That(resultLines[0], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        content = CORRECT_LINE2.Split(",");
        message = $"Test that your FileHandler can append and read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";
        Assert.That(resultLines[1], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        content = CORRECT_LINE3.Split(",");
        message = $"Test that your FileHandler can append and read and contains {content[0]}, {content[1]}, {content[2]} and {content[3]}";
        Assert.That(resultLines[2], Does.Contain(content[0]).And.Contain(content[1]).And.Contain(content[2]).And.Contain(content[3]), message);
        TestContext.Out.WriteLine("✅ FileHandler: SaveToFile funkar bra med flera rader");
    }

    [Test]
    public void TestWriteLinesCorrect()
    {
        var filePath = Path.Combine(this._testDirectory, APPEND_FILE);
        string[] fileLines = [CORRECT_LINE1, CORRECT_LINE2, CORRECT_LINE3];
        FileHandler.SaveToFile(filePath, fileLines);

        string[] resultLines = File.ReadAllLines(filePath);
        string message = $"Test that your FileHandler can write or append to file and should contain {CORRECT_LINE1}, {CORRECT_LINE2} and {CORRECT_LINE3}";
        
        Assert.That(resultLines, Has.Length.EqualTo(3), "Filen ska bara innehålla 3 rader.");
        Assert.That(resultLines[0], Does.Contain(CORRECT_LINE1).Or.Contain(CORRECT_LINE2).Or.Contain(CORRECT_LINE3), message);
        Assert.That(resultLines[1], Does.Contain(CORRECT_LINE1).Or.Contain(CORRECT_LINE2).Or.Contain(CORRECT_LINE3), message);
        Assert.That(resultLines[2], Does.Contain(CORRECT_LINE1).Or.Contain(CORRECT_LINE2).Or.Contain(CORRECT_LINE3), message);
        TestContext.Out.WriteLine("✅ FileHandler: SaveToFile funkar bra med flera rader");

    }
}
