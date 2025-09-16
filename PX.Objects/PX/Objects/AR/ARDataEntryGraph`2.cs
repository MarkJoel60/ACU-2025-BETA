// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDataEntryGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Async;
using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Reports;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.AR;

public class ARDataEntryGraph<TGraph, TPrimary> : 
  PXGraph<
  #nullable disable
  TGraph, TPrimary>,
  IVoucherEntry,
  IActionsMenuGraph
  where TGraph : PXGraph
  where TPrimary : ARRegister, new()
{
  public PXInitializeState<TPrimary> initializeState;
  public PXAction<TPrimary> putOnHold;
  public PXAction<TPrimary> releaseFromHold;
  public PXAction<TPrimary> printAREdit;
  public PXAction<TPrimary> printARRegister;
  private readonly FinDocCopyPasteHelper CopyPasteHelper;
  public PXAction<TPrimary> release;
  public PXAction<TPrimary> voidCheck;
  public PXAction<TPrimary> viewBatch;
  public PXAction<TPrimary> action;
  public PXAction<TPrimary> inquiry;
  public PXAction<TPrimary> report;

  [InjectDependency]
  protected IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  protected Func<string, ReportNotificationGenerator> ReportNotificationGeneratorFactory { get; private set; }

  public PXAction ActionsMenuItem => (PXAction) this.action;

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintAREdit(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AR610500");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintARRegister(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AR622000");
  }

  public PXAction DeleteButton => (PXAction) this.Delete;

  public ARDataEntryGraph()
  {
    this.CopyPasteHelper = new FinDocCopyPasteHelper((PXGraph) this);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(ARDataEntryGraph<TGraph, TPrimary>.\u003C\u003Ec.\u003C\u003E9__22_0 ?? (ARDataEntryGraph<TGraph, TPrimary>.\u003C\u003Ec.\u003C\u003E9__22_0 = new PXFieldDefaulting((object) ARDataEntryGraph<TGraph, TPrimary>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__22_0))));
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable Release(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable VoidCheck(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    foreach (TPrimary primary in adapter.Get<TPrimary>())
    {
      if (!string.IsNullOrEmpty(primary.BatchNbr))
      {
        JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
        ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<BatchModule.moduleAR>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) primary.BatchNbr
        }));
        throw new PXRedirectRequiredException((PXGraph) instance, "Current batch record");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  protected virtual IEnumerable Action(PXAdapter adapter, [PXString] string ActionName)
  {
    if (!string.IsNullOrEmpty(ActionName))
    {
      PXAction action = ((PXGraph) this).Actions[ActionName];
      if (action != null)
      {
        List<object> objectList1 = new List<object>();
        foreach (object obj in adapter.Get())
          objectList1.Add(obj);
        ((PXAction) this.Save).Press();
        List<object> objectList2 = new List<object>();
        foreach (object obj in action.Press(new PXAdapter((PXView) new PXView.Dummy((PXGraph) this, adapter.View.BqlSelect, objectList1))
        {
          MassProcess = adapter.MassProcess
        }))
          objectList2.Add(obj);
        return (IEnumerable) objectList2;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Inquiry(PXAdapter adapter, [PXString] string ActionName)
  {
    if (!string.IsNullOrEmpty(ActionName))
    {
      PXAction action = ((PXGraph) this).Actions[ActionName];
      if (action != null)
      {
        ((PXAction) this.Save).Press();
        foreach (object obj in action.Press(adapter))
          ;
      }
    }
    return adapter.Get();
  }

  public virtual Dictionary<string, string> PrepareReportParams(string reportID, TPrimary doc)
  {
    Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
    string[] source = new string[2]
    {
      "AR610500",
      "AR622000"
    };
    PX.Reports.Controls.Report report = this.ReportLoader.LoadReport(reportID, (IPXResultset) null);
    if (report == null)
      throw new PXException("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
      {
        (object) reportID
      });
    if (((IEnumerable<string>) source).Contains<string>(reportID))
    {
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>()
      {
        ["DocType"] = doc.DocType,
        ["RefNbr"] = doc.RefNbr,
        ["OrgBAccountID"] = PXAccess.GetBranchCD(doc.BranchID)
      };
      foreach (ReportParameter parameter in (List<ReportParameter>) report.Parameters)
      {
        string str = (string) null;
        bool flag = dictionary2.TryGetValue(parameter.Name, out str);
        if (!flag && parameter.Nullable)
          dictionary1[parameter.Name] = (string) null;
        else if (flag)
          dictionary1[parameter.Name] = str;
      }
    }
    else
    {
      string name = ((object) doc).GetType().Name;
      Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
      dictionary3[name + ".DocType"] = doc.DocType;
      dictionary3[name + ".RefNbr"] = doc.RefNbr;
      foreach (FilterExp filter in (List<FilterExp>) report.Filters)
      {
        string str;
        if (dictionary3.TryGetValue(filter.DataField, out str))
          dictionary1[filter.DataField] = str;
      }
    }
    return dictionary1;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    PXReportRequiredException ex = (PXReportRequiredException) null;
    Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
    PXProcessing<TPrimary>.ProcessRecords(adapter.Get<TPrimary>(), adapter.MassProcess, (System.Action<TPrimary>) (doc =>
    {
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (TPrimary)], (object) doc);
      Dictionary<string, string> dictionary = this.PrepareReportParams(reportID, doc);
      string customerReportId = this.GetCustomerReportID(reportID, doc);
      ex = PXReportRequiredException.CombineReport(ex, customerReportId, dictionary, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
      reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, dictionary, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, customerReportId, doc.BranchID, (CurrentLocalization) null);
    }), (System.Action<TPrimary>) null, (Func<TPrimary, Exception, bool, bool?>) null, (System.Action<TPrimary>) null, (System.Action<TPrimary>) null);
    ((PXAction) this.Save).Press();
    if (ex != null)
    {
      int num;
      ((ILongOperationManager) ((PXGraph) this).LongOperationManager).StartAsyncOperation((object) Guid.NewGuid(), (Func<CancellationToken, System.Threading.Tasks.Task>) (async ct => num = await SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct) ? 1 : 0));
      throw ex;
    }
    return adapter.Get();
  }

  public virtual string GetCustomerReportID(string reportID, TPrimary doc) => reportID;

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    this.CopyPasteHelper.SetBranchFieldCommandToTheTop(script);
  }

  protected virtual void AssertOnDelete(PX.Data.Events.RowPersisting<TPrimary> e)
  {
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TPrimary>>) e).Cache.GetStatus((object) e.Row) == 3 && (e.Row.Released.GetValueOrDefault() || e.Row.Voided.GetValueOrDefault()))
      throw new PXInvalidOperationException("The {0} document with the {1} ref. number is released and cannot be deleted.", new object[2]
      {
        (object) e.Row.DocType,
        (object) e.Row.RefNbr
      });
  }
}
