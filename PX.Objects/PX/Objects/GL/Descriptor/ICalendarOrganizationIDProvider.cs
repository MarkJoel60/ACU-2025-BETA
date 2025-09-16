// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.ICalendarOrganizationIDProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public interface ICalendarOrganizationIDProvider : 
  IPeriodKeyProvider<PeriodKeyProviderBase.SourcesSpecificationCollection, PeriodKeyProviderBase.SourceSpecificationItem, CalendarOrganizationIDProvider.PeriodKeyWithSourceValuesCollection, CalendarOrganizationIDProvider.KeyWithSourceValues, FinPeriod.Key>,
  IPeriodKeyProvider<FinPeriod.Key, PeriodKeyProviderBase.SourcesSpecificationCollection>,
  IPeriodKeyProviderBase
{
  int? GetCalendarOrganizationID(PXGraph graph, PXCache attributeCache, object extRow);

  int? GetCalendarOrganizationID(
    PXGraph graph,
    object[] pars,
    bool takeBranchForSelectorFromQueryParams,
    bool takeOrganizationForSelectorFromQueryParams);

  List<int?> GetDetailOrganizationIDs(PXGraph graph);
}
