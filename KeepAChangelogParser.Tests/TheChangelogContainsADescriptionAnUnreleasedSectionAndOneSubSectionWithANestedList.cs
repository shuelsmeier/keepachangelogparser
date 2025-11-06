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
  public class TheChangelogContainsADescriptionAnUnreleasedSectionAndOneSubSectionWithANestedList
  {
    [TestMethod]
    public void WhenTheChangelogContainsADescriptionAnUnreleasedSectionAndOneSubSectionWithANestedList_ThenTheResultIsCorrect()
    {
      // Arrange
      string textToParse =
        createTextToParse();

      IChangelogParser changelogParser =
        new ChangelogParser();

      // Act
      Result<Changelog> actualParseResult =
        changelogParser.Parse(
          textToParse,
          new ChangelogParserSettings()
          {
            ChangelogListHandling = ChangelogListHandling.AllowNestedLists,
          });

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
        "## [1.0.0] - 2017-06-20" + Environment.NewLine +
        "" + Environment.NewLine +
        "### Added" + Environment.NewLine +
        "- New visual identity by[@tylerfortune8](https://github.com/tylerfortune8)." + Environment.NewLine +
        "  - Version navigation." + Environment.NewLine +
        "    - Nested list parsing (#514)." + Environment.NewLine +
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
                    MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8).",
                  },
                  new ChangelogSubSectionItem()
                  {
                    MarkdownText = "Version navigation.",
                  }
                });

      expectedParseResult.Value.
        SectionCollection.
          Add(
            new ChangelogSection()
            {
              MarkdownVersion = "1.0.0",
              MarkdownDate = "2017-06-20",
              SubSectionCollection =
                new ChangelogSubSectionCollection()
                {
                  new ChangelogSubSection()
                  {
                    Type = ChangelogSubSectionType.Added,
                    ItemCollection = new ChangelogSubSectionItemCollection()
                    {
                      new ChangelogSubSectionItem()
                      {
                        MarkdownText = "New visual identity by[@tylerfortune8](https://github.com/tylerfortune8).",
                        ItemCollection = new ChangelogSubSectionItemCollection()
                        {
                          new ChangelogSubSectionItem()
                          {
                            MarkdownText = "Version navigation (#512).",
                            ItemCollection = new ChangelogSubSectionItemCollection()
                            {
                              new ChangelogSubSectionItem()
                              {
                                MarkdownText = "Nested list parsing (#514).",
                              }
                            }
                          },
                        }
                      }
                    }
                  }
                }
            });

      return expectedParseResult;
    }
  }
}
