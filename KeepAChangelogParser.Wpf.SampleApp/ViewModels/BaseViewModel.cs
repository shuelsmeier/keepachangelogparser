using System;
using System.ComponentModel;
using System.Diagnostics;

namespace KeepAChangelogParser.Wpf.SampleApp.ViewModels
{

  public abstract class BaseViewModel :
    INotifyPropertyChanged,
    IDisposable
  {

    private bool isDisposed;

    protected BaseViewModel()
    {
    }

    public virtual string? DisplayName { get; protected set; }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public void VerifyPropertyName(
      string propertyName
    )
    {
      if (TypeDescriptor.GetProperties(this)[propertyName] == null)
      {
        string msg = "Invalid property name: " + propertyName;

        if (this.ThrowOnInvalidPropertyName)
          throw new Exception(msg);
        else
          Debug.Fail(msg);
      }
    }

    protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged(
      string propertyName
    )
    {
      this.VerifyPropertyName(propertyName);

      PropertyChangedEventHandler? handler = this.PropertyChanged;
      if (handler != null)
      {
        var e = new PropertyChangedEventArgs(propertyName);
        handler(this, e);
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(
      bool disposing
    )
    {
      if (this.isDisposed) return;

      if (disposing)
      {
        // free managed resources
      }

      // free native resources if there are any.

      this.isDisposed = true;
    }

  }

}
