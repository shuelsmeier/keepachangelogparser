using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Wpf.SampleApp.Contracts;

namespace KeepAChangelogParser.Wpf.SampleApp.Services
{
  public class ChangelogService :
    IChangelogService
  {
    private readonly IChangelogParser changelogParser;
    private readonly IFileService fileService;

    public ChangelogService(
      IChangelogParser changelogParser,
      IFileService fileService
    )
    {
      this.changelogParser = changelogParser;
      this.fileService = fileService;
    }

    public Result<Changelog> ReadChangelog(
      string filePath
    )
    {
      Guard.Against.NullOrWhiteSpace(filePath, nameof(filePath));

      Result<ReadChangelogContext> readChangelogContextResult =
        createReadChangelogContext(
          filePath);

      readChangelogContextResult = this.readFile(readChangelogContextResult);
      readChangelogContextResult = this.parseText(readChangelogContextResult);

      return createReadChangelogReturnValue(readChangelogContextResult);
    }

    private static Result<ReadChangelogContext> createReadChangelogContext(
      string filePath
    )
    {
      ReadChangelogContext readChangelogContext =
        new ReadChangelogContext(
          filePath);

      return Result.Success(readChangelogContext);
    }

    private Result<ReadChangelogContext> readFile(
      Result<ReadChangelogContext> readChangelogContextResult
    )
    {
      if (readChangelogContextResult.IsFailure) { return readChangelogContextResult; }

      Result<string> readTextResult =
        this.fileService.ReadText(
          readChangelogContextResult.Value.FilePath);

      if (readTextResult.IsFailure)
      {
        return Result.Failure<ReadChangelogContext>(
          readTextResult.Error);
      }

      readChangelogContextResult.Value.Text =
        readTextResult.Value;

      return readChangelogContextResult;
    }

    private Result<ReadChangelogContext> parseText(
      Result<ReadChangelogContext> readChangelogContextResult
    )
    {
      if (readChangelogContextResult.IsFailure) { return readChangelogContextResult; }

      Guard.Against.Null(readChangelogContextResult.Value.Text, nameof(readChangelogContextResult.Value.Text));

      Result<Changelog> parseResult =
        this.changelogParser.Parse(
          readChangelogContextResult.Value.Text);

      if (parseResult.IsFailure)
      {
        return Result.Failure<ReadChangelogContext>(
          parseResult.Error);
      }

      readChangelogContextResult.Value.Changelog = parseResult.Value;

      return readChangelogContextResult;
    }

    private static Result<Changelog> createReadChangelogReturnValue(
      Result<ReadChangelogContext> readChangelogContextResult
    )
    {
      if (readChangelogContextResult.IsFailure)
      {
        return Result.Failure<Changelog>(readChangelogContextResult.Error);
      }

      Guard.Against.Null(readChangelogContextResult.Value.Changelog, nameof(readChangelogContextResult.Value.Changelog));

      return Result.Success(readChangelogContextResult.Value.Changelog);
    }

    private class ReadChangelogContext
    {
      public string FilePath { get; }

      public string? Text { get; set; } = null;

      public Changelog? Changelog { get; set; } = null;

      public ReadChangelogContext(
        string filePath
      )
      {
        this.FilePath = filePath;
      }
    }
  }
}
