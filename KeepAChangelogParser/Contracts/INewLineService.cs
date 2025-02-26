using CSharpFunctionalExtensions;

namespace KeepAChangelogParser.Contracts
{
  internal interface INewLineService
  {
    public Result<string> DetermineLineEndings(
      string text
    );
  }
}
