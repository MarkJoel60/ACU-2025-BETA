// Decompiled with JetBrains decompiler
// Type: PX.SM.MergedInspectingTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Inspecting;
using PX.Data.ReferentialIntegrity.Merging;
using System;

#nullable disable
namespace PX.SM;

[PXHidden]
[Serializable]
public class MergedInspectingTable : InspectingTable
{
  public MergedInspectingTable()
  {
  }

  public MergedInspectingTable(
    MergedReferencesInspectionResult mergedInspectionResult)
    : base(mergedInspectionResult.ReferencesInspectionResult)
  {
    this.MergedInspectionResult = mergedInspectionResult;
  }

  public MergedReferencesInspectionResult MergedInspectionResult { get; }

  public override ReferencesInspectionResult InspectionResult
  {
    get => this.MergedInspectionResult?.ReferencesInspectionResult;
  }
}
