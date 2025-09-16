// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractDetailAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CT;

public class ContractDetailAccumAttribute : PXAccumulatorAttribute
{
  public ContractDetailAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    columns.UpdateOnly = true;
    ContractDetailAcum contractDetailAcum = (ContractDetailAcum) row;
    columns.Update<ContractDetailAcum.used>((object) contractDetailAcum.Used, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<ContractDetailAcum.usedTotal>((object) contractDetailAcum.UsedTotal, (PXDataFieldAssign.AssignBehavior) 1);
    return true;
  }
}
