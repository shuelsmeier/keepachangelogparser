using CSharpFunctionalExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace KeepAChangelogParser.Tests.Comparer
{
  public class ResultComparer<T> :
    IComparer,
    IComparer<Result<T>>
  {
    private readonly IComparer<T> comparer;

    public ResultComparer(
      IComparer<T> comparer
    )
    {
      this.comparer = comparer;
    }

    public int Compare(
      object? x,
      object? y
    )
    {
      if (x is not Result<T>)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(Result<T>));

        throw new ArgumentException(message, nameof(x));
      }

      if (y is not Result<T>)
      {
        string message =
          string.Format(
            CultureInfo.CurrentCulture,
            "Required input {0} is not a recognized {1}.",
            nameof(y), typeof(Result<T>));

        throw new ArgumentException(message, nameof(y));
      }

      int result =
        this.Compare(
          (Result<T>)x,
          (Result<T>)y);

      return result;
    }

    public int Compare(
      Result<T> x,
      Result<T> y
    )
    {
      int result;

      if ((result = compare(x.IsSuccess, y.IsSuccess)) != 0) { return result; }

      if (x.IsSuccess)
      {
        if ((result = this.comparer.Compare(x.Value, y.Value)) != 0) { return result; }
      }

      if (x.IsFailure)
      {
        if ((result = compare(x.Error, y.Error)) != 0) { return result; }
      }

      return result;
    }

    private static int compare(
      string x,
      string y
    )
    {
      return string.CompareOrdinal(x, y);
    }

    private static int compare(
      bool x,
      bool y
    )
    {
      if (x != y) { return 1; }

      return 0;
    }
  }
}
