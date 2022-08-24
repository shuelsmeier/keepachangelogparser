using CSharpFunctionalExtensions;
using KeepAChangelogParser.Contracts;

namespace KeepAChangelogParser.Services
{
  internal class NewLineService :
    INewLineService
  {
    public const string MixedLineEndingsCRAndLFError = "Text contains mixed line endings CR and LF.";
    public const string MixedLineEndingsCRAndCRLFError = "Text contains mixed line endings CR and CRLF.";
    public const string MixedLineEndingsLFAndCRLFError = "Text contains mixed line endings LF and CRLF.";
    public const string MixedLineEndingsCRLFAndCRLFError = "Text contains mixed line endings CR, LF and CRLF.";
    public const string NoLineEndingsError = "Text contains no line endings.";

    public Result<string> DetermineLineEndings(
      string text
    )
    {
      int carriageReturnCount = 0;
      int lineFeedCount = 0;
      int carriageReturnLineFeedCount = 0;

      for (int index = 0; index < text.Length; ++index)
      {
        if (index < text.Length - 1)
        {
          if (text[index] == '\r' &&
              text[index + 1] == '\n')
          {
            carriageReturnLineFeedCount++;
            index++;
            continue;
          }
        }

        if (text[index] == '\r')
        {
          carriageReturnCount++;
          continue;
        }

        if (text[index] == '\n')
        {
          lineFeedCount++;
        }
      }

      if (carriageReturnCount > 0 && lineFeedCount > 0 && carriageReturnLineFeedCount > 0)
      {
        return Result.Failure<string>(MixedLineEndingsCRLFAndCRLFError);
      }

      if (carriageReturnCount > 0 && lineFeedCount > 0)
      {
        return Result.Failure<string>(MixedLineEndingsCRAndLFError);
      }

      if (carriageReturnCount > 0 && carriageReturnLineFeedCount > 0)
      {
        return Result.Failure<string>(MixedLineEndingsCRAndCRLFError);
      }

      if (lineFeedCount > 0 && carriageReturnLineFeedCount > 0)
      {
        return Result.Failure<string>(MixedLineEndingsLFAndCRLFError);
      }

      if (carriageReturnCount > 0) { return Result.Success("\r"); }
      if (lineFeedCount > 0) { return Result.Success("\n"); }
      if (carriageReturnLineFeedCount > 0) { return Result.Success("\r\n"); }

      return Result.Failure<string>(NoLineEndingsError);
    }
  }
}
