using System.Text.RegularExpressions;

namespace KeepAChangelogParser.Models
{

  internal class ChangelogTokenDefinition
  {

    public Regex Regex { get; }

    public ChangelogTokenType Type { get; }

    public int Precedence { get; }

    public ChangelogTokenDefinition(
      ChangelogTokenType type,
      string regexPattern,
      int precedence
    )
    {
      this.Type = type;
      this.Regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      this.Precedence = precedence;
    }

  }

}
