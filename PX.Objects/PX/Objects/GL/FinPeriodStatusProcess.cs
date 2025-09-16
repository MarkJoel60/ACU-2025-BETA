// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodStatusProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Formula;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.GL;

public class FinPeriodStatusProcess : PXGraph<
#nullable disable
FinPeriodStatusProcess>
{
  public PXCancel<FinPeriodStatusProcess.FinPeriodStatusProcessParameters> Cancel;
  public PXFilter<FinPeriodStatusProcess.FinPeriodStatusProcessParameters> Filter;
  public PXFilteredProcessing<FinPeriod, FinPeriodStatusProcess.FinPeriodStatusProcessParameters, Where<FinPeriod.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And<FinPeriod.finYear, GreaterEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear>>, And<FinPeriod.finYear, LessEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>>, And<FinPeriod.status, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.affectedStatus>>>>>>> FinPeriods;
  public PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>> OrganizationFinPeriods;
  public PXSetup<GLSetup> glsetup;
  private List<(string subledgerPrefix, Type closingFlag)> _subledgerClosingFlags;
  public PXAction<FinPeriodStatusProcess.FinPeriodStatusProcessParameters> ShowDocuments;

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public FinPeriodStatusProcess()
  {
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
  }

  public IEnumerable<FinPeriod> SelectedItems
  {
    get
    {
      return ((PXSelectBase) this.FinPeriods).Cache.Updated.Cast<FinPeriod>().Where<FinPeriod>((Func<FinPeriod, bool>) (p => p.Selected.GetValueOrDefault()));
    }
  }

  protected virtual List<(string subledgerPrefix, Type closingGraphType)> SubledgerClosingGraphTypes { get; } = new List<(string, Type)>()
  {
    ("AP", typeof (APClosingProcess)),
    ("AR", typeof (ARClosingProcess)),
    ("CA", typeof (CAClosingProcess)),
    ("FA", typeof (FAClosingProcess)),
    ("IN", typeof (INClosingProcess))
  };

  protected virtual List<(string subledgerPrefix, Type closingFlag)> SubledgerClosingFlags
  {
    get
    {
      if (this._subledgerClosingFlags == null)
      {
        this._subledgerClosingFlags = new List<(string, Type)>()
        {
          ("AP", typeof (FinPeriod.aPClosed)),
          ("AR", typeof (FinPeriod.aRClosed)),
          ("CA", typeof (FinPeriod.cAClosed))
        };
        if (PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>())
          this._subledgerClosingFlags.Add(("FA", typeof (FinPeriod.fAClosed)));
        if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
          this._subledgerClosingFlags.Add(("IN", typeof (FinPeriod.iNClosed)));
      }
      return this._subledgerClosingFlags;
    }
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true, IsLockedOnToolbar = true)]
  public virtual IEnumerable showDocuments(PXAdapter adapter)
  {
    if (this.SelectedItems.Any<FinPeriod>())
      this.ShowOpenDocuments(this.SelectedItems);
    return adapter.Get();
  }

  protected virtual IPXResultset GetResultset(
    BqlCommand command,
    int? organizationID,
    string fromPeriodID,
    string toPeriodID)
  {
    return (IPXResultset) new PXReportResultset((IEnumerable) new PXView((PXGraph) this, true, command).SelectMultiBound(new object[1]
    {
      (object) new FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters()
      {
        OrganizationID = organizationID,
        FromFinPeriodID = fromPeriodID,
        ToFinPeriodID = toPeriodID
      }
    }, Array.Empty<object>()));
  }

  protected virtual List<(string ReportID, IPXResultset ReportData)> GetReportsData(
    string fromPeriodID,
    string toPeriodID)
  {
    return ((IEnumerable<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule>) this.CheckingRules).Select<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule, (string, IPXResultset)>((Func<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule, (string, IPXResultset)>) (checker => (checker.ReportID, this.GetResultset(checker.CheckCommand, ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current.OrganizationID, fromPeriodID, toPeriodID)))).Where<(string, IPXResultset)>((Func<(string, IPXResultset), bool>) (tuple =>
    {
      IPXResultset reportData = tuple.ReportData;
      return (reportData != null ? reportData.GetRowCount() : 0) > 0;
    })).ToList<(string, IPXResultset)>();
  }

  protected virtual void ShowOpenDocuments(IEnumerable<FinPeriod> periods)
  {
    ParallelQuery<string> periodIDs = periods.Select<FinPeriod, string>((Func<FinPeriod, string>) (fp => fp.FinPeriodID)).AsParallel<string>();
    string fromFinPeriodID = periodIDs.Min<string>();
    string toFinPeriodID = periodIDs.Max<string>();
    List<(string, IPXResultset)> list = this.SubledgerClosingGraphTypes.Select<(string, Type), PXGraph>((Func<(string, Type), PXGraph>) (tuple => PXGraph.CreateInstance(tuple.closingGraphType))).OfType<FinPeriodClosingProcessBase>().Where<FinPeriodClosingProcessBase>((Func<FinPeriodClosingProcessBase, bool>) (closingGraph => closingGraph.NeedValidate)).SelectMany<FinPeriodClosingProcessBase, (string, IPXResultset)>((Func<FinPeriodClosingProcessBase, IEnumerable<(string, IPXResultset)>>) (closingGraph =>
    {
      if (closingGraph is ARClosingProcess arClosingProcess2 && ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current.Action == "Close")
        arClosingProcess2.ExcludePendingProcessingDocs = true;
      return (IEnumerable<(string, IPXResultset)>) closingGraph.GetReportsData(((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current.OrganizationID, fromFinPeriodID, toFinPeriodID);
    })).ToList<(string, IPXResultset)>();
    list.AddRange((IEnumerable<(string, IPXResultset)>) this.GetReportsData(fromFinPeriodID, toFinPeriodID));
    if (list.Any<(string, IPXResultset)>())
    {
      (string str, IPXResultset ipxResultset) = list.First<(string, IPXResultset)>();
      PXReportRequiredException report = new PXReportRequiredException((IPXResultset) new PXReportRedirectParameters()
      {
        ResultSet = ipxResultset,
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
      }, str, (CurrentLocalization) null);
      EnumerableExtensions.ForEach<(string, IPXResultset)>(list.Where<(string, IPXResultset)>((Func<(string, IPXResultset), bool>) (tuple => tuple.ReportID != str)), (Action<(string, IPXResultset)>) (tuple => report.AddSibling(tuple.ReportID, new PXReportRedirectParameters()
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
    ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Ask("There are no unposted documents for the selected period or periods.", (MessageButtons) 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear>>) e).ExternalCall)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>>) e).ExternalCall)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<FinPeriodStatusProcess.FinPeriodStatusProcessParameters> e)
  {
    if (RunningFlagScope<FinPeriodStatusProcess>.IsRunning)
      return;
    FinPeriodStatusProcess periodStatusProcess1 = GraphHelper.Clone<FinPeriodStatusProcess>(this);
    using (new RunningFlagScope<FinPeriodStatusProcess>())
      ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) periodStatusProcess1.Filter).Current = (FinPeriodStatusProcess.FinPeriodStatusProcessParameters) ((PXSelectBase) this.Filter).Cache.CreateCopy((object) ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current);
    PXFilteredProcessing<FinPeriod, FinPeriodStatusProcess.FinPeriodStatusProcessParameters, Where<FinPeriod.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And<FinPeriod.finYear, GreaterEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear>>, And<FinPeriod.finYear, LessEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>>, And<FinPeriod.status, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.affectedStatus>>>>>>> finPeriods1 = this.FinPeriods;
    FinPeriodStatusProcess periodStatusProcess2 = periodStatusProcess1;
    // ISSUE: virtual method pointer
    PXProcessingBase<FinPeriod>.ProcessListDelegate processListDelegate = new PXProcessingBase<FinPeriod>.ProcessListDelegate((object) periodStatusProcess2, __vmethodptr(periodStatusProcess2, ProcessStatus));
    ((PXProcessingBase<FinPeriod>) finPeriods1).SetProcessDelegate(processListDelegate);
    PXFilteredProcessing<FinPeriod, FinPeriodStatusProcess.FinPeriodStatusProcessParameters, Where<FinPeriod.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And<FinPeriod.finYear, GreaterEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear>>, And<FinPeriod.finYear, LessEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>>, And<FinPeriod.status, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.affectedStatus>>>>>>> finPeriods2 = this.FinPeriods;
    FinPeriodStatusProcess periodStatusProcess3 = this;
    // ISSUE: virtual method pointer
    PXProcessingBase<FinPeriod>.ParametersDelegate parametersDelegate = new PXProcessingBase<FinPeriod>.ParametersDelegate((object) periodStatusProcess3, __vmethodptr(periodStatusProcess3, ConfirmProcessing));
    ((PXProcessingBase<FinPeriod>) finPeriods2).SetParametersDelegate(parametersDelegate);
    ((PXAction) this.ShowDocuments).SetEnabled(this.SelectedItems.Any<FinPeriod>());
    bool closedPeriod = this.FinPeriodUtils.CanPostToClosedPeriod();
    PXStringListAttribute.SetList<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>>) e).Cache, (object) e.Row, closedPeriod ? (PXStringListAttribute) new FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.FullListAttribute() : (PXStringListAttribute) new FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.RestrictedListAttribute());
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<FinPeriodStatusProcess.FinPeriodStatusProcessParameters> e)
  {
    if (((PXSelectBase) this.Filter).Cache.ObjectsEqual<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.organizationID, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase) this.FinPeriods).Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FinPeriodStatusProcess.FinPeriodStatusProcessParameters, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action> e)
  {
    ((PXSelectBase) this.FinPeriods).Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FinPeriod, FinPeriod.selected> e)
  {
    FinPeriod currentPeriod = e.Row;
    if (currentPeriod == null || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FinPeriod, FinPeriod.selected>>) e).ExternalCall)
      return;
    bool isSelected = currentPeriod.Selected.GetValueOrDefault();
    bool isDirectAction = FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.GetDirection(((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current.Action) == FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction.Direct;
    EnumerableExtensions.ForEach<FinPeriod>(GraphHelper.RowCast<FinPeriod>((IEnumerable) ((PXSelectBase<FinPeriod>) this.FinPeriods).Select(Array.Empty<object>())).Where<FinPeriod>((Func<FinPeriod, bool>) (p => !(isSelected & isDirectAction) && (isSelected || isDirectAction) || isSelected == p.Selected.GetValueOrDefault() ? string.CompareOrdinal(p.FinPeriodID, currentPeriod.FinPeriodID) > 0 : string.CompareOrdinal(p.FinPeriodID, currentPeriod.FinPeriodID) < 0)), (Action<FinPeriod>) (p =>
    {
      p.Selected = new bool?(isSelected);
      GraphHelper.MarkUpdated(((PXSelectBase) this.FinPeriods).Cache, (object) p);
    }));
    ((PXSelectBase) this.FinPeriods).View.RequestRefresh();
  }

  protected virtual void CloseFinPeriodInSubledgers(FinPeriod period)
  {
    HashSet<string> stringSet = new HashSet<string>();
    foreach ((string subledgerPrefix, Type closingGraphType) closingGraphType in this.SubledgerClosingGraphTypes)
    {
      string subledgerPrefix = closingGraphType.subledgerPrefix;
      if (PXGraph.CreateInstance(closingGraphType.closingGraphType) is FinPeriodClosingProcessBase instance)
      {
        if (!(bool) ((PXSelectBase) this.FinPeriods).Cache.GetValue((object) period, instance.ClosedFieldName))
        {
          try
          {
            instance.ClosePeriod(period);
          }
          catch (PXException ex)
          {
            stringSet.Add(this.GetSubledgerTitle(subledgerPrefix));
          }
        }
      }
    }
    if (!stringSet.Any<string>())
      return;
    FinPeriodStatusProcess.MarkProcessedRowAsErrorAndStop("The {0} financial period cannot be closed in {1}.", (object) FinPeriodIDFormattingAttribute.FormatForError(period.FinPeriodID), (object) string.Join(", ", (IEnumerable<string>) stringSet));
  }

  protected virtual void ReopenFinPeriodInSubledgers(FinPeriod period)
  {
    foreach ((string subledgerPrefix, Type closingGraphType) closingGraphType in this.SubledgerClosingGraphTypes)
    {
      if (PXGraph.CreateInstance(closingGraphType.closingGraphType) is FinPeriodClosingProcessBase instance && (bool) ((PXSelectBase) this.FinPeriods).Cache.GetValue((object) period, instance.ClosedFieldName))
        instance.ReopenPeriod(period);
    }
  }

  protected virtual void VerifyFinPeriodForLock(FinPeriod period)
  {
    HashSet<string> stringSet = new HashSet<string>();
    foreach ((string subledgerPrefix, Type closingGraphType) closingGraphType in this.SubledgerClosingGraphTypes)
    {
      string subledgerPrefix = closingGraphType.subledgerPrefix;
      if (PXGraph.CreateInstance(closingGraphType.closingGraphType) is FinPeriodClosingProcessBase instance && instance.IsUnclosablePeriod(period))
        stringSet.Add(this.GetSubledgerTitle(subledgerPrefix));
    }
    if (this.IsUnclosablePeriod(period))
      stringSet.Add(this.GetSubledgerTitle<BatchModule.moduleGL>());
    if (!stringSet.Any<string>())
      return;
    FinPeriodStatusProcess.MarkProcessedRowAsErrorAndStop("The {0} financial period cannot be locked in {1}.", (object) FinPeriodIDFormattingAttribute.FormatForError(period.FinPeriodID), (object) string.Join(", ", (IEnumerable<string>) stringSet));
  }

  private string GetSubledgerTitle(string subledgerPrefix)
  {
    return BatchModule.GetDisplayName(subledgerPrefix);
  }

  private string GetSubledgerTitle<TSubledgerConst>() where TSubledgerConst : IConstant<string>, IBqlOperand, new()
  {
    return BatchModule.GetDisplayName<TSubledgerConst>();
  }

  protected virtual HashSet<string> GetSubledgerTitles(List<FinPeriod> finPeriods)
  {
    return this.SubledgerClosingFlags.Where<(string, Type)>((Func<(string, Type), bool>) (tuple => finPeriods.Any<FinPeriod>((Func<FinPeriod, bool>) (period => !((bool?) ((PXCache) GraphHelper.Caches<FinPeriod>((PXGraph) this)).GetValue((object) period, tuple.closingFlag.Name)).GetValueOrDefault())))).Select<(string, Type), string>((Func<(string, Type), string>) (tuple => this.GetSubledgerTitle(tuple.subledgerPrefix))).ToHashSet<string>();
  }

  protected virtual bool ConfirmProcessing(List<FinPeriod> finPeriods)
  {
    if (GraphExtensionMethods.IsScheduler((PXGraph) this))
      return true;
    switch (((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current.Action)
    {
      case "Close":
        HashSet<string> subledgerTitles = this.GetSubledgerTitles(finPeriods);
        if (subledgerTitles.Any<string>())
          return ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Ask("ConfirmProcessingKey", "Close Periods", string.Format(PXMessages.LocalizeNoPrefix("The selected period or periods will be closed in {0} and {1}. To proceed, click OK."), (object) string.Join(", ", (IEnumerable<string>) subledgerTitles), (object) this.GetSubledgerTitle<BatchModule.moduleGL>()), (MessageButtons) 1) == 1;
        break;
    }
    return true;
  }

  protected virtual void ProcessStatus(List<FinPeriod> finPeriods)
  {
    FinPeriodStatusProcess.FinPeriodStatusProcessParameters current = ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) this.Filter).Current;
    bool? reopenInSubledgers;
    if (current.Action == "Reopen")
    {
      reopenInSubledgers = current.ReopenInSubledgers;
      if (reopenInSubledgers.GetValueOrDefault())
      {
        foreach (PXResult<FinPeriod> pxResult in PXSelectBase<FinPeriod, PXSelect<FinPeriod, Where<FinPeriod.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>, And<Where<FinPeriod.aPClosed, Equal<True>, Or<FinPeriod.aRClosed, Equal<True>, Or<FinPeriod.cAClosed, Equal<True>, Or<FinPeriod.fAClosed, Equal<True>, Or<FinPeriod.iNClosed, Equal<True>>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) finPeriods.Last<FinPeriod>().FinPeriodID
        }))
          this.ReopenFinPeriodInSubledgers(PXResult<FinPeriod>.op_Implicit(pxResult));
      }
    }
    string applicableStatus = FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.GetApplicableStatus(current.Action);
    foreach (FinPeriod finPeriod in finPeriods)
    {
      PXProcessing.SetCurrentItem((object) finPeriod);
      switch (current.Action)
      {
        case "Close":
          this.CloseFinPeriodInSubledgers(finPeriod);
          break;
        case "Lock":
          this.VerifyFinPeriodForLock(finPeriod);
          break;
        case "Reopen":
          reopenInSubledgers = current.ReopenInSubledgers;
          if (reopenInSubledgers.GetValueOrDefault())
          {
            this.ReopenFinPeriodInSubledgers(finPeriod);
            break;
          }
          break;
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
      {
        foreach (PXResult<OrganizationFinPeriod> pxResult in ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Select(new object[1]
        {
          (object) finPeriod.FinPeriodID
        }))
        {
          OrganizationFinPeriod period = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult);
          this.AdditionalOrganizationFinPeriodProcessing(current.Action, (IFinPeriod) period);
          period.Status = applicableStatus;
          ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Update(period);
        }
      }
      else
        this.AdditionalOrganizationFinPeriodProcessing(current.Action, (IFinPeriod) finPeriod);
      finPeriod.Status = applicableStatus;
      ((PXSelectBase<FinPeriod>) this.FinPeriods).Update(finPeriod);
      PXProcessing.SetProcessed();
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected virtual void CreateAutoreverseBatches(IFinPeriod period)
  {
    PostGraph instance = PXGraph.CreateInstance<PostGraph>();
    foreach (PXResult<Batch> pxResult in PXSelectBase<Batch, PXViewOf<Batch>.BasedOn<SelectFromBase<Batch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Branch>.On<BqlOperand<Batch.branchID, IBqlInt>.IsEqual<Branch.branchID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.finPeriodID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.organizationID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.autoReverse, Equal<True>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<True>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) period.FinPeriodID,
      (object) period.OrganizationID
    }))
    {
      Batch b1 = PXResult<Batch>.op_Implicit(pxResult);
      if (!((IQueryable<PXResult<Batch>>) PXSelectBase<Batch, PXViewOf<Batch>.BasedOn<SelectFromBase<Batch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.origModule, Equal<P.AsString>>>>, And<BqlOperand<Batch.origBatchNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<Batch.autoReverseCopy, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) b1.Module,
        (object) b1.BatchNbr
      })).Any<PXResult<Batch>>())
      {
        ((PXGraph) instance).Clear();
        Batch b2 = instance.ReverseBatchProc(b1);
        b2.Status = "U";
        instance.ReleaseBatchProc(b2);
        if (((PXSelectBase<GLSetup>) this.glsetup).Current.AutoPostOption.GetValueOrDefault())
          instance.PostBatchProc(b2);
      }
    }
  }

  protected static BqlCommand OpenBatchesQuery { get; } = PXSelectBase<Batch, PXSelectJoin<Batch, InnerJoin<Branch, On<Batch.branchID, Equal<Branch.branchID>>, LeftJoin<GLTran, On<Batch.module, Equal<GLTran.module>, And<Batch.batchNbr, Equal<GLTran.batchNbr>>>, LeftJoin<TranBranch, On<GLTran.branchID, Equal<TranBranch.branchID>>, LeftJoin<GLTranDoc, On<Batch.batchNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleGL>>>>>>>, Where2<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<Batch.finPeriodID, Branch.organizationID>, Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<GLTran.finPeriodID, TranBranch.organizationID>>>, And<Where<Batch.posted, NotEqual<True>, And<Batch.released, Equal<True>, Or<Batch.released, NotEqual<True>, And<Batch.scheduled, NotEqual<True>, And<Batch.rejected, NotEqual<True>, And<Batch.voided, NotEqual<True>>>>>>>>>>.Config>.GetCommand();

  protected virtual FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules { get; } = new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[1]
  {
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "GL656100",
      ErrorMessage = "The {0} financial period cannot be closed because at least one unposted batch exists for this period.",
      CheckCommand = FinPeriodStatusProcess.OpenBatchesQuery
    }
  };

  protected virtual void VerifyOpenBatches(IFinPeriod finPeriod)
  {
    FinPeriodStatusProcess.MarkProcessedRowAsErrorAndStop(this.CheckOpenBatches(finPeriod));
  }

  protected virtual ProcessingResult CheckOpenBatches(IFinPeriod finPeriod)
  {
    ProcessingResult processingResult = new ProcessingResult();
    FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters objectsQueryParameters = new FinPeriodClosingProcessBase.UnprocessedObjectsQueryParameters()
    {
      OrganizationID = finPeriod.OrganizationID,
      FromFinPeriodID = finPeriod.FinPeriodID,
      ToFinPeriodID = finPeriod.FinPeriodID
    };
    foreach (FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule checkingRule in this.CheckingRules)
    {
      PXResult result = (PXResult) new PXView((PXGraph) this, true, checkingRule.CheckCommand).SelectSingleBound(new object[1]
      {
        (object) objectsQueryParameters
      }, Array.Empty<object>());
      if (result != null)
      {
        List<object> list = ((IEnumerable<Type>) checkingRule.MessageParameters).Select<Type, object>((Func<Type, object>) (param => ((PXGraph) this).Caches[BqlCommand.GetItemType(param)].GetStateExt((object) PXResult.Unwrap(result[BqlCommand.GetItemType(param)], BqlCommand.GetItemType(param)), param.Name))).ToList<object>();
        list.Add((object) PeriodIDAttribute.FormatForError(finPeriod.FinPeriodID));
        processingResult.AddErrorMessage(checkingRule.ErrorMessage, list.ToArray());
      }
    }
    return processingResult;
  }

  protected virtual bool IsUnclosablePeriod(FinPeriod finPeriod)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
      return !this.CheckOpenBatches((IFinPeriod) finPeriod).IsSuccess;
    return GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationFinPeriods).Select(new object[1]
    {
      (object) finPeriod.FinPeriodID
    })).Any<OrganizationFinPeriod>((Func<OrganizationFinPeriod, bool>) (orgFinPeriod => !this.CheckOpenBatches((IFinPeriod) orgFinPeriod).IsSuccess));
  }

  protected virtual void AdditionalOrganizationFinPeriodProcessing(string action, IFinPeriod period)
  {
    switch (action)
    {
      case "Close":
        this.CreateAutoreverseBatches(period);
        this.VerifyOpenBatches(period);
        break;
      case "Open":
        break;
      case "Lock":
        break;
      case "Deactivate":
        break;
      case "Reopen":
        break;
      default:
        int num = action == "Unlock" ? 1 : 0;
        break;
    }
  }

  public static void MarkProcessedRowAsErrorAndStop(string errorMessage, params object[] parameters)
  {
    PXException pxException = new PXException(errorMessage, parameters);
    PXProcessing.SetError((Exception) pxException);
    throw pxException;
  }

  public static void MarkProcessedRowAsErrorAndStop(ProcessingResult errors)
  {
    if (errors.IsSuccess)
      return;
    FinPeriodStatusProcess.MarkProcessedRowAsErrorAndStop(errors.GeneralMessage);
  }

  [Serializable]
  public class FinPeriodStatusProcessParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Organization(true, typeof (Switch<Case<Where<Not<FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>>>, OrganizationOfBranch<Current<AccessInfo.branchID>>>>))]
    public virtual int? OrganizationID { get; set; }

    [PXInt]
    [PXFormula(typeof (IIf<Where<FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>>, FinPeriod.organizationID.masterValue, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.organizationID>))]
    public virtual int? FilterOrganizationID { get; set; }

    [PXString(10)]
    [PXUIField(DisplayName = "Action")]
    [FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.FullList]
    [PXDefault("Undefined")]
    public virtual string Action { get; set; }

    [PXString(8)]
    [PXFormula(typeof (Switch<Case<Where<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.open>>, FinPeriod.status.inactive, Case<Where<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.deactivate>, Or<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.close>>>, FinPeriod.status.open, Case<Where<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.reopen>, Or<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.@lock>>>, FinPeriod.status.closed, Case<Where<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.unlock>>, FinPeriod.status.locked>>>>>))]
    public virtual string AffectedStatus { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIVisible(typeof (Where<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>, Equal<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.reopen>>))]
    [PXUIField(DisplayName = "Reopen Financial Periods in All Modules")]
    public virtual bool? ReopenInSubledgers { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (Default<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>))]
    [PXDefault(typeof (Search2<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, InnerJoin<FinPeriod, On<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<FinPeriod.organizationID>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Equal<FinPeriod.finYear>>>>, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And<FinPeriod.status, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.affectedStatus>>>>, OrderBy<Asc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>))]
    public virtual string FirstYear { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (Default<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>))]
    [PXDefault(typeof (Search2<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, InnerJoin<FinPeriod, On<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<FinPeriod.organizationID>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Equal<FinPeriod.finYear>>>>, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And<FinPeriod.status, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.affectedStatus>>>>, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>))]
    public virtual string LastYear { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (IIf2<Where<IsDirectFinPeriodAction<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>, Equal<True>>, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>))]
    [PXUIEnabled(typeof (Where<IsDirectFinPeriodAction<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>, NotEqual<True>>))]
    [PXUIField(DisplayName = "From Year", Required = true)]
    [PXDefault]
    [PXSelector(typeof (Search<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And2<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, GreaterEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear>>, Or<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear>, IsNull>>, And<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, LessEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>>, Or<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>, IsNull>>>>>>))]
    public virtual string FromYear { get; set; }

    [PXString(4, IsFixed = true)]
    [PXFormula(typeof (IIf2<Where<IsDirectFinPeriodAction<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>, Equal<True>>, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear, FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>))]
    [PXUIEnabled(typeof (Where<IsDirectFinPeriodAction<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>, Equal<True>>))]
    [PXUIField(DisplayName = "To Year", Required = true)]
    [PXDefault]
    [PXSelector(typeof (Search<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>>, And2<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, GreaterEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear>>, Or<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear>, IsNull>>, And<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, LessEqual<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>>, Or<Current<FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>, IsNull>>>>>>))]
    public virtual string ToYear { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.organizationID>
    {
    }

    public abstract class filterOrganizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.filterOrganizationID>
    {
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action>
    {
      public const string UnknownActionMessage = "Unknown financial period action";
      public const string Undefined = "Undefined";
      public const string Open = "Open";
      public const string Close = "Close";
      public const string Lock = "Lock";
      public const string Deactivate = "Deactivate";
      public const string Reopen = "Reopen";
      public const string Unlock = "Unlock";

      public static FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction GetDirection(
        string action)
      {
        switch (action)
        {
          case "Open":
          case "Close":
          case "Lock":
            return FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction.Direct;
          case "Unlock":
          case "Reopen":
          case "Deactivate":
            return FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction.Reverse;
          default:
            throw new PXException("Unknown financial period action");
        }
      }

      public static string GetApplicableStatus(string action)
      {
        switch (action)
        {
          case "Deactivate":
            return "Inactive";
          case "Open":
          case "Reopen":
            return "Open";
          case "Close":
          case "Unlock":
            return "Closed";
          case "Lock":
            return "Locked";
          default:
            throw new PXException("Unknown financial period action");
        }
      }

      public class RestrictedListAttribute : PXStringListAttribute
      {
        public RestrictedListAttribute()
          : base(new string[5]
          {
            "Undefined",
            "Open",
            "Close",
            "Lock",
            "Deactivate"
          }, new string[5]
          {
            "<SELECT>",
            "Open",
            "Close",
            "Lock",
            "Deactivate"
          })
        {
        }
      }

      public class FullListAttribute : PXStringListAttribute
      {
        public FullListAttribute()
          : base(new string[7]
          {
            "Undefined",
            "Open",
            "Close",
            "Lock",
            "Deactivate",
            "Reopen",
            "Unlock"
          }, new string[7]
          {
            "<SELECT>",
            "Open",
            "Close",
            "Lock",
            "Deactivate",
            "Reopen",
            "Unlock"
          })
        {
        }
      }

      public class undefined : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.undefined>
      {
        public undefined()
          : base("Undefined")
        {
        }
      }

      public class open : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.open>
      {
        public open()
          : base("Open")
        {
        }
      }

      public class close : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.close>
      {
        public close()
          : base("Close")
        {
        }
      }

      public class @lock : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.@lock>
      {
        public @lock()
          : base("Lock")
        {
        }
      }

      public class deactivate : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.deactivate>
      {
        public deactivate()
          : base("Deactivate")
        {
        }
      }

      public class reopen : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.reopen>
      {
        public reopen()
          : base("Reopen")
        {
        }
      }

      public class unlock : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.unlock>
      {
        public unlock()
          : base("Unlock")
        {
        }
      }

      public enum Direction
      {
        Direct,
        Reverse,
      }
    }

    public abstract class affectedStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.affectedStatus>
    {
    }

    public abstract class reopenInSubledgers : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.reopenInSubledgers>
    {
    }

    public abstract class firstYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.firstYear>
    {
    }

    public abstract class lastYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.lastYear>
    {
    }

    public abstract class fromYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.fromYear>
    {
    }

    public abstract class toYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.FinPeriodStatusProcessParameters.toYear>
    {
    }
  }

  [PXHidden]
  [Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2025R1.")]
  [Serializable]
  public class AutoReverseBatch : Batch
  {
    public new abstract class origBatchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.AutoReverseBatch.origBatchNbr>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FinPeriodStatusProcess.AutoReverseBatch.origModule>
    {
    }

    public new abstract class autoReverseCopy : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      FinPeriodStatusProcess.AutoReverseBatch.autoReverseCopy>
    {
    }
  }
}
