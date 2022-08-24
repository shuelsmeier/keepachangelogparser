using CSharpFunctionalExtensions;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{
  public interface IFileService
  {
    public Result<string> ReadText(
      string filePath
    );
  }
}
