// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INIssueEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INIssueEntryExt;

public class SpecialOrderCostCenterSupport : SpecialOrderCostCenterSupport<INIssueEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  [PXMergeAttributes]
  [SpecialOrderCostCenterSelector(typeof (INTran.inventoryID), typeof (INTran.siteID), typeof (INTran.invtMult), SOOrderTypeField = typeof (INTran.sOOrderType), SOOrderNbrField = typeof (INTran.sOOrderNbr), SOOrderLineNbrField = typeof (INTran.sOOrderLineNbr), IsSpecialOrderField = typeof (INTran.isSpecialOrder), CostCenterIDField = typeof (INTran.costCenterID), CostLayerTypeField = typeof (INTran.costLayerType), OrigModuleField = typeof (INTran.origModule), ReleasedField = typeof (INTran.released), ProjectIDField = typeof (INTran.projectID), TaskIDField = typeof (INTran.taskID), CostCodeIDField = typeof (INTran.costCodeID), InventorySourceField = typeof (INTran.inventorySource))]
  protected virtual void _(
    Events.CacheAttached<INTran.specialOrderCostCenterID> e)
  {
  }

  protected override void _(Events.RowSelected<INTran> e)
  {
    base._(e);
    if (e.Row == null || !(((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current?.OrigModule == "IN") || ((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current.Released.GetValueOrDefault())
      return;
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) null).For<INTran.uOM>((Action<PXUIFieldAttribute>) (a => a.Enabled = e.Row.CostLayerType != "S"));
  }

  protected virtual void _(Events.FieldVerifying<INTran, INTran.tranType> e)
  {
    this.VerifyTranType(e.Row?.CostLayerType, ((Events.FieldVerifyingBase<Events.FieldVerifying<INTran, INTran.tranType>, INTran, object>) e).NewValue as string, e.Row);
  }

  protected virtual void _(
    Events.FieldVerifying<INTran, INTran.costLayerType> e)
  {
    this.VerifyTranType(((Events.FieldVerifyingBase<Events.FieldVerifying<INTran, INTran.costLayerType>, INTran, object>) e).NewValue as string, e.Row?.TranType, e.Row);
  }

  protected virtual void VerifyTranType(string costLayerType, string tranType, INTran row)
  {
    if (costLayerType == "S" && tranType != "III" && row.OrigModule == "IN")
      throw new PXSetPropertyException("The lines with the cost layer of the Special type can have only the Issue value in the Tran. Type column.");
  }
}
