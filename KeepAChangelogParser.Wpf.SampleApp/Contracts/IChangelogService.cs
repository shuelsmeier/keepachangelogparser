using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{
  public interface IChangelogService
  {
    public Result<Changelog> ReadChangelog(
      string filePath
    );
  }
}
