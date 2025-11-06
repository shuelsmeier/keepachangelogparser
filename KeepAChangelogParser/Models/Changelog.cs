namespace KeepAChangelogParser.Models
{
  /// <summary>
  /// Represents a keepachangelog changelog.
  /// </summary>
  public class Changelog
  {
    /// <summary>
    /// Gets or sets the markdown title.
    /// </summary>
    /// <value>
    /// The markdown title.
    /// </value>
    public string MarkdownTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the markdown text.
    /// </summary>
    /// <value>
    /// The markdown text.
    /// </value>
    public string MarkdownText { get; set; } = string.Empty;

    /// <summary>
    /// Gets the section unreleased.
    /// </summary>
    /// <value>
    /// The section unreleased.
    /// </value>
#if NET6_0_OR_GREATER
    public ChangelogSectionUnreleased SectionUnreleased { get; init; } = new ChangelogSectionUnreleased();
#else
    public ChangelogSectionUnreleased SectionUnreleased { get; } = new ChangelogSectionUnreleased();
#endif

    /// <summary>
    /// Gets the section collection.
    /// </summary>
    /// <value>
    /// The section collection.
    /// </value>
#if NET6_0_OR_GREATER
    public ChangelogSectionCollection SectionCollection { get; init; } = new ChangelogSectionCollection();
#else
    public ChangelogSectionCollection SectionCollection { get; } = new ChangelogSectionCollection();
#endif
  }
}
