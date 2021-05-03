using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{

  public interface IChangelogService
  {

    Result<Changelog> ReadChangelog(
      string filePath
    );

  }

}
