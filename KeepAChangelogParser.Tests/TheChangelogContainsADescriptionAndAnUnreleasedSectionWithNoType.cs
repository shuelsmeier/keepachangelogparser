﻿using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Extensions;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KeepAChangelogParser.Tests
{
  [TestClass]
  public class TheChangelogContainsADescriptionAndAnUnreleasedSectionWithNoType
  {
    [TestMethod]
    public void WhenTheChangelogContainsADescriptionAndAnUnreleasedSectionWithNoType_ThenTheResultIsFailureInvalidSubsectionType()
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
        /* 01 */ "# Changelog" + Environment.NewLine +
        /* 02 */ "" + Environment.NewLine +
        /* 03 */ "All notable changes to this project will be documented in this file." + Environment.NewLine +
        /* 04 */ "" + Environment.NewLine +
        /* 05 */ "The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)," + Environment.NewLine +
        /* 06 */ "and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html)." + Environment.NewLine +
        /* 07 */ "" + Environment.NewLine +
        /* 08 */ "## [Unreleased]" + Environment.NewLine +
        /* 09 */ "" + Environment.NewLine +
        /* 10 */ "### " + Environment.NewLine +
        /* 11 */ "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        /* 12 */ "- Version navigation." + Environment.NewLine +
        /* 13 */ "" + Environment.NewLine;
    }

    private static Result<Changelog> createExpectedParseResult()
    {
      Result<Changelog> expectedParseResult =
        Result.Failure<Changelog>(
          "Invalid title. Error parsing text in line 11 / index 75.");

      return expectedParseResult;
    }
  }
}
