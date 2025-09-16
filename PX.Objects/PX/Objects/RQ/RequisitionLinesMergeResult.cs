// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RequisitionLinesMergeResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.RQ;

/// <summary>
/// The DTO with the results from the merge of the requistion lines.
/// </summary>
public class RequisitionLinesMergeResult
{
  public bool Merged { get; }

  public RQRequisitionLine ResultLine { get; }

  public RequisitionLinesMergeResult(bool merged, RQRequisitionLine resultLine)
  {
    this.Merged = merged;
    this.ResultLine = resultLine;
  }
}
