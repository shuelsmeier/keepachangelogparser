namespace KeepAChangelogParser.Models
{
  internal enum ChangelogTokenType
  {
    Asterisk,
    CloseParenthesis,
    CloseSquareBracket,
    Dash,
    Date,
    HeadingOne,
    HeadingTwo,
    HeadingThree,
    NewLine,
    OpenParenthesis,
    OpenSquareBracket,
    SequenceTerminator,
    Space,
    Text,
    SemanticVersion,
    MicrosoftVersion
  }
}
