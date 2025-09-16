// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.PeriodKeyProviderBase`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public abstract class PeriodKeyProviderBase<TSourcesSpecificationCollection, TSourceSpecificationItem, TKeyWithSourceValuesCollection, TKeyWithKeyWithSourceValues, TKey> : 
  PeriodKeyProviderBase,
  IPeriodKeyProvider<TSourcesSpecificationCollection, TSourceSpecificationItem, TKeyWithSourceValuesCollection, TKeyWithKeyWithSourceValues, TKey>,
  IPeriodKeyProvider<TKey, TSourcesSpecificationCollection>,
  IPeriodKeyProviderBase
  where TSourcesSpecificationCollection : PeriodKeyProviderBase.SourcesSpecificationCollection<TSourceSpecificationItem>, new()
  where TSourceSpecificationItem : PeriodKeyProviderBase.SourceSpecificationItem
  where TKeyWithSourceValuesCollection : PeriodKeyProviderBase.KeyWithSourceValuesCollection<TKeyWithKeyWithSourceValues, TSourceSpecificationItem, TKey>, new()
  where TKeyWithKeyWithSourceValues : PeriodKeyProviderBase.KeyWithSourceValues<TSourceSpecificationItem, TKey>, new()
  where TKey : OrganizationDependedPeriodKey, new()
{
  public virtual TSourcesSpecificationCollection CachedSourcesSpecification { get; set; }

  public virtual Type UseMasterCalendarSourceType { get; set; }

  public virtual bool UseMasterOrganizationIDByDefault { get; set; }

  public abstract int? MasterValue { get; }

  public PeriodKeyProviderBase(
    TSourcesSpecificationCollection sourcesSpecification = null,
    Type[] sourceSpecificationTypes = null,
    Type useMasterCalendarSourceType = null,
    bool useMasterOrganizationIDByDefault = false)
  {
    if ((object) sourcesSpecification != null)
    {
      List<TSourceSpecificationItem> specificationItemList = new List<TSourceSpecificationItem>((IEnumerable<TSourceSpecificationItem>) sourcesSpecification.SpecificationItems);
      sourcesSpecification.SpecificationItems.Clear();
      foreach (TSourceSpecificationItem specificationItem in specificationItemList)
      {
        if (specificationItem.IsAnySourceSpecified)
        {
          sourcesSpecification.SpecificationItems.Add(specificationItem);
          specificationItem.Initialize();
        }
      }
      this.CachedSourcesSpecification = sourcesSpecification;
    }
    else
      this.CachedSourcesSpecification = new TSourcesSpecificationCollection();
    if (sourceSpecificationTypes != null)
    {
      foreach (Type specificationType in sourceSpecificationTypes)
      {
        TSourceSpecificationItem instance = (TSourceSpecificationItem) Activator.CreateInstance(specificationType);
        instance.Initialize();
        this.CachedSourcesSpecification.SpecificationItems.Add(instance);
      }
    }
    if (this.CachedSourcesSpecification.SpecificationItems.Count == 1)
      this.CachedSourcesSpecification.SpecificationItems.First<TSourceSpecificationItem>().IsMain = true;
    this.CachedSourcesSpecification.MainSpecificationItem = this.CachedSourcesSpecification.SpecificationItems.SingleOrDefault<TSourceSpecificationItem>((Func<TSourceSpecificationItem, bool>) (s => s.IsMain));
    this.UseMasterCalendarSourceType = useMasterCalendarSourceType;
    this.UseMasterOrganizationIDByDefault = useMasterOrganizationIDByDefault;
  }

  public virtual PeriodKeyProviderBase.SourceSpecificationItem GetMainSourceSpecificationItem(
    PXCache cache,
    object row)
  {
    return (PeriodKeyProviderBase.SourceSpecificationItem) this.GetSourcesSpecification(cache, row).MainSpecificationItem;
  }

  public virtual TSourcesSpecificationCollection GetSourcesSpecification(PXCache cache, object row)
  {
    return this.CachedSourcesSpecification;
  }

  public virtual bool IsKeySourceValuesEquals(PXCache cache, object oldRow, object newRow)
  {
    TKeyWithSourceValuesCollection valuesCollection1 = this.BuildKeyCollection(cache.Graph, cache, oldRow, new Func<PXGraph, PXCache, object, TSourceSpecificationItem, TKeyWithKeyWithSourceValues>(this.CollectSourceValues), false);
    TKeyWithSourceValuesCollection valuesCollection2 = this.BuildKeyCollection(cache.Graph, cache, newRow, new Func<PXGraph, PXCache, object, TSourceSpecificationItem, TKeyWithKeyWithSourceValues>(this.CollectSourceValues), false);
    if (valuesCollection1.Items.Count != valuesCollection2.Items.Count)
      return false;
    TSourcesSpecificationCollection sourcesSpecification = this.GetSourcesSpecification(cache, oldRow);
    IEnumerable<object> first = sourcesSpecification.DependsOnFields.Select<Type, object>((Func<Type, object>) (fieldType => cache.GetValue(oldRow, fieldType.Name)));
    IEnumerable<object> second = sourcesSpecification.DependsOnFields.Select<Type, object>((Func<Type, object>) (fieldType => cache.GetValue(newRow, fieldType.Name)));
    for (int index = 0; index < valuesCollection1.Items.Count; ++index)
    {
      if (!valuesCollection1.Items[index].SourcesEqual((object) valuesCollection2.Items[index]))
        return false;
    }
    return first.SequenceEqual<object>(second);
  }

  public bool IsKeyDefined(PXGraph graph, PXCache attributeCache, object extRow)
  {
    return ((IEnumerable<object>) this.GetKeyAsArrayOfObjects(graph, attributeCache, extRow)).All<object>((Func<object, bool>) (value => value != null));
  }

  public object[] GetKeyAsArrayOfObjects(PXGraph graph, PXCache attributeCache, object extRow)
  {
    return this.GetKey(graph, attributeCache, extRow).ToListOfObjects(true).ToArray();
  }

  public object GetKeyAsObjects(PXGraph graph, PXCache attributeCache, object extRow)
  {
    return (object) this.GetKey(graph, attributeCache, extRow);
  }

  public virtual IEnumerable<PeriodKeyProviderBase.SourceSpecificationItem> GetSourceSpecificationItems(
    PXCache cache,
    object row)
  {
    return (IEnumerable<PeriodKeyProviderBase.SourceSpecificationItem>) this.GetSourcesSpecification(cache, row).SpecificationItems;
  }

  public virtual TKey GetKey(PXGraph graph, PXCache attributeCache, object extRow)
  {
    if (this.UseMasterCalendarSourceType != (Type) null && ((bool?) BqlHelper.GetCurrentValue(graph, this.UseMasterCalendarSourceType, extRow)).GetValueOrDefault())
    {
      TKey key = new TKey();
      key.OrganizationID = this.MasterValue;
      return key;
    }
    if (!this.GetSourcesSpecification(attributeCache, extRow).SpecificationItems.Any<TSourceSpecificationItem>() || this.GetSourcesSpecification(attributeCache, extRow).SpecificationItems.All<TSourceSpecificationItem>((Func<TSourceSpecificationItem, bool>) (spec => !spec.IsAnySourceSpecified)))
      return this.GetDefaultPeriodKey();
    TKeyWithKeyWithSourceValues withSourceValues = this.GetRawMainKeyWithSourceValues(graph, attributeCache, extRow);
    return (object) withSourceValues == null ? this.GetKeys(graph, attributeCache, extRow, false).ConsolidatedKey : withSourceValues.Key;
  }

  public virtual TKey GetDefaultPeriodKey()
  {
    TKey defaultPeriodKey = new TKey();
    defaultPeriodKey.OrganizationID = this.MasterValue;
    return defaultPeriodKey;
  }

  public virtual TKeyWithSourceValuesCollection BuildKeyCollection(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    Func<PXGraph, PXCache, object, TSourceSpecificationItem, TKeyWithKeyWithSourceValues> buildItemDelegate,
    bool skipMain = false)
  {
    List<TKeyWithKeyWithSourceValues> list = this.GetSourcesSpecification(attributeCache, extRow).SpecificationItems.Where<TSourceSpecificationItem>((Func<TSourceSpecificationItem, bool>) (item => !item.IsMain || !skipMain)).Select<TSourceSpecificationItem, TKeyWithKeyWithSourceValues>((Func<TSourceSpecificationItem, TKeyWithKeyWithSourceValues>) (specification => buildItemDelegate(graph, attributeCache, extRow, specification))).ToList<TKeyWithKeyWithSourceValues>();
    TKeyWithSourceValuesCollection valuesCollection = new TKeyWithSourceValuesCollection();
    valuesCollection.Items = list;
    valuesCollection.ConsolidatedOrganizationIDs = list.SelectMany<TKeyWithKeyWithSourceValues, int?>((Func<TKeyWithKeyWithSourceValues, IEnumerable<int?>>) (item => (IEnumerable<int?>) item.KeyOrganizationIDs)).ToList<int?>();
    // ISSUE: variable of a boxed type
    __Boxed<TKeyWithSourceValuesCollection> local1 = (object) valuesCollection;
    // ISSUE: variable of a boxed type
    __Boxed<TKeyWithKeyWithSourceValues> local2 = (object) list.FirstOrDefault<TKeyWithKeyWithSourceValues>();
    TKey key = local2 != null ? local2.Key : default (TKey);
    local1.ConsolidatedKey = key;
    return valuesCollection;
  }

  public virtual TKeyWithSourceValuesCollection GetKeys(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    bool skipMain = false)
  {
    return this.BuildKeyCollection(graph, attributeCache, extRow, new Func<PXGraph, PXCache, object, TSourceSpecificationItem, TKeyWithKeyWithSourceValues>(this.CollectSourceValuesAndPreEvaluateKey), skipMain);
  }

  public virtual TKeyWithSourceValuesCollection GetKeysWithBasisOrganizationIDs(
    PXGraph graph,
    PXCache attributeCache,
    object extRow)
  {
    TKeyWithSourceValuesCollection keys = this.GetKeys(graph, attributeCache, extRow, false);
    int?[] availableOrganizationIds = PXAccess.GetAvailableOrganizationIDs();
    if (keys.ConsolidatedOrganizationIDs == null || keys.ConsolidatedOrganizationIDs.All<int?>((Func<int?, bool>) (id => !id.HasValue)) || !keys.ConsolidatedOrganizationIDs.Any<int?>())
    {
      int? organizationId = this.GetKey(graph, attributeCache, extRow).OrganizationID;
      int? masterValue = this.MasterValue;
      if (organizationId.GetValueOrDefault() == masterValue.GetValueOrDefault() & organizationId.HasValue == masterValue.HasValue)
      {
        keys.ConsolidatedOrganizationIDs = ((IEnumerable<int?>) availableOrganizationIds).ToList<int?>();
        goto label_4;
      }
    }
    keys.ConsolidatedOrganizationIDs = keys.ConsolidatedOrganizationIDs.Intersect<int?>((IEnumerable<int?>) availableOrganizationIds).ToList<int?>();
label_4:
    return keys;
  }

  public virtual TKeyWithKeyWithSourceValues GetRawMainKeyWithSourceValues(
    PXGraph graph,
    PXCache attributeCache,
    object extRow)
  {
    return this.CollectSourceValuesAndPreEvaluateKey(graph, attributeCache, extRow, this.GetSourcesSpecification(attributeCache, extRow).MainSpecificationItem);
  }

  public virtual TKeyWithKeyWithSourceValues GetMainSourceOrganizationIDs(
    PXGraph graph,
    PXCache attributeCache,
    object extRow)
  {
    return this.GetOrganizationIDsValueFromField(graph, attributeCache, extRow, this.GetSourcesSpecification(attributeCache, extRow).MainSpecificationItem);
  }

  protected virtual TKeyWithKeyWithSourceValues CollectSourceValuesAndPreEvaluateKey(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem specificationItem)
  {
    return (object) specificationItem == null ? default (TKeyWithKeyWithSourceValues) : this.EvaluateRawKey(graph, this.CollectSourceValues(graph, attributeCache, extRow, specificationItem));
  }

  protected virtual TKeyWithKeyWithSourceValues CollectSourceValues(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem specificationItem)
  {
    if ((object) specificationItem == null)
      return default (TKeyWithKeyWithSourceValues);
    TKeyWithKeyWithSourceValues withSourceValues = new TKeyWithKeyWithSourceValues();
    withSourceValues.SourceOrganizationIDs = this.GetOrganizationIDsValueFromField(graph, attributeCache, extRow, specificationItem).SourceOrganizationIDs;
    withSourceValues.SourceBranchIDs = this.GetBranchIDsValueFromField(graph, attributeCache, extRow, specificationItem).SourceBranchIDs;
    withSourceValues.SpecificationItem = specificationItem;
    return withSourceValues;
  }

  protected abstract TKeyWithKeyWithSourceValues EvaluateRawKey(
    PXGraph graph,
    TKeyWithKeyWithSourceValues keyWithKeyWithSourceValues);

  public virtual TKeyWithKeyWithSourceValues GetIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem sourceSpecification,
    Type fieldType,
    Action<TKeyWithKeyWithSourceValues, List<int?>> setter)
  {
    int? nullable = new int?();
    if (fieldType != (Type) null)
    {
      PXCache sourceCache = this.GetSourceCache(graph, attributeCache, fieldType);
      object sourceRow = this.GetSourceRow(sourceCache, extRow);
      nullable = (int?) BqlHelper.GetOperandValue(sourceCache, sourceRow, fieldType);
    }
    TKeyWithKeyWithSourceValues withSourceValues = new TKeyWithKeyWithSourceValues();
    withSourceValues.SpecificationItem = sourceSpecification;
    TKeyWithKeyWithSourceValues idsValueFromField = withSourceValues;
    setter(idsValueFromField, nullable.SingleToList<int?>());
    return idsValueFromField;
  }

  public virtual TKeyWithKeyWithSourceValues GetOrganizationIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem sourceSpecification)
  {
    return this.GetIDsValueFromField(graph, attributeCache, extRow, sourceSpecification, sourceSpecification.OrganizationSourceType, (Action<TKeyWithKeyWithSourceValues, List<int?>>) ((item, value) => item.SourceOrganizationIDs = value));
  }

  public virtual TKeyWithKeyWithSourceValues GetBranchIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem calendarOrganizationIdSourceSpec)
  {
    bool? nullable = new bool?();
    object obj = (object) null;
    if (calendarOrganizationIdSourceSpec.BranchSourceType != (Type) null || calendarOrganizationIdSourceSpec.BranchSourceFormula != null)
    {
      PXCache sourceCache = this.GetSourceCache(graph, attributeCache, calendarOrganizationIdSourceSpec.BranchSourceType);
      object sourceRow = this.GetSourceRow(sourceCache, extRow);
      if (calendarOrganizationIdSourceSpec.BranchSourceFormula != null)
        BqlFormula.Verify(sourceCache, sourceRow, calendarOrganizationIdSourceSpec.BranchSourceFormula, ref nullable, ref obj);
      else
        obj = BqlHelper.GetOperandValue(sourceCache, sourceRow, calendarOrganizationIdSourceSpec.BranchSourceType);
    }
    TKeyWithKeyWithSourceValues idsValueFromField = new TKeyWithKeyWithSourceValues();
    idsValueFromField.SpecificationItem = calendarOrganizationIdSourceSpec;
    idsValueFromField.SourceBranchIDs = ((int?) obj).SingleToList<int?>();
    return idsValueFromField;
  }

  public virtual List<TKeyWithKeyWithSourceValues> GetBranchIDsValuesFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    bool skipMain = false)
  {
    return this.GetSourcesSpecification(attributeCache, extRow).SpecificationItems.Where<TSourceSpecificationItem>((Func<TSourceSpecificationItem, bool>) (specification => !specification.IsMain || !skipMain)).Select<TSourceSpecificationItem, TKeyWithKeyWithSourceValues>((Func<TSourceSpecificationItem, TKeyWithKeyWithSourceValues>) (specification => this.GetBranchIDsValueFromField(graph, attributeCache, extRow, specification))).ToList<TKeyWithKeyWithSourceValues>();
  }

  protected virtual PXCache GetSourceCache(PXGraph graph, PXCache attributeCache, Type sourceType)
  {
    return typeof (IBqlField).IsAssignableFrom(sourceType) && !BqlCommand.GetItemType(sourceType).IsAssignableFrom(attributeCache.GetItemType()) ? graph.Caches[BqlCommand.GetItemType(sourceType)] : attributeCache;
  }

  protected virtual object GetSourceRow(PXCache sourceCache, object extRow)
  {
    return extRow == null || !sourceCache.GetItemType().IsAssignableFrom(extRow.GetType()) ? sourceCache.Current : extRow;
  }

  protected bool IsIDsUndefined(List<int?> values)
  {
    return values == null || values.All<int?>((Func<int?, bool>) (id => !id.HasValue)) || !values.Any<int?>();
  }
}
