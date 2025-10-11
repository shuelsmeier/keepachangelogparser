using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace KeepAChangelogParser.Tests
{
  [TestClass]
  public class TheChangelogOfKeepAChangelogIsValid
  {
    [TestMethod]
    public void WhenThefKeepAChangelogChangelogIsParsed_ThenTheResultIsCorrect()
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
      Assert.IsTrue(
        actualParseResult.IsSuccess,
        actualParseResult.IsSuccess ? string.Empty : actualParseResult.Error);
    }

    private static string createTextToParse()
    {
      string changeLogPath;
      string currentDirectory = Environment.CurrentDirectory;
      string sampleFilesDirectory = Path.Combine(currentDirectory, "SampleFiles");

      while (true)
      {
        changeLogPath = Path.Combine(sampleFilesDirectory, "KeepAChangelogParser_CHANGELOG.md");
        if (File.Exists(changeLogPath)) { break; }

        DirectoryInfo? directoryInfo = Directory.GetParent(sampleFilesDirectory);
        if (directoryInfo == null) { break; }

        sampleFilesDirectory = directoryInfo.FullName;
      }

      return FileService.ReadText(changeLogPath).Value;
    }
  }
}
