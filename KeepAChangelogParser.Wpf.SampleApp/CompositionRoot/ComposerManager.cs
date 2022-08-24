using SimpleInjector;

namespace KeepAChangelogParser.Wpf.SampleApp.CompositionRoot
{
  public static class ComposerManager
  {
    public static Container Container { get; set; } = new Container();

    public static void Compose()
    {
      composeAssemblies();
      verifyContainer();
    }

    private static void composeAssemblies()
    {
      Container.ComposeKeepAChangelogParser();
      Container.ComposeTimeStopper();
    }

    private static void verifyContainer()
    {
      Container.Verify();
    }
  }
}
