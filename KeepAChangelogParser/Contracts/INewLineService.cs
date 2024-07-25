using CSharpFunctionalExtensions;

namespace KeepAChangelogParser.Contracts
{
  internal interface INewLineService
  {
    Result<string> DetermineLineEndings(
      string text
    );
  }
}
