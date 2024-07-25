using KeepAChangelogParser.Contracts;
using KeepAChangelogParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KeepAChangelogParser.Services
{
  internal sealed class ChangelogTokenizer :
    IChangelogTokenizer
  {
    private readonly List<ChangelogTokenDefinition> tokenDefinitionCollection =
      new List<ChangelogTokenDefinition>()
      {
        new ChangelogTokenDefinition(ChangelogTokenType.CloseParenthesis, "\\)", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.CloseSquareBracket, "\\]", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.Dash, "-", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.Date, "\\d\\d\\d\\d-\\d\\d-\\d\\d", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.HeadingOne, "#", 3),
        new ChangelogTokenDefinition(ChangelogTokenType.HeadingTwo, "##", 2),
        new ChangelogTokenDefinition(ChangelogTokenType.HeadingThree, "###", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.OpenParenthesis, "\\(", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.OpenSquareBracket, "\\[", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.Space, " ", 1),
        new ChangelogTokenDefinition(ChangelogTokenType.Text, "[^\\[|^\\]|^\\(|^\\)]+", 99),
        new ChangelogTokenDefinition(ChangelogTokenType.Version, "(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)(?:-((?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\\+([0-9a-zA-Z-]+(?:\\.[0-9a-zA-Z-]+)*))?", 1),
      };

    public IEnumerable<ChangelogToken> Tokenize(
      string text,
      string newLine
    )
    {
      if (string.IsNullOrEmpty(text))
      {
        return new List<ChangelogToken>();
      }

      List<ChangelogToken> tokenCollection =
        new List<ChangelogToken>();

      List<string> lineCollection =
        text.
          Split(new[] { newLine }, StringSplitOptions.None).
          ToList();

      for (int lineNumber = 1; lineNumber <= lineCollection.Count; lineNumber++)
      {
        if (lineNumber > 1)
        {
          ChangelogToken token =
            new ChangelogToken(
              ChangelogTokenType.NewLine,
              newLine,
              lineNumber,
              lineCollection[lineNumber - 1].Length);

          tokenCollection.
            Add(token);
        }

        int startIndex = 0;

        while (startIndex != lineCollection[lineNumber - 1].Length)
        {
          List<ChangelogTokenMatch> tokenMatchCollection =
            this.findTokenMatches(
              lineNumber,
              lineCollection[lineNumber - 1],
              startIndex);

          List<IGrouping<int, ChangelogTokenMatch>> tokenMatchByStartIndexCollection =
            tokenMatchCollection.
              GroupBy(x => x.StartIndex).
              OrderBy(x => x.Key).
              ToList();

          ChangelogTokenMatch tokenMatch =
            tokenMatchByStartIndexCollection[0].
              OrderBy(x => x.Precedence).
              First();

          ChangelogToken token =
            new ChangelogToken(
              tokenMatch.Type,
              tokenMatch.Value,
              lineNumber,
              tokenMatch.StartIndex + 1);

          tokenCollection.
            Add(token);

          startIndex =
            tokenMatch.EndIndex;
        }
      }

#if NETCOREAPP || NET5_0 || NET6_0

      tokenCollection.Add(
        new ChangelogToken(
          ChangelogTokenType.NewLine,
          newLine,
          lineCollection.Count,
          lineCollection[^1].Length));

#else

      tokenCollection.Add(
        new ChangelogToken(
          ChangelogTokenType.NewLine,
          newLine,
          lineCollection.Count,
          lineCollection[lineCollection.Count - 1].Length));

#endif

      tokenCollection.Add(
        new ChangelogToken(
          ChangelogTokenType.SequenceTerminator));

      return tokenCollection;
    }

    private static IEnumerable<ChangelogTokenMatch> findMatches(
      ChangelogTokenDefinition tokenDefinition,
      int lineNumber,
      string inputString,
      int startIndex
    )
    {
      MatchCollection matches =
        tokenDefinition.Regex.
          Matches(
            inputString,
            startIndex);

      for (int i = 0; i < matches.Count; i++)
      {
        yield return new ChangelogTokenMatch()
        {
          LineNumber = lineNumber,
          StartIndex = matches[i].Index,
          EndIndex = matches[i].Index + matches[i].Length,
          Type = tokenDefinition.Type,
          Value = matches[i].Value,
          Precedence = tokenDefinition.Precedence
        };
      }
    }

    private List<ChangelogTokenMatch> findTokenMatches(
      int lineNumber,
      string text,
      int startIndex
    )
    {
      List<ChangelogTokenMatch> tokenMatchCollection =
        new List<ChangelogTokenMatch>();

      foreach (ChangelogTokenDefinition tokenDefinition in this.tokenDefinitionCollection)
      {
        tokenMatchCollection.
          AddRange(
            findMatches(tokenDefinition, lineNumber, text, startIndex).
            ToList());
      }

      return tokenMatchCollection;
    }
  }
}
