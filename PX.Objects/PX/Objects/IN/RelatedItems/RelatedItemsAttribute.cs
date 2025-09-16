// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.RelatedItemsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.RelatedItems;

[PXImage]
[PXUIField(DisplayName = "Related Items", IsReadOnly = true)]
public class RelatedItemsAttribute : 
  PXAggregateAttribute,
  IPXRowSelectedSubscriber,
  IPXRowInsertedSubscriber
{
  private int _uiFieldAttributeIndex;
  protected Type SuggestRelatedItemsField;
  protected Type RelationField;
  protected Type RequiredField;
  protected Type DocType;
  protected Type DocDateField;

  public Type DocumentDateField
  {
    get => this.DocDateField;
    set
    {
      this.DocDateField = value;
      this.DocType = BqlCommand.GetItemType(value);
    }
  }

  public bool CacheGlobal { get; set; } = true;

  protected PXUIFieldAttribute UIFieldAttribute
  {
    get => (PXUIFieldAttribute) this._Attributes[this._uiFieldAttributeIndex];
  }

  public RelatedItemsAttribute()
  {
    this._uiFieldAttributeIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).FindIndex((Predicate<PXEventSubscriberAttribute>) (x => x is PXUIFieldAttribute));
  }

  public RelatedItemsAttribute(
    Type suggestRelatedItemsField,
    Type relationField,
    Type requiredField)
    : this()
  {
    this.SuggestRelatedItemsField = suggestRelatedItemsField;
    this.RelationField = relationField;
    this.RequiredField = requiredField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    Type itemType = sender.GetItemType();
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    Type type1 = itemType;
    RelatedItemsAttribute relatedItemsAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) relatedItemsAttribute1, __vmethodptr(relatedItemsAttribute1, RowSelecting));
    rowSelecting.AddHandler(type1, pxRowSelecting);
    if (this.DocType != (Type) null)
    {
      PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
      Type docType = this.DocType;
      RelatedItemsAttribute relatedItemsAttribute2 = this;
      // ISSUE: virtual method pointer
      PXRowUpdated pxRowUpdated = new PXRowUpdated((object) relatedItemsAttribute2, __vmethodptr(relatedItemsAttribute2, DocumentUpdated));
      rowUpdated.AddHandler(docType, pxRowUpdated);
    }
    if (!typeof (ISubstitutableLine).IsAssignableFrom(itemType))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type type2 = itemType;
    RelatedItemsAttribute relatedItemsAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) relatedItemsAttribute3, __vmethodptr(relatedItemsAttribute3, RelatedFieldUpdated));
    fieldUpdated1.AddHandler(type2, "InventoryID", pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
    Type type3 = itemType;
    RelatedItemsAttribute relatedItemsAttribute4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) relatedItemsAttribute4, __vmethodptr(relatedItemsAttribute4, RelatedFieldUpdated));
    fieldUpdated2.AddHandler(type3, "BranchID", pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = sender.Graph.FieldUpdated;
    Type type4 = itemType;
    RelatedItemsAttribute relatedItemsAttribute5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) relatedItemsAttribute5, __vmethodptr(relatedItemsAttribute5, RelatedFieldUpdated));
    fieldUpdated3.AddHandler(type4, "SuggestRelatedItems", pxFieldUpdated3);
  }

  protected virtual void DocumentUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is ISubstitutableDocument row))
      return;
    bool flag1 = !object.Equals((object) row.SuggestRelatedItems, (object) ((ISubstitutableDocument) e.OldRow).SuggestRelatedItems);
    bool flag2 = flag1 || !object.Equals(sender.GetValue(e.OldRow, this.DocDateField.Name), sender.GetValue(e.Row, this.DocDateField.Name));
    if (!(flag1 | flag2))
      return;
    PXCache cach = sender.Graph.Caches[((PXEventSubscriberAttribute) this).BqlTable];
    foreach (ISubstitutableLine selectChild in PXParentAttribute.SelectChildren(cach, e.Row, sender.GetItemType()))
    {
      if (flag1 && this.SuggestRelatedItemsField != (Type) null)
      {
        object obj = PXFormulaAttribute.Evaluate(cach, this.SuggestRelatedItemsField.Name, (object) selectChild);
        cach.SetValue((object) selectChild, this.SuggestRelatedItemsField.Name, obj);
      }
      if (flag2)
        this.CalculateRelatedItems(cach, selectChild);
    }
  }

  protected virtual void RelatedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ISubstitutableLine row))
      return;
    this.CalculateRelatedItems(sender, row);
  }

  protected virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is ISubstitutableLine row))
      return;
    this.CalculateRelatedItems(sender, row);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ISubstitutableLine row))
      return;
    this.SetRelatedItemsWarning(sender, row);
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is ISubstitutableLine row))
      return;
    this.CalculateRelatedItems(sender, row);
  }

  protected virtual string RelatedItemsImage(
    InventoryRelation.RelationType relation,
    InventoryRelation.RelationType required)
  {
    if (relation == InventoryRelation.RelationType.None)
      return (string) null;
    if (required == InventoryRelation.RelationType.None)
      return "~/Icons/dollarGreen.svg";
    if (required.HasFlag((Enum) InventoryRelation.RelationType.Substitute))
      return "~/Icons/switchRed.svg";
    return required.HasFlag((Enum) InventoryRelation.RelationType.CrossSell) || required.HasFlag((Enum) InventoryRelation.RelationType.Other) ? "~/Icons/dollarRed.svg" : (string) null;
  }

  protected virtual PXExceptionInfo RelatedItemsMessage(
    InventoryRelation.RelationType relation,
    InventoryRelation.RelationType required)
  {
    if (relation == InventoryRelation.RelationType.None)
      return (PXExceptionInfo) null;
    if (required == InventoryRelation.RelationType.None)
    {
      string[] strArray = RelatedItemsAttribute.RelatedItemsMessageArguments(relation);
      string messageFormat = string.Empty;
      switch (strArray.Length)
      {
        case 1:
          messageFormat = "{0} items exist for this item. Click this button to select an item.";
          break;
        case 2:
          messageFormat = "{0} and {1} items exist for this item. Click this button to select an item.";
          break;
        case 3:
          messageFormat = "{0}, {1}, and {2} items exist for this item. Click this button to select an item.";
          break;
        case 4:
          messageFormat = "{0}, {1}, {2}, and {3} items exist for this item. Click this button to select an item.";
          break;
      }
      return new PXExceptionInfo((PXErrorLevel) 2, messageFormat, (object[]) strArray)
      {
        Css = "RelatedItemsAvailable"
      };
    }
    if (required.HasFlag((Enum) InventoryRelation.RelationType.Substitute))
      return new PXExceptionInfo((PXErrorLevel) 2, "This item has to be substituted. Click this button to select a substitute item.", Array.Empty<object>())
      {
        Css = "RelatedItemsRequired"
      };
    if (!required.HasFlag((Enum) InventoryRelation.RelationType.CrossSell) && !required.HasFlag((Enum) InventoryRelation.RelationType.Other))
      return (PXExceptionInfo) null;
    return new PXExceptionInfo((PXErrorLevel) 2, "Related items are required for this item. Click this button to select a related item.", Array.Empty<object>())
    {
      Css = "RelatedItemsRequired"
    };
  }

  public virtual PXExceptionInfo QtyMessage(
    object inventoryCD,
    object subItemCD,
    object siteCD,
    InventoryRelation.RelationType relation)
  {
    if (relation == InventoryRelation.RelationType.None)
      return (PXExceptionInfo) null;
    string[] strArray = RelatedItemsAttribute.RelatedItemsMessageArguments(relation);
    string messageFormat = string.Empty;
    switch (strArray.Length)
    {
      case 1:
        messageFormat = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2} items are available for this item. Click the button in the Related Items column to select an item.";
        break;
      case 2:
        messageFormat = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2} and {3} items are available for this item. Click the button in the Related Items column to select an item.";
        break;
      case 3:
        messageFormat = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2}, {3}, and {4} items are available for this item. Click the button in the Related Items column to select an item.";
        break;
      case 4:
        messageFormat = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2}, {3}, {4}, and {5} items are available for this item. Click the button in the Related Items column to select an item.";
        break;
    }
    return new PXExceptionInfo((PXErrorLevel) 2, messageFormat, EnumerableExtensions.Append<object>(new object[2]
    {
      inventoryCD,
      siteCD
    }, (object[]) strArray));
  }

  private static string[] RelatedItemsMessageArguments(InventoryRelation.RelationType relation)
  {
    List<string> source = new List<string>();
    if (relation.HasFlag((Enum) InventoryRelation.RelationType.Substitute))
      source.Add("Substitute");
    if (relation.HasFlag((Enum) InventoryRelation.RelationType.UpSell))
      source.Add("Up-Sell");
    if (relation.HasFlag((Enum) InventoryRelation.RelationType.CrossSell))
      source.Add("Cross-Sell");
    if (relation.HasFlag((Enum) InventoryRelation.RelationType.Other))
      source.Add("Other");
    List<string> list = source.Select<string, string>((Func<string, string>) (x => PXMessages.LocalizeNoPrefix(x).ToLower())).ToList<string>();
    list[0] = list[0].Substring(0, 1).ToUpper() + list[0].Substring(1);
    return list.ToArray();
  }

  public virtual bool FindRelatedItems(
    PXGraph graph,
    ISubstitutableLine substitutableLine,
    bool? showOnlyAvailableItems,
    out InventoryRelation.RelationType relation,
    out InventoryRelation.RelationType required)
  {
    relation = required = InventoryRelation.RelationType.None;
    return (substitutableLine != null ? (!substitutableLine.InventoryID.HasValue ? 1 : 0) : 1) == 0 && this.FindRelatedItems(graph, (ISubstitutableDocument) null, substitutableLine, showOnlyAvailableItems, out relation, out required);
  }

  protected virtual bool FindRelatedItems(
    PXGraph graph,
    ISubstitutableDocument substitutableDocument,
    ISubstitutableLine substitutableLine,
    bool? showOnlyAvailableItems,
    out InventoryRelation.RelationType relation,
    out InventoryRelation.RelationType required)
  {
    int? inventoryId = substitutableLine.InventoryID;
    int? branchId = substitutableLine.BranchID;
    DateTime? documentDate = this.GetDocumentDate(graph, substitutableDocument);
    showOnlyAvailableItems = new bool?(((int) showOnlyAvailableItems ?? (this.ShowOnlyAvailableRelatedItems(graph) ? 1 : 0)) != 0);
    ConcurrentDictionary<int?, RelatedItemsAttribute.RelatedItemsResult> concurrentDictionary = (ConcurrentDictionary<int?, RelatedItemsAttribute.RelatedItemsResult>) null;
    if (this.CacheGlobal)
    {
      concurrentDictionary = RelatedItemsAttribute.RelatedItemsCache.Get(branchId, showOnlyAvailableItems, documentDate);
      RelatedItemsAttribute.RelatedItemsResult relatedItemsResult;
      if (concurrentDictionary.TryGetValue(inventoryId, out relatedItemsResult))
      {
        relation = relatedItemsResult.Relation;
        required = relatedItemsResult.Required;
        return (relation | required) > InventoryRelation.RelationType.None;
      }
    }
    INAvailableRelatedItems[] relatedItems = this.LoadRelatedItems(graph, substitutableLine, documentDate, showOnlyAvailableItems);
    bool relatedItems1;
    if (relatedItems.Length == 0)
    {
      relation = InventoryRelation.RelationType.None;
      required = InventoryRelation.RelationType.None;
      relatedItems1 = false;
    }
    else
    {
      InventoryRelation.RelationType newRelation = InventoryRelation.RelationType.None;
      InventoryRelation.RelationType newRequired = InventoryRelation.RelationType.None;
      setType(find("SUBST"), InventoryRelation.RelationType.Substitute);
      setType(find("USELL"), InventoryRelation.RelationType.UpSell);
      setType(find("CSELL"), InventoryRelation.RelationType.CrossSell);
      setType(find("OTHER"), InventoryRelation.RelationType.Other);
      relation = newRelation;
      required = newRequired;
      relatedItems1 = true;

      void setType(INAvailableRelatedItems relatedInventory, InventoryRelation.RelationType type)
      {
        if (relatedInventory == null)
          return;
        newRelation |= type;
        if (!relatedInventory.Required.GetValueOrDefault())
          return;
        newRequired |= type;
      }
    }
    concurrentDictionary?.TryAdd(inventoryId, new RelatedItemsAttribute.RelatedItemsResult(inventoryId, relation, required));
    return relatedItems1;

    INAvailableRelatedItems find(string r)
    {
      return ((IEnumerable<INAvailableRelatedItems>) relatedItems).FirstOrDefault<INAvailableRelatedItems>((Func<INAvailableRelatedItems, bool>) (x => x.Relation == r));
    }
  }

  protected virtual INAvailableRelatedItems[] LoadRelatedItems(
    PXGraph graph,
    ISubstitutableLine substitutableLine,
    DateTime? documentDate,
    bool? showOnlyAvailableItems)
  {
    PXSelectBase<INAvailableRelatedItems> relatedItemsQuery = this.GetRelatedItemsQuery(graph);
    using (new PXFieldScope(((PXSelectBase) relatedItemsQuery).View, new Type[2]
    {
      typeof (INAvailableRelatedItems.relation),
      typeof (INAvailableRelatedItems.required)
    }))
      return relatedItemsQuery.SelectMain(this.GetRelatedItemsQueryArguments(graph, substitutableLine, documentDate, showOnlyAvailableItems));
  }

  protected virtual PXSelectBase<INAvailableRelatedItems> GetRelatedItemsQuery(PXGraph graph)
  {
    return (PXSelectBase<INAvailableRelatedItems>) new PXViewOf<INAvailableRelatedItems>.BasedOn<SelectFromBase<INAvailableRelatedItems, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INAvailableRelatedItems.originalInventoryID, Equal<P.AsInt>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofDateTimeUTC>, IsNull>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INAvailableRelatedItems.effectiveDate, IsNull>>>>.Or<BqlOperand<INAvailableRelatedItems.effectiveDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INAvailableRelatedItems.expirationDate, IsNull>>>>.Or<BqlOperand<INAvailableRelatedItems.expirationDate, IBqlDateTime>.IsGreaterEqual<P.AsDateTime>>>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofBool>, NotEqual<True>>>>, Or<BqlOperand<INAvailableRelatedItems.stkItem, IBqlBool>.IsNotEqual<True>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INAvailableRelatedItems.relation, Equal<InventoryRelation.substitute>>>>>.And<BqlOperand<INAvailableRelatedItems.required, IBqlBool>.IsEqual<True>>>>>.Or<BqlOperand<INAvailableRelatedItems.qtyAvail, IBqlDecimal>.IsGreater<decimal0>>>>>>.And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INAvailableRelatedItems.siteID, IsNull>>>, Or<FeatureInstalled<FeaturesSet.interBranch>>>, Or<BqlOperand<INAvailableRelatedItems.branchID, IBqlInt>.IsNull>>, Or<BqlOperand<INAvailableRelatedItems.branchID, IBqlInt>.IsIn<P.AsInt>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofString>, IsNotNull>>>>.And<BqlOperand<Required<Parameter.ofString>, IBqlString>.IsEqual<SOBehavior.qT>>>>>>.Aggregate<To<GroupBy<INAvailableRelatedItems.relation>>>>.ReadOnly(graph);
  }

  protected virtual object[] GetRelatedItemsQueryArguments(
    PXGraph graph,
    ISubstitutableLine substitutableLine,
    DateTime? documentDate,
    bool? showOnlyAvailableItems)
  {
    int[] numArray = PXAccess.GetChildBranchIDs(PXAccess.GetParentOrganizationID(substitutableLine.BranchID), false);
    if (numArray.Length == 0)
      numArray = new int[1];
    return new object[8]
    {
      (object) substitutableLine.InventoryID,
      (object) documentDate,
      (object) documentDate,
      (object) documentDate,
      (object) showOnlyAvailableItems,
      (object) numArray,
      null,
      null
    };
  }

  protected virtual bool ShowOnlyAvailableRelatedItems(PXGraph graph)
  {
    SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSetup<SOSetup>.Select(graph, Array.Empty<object>()));
    return soSetup != null && soSetup.ShowOnlyAvailableRelatedItems.GetValueOrDefault();
  }

  protected virtual DateTime? GetDocumentDate(
    PXGraph graph,
    ISubstitutableDocument substitutableDocument)
  {
    if (this.DocType == (Type) null)
      return new DateTime?();
    PXCache cach = graph.Caches[this.DocType];
    return (DateTime?) cach.GetValue((object) substitutableDocument ?? cach.Current, this.DocDateField.Name);
  }

  protected virtual void CalculateRelatedItems(PXCache cache, ISubstitutableLine substitutableLine)
  {
    string str = (string) null;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    if (substitutableLine.InventoryID.HasValue && (!cache.Graph.IsImport || cache.Graph.IsCopyPasteContext))
    {
      bool? nullable3;
      if (!(this.SuggestRelatedItemsField == (Type) null))
      {
        nullable3 = (bool?) cache.GetValue((object) substitutableLine, this.SuggestRelatedItemsField.Name);
        if (!nullable3.GetValueOrDefault())
          goto label_5;
      }
      PXGraph graph = cache.Graph;
      ISubstitutableLine substitutableLine1 = substitutableLine;
      nullable3 = new bool?();
      bool? showOnlyAvailableItems = nullable3;
      InventoryRelation.RelationType relation;
      ref InventoryRelation.RelationType local1 = ref relation;
      InventoryRelation.RelationType required;
      ref InventoryRelation.RelationType local2 = ref required;
      if (this.FindRelatedItems(graph, (ISubstitutableDocument) null, substitutableLine1, showOnlyAvailableItems, out local1, out local2))
      {
        nullable1 = new int?((int) relation);
        nullable2 = new int?((int) required);
        str = this.RelatedItemsImage(relation, required);
      }
    }
label_5:
    cache.SetValue((object) substitutableLine, ((PXEventSubscriberAttribute) this)._FieldName, (object) str);
    setRelation((object) nullable1);
    setRequired((object) nullable2);

    void setRelation(object value)
    {
      if (!(this.RelationField != (Type) null))
        return;
      cache.SetValue((object) substitutableLine, this.RelationField.Name, value);
    }

    void setRequired(object value)
    {
      if (!(this.RequiredField != (Type) null))
        return;
      cache.SetValue((object) substitutableLine, this.RequiredField.Name, value);
    }
  }

  public virtual bool AnyRequired(
    PXGraph graph,
    ISubstitutableDocument substitutableDocument,
    ISubstitutableLine substitutableLine)
  {
    InventoryRelation.RelationType relation;
    InventoryRelation.RelationType required;
    return (substitutableLine != null ? (!substitutableLine.InventoryID.HasValue ? 1 : 0) : 1) == 0 && this.FindRelatedItems(graph, substitutableDocument, substitutableLine, new bool?(), out relation, out required) && relation > InventoryRelation.RelationType.None && required > InventoryRelation.RelationType.None;
  }

  protected virtual void SetRelatedItemsWarning(PXCache cache, ISubstitutableLine substitutableLine)
  {
    if (this.RelationField == (Type) null || this.RequiredField == (Type) null)
      return;
    int? nullable = (int?) cache.GetValue((object) substitutableLine, this.RelationField.Name);
    PXExceptionInfo pxExceptionInfo;
    if (!nullable.HasValue)
    {
      pxExceptionInfo = (PXExceptionInfo) null;
    }
    else
    {
      int valueOrDefault = ((int?) cache.GetValue((object) substitutableLine, this.RequiredField.Name)).GetValueOrDefault();
      pxExceptionInfo = this.RelatedItemsMessage((InventoryRelation.RelationType) nullable.Value, (InventoryRelation.RelationType) valueOrDefault);
    }
    PXSetPropertyException propertyException = pxExceptionInfo?.ToSetPropertyException();
    cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, (object) substitutableLine, cache.GetValue((object) substitutableLine, ((PXEventSubscriberAttribute) this)._FieldName), (Exception) propertyException);
  }

  [PXInternalUseOnly]
  public class RelatedItemsCache : 
    ConcurrentDictionary<Composite, ConcurrentDictionary<int?, RelatedItemsAttribute.RelatedItemsResult>>
  {
    public static ConcurrentDictionary<int?, RelatedItemsAttribute.RelatedItemsResult> Get(
      int? branchID,
      bool? onlyAvailableItems,
      DateTime? date)
    {
      Composite<int, bool, DateTime> key = Composite.Create<int, bool, DateTime>(branchID.GetValueOrDefault(), onlyAvailableItems.GetValueOrDefault(), date ?? DateTime.Today);
      return RelatedItemsAttribute.RelatedItemsCache.GetCache().GetOrAdd((Composite) key, (Func<Composite, ConcurrentDictionary<int?, RelatedItemsAttribute.RelatedItemsResult>>) (k => new ConcurrentDictionary<int?, RelatedItemsAttribute.RelatedItemsResult>()));
    }

    private static RelatedItemsAttribute.RelatedItemsCache GetCache()
    {
      string key1 = nameof (RelatedItemsCache);
      RelatedItemsAttribute.RelatedItemsCache cache = PXContext.GetSlot<RelatedItemsAttribute.RelatedItemsCache>(key1);
      if (cache == null)
      {
        string key2 = key1;
        string str = key1;
        Type[] typeArray = new Type[5]
        {
          typeof (INRelatedInventory),
          typeof (PX.Objects.IN.InventoryItem),
          typeof (INSubItem),
          typeof (INSiteStatusByCostCenter),
          typeof (PX.Objects.IN.INSite)
        };
        RelatedItemsAttribute.RelatedItemsCache localizableSlot;
        cache = localizableSlot = PXDatabase.GetLocalizableSlot<RelatedItemsAttribute.RelatedItemsCache>(str, typeArray);
        PXContext.SetSlot<RelatedItemsAttribute.RelatedItemsCache>(key2, localizableSlot);
      }
      return cache;
    }
  }

  [PXInternalUseOnly]
  public class RelatedItemsResult
  {
    public RelatedItemsResult(
      int? inventoryID,
      InventoryRelation.RelationType relation,
      InventoryRelation.RelationType required)
    {
      this.InventoryID = inventoryID;
      this.Relation = relation;
      this.Required = required;
    }

    public int? InventoryID { get; }

    public InventoryRelation.RelationType Relation { get; }

    public InventoryRelation.RelationType Required { get; }
  }
}
