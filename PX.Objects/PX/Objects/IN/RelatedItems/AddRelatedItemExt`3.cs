// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.AddRelatedItemExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

public abstract class AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine> : 
  PXGraphExtension<
  #nullable disable
  TGraph>
  where TGraph : PXGraph
  where TSubstitutableDocument : class, IBqlTable, ISubstitutableDocument, new()
  where TSubstitutableLine : class, IBqlTable, ISubstitutableLine, new()
{
  public PXFilter<PX.Objects.IN.RelatedItems.RelatedItemsFilter> RelatedItemsFilter;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<RelatedItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  PX.Objects.IN.RelatedItems.RelatedItemsFilter.onlyAvailableItems>, 
  #nullable disable
  NotEqual<True>>>>, Or<BqlOperand<
  #nullable enable
  RelatedItem.stkItem, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  RelatedItem.relation, 
  #nullable disable
  Equal<InventoryRelation.substitute>>>>>.And<BqlOperand<
  #nullable enable
  RelatedItem.required, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>.Or<BqlOperand<
  #nullable enable
  RelatedItem.baseAvailableQty, IBqlDecimal>.IsGreater<
  #nullable disable
  decimal0>>>.Order<By<BqlField<
  #nullable enable
  RelatedItem.relation, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.rank, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.inventoryID, IBqlInt>.Asc>>, 
  #nullable disable
  RelatedItem>.View AllRelatedItems;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<RelatedItem, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  RelatedItem.relation, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.rank, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.inventoryID, IBqlInt>.Asc>>, 
  #nullable disable
  RelatedItem>.View SubstituteItems;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<RelatedItem, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  RelatedItem.relation, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.rank, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.inventoryID, IBqlInt>.Asc>>, 
  #nullable disable
  RelatedItem>.View UpSellItems;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<RelatedItem, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  RelatedItem.relation, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.rank, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.inventoryID, IBqlInt>.Asc>>, 
  #nullable disable
  RelatedItem>.View CrossSellItems;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<RelatedItem, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  RelatedItem.relation, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.rank, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  RelatedItem.inventoryID, IBqlInt>.Asc>>, 
  #nullable disable
  RelatedItem>.View OtherRelatedItems;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<RelatedItemHistory, TypeArrayOf<IFbqlJoin>.Empty>, RelatedItemHistory>.View RelatedItemsHistory;
  public PXAction<TSubstitutableDocument> addRelatedItems;

  protected virtual bool SplitSerialTrackingItems => false;

  protected abstract DateTime? GetDocumentDate(TSubstitutableDocument document);

  protected PXCache DocumentCache
  {
    get => (PXCache) GraphHelper.Caches<TSubstitutableDocument>((PXGraph) this.Base);
  }

  protected PXCache LinesCache
  {
    get => (PXCache) GraphHelper.Caches<TSubstitutableLine>((PXGraph) this.Base);
  }

  protected virtual ISubstitutableLineExt GetSubstitutableLineExt(
    TSubstitutableLine substitutableLine)
  {
    // ISSUE: variable of a boxed type
    __Boxed<TSubstitutableLine> local = (object) substitutableLine;
    if (local == null)
      return (ISubstitutableLineExt) null;
    PXCacheExtension[] extensions = PXCacheEx.GetExtensions((IBqlTable) local);
    if (extensions == null)
      return (ISubstitutableLineExt) null;
    IEnumerable<ISubstitutableLineExt> source = extensions.OfType<ISubstitutableLineExt>();
    return source == null ? (ISubstitutableLineExt) null : source.FirstOrDefault<ISubstitutableLineExt>();
  }

  protected virtual bool AllowRelatedItems(TSubstitutableDocument document)
  {
    if ((object) document == null || !document.SuggestRelatedItems.GetValueOrDefault())
      return false;
    return !this.Base.IsImport || this.Base.IsCopyPasteContext;
  }

  protected virtual bool AllowRelatedItems(TSubstitutableLine line)
  {
    ISubstitutableLineExt substitutableLineExt = this.GetSubstitutableLineExt(line);
    if (substitutableLineExt == null)
      return false;
    int? relatedItemsRelation = substitutableLineExt.RelatedItemsRelation;
    int num = 0;
    return relatedItemsRelation.GetValueOrDefault() > num & relatedItemsRelation.HasValue;
  }

  protected virtual TSubstitutableLine FindFocusFor(TSubstitutableLine line)
  {
    return default (TSubstitutableLine);
  }

  public virtual IEnumerable allRelatedItems()
  {
    return (IEnumerable) this.LoadRelatedItems((string) null);
  }

  public virtual IEnumerable substituteItems() => (IEnumerable) this.LoadRelatedItems("SUBST");

  public virtual IEnumerable upSellItems() => (IEnumerable) this.LoadRelatedItems("USELL");

  public virtual IEnumerable crossSellItems() => (IEnumerable) this.LoadRelatedItems("CSELL");

  public virtual IEnumerable otherRelatedItems() => (IEnumerable) this.LoadRelatedItems("OTHER");

  protected virtual BqlCommand RelatedItemsLoadCommand(string relation)
  {
    BqlCommand bqlCommand = ((PXSelectBase) this.AllRelatedItems).View.BqlSelect;
    if (!string.IsNullOrEmpty(relation))
      bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<RelatedItem.relation, IBqlString>.IsEqual<P.AsString.ASCII>>>();
    if (!((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current.ShowForAllWarehouses.GetValueOrDefault() && ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current.SiteID.HasValue)
      bqlCommand = bqlCommand.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RelatedItem.siteID, Equal<BqlField<PX.Objects.IN.RelatedItems.RelatedItemsFilter.siteID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<RelatedItem.siteID, IBqlInt>.IsNull>>>();
    return bqlCommand;
  }

  public virtual RelatedItem[] LoadRelatedItems(string relation)
  {
    if (!((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current.InventoryID.HasValue)
      return Array<RelatedItem>.Empty;
    PXCache cache = ((PXSelectBase) this.AllRelatedItems).Cache;
    RelatedItem instance = PXView.MaximumRows == 1 ? cache.CreateInstance<RelatedItem>(PXView.SortColumns, PXView.Searches) : (RelatedItem) null;
    if (NonGenericIEnumerableExtensions.Any_(cache.Cached) && instance != null)
    {
      RelatedItem relatedItem = (RelatedItem) cache.Locate((object) instance);
      if (relatedItem != null)
        return new RelatedItem[1]{ relatedItem };
    }
    if (instance != null)
    {
      int? inventoryId1 = instance.InventoryID;
      int? inventoryId2 = ((TSubstitutableLine) this.LinesCache.Current).InventoryID;
      if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
        return Array<RelatedItem>.Empty;
    }
    object[] objArray = PXView.Parameters;
    BqlCommand bqlCommand = this.RelatedItemsLoadCommand(relation);
    if (!string.IsNullOrEmpty(relation))
      objArray = EnumerableExtensions.Append<object>(objArray, (object) relation);
    int startRow = PXView.StartRow;
    int num = 0;
    RelatedItem[] array = GraphHelper.RowCast<RelatedItem>((IEnumerable) new PXView((PXGraph) this.Base, false, bqlCommand).Select(PXView.Currents, objArray, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num)).ToArray<RelatedItem>();
    PXView.StartRow = 0;
    foreach (RelatedItem relatedItem in array)
    {
      ((PXSelectBase) this.AllRelatedItems).Cache.SetDefaultExt<RelatedItem.qtySelected>((object) relatedItem);
      ((PXSelectBase) this.AllRelatedItems).Cache.SetDefaultExt<RelatedItem.curyUnitPrice>((object) relatedItem);
    }
    return array;
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXNoteAttribute.ForcePassThrow<RelatedItem.noteID>(((PXSelectBase) this.AllRelatedItems).Cache);
  }

  [PXUIField(DisplayName = "Add Related Items", Visible = false)]
  [PXButton]
  public virtual IEnumerable AddRelatedItems(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine>.\u003C\u003Ec__DisplayClass27_0 cDisplayClass270 = new AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine>.\u003C\u003Ec__DisplayClass27_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.document = (TSubstitutableDocument) this.DocumentCache.Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if ((object) cDisplayClass270.document == null || !this.AllowRelatedItems(cDisplayClass270.document))
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.line = (TSubstitutableLine) this.LinesCache.Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if ((object) cDisplayClass270.line == null || !this.AllowRelatedItems(cDisplayClass270.line))
      return adapter.Get();
    // ISSUE: method pointer
    if (((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).AskExt(new PXView.InitializePanel((object) cDisplayClass270, __methodptr(\u003CAddRelatedItems\u003Eb__0))) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine>.\u003C\u003Ec__DisplayClass27_1 cDisplayClass271 = new AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine>.\u003C\u003Ec__DisplayClass27_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass271.filter = ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current;
      // ISSUE: reference to a compiler-generated method
      RelatedItem[] array = ((PXSelectBase) this.AllRelatedItems).Cache.Updated.OfType<RelatedItem>().Where<RelatedItem>(new Func<RelatedItem, bool>(cDisplayClass271.\u003CAddRelatedItems\u003Eb__1)).ToArray<RelatedItem>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.AddSelectedRelatedItems(cDisplayClass271.filter, cDisplayClass270.line, array);
    }
    this.ClearAddRelatedPanelCaches();
    return adapter.Get();
  }

  protected virtual void ClearAddRelatedPanelCaches()
  {
    ((PXSelectBase) this.RelatedItemsFilter).Cache.Clear();
    ((PXSelectBase) this.AllRelatedItems).Cache.Clear();
    ((PXSelectBase) this.AllRelatedItems).Cache.ClearQueryCache();
  }

  protected virtual void InitializeAddRelatedItemsPanel(
    TSubstitutableDocument document,
    TSubstitutableLine line)
  {
    this.ClearAddRelatedPanelCaches();
    this.InitializeFilter(document, line);
  }

  protected virtual PX.Objects.IN.RelatedItems.RelatedItemsFilter InitializeFilter(
    TSubstitutableDocument document,
    TSubstitutableLine line)
  {
    PX.Objects.IN.RelatedItems.RelatedItemsFilter current = ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current;
    current.DocumentDate = this.GetDocumentDate(document);
    current.LineNbr = line.LineNbr;
    current.BranchID = line.BranchID;
    current.SiteID = line.SiteID;
    current.InventoryID = line.InventoryID;
    current.SubItemID = line.SubItemID;
    current.UOM = line.UOM;
    INUnit inUnit = INUnit.UK.ByInventory.Find((PXGraph) this.Base, current.InventoryID, current.UOM);
    current.BaseUnitMultDiv = inUnit != null ? inUnit.UnitMultDiv : throw new PXUnitConversionException();
    current.BaseUnitRate = inUnit.UnitRate;
    current.Qty = current.OriginalQty = line.Qty;
    current.CuryID = document.CuryID;
    current.CuryUnitPrice = line.CuryUnitPrice;
    current.OriginalCuryExtPrice = current.CuryExtPrice = line.CuryExtPrice;
    current.AvailableQty = this.GetAvailableQty(line);
    current.RelatedItemsRelation = (int?) this.GetSubstitutableLineExt(line)?.RelatedItemsRelation;
    InventoryRelation.RelationType valueOrDefault = (InventoryRelation.RelationType) current.RelatedItemsRelation.GetValueOrDefault();
    this.SetTabsVisibility(current, valueOrDefault);
    return current;
  }

  protected virtual Decimal? GetAvailableQty(TSubstitutableLine line)
  {
    INSiteStatusByCostCenter statusByCostCenter = INSiteStatusByCostCenter.PK.Find((PXGraph) this.Base, line.InventoryID, line.SubItemID, line.SiteID, new int?(0));
    return new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.RelatedItemsFilter).Cache, line.InventoryID, line.UOM, ((Decimal?) statusByCostCenter?.QtyAvail).GetValueOrDefault(), INPrecision.QUANTITY));
  }

  protected virtual void SetTabsVisibility(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    InventoryRelation.RelationType relation)
  {
    filter.ShowAllRelatedItems = new bool?(relation == InventoryRelation.RelationType.None || Convert.ToString((int) relation, 2).Replace("0", "").Length > 1);
    filter.ShowSubstituteItems = new bool?(relation.HasFlag((Enum) InventoryRelation.RelationType.Substitute));
    filter.ShowUpSellItems = new bool?(relation.HasFlag((Enum) InventoryRelation.RelationType.UpSell));
    filter.ShowCrossSellItems = new bool?(relation.HasFlag((Enum) InventoryRelation.RelationType.CrossSell));
    filter.ShowOtherRelatedItems = new bool?(relation.HasFlag((Enum) InventoryRelation.RelationType.Other));
  }

  protected virtual void AddSelectedRelatedItems(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine line,
    RelatedItem[] relatedItems)
  {
    if (relatedItems == null || !((IEnumerable<RelatedItem>) relatedItems).Any<RelatedItem>())
      return;
    RelatedItem relatedItem1 = ((IEnumerable<RelatedItem>) relatedItems).First<RelatedItem>();
    if (this.SingleSelection(relatedItem1.Relation))
    {
      TSubstitutableLine copy = (TSubstitutableLine) this.LinesCache.CreateCopy((object) line);
      Decimal? qty1 = filter.Qty;
      Decimal? qty2 = copy.Qty;
      if (qty1.GetValueOrDefault() < qty2.GetValueOrDefault() & qty1.HasValue & qty2.HasValue)
      {
        this.DecreaseOriginalItemQty(filter, line, relatedItem1);
        this.AddRelatedItem(filter, copy, relatedItem1);
      }
      else
        this.SubstituteOriginalItem(filter, line, relatedItem1);
    }
    else
    {
      TSubstitutableLine line1 = line;
      foreach (RelatedItem relatedItem2 in relatedItems)
      {
        TSubstitutableLine focusFor = this.FindFocusFor(line1);
        line1 = this.AddRelatedItem(filter, line, relatedItem2, focusFor).Last<TSubstitutableLine>();
      }
    }
  }

  protected virtual IEnumerable<TSubstitutableLine> AddRelatedItem(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem,
    TSubstitutableLine focus = null)
  {
    TSubstitutableLine[] substitutableLineArray;
    if (this.SplitSerialTrackingItems && this.IsSerialTrackingItem(relatedItem.RelatedInventoryID))
    {
      Decimal num = INUnitAttribute.ConvertToBase(this.LinesCache, relatedItem.RelatedInventoryID, relatedItem.UOM, relatedItem.QtySelected.GetValueOrDefault(), INPrecision.NOROUND);
      substitutableLineArray = this.AddSerialTrackingRelatedItems(filter, originalLine, relatedItem, focus, new Decimal?(num)).ToArray<TSubstitutableLine>();
    }
    else
    {
      focus = focus ?? this.FindFocusFor(originalLine);
      substitutableLineArray = new TSubstitutableLine[1]
      {
        this.AddRelatedItem(filter, originalLine, relatedItem, focus, relatedItem.UOM, relatedItem.QtySelected)
      };
    }
    return (IEnumerable<TSubstitutableLine>) substitutableLineArray;
  }

  protected virtual IEnumerable<TSubstitutableLine> AddSerialTrackingRelatedItems(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem,
    TSubstitutableLine focus,
    Decimal? baseQty)
  {
    List<TSubstitutableLine> substitutableLineList = new List<TSubstitutableLine>();
    Decimal? nullable1 = baseQty;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() <= num1 & nullable1.HasValue)
      return (IEnumerable<TSubstitutableLine>) substitutableLineList;
    Decimal? nullable2 = baseQty;
    Decimal num2 = (Decimal) 1;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() % num2) : new Decimal?();
    Decimal num3 = 0M;
    if (nullable3.GetValueOrDefault() > num3 & nullable3.HasValue)
    {
      Decimal num4 = INUnitAttribute.ConvertFromBase(this.LinesCache, relatedItem.RelatedInventoryID, relatedItem.UOM, Math.Ceiling(baseQty.GetValueOrDefault()), INPrecision.NOROUND);
      ((PXSelectBase) this.AllRelatedItems).Cache.SetValueExt<RelatedItem.qtySelected>((object) relatedItem, (object) num4);
    }
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, relatedItem.RelatedInventoryID);
    TSubstitutableLine line = originalLine;
    while (true)
    {
      nullable2 = baseQty;
      Decimal num5 = 0M;
      if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue)
      {
        focus = focus ?? this.FindFocusFor(line);
        nullable3 = baseQty;
        Decimal num6 = (Decimal) 1;
        Decimal? qty = nullable3.GetValueOrDefault() > num6 & nullable3.HasValue ? new Decimal?((Decimal) 1) : baseQty;
        line = this.AddRelatedItem(filter, originalLine, relatedItem, focus, inventoryItem?.BaseUnit, qty);
        substitutableLineList.Add(line);
        focus = default (TSubstitutableLine);
        nullable3 = baseQty;
        nullable2 = qty;
        baseQty = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
        break;
    }
    return (IEnumerable<TSubstitutableLine>) substitutableLineList;
  }

  protected virtual TSubstitutableLine AddRelatedItem(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem,
    TSubstitutableLine focus,
    string uom,
    Decimal? qty)
  {
    try
    {
      if ((object) focus != null)
      {
        this.LinesCache.InsertPositionMode = true;
        this.LinesCache.InsertPosition = ((IEnumerable<string>) this.LinesCache.Keys).ToDictionary<string, string, object>((Func<string, string>) (x => x), (Func<string, object>) (x => this.LinesCache.GetValue((object) focus, x)));
      }
      TSubstitutableLine substitutableLine1 = new TSubstitutableLine();
      substitutableLine1.InventoryID = relatedItem.RelatedInventoryID;
      substitutableLine1.SubItemID = relatedItem.SubItemID;
      substitutableLine1.SiteID = relatedItem.SiteID;
      substitutableLine1.UOM = uom;
      substitutableLine1.Qty = qty;
      TSubstitutableLine substitutableLine2 = (TSubstitutableLine) this.LinesCache.Insert((object) substitutableLine1);
      Decimal? extPrice = this.CalculateExtPrice(filter, originalLine, relatedItem, substitutableLine2);
      if (extPrice.HasValue)
      {
        substitutableLine2.CuryExtPrice = extPrice;
        substitutableLine2 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine2);
      }
      if (filter.KeepOriginalPrice.GetValueOrDefault())
      {
        substitutableLine2.SkipLineDiscounts = originalLine.SkipLineDiscounts;
        substitutableLine2 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine2);
      }
      if (extPrice.HasValue)
      {
        ValueSetter<TSubstitutableLine> setterFor = PXCacheEx.GetSetterFor<TSubstitutableLine>(this.LinesCache, substitutableLine2);
        // ISSUE: explicit reference operation
        (^ref setterFor).Set<bool?>((Expression<Func<TSubstitutableLine, bool?>>) (x => x.ManualPrice), new bool?(true));
      }
      this.AddRelatedItemHistory(filter, originalLine, substitutableLine2, relatedItem);
      return substitutableLine2;
    }
    finally
    {
      if ((object) focus != null)
      {
        this.LinesCache.InsertPositionMode = false;
        this.LinesCache.InsertPosition = (Dictionary<string, object>) null;
      }
    }
  }

  protected virtual TSubstitutableLine DecreaseOriginalItemQty(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem)
  {
    TSubstitutableLine copy1 = (TSubstitutableLine) this.LinesCache.CreateCopy((object) originalLine);
    TSubstitutableLine copy2 = (TSubstitutableLine) this.LinesCache.CreateCopy((object) originalLine);
    // ISSUE: variable of a boxed type
    __Boxed<TSubstitutableLine> local = (object) copy2;
    Decimal? qty1 = originalLine.Qty;
    Decimal? qty2 = filter.Qty;
    Decimal? nullable = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() - qty2.GetValueOrDefault()) : new Decimal?();
    local.Qty = nullable;
    TSubstitutableLine relatedItemLine = (TSubstitutableLine) this.LinesCache.Update((object) copy2);
    Decimal? extPrice = this.CalculateExtPrice(filter, copy1, relatedItem, relatedItemLine);
    if (extPrice.HasValue)
    {
      relatedItemLine.CuryExtPrice = extPrice;
      relatedItemLine = (TSubstitutableLine) this.LinesCache.Update((object) relatedItemLine);
    }
    return relatedItemLine;
  }

  protected virtual IEnumerable<TSubstitutableLine> SubstituteOriginalItem(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem)
  {
    TSubstitutableLine[] substitutableLineArray;
    if (this.SplitSerialTrackingItems && this.IsSerialTrackingItem(relatedItem.RelatedInventoryID))
    {
      TSubstitutableLine copy = (TSubstitutableLine) this.LinesCache.CreateCopy((object) originalLine);
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, relatedItem.RelatedInventoryID);
      this.SubstituteOriginalItem(filter, originalLine, relatedItem, inventoryItem.BaseUnit, new Decimal?((Decimal) 1));
      Decimal num = Decimal.op_Decrement(INUnitAttribute.ConvertToBase(this.LinesCache, relatedItem.RelatedInventoryID, relatedItem.UOM, relatedItem.QtySelected.GetValueOrDefault(), INPrecision.NOROUND));
      substitutableLineArray = this.AddSerialTrackingRelatedItems(filter, copy, relatedItem, default (TSubstitutableLine), new Decimal?(num)).ToArray<TSubstitutableLine>();
    }
    else
      substitutableLineArray = new TSubstitutableLine[1]
      {
        this.SubstituteOriginalItem(filter, originalLine, relatedItem, relatedItem.UOM, relatedItem.QtySelected)
      };
    return (IEnumerable<TSubstitutableLine>) substitutableLineArray;
  }

  protected virtual TSubstitutableLine SubstituteOriginalItem(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem,
    string uom,
    Decimal? qty)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine>.\u003C\u003Ec__DisplayClass39_0 cDisplayClass390 = new AddRelatedItemExt<TGraph, TSubstitutableDocument, TSubstitutableLine>.\u003C\u003Ec__DisplayClass39_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.uom = uom;
    TSubstitutableLine copy = (TSubstitutableLine) this.LinesCache.CreateCopy((object) originalLine);
    TSubstitutableLine substitutableLine1 = (TSubstitutableLine) this.LinesCache.CreateCopy((object) originalLine);
    substitutableLine1.InventoryID = relatedItem.RelatedInventoryID;
    substitutableLine1.SubItemID = relatedItem.SubItemID;
    int? nullable1 = relatedItem.SiteID ?? originalLine.SiteID;
    try
    {
      // ISSUE: method pointer
      this.Base.FieldDefaulting.AddHandler(typeof (TSubstitutableLine), "UOM", new PXFieldDefaulting((object) cDisplayClass390, __methodptr(\u003CSubstituteOriginalItem\u003Eg__uomDefaulting\u007C0)));
      substitutableLine1 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine1);
    }
    finally
    {
      // ISSUE: method pointer
      this.Base.FieldDefaulting.RemoveHandler(typeof (TSubstitutableLine), "UOM", new PXFieldDefaulting((object) cDisplayClass390, __methodptr(\u003CSubstituteOriginalItem\u003Eg__uomDefaulting\u007C0)));
    }
    substitutableLine1.Qty = qty;
    substitutableLine1.SiteID = nullable1;
    TSubstitutableLine substitutableLine2 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine1);
    Decimal? extPrice = this.CalculateExtPrice(filter, copy, relatedItem, substitutableLine2);
    bool? nullable2;
    if (extPrice.HasValue)
    {
      substitutableLine2.CuryExtPrice = extPrice;
      substitutableLine2 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine2);
    }
    else
    {
      nullable2 = substitutableLine2.ManualPrice;
      if (nullable2.GetValueOrDefault())
      {
        substitutableLine2.ManualPrice = new bool?(false);
        substitutableLine2 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine2);
      }
    }
    nullable2 = filter.KeepOriginalPrice;
    if (nullable2.GetValueOrDefault())
    {
      substitutableLine2.SkipLineDiscounts = copy.SkipLineDiscounts;
      substitutableLine2 = (TSubstitutableLine) this.LinesCache.Update((object) substitutableLine2);
    }
    this.UpdateRelatedItemHistory(filter, copy, substitutableLine2, relatedItem);
    return substitutableLine2;
  }

  protected virtual Decimal? CalculateExtPrice(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    RelatedItem relatedItem,
    TSubstitutableLine relatedItemLine)
  {
    int? inventoryId1 = originalLine.InventoryID;
    int? inventoryId2 = relatedItemLine.InventoryID;
    bool? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
    {
      nullable1 = originalLine.ManualPrice;
      if (!nullable1.GetValueOrDefault())
        return new Decimal?();
      nullable2 = originalLine.Qty;
      nullable3 = filter.Qty;
      nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      nullable1 = filter.KeepOriginalPrice;
      if (!nullable1.GetValueOrDefault())
        return new Decimal?();
      nullable4 = filter.Qty;
    }
    nullable1 = originalLine.ManualPrice;
    Decimal? nullable5;
    if (!nullable1.GetValueOrDefault())
    {
      nullable5 = originalLine.CuryUnitPrice;
    }
    else
    {
      nullable3 = originalLine.Qty;
      if (!(nullable3.GetValueOrDefault() == 0M))
      {
        nullable3 = originalLine.CuryExtPrice;
        nullable2 = originalLine.Qty;
        nullable5 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable5 = originalLine.CuryExtPrice;
    }
    nullable2 = nullable5;
    nullable3 = nullable4;
    Decimal? extPrice1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
    int? inventoryId3 = originalLine.InventoryID;
    int? inventoryId4 = relatedItemLine.InventoryID;
    if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue)
      return extPrice1;
    Decimal? nullable6 = relatedItem.QtySelected;
    if (relatedItem.UOM != relatedItemLine.UOM)
    {
      nullable6 = new Decimal?(INUnitAttribute.ConvertToBase(this.LinesCache, relatedItemLine.InventoryID, relatedItem.UOM, nullable6.GetValueOrDefault(), INPrecision.NOROUND));
      nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase(this.LinesCache, relatedItemLine.InventoryID, relatedItemLine.UOM, nullable6.GetValueOrDefault(), INPrecision.NOROUND));
    }
    Decimal? extPrice2 = new Decimal?(0M);
    nullable3 = nullable6;
    Decimal num = 0M;
    if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
    {
      Decimal? nullable7 = extPrice1;
      Decimal? nullable8 = relatedItemLine.Qty;
      nullable3 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable8.GetValueOrDefault()) : new Decimal?();
      nullable2 = nullable6;
      Decimal? nullable9;
      if (!(nullable3.HasValue & nullable2.HasValue))
      {
        nullable8 = new Decimal?();
        nullable9 = nullable8;
      }
      else
        nullable9 = new Decimal?(nullable3.GetValueOrDefault() / nullable2.GetValueOrDefault());
      extPrice2 = nullable9;
    }
    return extPrice2;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.RelatedItems.RelatedItemsFilter> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.RelatedItems.RelatedItemsFilter.keepOriginalPrice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.RelatedItems.RelatedItemsFilter>>) e).Cache, (object) e.Row, this.AllowToKeepOriginalPrice());
  }

  protected virtual void _(PX.Data.Events.RowSelected<TSubstitutableDocument> e)
  {
    if ((object) e.Row == null)
      return;
    this.SetRelatedItemsVisible(this.AllowRelatedItems(e.Row));
  }

  protected virtual void _(PX.Data.Events.RowInserted<TSubstitutableLine> e)
  {
    this.ResetSubstitutionRequired(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<RelatedItem, RelatedItem.curyUnitPrice> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<RelatedItem, RelatedItem.curyUnitPrice>, RelatedItem, object>) e).NewValue = (object) this.CalculateUnitPrice(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<RelatedItem, RelatedItem.curyUnitPrice>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<RelatedItem> e)
  {
    if (e.Row == null)
      return;
    this.RaiseRelatedItemWarning(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<RelatedItem> e)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<RelatedItem>>) e).Cache.ObjectsEqual<RelatedItem.selected>((object) e.OldRow, (object) e.Row))
    {
      bool? nullable = e.Row.Selected;
      if (nullable.GetValueOrDefault())
      {
        RelatedItem[] array = EnumerableExtensions.Except<RelatedItem>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<RelatedItem>>) e).Cache.Updated.OfType<RelatedItem>(), e.Row).Where<RelatedItem>((Func<RelatedItem, bool>) (x => x.Selected.GetValueOrDefault())).ToArray<RelatedItem>();
        if (((IEnumerable<RelatedItem>) array).Any<RelatedItem>() && (this.SingleSelection(e.Row.Relation) || ((IEnumerable<RelatedItem>) array).Any<RelatedItem>((Func<RelatedItem, bool>) (x => x.Relation != e.Row.Relation))))
        {
          foreach (RelatedItem relatedItem in array)
          {
            ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<RelatedItem>>) e).Cache.SetValue<RelatedItem.selected>((object) relatedItem, (object) false);
            this.GetRelatedItemsView(relatedItem)?.RequestRefresh();
          }
          nullable = ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current.KeepOriginalPrice;
          if (nullable.GetValueOrDefault() && this.AllowToKeepOriginalPrice(e.Row.Relation) != this.AllowToKeepOriginalPrice(array[0].Relation))
          {
            ((PXSelectBase) this.RelatedItemsFilter).Cache.SetValueExt<PX.Objects.IN.RelatedItems.RelatedItemsFilter.keepOriginalPrice>((object) ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current, (object) false);
            ((PXSelectBase) this.RelatedItemsFilter).View.RequestRefresh();
          }
        }
      }
      else
      {
        nullable = ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current.KeepOriginalPrice;
        if (nullable.GetValueOrDefault() && this.AllowToKeepOriginalPrice(e.Row.Relation) && !this.AllowToKeepOriginalPrice())
        {
          ((PXSelectBase) this.RelatedItemsFilter).Cache.SetValueExt<PX.Objects.IN.RelatedItems.RelatedItemsFilter.keepOriginalPrice>((object) ((PXSelectBase<PX.Objects.IN.RelatedItems.RelatedItemsFilter>) this.RelatedItemsFilter).Current, (object) false);
          ((PXSelectBase) this.RelatedItemsFilter).View.RequestRefresh();
        }
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<RelatedItem>>) e).Cache.ClearQueryCache();
      }
    }
    ((PXSelectBase) this.AllRelatedItems).View.RequestRefresh();
    this.GetRelatedItemsView(e.Row)?.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.IN.RelatedItems.RelatedItemsFilter> e)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.IN.RelatedItems.RelatedItemsFilter>>) e).Cache.ObjectsEqual<PX.Objects.IN.RelatedItems.RelatedItemsFilter.qty>((object) e.OldRow, (object) e.Row))
    {
      foreach (PXResult<RelatedItem> pxResult in ((PXSelectBase<RelatedItem>) this.AllRelatedItems).Select(Array.Empty<object>()))
        ((PXSelectBase) this.AllRelatedItems).Cache.SetDefaultExt<RelatedItem.qtySelected>((object) PXResult<RelatedItem>.op_Implicit(pxResult));
    }
    else if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.IN.RelatedItems.RelatedItemsFilter>>) e).Cache.ObjectsEqual<PX.Objects.IN.RelatedItems.RelatedItemsFilter.onlyAvailableItems>((object) e.OldRow, (object) e.Row))
    {
      TSubstitutableLine current = (TSubstitutableLine) this.LinesCache.Current;
      InventoryRelation.RelationType relation = InventoryRelation.RelationType.None;
      bool? onlyAvailableItems = e.Row.OnlyAvailableItems;
      bool? availableRelatedItems = (bool?) PXResultset<SOSetup>.op_Implicit(PXSetup<SOSetup>.Select((PXGraph) this.Base, Array.Empty<object>()))?.ShowOnlyAvailableRelatedItems;
      if (onlyAvailableItems.GetValueOrDefault() == availableRelatedItems.GetValueOrDefault() & onlyAvailableItems.HasValue == availableRelatedItems.HasValue)
        relation = (InventoryRelation.RelationType) ((int?) this.GetSubstitutableLineExt(current)?.RelatedItemsRelation).GetValueOrDefault();
      else
        this.LinesCache.GetAttributesOfType<RelatedItemsAttribute>((object) current, "RelatedItems").FirstOrDefault<RelatedItemsAttribute>()?.FindRelatedItems((PXGraph) this.Base, (ISubstitutableLine) current, e.Row.OnlyAvailableItems, out relation, out InventoryRelation.RelationType _);
      this.SetTabsVisibility(e.Row, relation);
    }
    ((PXSelectBase) this.AllRelatedItems).Cache.ClearQueryCache();
  }

  protected virtual PXView GetRelatedItemsView(RelatedItem relatedItem)
  {
    switch (relatedItem.Relation)
    {
      case "SUBST":
        return ((PXSelectBase) this.SubstituteItems).View;
      case "USELL":
        return ((PXSelectBase) this.UpSellItems).View;
      case "CSELL":
        return ((PXSelectBase) this.CrossSellItems).View;
      case "OTHER":
        return ((PXSelectBase) this.OtherRelatedItems).View;
      default:
        return (PXView) null;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<TSubstitutableLine> e)
  {
    int? inventoryId1;
    if (e.OldRow.InventoryID.HasValue)
    {
      int? inventoryId2 = e.Row.InventoryID;
      inventoryId1 = e.OldRow.InventoryID;
      if (!(inventoryId2.GetValueOrDefault() == inventoryId1.GetValueOrDefault() & inventoryId2.HasValue == inventoryId1.HasValue))
        this.RemoveLineFromHistory(e.Row);
    }
    inventoryId1 = e.Row.InventoryID;
    int? inventoryId3 = e.OldRow.InventoryID;
    if (inventoryId1.GetValueOrDefault() == inventoryId3.GetValueOrDefault() & inventoryId1.HasValue == inventoryId3.HasValue)
      return;
    this.ResetSubstitutionRequired(e.Row);
  }

  protected virtual Decimal? CalculateUnitPrice(PXCache cache, RelatedItem relatedItem)
  {
    TSubstitutableDocument current = (TSubstitutableDocument) this.DocumentCache.Current;
    TSubstitutableLine instance = (TSubstitutableLine) this.LinesCache.CreateInstance();
    instance.CustomerID = current.CustomerID;
    instance.InventoryID = relatedItem.RelatedInventoryID;
    instance.SiteID = relatedItem.SiteID;
    instance.UOM = relatedItem.UOM;
    instance.Qty = relatedItem.QtySelected;
    object obj;
    this.LinesCache.RaiseFieldDefaulting("CuryUnitPrice", (object) instance, ref obj);
    return new Decimal?((obj as Decimal?).GetValueOrDefault());
  }

  protected virtual RelatedItemHistory UpdateRelatedItemHistory(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    TSubstitutableLine relatedLine,
    RelatedItem relatedItem)
  {
    RelatedItemHistory historyLine = this.FindHistoryLine(relatedLine) ?? new RelatedItemHistory();
    this.FillRelatedItemHistory(historyLine, filter, originalLine, relatedLine, relatedItem);
    return (RelatedItemHistory) ((PXSelectBase) this.RelatedItemsHistory).Cache.Update((object) historyLine);
  }

  protected virtual RelatedItemHistory AddRelatedItemHistory(
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    TSubstitutableLine relatedLine,
    RelatedItem relatedItem)
  {
    RelatedItemHistory historyLine = new RelatedItemHistory();
    this.FillRelatedItemHistory(historyLine, filter, originalLine, relatedLine, relatedItem);
    RelatedItemHistory relatedItemHistory = (RelatedItemHistory) ((PXSelectBase) this.RelatedItemsHistory).Cache.Insert((object) historyLine);
    ISubstitutableLineExt substitutableLineExt = this.GetSubstitutableLineExt(relatedLine);
    if (substitutableLineExt != null)
      substitutableLineExt.HistoryLineID = relatedItemHistory.LineID;
    return relatedItemHistory;
  }

  protected virtual RelatedItemHistory FindHistoryLine(TSubstitutableLine relatedLine)
  {
    ISubstitutableLineExt substitutableLineExt = this.GetSubstitutableLineExt(relatedLine);
    int? historyLineId = (int?) substitutableLineExt?.HistoryLineID;
    if (historyLineId.HasValue)
      return (RelatedItemHistory) ((PXSelectBase) this.RelatedItemsHistory).Cache.Locate((object) new RelatedItemHistory()
      {
        LineID = historyLineId
      }) ?? RelatedItemHistory.PK.Dirty.Find((PXGraph) this.Base, historyLineId);
    RelatedItemHistory historyLine = this.FindHistoryLine(relatedLine.LineNbr);
    if (historyLine != null && substitutableLineExt != null)
      substitutableLineExt.HistoryLineID = historyLine.LineID;
    return historyLine;
  }

  protected abstract RelatedItemHistory FindHistoryLine(int? lineNbr);

  protected virtual void FillRelatedItemHistory(
    RelatedItemHistory historyLine,
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    TSubstitutableLine originalLine,
    TSubstitutableLine relatedLine,
    RelatedItem relatedItem)
  {
    historyLine.OriginalInventoryID = originalLine.InventoryID;
    historyLine.OriginalInventoryUOM = originalLine.UOM;
    Decimal? qty1 = filter.Qty;
    Decimal? qty2 = originalLine.Qty;
    Decimal? nullable1 = qty1.GetValueOrDefault() < qty2.GetValueOrDefault() & qty1.HasValue & qty2.HasValue ? filter.Qty : originalLine.Qty;
    Decimal? qtySelected1 = relatedItem.QtySelected;
    Decimal num = 0M;
    if (!(qtySelected1.GetValueOrDefault() == num & qtySelected1.HasValue))
    {
      Decimal? nullable2 = nullable1;
      Decimal? qty3 = relatedLine.Qty;
      Decimal? nullable3 = nullable2.HasValue & qty3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * qty3.GetValueOrDefault()) : new Decimal?();
      Decimal? qtySelected2 = relatedItem.QtySelected;
      nullable1 = nullable3.HasValue & qtySelected2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / qtySelected2.GetValueOrDefault()) : new Decimal?();
    }
    historyLine.OriginalInventoryQty = nullable1;
    historyLine.RelatedInventoryID = relatedLine.InventoryID;
    historyLine.RelatedInventoryUOM = relatedLine.UOM;
    historyLine.RelatedInventoryQty = relatedLine.Qty;
    historyLine.Relation = relatedItem.Relation;
    historyLine.Tag = relatedItem.Tag;
    historyLine.DocumentDate = this.GetDocumentDate((TSubstitutableDocument) this.DocumentCache.Current);
  }

  protected virtual void RemoveLineFromHistory(TSubstitutableLine line)
  {
    RelatedItemHistory historyLine = this.FindHistoryLine(line);
    if (historyLine == null)
      return;
    ((PXSelectBase) this.RelatedItemsHistory).Cache.Delete((object) historyLine);
    ISubstitutableLineExt substitutableLineExt = this.GetSubstitutableLineExt(line);
    if (substitutableLineExt == null)
      return;
    substitutableLineExt.HistoryLineID = new int?();
  }

  protected virtual bool IsSerialTrackingItem(int? inventoryID)
  {
    return INLotSerClass.PK.Find((PXGraph) this.Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID)?.LotSerClassID)?.LotSerTrack == "S";
  }

  protected virtual void SetRelatedItemsVisible(bool visible)
  {
    PXUIFieldAttribute.SetVisible(this.LinesCache, (object) null, "RelatedItems", visible);
    PXUIFieldAttribute.SetVisible(this.LinesCache, (object) null, "SubstitutionRequired", visible);
  }

  protected virtual bool SingleSelection(string relation)
  {
    return EnumerableExtensions.IsIn<string>(relation, "SUBST", "USELL");
  }

  protected virtual bool AllowToKeepOriginalPrice(string relation) => relation == "SUBST";

  protected virtual bool AllowToKeepOriginalPrice()
  {
    RelatedItem[] array = ((PXSelectBase) this.AllRelatedItems).Cache.Updated.OfType<RelatedItem>().Where<RelatedItem>((Func<RelatedItem, bool>) (x => x.Selected.GetValueOrDefault())).ToArray<RelatedItem>();
    return ((IEnumerable<RelatedItem>) array).Any<RelatedItem>() && ((IEnumerable<RelatedItem>) array).All<RelatedItem>((Func<RelatedItem, bool>) (x => this.AllowToKeepOriginalPrice(x.Relation)));
  }

  protected virtual void RaiseRelatedItemWarning(RelatedItem relatedItem)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (relatedItem.Selected.GetValueOrDefault())
    {
      Decimal? availableQty = relatedItem.AvailableQty;
      Decimal? qtySelected = relatedItem.QtySelected;
      if (availableQty.GetValueOrDefault() < qtySelected.GetValueOrDefault() & availableQty.HasValue & qtySelected.HasValue)
        propertyException = new PXSetPropertyException("The available quantity of the item is less than the selected quantity.", (PXErrorLevel) 2);
    }
    ((PXSelectBase) this.AllRelatedItems).Cache.RaiseExceptionHandling<RelatedItem.qtySelected>((object) relatedItem, (object) relatedItem.QtySelected, (Exception) propertyException);
  }

  protected virtual void ResetSubstitutionRequired(TSubstitutableLine line)
  {
    ISubstitutableLineExt substitutableLineExt = this.GetSubstitutableLineExt(line);
    if (substitutableLineExt == null || !substitutableLineExt.SuggestRelatedItems.GetValueOrDefault())
      return;
    line.SubstitutionRequired = new bool?(((InventoryRelation.RelationType) substitutableLineExt.RelatedItemsRequired.GetValueOrDefault()).HasFlag((Enum) InventoryRelation.RelationType.Substitute));
  }
}
