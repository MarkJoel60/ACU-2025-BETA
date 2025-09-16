// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.DAC.RecognizedRecordEntityTypeListWithAnyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CloudServices.DAC;

/// <exclude />
[PXInternalUseOnly]
public class RecognizedRecordEntityTypeListWithAnyAttribute : RecognizedRecordEntityTypeListAttribute
{
  public RecognizedRecordEntityTypeListWithAnyAttribute()
  {
    ((PXStringListAttribute) this)._AllowedValues = ((IEnumerable<string>) new string[1]
    {
      "ANY"
    }).Concat<string>((IEnumerable<string>) ((PXStringListAttribute) this)._AllowedValues).ToArray<string>();
    ((PXStringListAttribute) this)._AllowedLabels = ((IEnumerable<string>) new string[1]
    {
      "All"
    }).Concat<string>((IEnumerable<string>) ((PXStringListAttribute) this)._AllowedLabels).ToArray<string>();
    ((PXStringListAttribute) this)._NeutralAllowedLabels = ((IEnumerable<string>) new string[1]
    {
      "All"
    }).Concat<string>((IEnumerable<string>) ((PXStringListAttribute) this)._NeutralAllowedLabels).ToArray<string>();
  }
}
