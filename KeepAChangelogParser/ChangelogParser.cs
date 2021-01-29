using CSharpFunctionalExtensions;
using KeepAChangelogParser.Contracts;
using KeepAChangelogParser.Models;
using KeepAChangelogParser.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeepAChangelogParser
{

  public class ChangelogParser :
    IChangelogParser
  {

    public Result<Changelog> Parse(
      string text
    )
    {
      Result<Changelog> changelogResult =
        Result.Success(new Changelog());

      IChangelogTokenizer changelogTokenizer =
        new ChangelogTokenizer();

      IEnumerable<ChangelogToken> tokenCollection =
        changelogTokenizer.Tokenize(text);

      Stack<ChangelogToken> tokenStack =
        new Stack<ChangelogToken>(tokenCollection.Reverse());

      changelogResult = parseHeadingOne(changelogResult, tokenStack);
      changelogResult = parseSpace(changelogResult, tokenStack);
      changelogResult = parseTitle(changelogResult, tokenStack);
      changelogResult = parseNewLine(changelogResult, tokenStack);

      while (isHeadingOneTextOrNewLine(changelogResult, tokenStack))
      {
        if (isText(tokenStack))
        {
          changelogResult = parseText(changelogResult, tokenStack);
        }

        changelogResult = parseTextNewLine(changelogResult, tokenStack);
      }

      changelogResult = trimText(changelogResult);

      while (isHeadingTwo(changelogResult, tokenStack))
      {
        changelogResult = parseHeadingTwo(changelogResult, tokenStack);
        changelogResult = parseSpace(changelogResult, tokenStack);
        changelogResult = parseOpenSquareBracket(changelogResult, tokenStack);

        if (isTitle(changelogResult, tokenStack) &&
            isEmptySectionUnreleased(changelogResult) &&
            isEmptySectionCollection(changelogResult))
        {
          changelogResult = parseUnreleased(changelogResult, tokenStack);
          changelogResult = parseCloseSquareBracket(changelogResult, tokenStack);
          changelogResult = parseNewLine(changelogResult, tokenStack);

          while (isNewLine(changelogResult, tokenStack))
          {
            changelogResult = parseNewLine(changelogResult, tokenStack);
          }

          while (isHeadingThree(changelogResult, tokenStack))
          {
            changelogResult = parseUnreleasedHeadingThree(changelogResult, tokenStack);
            changelogResult = parseSpace(changelogResult, tokenStack);
            changelogResult = parseUnreleasedTitle(changelogResult, tokenStack);
            changelogResult = parseNewLine(changelogResult, tokenStack);

            while (isHeadingThreeUnreleasedTextOrNewLine(changelogResult, tokenStack))
            {
              if (isText(tokenStack))
              {
                changelogResult = parseDash(changelogResult, tokenStack);
                changelogResult = parseSpace(changelogResult, tokenStack);
                changelogResult = parseUnreleasedText(changelogResult, tokenStack);
              }

              changelogResult = parseListTextNewLine(changelogResult, tokenStack);
            }
          }
        }
        else
        {
          changelogResult = parseVersion(changelogResult, tokenStack);
          changelogResult = parseCloseSquareBracket(changelogResult, tokenStack);
          changelogResult = parseSpace(changelogResult, tokenStack);
          changelogResult = parseDash(changelogResult, tokenStack);
          changelogResult = parseSpace(changelogResult, tokenStack);
          changelogResult = parseDate(changelogResult, tokenStack);
          changelogResult = parseNewLine(changelogResult, tokenStack);

          while (isNewLine(changelogResult, tokenStack))
          {
            changelogResult = parseNewLine(changelogResult, tokenStack);
          }

          while (isHeadingThree(changelogResult, tokenStack))
          {
            changelogResult = parseHeadingThree(changelogResult, tokenStack);
            changelogResult = parseSpace(changelogResult, tokenStack);
            changelogResult = parseTitle(changelogResult, tokenStack);
            changelogResult = parseNewLine(changelogResult, tokenStack);

            while (isHeadingThreeTextOrNewLine(changelogResult, tokenStack))
            {
              if (isText(tokenStack))
              {
                changelogResult = parseDash(changelogResult, tokenStack);
                changelogResult = parseSpace(changelogResult, tokenStack);
                changelogResult = parseText(changelogResult, tokenStack);
              }

              changelogResult = parseListTextNewLine(changelogResult, tokenStack);
            }
          }
        }
      }

      changelogResult = parseSequenceTerminator(changelogResult, tokenStack);

      if (changelogResult.IsFailure)
      {
        return Result.Failure<Changelog>(changelogResult.Error);
      }

      return Result.Success(changelogResult.Value);
    }

    private static Result<Changelog> addTokenValueToText(
      Result<Changelog> changelogResult,
      ChangelogToken token
    )
    {
      int sectionCollectionCount =
        changelogResult.Value.
          SectionCollection.Count;

      if (sectionCollectionCount == 0)
      {
        changelogResult.Value.
          MarkdownText += token.Value;

        return changelogResult;
      }

      int subSectionCollectionCount =
        changelogResult.Value.
          SectionCollection[sectionCollectionCount - 1].
            SubSectionCollection.Count;

      int subSectionItemCollectionCount =
        changelogResult.Value.
          SectionCollection[sectionCollectionCount - 1].
            SubSectionCollection[subSectionCollectionCount - 1].
              ItemCollection.Count;

      changelogResult.Value.
        SectionCollection[sectionCollectionCount - 1].
          SubSectionCollection[subSectionCollectionCount - 1].
            ItemCollection[subSectionItemCollectionCount - 1].
              MarkdownText += token.Value;

      return changelogResult;
    }

    private static Result<Changelog> addTokenValueToTitleOrSetType(
      Result<Changelog> changelogResult,
      ChangelogToken token
    )
    {
      int sectionCollectionCount =
        changelogResult.Value.
          SectionCollection.Count;

      if (sectionCollectionCount == 0)
      {
        changelogResult.Value.
          MarkdownTitle += token.Value;

        return changelogResult;
      }

      int subSectionCollectionCount =
        changelogResult.Value.
          SectionCollection[sectionCollectionCount - 1].
            SubSectionCollection.Count;

      if (!Enum.TryParse(token.Value, out ChangelogSubSectionType subSectionType))
      {
        return Result.Failure<Changelog>(
          $"Invalid subsection type. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      for (int index = 0; index < changelogResult.Value.SectionCollection[sectionCollectionCount - 1].SubSectionCollection.Count - 1; index++)
      {
        if (changelogResult.Value.SectionCollection[sectionCollectionCount - 1].SubSectionCollection[index].Type == subSectionType)
        {
          return Result.Failure<Changelog>(
            $"Subsection type already exists. Error parsing text in line {token.LineNumber} / index {token.Index}.");
        }
      }

      changelogResult.Value.
        SectionCollection[sectionCollectionCount - 1].
          SubSectionCollection[subSectionCollectionCount - 1].
            Type = subSectionType;

      return changelogResult;
    }

    private static Result<Changelog> addTokenValueToUnreleasedText(
      Result<Changelog> changelogResult,
      ChangelogToken token
    )
    {
      int subSectionUnreleasedCollectionCount =
        changelogResult.Value.
          SectionUnreleased.
            SubSectionCollection.Count;

      int subSectionUnreleasedItemCollectionCount =
        changelogResult.Value.
          SectionUnreleased.
            SubSectionCollection[subSectionUnreleasedCollectionCount - 1].
              ItemCollection.Count;

      changelogResult.Value.
        SectionUnreleased.
          SubSectionCollection[subSectionUnreleasedCollectionCount - 1].
            ItemCollection[subSectionUnreleasedItemCollectionCount - 1].
              MarkdownText += token.Value;

      return changelogResult;
    }

    private static bool isHeadingOneTextOrNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.CloseParenthesis:
        case ChangelogTokenType.CloseSquareBracket:
        case ChangelogTokenType.Dash:
        case ChangelogTokenType.OpenParenthesis:
        case ChangelogTokenType.OpenSquareBracket:
        case ChangelogTokenType.Text:
        case ChangelogTokenType.NewLine:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isHeadingThree(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.HeadingThree:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isHeadingThreeTextOrNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.Dash:
          {
            int sectionCollectionCount =
              changelogResult.Value.
                SectionCollection.Count;

            int subSectionCollectionCount =
              changelogResult.Value.
                SectionCollection[sectionCollectionCount - 1].
                  SubSectionCollection.Count;

            changelogResult.Value.
                SectionCollection[sectionCollectionCount - 1].
                  SubSectionCollection[subSectionCollectionCount - 1].
                    ItemCollection.
                      Add(new ChangelogSubSectionItem());

            return true;
          }

        case ChangelogTokenType.CloseParenthesis:
        case ChangelogTokenType.CloseSquareBracket:
        case ChangelogTokenType.OpenParenthesis:
        case ChangelogTokenType.OpenSquareBracket:
        case ChangelogTokenType.Text:
        case ChangelogTokenType.Space:
        case ChangelogTokenType.NewLine:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isHeadingThreeUnreleasedTextOrNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.Dash:
          {
            int subSectionCollectionCount =
              changelogResult.Value.
                SectionUnreleased.
                  SubSectionCollection.Count;

            changelogResult.Value.
              SectionUnreleased.
                SubSectionCollection[subSectionCollectionCount - 1].
                  ItemCollection.
                    Add(new ChangelogSubSectionItem());

            return true;
          }

        case ChangelogTokenType.CloseParenthesis:
        case ChangelogTokenType.CloseSquareBracket:
        case ChangelogTokenType.OpenParenthesis:
        case ChangelogTokenType.OpenSquareBracket:
        case ChangelogTokenType.Space:
        case ChangelogTokenType.Text:
        case ChangelogTokenType.NewLine:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isHeadingTwo(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.HeadingTwo:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.NewLine:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isText(
      Stack<ChangelogToken> tokenStack
    )
    {
      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.CloseParenthesis:
        case ChangelogTokenType.CloseSquareBracket:
        case ChangelogTokenType.Dash:
        case ChangelogTokenType.OpenParenthesis:
        case ChangelogTokenType.OpenSquareBracket:
        case ChangelogTokenType.Space:
        case ChangelogTokenType.Text:
          {
            return true;
          }
        default:
          {
            return false;
          }
      }
    }

    private static bool isTitle(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return false; }

      ChangelogToken token = tokenStack.Peek();

      switch (token.Type)
      {
        case ChangelogTokenType.CloseParenthesis:
        case ChangelogTokenType.CloseSquareBracket:
        case ChangelogTokenType.Dash:
        case ChangelogTokenType.OpenParenthesis:
        case ChangelogTokenType.OpenSquareBracket:
        case ChangelogTokenType.Space:
        case ChangelogTokenType.Text:
          {
            return true;
          }

        default:
          {
            return false;
          }
      }
    }

    private static bool isEmptySectionUnreleased(
      Result<Changelog> changelogResult
    )
    {
      if (changelogResult.IsFailure) { return false; }

      return string.IsNullOrEmpty(
        changelogResult.Value.SectionUnreleased.MarkdownTitle);
    }

    private static bool isEmptySectionCollection(
      Result<Changelog> changelogResult
    )
    {
      if (changelogResult.IsFailure) { return false; }

      return changelogResult.Value.SectionCollection.Count == 0;
    }

    private static Result<Changelog> parseCloseSquareBracket(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.CloseSquareBracket)
      {
        return Result.Failure<Changelog>(
          $"No close square bracket. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseDash(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      switch (token.Type)
      {
        case ChangelogTokenType.Dash:
          {
          }
          break;

        default:
          {
            return Result.Failure<Changelog>(
              $"No dash. Error parsing text in line {token.LineNumber} / index {token.Index}.");
          }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseDate(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      switch (token.Type)
      {
        case ChangelogTokenType.Date:
          {
            int sectionCollectionCount =
              changelogResult.Value.
                SectionCollection.Count;

            changelogResult.Value.
              SectionCollection[sectionCollectionCount - 1].
                MarkdownDate = token.Value;
          }
          break;

        default:
          {
            return Result.Failure<Changelog>(
              $"No date. Error parsing text in line {token.LineNumber} / index {token.Index}.");
          }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseHeadingOne(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.HeadingOne)
      {
        return Result.Failure<Changelog>(
          $"No level one heading. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseHeadingThree(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      tokenStack.Pop();

      int sectionCollectionCount =
        changelogResult.Value.
          SectionCollection.Count;

      changelogResult.Value.
        SectionCollection[sectionCollectionCount - 1].
          SubSectionCollection.
            Add(new ChangelogSubSection());

      return changelogResult;
    }

    private static Result<Changelog> parseHeadingTwo(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      tokenStack.Pop();

      return changelogResult;
    }

    private static Result<Changelog> parseListTextNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.NewLine)
      {
        return Result.Failure<Changelog>(
          $"No new line. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.NewLine)
      {
        return Result.Failure<Changelog>(
          $"No new line. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseOpenSquareBracket(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.OpenSquareBracket)
      {
        return Result.Failure<Changelog>(
          $"No open square bracket. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseSequenceTerminator(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.SequenceTerminator)
      {
        return Result.Failure<Changelog>(
          $"No sequence terminator. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseSpace(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.Space)
      {
        return Result.Failure<Changelog>(
          $"No space. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return changelogResult;
    }

    private static Result<Changelog> parseText(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      bool first = true;

      while (true)
      {
        ChangelogToken token = tokenStack.Peek();

        if (first == false && token.Type == ChangelogTokenType.NewLine) { break; }
        if (first == false && token.Type == ChangelogTokenType.SequenceTerminator) { break; }

        first = false;

        token = tokenStack.Pop();

        switch (token.Type)
        {
          case ChangelogTokenType.CloseParenthesis:
          case ChangelogTokenType.CloseSquareBracket:
          case ChangelogTokenType.Dash:
          case ChangelogTokenType.OpenParenthesis:
          case ChangelogTokenType.OpenSquareBracket:
          case ChangelogTokenType.Space:
          case ChangelogTokenType.Text:
            {
              changelogResult =
                addTokenValueToText(
                  changelogResult,
                  token);

              if (changelogResult.IsFailure) { return changelogResult; }
            }
            break;

          default:
            {
              return Result.Failure<Changelog>(
                $"Invalid text. Error parsing text in line {token.LineNumber} / index {token.Index}.");
            }
        }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseTextNewLine(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      if (token.Type != ChangelogTokenType.NewLine)
      {
        return Result.Failure<Changelog>(
          $"No new line. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      return addTokenValueToText(
        changelogResult,
        token);
    }

    private static Result<Changelog> parseTitle(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      bool first = true;

      while (true)
      {
        ChangelogToken token = tokenStack.Peek();

        if (first == false && token.Type == ChangelogTokenType.NewLine) { break; }

        first = false;

        token = tokenStack.Pop();

        switch (token.Type)
        {
          case ChangelogTokenType.CloseParenthesis:
          case ChangelogTokenType.CloseSquareBracket:
          case ChangelogTokenType.Dash:
          case ChangelogTokenType.OpenParenthesis:
          case ChangelogTokenType.OpenSquareBracket:
          case ChangelogTokenType.Space:
          case ChangelogTokenType.Text:
            {
              changelogResult =
                addTokenValueToTitleOrSetType(
                  changelogResult,
                  token);

              if (changelogResult.IsFailure) { return changelogResult; }
            }
            break;

          default:
            {
              return Result.Failure<Changelog>(
                $"Invalid title. Error parsing text in line {token.LineNumber} / index {token.Index}.");
            }
        }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseUnreleased(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      switch (token.Type)
      {
        case ChangelogTokenType.Text:
          {
            if (!string.Equals(token.Value, "Unreleased", StringComparison.Ordinal))
            {
              return Result.Failure<Changelog>(
                $"Invalid unreleased title. Error parsing text in line {token.LineNumber} / index {token.Index}.");
            }

            changelogResult.Value.SectionUnreleased.MarkdownTitle = token.Value;
          }
          break;

        default:
          {
            return Result.Failure<Changelog>(
              $"Invalid text. Error parsing text in line {token.LineNumber} / index {token.Index}.");
          }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseUnreleasedHeadingThree(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      tokenStack.Pop();

      changelogResult.Value.
        SectionUnreleased.
          SubSectionCollection.
            Add(new ChangelogSubSection());

      return changelogResult;
    }

    private static Result<Changelog> parseUnreleasedText(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      bool first = true;

      while (true)
      {
        ChangelogToken token = tokenStack.Peek();

        if (first == false && token.Type == ChangelogTokenType.NewLine) { break; }
        if (first == false && token.Type == ChangelogTokenType.SequenceTerminator) { break; }

        first = false;

        token = tokenStack.Pop();

        switch (token.Type)
        {
          case ChangelogTokenType.CloseParenthesis:
          case ChangelogTokenType.CloseSquareBracket:
          case ChangelogTokenType.Dash:
          case ChangelogTokenType.OpenParenthesis:
          case ChangelogTokenType.OpenSquareBracket:
          case ChangelogTokenType.Space:
          case ChangelogTokenType.Text:
            {
              changelogResult =
                addTokenValueToUnreleasedText(
                  changelogResult,
                  token);

              if (changelogResult.IsFailure) { return changelogResult; }
            }
            break;

          default:
            {
              return Result.Failure<Changelog>(
                $"Invalid text. Error parsing text in line {token.LineNumber} / index {token.Index}.");
            }
        }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseUnreleasedTitle(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      bool first = true;

      while (true)
      {
        ChangelogToken token = tokenStack.Peek();

        if (first == false && token.Type == ChangelogTokenType.NewLine) { break; }

        first = false;

        token = tokenStack.Pop();

        switch (token.Type)
        {
          case ChangelogTokenType.CloseParenthesis:
          case ChangelogTokenType.CloseSquareBracket:
          case ChangelogTokenType.Dash:
          case ChangelogTokenType.OpenParenthesis:
          case ChangelogTokenType.OpenSquareBracket:
          case ChangelogTokenType.Space:
          case ChangelogTokenType.Text:
            {
              changelogResult =
                setUnreleasedType(
                  changelogResult,
                  token);

              if (changelogResult.IsFailure) { return changelogResult; }
            }
            break;

          default:
            {
              return Result.Failure<Changelog>(
                $"Invalid title. Error parsing text in line {token.LineNumber} / index {token.Index}.");
            }
        }
      }

      return changelogResult;
    }

    private static Result<Changelog> parseVersion(
      Result<Changelog> changelogResult,
      Stack<ChangelogToken> tokenStack
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      ChangelogToken token = tokenStack.Pop();

      switch (token.Type)
      {
        case ChangelogTokenType.Version:
          {
            changelogResult.Value.
              SectionCollection.
                Add(new ChangelogSection());

            int sectionCollectionCount =
              changelogResult.Value.
                SectionCollection.Count;

            changelogResult.Value.
              SectionCollection[sectionCollectionCount - 1].
                MarkdownVersion = token.Value;
          }
          break;

        default:
          {
            return Result.Failure<Changelog>(
              $"Invalid version. Error parsing text in line {token.LineNumber} / index {token.Index}.");
          }
      }

      return changelogResult;
    }

    private static Result<Changelog> setUnreleasedType(
      Result<Changelog> changelogResult,
      ChangelogToken token
    )
    {
      int subSectionUnreleasedCollectionCount =
        changelogResult.Value.
          SectionUnreleased.
            SubSectionCollection.Count;

      if (!Enum.TryParse(token.Value, out ChangelogSubSectionType subSectionType))
      {
        return Result.Failure<Changelog>(
          $"Invalid subsection type. Error parsing text in line {token.LineNumber} / index {token.Index}.");
      }

      for (int index = 0; index < changelogResult.Value.SectionUnreleased.SubSectionCollection.Count - 1; index++)
      {
        if (changelogResult.Value.SectionUnreleased.SubSectionCollection[index].Type == subSectionType)
        {
          return Result.Failure<Changelog>(
            $"Subsection type already exists. Error parsing text in line {token.LineNumber} / index {token.Index}.");
        }
      }

      changelogResult.Value.
        SectionUnreleased.
          SubSectionCollection[subSectionUnreleasedCollectionCount - 1].
            Type = subSectionType;

      return changelogResult;
    }

    private static Result<Changelog> trimText(
      Result<Changelog> changelogResult
    )
    {
      if (changelogResult.IsFailure) { return changelogResult; }

      changelogResult.Value.MarkdownText =
        changelogResult.Value.MarkdownText.
          Trim(new char[] { '\r', '\n' });

      return changelogResult;
    }

  }

}
