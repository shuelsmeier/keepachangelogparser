using KeepAChangelogParser.Models;
using System.Collections.Generic;

namespace KeepAChangelogParser.Contracts
{

  internal interface IChangelogTokenizer
  {

    IEnumerable<ChangelogToken> Tokenize(
      string text
    );

  }

}
