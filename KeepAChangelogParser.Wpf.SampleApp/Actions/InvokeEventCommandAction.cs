using Microsoft.Xaml.Behaviors;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace KeepAChangelogParser.Wpf.SampleApp.Actions
{
  public class InvokeEventCommandAction :
    TriggerAction<DependencyObject>
  {
    protected override void Invoke(
      object parameter
    )
    {
      if (this.AssociatedObject != null)
      {
        ICommand? command = this.resolveCommand();

        if (command?.CanExecute(parameter) == true)
        {
          command.Execute(parameter);
        }
      }
    }

    private ICommand? resolveCommand()
    {
      ICommand? command = null;

      if (this.Command != null)
      {
        return this.Command;
      }

      if (this.AssociatedObject != null)
      {
        foreach (PropertyInfo info in this.AssociatedObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
          if (typeof(ICommand).IsAssignableFrom(info.PropertyType) && string.Equals(info.Name, this.CommandName, StringComparison.Ordinal))
          {
            command = (ICommand?)info.GetValue(this.AssociatedObject, null);
          }
        }
      }

      return command;
    }

    private string? commandName;

    public string? CommandName
    {
      get
      {
        this.ReadPreamble();
        return this.commandName;
      }
      set
      {
        if (this.CommandName != value)
        {
          this.WritePreamble();
          this.commandName = value;
          this.WritePostscript();
        }
      }
    }

    public ICommand Command
    {
      get => (ICommand)this.GetValue(CommandProperty);
      set => this.SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandProperty =
      DependencyProperty.Register(
        "Command",
        typeof(ICommand),
        typeof(InvokeEventCommandAction),
        new UIPropertyMetadata(null));
  }
}
