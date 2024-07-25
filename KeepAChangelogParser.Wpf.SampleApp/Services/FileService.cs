using CSharpFunctionalExtensions;
using KeepAChangelogParser.Wpf.SampleApp.Contracts;
using System.IO;

namespace KeepAChangelogParser.Wpf.SampleApp.Services
{
  public class FileService :
    IFileService
  {
    public Result<string> ReadText(
      string filePath
    )
    {
      if (!File.Exists(filePath))
      {
        return Result.Failure<string>(
          $"The file {filePath} does not exist!");
      }

      using FileStream fileStream =
        new FileStream(
          filePath,
          FileMode.Open,
          FileAccess.Read);

      using StreamReader streamReader =
        new StreamReader(
          fileStream);

      string text =
        streamReader.ReadToEnd();

      return Result.Success(text);
    }
  }
}
