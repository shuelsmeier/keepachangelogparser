using Ardalis.GuardClauses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace KeepAChangelogParser.Tests.Extensions
{
  public static class AssertExtensions
  {
    [SuppressMessage(
      "Style",
      "IDE0060:Nicht verwendete Parameter entfernen",
      Justification = "Suppress message for necessary assert extension parameter.")]
    public static void AreEqual<T>(
      this Assert assert,
      T expected,
      T actual,
      IComparer comparer
    )
    {
      Guard.Against.Null(comparer, nameof(comparer));

      int compareResult =
        comparer.Compare(
          expected,
          actual);

      if (compareResult == 0)
      {
        return;
      }

      List<string> expectedJsonStringCollection = serializeToJson(expected);
      List<string> actualJsonStringCollection = serializeToJson(actual);

      int expectedMaxStringLength =
        expectedJsonStringCollection.
          Max(x => x.Length);

      int actualMaxStringLength =
        actualJsonStringCollection.
          Max(x => x.Length);

      int chunkSize = 80;

      bool isCutLength =
           expectedMaxStringLength > chunkSize
        || actualMaxStringLength > chunkSize;

      if (isCutLength)
      {
        expectedMaxStringLength =
          expectedMaxStringLength > chunkSize ?
          chunkSize :
          expectedMaxStringLength;

        actualMaxStringLength =
          actualMaxStringLength > chunkSize ?
          chunkSize :
          actualMaxStringLength;

        List<CutElement>? expectedJsonStringCollectionToCut =
          expectedJsonStringCollection.
            Select((x, index) => new CutElement() { Element = x, Index = index }).
            Where(x => x.Element.Length > chunkSize).
            ToList();

        for (int index1 = 0; index1 < expectedJsonStringCollectionToCut.Count; index1++)
        {
          CutElement? x = expectedJsonStringCollectionToCut[index1];

          IEnumerable<string> lines =
            Enumerable.
              Range(0, (x.Element.Length / chunkSize) + 1).
              Select(i => x.Element.
                Substring(
                  i * chunkSize,
                  x.Element.Length - (chunkSize * i) >= chunkSize ? chunkSize : x.Element.Length - (chunkSize * i)));

          int indexToAdd =
            lines.Count();

          for (int index2 = index1 + 1; index2 < expectedJsonStringCollectionToCut.Count; index2++)
          {
            expectedJsonStringCollectionToCut[index2].Index += indexToAdd - 1;
          }

          expectedJsonStringCollection.
          RemoveAt(x.Index);

          expectedJsonStringCollection.
            InsertRange(x.Index, lines);

          if (x.Index < actualJsonStringCollection.Count)
          {
            Enumerable.
              Range(0, lines.Count() - 1).
              ToList().
              ForEach(y => actualJsonStringCollection.Insert(x.Index + 1, ""));
          }
        }

        var actualJsonStringCollectionToCut =
          actualJsonStringCollection.
            Select((x, index) => new { Element = x, Index = index }).
            Where(x => x.Element.Length > chunkSize).
            ToList();

        foreach (var x in actualJsonStringCollectionToCut)
        {
          List<string> lines =
            Enumerable.
              Range(0, (x.Element.Length / chunkSize) + 1).
              Select(i => x.Element.
                Substring(
                  i * chunkSize,
                  x.Element.Length - (chunkSize * i) >= chunkSize ? chunkSize : x.Element.Length - (chunkSize * i))).
              ToList();

          actualJsonStringCollection.
            RemoveAt(x.Index);

          actualJsonStringCollection.
            Insert(x.Index, lines[0]);

          for (int i = 1; i < lines.Count; i++)
          {
            if (string.IsNullOrEmpty(actualJsonStringCollection[x.Index + i]))
            {
              actualJsonStringCollection.
                RemoveAt(x.Index + i);
            }
            else
            {
              if (x.Index < expectedJsonStringCollection.Count)
              {
                expectedJsonStringCollection.
                  Insert(x.Index + i, "");
              }
            }

            actualJsonStringCollection.
              Insert(x.Index + i, lines[i]);
          }
        }
      }

      int maxJsonTringCollectionCount =
      expectedJsonStringCollection.Count > actualJsonStringCollection.Count ?
      expectedJsonStringCollection.Count :
      actualJsonStringCollection.Count;

      string message = Environment.NewLine;

      string expectedHeader =
        "Expected".
        PadRight(expectedMaxStringLength, ' ');

      string expectedLine =
        "--------".
        PadRight(expectedMaxStringLength, '-');

      string actualLine =
        "------".
        PadRight(actualMaxStringLength, '-');

      message += $"    | {expectedHeader} | Actual{Environment.NewLine}";
      message += $"----+-{expectedLine}-+-{actualLine}-{Environment.NewLine}";

      for (int index = 0; index < maxJsonTringCollectionCount; index++)
      {
        string expectedJsonStringLine =
          index < expectedJsonStringCollection.Count ?
          expectedJsonStringCollection[index] :
          string.Empty;

        string actualJsonStringLine =
          index < actualJsonStringCollection.Count ?
          actualJsonStringCollection[index] :
          string.Empty;

        bool isDifferent =
          !string.Equals(
            expectedJsonStringLine,
            actualJsonStringLine,
            StringComparison.Ordinal);

        message +=
          isDifferent ?
          $"--> | {expectedJsonStringLine.PadRight(expectedMaxStringLength, ' ')} | {actualJsonStringLine}{Environment.NewLine}" :
          $"    | {expectedJsonStringLine.PadRight(expectedMaxStringLength, ' ')} | {actualJsonStringLine}{Environment.NewLine}";
      }

      Assert.IsTrue(false, message);
    }

    private static List<string> serializeToJson<T>(
      T t
    )
    {
      JsonSerializer jsonSerializer = JsonSerializer.Create(
        new JsonSerializerSettings()
        {
          Formatting = Formatting.Indented
        });

      StringBuilder stringBuilder = new StringBuilder();

      using StringWriter stringWriter = new StringWriter(stringBuilder);

      using JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter);

      jsonSerializer.Serialize(jsonTextWriter, t);

      List<string> JsonStringCollection =
        stringBuilder.
          ToString().
          Split(Environment.NewLine).
          ToList();

      return JsonStringCollection;
    }

    private class CutElement
    {
      public string Element { get; set; } = string.Empty;
      public int Index { get; set; } = 0;
    }
  }
}
