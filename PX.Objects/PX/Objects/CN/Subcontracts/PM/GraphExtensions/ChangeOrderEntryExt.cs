// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PM.GraphExtensions.ChangeOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PM.GraphExtensions;

public class ChangeOrderEntryExt : PXGraphExtension<ChangeOrderEntry>
{
  public virtual void Initialize() => this.SetDisplayNames();

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXOverride]
  public virtual POOrderEntry CreateTarget(PX.Objects.PO.POOrder order)
  {
    return !(order.OrderType == "RS") ? PXGraph.CreateInstance<POOrderEntry>() : (POOrderEntry) ChangeOrderEntryExt.CreateSubcontractEntry();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewCommitments(PXAdapter adapter)
  {
    if (this.GetCurrentCommitmentType() == "RS")
    {
      SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
      ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = this.GetCurrentPurchaseOrder();
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return this.Base.ViewCommitments(adapter);
  }

  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXSelector(typeof (Search4<PX.Objects.PO.POLine.orderNbr, Where<PX.Objects.PO.POLine.orderType, In3<POOrderType.regularOrder, POOrderType.regularSubcontract, POOrderType.projectDropShip>, And<PX.Objects.PO.POLine.projectID, Equal<Current<PMChangeOrder.projectID>>, And2<Where<PX.Objects.PO.POLine.cancelled, Equal<False>, Or<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, Equal<True>>>, And2<Where<PX.Objects.PO.POLine.completed, Equal<False>, Or<Current<ChangeOrderEntry.POLineFilter.includeNonOpen>, Equal<True>>>, And<Where<Current<ChangeOrderEntry.POLineFilter.vendorID>, IsNull, Or<PX.Objects.PO.POLine.vendorID, Equal<Current<ChangeOrderEntry.POLineFilter.vendorID>>>>>>>>>, Aggregate<GroupBy<PX.Objects.PO.POLine.orderType, GroupBy<PX.Objects.PO.POLine.orderNbr, GroupBy<PX.Objects.PO.POLine.vendorID>>>>>), new Type[] {typeof (PX.Objects.PO.POLine.orderType), typeof (PX.Objects.PO.POLine.orderNbr), typeof (PX.Objects.PO.POLine.vendorID)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ChangeOrderEntry.POLineFilter.pOOrderNbr> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXUIFieldAttribute))]
  [PXUIField(DisplayName = "Commitment Type", Enabled = true)]
  [POOrderType.RPSList]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderLine.pOOrderType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMChangeOrderLine> args)
  {
    PMChangeOrderLine row = args.Row;
    if (row == null || args.Operation == 3 || !(row.POOrderType != "RO") || !(row.POOrderType != "PD"))
      return;
    this.ValidateInventoryItem(args, row);
  }

  private static SubcontractEntry CreateSubcontractEntry()
  {
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    PXCacheEx.GetExtension<PoSetupExt>((IBqlTable) ((PXSelectBase<POSetup>) instance.POSetup).Current).RequireSubcontractControlTotal = new bool?(false);
    return instance;
  }

  private void ValidateInventoryItem(
    PX.Data.Events.RowPersisting<PMChangeOrderLine> args,
    PMChangeOrderLine line)
  {
    PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(line.InventoryID);
    if (inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() && !inventoryItem.NonStockReceipt.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMChangeOrderLine>>) args).Cache.RaiseExceptionHandling<PMChangeOrderLine.inventoryID>((object) line, (object) inventoryItem.InventoryCD, (Exception) new PXSetPropertyException("Item should not require receipt.", (PXErrorLevel) 4));
  }

  private PX.Objects.IN.InventoryItem GetInventoryItem(int? inventoryId)
  {
    return ((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) inventoryId
    });
  }

  private void SetDisplayNames()
  {
    PXUIFieldAttribute.SetDisplayName<PMChangeOrderLine.pOOrderNbr>(((PXSelectBase) this.Base.Details).Cache, "Commitment Nbr.");
    PXUIFieldAttribute.SetDisplayName<PMChangeOrderLine.pOLineNbr>(((PXSelectBase) this.Base.Details).Cache, "Commitment Line Nbr.");
    PXUIFieldAttribute.SetDisplayName<ChangeOrderEntry.POLineFilter.pOOrderNbr>(((PXSelectBase) this.Base.AvailablePOLineFilter).Cache, "Commitment Nbr.");
    PXUIFieldAttribute.SetDisplayName<POLinePM.orderType>(((PXSelectBase) this.Base.AvailablePOLines).Cache, "Commitment Type");
    PXUIFieldAttribute.SetDisplayName<POLinePM.orderNbr>(((PXSelectBase) this.Base.AvailablePOLines).Cache, "Commitment Nbr.");
    PXUIFieldAttribute.SetDisplayName<POLinePM.lineNbr>(((PXSelectBase) this.Base.AvailablePOLines).Cache, "Commitment Line Nbr.");
  }

  private string GetCurrentCommitmentType()
  {
    return ((PXSelectBase<PMChangeOrderLine>) this.Base.Details).Current == null ? (string) null : ((PXSelectBase<PMChangeOrderLine>) this.Base.Details).Current.POOrderType;
  }

  private PX.Objects.PO.POOrder GetCurrentPurchaseOrder()
  {
    return ((PXSelectBase<PX.Objects.PO.POOrder>) new PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<PMChangeOrderLine.pOOrderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Current<PMChangeOrderLine.pOOrderNbr>>>>>((PXGraph) this.Base)).SelectSingle(Array.Empty<object>());
  }
}
