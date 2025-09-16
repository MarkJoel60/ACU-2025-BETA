// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.IPeriodKeyProvider`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Abstractions.Periods;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public interface IPeriodKeyProvider<TSourcesSpecificationCollection, TSourceSpecificationItem, TKeyWithSourceValuesCollection, TKeyWithSourceValuesCollectionItem, TKey> : 
  IPeriodKeyProvider<TKey, TSourcesSpecificationCollection>,
  IPeriodKeyProviderBase
  where TSourcesSpecificationCollection : PeriodKeyProviderBase.SourcesSpecificationCollection<TSourceSpecificationItem>
  where TSourceSpecificationItem : PeriodKeyProviderBase.SourceSpecificationItem
  where TKeyWithSourceValuesCollection : PeriodKeyProviderBase.KeyWithSourceValuesCollection<TKeyWithSourceValuesCollectionItem, TSourceSpecificationItem, TKey>
  where TKeyWithSourceValuesCollectionItem : PeriodKeyProviderBase.KeyWithSourceValues<TSourceSpecificationItem, TKey>
  where TKey : OrganizationDependedPeriodKey, new()
{
  Type UseMasterCalendarSourceType { get; set; }

  bool UseMasterOrganizationIDByDefault { get; set; }

  TKeyWithSourceValuesCollection BuildKeyCollection(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    Func<PXGraph, PXCache, object, TSourceSpecificationItem, TKeyWithSourceValuesCollectionItem> buildItemDelegate,
    bool skipMain = false);

  TKeyWithSourceValuesCollection GetKeys(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    bool skipMain = false);

  TKeyWithSourceValuesCollection GetKeysWithBasisOrganizationIDs(
    PXGraph graph,
    PXCache attributeCache,
    object extRow);

  TKeyWithSourceValuesCollectionItem GetRawMainKeyWithSourceValues(
    PXGraph graph,
    PXCache attributeCache,
    object extRow);

  TKeyWithSourceValuesCollectionItem GetMainSourceOrganizationIDs(
    PXGraph graph,
    PXCache attributeCache,
    object extRow);

  TKeyWithSourceValuesCollectionItem GetOrganizationIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem sourceSpecification);

  TKeyWithSourceValuesCollectionItem GetBranchIDsValueFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    TSourceSpecificationItem sourceSpecification);

  List<TKeyWithSourceValuesCollectionItem> GetBranchIDsValuesFromField(
    PXGraph graph,
    PXCache attributeCache,
    object extRow,
    bool skipMain = false);
}
