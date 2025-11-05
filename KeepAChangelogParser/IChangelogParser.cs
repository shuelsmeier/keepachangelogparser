using CSharpFunctionalExtensions;
using KeepAChangelogParser.Models;
using System.Collections.Generic;

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
    /// <param name="changelogVersionType">The optional type of version the parser should check for.</param>
    /// <param name="customChangelogSubSectionTypeCollection">The optional collection of custom changelog sub section types.</param>
    /// <returns>An instance of <see cref="Result"/> class containing an instance of <see cref="Changelog"/> class,
    /// when successful; otherwise, the error message.</returns>
    public Result<Changelog> Parse(
      string text,
      ChangelogVersionType changelogVersionType = ChangelogVersionType.SemanticVersion,
      List<string>? customChangelogSubSectionTypeCollection = null
    );
  }
}
