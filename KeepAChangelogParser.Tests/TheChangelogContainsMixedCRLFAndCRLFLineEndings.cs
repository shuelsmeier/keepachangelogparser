using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeepAChangelogParser.Tests
{

  [TestClass]
  public class TheChangelogContainsMixedCRLFAndCRLFLineEndings
  {

    [TestMethod]
    public void WhenTheChangelogIsParsed_ThenTheResultIsFailure()
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
      Assert.AreEqual(
        actualParseResult.Error,
        "Text contains mixed line endings CR, LF and CRLF.");
    }

    private static string createTextToParse()
    {
      return
        "# Changelog\n" +
        "\n" +
        "All notable changes to this project will be documented in this file.\n" +
        "\r\n" +
        "The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),\r\n" +
        "and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).\n" +
        "\r";
    }

  }

}
