using System;

namespace KeepAChangelogParser.Models
{
  /// <summary>
  /// Changelog list handling.
  /// </summary>
  [Flags]
  public enum ChangelogListHandling
  {
    /// <summary>
    /// None
    /// </summary>
    None = 0,
    /// <summary>
    /// Allow nested lists
    /// </summary>
    AllowNestedLists = 1,
    /// <summary>
    /// Allow asterisk
    /// </summary>
    AllowAsterisk = 2,
  }
}
