// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt.CostCenterDispatcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;

public class CostCenterDispatcher : 
  CostCenterDispatcher<INRegisterEntryBase, INTran, INTran.costCenterID>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(Events.CacheAttached<INTran.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(Events.CacheAttached<INTran.toCostCenterID> e)
  {
  }

  protected override void SubscribeToFieldsDependOn()
  {
    base.SubscribeToFieldsDependOn();
    PXGraph.FieldUpdatedEvents fieldUpdated1 = ((PXGraph) this.Base).FieldUpdated;
    Type type1 = typeof (INTran);
    string name1 = typeof (INTran.inventorySource).Name;
    CostCenterDispatcher centerDispatcher1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) centerDispatcher1, __vmethodptr(centerDispatcher1, FieldDependOnUpdated));
    fieldUpdated1.AddHandler(type1, name1, pxFieldUpdated1);
    foreach (Type c in ((PXGraph) this.Base).FindAllImplementations<IINTranCostCenterSupport>().SelectMany<IINTranCostCenterSupport, Type>((Func<IINTranCostCenterSupport, IEnumerable<Type>>) (ext => ext.GetDestinationFieldsDependOn())).Append<Type>(typeof (INTran.toInventorySource)))
    {
      if (!typeof (IBqlField).IsAssignableFrom(c) || BqlCommand.GetItemType(c) != typeof (INTran))
        throw new PXArgumentException();
      PXGraph.FieldUpdatedEvents fieldUpdated2 = ((PXGraph) this.Base).FieldUpdated;
      Type type2 = typeof (INTran);
      string name2 = c.Name;
      CostCenterDispatcher centerDispatcher2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) centerDispatcher2, __vmethodptr(centerDispatcher2, FieldDestinationDependOnUpdated));
      fieldUpdated2.AddHandler(type2, name2, pxFieldUpdated2);
    }
  }

  private IINTranCostCenterSupport GetCostCenterSupportByINTran(INTran tran)
  {
    return ((PXGraph) this.Base).FindAllImplementations<IINTranCostCenterSupport>().OrderBy<IINTranCostCenterSupport, int>((Func<IINTranCostCenterSupport, int>) (e => e.SortOrder)).FirstOrDefault<IINTranCostCenterSupport>((Func<IINTranCostCenterSupport, bool>) (e => e.IsSupported(tran)));
  }

  private IINTranCostCenterSupport GetCostCenterSupportByInventorySource(string inventorySource)
  {
    return ((PXGraph) this.Base).FindAllImplementations<IINTranCostCenterSupport>().SingleOrDefault<IINTranCostCenterSupport>((Func<IINTranCostCenterSupport, bool>) (e => e.IsSupported(inventorySource)));
  }

  public void SetInventorySource(INTran tran)
  {
    IINTranCostCenterSupport centerSupportByInTran = this.GetCostCenterSupportByINTran(tran);
    if (centerSupportByInTran == null)
      return;
    INTran inTran1 = tran;
    string str;
    if (inTran1.InventorySource == null)
      inTran1.InventorySource = str = centerSupportByInTran.GetInventorySource(tran);
    INTran inTran2 = tran;
    if (inTran2.ToInventorySource != null)
      return;
    inTran2.ToInventorySource = str = centerSupportByInTran.GetDestinationInventorySource(tran);
  }

  protected virtual void _(
    Events.FieldUpdated<INTran, INTran.inventorySource> e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    string oldValue = (string) ((Events.FieldUpdatedBase<Events.FieldUpdated<INTran, INTran.inventorySource>, INTran, object>) e).OldValue;
    string newValue = (string) e.NewValue;
    if (oldValue == newValue)
      return;
    this.GetCostCenterSupportByInventorySource(oldValue)?.OnInventorySourceChanged(e.Row, newValue, ((Events.FieldUpdatedBase<Events.FieldUpdated<INTran, INTran.inventorySource>>) e).ExternalCall);
  }

  protected virtual void _(
    Events.FieldUpdated<INTran, INTran.toInventorySource> e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    string oldValue = (string) ((Events.FieldUpdatedBase<Events.FieldUpdated<INTran, INTran.toInventorySource>, INTran, object>) e).OldValue;
    string newValue = (string) e.NewValue;
    if (oldValue == newValue)
      return;
    this.GetCostCenterSupportByInventorySource(oldValue)?.OnDestinationInventorySourceChanged(e.Row, newValue, ((Events.FieldUpdatedBase<Events.FieldUpdated<INTran, INTran.toInventorySource>>) e).ExternalCall);
  }

  protected virtual void _(Events.RowPersisting<INTran> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    IINTranCostCenterSupport byInventorySource1 = this.GetCostCenterSupportByInventorySource(e.Row.InventorySource);
    if (byInventorySource1 == null && e.Row.InventorySource != "F")
      throw new InvalidOperationException($"Needed graph extension for Inventory Source = '{e.Row.InventorySource}' was not found.");
    byInventorySource1?.ValidateForPersisting(e.Row);
    IINTranCostCenterSupport byInventorySource2 = this.GetCostCenterSupportByInventorySource(e.Row.ToInventorySource);
    if (byInventorySource2 == null && e.Row.ToInventorySource != "F")
      throw new InvalidOperationException($"Needed graph extension for Inventory Source = '{e.Row.ToInventorySource}' was not found.");
    byInventorySource2?.ValidateDestinationForPersisting(e.Row);
  }

  protected override void FieldDependOnUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    INTran row = (INTran) e.Row;
    IINTranCostCenterSupport byInventorySource = this.GetCostCenterSupportByInventorySource(row.InventorySource);
    INCostCenter costCenter = (byInventorySource != null ? (byInventorySource.IsSpecificCostCenter(row) ? 1 : 0) : 0) != 0 ? byInventorySource.GetCostCenter(row) : (INCostCenter) null;
    int valueOrDefault = ((int?) costCenter?.CostCenterID).GetValueOrDefault();
    int? costCenterId = row.CostCenterID;
    int num = valueOrDefault;
    if (!(costCenterId.GetValueOrDefault() == num & costCenterId.HasValue))
      cache.SetValueExt<INTran.costCenterID>((object) row, (object) valueOrDefault);
    string str = costCenter?.CostLayerType ?? "N";
    if (!(row.CostLayerType != str))
      return;
    cache.SetValueExt<INTran.costLayerType>((object) row, (object) str);
  }

  protected virtual void FieldDestinationDependOnUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    INTran row = (INTran) e.Row;
    IINTranCostCenterSupport byInventorySource = this.GetCostCenterSupportByInventorySource(row.ToInventorySource);
    INCostCenter destinationCostCenter = (byInventorySource != null ? (byInventorySource.IsDestinationSpecificCostCenter(row) ? 1 : 0) : 0) != 0 ? byInventorySource.GetDestinationCostCenter(row) : (INCostCenter) null;
    int valueOrDefault = ((int?) destinationCostCenter?.CostCenterID).GetValueOrDefault();
    int? toCostCenterId = row.ToCostCenterID;
    int num = valueOrDefault;
    if (!(toCostCenterId.GetValueOrDefault() == num & toCostCenterId.HasValue))
      cache.SetValueExt<INTran.toCostCenterID>((object) row, (object) valueOrDefault);
    string str = destinationCostCenter?.CostLayerType ?? "N";
    if (!(row.ToCostLayerType != str))
      return;
    cache.SetValueExt<INTran.toCostLayerType>((object) row, (object) str);
  }
}
