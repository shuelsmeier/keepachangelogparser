using System.Diagnostics;

namespace KeepAChangelogParser.Models
{
  [DebuggerDisplay("{debuggerDisplay,nq}")]
  internal class ChangelogToken
  {
    public ChangelogTokenType Type { get; }

    public int LineNumber { get; }

    public int Index { get; }

    public string Value { get; }

    public ChangelogToken(
      ChangelogTokenType type
    )
    {
      this.Type = type;
      this.Value = string.Empty;
      this.LineNumber = -1;
      this.Index = -1;
    }

    public ChangelogToken(
      ChangelogTokenType type,
      string value,
      int lineNumber,
      int index
    )
    {
      this.Type = type;
      this.Value = value;
      this.LineNumber = lineNumber;
      this.Index = index;
    }

    private string debuggerDisplay
    {
      get
      {
        string debuggerDisplay = string.Empty;

        debuggerDisplay += $"\"{this.Type}\"";
        debuggerDisplay += " | ";
        debuggerDisplay += $"\"{this.LineNumber}\"";
        debuggerDisplay += " | ";
        debuggerDisplay += $"\"{this.Index}\"";
        debuggerDisplay += " | ";
        debuggerDisplay += $"\"{this.Value}\"";

        return debuggerDisplay;
      }
    }
  }
}
