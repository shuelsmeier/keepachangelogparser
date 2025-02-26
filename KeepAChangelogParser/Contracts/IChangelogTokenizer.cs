using KeepAChangelogParser.Models;
using System.Collections.Generic;

namespace KeepAChangelogParser.Contracts
{
  internal interface IChangelogTokenizer
  {
    public IEnumerable<ChangelogToken> Tokenize(
      string text,
      string newLine
    );
  }
}
