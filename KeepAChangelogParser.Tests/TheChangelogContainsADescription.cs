using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Extensions;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KeepAChangelogParser.Tests
{

  [TestClass]
  public class TheChangelogContainsADescription
  {

    [TestMethod]
    public void WhenTheChangelogContainsADescription_ThenTheResultIsCorrect()
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
        actualParseResult,
        expectedParseResult,
        ChangelogResultComparerFactory.Create());
    }

    private static string createTextToParse()
    {
      return
        "# Changelog" + Environment.NewLine +
        "" + Environment.NewLine +
        "All notable changes to this project will be documented in this file." + Environment.NewLine +
        "" + Environment.NewLine +
        "The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)," + Environment.NewLine +
        "and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html)." + Environment.NewLine +
        "" + Environment.NewLine;
    }

    private static Result<Changelog> createExpectedParseResult()
    {
      Result<Changelog> expectedParseResult =
        Result.Success(
          new Changelog()
          {
            MarkdownTitle =
              "Changelog",
            MarkdownText =
              "All notable changes to this project will be documented in this file." + Environment.NewLine +
              "" + Environment.NewLine +
              "The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)," + Environment.NewLine +
              "and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).",
          });

      return expectedParseResult;
    }

  }

}
