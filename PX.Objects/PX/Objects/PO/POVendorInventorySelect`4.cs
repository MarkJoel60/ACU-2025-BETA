// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorInventorySelect`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

public class POVendorInventorySelect<Table, Join, Where, PrimaryType> : 
  PXSelectJoin<Table, Join, Where>
  where Table : POVendorInventory, new()
  where Join : class, IBqlJoin, new()
  where Where : IBqlWhere, new()
  where PrimaryType : class, IBqlTable, new()
{
  protected const string _UPDATEVENDORPRICE_COMMAND = "UpdateVendorPrice";
  protected const string _UPDATEVENDORPRICE_VIEW = "VendorInventory$UpdatePrice";

  public POVendorInventorySelect(PXGraph graph)
    : base(graph)
  {
    graph.Views.Caches.Add(typeof (INItemXRef));
    PXGraph.RowSelectedEvents rowSelected1 = graph.RowSelected;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected1 = new PXRowSelected((object) vendorInventorySelect1, __vmethodptr(vendorInventorySelect1, OnRowSelected));
    rowSelected1.AddHandler<Table>(pxRowSelected1);
    PXGraph.RowInsertedEvents rowInserted = graph.RowInserted;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect2 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) vendorInventorySelect2, __vmethodptr(vendorInventorySelect2, OnRowInserted));
    rowInserted.AddHandler<Table>(pxRowInserted);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect3 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) vendorInventorySelect3, __vmethodptr(vendorInventorySelect3, OnRowUpdated));
    rowUpdated.AddHandler<Table>(pxRowUpdated);
    PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect4 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) vendorInventorySelect4, __vmethodptr(vendorInventorySelect4, OnRowDeleted));
    rowDeleted.AddHandler<Table>(pxRowDeleted);
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect5 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) vendorInventorySelect5, __vmethodptr(vendorInventorySelect5, OnRowPersisting));
    rowPersisting.AddHandler<Table>(pxRowPersisting);
    PXGraph.RowSelectedEvents rowSelected2 = graph.RowSelected;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect6 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected2 = new PXRowSelected((object) vendorInventorySelect6, __vmethodptr(vendorInventorySelect6, OnParentRowSelected));
    rowSelected2.AddHandler<PrimaryType>(pxRowSelected2);
    PXFilter<POVendorPriceUpdate> pxFilter = new PXFilter<POVendorPriceUpdate>(graph);
    graph.Views.Add("VendorInventory$UpdatePrice", ((PXSelectBase) pxFilter).View);
  }

  public POVendorInventorySelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    graph.Views.Caches.Add(typeof (INItemXRef));
    PXGraph.RowSelectedEvents rowSelected1 = graph.RowSelected;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected1 = new PXRowSelected((object) vendorInventorySelect1, __vmethodptr(vendorInventorySelect1, OnRowSelected));
    rowSelected1.AddHandler<Table>(pxRowSelected1);
    PXGraph.RowInsertedEvents rowInserted = graph.RowInserted;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect2 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) vendorInventorySelect2, __vmethodptr(vendorInventorySelect2, OnRowInserted));
    rowInserted.AddHandler<Table>(pxRowInserted);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect3 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) vendorInventorySelect3, __vmethodptr(vendorInventorySelect3, OnRowUpdated));
    rowUpdated.AddHandler<Table>(pxRowUpdated);
    PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect4 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) vendorInventorySelect4, __vmethodptr(vendorInventorySelect4, OnRowDeleted));
    rowDeleted.AddHandler<Table>(pxRowDeleted);
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect5 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) vendorInventorySelect5, __vmethodptr(vendorInventorySelect5, OnRowPersisting));
    rowPersisting.AddHandler<Table>(pxRowPersisting);
    PXGraph.RowSelectedEvents rowSelected2 = graph.RowSelected;
    POVendorInventorySelect<Table, Join, Where, PrimaryType> vendorInventorySelect6 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected2 = new PXRowSelected((object) vendorInventorySelect6, __vmethodptr(vendorInventorySelect6, OnParentRowSelected));
    rowSelected2.AddHandler<PrimaryType>(pxRowSelected2);
    PXFilter<POVendorPriceUpdate> pxFilter = new PXFilter<POVendorPriceUpdate>(graph);
    graph.Views.Add("VendorInventory$UpdatePrice", ((PXSelectBase) pxFilter).View);
  }

  private void AddAction(PXGraph graph, string name, string displayName, PXButtonDelegate handler)
  {
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = PXMessages.LocalizeNoPrefix(displayName)
    };
    graph.Actions[name] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (PrimaryType)), (object) graph, (object) name, (object) handler, (object) pxuiFieldAttribute);
  }

  protected virtual PX.Objects.IN.InventoryItem ReadInventory(object current)
  {
    return PX.Objects.IN.InventoryItem.PK.FindDirty(((PXSelectBase) this)._Graph, ((Table) current).InventoryID);
  }

  protected virtual void OnRowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is Table row))
      return;
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      INSetup current = (INSetup) cache.Graph.Caches[typeof (INSetup)].Current;
      if (current != null && current.UseInventorySubItem.GetValueOrDefault())
      {
        PX.Objects.IN.InventoryItem inventoryItem = this.ReadInventory((object) row);
        if (inventoryItem != null && !inventoryItem.DefaultSubItemID.HasValue && inventoryItem.StkItem.GetValueOrDefault())
          row.OverrideSettings = new bool?(true);
      }
    }
    if (cache.Graph.IsCopyPasteContext)
      return;
    this.UpdateXRef(row);
  }

  protected virtual void OnParentRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    INSetup current = (INSetup) sender.Graph.Caches[typeof (INSetup)].Current;
    PXUIFieldAttribute.SetVisible<POVendorInventory.overrideSettings>(sender.Graph.Caches[typeof (POVendorInventory)], (object) null, current.UseInventorySubItem.GetValueOrDefault());
  }

  protected virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Table row = (Table) e.Row;
    PXUIFieldAttribute.SetVisible<POVendorInventory.curyID>(sender, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>() || (object) row == null)
      return;
    INSetup current = (INSetup) sender.Graph.Caches[typeof (INSetup)].Current;
    PX.Objects.IN.InventoryItem inventoryItem = this.ReadInventory((object) row);
    bool? nullable;
    int num1;
    if (!row.OverrideSettings.GetValueOrDefault() && inventoryItem != null)
    {
      nullable = current.UseInventorySubItem;
      if (nullable.GetValueOrDefault())
      {
        int? defaultSubItemId = inventoryItem.DefaultSubItemID;
        int? subItemId = row.SubItemID;
        num1 = defaultSubItemId.GetValueOrDefault() == subItemId.GetValueOrDefault() & defaultSubItemId.HasValue == subItemId.HasValue ? 1 : 0;
        goto label_6;
      }
    }
    num1 = 1;
label_6:
    bool flag = num1 != 0;
    PXCache pxCache1 = sender;
    // ISSUE: variable of a boxed type
    __Boxed<Table> local1 = (object) row;
    nullable = current.UseInventorySubItem;
    int num2;
    if (nullable.GetValueOrDefault() && inventoryItem != null)
    {
      nullable = inventoryItem.StkItem;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<POVendorInventory.overrideSettings>(pxCache1, (object) local1, num2 != 0);
    PXUIFieldAttribute.SetEnabled<POVendorInventory.addLeadTimeDays>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<POVendorInventory.eRQ>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<POVendorInventory.lotSize>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<POVendorInventory.maxOrdQty>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<POVendorInventory.minOrdFreq>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<POVendorInventory.minOrdQty>(sender, (object) row, flag);
    PXCache pxCache2 = sender;
    // ISSUE: variable of a boxed type
    __Boxed<Table> local2 = (object) row;
    int num3;
    if (inventoryItem != null)
    {
      nullable = inventoryItem.StkItem;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<POVendorInventory.subItemID>(pxCache2, (object) local2, num3 != 0);
  }

  protected virtual void OnRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Table row = e.Row as Table;
    Table oldRow = e.OldRow as Table;
    if ((object) row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = this.ReadInventory((object) row);
    if (inventoryItem != null && inventoryItem.DefaultSubItemID.HasValue)
    {
      int? nullable1 = inventoryItem.DefaultSubItemID;
      int? nullable2 = row.SubItemID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        foreach (PXResult<POVendorInventory> pxResult in PXSelectBase<POVendorInventory, PXSelect<POVendorInventory, PX.Data.Where<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And2<PX.Data.Where<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>, Or<PX.Data.Where<Required<POVendorInventory.vendorLocationID>, IsNull, And<POVendorInventory.vendorLocationID, IsNull>>>>, And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.subItemID, NotEqual<Required<POVendorInventory.subItemID>>, And<POVendorInventory.overrideSettings, Equal<boolFalse>>>>>>>.Config>.Select(sender.Graph, new object[5]
        {
          (object) row.VendorID,
          (object) row.VendorLocationID,
          (object) row.VendorLocationID,
          (object) row.InventoryID,
          (object) row.SubItemID
        }))
        {
          POVendorInventory poVendorInventory = PXResult<POVendorInventory>.op_Implicit(pxResult);
          nullable2 = poVendorInventory.RecordID;
          nullable1 = row.RecordID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          {
            POVendorInventory copy = PXCache<POVendorInventory>.CreateCopy(poVendorInventory);
            copy.AddLeadTimeDays = row.AddLeadTimeDays;
            copy.ERQ = row.ERQ;
            copy.VLeadTime = row.VLeadTime;
            copy.LotSize = row.LotSize;
            copy.MaxOrdQty = row.MaxOrdQty;
            copy.MinOrdFreq = row.MinOrdFreq;
            copy.MinOrdQty = row.MinOrdQty;
            sender.Update((object) copy);
          }
        }
      }
    }
    if (sender.Graph.IsCopyPasteContext || POVendorInventorySelect<Table, Join, Where, PrimaryType>.IsEqualByItemXRef(row, oldRow))
      return;
    if (!this.ExistRelatedPOVendorInventory(oldRow))
      this.DeleteXRef(oldRow);
    this.UpdateXRef(row);
  }

  protected virtual void OnRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    Table row = (Table) e.Row;
    if (this.ExistRelatedPOVendorInventory(row))
      return;
    this.DeleteXRef(row);
  }

  protected virtual void OnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2 && e.Operation != 1)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = this.ReadInventory(e.Row);
    PXDefaultAttribute.SetPersistingCheck<POVendorInventory.subItemID>(sender, e.Row, inventoryItem == null || inventoryItem.StkItem.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  private static bool IsEqualByItemXRef(Table op1, Table op2)
  {
    int? vendorId1 = op1.VendorID;
    int? vendorId2 = op2.VendorID;
    if (vendorId1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & vendorId1.HasValue == vendorId2.HasValue)
    {
      int? inventoryId1 = op1.InventoryID;
      int? inventoryId2 = op2.InventoryID;
      if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
      {
        int? subItemId1 = op1.SubItemID;
        int? subItemId2 = op2.SubItemID;
        if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
          return op1.VendorInventoryID == op2.VendorInventoryID;
      }
    }
    return false;
  }

  private void DeleteXRef(Table doc)
  {
    int? subItemID;
    if (!this.CanProcessXRef(doc, out subItemID))
      return;
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (INItemXRef)];
    foreach (PXResult<INItemXRef> pxResult in PXSelectBase<INItemXRef, PXSelect<INItemXRef, PX.Data.Where<INItemXRef.alternateID, Equal<Required<POVendorInventory.vendorInventoryID>>, And<INItemXRef.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<POVendorInventory.subItemID>>, And<INItemXRef.bAccountID, Equal<Required<POVendorInventory.vendorID>>, And<INItemXRef.alternateType, Equal<INAlternateType.vPN>>>>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[4]
    {
      (object) doc.VendorInventoryID,
      (object) doc.InventoryID,
      (object) subItemID,
      (object) doc.VendorID
    }))
    {
      INItemXRef inItemXref = PXResult<INItemXRef>.op_Implicit(pxResult);
      cach.Delete((object) inItemXref);
    }
  }

  private bool ExistRelatedPOVendorInventory(Table doc)
  {
    if (!doc.InventoryID.HasValue || !doc.VendorID.HasValue || string.IsNullOrEmpty(doc.VendorInventoryID))
      return false;
    PXSelect<POVendorInventory, PX.Data.Where<POVendorInventory.vendorInventoryID, Equal<Required<POVendorInventory.vendorInventoryID>>, And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And<POVendorInventory.recordID, NotEqual<Required<POVendorInventory.recordID>>>>>>> pxSelect = new PXSelect<POVendorInventory, PX.Data.Where<POVendorInventory.vendorInventoryID, Equal<Required<POVendorInventory.vendorInventoryID>>, And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And<POVendorInventory.recordID, NotEqual<Required<POVendorInventory.recordID>>>>>>>(((PXSelectBase) this)._Graph);
    if (doc.SubItemID.HasValue)
    {
      ((PXSelectBase<POVendorInventory>) pxSelect).WhereAnd<PX.Data.Where<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>>>();
      return ((IQueryable<PXResult<POVendorInventory>>) ((PXSelectBase<POVendorInventory>) pxSelect).SelectWindowed(0, 1, new object[5]
      {
        (object) doc.VendorInventoryID,
        (object) doc.InventoryID,
        (object) doc.VendorID,
        (object) doc.RecordID,
        (object) doc.SubItemID
      })).Any<PXResult<POVendorInventory>>();
    }
    PX.Objects.IN.InventoryItem inventoryItem = this.ReadInventory((object) doc);
    if (inventoryItem != null)
    {
      bool? stkItem = inventoryItem.StkItem;
      bool flag = false;
      if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue)
        return ((IQueryable<PXResult<POVendorInventory>>) ((PXSelectBase<POVendorInventory>) pxSelect).SelectWindowed(0, 1, new object[4]
        {
          (object) doc.VendorInventoryID,
          (object) doc.InventoryID,
          (object) doc.VendorID,
          (object) doc.RecordID
        })).Any<PXResult<POVendorInventory>>();
    }
    return false;
  }

  private void UpdateXRef(Table doc)
  {
    int? subItemID;
    if (!this.CanProcessXRef(doc, out subItemID))
      return;
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (INItemXRef)];
    INItemXRef inItemXref1 = (INItemXRef) null;
    INItemXRef inItemXref2 = (INItemXRef) null;
    foreach (PXResult<INItemXRef> pxResult in PXSelectBase<INItemXRef, PXSelect<INItemXRef, PX.Data.Where<INItemXRef.alternateID, Equal<Required<POVendorInventory.vendorInventoryID>>, And<INItemXRef.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<POVendorInventory.subItemID>>, And<Where2<PX.Data.Where<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>>>, Or<PX.Data.Where<INItemXRef.alternateType, Equal<INAlternateType.vPN>, And<INItemXRef.bAccountID, Equal<Required<POVendorInventory.vendorID>>>>>>>>>>, OrderBy<Asc<INItemXRef.alternateType>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[4]
    {
      (object) doc.VendorInventoryID,
      (object) doc.InventoryID,
      (object) subItemID,
      (object) doc.VendorID
    }))
    {
      INItemXRef inItemXref3 = PXResult<INItemXRef>.op_Implicit(pxResult);
      if (inItemXref3.AlternateType == "0VPN")
        inItemXref1 = inItemXref3;
      else if (inItemXref2 == null)
        inItemXref2 = inItemXref3;
    }
    INItemXRef inItemXref4;
    if (inItemXref1 == null)
    {
      if (inItemXref2 != null)
        return;
      INItemXRef dest = new INItemXRef();
      POVendorInventorySelect<Table, Join, Where, PrimaryType>.Copy(dest, doc, subItemID);
      inItemXref4 = (INItemXRef) cach.Insert((object) dest);
    }
    else
    {
      INItemXRef copy = (INItemXRef) cach.CreateCopy((object) inItemXref1);
      POVendorInventorySelect<Table, Join, Where, PrimaryType>.Copy(copy, doc, subItemID);
      inItemXref4 = (INItemXRef) cach.Update((object) copy);
    }
  }

  private bool CanProcessXRef(Table doc, out int? subItemID)
  {
    subItemID = doc.SubItemID;
    if (doc.InventoryID.HasValue)
    {
      int? nullable = doc.VendorID;
      if (nullable.HasValue && !string.IsNullOrEmpty(doc.VendorInventoryID))
      {
        nullable = doc.SubItemID;
        if (!nullable.HasValue)
        {
          PX.Objects.IN.InventoryItem inventoryItem = this.ReadInventory((object) doc);
          if (inventoryItem != null)
          {
            bool? stkItem = inventoryItem.StkItem;
            bool flag = false;
            if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue)
            {
              object obj;
              ((PXSelectBase) this)._Graph.Caches[typeof (INItemXRef)].RaiseFieldDefaulting<INItemXRef.subItemID>((object) null, ref obj);
              subItemID = (int?) obj;
              goto label_7;
            }
          }
          return false;
        }
label_7:
        return true;
      }
    }
    return false;
  }

  private static void Copy(INItemXRef dest, Table src, int? subItemID)
  {
    dest.InventoryID = src.InventoryID;
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      dest.SubItemID = subItemID;
    dest.BAccountID = src.VendorID;
    dest.AlternateType = "0VPN";
    dest.AlternateID = src.VendorInventoryID;
    dest.UOM = src.PurchaseUnit;
  }
}
