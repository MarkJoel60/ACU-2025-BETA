// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INAdjustmentEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INAdjustmentEntryExt;

public class SpecialOrderCostCenterSupport : SpecialOrderCostCenterSupport<INAdjustmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (SpecialOrderCostCenterSelectorAttribute))]
  [AdjustmentSpecialOrderCostCenterSelector]
  protected virtual void _(
    Events.CacheAttached<INTran.specialOrderCostCenterID> e)
  {
  }

  protected virtual void _(Events.FieldVerifying<INTran, INTran.qty> e)
  {
    this.VerifyQty(e.Row?.CostLayerType, ((Events.FieldVerifyingBase<Events.FieldVerifying<INTran, INTran.qty>, INTran, object>) e).NewValue as Decimal?);
  }

  protected virtual void _(
    Events.FieldVerifying<INTran, INTran.costLayerType> e)
  {
    this.VerifyQty(((Events.FieldVerifyingBase<Events.FieldVerifying<INTran, INTran.costLayerType>, INTran, object>) e).NewValue as string, (Decimal?) e.Row?.Qty);
  }

  protected virtual void VerifyQty(string costLayerType, Decimal? qty)
  {
    if (!(costLayerType == "S"))
      return;
    Decimal? nullable = qty;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      throw new PXSetPropertyException("A positive adjustment cannot be made for a line with the cost layer of the Special type.");
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.INAdjustmentEntry.GetUOMEnabled(System.Boolean,PX.Objects.IN.INTran)" />
  /// </summary>
  [PXOverride]
  public virtual bool GetUOMEnabled(
    bool isPIAdjustment,
    INTran tran,
    Func<bool, INTran, bool> baseMethod)
  {
    return baseMethod(isPIAdjustment, tran) && tran?.CostLayerType != "S";
  }
}
