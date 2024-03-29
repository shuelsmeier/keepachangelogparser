﻿using Microsoft.Xaml.Behaviors;
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
      if (base.AssociatedObject != null)
      {
        ICommand? command = this.resolveCommand();

        if ((command != null) && command.CanExecute(parameter))
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

      if (base.AssociatedObject != null)
      {
        foreach (PropertyInfo info in base.AssociatedObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
          if (typeof(ICommand).IsAssignableFrom(info.PropertyType) && string.Equals(info.Name, this.CommandName, StringComparison.Ordinal))
          {
            command = (ICommand?)info.GetValue(base.AssociatedObject, null);
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
        base.ReadPreamble();
        return this.commandName;
      }
      set
      {
        if (this.CommandName != value)
        {
          base.WritePreamble();
          this.commandName = value;
          base.WritePostscript();
        }
      }
    }

    public ICommand Command
    {
      get { return (ICommand)this.GetValue(CommandProperty); }
      set { this.SetValue(CommandProperty, value); }
    }

    public static readonly DependencyProperty CommandProperty =
      DependencyProperty.Register(
        "Command",
        typeof(ICommand),
        typeof(InvokeEventCommandAction),
        new UIPropertyMetadata(null));

  }

}
