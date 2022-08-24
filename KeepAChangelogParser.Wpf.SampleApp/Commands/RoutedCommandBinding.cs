using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace KeepAChangelogParser.Wpf.SampleApp.Commands
{
  public class RoutedCommandBinding :
    Behavior<FrameworkElement>
  {
    public static readonly DependencyProperty CommandProperty =
      DependencyProperty.Register(
        "Command",
        typeof(ICommand),
        typeof(RoutedCommandBinding),
        new PropertyMetadata(default(ICommand)));

    public ICommand Command
    {
      get { return (ICommand)this.GetValue(CommandProperty); }
      set { this.SetValue(CommandProperty, value); }
    }

    public ICommand? RoutedCommand { get; set; }

    protected override void OnAttached()
    {
      base.OnAttached();

      CommandBinding? binding =
        new CommandBinding(
          this.RoutedCommand,
          this.handleExecuted,
          this.handleCanExecute);

      _ = this.AssociatedObject.CommandBindings.Add(binding);
    }

    private void handleCanExecute(
      object sender,
      CanExecuteRoutedEventArgs canExecuteRoutedEventArgs
    )
    {
      canExecuteRoutedEventArgs.CanExecute =
        this.Command?.CanExecute(canExecuteRoutedEventArgs.Parameter) == true;

      canExecuteRoutedEventArgs.Handled = true;
    }

    private void handleExecuted(
      object sender,
      ExecutedRoutedEventArgs executedRoutedEventArgs
    )
    {
      this.Command?.Execute(executedRoutedEventArgs.Parameter);

      executedRoutedEventArgs.Handled = true;
    }
  }
}
