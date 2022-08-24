using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;

namespace KeepAChangelogParser
{

  /// <summary>
  /// Parser for keepachangelog markdown texts.
  /// </summary>
  public interface IChangelogParser
  {

    /// <summary>
    /// Parses and returns the changelog specified by the keepachangelog markdown text.
    /// </summary>
    /// <param name="text">The <see cref="string"/> instance that contains the keepachangelog markdown text.</param>
    /// <returns>An instance of <see cref="Result"/> class containing an instance of <see cref="Changelog"/> class,
    /// when successful; otherwise, the error message.</returns>
    Result<Changelog> Parse(
      string text
    );

  }

}
