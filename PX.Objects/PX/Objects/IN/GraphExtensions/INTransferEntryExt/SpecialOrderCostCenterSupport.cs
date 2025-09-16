// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INTransferEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INTransferEntryExt;

public class SpecialOrderCostCenterSupport : SpecialOrderCostCenterSupport<INTransferEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  [PXMergeAttributes]
  [PXUIField(DisplayName = "From Inventory Source")]
  protected virtual void _(Events.CacheAttached<INTran.inventorySource> e)
  {
  }

  [PXMergeAttributes]
  [SpecialOrderCostCenterSelector(typeof (INTran.inventoryID), typeof (INTran.siteID), typeof (INTran.invtMult), SOOrderTypeField = typeof (INTran.sOOrderType), SOOrderNbrField = typeof (INTran.sOOrderNbr), SOOrderLineNbrField = typeof (INTran.sOOrderLineNbr), IsSpecialOrderField = typeof (INTran.isSpecialOrder), CostCenterIDField = typeof (INTran.costCenterID), CostLayerTypeField = typeof (INTran.costLayerType), OrigModuleField = typeof (INTran.origModule), ReleasedField = typeof (INTran.released), ProjectIDField = typeof (INTran.projectID), TaskIDField = typeof (INTran.taskID), CostCodeIDField = typeof (INTran.costCodeID), InventorySourceField = typeof (INTran.inventorySource))]
  protected virtual void _(
    Events.CacheAttached<INTran.specialOrderCostCenterID> e)
  {
  }

  protected virtual void _(Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    bool inModule = e.Row.OrigModule == "IN";
    bool oneStep = e.Row.TransferType == "1";
    PXCacheEx.AdjustUI(((PXSelectBase) this.Base.transactions).Cache, (object) null).For<INTran.specialOrderCostCenterID>((Action<PXUIFieldAttribute>) (a => a.Visible = !inModule | oneStep)).For<INTran.toSpecialOrderCostCenterID>((Action<PXUIFieldAttribute>) (a => a.Visible = oneStep));
  }

  protected override void _(Events.RowSelected<INTran> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    INRegister current = ((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current;
    bool inModule = current?.OrigModule == "IN";
    bool oneStep = current?.TransferType == "1";
    bool released = current != null && current.Released.GetValueOrDefault();
    PXCacheEx.Adjust<CostLayerType.ListAttribute>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) e.Row).For<INTran.costLayerType>((Action<CostLayerType.ListAttribute>) (a => a.AllowSpecialOrders = !inModule | oneStep)).For<INTran.toCostLayerType>((Action<CostLayerType.ListAttribute>) (a => a.AllowSpecialOrders = e.Row.CostLayerType == "S"));
    int? numberOfValues = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache.GetAttributes<INTran.costLayerType>((object) e.Row).OfType<CostLayerType.ListAttribute>().FirstOrDefault<CostLayerType.ListAttribute>()?.SetValues(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) e.Row);
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache.GetAttributes<INTran.toCostLayerType>((object) e.Row).OfType<CostLayerType.ListAttribute>().FirstOrDefault<CostLayerType.ListAttribute>()?.SetValues(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) e.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) e.Row);
    attributeAdjuster.For<INTran.inventorySource>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      int? nullable = numberOfValues;
      int num1 = 1;
      int num2 = (!(nullable.GetValueOrDefault() > num1 & nullable.HasValue) ? 0 : (!released ? 1 : 0)) & (inModule ? 1 : 0);
      pxuiFieldAttribute.Enabled = num2 != 0;
    })).For<INTran.toCostCodeID>((Action<PXUIFieldAttribute>) (a => a.Enabled = e.Row.ToCostLayerType != "S"));
    if (!inModule || released)
      return;
    attributeAdjuster = PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) null);
    attributeAdjuster.For<INTran.uOM>((Action<PXUIFieldAttribute>) (a => a.Enabled = e.Row.CostLayerType != "S"));
  }

  [PXOverride]
  public virtual (bool? ToProject, bool? ToTask) IsToProjectTaskEnabled(
    INTran row,
    Func<INTran, (bool? ToProject, bool? ToTask)> baseMethod)
  {
    (bool?, bool?) valueTuple = baseMethod(row);
    return (new bool?((valueTuple.Item1 ?? true) && row.ToCostLayerType != "S"), new bool?((valueTuple.Item2 ?? true) && row.ToCostLayerType != "S"));
  }

  protected virtual void _(Events.RowUpdated<INRegister> e)
  {
    if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INRegister>>) e).Cache.ObjectsEqual<INRegister.transferType>((object) e.OldRow, (object) e.Row))
    {
      foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) this.Base.transactions).Select(Array.Empty<object>()))
      {
        INTran inTran = PXResult<INTran>.op_Implicit(pxResult);
        if (EnumerableExtensions.IsIn<string>("S", inTran.CostLayerType, inTran.ToCostLayerType))
        {
          if (inTran.CostLayerType == "S")
            inTran.CostLayerType = "N";
          if (inTran.ToCostLayerType == "S")
            inTran.ToCostLayerType = "N";
          ((PXSelectBase<INTran>) this.Base.transactions).Update(inTran);
        }
      }
      ((PXSelectBase) this.Base.transactions).View.RequestRefresh();
    }
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INRegister>>) e).Cache.ObjectsEqual<INRegister.siteID, INRegister.toSiteID>((object) e.OldRow, (object) e.Row))
      return;
    foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) this.Base.transactions).Select(Array.Empty<object>()))
    {
      INTran row = PXResult<INTran>.op_Implicit(pxResult);
      PXCache cache = ((PXSelectBase) this.Base.transactions).Cache;
      GraphHelper.MarkUpdated(cache, (object) row);
      cache.VerifyFieldAndRaiseException<INTran.costLayerType>((object) row);
    }
    ((PXSelectBase) this.Base.transactions).View.RequestRefresh();
  }

  protected virtual void _(Events.RowPersisting<INTran> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INTran>>) e).Cache.VerifyFieldAndRaiseException<INTran.costLayerType>((object) e.Row);
  }

  protected virtual void _(
    Events.FieldVerifying<INTran, INTran.costLayerType> e)
  {
    INRegister current = ((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current;
    if (!object.Equals(((Events.FieldVerifyingBase<Events.FieldVerifying<INTran, INTran.costLayerType>, INTran, object>) e).NewValue, (object) "S") || !(e.Row.OrigModule == "IN") || current == null)
      return;
    if (current.TransferType != "1")
      throw new PXSetPropertyException("You can select the cost layer of the Special type only for one-step transfers.");
    if (!current.SiteID.HasValue || !current.ToSiteID.HasValue)
      return;
    int? siteId = current.SiteID;
    int? toSiteId = current.ToSiteID;
    if (!(siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue))
      throw new PXSetPropertyException("Special-order items cannot be transferred between warehouses on this form. To transfer the special-order items, use the Create Transfer Orders (SO509000) form.");
  }

  protected virtual void _(
    Events.FieldUpdated<INTran, INTran.costLayerType> e)
  {
    if (e.Row.CostLayerType != "S" && e.Row.ToCostLayerType == "S")
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INTran, INTran.costLayerType>>) e).Cache.SetValueExt<INTran.toCostLayerType>((object) e.Row, (object) "N");
    if (!(e.Row.CostLayerType == "S") || !EnumerableExtensions.IsNotIn<string>(e.Row.ToCostLayerType, "S", "N"))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INTran, INTran.costLayerType>>) e).Cache.SetValueExt<INTran.toCostLayerType>((object) e.Row, (object) "N");
  }
}
