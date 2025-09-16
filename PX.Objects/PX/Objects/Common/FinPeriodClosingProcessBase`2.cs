// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.FinPeriodClosingProcessBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public abstract class FinPeriodClosingProcessBase<TGraph, TSubledgerClosedFlagField> : 
  FinPeriodClosingProcessBase
  where TGraph : PXGraph
  where TSubledgerClosedFlagField : IBqlField
{
  public PXFilter<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> Filter;
  public PXCancel<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<FinPeriod, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters, Where<FinPeriod.organizationID, Equal<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID>>, And<FinPeriod.finYear, GreaterEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.fromYear>>, And<FinPeriod.finYear, LessEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.toYear>>, And<Where<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.close>, And<TSubledgerClosedFlagField, NotEqual<True>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.reopen>, And<TSubledgerClosedFlagField, Equal<True>, And<FinPeriod.status, NotEqual<FinPeriod.status.locked>>>>>>>>>>> FinPeriods;
  public PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.masterFinPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>> OrganizationFinPeriods;
  private BqlCommand applicableYearsQuery;
  public PXAction<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> ShowDocuments;

  public IEnumerable<FinPeriod> SelectedItems
  {
    get
    {
      return ((PXSelectBase) this.FinPeriods).Cache.Updated.Cast<FinPeriod>().Where<FinPeriod>((Func<FinPeriod, bool>) (p => p.Selected.GetValueOrDefault()));
    }
  }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  protected virtual void _(
    Events.FieldVerifying<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.fromYear> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.fromYear>>) e).ExternalCall)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.fromYear>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.FieldVerifying<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.toYear> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.toYear>>) e).ExternalCall)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.toYear>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.RowSelected<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> e)
  {
    FinPeriodClosingProcessBase closingProcessBase1 = GraphHelper.TypelessClone((PXGraph) this) as FinPeriodClosingProcessBase;
    PXFilteredProcessing<FinPeriod, FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters, Where<FinPeriod.organizationID, Equal<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID>>, And<FinPeriod.finYear, GreaterEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.fromYear>>, And<FinPeriod.finYear, LessEqual<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.toYear>>, And<Where<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.close>, And<TSubledgerClosedFlagField, NotEqual<True>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.reopen>, And<TSubledgerClosedFlagField, Equal<True>, And<FinPeriod.status, NotEqual<FinPeriod.status.locked>>>>>>>>>>> finPeriods = this.FinPeriods;
    FinPeriodClosingProcessBase closingProcessBase2 = closingProcessBase1;
    // ISSUE: virtual method pointer
    PXProcessingBase<FinPeriod>.ProcessListDelegate processListDelegate = new PXProcessingBase<FinPeriod>.ProcessListDelegate((object) closingProcessBase2, __vmethodptr(closingProcessBase2, ProcessPeriods));
    ((PXProcessingBase<FinPeriod>) finPeriods).SetProcessDelegate(processListDelegate);
    ((PXAction) this.ShowDocuments).SetEnabled(this.SelectedItems.Any<FinPeriod>());
    bool closedPeriod = this.FinPeriodUtils.CanPostToClosedPeriod();
    PXStringListAttribute.SetList<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters>>) e).Cache, (object) e.Row, closedPeriod ? (PXStringListAttribute) new FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.FullListAttribute() : (PXStringListAttribute) new FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.RestrictedListAttribute());
  }

  protected virtual void _(
    Events.RowUpdated<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> e)
  {
    int? organizationId1 = e.Row.OrganizationID;
    int? organizationId2 = e.OldRow.OrganizationID;
    if (organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue && !(e.Row.FromYear != e.OldRow.FromYear) && !(e.Row.ToYear != e.OldRow.ToYear))
      return;
    ((PXSelectBase) this.FinPeriods).Cache.Clear();
  }

  protected BqlCommand ApplicableYearsQuery
  {
    get
    {
      return this.applicableYearsQuery = this.applicableYearsQuery ?? PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXSelectJoin<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, InnerJoin<FinPeriod, On<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<FinPeriod.organizationID>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Equal<FinPeriod.finYear>>>>, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.filterOrganizationID>>, And<Where<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.close>, And<TSubledgerClosedFlagField, NotEqual<True>, Or<Current<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action>, Equal<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.action.reopen>, And<TSubledgerClosedFlagField, Equal<True>>>>>>>>.Config>.GetCommand();
    }
  }

  protected virtual void _(
    Events.FieldDefaulting<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.firstYear>, object, object>) e).NewValue = (object) BqlExtensions.SelectSingleReadonly<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>(this.ApplicableYearsQuery.OrderByNew<OrderBy<Asc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>(), (PXGraph) this, new IBqlTable[1]
    {
      (IBqlTable) e.Row
    })?.Year;
  }

  protected virtual void _(
    Events.FieldDefaulting<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters.lastYear>, object, object>) e).NewValue = (object) BqlExtensions.SelectSingleReadonly<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>(this.ApplicableYearsQuery.OrderByNew<OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>(), (PXGraph) this, new IBqlTable[1]
    {
      (IBqlTable) e.Row
    })?.Year;
  }

  public override string ClosedFieldName => typeof (TSubledgerClosedFlagField).Name;

  protected virtual void _(
    Events.FieldUpdated<FinPeriod, FinPeriod.selected> e)
  {
    FinPeriod currentPeriod = e.Row;
    if (currentPeriod == null)
      return;
    bool isSelected = currentPeriod.Selected.GetValueOrDefault();
    bool isDirectAction = FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.GetDirection(((PXSelectBase<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters>) this.Filter).Current.Action) == FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction.Direct;
    EnumerableExtensions.ForEach<FinPeriod>(GraphHelper.RowCast<FinPeriod>((IEnumerable) ((PXSelectBase<FinPeriod>) this.FinPeriods).Select(Array.Empty<object>())).Where<FinPeriod>((Func<FinPeriod, bool>) (p => !(isSelected & isDirectAction) && (isSelected || isDirectAction) || isSelected == p.Selected.GetValueOrDefault() ? string.CompareOrdinal(p.FinPeriodID, currentPeriod.FinPeriodID) > 0 : string.CompareOrdinal(p.FinPeriodID, currentPeriod.FinPeriodID) < 0)), (Action<FinPeriod>) (p =>
    {
      p.Selected = new bool?(isSelected);
      GraphHelper.MarkUpdated(((PXSelectBase) this.FinPeriods).Cache, (object) p);
    }));
    ((PXSelectBase) this.FinPeriods).View.RequestRefresh();
  }

  public override void ProcessPeriods(List<FinPeriod> finPeriods)
  {
    switch (((PXSelectBase<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters>) this.Filter).Current.Action)
    {
      case "Close":
        this.ClosePeriods(finPeriods);
        break;
      case "Reopen":
        this.ReopenPeriods(finPeriods);
        break;
    }
  }

  public override void ClosePeriods(List<FinPeriod> finPeriods)
  {
    GraphHelper.Caches<FinPeriod>((PXGraph) this);
    foreach (FinPeriod finPeriod in finPeriods)
    {
      PXProcessing.SetCurrentItem((object) finPeriod);
      this.ClosePeriod(finPeriod);
      PXProcessing.SetProcessed();
    }
  }

  public virtual void ReopenPeriods(List<FinPeriod> finPeriods)
  {
    GraphHelper.Caches<FinPeriod>((PXGraph) this);
    foreach (FinPeriod finPeriod in finPeriods)
    {
      PXProcessing.SetCurrentItem((object) finPeriod);
      this.ReopenPeriod(finPeriod);
      PXProcessing.SetProcessed();
    }
  }

  public override void ClosePeriod(FinPeriod finPeriod)
  {
    this.VerifyOpenDocuments((IFinPeriod) finPeriod);
    if (PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
    {
      foreach (PXResult<OrganizationFinPeriod> pxResult in ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Select(new object[1]
      {
        (object) finPeriod.FinPeriodID
      }))
      {
        OrganizationFinPeriod organizationFinPeriod = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult);
        ((PXSelectBase) this.OrganizationFinPeriods).Cache.SetValue((object) organizationFinPeriod, this.ClosedFieldName, (object) true);
        ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Update(organizationFinPeriod);
      }
    }
    ((PXSelectBase) this.FinPeriods).Cache.SetValue((object) finPeriod, this.ClosedFieldName, (object) true);
    ((PXSelectBase<FinPeriod>) this.FinPeriods).Update(finPeriod);
    this.Actions.PressSave();
  }

  public override void ReopenPeriod(FinPeriod finPeriod)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
    {
      foreach (PXResult<OrganizationFinPeriod> pxResult in ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Select(new object[1]
      {
        (object) finPeriod.FinPeriodID
      }))
      {
        OrganizationFinPeriod organizationFinPeriod = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult);
        ((PXSelectBase) this.OrganizationFinPeriods).Cache.SetValue((object) organizationFinPeriod, this.ClosedFieldName, (object) false);
        if (organizationFinPeriod.Status == "Closed")
          organizationFinPeriod.Status = "Open";
        ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Update(organizationFinPeriod);
      }
    }
    ((PXSelectBase) this.FinPeriods).Cache.SetValue((object) finPeriod, this.ClosedFieldName, (object) false);
    if (finPeriod.Status == "Closed")
      finPeriod.Status = "Open";
    ((PXSelectBase<FinPeriod>) this.FinPeriods).Update(finPeriod);
    this.Actions.PressSave();
  }

  public override bool IsUnclosablePeriod(FinPeriod finPeriod)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
      return !this.CheckOpenDocuments((IFinPeriod) finPeriod).IsSuccess;
    return GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Select(new object[1]
    {
      (object) finPeriod.FinPeriodID
    })).Any<OrganizationFinPeriod>((Func<OrganizationFinPeriod, bool>) (orgFinPeriod => !this.CheckOpenDocuments((IFinPeriod) orgFinPeriod).IsSuccess));
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true, IsLockedOnToolbar = true)]
  public virtual IEnumerable showDocuments(PXAdapter adapter)
  {
    this.ShowOpenDocuments(this.SelectedItems);
    return adapter.Get();
  }

  protected virtual IPXResultset GetResultset(
    BqlCommand command,
    int? organizationID,
    string fromPeriodID,
    string toPeriodID)
  {
    return this.GetResultset(command, organizationID, fromPeriodID, toPeriodID, (string[]) null);
  }

  protected IPXResultset GetResultset(
    BqlCommand command,
    int? organizationID,
    string fromPeriodID,
    string toPeriodID,
    string[] sortAsImplicitColumns)
  {
    return (IPXResultset) new PXReportResultset((IEnumerable) new PXView((PXGraph) this, true, command).SelectMultiBoundSortAsImplicit(new object[1]
    {
      (object) new FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters()
      {
        OrganizationID = organizationID,
        FromFinPeriodID = fromPeriodID,
        ToFinPeriodID = toPeriodID
      }
    }, sortAsImplicitColumns, Array.Empty<object>()));
  }

  public override List<(string ReportID, IPXResultset ReportData)> GetReportsData(
    int? organizationID,
    string fromPeriodID,
    string toPeriodID)
  {
    return ((IEnumerable<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule>) this.CheckingRules).Select<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule, (string, IPXResultset)>((Func<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule, (string, IPXResultset)>) (checker => (checker.ReportID, this.GetResultset(checker.CheckCommand, organizationID, fromPeriodID, toPeriodID)))).Where<(string, IPXResultset)>((Func<(string, IPXResultset), bool>) (tuple =>
    {
      IPXResultset reportData = tuple.ReportData;
      return (reportData != null ? reportData.GetRowCount() : 0) > 0;
    })).ToList<(string, IPXResultset)>();
  }

  protected virtual string EmptyReportMessage
  {
    get => "There are no unreleased documents for the selected period or periods.";
  }

  protected virtual void ShowOpenDocuments(IEnumerable<FinPeriod> periods)
  {
    ParallelQuery<string> periodIDs = periods.Select<FinPeriod, string>((Func<FinPeriod, string>) (fp => fp.FinPeriodID)).AsParallel<string>();
    List<(string ReportID, IPXResultset ReportData)> reportsData = this.GetReportsData(((PXSelectBase<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters>) this.Filter).Current.OrganizationID, periodIDs.Min<string>(), periodIDs.Max<string>());
    if (reportsData.Any<(string, IPXResultset)>())
    {
      (string ReportID, IPXResultset ReportData) = reportsData.First<(string, IPXResultset)>();
      PXReportRequiredException report = new PXReportRequiredException((IPXResultset) new PXReportRedirectParameters()
      {
        ResultSet = ReportData,
        ReportParameters = new Dictionary<string, string>()
        {
          {
            "OrganizationID",
            OrganizationMaint.FindOrganizationByID((PXGraph) this, periods.First<FinPeriod>().OrganizationID)?.OrganizationCD
          },
          {
            "FromPeriodID",
            FinPeriodIDFormattingAttribute.FormatForDisplay(periodIDs.Min<string>())
          },
          {
            "ToPeriodID",
            FinPeriodIDFormattingAttribute.FormatForDisplay(periodIDs.Max<string>())
          }
        }
      }, ReportID, (CurrentLocalization) null);
      EnumerableExtensions.ForEach<(string, IPXResultset)>(reportsData.Where<(string, IPXResultset)>((Func<(string, IPXResultset), bool>) (tuple => tuple.ReportID != ReportID)), (Action<(string, IPXResultset)>) (tuple => report.AddSibling(tuple.ReportID, new PXReportRedirectParameters()
      {
        ResultSet = tuple.ReportData,
        ReportParameters = new Dictionary<string, string>()
        {
          {
            "OrganizationID",
            OrganizationMaint.FindOrganizationByID((PXGraph) this, periods.First<FinPeriod>().OrganizationID)?.OrganizationCD
          },
          {
            "FromPeriodID",
            FinPeriodIDFormattingAttribute.FormatForDisplay(periodIDs.Min<string>())
          },
          {
            "ToPeriodID",
            FinPeriodIDFormattingAttribute.FormatForDisplay(periodIDs.Max<string>())
          }
        }
      })));
      throw report;
    }
    ((PXSelectBase<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters>) this.Filter).Ask(this.EmptyReportMessage, (MessageButtons) 0);
  }

  protected virtual ProcessingResult CheckOpenDocuments(IFinPeriod finPeriod)
  {
    ProcessingResult processingResult = new ProcessingResult();
    if (!this.NeedValidate)
      return processingResult;
    FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters objectsQueryParameters1 = new FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters();
    int? nullable1 = finPeriod.OrganizationID;
    int num = 0;
    int? nullable2;
    if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
    {
      nullable2 = finPeriod.OrganizationID;
    }
    else
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    objectsQueryParameters1.OrganizationID = nullable2;
    objectsQueryParameters1.FromFinPeriodID = finPeriod.FinPeriodID;
    objectsQueryParameters1.ToFinPeriodID = finPeriod.FinPeriodID;
    objectsQueryParameters1.FromFinPeriodStartDate = finPeriod.StartDate;
    objectsQueryParameters1.ToFinPeriodEndDate = finPeriod.EndDate;
    FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters objectsQueryParameters2 = objectsQueryParameters1;
    foreach (FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule checkingRule in this.CheckingRules)
    {
      BqlCommand checkCommand = checkingRule.CheckCommand;
      PXView pxView = new PXView((PXGraph) this, true, checkCommand);
      PXResult result = (PXResult) null;
      List<Type> typeList = new List<Type>();
      typeList.AddRange((IEnumerable<Type>) this.Caches[checkCommand.GetFirstTable()].BqlKeys);
      typeList.AddRange((IEnumerable<Type>) checkingRule.MessageParameters);
      using (new PXFieldScope(pxView, typeList.ToArray()))
        result = (PXResult) pxView.SelectSingleBound(new object[1]
        {
          (object) objectsQueryParameters2
        }, Array.Empty<object>());
      if (result != null)
      {
        List<object> list = ((IEnumerable<Type>) checkingRule.MessageParameters).Select<Type, object>((Func<Type, object>) (param => this.Caches[BqlCommand.GetItemType(param)].GetStateExt((object) PXResult.Unwrap(result[BqlCommand.GetItemType(param)], BqlCommand.GetItemType(param)), param.Name))).ToList<object>();
        list.Add((object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID));
        processingResult.AddErrorMessage(checkingRule.ErrorMessage, list.ToArray());
      }
    }
    return processingResult;
  }

  protected virtual void VerifyOpenDocuments(IFinPeriod finPeriod)
  {
    FinPeriodStatusProcess.MarkProcessedRowAsErrorAndStop(this.CheckOpenDocuments(finPeriod));
  }
}
