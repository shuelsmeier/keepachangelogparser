using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Extensions;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeepAChangelogParser.Tests
{
  [TestClass]
  public class TheChangelogContainsNothing
  {
    [TestMethod]
    public void WhenTheChangelogContainsNothing_ThenTheResultIsCorrect()
    {
      // Arrange
      string textToParse =
        createTextToParse();

      IChangelogParser changelogParser =
        new ChangelogParser();

      // Act
      Result<Changelog> actualParseResult =
        changelogParser.Parse(
          textToParse);

      // Assert
      Result<Changelog> expectedParseResult =
        createExpectedParseResult();

      Assert.That.AreEqual(
        expectedParseResult,
        actualParseResult,
        ChangelogResultComparerFactory.Create());
    }

    private static string createTextToParse()
    {
      return
        "# Changelog";
    }

    private static Result<Changelog> createExpectedParseResult()
    {
      Result<Changelog> expectedParseResult =
        Result.Success(
          new Changelog()
          {
            MarkdownTitle =
              "Changelog",
          });

      return expectedParseResult;
    }
  }
}
