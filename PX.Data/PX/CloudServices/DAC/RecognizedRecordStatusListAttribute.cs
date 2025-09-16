// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.DAC.RecognizedRecordStatusListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CloudServices.DAC;

[PXInternalUseOnly]
public class RecognizedRecordStatusListAttribute : PXStringListAttribute
{
  public const string PendingRecognition = "N";
  public const string InProgress = "I";
  public const string Recognized = "R";
  public const string Processed = "P";
  public const string Error = "E";
  private static readonly string[] _values = new string[5]
  {
    "N",
    "I",
    "R",
    "P",
    "E"
  };
  private static readonly string[] _labels = new string[5]
  {
    "Pending Recognition",
    "In Progress",
    nameof (Recognized),
    nameof (Processed),
    nameof (Error)
  };

  public RecognizedRecordStatusListAttribute()
    : base(RecognizedRecordStatusListAttribute._values, RecognizedRecordStatusListAttribute._labels)
  {
  }

  protected RecognizedRecordStatusListAttribute(
    IEnumerable<string> additionalValues,
    IEnumerable<string> additionalLabels)
    : base(((IEnumerable<string>) RecognizedRecordStatusListAttribute._values).Concat<string>(additionalValues).Distinct<string>().ToArray<string>(), ((IEnumerable<string>) RecognizedRecordStatusListAttribute._labels).Concat<string>(additionalLabels).Distinct<string>().ToArray<string>())
  {
  }
}
