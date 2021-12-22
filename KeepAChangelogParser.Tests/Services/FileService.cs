using CSharpFunctionalExtensions;
using System;
using System.IO;
using System.Security;

namespace KeepAChangelogParser.Tests.Services
{

  public static class FileService
  {

    public static Result<string> ReadText(
      string filePath
    )
    {
      if (!File.Exists(filePath))
      {
        return Result.Failure<string>(
          $"The file {filePath} does not exist!");
      }

      FileStream? fileStream = null;

      try
      {
        try
        {
          fileStream =
            new FileStream(
              filePath,
              FileMode.Open,
              FileAccess.Read);
        }
        catch (Exception exception)
        when (exception is ArgumentNullException
                        or ArgumentException
                        or NotSupportedException
                        or FileNotFoundException
                        or IOException
                        or SecurityException
                        or DirectoryNotFoundException
                        or UnauthorizedAccessException
                        or PathTooLongException
                        or ArgumentOutOfRangeException)
        {
          return Result.Failure<string>(exception.Message);
        }

        StreamReader? streamReader = null;

        try
        {
          try
          {
            streamReader =
              new StreamReader(
                fileStream);
          }
          catch (Exception exception)
          when (exception is ArgumentException
                          or ArgumentNullException)
          {
            return Result.Failure<string>(exception.Message);
          }

          string text = string.Empty;

          try
          {
            text =
              streamReader.ReadToEnd();

            return Result.Success(text);
          }
          catch (Exception exception)
          when (exception is OutOfMemoryException
                          or IOException)
          {
            return Result.Failure<string>(exception.Message);
          }
        }
        finally
        {
          if (streamReader != null)
          {
            streamReader.Close();
            streamReader.Dispose();
          }
        }
      }
      finally
      {
        if (fileStream != null)
        {
          fileStream.Close();
          fileStream.Dispose();
        }
      }
    }

  }

}
