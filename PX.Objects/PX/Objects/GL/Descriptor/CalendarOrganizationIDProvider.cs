// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.CalendarOrganizationIDProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public class CalendarOrganizationIDProvider(
  PeriodKeyProviderBase.SourcesSpecificationCollection sourcesSpecification = null,
  Type[] sourceSpecificationTypes = null,
  Type useMasterCalendarSourceType = null,
  bool useMasterOrganizationIDByDefault = false) : 
  PeriodKeyProviderBase<PeriodKeyProviderBase.SourcesSpecificationCollection, PeriodKeyProviderBase.SourceSpecificationItem, CalendarOrganizationIDProvider.PeriodKeyWithSourceValuesCollection, CalendarOrganizationIDProvider.KeyWithSourceValues, FinPeriod.Key>(sourcesSpecification, sourceSpecificationTypes, useMasterCalendarSourceType, useMasterOrganizationIDByDefault),
  ICalendarOrganizationIDProvider,
  IPeriodKeyProvider<PeriodKeyProviderBase.SourcesSpecificationCollection, PeriodKeyProviderBase.SourceSpecificationItem, CalendarOrganizationIDProvider.PeriodKeyWithSourceValuesCollection, CalendarOrganizationIDProvider.KeyWithSourceValues, FinPeriod.Key>,
  IPeriodKeyProvider<FinPeriod.Key, PeriodKeyProviderBase.SourcesSpecificationCollection>,
  IPeriodKeyProviderBase
{
  public override int? MasterValue => new int?(0);

  public int? GetCalendarOrganizationID(PXGraph graph, PXCache attributeCache, object extRow)
  {
    return this.GetKey(graph, attributeCache, extRow).OrganizationID;
  }

  public override FinPeriod.Key GetKey(PXGraph graph, PXCache attributeCache, object extRow)
  {
    FinPeriod.Key key = base.GetKey(graph, attributeCache, extRow);
    if (!key.OrganizationID.HasValue && (this.UseMasterOrganizationIDByDefault || this.UseMasterCalendarSourceType != (Type) null))
      key.OrganizationID = this.MasterValue;
    return key;
  }

  public virtual int? GetCalendarOrganizationID(
    PXGraph graph,
    object[] pars,
    bool takeBranchForSelectorFromQueryParams,
    bool takeOrganizationForSelectorFromQueryParams)
  {
    PXGraph graph1 = graph;
    CalendarOrganizationIDProvider.KeyWithSourceValues keyWithKeyWithSourceValues = new CalendarOrganizationIDProvider.KeyWithSourceValues();
    keyWithKeyWithSourceValues.SourceBranchIDs = takeBranchForSelectorFromQueryParams ? ((int?) pars[0]).SingleToList<int?>() : (List<int?>) null;
    keyWithKeyWithSourceValues.SourceOrganizationIDs = takeOrganizationForSelectorFromQueryParams ? ((int?) pars[takeBranchForSelectorFromQueryParams ? 1 : 0]).SingleToList<int?>() : (List<int?>) null;
    CalendarOrganizationIDProvider.KeyWithSourceValues rawKey = this.EvaluateRawKey(graph1, keyWithKeyWithSourceValues);
    return !this.IsIDsUndefined(rawKey.KeyOrganizationIDs) || !this.UseMasterOrganizationIDByDefault ? rawKey.KeyOrganizationIDs.FirstOrDefault<int?>() : this.MasterValue;
  }

  public virtual List<int?> GetDetailOrganizationIDs(PXGraph graph)
  {
    IDocumentWithFinDetailsGraphExtension implementation = graph.FindImplementation<IDocumentWithFinDetailsGraphExtension>();
    return implementation == null ? new List<int?>() : implementation.GetOrganizationIDsInDetails();
  }

  protected override CalendarOrganizationIDProvider.KeyWithSourceValues EvaluateRawKey(
    PXGraph graph,
    CalendarOrganizationIDProvider.KeyWithSourceValues keyWithSourceValues)
  {
    if (keyWithSourceValues == null)
      return (CalendarOrganizationIDProvider.KeyWithSourceValues) null;
    keyWithSourceValues.KeyOrganizationIDs = keyWithSourceValues.SourceOrganizationIDs;
    if (this.IsIDsUndefined(keyWithSourceValues.KeyOrganizationIDs) && keyWithSourceValues.SourceBranchIDs != null)
      keyWithSourceValues.KeyOrganizationIDs = keyWithSourceValues.SourceBranchIDs.Select<int?, int?>((Func<int?, int?>) (branchID => PXAccess.GetParentOrganizationID(branchID))).ToList<int?>();
    keyWithSourceValues.Key.OrganizationID = keyWithSourceValues.KeyOrganizationIDs.First<int?>();
    return keyWithSourceValues;
  }

  public class KeyWithSourceValues : 
    PeriodKeyProviderBase.KeyWithSourceValues<PeriodKeyProviderBase.SourceSpecificationItem, FinPeriod.Key>
  {
  }

  public class PeriodKeyWithSourceValuesCollection : 
    PeriodKeyProviderBase.KeyWithSourceValuesCollection<CalendarOrganizationIDProvider.KeyWithSourceValues, PeriodKeyProviderBase.SourceSpecificationItem, FinPeriod.Key>
  {
  }
}
