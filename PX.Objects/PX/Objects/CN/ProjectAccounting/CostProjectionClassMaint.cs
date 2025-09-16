// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.CostProjectionClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting;

public class CostProjectionClassMaint : PXGraph<CostProjectionClassMaint>
{
  [PXImport(typeof (PMCostProjectionClass))]
  public PXSelect<PMCostProjectionClass> Items;
  public PXSavePerRow<PMCostProjectionClass> Save;
  public PXCancel<PMCostProjectionClass> Cancel;

  protected virtual void _(
    Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.accountGroupID> e)
  {
    bool? accountGroupId = e.Row.AccountGroupID;
    bool? newValue = (bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.accountGroupID>, PMCostProjectionClass, object>) e).NewValue;
    if (accountGroupId.GetValueOrDefault() == newValue.GetValueOrDefault() & accountGroupId.HasValue == newValue.HasValue)
      return;
    this.VerifyAndRaiseException();
  }

  protected virtual void _(
    Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.taskID> e)
  {
    bool? taskId = e.Row.TaskID;
    bool? newValue = (bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.taskID>, PMCostProjectionClass, object>) e).NewValue;
    if (taskId.GetValueOrDefault() == newValue.GetValueOrDefault() & taskId.HasValue == newValue.HasValue)
      return;
    this.VerifyAndRaiseException();
  }

  protected virtual void _(
    Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.inventoryID> e)
  {
    bool? inventoryId = e.Row.InventoryID;
    bool? newValue = (bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.inventoryID>, PMCostProjectionClass, object>) e).NewValue;
    if (inventoryId.GetValueOrDefault() == newValue.GetValueOrDefault() & inventoryId.HasValue == newValue.HasValue)
      return;
    this.VerifyAndRaiseException();
  }

  protected virtual void _(
    Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.costCodeID> e)
  {
    bool? costCodeId = e.Row.CostCodeID;
    bool? newValue = (bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<PMCostProjectionClass, PMCostProjectionClass.costCodeID>, PMCostProjectionClass, object>) e).NewValue;
    if (costCodeId.GetValueOrDefault() == newValue.GetValueOrDefault() & costCodeId.HasValue == newValue.HasValue)
      return;
    this.VerifyAndRaiseException();
  }

  protected virtual void VerifyAndRaiseException()
  {
    if (PXResultset<PMCostProjection>.op_Implicit(((PXSelectBase<PMCostProjection>) new PXSelect<PMCostProjection, Where<PMCostProjection.classID, Equal<Current<PMCostProjectionClass.classID>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())) != null)
      throw new PXSetPropertyException("The class is used and cannot be changed.");
  }
}
