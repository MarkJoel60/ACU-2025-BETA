// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionReview
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARSPCommissionReview : PXGraph<ARSPCommissionProcess>
{
  public PXCancel<ARSPCommissionPeriod> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<ARSPCommissionPeriod> EditDetail;
  public PXFirst<ARSPCommissionPeriod> First;
  public PXPrevious<ARSPCommissionPeriod> Previous;
  public PXNext<ARSPCommissionPeriod> Next;
  public PXLast<ARSPCommissionPeriod> Last;
  public PXAction<ARSPCommissionPeriod> actionsFolder;
  public PXAction<ARSPCommissionPeriod> VoidCommissions;
  public PXAction<ARSPCommissionPeriod> ClosePeriod;
  public PXAction<ARSPCommissionPeriod> ReopenPeriod;
  public PXSelect<ARSPCommissionPeriod> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<ARSPCommnHistory> ToProcess;
  private string[] postmessages;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;

  public ARSPCommissionReview()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    PXCache cache = ((PXSelectBase) this.ToProcess).Cache;
    PXUIFieldAttribute.SetEnabled(cache, (string) null, false);
    cache.AllowInsert = false;
    cache.AllowDelete = false;
    cache.AllowUpdate = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Filter).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<ARSPCommissionPeriod.commnPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, true);
    ((PXSelectBase) this.Filter).Cache.AllowInsert = false;
    ((PXSelectBase) this.Filter).Cache.AllowDelete = false;
    ((PXAction) this.actionsFolder).MenuAutoOpen = true;
    ((PXAction) this.actionsFolder).AddMenuAction((PXAction) this.VoidCommissions);
    ((PXAction) this.actionsFolder).AddMenuAction((PXAction) this.ClosePeriod);
    ((PXAction) this.actionsFolder).AddMenuAction((PXAction) this.ReopenPeriod);
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSPCommnHistory>) this.ToProcess).Current != null && ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current != null)
    {
      ARSPCommissionPeriod current1 = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
      ARSPCommnHistory current2 = ((PXSelectBase<ARSPCommnHistory>) this.ToProcess).Current;
      ARSPCommissionDocEnq instance = PXGraph.CreateInstance<ARSPCommissionDocEnq>();
      SPDocFilter current3 = ((PXSelectBase<SPDocFilter>) instance.Filter).Current;
      current3.SalesPersonID = current2.SalesPersonID;
      current3.CommnPeriod = current1.CommnPeriodID;
      ((PXSelectBase<SPDocFilter>) instance.Filter).Update(current3);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual IEnumerable toprocess()
  {
    ARSPCommissionReview commissionReview = this;
    foreach (PXResult<ARSPCommnHistory> pxResult in ((PXSelectBase<ARSPCommnHistory>) new PXSelectGroupBy<ARSPCommnHistory, Where<ARSPCommnHistory.commnPeriod, Equal<Current<ARSPCommissionPeriod.commnPeriodID>>>, Aggregate<GroupBy<ARSPCommnHistory.salesPersonID, GroupBy<ARSPCommnHistory.baseCuryID, Sum<ARSPCommnHistory.commnblAmt, Sum<ARSPCommnHistory.commnAmt, Max<ARSPCommnHistory.pRProcessedDate>>>>>>>((PXGraph) commissionReview)).Select(Array.Empty<object>()))
    {
      ARSPCommnHistory arspCommnHistory = PXResult<ARSPCommnHistory>.op_Implicit(pxResult);
      EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.salesPersonID, Equal<Required<ARSPCommnHistory.salesPersonID>>>>.Config>.Select((PXGraph) commissionReview, new object[1]
      {
        (object) arspCommnHistory.SalesPersonID
      }));
      arspCommnHistory.Type = epEmployee != null ? "EP" : "VE";
      yield return (object) arspCommnHistory;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ActionsFolder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable voidCommissions(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARSPCommissionReview.\u003C\u003Ec__DisplayClass18_0 cDisplayClass180 = new ARSPCommissionReview.\u003C\u003Ec__DisplayClass18_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (ARSPCommnHistory)];
    if (PXLongOperation.GetStatus(((PXGraph) this).UID) == 1)
      throw new ApplicationException("The previous operation has not been completed.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.processPeriod = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass180.processPeriod != null)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass180, __methodptr(\u003CvoidCommissions\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable closePeriod(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARSPCommissionReview.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new ARSPCommissionReview.\u003C\u003Ec__DisplayClass19_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (ARSPCommnHistory)];
    if (PXLongOperation.GetStatus(((PXGraph) this).UID) == 1)
      throw new ApplicationException("The previous operation has not been completed.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.processPeriod = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass190.processPeriod != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (PXResultset<ARSPCommissionPeriod>.op_Implicit(PXSelectBase<ARSPCommissionPeriod, PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, Less<Required<ARSPCommissionPeriod.commnPeriodID>>, And<ARSPCommissionPeriod.status, NotEqual<ARSPCommissionPeriodStatus.closed>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) cDisplayClass190.processPeriod.CommnPeriodID
      })) != null)
        throw new ApplicationException("This Commission Period cannot be closed - all the previous commission periods must be closed first");
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass190, __methodptr(\u003CclosePeriod\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable reopenPeriod(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARSPCommissionReview.\u003C\u003Ec__DisplayClass20_0 cDisplayClass200 = new ARSPCommissionReview.\u003C\u003Ec__DisplayClass20_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (ARSPCommnHistory)];
    if (PXLongOperation.GetStatus(((PXGraph) this).UID) == 1)
      throw new ApplicationException("The previous operation has not been completed.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass200.processPeriod = ((PXSelectBase<ARSPCommissionPeriod>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass200.processPeriod != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (PXResultset<ARSPCommissionPeriod>.op_Implicit(PXSelectBase<ARSPCommissionPeriod, PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, Greater<Required<ARSPCommissionPeriod.commnPeriodID>>, And<ARSPCommissionPeriod.status, NotEqual<ARSPCommissionPeriodStatus.open>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) cDisplayClass200.processPeriod.CommnPeriodID
      })) != null)
        throw new ApplicationException("This Commission Period cannot be reopened - there are closed commission periods after it");
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass200, __methodptr(\u003CreopenPeriod\u003Eb__0)));
    }
    return adapter.Get();
  }

  private static void VoidCommissionsProc(ARSPCommissionPeriod aProcessPeriod)
  {
    string[] strArray = new string[1];
    PXLongOperation.SetCustomInfo((object) strArray);
    try
    {
      PXGraph.CreateInstance<ARSPCommissionUpdate>().VoidReportProc(aProcessPeriod);
    }
    catch (Exception ex)
    {
      strArray[0] = ex.Message;
      throw new ApplicationException("Voiding commissions for the selected period has failed");
    }
  }

  private static void CloseCommnPeriod(ARSPCommissionPeriod aProcessPeriod)
  {
    string[] strArray = new string[1];
    PXLongOperation.SetCustomInfo((object) strArray);
    try
    {
      PXGraph.CreateInstance<ARSPCommissionUpdate>().ClosePeriodProc(aProcessPeriod);
    }
    catch (Exception ex)
    {
      strArray[0] = ex.Message;
      throw new ApplicationException("Closing commission period has failed");
    }
  }

  private static void ReopenCommnPeriod(ARSPCommissionPeriod aProcessPeriod)
  {
    string[] strArray = new string[1];
    PXLongOperation.SetCustomInfo((object) strArray);
    try
    {
      PXGraph.CreateInstance<ARSPCommissionUpdate>().ReopenPeriodProc(aProcessPeriod);
    }
    catch (Exception ex)
    {
      strArray[0] = ex.Message;
      throw new ApplicationException("Reopening commission period has failed");
    }
  }

  protected virtual void ARSPCommissionPeriod_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    ARSPCommissionPeriod row = (ARSPCommissionPeriod) e.Row;
    bool flag1 = ((ARSPCommissionPeriod) e.Row).Status == "P";
    bool flag2 = ((ARSPCommissionPeriod) e.Row).Status == "C";
    bool flag3 = PXResultset<ARSPCommissionPeriod>.op_Implicit(PXSelectBase<ARSPCommissionPeriod, PXSelect<ARSPCommissionPeriod, Where<ARSPCommissionPeriod.commnPeriodID, Greater<Required<ARSPCommissionPeriod.commnPeriodID>>, And<ARSPCommissionPeriod.status, NotEqual<ARSPCommissionPeriodStatus.open>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.CommnPeriodID
    })) == null;
    ((PXAction) this.VoidCommissions).SetEnabled(flag1);
    ((PXAction) this.ClosePeriod).SetEnabled(flag1);
    ((PXAction) this.ReopenPeriod).SetEnabled(flag2 & flag3);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Currency")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARSPCommnHistory.baseCuryID> e)
  {
  }

  private List<ARSPCommnHistory> getSelected() => this.getAll();

  private List<ARSPCommnHistory> getAll()
  {
    List<ARSPCommnHistory> all = new List<ARSPCommnHistory>();
    foreach (PXResult<ARSPCommnHistory> pxResult in ((PXSelectBase<ARSPCommnHistory>) this.ToProcess).Select(Array.Empty<object>()))
    {
      ARSPCommnHistory arspCommnHistory = PXResult<ARSPCommnHistory>.op_Implicit(pxResult);
      all.Add(arspCommnHistory);
    }
    return all;
  }

  public delegate void DoProcess(ARSPCommissionPeriod aCommnPeriod);
}
