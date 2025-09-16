// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaskAllocTotalAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public class TaskAllocTotalAccumAttribute : PXAccumulatorAttribute
{
  public TaskAllocTotalAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMTaskAllocTotal pmTaskAllocTotal = (PMTaskAllocTotal) row;
    columns.Update<PMTaskAllocTotal.amount>((object) pmTaskAllocTotal.Amount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMTaskAllocTotal.quantity>((object) pmTaskAllocTotal.Quantity, (PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }
}
