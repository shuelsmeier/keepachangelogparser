namespace KeepAChangelogParser.Models
{
  internal class ChangelogTokenMatch
  {
    public int LineNumber { get; set; }

    public ChangelogTokenType Type { get; set; }

    public string Value { get; set; } = string.Empty;

    public int StartIndex { get; set; }

    public int EndIndex { get; set; }

    public int Precedence { get; set; }
  }
}
