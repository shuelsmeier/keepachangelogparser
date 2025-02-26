using Ardalis.GuardClauses;
using SimpleInjector;

namespace KeepAChangelogParser.Wpf.SampleApp.CompositionRoot
{
  public static class KeepAChangelogParserComposer
  {
    public static void ComposeKeepAChangelogParser(
      this Container container
    )
    {
      _ = Guard.Against.Null(container, nameof(container));

      container.Register<IChangelogParser, ChangelogParser>(Lifestyle.Singleton);
    }
  }
}
