using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Extensions;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KeepAChangelogParser.Tests
{
  [TestClass]
  public class TheChangelogContainsADescriptionAnUnreleasedSectionAndTwoSubSections
  {
    [TestMethod]
    public void WhenTheChangelogContainsADescriptionAnUnreleasedSectionAndTwoSubSections_ThenTheResultIsCorrect()
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
        "# Changelog" + Environment.NewLine +
        "" + Environment.NewLine +
        "All notable changes to this project will be documented in this file." + Environment.NewLine +
        "" + Environment.NewLine +
        "The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)," + Environment.NewLine +
        "and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html)." + Environment.NewLine +
        "" + Environment.NewLine +
        "## [Unreleased]" + Environment.NewLine +
        "" + Environment.NewLine +
        "### Added" + Environment.NewLine +
        "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        "- Version navigation." + Environment.NewLine +
        "" + Environment.NewLine +
        "## [1.1.0] - 2017-07-01" + Environment.NewLine +
        "" + Environment.NewLine +
        "### Added" + Environment.NewLine +
        "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        "- Version navigation." + Environment.NewLine +
        "" + Environment.NewLine +
        "### Changed" + Environment.NewLine +
        "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        "- Version navigation." + Environment.NewLine +
        "" + Environment.NewLine +
        "## [1.0.0] - 2017-06-20" + Environment.NewLine +
        "" + Environment.NewLine +
        "### Added" + Environment.NewLine +
        "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        "- Version navigation." + Environment.NewLine +
        "" + Environment.NewLine +
        "### Changed" + Environment.NewLine +
        "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        "- Version navigation." + Environment.NewLine +
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

      expectedParseResult.Value.
        SectionUnreleased.
          MarkdownTitle = "Unreleased";

      expectedParseResult.Value.
        SectionUnreleased.
          SubSectionCollection.
            AddRange(
              new List<ChangelogSubSection>()
              {
                new ChangelogSubSection()
                {
                  Type = ChangelogSubSectionType.Added,
                },
              });

      expectedParseResult.Value.
        SectionUnreleased.
          SubSectionCollection[0].
            ItemCollection.
              AddRange(
                new List<ChangelogSubSectionItem>()
                {
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)."
                  },
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "Version navigation."
                  },
                });

      expectedParseResult.Value.
        SectionCollection.
          AddRange(
            new List<ChangelogSection>()
            {
              new ChangelogSection()
              {
                MarkdownVersion = "1.1.0",
                MarkdownDate = "2017-07-01"
              },
              new ChangelogSection()
              {
                MarkdownVersion = "1.0.0",
                MarkdownDate = "2017-06-20"
              },
            });

      expectedParseResult.Value.
        SectionCollection[0].
          SubSectionCollection.
            AddRange(
              new List<ChangelogSubSection>()
              {
                new ChangelogSubSection()
                {
                  Type = ChangelogSubSectionType.Added,
                },
                new ChangelogSubSection()
                {
                  Type = ChangelogSubSectionType.Changed,
                },
              });

      expectedParseResult.Value.
        SectionCollection[0].
          SubSectionCollection[0].
            ItemCollection.
              AddRange(
                new List<ChangelogSubSectionItem>()
                {
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)."
                  },
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "Version navigation."
                  },
                });

      expectedParseResult.Value.
        SectionCollection[0].
          SubSectionCollection[1].
            ItemCollection.
              AddRange(
                new List<ChangelogSubSectionItem>()
                {
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)."
                  },
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "Version navigation."
                  },
                });

      expectedParseResult.Value.
        SectionCollection[1].
          SubSectionCollection.
            AddRange(
              new List<ChangelogSubSection>()
              {
                new ChangelogSubSection()
                {
                  Type = ChangelogSubSectionType.Added,
                },
                new ChangelogSubSection()
                {
                  Type = ChangelogSubSectionType.Changed,
                },
              });

      expectedParseResult.Value.
        SectionCollection[1].
          SubSectionCollection[0].
            ItemCollection.
              AddRange(
                new List<ChangelogSubSectionItem>()
                {
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)."
                  },
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "Version navigation."
                  },
                });

      expectedParseResult.Value.
        SectionCollection[1].
          SubSectionCollection[1].
            ItemCollection.
              AddRange(
                new List<ChangelogSubSectionItem>()
                {
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)."
                  },
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "Version navigation."
                  },
                });

      return expectedParseResult;
    }
  }
}
