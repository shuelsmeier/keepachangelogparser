using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeepAChangelogParser.Tests
{

  [TestClass]
  public class TheChangelogContainsMixedCRAndCRLFLineEndings
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
        "Text contains mixed line endings CR and CRLF.");
    }

    private static string createTextToParse()
    {
      return
        "# Changelog\r" +
        "\r" +
        "All notable changes to this project will be documented in this file.\r" +
        "\r\n" +
        "The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),\r\n" +
        "and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).\r\n" +
        "\r";
    }

  }

}
