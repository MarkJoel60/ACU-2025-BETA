// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.GenericFinPeriodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.DAC;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public class GenericFinPeriodSelectorAttribute : PXCustomSelectorAttribute
{
  public Type OrigSearchType { get; set; }

  public Func<ICalendarOrganizationIDProvider> GetCalendarOrganizationIDProvider { get; set; }

  public bool TakeBranchForSelectorFromQueryParams { get; set; }

  public bool TakeOrganizationForSelectorFromQueryParams { get; set; }

  public bool MasterPeriodBasedOnOrganizationPeriods { get; set; }

  public GenericFinPeriodSelectorAttribute(
    Type searchType,
    Func<ICalendarOrganizationIDProvider> getCalendarOrganizationIDProvider,
    bool takeBranchForSelectorFromQueryParams = false,
    bool takeOrganizationForSelectorFromQueryParams = false,
    bool masterPeriodBasedOnOrganizationPeriods = true,
    FinPeriodSelectorAttribute.SelectionModesWithRestrictions selectionModeWithRestrictions = FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined,
    Type[] fieldList = null)
    : base(GenericFinPeriodSelectorAttribute.GetSearchType(searchType, takeBranchForSelectorFromQueryParams, takeOrganizationForSelectorFromQueryParams), fieldList)
  {
    this.OrigSearchType = searchType;
    this.GetCalendarOrganizationIDProvider = getCalendarOrganizationIDProvider;
    this.TakeBranchForSelectorFromQueryParams = takeBranchForSelectorFromQueryParams;
    this.TakeOrganizationForSelectorFromQueryParams = takeOrganizationForSelectorFromQueryParams;
    this.MasterPeriodBasedOnOrganizationPeriods = masterPeriodBasedOnOrganizationPeriods;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall && e.Row == null)
      e.NewValue = (object) FinPeriodIDFormattingAttribute.FormatForStoring(e.NewValue as string);
    base.FieldVerifying(sender, e);
  }

  public static Type GetSearchType(
    Type origSearchType,
    bool takeBranchForSelectorFromQueryParams,
    bool takeOrganizationForSelectorFromQueryParams)
  {
    if (!(takeBranchForSelectorFromQueryParams | takeOrganizationForSelectorFromQueryParams))
      return origSearchType;
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
    {
      origSearchType
    });
    if (takeBranchForSelectorFromQueryParams)
      bqlCommand = bqlCommand.WhereAnd<Where<FinPeriod.organizationID, Equal<Optional2<QueryParameters.branchID>>>>();
    if (takeOrganizationForSelectorFromQueryParams)
      bqlCommand = bqlCommand.WhereAnd<Where<FinPeriod.organizationID, Equal<Optional2<QueryParameters.organizationID>>>>();
    return bqlCommand.GetType();
  }

  protected virtual FinPeriod BuildFinPeriod(int? organizationID, object record)
  {
    int? nullable = organizationID;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue && this.MasterPeriodBasedOnOrganizationPeriods)
    {
      MasterFinPeriod masterFinPeriod = (record as PXResult).GetItem<MasterFinPeriod>();
      return new FinPeriod()
      {
        FinPeriodID = masterFinPeriod.FinPeriodID,
        StartDateUI = masterFinPeriod.StartDateUI,
        EndDateUI = masterFinPeriod.EndDateUI,
        Descr = masterFinPeriod.Descr,
        NoteID = masterFinPeriod.NoteID
      };
    }
    FinPeriod finPeriod = record is PXResult pxResult ? pxResult.GetItem<FinPeriod>() : (FinPeriod) record;
    return new FinPeriod()
    {
      FinPeriodID = finPeriod.FinPeriodID,
      StartDateUI = finPeriod.StartDateUI,
      EndDateUI = finPeriod.EndDateUI,
      Descr = finPeriod.Descr,
      NoteID = finPeriod.NoteID
    };
  }

  protected virtual IEnumerable GetRecords()
  {
    PXCache cach = this._Graph.Caches[((PXSelectorAttribute) this)._CacheType];
    object extRow = ((IEnumerable<object>) PXView.Currents).Where<object>((Func<object, bool>) (_ => _ != null)).FirstOrDefault<object>((Func<object, bool>) (c => ((PXSelectorAttribute) this)._CacheType.IsAssignableFrom(c.GetType())));
    int? calendarOrganizationID = this.TakeBranchForSelectorFromQueryParams || this.TakeOrganizationForSelectorFromQueryParams ? this.GetCalendarOrganizationIDProvider().GetCalendarOrganizationID(this._Graph, PXView.Parameters, this.TakeBranchForSelectorFromQueryParams, this.TakeOrganizationForSelectorFromQueryParams) : this.GetCalendarOrganizationIDProvider().GetCalendarOrganizationID(cach.Graph, cach, extRow);
    calendarOrganizationID = new int?(calendarOrganizationID.GetValueOrDefault());
    int startRow = PXView.StartRow;
    int num1 = 0;
    List<object> parameters = new List<object>();
    BqlCommand command = this.GetCommand(cach, extRow, parameters, calendarOrganizationID);
    PXGraph graph = this._Graph;
    PXView view = PXView.View;
    int num2 = view != null ? (view.IsReadOnly ? 1 : 0) : 1;
    BqlCommand bqlCommand = command;
    PXView pxView = new PXView(graph, num2 != 0, bqlCommand);
    try
    {
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      foreach (PXFilterRow filter in PXView.Filters)
      {
        int? nullable = calendarOrganizationID;
        int num3 = 0;
        if (nullable.GetValueOrDefault() == num3 & nullable.HasValue && this.MasterPeriodBasedOnOrganizationPeriods && string.Equals(filter.DataField, "finPeriodID", StringComparison.OrdinalIgnoreCase))
          filter.DataField = "masterFinPeriodID";
        pxFilterRowList.Add(filter);
      }
      return (IEnumerable) pxView.Select(PXView.Currents, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, pxFilterRowList.ToArray(), ref startRow, PXView.MaximumRows, ref num1).Select<object, FinPeriod>((Func<object, FinPeriod>) (record => this.BuildFinPeriod(calendarOrganizationID, record))).ToArray<FinPeriod>();
    }
    finally
    {
      PXView.StartRow = 0;
    }
  }

  protected virtual BqlCommand GetCommand(
    PXCache cache,
    object extRow,
    List<object> parameters,
    int? calendarOrganizationID)
  {
    BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
    {
      this.OrigSearchType
    });
    int? nullable = calendarOrganizationID;
    int num = 0;
    BqlCommand command;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue && this.MasterPeriodBasedOnOrganizationPeriods)
    {
      BqlCommand bqlCommand = BqlCommand.AppendJoin<LeftJoin<MasterFinPeriod, On<FinPeriod.masterFinPeriodID, Equal<MasterFinPeriod.finPeriodID>>>>(instance).AggregateNew<Aggregate<GroupBy<FinPeriod.masterFinPeriodID, GroupBy<MasterFinPeriod.startDate, GroupBy<MasterFinPeriod.endDate, GroupBy<MasterFinPeriod.noteID>>>>>>();
      int?[] array = this.GetCalendarOrganizationIDProvider().GetKeysWithBasisOrganizationIDs(cache.Graph, cache, extRow).ConsolidatedOrganizationIDs.ToArray();
      if (((IEnumerable<int?>) array).Any<int?>())
      {
        command = bqlCommand.WhereAnd<Where<FinPeriod.organizationID, In<Required<FinPeriod.organizationID>>>>();
        parameters.Add((object) array);
      }
      else
      {
        command = bqlCommand.WhereAnd<Where<FinPeriod.organizationID, NotEqual<Required<FinPeriod.organizationID>>>>();
        parameters.Add((object) calendarOrganizationID);
      }
    }
    else
    {
      command = instance.WhereAnd<Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>();
      parameters.Add((object) calendarOrganizationID);
    }
    return command;
  }
}
