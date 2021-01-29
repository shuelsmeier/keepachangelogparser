using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Extensions;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeepAChangelogParser.Tests
{

  [TestClass]
  public class TheChangelogContainsNothingWithInvalidHeadingOne
  {

    [TestMethod]
    public void WhenTheChangelogContainsNothingWithInvalidHeadingOne_ThenTheResultIsFailure()
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
        /* 1 */ "Changelog";
    }

    private static Result<Changelog> createExpectedParseResult()
    {
      Result<Changelog> expectedParseResult =
        Result.Failure<Changelog>(
          "No level one heading. Error parsing text in line 1 / index 1.");

      return expectedParseResult;
    }

  }

}
