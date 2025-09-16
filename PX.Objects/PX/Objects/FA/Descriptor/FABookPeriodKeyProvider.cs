// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Descriptor.FABookPeriodKeyProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.Descriptor;

public class FABookPeriodKeyProvider(
  PeriodKeyProviderBase.SourcesSpecificationCollection<FABookPeriodKeyProvider.FASourceSpecificationItem> sourcesSpecification = null,
  Type[] sourceSpecificationTypes = null,
  bool useMasterOrganizationIDByDefault = false) : 
  PeriodKeyProviderBase<PeriodKeyProviderBase.SourcesSpecificationCollection<FABookPeriodKeyProvider.FASourceSpecificationItem>, FABookPeriodKeyProvider.FASourceSpecificationItem, PeriodKeyProviderBase.KeyWithSourceValuesCollection<FABookPeriodKeyProvider.FAKeyWithSourceValues, FABookPeriodKeyProvider.FASourceSpecificationItem, FABookPeriod.Key>, FABookPeriodKeyProvider.FAKeyWithSourceValues, FABookPeriod.Key>(sourcesSpecification, sourceSpecificationTypes, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault)
{
  public override int? MasterValue => new int?(0);

  public virtual FABookPeriod.Key GetKeyFromReportParameters(
    PXGraph graph,
    object[] parameters,
    ReportParametersFlag reportParametersMask)
  {
    FABookPeriodKeyProvider.ParameterEvaluator<ReportParametersFlag> parameterEvaluator = new FABookPeriodKeyProvider.ParameterEvaluator<ReportParametersFlag>(reportParametersMask, parameters);
    HashSet<int?> nullableSet = new HashSet<int?>();
    int? nullable = (int?) parameterEvaluator[ReportParametersFlag.Branch];
    if (nullable.HasValue)
      nullableSet.Add(nullable);
    nullableSet.AddRange<int?>(PXAccess.GetBranchIDsByBAccount((int?) parameterEvaluator[ReportParametersFlag.BAccount]).Cast<int?>());
    PXGraph graph1 = graph;
    FABookPeriodKeyProvider.FAKeyWithSourceValues keyWithKeyWithSourceValues = new FABookPeriodKeyProvider.FAKeyWithSourceValues();
    keyWithKeyWithSourceValues.SpecificationItem = this.CachedSourcesSpecification.MainSpecificationItem;
    keyWithKeyWithSourceValues.SourceOrganizationIDs = ((int?) parameterEvaluator[ReportParametersFlag.Organization]).SingleToList<int?>();
    keyWithKeyWithSourceValues.SourceBranchIDs = nullableSet.ToList<int?>();
    keyWithKeyWithSourceValues.SourceAssetIDs = ((int?) parameterEvaluator[ReportParametersFlag.FixedAsset]).SingleToList<int?>();
    keyWithKeyWithSourceValues.SourceBookIDs = ((int?) parameterEvaluator[ReportParametersFlag.Book]).SingleToList<int?>();
    FABookPeriodKeyProvider.FAKeyWithSourceValues rawKey = this.EvaluateRawKey(graph1, keyWithKeyWithSourceValues);
    if (!rawKey.Key.OrganizationID.HasValue && this.UseMasterOrganizationIDByDefault)
      rawKey.Key.OrganizationID = this.MasterValue;
    return rawKey.Key;
  }

  protected override FABookPeriodKeyProvider.FAKeyWithSourceValues CollectSourceValues(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    FABookPeriodKeyProvider.FASourceSpecificationItem specificationItem)
  {
    if (specificationItem == null)
      return (FABookPeriodKeyProvider.FAKeyWithSourceValues) null;
    FABookPeriodKeyProvider.FAKeyWithSourceValues withSourceValues = base.CollectSourceValues(graph, attributeCache, extRow, specificationItem);
    withSourceValues.SourceAssetIDs = this.GetAssetIDsValueFromField(graph, attributeCache, extRow, specificationItem).SourceAssetIDs;
    withSourceValues.SourceBookIDs = this.GetBookIDsValueFromField(graph, attributeCache, extRow, specificationItem).SourceBookIDs;
    return withSourceValues;
  }

  protected override FABookPeriodKeyProvider.FAKeyWithSourceValues EvaluateRawKey(
    PXGraph graph,
    FABookPeriodKeyProvider.FAKeyWithSourceValues keyWithSourceValues)
  {
    if (keyWithSourceValues == null)
      return (FABookPeriodKeyProvider.FAKeyWithSourceValues) null;
    FABook book = BookMaint.FindByID(graph, keyWithSourceValues.SourceBookIDs.First<int?>());
    if (book == null)
    {
      if (keyWithSourceValues.SpecificationItem.IsBookRequired)
        return keyWithSourceValues;
      book = BookMaint.FindByBookMarker(graph, 2);
    }
    if (book == null)
      return keyWithSourceValues;
    keyWithSourceValues.Key.SetBookID(book);
    if (book.UpdateGL.GetValueOrDefault())
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.branch>())
      {
        keyWithSourceValues.KeyOrganizationIDs = PXAccess.GetParentOrganizationID(PXAccess.GetBranchID()).SingleToList<int?>();
      }
      else
      {
        keyWithSourceValues.KeyOrganizationIDs = keyWithSourceValues.SourceOrganizationIDs;
        if (this.IsIDsUndefined(keyWithSourceValues.KeyOrganizationIDs))
        {
          if (!this.IsIDsUndefined(keyWithSourceValues.SourceBranchIDs))
            keyWithSourceValues.KeyOrganizationIDs = keyWithSourceValues.SourceBranchIDs.Select<int?, int?>((Func<int?, int?>) (branchID => PXAccess.GetParentOrganizationID(branchID))).ToList<int?>();
          else if (!this.IsIDsUndefined(keyWithSourceValues.SourceAssetIDs))
            keyWithSourceValues.KeyOrganizationIDs = keyWithSourceValues.SourceAssetIDs.Select<int?, int?>((Func<int?, int?>) (assetID => PXAccess.GetParentOrganizationID((int?) AssetMaint.FindByID(graph, assetID)?.BranchID))).ToList<int?>();
          else if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
            keyWithSourceValues.KeyOrganizationIDs = new int?(0).SingleToList<int?>();
        }
      }
    }
    else
      keyWithSourceValues.KeyOrganizationIDs = new int?(0).SingleToList<int?>();
    keyWithSourceValues.Key.OrganizationID = keyWithSourceValues.KeyOrganizationIDs.First<int?>();
    return keyWithSourceValues;
  }

  public virtual FABookPeriodKeyProvider.FAKeyWithSourceValues GetAssetIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    FABookPeriodKeyProvider.FASourceSpecificationItem sourceSpecification)
  {
    return this.GetIDsValueFromField(graph, attributeCache, extRow, sourceSpecification, sourceSpecification.AssetSourceType, (Action<FABookPeriodKeyProvider.FAKeyWithSourceValues, List<int?>>) ((item, value) => item.SourceAssetIDs = value));
  }

  public virtual FABookPeriodKeyProvider.FAKeyWithSourceValues GetBookIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    FABookPeriodKeyProvider.FASourceSpecificationItem sourceSpecification)
  {
    if (typeof (IBqlField).IsAssignableFrom(sourceSpecification.BookSourceType))
      return this.GetIDsValueFromField(graph, attributeCache, extRow, sourceSpecification, sourceSpecification.BookSourceType, (Action<FABookPeriodKeyProvider.FAKeyWithSourceValues, List<int?>>) ((item, value) => item.SourceBookIDs = value));
    List<int?> nullableList = new List<int?>()
    {
      new int?()
    };
    if (typeof (IBqlSearch).IsAssignableFrom(sourceSpecification.BookSourceType))
      nullableList = ((int?) ((IBqlSearch) sourceSpecification.BookSourceType).SelectFirst(graph, extRow)).SingleToList<int?>();
    FABookPeriodKeyProvider.FAKeyWithSourceValues idsValueFromField = new FABookPeriodKeyProvider.FAKeyWithSourceValues();
    idsValueFromField.SpecificationItem = sourceSpecification;
    idsValueFromField.SourceBookIDs = nullableList;
    return idsValueFromField;
  }

  public class FASourceSpecificationItem : PeriodKeyProviderBase.SourceSpecificationItem
  {
    public virtual Type BookSourceType { get; set; }

    public virtual Type AssetSourceType { get; set; }

    public bool IsBookRequired { get; set; }

    public override bool IsAnySourceSpecified
    {
      get
      {
        return base.IsAnySourceSpecified || this.AssetSourceType != (Type) null || this.BookSourceType != (Type) null;
      }
    }

    protected override List<Type> BuildSourceFields(PXCache cache)
    {
      List<Type> typeList = base.BuildSourceFields(cache);
      if (this.BookSourceType != (Type) null)
        typeList.Add(this.BookSourceType);
      if (this.AssetSourceType != (Type) null)
        typeList.Add(this.AssetSourceType);
      return typeList;
    }
  }

  public class FAKeyWithSourceValues : 
    PeriodKeyProviderBase.KeyWithSourceValues<FABookPeriodKeyProvider.FASourceSpecificationItem, FABookPeriod.Key>
  {
    public virtual List<int?> SourceAssetIDs { get; set; }

    public virtual List<int?> SourceBookIDs { get; set; }

    public override bool SourcesEqual(object otherObject)
    {
      FABookPeriodKeyProvider.FAKeyWithSourceValues withSourceValues = (FABookPeriodKeyProvider.FAKeyWithSourceValues) otherObject;
      return base.SourcesEqual(otherObject) && this.SourceAssetIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v)).SequenceEqual<int?>((IEnumerable<int?>) withSourceValues.SourceAssetIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v))) && this.SourceBookIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v)).SequenceEqual<int?>((IEnumerable<int?>) withSourceValues.SourceBookIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v)));
    }
  }

  protected class ParameterEvaluator<TEnum> where TEnum : struct, IConvertible
  {
    protected Dictionary<TEnum, object> Parameters = new Dictionary<TEnum, object>();

    public ParameterEvaluator(TEnum mask, object[] parameters)
    {
      if (!typeof (TEnum).IsEnum)
        throw new ArgumentException("TEnum must be an enumerated type.");
      Queue<object> objectQueue = new Queue<object>((IEnumerable<object>) parameters);
      foreach (TEnum key in (TEnum[]) Enum.GetValues(typeof (TEnum)))
      {
        if (((int) (ValueType) key & (int) (ValueType) mask) > 0)
          this.Parameters.Add(key, objectQueue.Dequeue());
      }
    }

    public object this[TEnum index]
    {
      get
      {
        object obj;
        this.Parameters.TryGetValue(index, out obj);
        return obj;
      }
    }
  }
}
