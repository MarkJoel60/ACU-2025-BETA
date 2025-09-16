// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitSelect`7
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

[Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
public class INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> : 
  PXSelect<INUnit, Where<INUnit.inventoryID, Equal<Current<inventoryID>>, And<INUnit.toUnit, Equal<Optional<baseUnit>>, And<INUnit.fromUnit, NotEqual<Optional<baseUnit>>>>>>
  where Table : INUnit
  where inventoryID : IBqlField
  where itemClassID : IBqlField
  where salesUnit : IBqlField
  where purchaseUnit : IBqlField
  where baseUnit : IBqlField
  where lotSerClass : IBqlField
{
  protected PXCache TopCache;
  private Dictionary<int?, int?> _persisted = new Dictionary<int?, int?>();

  public INUnitSelect(PXGraph graph)
    : base(graph)
  {
    this.TopCache = ((PXSelectBase) this).Cache.Graph.Caches[BqlCommand.GetItemType(typeof (inventoryID))];
    PXGraph.FieldVerifyingEvents fieldVerifying1 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) inUnitSelect1, __vmethodptr(inUnitSelect1, SalesUnit_FieldVerifying));
    fieldVerifying1.AddHandler<salesUnit>(pxFieldVerifying1);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) inUnitSelect2, __vmethodptr(inUnitSelect2, PurchaseUnit_FieldVerifying));
    fieldVerifying2.AddHandler<purchaseUnit>(pxFieldVerifying2);
    PXGraph.FieldVerifyingEvents fieldVerifying3 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect3 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying3 = new PXFieldVerifying((object) inUnitSelect3, __vmethodptr(inUnitSelect3, BaseUnit_FieldVerifying));
    fieldVerifying3.AddHandler<baseUnit>(pxFieldVerifying3);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = graph.FieldUpdated;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) inUnitSelect4, __vmethodptr(inUnitSelect4, SalesUnit_FieldUpdated));
    fieldUpdated1.AddHandler<salesUnit>(pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = graph.FieldUpdated;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) inUnitSelect5, __vmethodptr(inUnitSelect5, PurchaseUnit_FieldUpdated));
    fieldUpdated2.AddHandler<purchaseUnit>(pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = graph.FieldUpdated;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect6 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) inUnitSelect6, __vmethodptr(inUnitSelect6, BaseUnit_FieldUpdated));
    fieldUpdated3.AddHandler<baseUnit>(pxFieldUpdated3);
    PXGraph.FieldVerifyingEvents fieldVerifying4 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect7 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying4 = new PXFieldVerifying((object) inUnitSelect7, __vmethodptr(inUnitSelect7, LotSerClass_FieldVerifying));
    fieldVerifying4.AddHandler<lotSerClass>(pxFieldVerifying4);
    PXGraph.RowInsertedEvents rowInserted1 = graph.RowInserted;
    Type itemType1 = this.TopCache.GetItemType();
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect8 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted1 = new PXRowInserted((object) inUnitSelect8, __vmethodptr(inUnitSelect8, Top_RowInserted));
    rowInserted1.AddHandler(itemType1, pxRowInserted1);
    PXGraph.RowPersistingEvents rowPersisting1 = graph.RowPersisting;
    Type itemType2 = this.TopCache.GetItemType();
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect9 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting1 = new PXRowPersisting((object) inUnitSelect9, __vmethodptr(inUnitSelect9, Top_RowPersisting));
    rowPersisting1.AddHandler(itemType2, pxRowPersisting1);
    PXGraph.FieldDefaultingEvents fieldDefaulting1 = graph.FieldDefaulting;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect10 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting1 = new PXFieldDefaulting((object) inUnitSelect10, __vmethodptr(inUnitSelect10, INUnit_InventoryID_FieldDefaulting));
    fieldDefaulting1.AddHandler<INUnit.inventoryID>(pxFieldDefaulting1);
    PXGraph.FieldVerifyingEvents fieldVerifying5 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect11 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying5 = new PXFieldVerifying((object) inUnitSelect11, __vmethodptr(inUnitSelect11, INUnit_InventoryID_FieldVerifying));
    fieldVerifying5.AddHandler<INUnit.inventoryID>(pxFieldVerifying5);
    PXGraph.RowPersistingEvents rowPersisting2 = graph.RowPersisting;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect12 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting2 = new PXRowPersisting((object) inUnitSelect12, __vmethodptr(inUnitSelect12, INUnit_RowPersisting));
    rowPersisting2.AddHandler<INUnit>(pxRowPersisting2);
    PXGraph.RowPersistedEvents rowPersisted = graph.RowPersisted;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect13 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) inUnitSelect13, __vmethodptr(inUnitSelect13, INUnit_RowPersisted));
    rowPersisted.AddHandler<INUnit>(pxRowPersisted);
    PXGraph.FieldDefaultingEvents fieldDefaulting2 = graph.FieldDefaulting;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect14 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting2 = new PXFieldDefaulting((object) inUnitSelect14, __vmethodptr(inUnitSelect14, INUnit_ToUnit_FieldDefaulting));
    fieldDefaulting2.AddHandler<INUnit.toUnit>(pxFieldDefaulting2);
    PXGraph.FieldDefaultingEvents fieldDefaulting3 = graph.FieldDefaulting;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect15 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting3 = new PXFieldDefaulting((object) inUnitSelect15, __vmethodptr(inUnitSelect15, INUnit_UnitType_FieldDefaulting));
    fieldDefaulting3.AddHandler<INUnit.unitType>(pxFieldDefaulting3);
    PXGraph.FieldVerifyingEvents fieldVerifying6 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect16 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying6 = new PXFieldVerifying((object) inUnitSelect16, __vmethodptr(inUnitSelect16, INUnit_UnitType_FieldVerifying));
    fieldVerifying6.AddHandler<INUnit.unitType>(pxFieldVerifying6);
    PXGraph.FieldVerifyingEvents fieldVerifying7 = graph.FieldVerifying;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect17 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying7 = new PXFieldVerifying((object) inUnitSelect17, __vmethodptr(inUnitSelect17, INUnit_UnitRate_FieldVerifying));
    fieldVerifying7.AddHandler<INUnit.unitRate>(pxFieldVerifying7);
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect18 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) inUnitSelect18, __vmethodptr(inUnitSelect18, INUnit_RowSelected));
    rowSelected.AddHandler<INUnit>(pxRowSelected);
    PXGraph.RowInsertingEvents rowInserting = graph.RowInserting;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect19 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) inUnitSelect19, __vmethodptr(inUnitSelect19, INUnit_RowInserting));
    rowInserting.AddHandler<INUnit>(pxRowInserting);
    PXGraph.RowInsertedEvents rowInserted2 = graph.RowInserted;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect20 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted2 = new PXRowInserted((object) inUnitSelect20, __vmethodptr(inUnitSelect20, INUnit_RowInserted));
    rowInserted2.AddHandler<INUnit>(pxRowInserted2);
    PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
    INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect21 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) inUnitSelect21, __vmethodptr(inUnitSelect21, INUnit_RowDeleted));
    rowDeleted.AddHandler<INUnit>(pxRowDeleted);
    if (((PXSelectBase) this).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
    {
      PXGraph.FieldVerifyingEvents fieldVerifying8 = graph.FieldVerifying;
      INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect22 = this;
      // ISSUE: virtual method pointer
      PXFieldVerifying pxFieldVerifying8 = new PXFieldVerifying((object) inUnitSelect22, __vmethodptr(inUnitSelect22, INUnit_FromUnit_FieldVerifying));
      fieldVerifying8.AddHandler<INUnit.fromUnit>(pxFieldVerifying8);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      graph.ExceptionHandling.AddHandler<salesUnit>(INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXExceptionHandling((object) INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      graph.ExceptionHandling.AddHandler<purchaseUnit>(INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_1 ?? (INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_1 = new PXExceptionHandling((object) INUnitSelect<Table, inventoryID, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_1))));
    }
  }

  protected object TopGetValue<Field>(object data) where Field : IBqlField
  {
    if (BqlCommand.GetItemType(typeof (Field)) == this.TopCache.GetItemType() || this.TopCache.GetItemType().IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
      return this.TopCache.GetValue<Field>(data);
    PXCache cach = ((PXSelectBase) this).Cache.Graph.Caches[BqlCommand.GetItemType(typeof (Field))];
    return cach.GetValue<Field>(cach.Current);
  }

  protected DataType TopGetValue<Field, DataType>(object data) where Field : IBqlField
  {
    return (DataType) this.TopGetValue<Field>(data);
  }

  protected object TopGetValue<Field>() where Field : IBqlField
  {
    return this.TopGetValue<Field>(this.TopCache.Current);
  }

  protected DataType TopGetValue<Field, DataType>() where Field : IBqlField
  {
    return (DataType) this.TopGetValue<Field>();
  }

  protected virtual void SalesUnit_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SalesUnit_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.InsertConversion<salesUnit>(sender, e.Row, (string) e.OldValue);
  }

  protected virtual void PurchaseUnit_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PurchaseUnit_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.InsertConversion<purchaseUnit>(sender, e.Row, (string) e.OldValue);
  }

  protected virtual void BaseUnit_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  private void InsertConversion<TFromUnit>(PXCache cache, object row, string oldFromValue) where TFromUnit : IBqlField
  {
    string fromUnit = this.TopGetValue<TFromUnit, string>(row);
    if (!string.IsNullOrEmpty(fromUnit))
    {
      int? inventoryID = this.TopGetValue<inventoryID, int?>(row);
      if (INUnit.UK.ByInventory.FindDirty(cache.Graph, inventoryID, fromUnit) == null)
      {
        string str = this.TopGetValue<baseUnit, string>(row);
        INUnit dirty;
        INUnit inUnit;
        if ((dirty = INUnit.UK.ByGlobal.FindDirty(cache.Graph, fromUnit, str)) != null)
        {
          inUnit = PXCache<INUnit>.CreateCopy(dirty);
          inUnit.UnitType = new short?((short) 1);
          inUnit.ItemClassID = new int?(0);
          inUnit.InventoryID = inventoryID;
          inUnit.RecordID = new long?();
        }
        else
          inUnit = this.ResolveInventoryConversion(inventoryID, fromUnit, str);
        ((PXSelectBase) this).Cache.Insert((object) inUnit);
      }
    }
    if (string.IsNullOrEmpty(oldFromValue) || string.Equals(oldFromValue, this.TopGetValue<purchaseUnit, string>(row)) || string.Equals(oldFromValue, this.TopGetValue<salesUnit, string>(row)) || string.Equals(oldFromValue, this.TopGetValue<baseUnit, string>(row)))
      return;
    INUnit inUnit1 = this.ResolveInventoryConversion(this.TopGetValue<inventoryID, int?>(row), oldFromValue, this.TopGetValue<baseUnit, string>(row));
    if (((PXSelectBase) this).Cache.GetStatus((object) inUnit1) != 2)
      return;
    ((PXSelectBase) this).Cache.Delete((object) inUnit1);
  }

  protected virtual void LotSerClass_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    INLotSerClass dirty = INLotSerClass.PK.FindDirty(sender.Graph, (string) e.NewValue);
    if (dirty == null || !(dirty.LotSerTrack == "S"))
      return;
    foreach (PXResult<INUnit> pxResult in ((PXSelectBase<INUnit>) this).Select(Array.Empty<object>()))
    {
      INUnit conv = PXResult<INUnit>.op_Implicit(pxResult);
      if (INUnitAttribute.IsFractional(conv))
      {
        GraphHelper.MarkUpdated(((PXSelectBase) this).Cache, (object) conv, true);
        ((PXSelectBase) this).Cache.RaiseExceptionHandling<INUnit.unitMultDiv>((object) conv, (object) conv.UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
      }
    }
  }

  protected virtual void BaseUnit_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    string str = this.TopGetValue<baseUnit, string>(e.Row);
    if (!string.Equals((string) e.OldValue, str))
    {
      if (!string.IsNullOrEmpty((string) e.OldValue))
      {
        ((PXSelectBase) this).Cache.Delete((object) this.ResolveInventoryConversion(this.TopGetValue<inventoryID, int?>(e.Row), (string) e.OldValue, (string) e.OldValue));
        foreach (PXResult<INUnit> pxResult in ((PXSelectBase<INUnit>) this).Select(new object[2]
        {
          (object) (string) e.OldValue,
          (object) (string) e.OldValue
        }))
          ((PXSelectBase) this).Cache.Delete((object) PXResult<INUnit>.op_Implicit(pxResult));
      }
      if (!string.IsNullOrEmpty(str))
      {
        foreach (PXResult<INUnit> pxResult in PXSelectBase<INUnit, PXSelect<INUnit, Where<INUnit.unitType, Equal<INUnitType.itemClass>, And<INUnit.itemClassID, Equal<Current<itemClassID>>, And<INUnit.toUnit, Equal<Required<baseUnit>>, And<INUnit.fromUnit, NotEqual<Required<baseUnit>>>>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) str,
          (object) str
        }))
        {
          INUnit copy = PXCache<INUnit>.CreateCopy(PXResult<INUnit>.op_Implicit(pxResult));
          copy.InventoryID = this.TopGetValue<inventoryID, int?>(e.Row);
          copy.ItemClassID = new int?(0);
          copy.UnitType = new short?((short) 1);
          copy.RecordID = new long?();
          ((PXSelectBase) this).Cache.Insert((object) copy);
        }
      }
    }
    if (string.IsNullOrEmpty(str))
      return;
    if (INUnit.UK.ByInventory.FindDirty(sender.Graph, this.TopGetValue<inventoryID, int?>(e.Row), str) == null)
      ((PXSelectBase) this).Cache.Insert((object) this.ResolveInventoryConversion(this.TopGetValue<inventoryID, int?>(e.Row), str, str));
    sender.RaiseFieldUpdated<salesUnit>(e.Row, (object) this.TopGetValue<salesUnit, string>(e.Row));
    sender.RaiseFieldUpdated<purchaseUnit>(e.Row, (object) this.TopGetValue<purchaseUnit, string>(e.Row));
  }

  protected virtual void Top_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (string.IsNullOrEmpty(this.TopGetValue<baseUnit, string>(e.Row)))
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this).Cache
    }))
      sender.RaiseFieldUpdated<baseUnit>(e.Row, (object) null);
  }

  protected virtual void Top_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    sender.RaiseFieldUpdated<baseUnit>(e.Row, (object) this.TopGetValue<baseUnit, string>(e.Row));
  }

  protected virtual void INUnit_ToUnit_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    ((INUnit) e.Row).SampleToUnit = this.TopGetValue<baseUnit, string>();
    e.NewValue = (object) this.TopGetValue<baseUnit, string>();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_InventoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = this.TopGetValue<inventoryID>();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = this.TopGetValue<inventoryID>();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_UnitType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = (object) (short) 1;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_UnitType_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = (object) (short) 1;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_UnitRate_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() <= num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) "0"
      });
  }

  protected virtual void INUnit_FromUnit_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if ((INUnit) e.Row != null && e.ExternalCall && !string.IsNullOrEmpty((string) e.NewValue) && (string) e.NewValue == this.TopGetValue<baseUnit, string>())
      throw new PXSetPropertyException("The entered unit is the base unit and cannot be used to convert from. Enter a different unit.");
  }

  protected virtual void INUnit_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    INUnit row = (INUnit) e.Row;
    if ((e.Operation & 3) == 2 || (e.Operation & 3) == 1)
    {
      PXCache cach = sender.Graph.Caches[typeof (INLotSerClass)];
      if (cach.Current != null && ((INLotSerClass) cach.Current).LotSerTrack == "S" && INUnitAttribute.IsFractional((INUnit) e.Row))
        sender.RaiseExceptionHandling<INUnit.unitMultDiv>(e.Row, (object) row.UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
    }
    int? nullable;
    if ((e.Operation & 3) == 2)
    {
      nullable = row.InventoryID;
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue && this.TopCache.Current != null)
      {
        int? key = this.TopGetValue<inventoryID, int?>();
        if (!this._persisted.ContainsKey(key))
          this._persisted.Add(key, row.InventoryID);
        row.InventoryID = key;
        sender.Normalize();
      }
    }
    if (!EnumerableExtensions.IsIn<PXDBOperation>((PXDBOperation) (e.Operation & 3), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    short? unitType = row.UnitType;
    nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num1 = 1;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
    {
      nullable = row.InventoryID;
      int num2 = 0;
      if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        throw new PXInvalidOperationException("'{0}' should not be negative.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<INUnit.inventoryID>(sender)
        });
    }
    unitType = row.UnitType;
    nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num3 = 2;
    if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
      return;
    nullable = row.ItemClassID;
    int num4 = 0;
    if (nullable.GetValueOrDefault() < num4 & nullable.HasValue)
      throw new PXInvalidOperationException("'{0}' should not be negative.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<INUnit.itemClassID>(sender)
      });
  }

  protected virtual void INUnit_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    int? nullable;
    if (e.TranStatus != 2 || (e.Operation & 3) != 2 || !this._persisted.TryGetValue(((INUnit) e.Row).InventoryID, out nullable))
      return;
    ((INUnit) e.Row).InventoryID = nullable;
  }

  protected virtual void INUnit_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXFieldState stateExt = (PXFieldState) sender.GetStateExt<INUnit.unitMultDiv>(e.Row);
    string str;
    if (stateExt.Error != null && !(stateExt.Error == PXMessages.Localize("Fractional unit conversions not supported for serial numbered items", ref str)))
      return;
    INLotSerClass inLotSerClass = this.ReadLotSerClass();
    if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && INUnitAttribute.IsFractional((INUnit) e.Row))
      sender.RaiseExceptionHandling<INUnit.unitMultDiv>(e.Row, (object) ((INUnit) e.Row).UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
    else
      sender.RaiseExceptionHandling<INUnit.unitMultDiv>(e.Row, (object) null, (Exception) null);
  }

  protected virtual void INUnit_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    INUnit row = (INUnit) e.Row;
    if (row != null && row.ToUnit == null)
      ((CancelEventArgs) e).Cancel = true;
    if (row == null)
      return;
    foreach (INUnit inUnit in sender.Deleted)
    {
      if (sender.ObjectsEqual((object) inUnit, (object) row))
      {
        row.RecordID = inUnit.RecordID;
        row.tstamp = inUnit.tstamp;
        break;
      }
    }
  }

  protected virtual void INUnit_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    INUnit row = (INUnit) e.Row;
    if (row.FromUnit == null)
      return;
    short? unitType = row.UnitType;
    int? nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num = 1;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) || INUnit.UK.ByGlobal.FindDirty(sender.Graph, row.FromUnit, row.FromUnit) != null)
      return;
    INUnit inUnit = this.ResolveGlobalConversion(row.FromUnit);
    sender.RaiseRowInserting((object) inUnit);
    sender.SetStatus((object) inUnit, (PXEntryStatus) 2);
    sender.ClearQueryCacheObsolete();
  }

  protected virtual void INUnit_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    INUnit row = (INUnit) e.Row;
    short? unitType = row.UnitType;
    int? nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num = 1;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    INUnit inUnit = this.ResolveGlobalConversion(row.FromUnit);
    if (sender.GetStatus((object) inUnit) != 2)
      return;
    sender.SetStatus((object) inUnit, (PXEntryStatus) 4);
    sender.ClearQueryCacheObsolete();
  }

  private INLotSerClass ReadLotSerClass()
  {
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[BqlCommand.GetItemType(typeof (lotSerClass))];
    return INLotSerClass.PK.FindDirty(((PXSelectBase) this)._Graph, (string) cach.GetValue(cach.Current, typeof (lotSerClass).Name));
  }

  private INUnit ResolveInventoryConversion(int? inventoryID, string fromUnit, string baseUnit)
  {
    return new INUnit()
    {
      UnitType = new short?((short) 1),
      ItemClassID = new int?(0),
      InventoryID = inventoryID,
      FromUnit = fromUnit,
      ToUnit = baseUnit,
      UnitRate = new Decimal?(1M),
      PriceAdjustmentMultiplier = new Decimal?(1M),
      UnitMultDiv = "M"
    };
  }

  private INUnit ResolveGlobalConversion(string fromUnit)
  {
    return new INUnit()
    {
      UnitType = new short?((short) 3),
      ItemClassID = new int?(0),
      InventoryID = new int?(0),
      FromUnit = fromUnit,
      ToUnit = fromUnit,
      UnitRate = new Decimal?(1M),
      PriceAdjustmentMultiplier = new Decimal?(1M),
      UnitMultDiv = "M"
    };
  }
}
