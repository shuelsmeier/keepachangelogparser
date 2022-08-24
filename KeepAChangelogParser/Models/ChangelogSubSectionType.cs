namespace KeepAChangelogParser.Models
{

  /// <summary>
  /// Specifies the changelog sub section type used in the changelog sub section.
  /// </summary>
  public enum ChangelogSubSectionType
  {
    /// <summary>For new features</summary>
    Added,
    /// <summary>For changes in existing functionality</summary>
    Changed,
    /// <summary>For soon-to-be removed features</summary>
    Deprecated,
    /// <summary>For now removed features</summary>
    Removed,
    /// <summary>For any bug fixes</summary>
    Fixed,
    /// <summary>In case of vulnerabilities</summary>
    Security,
  }

}
