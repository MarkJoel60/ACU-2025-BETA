// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INBarCodeItemLookup`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INBarCodeItemLookup<Filter> : PXFilter<Filter> where Filter : INBarCodeItem, new()
{
  public INBarCodeItemLookup(PXGraph graph)
    : base(graph)
  {
    this.InitHandlers(graph);
  }

  public INBarCodeItemLookup(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.InitHandlers(graph);
  }

  private void InitHandlers(PXGraph graph)
  {
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    Type type1 = typeof (Filter);
    INBarCodeItemLookup<Filter> barCodeItemLookup1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) barCodeItemLookup1, __vmethodptr(barCodeItemLookup1, OnFilterSelected));
    rowSelected.AddHandler(type1, pxRowSelected);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = graph.FieldUpdated;
    Type type2 = typeof (Filter);
    string name1 = typeof (INBarCodeItem.barCode).Name;
    INBarCodeItemLookup<Filter> barCodeItemLookup2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) barCodeItemLookup2, __vmethodptr(barCodeItemLookup2, Filter_BarCode_FieldUpdated));
    fieldUpdated1.AddHandler(type2, name1, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = graph.FieldUpdated;
    Type type3 = typeof (Filter);
    string name2 = typeof (INBarCodeItem.inventoryID).Name;
    INBarCodeItemLookup<Filter> barCodeItemLookup3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) barCodeItemLookup3, __vmethodptr(barCodeItemLookup3, Filter_InventoryID_FieldUpdated));
    fieldUpdated2.AddHandler(type3, name2, pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = graph.FieldUpdated;
    Type type4 = typeof (Filter);
    string name3 = typeof (INBarCodeItem.byOne).Name;
    INBarCodeItemLookup<Filter> barCodeItemLookup4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) barCodeItemLookup4, __vmethodptr(barCodeItemLookup4, Filter_ByOne_FieldUpdated));
    fieldUpdated3.AddHandler(type4, name3, pxFieldUpdated3);
  }

  public virtual void Reset(bool keepDescription)
  {
    Filter current = ((PXSelectBase<Filter>) this).Current;
    ((PXSelectBase) this).Cache.Remove((object) current);
    ((PXSelectBase) this).Cache.Insert(((PXSelectBase) this).Cache.CreateInstance());
    ((PXSelectBase<Filter>) this).Current.ByOne = current.ByOne;
    ((PXSelectBase<Filter>) this).Current.AutoAddLine = current.AutoAddLine;
    if (!keepDescription)
      return;
    ((PXSelectBase<Filter>) this).Current.Description = current.Description;
  }

  protected virtual void Filter_BarCode_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    PXResult<INItemXRef, InventoryItem, INSubItem> pxResult = (PXResult<INItemXRef, InventoryItem, INSubItem>) PXResultset<INItemXRef>.op_Implicit(PXSelectBase<INItemXRef, PXSelectJoin<INItemXRef, InnerJoin<InventoryItem, On2<INItemXRef.FK.InventoryItem, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noRequest>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>>, InnerJoin<INSubItem, On<INItemXRef.FK.SubItem>>>, Where<INItemXRef.alternateID, Equal<Current<INBarCodeItem.barCode>>, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    if (pxResult != null)
    {
      sender.SetValue<INBarCodeItem.inventoryID>(e.Row, (object) null);
      sender.SetValuePending<INBarCodeItem.inventoryID>(e.Row, (object) PXResult<INItemXRef, InventoryItem, INSubItem>.op_Implicit(pxResult).InventoryCD);
      sender.SetValuePending<INBarCodeItem.subItemID>(e.Row, (object) PXResult<INItemXRef, InventoryItem, INSubItem>.op_Implicit(pxResult).SubItemCD);
      INItemXRef inItemXref = PXResult<INItemXRef, InventoryItem, INSubItem>.op_Implicit(pxResult);
      if (string.IsNullOrEmpty(inItemXref.UOM))
        return;
      sender.SetValuePending<INBarCodeItem.uOM>(e.Row, (object) inItemXref.UOM);
    }
    else
    {
      sender.SetValuePending<INBarCodeItem.inventoryID>(e.Row, (object) null);
      sender.SetValuePending<INBarCodeItem.subItemID>(e.Row, (object) null);
    }
  }

  protected virtual void Filter_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    Filter row = e.Row as Filter;
    if (e.OldValue != null && row.InventoryID.HasValue)
      row.BarCode = (string) null;
    sender.SetDefaultExt<INBarCodeItem.subItemID>((object) e);
    sender.SetDefaultExt<INBarCodeItem.qty>((object) e);
  }

  protected virtual void Filter_ByOne_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall || !(e.Row is Filter row) || !row.ByOne.GetValueOrDefault())
      return;
    row.Qty = new Decimal?(1M);
  }

  protected virtual void OnFilterSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is Filter row))
      return;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(((PXSelectBase) this)._Graph, InventoryItem.PK.Find(((PXSelectBase) this)._Graph, row.InventoryID)?.LotSerClassID);
    bool flag = inLotSerClass != null && inLotSerClass.LotSerTrack != "N" && inLotSerClass.LotSerAssign == "R";
    PXUIFieldAttribute.SetEnabled<INBarCodeItem.lotSerialNbr>(sender, (object) null, flag || sender.Graph.IsContractBasedAPI);
    PXDefaultAttribute.SetPersistingCheck<INBarCodeItem.lotSerialNbr>(sender, (object) null, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<INBarCodeItem.expireDate>(sender, (object) null, flag && inLotSerClass.LotSerTrackExpiration.GetValueOrDefault() || sender.Graph.IsContractBasedAPI);
    PXDefaultAttribute.SetPersistingCheck<INBarCodeItem.expireDate>(sender, (object) null, !flag || !inLotSerClass.LotSerTrackExpiration.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXCache pxCache1 = sender;
    int? inventoryId;
    int num1;
    if ((!flag || !(inLotSerClass.LotSerTrack == "S")) && !row.ByOne.GetValueOrDefault())
    {
      inventoryId = row.InventoryID;
      num1 = inventoryId.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<INBarCodeItem.uOM>(pxCache1, (object) null, num1 != 0);
    PXCache pxCache2 = sender;
    int num2;
    if (!string.IsNullOrEmpty(row.BarCode))
    {
      inventoryId = row.InventoryID;
      num2 = !inventoryId.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetEnabled<INBarCodeItem.inventoryID>(pxCache2, (object) null, num2 != 0);
  }
}
