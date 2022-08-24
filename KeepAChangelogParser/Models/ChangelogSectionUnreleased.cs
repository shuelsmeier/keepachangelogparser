namespace KeepAChangelogParser.Models
{

  /// <summary>
  /// Represents a changelog section unreleased.
  /// </summary>
  public class ChangelogSectionUnreleased
  {

    /// <summary>
    /// Gets or sets the markdown title.
    /// </summary>
    /// <value>
    /// The markdown title.
    /// </value>
    public string MarkdownTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets the sub section collection.
    /// </summary>
    /// <value>
    /// The sub section collection.
    /// </value>
    public ChangelogSubSectionCollection SubSectionCollection { get; } = new ChangelogSubSectionCollection();

  }

}
