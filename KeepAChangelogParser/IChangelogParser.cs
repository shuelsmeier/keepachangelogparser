using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;

namespace KeepAChangelogParser
{

  public interface IChangelogParser
  {

    Result<Changelog> Parse(
      string text
    );

  }

}
