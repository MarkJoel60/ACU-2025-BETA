// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.AddLotSerialPanelExtBase`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions;

public abstract class AddLotSerialPanelExtBase<TGraph, TDocument, TLine, TSplit> : 
  LotSerialGraphExtBase<
  #nullable disable
  TGraph>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  public PXFilter<AddLotSerialHeader> LotSerialNbrHeader;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemLotSerialAttributesHeaderSelected, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  AddLotSerialHeader.onlyAvailable>, 
  #nullable disable
  NotEqual<True>>>>>.Or<BqlOperand<
  #nullable enable
  INItemLotSerialAttributesHeaderSelected.qtyHardAvail, IBqlDecimal>.IsGreater<
  #nullable disable
  decimal0>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  AddLotSerialHeader.lotSerClassID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  INItemLotSerialAttributesHeaderSelected.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddLotSerialHeader.lotSerClassID, IBqlString>.FromCurrent.NoDefault>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  AddLotSerialHeader.inventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  INItemLotSerialAttributesHeaderSelected.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddLotSerialHeader.inventoryID, IBqlInt>.FromCurrent.NoDefault>>>>, 
  #nullable disable
  INItemLotSerialAttributesHeaderSelected>.View LotSerialNbrResult;
  public PXAction<TDocument> showAddLotSerialNbrPanel;
  public PXAction<TDocument> addSelectedLotSerialNbrs;

  public LineSplittingExtension<TGraph, TDocument, TLine, TSplit> LineSplittingExt
  {
    get => this.Base.FindImplementation<LineSplittingExtension<TGraph, TDocument, TLine, TSplit>>();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.LotSerialNbrResult).AllowDelete = ((PXSelectBase) this.LotSerialNbrResult).AllowInsert = false;
    PXCache<INItemLotSerialAttributesHeaderSelected> pxCache = GraphHelper.Caches<INItemLotSerialAttributesHeaderSelected>((PXGraph) this.Base);
    PXUIFieldAttribute.SetEnabled((PXCache) pxCache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INItemLotSerialAttributesHeaderSelected.selected>((PXCache) pxCache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INItemLotSerialAttributesHeaderSelected.qtySelected>((PXCache) pxCache, (object) null, true);
    foreach (string str1 in ((IEnumerable<string>) ((PXCache) pxCache).Fields).Where<string>((Func<string, bool>) (f => f.StartsWith("Attribute", StringComparison.OrdinalIgnoreCase))))
    {
      PXGraph.FieldSelectingEvents fieldSelecting = this.Base.FieldSelecting;
      Type type = typeof (INItemLotSerialAttributesHeaderSelected);
      string str2 = str1;
      AddLotSerialPanelExtBase<TGraph, TDocument, TLine, TSplit> serialPanelExtBase = this;
      // ISSUE: virtual method pointer
      PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) serialPanelExtBase, __vmethodptr(serialPanelExtBase, OnAttributeFieldSelecting));
      fieldSelecting.AddHandler(type, str2, pxFieldSelecting);
    }
  }

  protected virtual void OnAttributeFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXFieldState pxFieldState = (PXFieldState) e.ReturnState;
    if (pxFieldState == null)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState = e.ReturnState;
      Type type = typeof (object);
      bool? nullable1 = new bool?();
      bool? nullable2 = new bool?();
      int? nullable3 = new int?();
      int? nullable4 = new int?();
      int? nullable5 = new int?();
      bool? nullable6 = new bool?();
      bool? nullable7 = new bool?();
      bool? nullable8 = new bool?();
      PXFieldState instance;
      pxFieldState = instance = PXFieldState.CreateInstance(returnState, type, nullable1, nullable2, nullable3, nullable4, nullable5, (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable6, nullable7, nullable8, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      selectingEventArgs.ReturnState = (object) instance;
    }
    pxFieldState.Enabled = false;
  }

  protected virtual IEnumerable lotSerialNbrResult()
  {
    AddLotSerialPanelExtBase<TGraph, TDocument, TLine, TSplit> serialPanelExtBase = this;
    PXView pxView = new PXView((PXGraph) serialPanelExtBase.Base, false, PXView.View.BqlSelect);
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    if (PXView.Filters != null)
    {
      foreach (PXFilterRow filter in PXView.Filters)
        pxFilterRowList.AddRange(serialPanelExtBase.ProcessResultFilter(filter));
    }
    int startRow = PXView.StartRow;
    int maximumRows = PXView.MaximumRows;
    int num = 0;
    foreach (object obj in pxView.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, pxFilterRowList.ToArray(), ref startRow, maximumRows, ref num))
      yield return obj;
    PXView.StartRow = 0;
  }

  private IEnumerable<PXFilterRow> ProcessResultFilter(PXFilterRow filter)
  {
    string dataField = filter.DataField;
    if ((dataField != null ? (dataField.StartsWith("Attribute", StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0)
      return this.ProcessResultFilterAttribute(filter, this.ConvertFieldNameToAttributeID(filter.DataField));
    return (IEnumerable<PXFilterRow>) new PXFilterRow[1]
    {
      filter
    };
  }

  private IEnumerable<PXFilterRow> ProcessResultFilterAttribute(
    PXFilterRow filter,
    string attributeID)
  {
    if (filter.Condition != 6)
      return (IEnumerable<PXFilterRow>) new PXFilterRow[1]
      {
        filter
      };
    KeyValueHelper.Attribute keyValueAttribute = this.GetKeyValueAttribute(attributeID);
    switch (keyValueAttribute.ControlType)
    {
      case 1:
      case 7:
        return (IEnumerable<PXFilterRow>) new PXFilterRow[1]
        {
          filter
        };
      case 2:
      case 6:
        return this.ProcessResultFilterAttributeCombo(filter, keyValueAttribute);
      default:
        return (IEnumerable<PXFilterRow>) new PXFilterRow[0];
    }
  }

  private IEnumerable<PXFilterRow> ProcessResultFilterAttributeCombo(
    PXFilterRow filter,
    KeyValueHelper.Attribute crAttribute)
  {
    List<PXFilterRow> list = crAttribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (v =>
    {
      if (v.Disabled)
        return false;
      string description = v.Description;
      return description != null && description.IndexOf(filter.Value as string, StringComparison.OrdinalIgnoreCase) >= 0;
    })).Select<KeyValueHelper.AttributeValue, PXFilterRow>((Func<KeyValueHelper.AttributeValue, PXFilterRow>) (v =>
    {
      PXFilterRow pxFilterRow = (PXFilterRow) filter.Clone();
      pxFilterRow.Value = (object) v.ValueID;
      return pxFilterRow;
    })).ToList<PXFilterRow>();
    if (list.Count == 0)
      return (IEnumerable<PXFilterRow>) list;
    list[0].OpenBrackets = 1;
    list[list.Count - 1].CloseBrackets = 1;
    return (IEnumerable<PXFilterRow>) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ShowAddLotSerialNbrPanel(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    if (((PXSelectBase<AddLotSerialHeader>) this.LotSerialNbrHeader).AskExt(AddLotSerialPanelExtBase<TGraph, TDocument, TLine, TSplit>.\u003C\u003Ec.\u003C\u003E9__11_0 ?? (AddLotSerialPanelExtBase<TGraph, TDocument, TLine, TSplit>.\u003C\u003Ec.\u003C\u003E9__11_0 = new PXView.InitializePanel((object) AddLotSerialPanelExtBase<TGraph, TDocument, TLine, TSplit>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CShowAddLotSerialNbrPanel\u003Eb__11_0)))) == 1)
    {
      this.AddLines();
      return this.AddSelectedLotSerialNbrs(adapter);
    }
    ((PXSelectBase) this.LotSerialNbrResult).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(CommitChanges = true, DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddSelectedLotSerialNbrs(PXAdapter adapter)
  {
    this.AddLines();
    ((PXSelectBase) this.LotSerialNbrResult).Cache.Clear();
    return adapter.Get();
  }

  protected virtual void AddLines()
  {
    PXCache headerCache = ((PXSelectBase) this.LotSerialNbrResult).Cache;
    foreach (INItemLotSerialAttributesHeaderSelected lotSerial in GraphHelper.RowCast<INItemLotSerialAttributesHeaderSelected>(headerCache.Cached).Where<INItemLotSerialAttributesHeaderSelected>((Func<INItemLotSerialAttributesHeaderSelected, bool>) (a => !EnumerableExtensions.IsIn<PXEntryStatus>(headerCache.GetStatus((object) a), (PXEntryStatus) 3, (PXEntryStatus) 4))))
    {
      if (lotSerial.Selected.GetValueOrDefault())
      {
        Decimal? qtySelected = lotSerial.QtySelected;
        Decimal num = 0M;
        if (qtySelected.GetValueOrDefault() > num & qtySelected.HasValue)
        {
          this.AddLineWithSplit(lotSerial);
          lotSerial.Selected = new bool?(false);
          lotSerial.QtySelected = new Decimal?(0M);
          headerCache.Update((object) lotSerial);
        }
      }
    }
  }

  protected virtual void AddLineWithSplit(INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    TLine line = this.InsertLine(GraphHelper.Caches<TLine>((PXGraph) this.Base).Rows.CreateInstance(), lotSerial);
    this.InsertSplit(GraphHelper.Caches<TSplit>((PXGraph) this.Base).Rows.CreateInstance(), line, lotSerial);
  }

  protected abstract TLine InsertLine(
    TLine newLine,
    INItemLotSerialAttributesHeaderSelected lotSerial);

  protected abstract TSplit InsertSplit(
    TSplit newSplit,
    TLine line,
    INItemLotSerialAttributesHeaderSelected lotSerial);

  protected virtual void _(
    Events.FieldUpdated<AddLotSerialHeader, AddLotSerialHeader.lotSerClassID> e)
  {
    this.ResetFilterInventoryID(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<AddLotSerialHeader, AddLotSerialHeader.lotSerClassID>>) e).Cache, e.Row);
  }

  protected virtual void _(Events.RowUpdated<AddLotSerialHeader> e)
  {
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<AddLotSerialHeader>>) e).Cache.ObjectsEqual<AddLotSerialHeader.lotSerClassID, AddLotSerialHeader.inventoryID>((object) e.Row, (object) e.OldRow))
      return;
    this.RefreshAttributes(e.Row);
  }

  protected virtual void RefreshAttributes(AddLotSerialHeader header)
  {
    IEnumerable<INItemLotSerialAttribute> source;
    if (header.InventoryID.HasValue)
      source = GraphHelper.RowCast<INItemLotSerialAttribute>((IEnumerable) PXSelectBase<INItemLotSerialAttribute, PXViewOf<INItemLotSerialAttribute>.BasedOn<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemLotSerialAttribute.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemLotSerialAttribute.isActive, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) header.InventoryID
      }));
    else if (header.LotSerClassID != null)
      source = GraphHelper.RowCast<INItemLotSerialAttribute>((IEnumerable) PXSelectBase<INItemLotSerialAttribute, PXViewOf<INItemLotSerialAttribute>.BasedOn<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionLite<Exists<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.inventoryID, Equal<INItemLotSerialAttribute.inventoryID>>>>>.And<BqlOperand<InventoryItem.lotSerClassID, IBqlString>.IsEqual<P.AsString>>>>>.And<BqlOperand<INItemLotSerialAttribute.isActive, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<INItemLotSerialAttribute.attributeID>>>.Order<By<BqlField<INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) header.LotSerClassID
      }));
    else
      source = GraphHelper.RowCast<INItemLotSerialAttribute>((IEnumerable) PXSelectBase<INItemLotSerialAttribute, PXViewOf<INItemLotSerialAttribute>.BasedOn<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemLotSerialAttribute.isActive, IBqlBool>.IsEqual<True>>.Aggregate<To<GroupBy<INItemLotSerialAttribute.attributeID>>>.Order<By<BqlField<INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    header.AttributeIdentifiers = source != null ? source.Select<INItemLotSerialAttribute, string>((Func<INItemLotSerialAttribute, string>) (a => a.AttributeID)).ToArray<string>() : (string[]) null;
  }

  private void ResetFilterInventoryID(PXCache cache, AddLotSerialHeader filter)
  {
    int? inventoryId = (int?) filter?.InventoryID;
    if (!inventoryId.HasValue)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, inventoryId);
    if (inventoryItem == null || filter.LotSerClassID == null || !(inventoryItem.LotSerClassID != filter.LotSerClassID))
      return;
    cache.SetValueExt<AddLotSerialHeader.inventoryID>((object) filter, (object) null);
  }

  protected virtual void _(
    Events.FieldUpdated<INItemLotSerialAttributesHeaderSelected, INItemLotSerialAttributesHeaderSelected.selected> e)
  {
    bool? selected = e.Row.Selected;
    Decimal? qtySelected = e.Row.QtySelected;
    if (selected.GetValueOrDefault())
    {
      if (qtySelected.HasValue)
      {
        Decimal? nullable = qtySelected;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
          return;
      }
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INItemLotSerialAttributesHeaderSelected, INItemLotSerialAttributesHeaderSelected.selected>>) e).Cache.SetValueExt<INItemLotSerialAttributesHeaderSelected.qtySelected>((object) e.Row, (object) 1M);
    }
    else
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INItemLotSerialAttributesHeaderSelected, INItemLotSerialAttributesHeaderSelected.selected>>) e).Cache.SetValueExt<INItemLotSerialAttributesHeaderSelected.qtySelected>((object) e.Row, (object) 0M);
  }

  protected virtual void _(
    Events.FieldUpdated<INItemLotSerialAttributesHeaderSelected, INItemLotSerialAttributesHeaderSelected.qtySelected> e)
  {
    bool? selected = e.Row.Selected;
    Decimal? qtySelected = e.Row.QtySelected;
    Decimal? nullable1 = qtySelected;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue && !selected.GetValueOrDefault())
    {
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INItemLotSerialAttributesHeaderSelected, INItemLotSerialAttributesHeaderSelected.qtySelected>>) e).Cache.SetValueExt<INItemLotSerialAttributesHeaderSelected.selected>((object) e.Row, (object) true);
    }
    else
    {
      if (qtySelected.HasValue)
      {
        Decimal? nullable2 = qtySelected;
        Decimal num2 = 0M;
        if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
          return;
      }
      if (!selected.GetValueOrDefault())
        return;
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INItemLotSerialAttributesHeaderSelected, INItemLotSerialAttributesHeaderSelected.qtySelected>>) e).Cache.SetValueExt<INItemLotSerialAttributesHeaderSelected.selected>((object) e.Row, (object) false);
    }
  }

  protected virtual void _(Events.RowSelected<TDocument> e)
  {
    if ((object) e.Row == null)
      return;
    ((PXAction) this.showAddLotSerialNbrPanel).SetEnabled(!this.IsDocumentReadonly(e.Row));
  }

  protected abstract bool IsDocumentReadonly(TDocument document);
}
