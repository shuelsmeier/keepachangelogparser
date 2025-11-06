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
    /// AllowNestedLists
    /// </summary>
    AllowNestedLists = 1
  }
}
