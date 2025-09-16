// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRExpenseAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.DR;

public class DRExpenseAccumAttribute : PXAccumulatorAttribute
{
  public DRExpenseAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    DRExpenseProjectionAccum expenseProjectionAccum = (DRExpenseProjectionAccum) row;
    columns.Update<DRExpenseProjectionAccum.pTDProjected>((object) expenseProjectionAccum.PTDProjected, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<DRExpenseProjectionAccum.pTDRecognized>((object) expenseProjectionAccum.PTDRecognized, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<DRExpenseProjectionAccum.pTDRecognizedSamePeriod>((object) expenseProjectionAccum.PTDRecognizedSamePeriod, (PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }
}
