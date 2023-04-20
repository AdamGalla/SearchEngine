using DataFormatter.FormatterLogic.Model;
using DataFormatter.FormatterLogic.Strategies;
using Newtonsoft.Json;

namespace TestDataFormatter.Tests;
internal class TestJsonFormatter
{
    [Test]
    public async Task FormatText_ReturnsValidJSONString()
    {
        // Arrange
        var formatter = new JSONFormatterStrategy();
        var fileData = new FileData
        {
            SearchWord = "test",
            DocIds = new List<KeyValuePair<int, int>> { new KeyValuePair<int, int>(1, 2), new KeyValuePair<int, int>(3, 4) },
            Top10 = new List<int> { 1, 2, 3 },
            Top10Details = new List<string> { "/path/to/file1", "/path/to/file2" },
            Used = TimeSpan.FromSeconds(5)
        };

        // Act
        var formattedText = await formatter.FormatTextAsync(fileData);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(formattedText, Is.Not.Null.Or.Empty);
            Assert.That(() => JsonConvert.DeserializeObject(formattedText), Throws.Nothing);
        });
    }

    [Test]
    public async Task FormatText_ReturnsEmptyString_WhenFileDataIsNull()
    {
        // Arrange
        var formatter = new JSONFormatterStrategy();

        // Act
        var formattedText = await formatter.FormatTextAsync(null);

        // Assert
        Assert.That(formattedText, Is.Empty);
    }
}
