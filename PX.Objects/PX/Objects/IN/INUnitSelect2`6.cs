// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitSelect2`6
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
public class INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> : 
  PXSelect<INUnit, Where<INUnit.itemClassID, Equal<Optional<itemClassID>>, And<INUnit.toUnit, Equal<Optional<baseUnit>>, And<INUnit.fromUnit, NotEqual<Optional<baseUnit>>>>>>
  where Table : INUnit
  where itemClassID : IBqlField
  where salesUnit : IBqlField
  where purchaseUnit : IBqlField
  where baseUnit : IBqlField
  where lotSerClass : IBqlField
{
  protected PXCache TopCache;
  private Dictionary<int?, int?> _persisted = new Dictionary<int?, int?>();

  public INUnitSelect2(PXGraph graph)
    : base(graph)
  {
    this.TopCache = ((PXSelectBase) this).Cache.Graph.Caches[BqlCommand.GetItemType(typeof (itemClassID))];
    PXGraph.FieldVerifyingEvents fieldVerifying1 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) inUnitSelect2_1, __vmethodptr(inUnitSelect2_1, SalesUnit_FieldVerifying));
    fieldVerifying1.AddHandler<salesUnit>(pxFieldVerifying1);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) inUnitSelect2_2, __vmethodptr(inUnitSelect2_2, PurchaseUnit_FieldVerifying));
    fieldVerifying2.AddHandler<purchaseUnit>(pxFieldVerifying2);
    PXGraph.FieldVerifyingEvents fieldVerifying3 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_3 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying3 = new PXFieldVerifying((object) inUnitSelect2_3, __vmethodptr(inUnitSelect2_3, BaseUnit_FieldVerifying));
    fieldVerifying3.AddHandler<baseUnit>(pxFieldVerifying3);
    PXGraph.FieldVerifyingEvents fieldVerifying4 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_4 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying4 = new PXFieldVerifying((object) inUnitSelect2_4, __vmethodptr(inUnitSelect2_4, LotSerClass_FieldVerifying));
    fieldVerifying4.AddHandler<lotSerClass>(pxFieldVerifying4);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = graph.FieldUpdated;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) inUnitSelect2_5, __vmethodptr(inUnitSelect2_5, SalesUnit_FieldUpdated));
    fieldUpdated1.AddHandler<salesUnit>(pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = graph.FieldUpdated;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_6 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) inUnitSelect2_6, __vmethodptr(inUnitSelect2_6, PurchaseUnit_FieldUpdated));
    fieldUpdated2.AddHandler<purchaseUnit>(pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = graph.FieldUpdated;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_7 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) inUnitSelect2_7, __vmethodptr(inUnitSelect2_7, BaseUnit_FieldUpdated));
    fieldUpdated3.AddHandler<baseUnit>(pxFieldUpdated3);
    PXGraph.RowInsertedEvents rowInserted = graph.RowInserted;
    Type itemType1 = this.TopCache.GetItemType();
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_8 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) inUnitSelect2_8, __vmethodptr(inUnitSelect2_8, Top_RowInserted));
    rowInserted.AddHandler(itemType1, pxRowInserted);
    PXGraph.RowPersistingEvents rowPersisting1 = graph.RowPersisting;
    Type itemType2 = this.TopCache.GetItemType();
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_9 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting1 = new PXRowPersisting((object) inUnitSelect2_9, __vmethodptr(inUnitSelect2_9, Top_RowPersisting));
    rowPersisting1.AddHandler(itemType2, pxRowPersisting1);
    PXGraph.FieldDefaultingEvents fieldDefaulting1 = graph.FieldDefaulting;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_10 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting1 = new PXFieldDefaulting((object) inUnitSelect2_10, __vmethodptr(inUnitSelect2_10, INUnit_ItemClassID_FieldDefaulting));
    fieldDefaulting1.AddHandler<INUnit.itemClassID>(pxFieldDefaulting1);
    PXGraph.FieldVerifyingEvents fieldVerifying5 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_11 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying5 = new PXFieldVerifying((object) inUnitSelect2_11, __vmethodptr(inUnitSelect2_11, INUnit_ItemClassID_FieldVerifying));
    fieldVerifying5.AddHandler<INUnit.itemClassID>(pxFieldVerifying5);
    PXGraph.FieldDefaultingEvents fieldDefaulting2 = graph.FieldDefaulting;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_12 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting2 = new PXFieldDefaulting((object) inUnitSelect2_12, __vmethodptr(inUnitSelect2_12, INUnit_ToUnit_FieldDefaulting));
    fieldDefaulting2.AddHandler<INUnit.toUnit>(pxFieldDefaulting2);
    PXGraph.FieldDefaultingEvents fieldDefaulting3 = graph.FieldDefaulting;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_13 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting3 = new PXFieldDefaulting((object) inUnitSelect2_13, __vmethodptr(inUnitSelect2_13, INUnit_UnitType_FieldDefaulting));
    fieldDefaulting3.AddHandler<INUnit.unitType>(pxFieldDefaulting3);
    PXGraph.FieldVerifyingEvents fieldVerifying6 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_14 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying6 = new PXFieldVerifying((object) inUnitSelect2_14, __vmethodptr(inUnitSelect2_14, INUnit_UnitType_FieldVerifying));
    fieldVerifying6.AddHandler<INUnit.unitType>(pxFieldVerifying6);
    PXGraph.FieldVerifyingEvents fieldVerifying7 = graph.FieldVerifying;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_15 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying7 = new PXFieldVerifying((object) inUnitSelect2_15, __vmethodptr(inUnitSelect2_15, INUnit_UnitRate_FieldVerifying));
    fieldVerifying7.AddHandler<INUnit.unitRate>(pxFieldVerifying7);
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_16 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) inUnitSelect2_16, __vmethodptr(inUnitSelect2_16, INUnit_RowSelected));
    rowSelected.AddHandler<INUnit>(pxRowSelected);
    PXGraph.RowPersistingEvents rowPersisting2 = graph.RowPersisting;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_17 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting2 = new PXRowPersisting((object) inUnitSelect2_17, __vmethodptr(inUnitSelect2_17, INUnit_RowPersisting));
    rowPersisting2.AddHandler<INUnit>(pxRowPersisting2);
    PXGraph.RowPersistedEvents rowPersisted = graph.RowPersisted;
    INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_18 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) inUnitSelect2_18, __vmethodptr(inUnitSelect2_18, INUnit_RowPersisted));
    rowPersisted.AddHandler<INUnit>(pxRowPersisted);
    if (((PXSelectBase) this).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
    {
      PXGraph.FieldVerifyingEvents fieldVerifying8 = graph.FieldVerifying;
      INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass> inUnitSelect2_19 = this;
      // ISSUE: virtual method pointer
      PXFieldVerifying pxFieldVerifying8 = new PXFieldVerifying((object) inUnitSelect2_19, __vmethodptr(inUnitSelect2_19, INUnit_FromUnit_FieldVerifying));
      fieldVerifying8.AddHandler<INUnit.fromUnit>(pxFieldVerifying8);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      graph.ExceptionHandling.AddHandler<salesUnit>(INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXExceptionHandling((object) INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      graph.ExceptionHandling.AddHandler<purchaseUnit>(INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_1 ?? (INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9__1_1 = new PXExceptionHandling((object) INUnitSelect2<Table, itemClassID, salesUnit, purchaseUnit, baseUnit, lotSerClass>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_1))));
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
    this.InsertConversionIfNotExists<salesUnit>(sender, e.Row);
  }

  protected virtual void PurchaseUnit_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PurchaseUnit_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.InsertConversionIfNotExists<purchaseUnit>(sender, e.Row);
  }

  protected virtual void BaseUnit_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  private void InsertConversionIfNotExists<TFromUnit>(PXCache sender, object row) where TFromUnit : IBqlField
  {
    string fromUnit = this.TopGetValue<TFromUnit, string>(row);
    if (string.IsNullOrEmpty(fromUnit))
      return;
    int? itemClassID = this.TopGetValue<itemClassID, int?>(row);
    if (INUnit.UK.ByItemClass.FindDirty(sender.Graph, itemClassID, fromUnit) != null)
      return;
    ((PXSelectBase) this).Cache.Insert((object) this.ResolveItemClassConversion(itemClassID, this.TopGetValue<baseUnit, string>(row), fromUnit));
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
    string b = this.TopGetValue<baseUnit, string>(e.Row);
    if (!string.Equals((string) e.OldValue, b))
    {
      if (!string.IsNullOrEmpty((string) e.OldValue))
      {
        int? itemClassID = this.TopGetValue<itemClassID, int?>(e.Row);
        ((PXSelectBase) this).Cache.Delete((object) this.ResolveItemClassConversion(itemClassID, (string) e.OldValue, (string) e.OldValue));
        foreach (PXResult<INUnit> pxResult in ((PXSelectBase<INUnit>) this).Select(new object[3]
        {
          (object) itemClassID,
          (object) (string) e.OldValue,
          (object) (string) e.OldValue
        }))
          ((PXSelectBase) this).Cache.Delete((object) PXResult<INUnit>.op_Implicit(pxResult));
      }
      if (!string.IsNullOrEmpty(b))
      {
        foreach (PXResult<INUnit> pxResult in PXSelectBase<INUnit, PXSelect<INUnit, Where<INUnit.unitType, Equal<INUnitType.global>, And<INUnit.toUnit, Equal<Required<baseUnit>>, And<INUnit.fromUnit, NotEqual<Required<baseUnit>>>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) b,
          (object) b
        }))
        {
          INUnit copy = PXCache<INUnit>.CreateCopy(PXResult<INUnit>.op_Implicit(pxResult));
          copy.ItemClassID = new int?();
          copy.UnitType = new short?();
          copy.RecordID = new long?();
          ((PXSelectBase) this).Cache.Insert((object) copy);
        }
      }
    }
    if (string.IsNullOrEmpty(b))
      return;
    this.InsertConversionIfNotExists<baseUnit>(sender, e.Row);
    sender.RaiseFieldUpdated<salesUnit>(e.Row, this.TopGetValue<salesUnit>(e.Row));
    sender.RaiseFieldUpdated<purchaseUnit>(e.Row, this.TopGetValue<purchaseUnit>(e.Row));
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
    sender.RaiseFieldUpdated<baseUnit>(e.Row, this.TopGetValue<baseUnit>(e.Row));
  }

  protected virtual void INUnit_ToUnit_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    ((INUnit) e.Row).SampleToUnit = this.TopGetValue<baseUnit, string>();
    e.NewValue = (object) this.TopGetValue<baseUnit, string>();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_ItemClassID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = this.TopGetValue<itemClassID>();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_ItemClassID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = this.TopGetValue<itemClassID>();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_UnitType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = (object) (short) 2;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_UnitType_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.TopCache.Current == null)
      return;
    e.NewValue = (object) (short) 2;
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

  protected virtual void INUnit_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INLotSerClass inLotSerClass = this.ReadLotSerClass();
    if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && INUnitAttribute.IsFractional((INUnit) e.Row))
      sender.RaiseExceptionHandling<INUnit.unitMultDiv>(e.Row, (object) ((INUnit) e.Row).UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
    else
      sender.RaiseExceptionHandling<INUnit.unitMultDiv>(e.Row, (object) null, (Exception) null);
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
      nullable = row.ItemClassID;
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue && this.TopCache.Current != null)
      {
        int? key = this.TopGetValue<itemClassID, int?>();
        if (!this._persisted.ContainsKey(key))
          this._persisted.Add(key, row.ItemClassID);
        row.ItemClassID = key;
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
    if (e.TranStatus != 2 || (e.Operation & 3) != 2 || !this._persisted.TryGetValue(((INUnit) e.Row).ItemClassID, out nullable))
      return;
    ((INUnit) e.Row).ItemClassID = nullable;
  }

  private INLotSerClass ReadLotSerClass()
  {
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[BqlCommand.GetItemType(typeof (lotSerClass))];
    return INLotSerClass.PK.FindDirty(((PXSelectBase) this)._Graph, (string) cach.GetValue(cach.Current, typeof (lotSerClass).Name));
  }

  private INUnit ResolveItemClassConversion(int? itemClassID, string baseUnit, string fromUnit)
  {
    return new INUnit()
    {
      UnitType = new short?((short) 2),
      ItemClassID = itemClassID,
      InventoryID = new int?(0),
      FromUnit = fromUnit,
      ToUnit = baseUnit,
      UnitRate = new Decimal?(1M),
      PriceAdjustmentMultiplier = new Decimal?(1M),
      UnitMultDiv = "M"
    };
  }
}
