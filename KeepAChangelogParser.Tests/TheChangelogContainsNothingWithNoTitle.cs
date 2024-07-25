using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Extensions;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeepAChangelogParser.Tests
{
  [TestClass]
  public class TheChangelogContainsNothingWithNoTitle
  {
    [TestMethod]
    public void WhenTheChangelogContainsNothingWithNoTitle_ThenTheResultIsFailure()
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
        expected: expectedParseResult,
        actual: actualParseResult,
        comparer: ChangelogResultComparerFactory.Create());
    }

    private static string createTextToParse()
    {
      return
        /* 1 */ "# ";
    }

    private static Result<Changelog> createExpectedParseResult()
    {
      Result<Changelog> expectedParseResult =
        Result.Failure<Changelog>(
          "Invalid title. Error parsing text in line 1 / index 2.");

      return expectedParseResult;
    }
  }
}
