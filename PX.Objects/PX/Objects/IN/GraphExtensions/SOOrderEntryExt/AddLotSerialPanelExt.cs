// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.SOOrderEntryExt.AddLotSerialPanelExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.SOOrderEntryExt;

public class AddLotSerialPanelExt : 
  AddLotSerialPanelExtBase<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<Current<PX.Objects.SO.SOOrder.orderType>, PX.Objects.SO.SOOrderType.requireLocation>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<AddLotSerialHeader.showLocation> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (PX.Objects.SO.SOOrder.branchID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<AddLotSerialHeader.branchID> e)
  {
  }

  protected override bool IsDocumentReadonly(PX.Objects.SO.SOOrder document)
  {
    return !((PXSelectBase) this.Base.Transactions).AllowInsert || !this.IsAddLotSerialNbrButtonVisibleForDocument(document);
  }

  protected override PX.Objects.SO.SOLine InsertLine(
    PX.Objects.SO.SOLine newLine,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    PX.Objects.SO.SOOrderType current = ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current;
    newLine.InventoryID = lotSerial.InventoryID;
    newLine.SiteID = lotSerial.SiteID;
    newLine.UOM = lotSerial.BaseUnit;
    if (current == null || current.RequireLocation.GetValueOrDefault() || current.RequireLotSerial.GetValueOrDefault())
      newLine.LocationID = lotSerial.LocationID;
    newLine.TranDesc = lotSerial.Descr;
    return ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(newLine);
  }

  protected override PX.Objects.SO.SOLineSplit InsertSplit(
    PX.Objects.SO.SOLineSplit newSplit,
    PX.Objects.SO.SOLine line,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    PX.Objects.SO.SOLineSplit soLineSplit1 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Insert(newSplit);
    soLineSplit1.LotSerialNbr = lotSerial.LotSerialNbr;
    soLineSplit1.IsAllocated = new bool?(true);
    PX.Objects.SO.SOLineSplit soLineSplit2 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(soLineSplit1);
    soLineSplit2.Qty = lotSerial.QtySelected;
    return ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(soLineSplit2);
  }

  protected override void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    ((PXAction) this.showAddLotSerialNbrPanel).SetVisible(this.IsAddLotSerialNbrButtonVisibleForDocument(e.Row));
  }

  protected virtual bool IsAddLotSerialNbrButtonVisibleForDocument(PX.Objects.SO.SOOrder document)
  {
    return EnumerableExtensions.IsNotIn<string>(document.OrderType, "BL", "QT");
  }
}
