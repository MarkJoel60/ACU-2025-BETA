// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL.DAC;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.GraphBaseExtensions;
using PX.Objects.GL.JournalEntryState;
using PX.Objects.GL.JournalEntryState.PartiallyEditable;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.GL;

public class JournalEntry : 
  PXGraph<
  #nullable disable
  JournalEntry, Batch>,
  PXImportAttribute.IPXPrepareItems,
  IVoucherEntry
{
  public ToggleCurrency<Batch> CurrencyView;
  [PXViewName("GL Batch")]
  public PXSelect<Batch, Where<Batch.module, Equal<Optional<Batch.module>>, And<Batch.draft, Equal<False>>>> BatchModule;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<Batch.curyInfoID>>>> currencyinfo;
  [PXImport(typeof (Batch))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (GLTran.reclassified), typeof (GLTran.isReclassReverse), typeof (GLTran.reclassificationProhibited), typeof (GLTran.reclassBatchModule), typeof (GLTran.reclassBatchNbr), typeof (GLTran.reclassSourceTranModule), typeof (GLTran.reclassSourceTranBatchNbr), typeof (GLTran.reclassSourceTranLineNbr), typeof (GLTran.origModule), typeof (GLTran.origBatchNbr), typeof (GLTran.origLineNbr), typeof (GLTran.curyReclassRemainingAmt), typeof (GLTran.reclassRemainingAmt), typeof (GLTran.reclassOrigTranDate), typeof (GLTran.reclassTotalCount), typeof (GLTran.reclassReleasedCount)})]
  [PXViewName("GL Transaction")]
  public PXSelect<GLTran, Where<GLTran.module, Equal<Current<Batch.module>>, And<GLTran.batchNbr, Equal<Current<Batch.batchNbr>>>>> GLTranModuleBatNbr;
  [PXViewName("GL Account")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<Account, InnerJoin<GLTran, On<GLTran.accountID, Equal<Account.accountID>>>, Where<GLTran.module, Equal<Current<Batch.module>>, And<GLTran.batchNbr, Equal<Current<Batch.batchNbr>>>>> Accounts;
  public PXSelect<GLAllocationHistory, Where<GLAllocationHistory.batchNbr, Equal<Current<Batch.batchNbr>>, And<GLAllocationHistory.module, Equal<Current<Batch.module>>>>> AllocationHistory;
  public PXSelect<GLAllocationAccountHistory, Where<GLAllocationAccountHistory.batchNbr, Equal<Current<Batch.batchNbr>>, And<GLAllocationAccountHistory.module, Equal<Current<Batch.module>>>>> AllocationAccountHistory;
  public PXSelect<CATran> catran;
  public PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<Batch.finPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<Batch.branchID>>>>> finperiod;
  public PXSetup<Branch, Where<Branch.branchID, Equal<Optional<Batch.branchID>>>> branch;
  public PXSetup<Company> company;
  public PXSelect<Sub> ViewSub;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSelect<GLSetupApproval, Where<GLSetupApproval.batchType, Equal<BqlField<
  #nullable enable
  Batch.batchType, IBqlString>.FromCurrent>, 
  #nullable disable
  And<Where<BqlField<
  #nullable enable
  Batch.origModule, IBqlString>.FromCurrent, 
  #nullable disable
  IsNull, Or<BqlField<
  #nullable enable
  Batch.origModule, IBqlString>.FromCurrent, 
  #nullable disable
  NotEqual<PX.Objects.GL.BatchModule.moduleAM>, Or<BqlField<
  #nullable enable
  Batch.origBatchNbr, IBqlString>.FromCurrent, 
  #nullable disable
  IsNotNull>>>>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomation<Batch, Batch.approved, Batch.rejected, Batch.hold, GLSetupApproval> Approval;
  protected SummaryPostingController SummaryPostingController;
  protected Lazy<IEnumerable<PXResult<Branch, Ledger>>> ledgersByBranch;
  public PXSetup<GLSetup> glsetup;
  public CMSetupSelect CMSetup;
  protected PX.Objects.CM.CurrencyInfo _CurrencyInfo;
  public PXInitializeState<Batch> initializeState;
  public PXAction<Batch> putOnHold;
  public PXAction<Batch> releaseFromHold;
  public PXAction<Batch> batchRegisterDetails;
  public PXAction<Batch> glEditDetails;
  public PXAction<Batch> glReversingBatches;
  public PXAction<Batch> editReclassBatch;
  public PXAction<Batch> post;
  public PXAction<Batch> release;
  public PXAction<Batch> action;
  public PXAction<Batch> report;
  public PXAction<Batch> createSchedule;
  public PXAction<Batch> reverseBatch;
  public PXAction<Batch> reclassify;
  public PXAction<Batch> reclassificationHistory;
  public PXAction<Batch> viewDocument;
  public PXAction<Batch> viewOrigBatch;
  public PXAction<Batch> ViewReclassBatch;
  public PXWorkflowEventHandler<Batch> OnConfirmSchedule;
  public PXWorkflowEventHandler<Batch> OnVoidSchedule;
  public PXWorkflowEventHandler<Batch> OnReleaseBatch;
  public PXWorkflowEventHandler<Batch> OnPostBatch;
  public PXWorkflowEventHandler<Batch> OnUpdateStatus;
  protected bool _IsOffline;
  protected DocumentList<Batch> _created;
  private bool _importing;

  public PXAction DeleteButton => (PXAction) this.Delete;

  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<Selector<Current<Batch.ledgerID>, Ledger.balanceType>, Equal<LedgerBalanceType.actual>>, Selector<GLTran.branchID, Selector<Branch.ledgerID, Ledger.ledgerID>>>, Selector<Current<Batch.ledgerID>, Ledger.ledgerID>>))]
  [PXUIField(DisplayName = "Ledger", Enabled = false)]
  [PXSelector(typeof (Ledger.ledgerID), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  public virtual void GLTran_LedgerID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  public virtual void GLTran_ReclassType_CacheAttached(PXCache sender)
  {
  }

  public JournalEntry.Modes Mode { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  public static IEnumerable<Batch> GetReversingBatches(
    PXGraph graph,
    string module,
    string batchNbr)
  {
    return GraphHelper.RowCast<Batch>((IEnumerable) PXSelectBase<Batch, PXSelectReadonly<Batch, Where<Batch.origModule, Equal<Required<Batch.module>>, And<Batch.origBatchNbr, Equal<Required<Batch.batchNbr>>, And<Batch.autoReverseCopy, Equal<True>>>>>.Config>.Select(graph, new object[2]
    {
      (object) module,
      (object) batchNbr
    }));
  }

  public static IEnumerable<Batch> GetReleasedReversingBatches(
    PXGraph graph,
    string module,
    string batchNbr)
  {
    return GraphHelper.RowCast<Batch>((IEnumerable) PXSelectBase<Batch, PXSelectReadonly<Batch, Where<Batch.origModule, Equal<Required<Batch.module>>, And<Batch.origBatchNbr, Equal<Required<Batch.batchNbr>>, And<Batch.autoReverseCopy, Equal<True>, And<Batch.released, Equal<True>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) module,
      (object) batchNbr
    }));
  }

  public static Batch FindBatch(PXGraph graph, GLTran tran)
  {
    return JournalEntry.FindBatch(graph, tran.Module, tran.BatchNbr);
  }

  public static Batch FindBatch(PXGraph graph, string module, string batchNbr)
  {
    return PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) new PXSelect<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>(graph)).Select(new object[2]
    {
      (object) module,
      (object) batchNbr
    }));
  }

  public static GLTran FindTran(PXGraph graph, GLTranKey key)
  {
    return JournalEntry.FindTran(graph, key.Module, key.BatchNbr, key.LineNbr.Value);
  }

  public static GLTran GetTran(PXGraph graph, string module, string batchNbr, int lineNbr)
  {
    if (JournalEntry.FindTran(graph, module, batchNbr, lineNbr) == null)
      throw new PXException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) GLTran.GetImage(module, batchNbr, new int?(lineNbr))
      });
    return (GLTran) null;
  }

  public static GLTran FindTran(PXGraph graph, string module, string batchNbr, int lineNbr)
  {
    return PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, Equal<Required<GLTran.lineNbr>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) module,
      (object) batchNbr,
      (object) lineNbr
    }));
  }

  public static IEnumerable<GLTran> GetTrans(PXGraph graph, string module, string batchNbr)
  {
    return GraphHelper.RowCast<GLTran>((IEnumerable) PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) module,
      (object) batchNbr
    }));
  }

  public static IEnumerable<GLTran> GetTrans(
    PXGraph graph,
    string module,
    string batchNbr,
    int?[] lineNbrs)
  {
    return GraphHelper.RowCast<GLTran>((IEnumerable) PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Current<Batch.module>>, And<GLTran.batchNbr, Equal<Current<Batch.batchNbr>>, And<GLTran.lineNbr, In<Required<GLTran.lineNbr>>>>>>.Config>.Select(graph, new object[1]
    {
      (object) lineNbrs
    }));
  }

  protected IReadOnlyCollection<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>> GetTranCuryInfoNotInterCompany(
    string module,
    string batchNbr)
  {
    return (IReadOnlyCollection<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>>) ((IEnumerable) PXSelectBase<GLTran, PXSelectJoin<GLTran, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<GLTran.curyInfoID>>>, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.isInterCompany, Equal<False>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) module,
      (object) batchNbr
    })).ToArray<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>>();
  }

  public virtual void Clear(PXClearOption option)
  {
    if (((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).ContainsKey(typeof (PX.Objects.CM.CurrencyInfo)))
      ((PXGraph) this).Caches[typeof (PX.Objects.CM.CurrencyInfo)].ClearQueryCache();
    ((PXGraph) this).Clear(option);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public static void OpenDocumentByTran(GLTran tran, Batch batch)
  {
    PXGraph.CreateInstance<JournalEntry>().RedirectToDocumentByTran(tran, batch);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public virtual void RedirectToDocumentByTran(GLTran tran, Batch batch)
  {
    ((PXGraph) this).GetExtension<JournalEntry.RedirectToSourceDocumentFromJournalEntryExtension>().RedirectToSourceDocument(tran, batch);
  }

  public virtual IDocGraphCreator GetGraphCreator(string tranModule, string batchType)
  {
    if (tranModule != null && tranModule.Length == 2)
    {
      switch (tranModule[0])
      {
        case 'A':
          switch (tranModule)
          {
            case "AP":
              return (IDocGraphCreator) new APDocGraphCreator();
            case "AR":
              return (IDocGraphCreator) new ARDocGraphCreator();
          }
          break;
        case 'C':
          if (tranModule == "CA")
            return (IDocGraphCreator) new CADocGraphCreator();
          break;
        case 'D':
          if (tranModule == "DR")
            return (IDocGraphCreator) new DRDocGraphCreator();
          break;
        case 'F':
          if (tranModule == "FA")
            return (IDocGraphCreator) new FADocGraphCreator();
          break;
        case 'G':
          if (tranModule == "GL")
            return batchType == "T" ? (IDocGraphCreator) new JournalEntryImportGraphCreator() : (IDocGraphCreator) null;
          break;
        case 'I':
          if (tranModule == "IN")
            return (IDocGraphCreator) new INDocGraphCreator();
          break;
        case 'P':
          if (tranModule == "PM")
            return (IDocGraphCreator) new PMDocGraphCreator();
          break;
      }
    }
    return (IDocGraphCreator) null;
  }

  public bool AutoRevEntry
  {
    get => ((PXSelectBase<GLSetup>) this.glsetup).Current.AutoRevEntry.GetValueOrDefault();
  }

  public PX.Objects.CM.CurrencyInfo currencyInfo
  {
    get
    {
      return PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
    }
  }

  public OrganizationFinPeriod FINPERIOD
  {
    get
    {
      return PXResultset<OrganizationFinPeriod>.op_Implicit(((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Select(Array.Empty<object>()));
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable BatchRegisterDetails(PXAdapter adapter, string reportID = null)
  {
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null && ((PXSelectBase<Batch>) this.BatchModule).Current.Released.GetValueOrDefault())
      throw new PXReportRequiredException(this.CreateBatchRegisterDetailsReportParams(), reportID ?? "GL621000", "Batch Register Details", (CurrentLocalization) null);
    return adapter.Get();
  }

  private Dictionary<string, string> CreateBatchRegisterDetailsReportParams()
  {
    BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<Branch>) this.branch).Current.BAccountID
    }));
    string str = ((PXSelectBase<Batch>) this.BatchModule).Current.FinPeriodID.Substring(4, 2) + ((PXSelectBase<Batch>) this.BatchModule).Current.FinPeriodID.Substring(0, 4);
    return new Dictionary<string, string>()
    {
      ["LedgerID"] = Ledger.PK.Find((PXGraph) this, ((PXSelectBase<Batch>) this.BatchModule).Current.LedgerID)?.LedgerCD,
      ["OrgBAccountID"] = baccountR?.AcctCD,
      ["PeriodFrom"] = str,
      ["PeriodTo"] = str,
      ["Module"] = ((PXSelectBase<Batch>) this.BatchModule).Current.Module,
      ["Batch.BatchNbr"] = ((PXSelectBase<Batch>) this.BatchModule).Current.BatchNbr
    };
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable GLEditDetails(PXAdapter adapter, string reportID = null)
  {
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null)
    {
      bool? nullable = ((PXSelectBase<Batch>) this.BatchModule).Current.Released;
      if (!nullable.Value)
      {
        nullable = ((PXSelectBase<Batch>) this.BatchModule).Current.Posted;
        if (!nullable.Value)
          throw new PXReportRequiredException(this.CreateGLEditDetailsReportParams(), reportID ?? "GL610500", "GL Edit Details", (CurrentLocalization) null);
      }
    }
    return adapter.Get();
  }

  private Dictionary<string, string> CreateGLEditDetailsReportParams()
  {
    BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<Branch>) this.branch).Current.BAccountID
    }));
    string str = ((PXSelectBase<Batch>) this.BatchModule).Current.FinPeriodID.Substring(4, 2) + ((PXSelectBase<Batch>) this.BatchModule).Current.FinPeriodID.Substring(0, 4);
    return new Dictionary<string, string>()
    {
      ["LedgerID"] = Ledger.PK.Find((PXGraph) this, ((PXSelectBase<Batch>) this.BatchModule).Current.LedgerID)?.LedgerCD,
      ["OrgBAccountID"] = baccountR?.AcctCD,
      ["PeriodFrom"] = str,
      ["PeriodTo"] = str,
      ["Batch.BatchNbr"] = ((PXSelectBase<Batch>) this.BatchModule).Current.BatchNbr
    };
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable GLReversingBatches(PXAdapter adapter, string reportID)
  {
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["Module"] = ((PXSelectBase<Batch>) this.BatchModule).Current.Module,
        ["OrigBatchNbr"] = ((PXSelectBase<Batch>) this.BatchModule).Current.BatchNbr
      }, reportID ?? "GL690010", "GL Reversing Batches", (CurrentLocalization) null);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EditReclassBatch(PXAdapter adapter)
  {
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current != null)
      ReclassifyTransactionsProcess.OpenForReclassBatchEditing(current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Post(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    JournalEntry.\u003C\u003Ec__DisplayClass82_0 cDisplayClass820 = new JournalEntry.\u003C\u003Ec__DisplayClass82_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass820.list = new List<Batch>();
    foreach (Batch batch in adapter.Get())
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass820.list.Add(batch);
    }
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass820, __methodptr(\u003CPost\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass820.list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    JournalEntry.\u003C\u003Ec__DisplayClass84_0 cDisplayClass840 = new JournalEntry.\u003C\u003Ec__DisplayClass84_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (Batch)];
    // ISSUE: reference to a compiler-generated field
    cDisplayClass840.list = new List<Batch>();
    foreach (object obj in adapter.Get())
    {
      Batch batch;
      switch (obj)
      {
        case Batch _:
          batch = obj as Batch;
          break;
        case PXResult _:
          batch = PXResult<Batch>.op_Implicit(obj as PXResult<Batch>);
          break;
        default:
          batch = (Batch) obj;
          break;
      }
      if (batch.Status == "B")
      {
        cach.Update((object) batch);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass840.list.Add(batch);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass840.list.Count == 0)
      throw new PXException("Batch Status invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass840.list.Count > 0)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass840, __methodptr(\u003CRelease\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass840.list;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CreateSchedule(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null && !((PXSelectBase<Batch>) this.BatchModule).Current.Released.Value && !((PXSelectBase<Batch>) this.BatchModule).Current.Hold.Value)
    {
      ScheduleMaint instance1 = PXGraph.CreateInstance<ScheduleMaint>();
      if (((PXSelectBase<Batch>) this.BatchModule).Current.Scheduled.Value && ((PXSelectBase<Batch>) this.BatchModule).Current.ScheduleID != null)
      {
        ((PXSelectBase<Schedule>) instance1.Schedule_Header).Current = PXResultset<Schedule>.op_Implicit(PXSelectBase<Schedule, PXSelect<Schedule, Where<Schedule.scheduleID, Equal<Required<Schedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<Batch>) this.BatchModule).Current.ScheduleID
        }));
      }
      else
      {
        ((PXSelectBase) instance1.Schedule_Header).Cache.Insert((object) new Schedule());
        Batch instance2 = (Batch) ((PXSelectBase) instance1.Batch_Detail).Cache.CreateInstance();
        PXCache<Batch>.RestoreCopy(instance2, ((PXSelectBase<Batch>) this.BatchModule).Current);
        Batch batch = (Batch) ((PXSelectBase) instance1.Batch_Detail).Cache.Update((object) instance2);
      }
      throw new PXRedirectRequiredException((PXGraph) instance1, "Schedule");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReverseBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<Batch>) this.BatchModule).Current == null || !this.AskUserApprovalToReverseBatch(((PXSelectBase<Batch>) this.BatchModule).Current))
      return adapter.Get();
    if (((PXSelectBase<Batch>) this.BatchModule).Current.BatchType == "RCL")
      ReclassifyTransactionsProcess.OpenForReclassBatchReversing(((PXSelectBase<Batch>) this.BatchModule).Current);
    if (((PXSelectBase<Batch>) this.BatchModule).Current.Module != "GL" && ((PXSelectBase<Batch>) this.BatchModule).Current.Module != "CM")
    {
      if (((PXSelectBase<Batch>) this.BatchModule).Ask(((PXSelectBase<Batch>) this.BatchModule).Current, "Confirmation", PXMessages.LocalizeFormatNoPrefixNLA("The reversal of a batch that originated in the {0} module can lead to inconsistency between data in the modules. Are you sure you want to reverse the batch that originated in the {0} module?", new object[1]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<Batch.module>(((PXSelectBase) this.BatchModule).Cache, (object) ((PXSelectBase<Batch>) this.BatchModule).Current)
      }), (MessageButtons) 4, (MessageIcon) 2) != 6)
        return adapter.Get();
    }
    ((PXAction) this.Save).Press();
    try
    {
      Batch current1 = ((PXSelectBase<Batch>) this.BatchModule).Current;
      Batch copy = PXCache<Batch>.CreateCopy(current1);
      ((PXSelectBase) this.finperiod).Cache.Current = ((PXSelectBase) this.finperiod).View.SelectSingleBound(new object[1]
      {
        (object) copy
      }, Array.Empty<object>());
      this.ReverseBatchProc(copy);
      if (((PXSelectBase<Batch>) this.BatchModule).Current == null)
        return adapter.Get();
      Batch current2 = ((PXSelectBase<Batch>) this.BatchModule).Current;
      this.FinPeriodUtils.CopyPeriods<Batch, Batch.finPeriodID, Batch.tranPeriodID>(((PXSelectBase) this.BatchModule).Cache, current1, current2);
      return (IEnumerable) new List<Batch>() { current2 };
    }
    catch (PXException ex)
    {
      ((PXGraph) this).Clear();
      throw;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Reclassify(PXAdapter adapter)
  {
    ReclassifyTransactionsProcess.TryOpenForReclassification<GLTran>((PXGraph) this, (IEnumerable<GLTran>) this.GetFilteredTrans().ToArray<GLTran>(), Ledger.PK.Find((PXGraph) this, ((PXSelectBase<Batch>) this.BatchModule).Current.LedgerID), (Func<GLTran, string>) (tran => ((PXSelectBase<Batch>) this.BatchModule).Current.BatchType), ((PXSelectBase) this.BatchModule).View, "Some transactions of the batch cannot be reclassified. These transactions will not be loaded.", "No transactions, for which the reclassification can be performed, have been found in the batch.", (PXBaseRedirectException.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReclassificationHistory(PXAdapter adapter)
  {
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null && ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Current != null)
      ReclassificationHistoryInq.OpenForTransaction(((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Current != null)
    {
      GLTran current = ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Current;
      ((PXGraph) this).GetExtension<JournalEntry.RedirectToSourceDocumentFromJournalEntryExtension>().RedirectToSourceDocument(current, ((PXSelectBase<Batch>) this.BatchModule).Current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewOrigBatch(PXAdapter adapter)
  {
    GLTran current = ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Current;
    if (current != null)
      JournalEntry.RedirectToBatch((PXGraph) this, current.OrigModule, current.OrigBatchNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewReclassBatch(PXAdapter adapter)
  {
    GLTran current = ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Current;
    if (current != null)
      JournalEntry.RedirectToBatch((PXGraph) this, current.ReclassBatchModule, current.ReclassBatchNbr);
    return adapter.Get();
  }

  public virtual void PrepareForDocumentRelease()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldVerifying.AddHandler<GLTran.projectID>(JournalEntry.\u003C\u003Ec.\u003C\u003E9__108_0 ?? (JournalEntry.\u003C\u003Ec.\u003C\u003E9__108_0 = new PXFieldVerifying((object) JournalEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CPrepareForDocumentRelease\u003Eb__108_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldVerifying.AddHandler<GLTran.taskID>(JournalEntry.\u003C\u003Ec.\u003C\u003E9__108_1 ?? (JournalEntry.\u003C\u003Ec.\u003C\u003E9__108_1 = new PXFieldVerifying((object) JournalEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CPrepareForDocumentRelease\u003Eb__108_1))));
  }

  public void SetZeroPostIfUndefined(
    GLTran tran,
    IReadOnlyCollection<string> transClassesWithoutZeroPost)
  {
    if (tran.ZeroPost.HasValue)
      return;
    tran.ZeroPost = new bool?(!transClassesWithoutZeroPost.Contains<string>(tran.TranClass));
  }

  public int GetReversingBatchesCount(Batch batch)
  {
    return JournalEntry.GetReversingBatches((PXGraph) this, batch.Module, batch.BatchNbr).Count<Batch>();
  }

  protected IEnumerable<GLTran> GetFilteredTrans()
  {
    int num1 = 0;
    int num2 = 0;
    return GraphHelper.RowCast<GLTran>((IEnumerable) ((PXSelectBase) this.GLTranModuleBatNbr).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, ((PXSelectBase) this.GLTranModuleBatNbr).View.GetExternalFilters(), ref num1, PXView.MaximumRows, ref num2));
  }

  public void ReverseDocumentBatch(Batch batch)
  {
    IReadOnlyCollection<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>> infoNotInterCompany = this.GetTranCuryInfoNotInterCompany(batch.Module, batch.BatchNbr);
    if (infoNotInterCompany.Select<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>, GLTran>((Func<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>, GLTran>) (row => PXResult<GLTran, PX.Objects.CM.CurrencyInfo>.op_Implicit(row))).GroupBy(tran => new
    {
      RefNbr = tran.RefNbr,
      TranType = tran.TranType
    }).Count<IGrouping<\u003C\u003Ef__AnonymousType59<string, string>, GLTran>>() > 1)
      throw new PXException("Batch cannot be reversed because it contains transactions for more than one document.");
    Func<PXGraph, GLTran, PX.Objects.CM.CurrencyInfo, GLTran> buildTran = (Func<PXGraph, GLTran, PX.Objects.CM.CurrencyInfo, GLTran>) ((graph, srcTran, curyInfo) => JournalEntry.BuildReverseTran(graph, srcTran, JournalEntry.TranBuildingModes.None, curyInfo));
    this.ReverseBatchProc(batch, infoNotInterCompany, new Func<Batch, PX.Objects.CM.CurrencyInfo, Batch>(this.BuildBatchHeaderBase), buildTran);
  }

  public virtual void ReverseBatchProc(Batch batch)
  {
    IReadOnlyCollection<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>> infoNotInterCompany = this.GetTranCuryInfoNotInterCompany(batch.Module, batch.BatchNbr);
    Func<PXGraph, GLTran, PX.Objects.CM.CurrencyInfo, GLTran> buildTran = (Func<PXGraph, GLTran, PX.Objects.CM.CurrencyInfo, GLTran>) ((graph, srcTran, curyInfo) => JournalEntry.BuildReverseTran(graph, srcTran, JournalEntry.TranBuildingModes.SetLinkToOriginal, curyInfo));
    this.ReverseBatchProc(batch, infoNotInterCompany, new Func<Batch, PX.Objects.CM.CurrencyInfo, Batch>(this.BuildReverseBatchHeader), buildTran);
  }

  public virtual Batch ReverseBatchProc(
    Batch srcBatch,
    IReadOnlyCollection<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>> transWithCuryInfoForReverse,
    Func<Batch, PX.Objects.CM.CurrencyInfo, Batch> buildBatchHeader,
    Func<PXGraph, GLTran, PX.Objects.CM.CurrencyInfo, GLTran> buildTran)
  {
    ((PXGraph) this).Clear((PXClearOption) 1);
    if (!this.GetStateController(srcBatch).CanReverseBatch(srcBatch))
      throw new Exception("Batch can't be reversed, because batches from CM module are prohibited for reversing.");
    PX.Objects.CM.CurrencyInfo copy1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(GraphHelper.RowCast<PX.Objects.CM.CurrencyInfo>((IEnumerable) transWithCuryInfoForReverse).FirstOrDefault<PX.Objects.CM.CurrencyInfo>((Func<PX.Objects.CM.CurrencyInfo, bool>) (_ =>
    {
      long? curyInfoId1 = _.CuryInfoID;
      long? curyInfoId2 = srcBatch.CuryInfoID;
      return curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue;
    })) ?? PXResult<GLTran, PX.Objects.CM.CurrencyInfo>.op_Implicit(transWithCuryInfoForReverse.First<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>>()));
    copy1.CuryInfoID = new long?();
    copy1.IsReadOnly = new bool?(false);
    copy1.BaseCalc = new bool?(true);
    PX.Objects.CM.CurrencyInfo copy2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(copy1));
    Batch batch1 = buildBatchHeader(srcBatch, copy2);
    if (EPApprovalSettings<GLSetupApproval, GLSetupApproval.batchType, BatchTypeCode, BatchStatus.hold, BatchStatus.pendingApproval, BatchStatus.rejected>.ApprovableDocTypes.Contains(batch1.BatchType))
      batch1.Hold = new bool?(true);
    Batch batch2 = batch1;
    bool? hold = batch1.Hold;
    bool? nullable1 = hold.HasValue ? new bool?(!hold.GetValueOrDefault()) : new bool?();
    batch2.Approved = nullable1;
    Batch batch3 = ((PXSelectBase<Batch>) this.BatchModule).Insert(batch1);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.BatchModule).Cache, (object) srcBatch, ((PXSelectBase) this.BatchModule).Cache, (object) batch3, (PXNoteAttribute.IPXCopySettings) null);
    if (copy2 != null)
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<Batch.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
      currencyInfo.CuryID = copy2.CuryID;
      currencyInfo.CuryEffDate = copy2.CuryEffDate;
      currencyInfo.CuryRateTypeID = copy2.CuryRateTypeID;
      currencyInfo.CuryRate = copy2.CuryRate;
      currencyInfo.RecipRate = copy2.RecipRate;
      currencyInfo.CuryMultDiv = copy2.CuryMultDiv;
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Update((PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.CreateCopy((object) currencyInfo));
    }
    foreach (PXResult<GLAllocationAccountHistory> pxResult in PXSelectBase<GLAllocationAccountHistory, PXSelect<GLAllocationAccountHistory, Where<GLAllocationAccountHistory.module, Equal<Required<Batch.module>>, And<GLAllocationAccountHistory.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) srcBatch.Module,
      (object) srcBatch.BatchNbr
    }))
    {
      GLAllocationAccountHistory copy3 = PXCache<GLAllocationAccountHistory>.CreateCopy(PXResult<GLAllocationAccountHistory>.op_Implicit(pxResult));
      copy3.BatchNbr = (string) null;
      batch3.ReverseCount = new int?(0);
      GLAllocationAccountHistory allocationAccountHistory1 = copy3;
      Decimal num1 = -1M;
      Decimal? nullable2 = copy3.AllocatedAmount;
      Decimal? nullable3 = nullable2.HasValue ? new Decimal?(num1 * nullable2.GetValueOrDefault()) : new Decimal?();
      allocationAccountHistory1.AllocatedAmount = nullable3;
      GLAllocationAccountHistory allocationAccountHistory2 = copy3;
      Decimal num2 = -1M;
      nullable2 = copy3.PriorPeriodsAllocAmount;
      Decimal? nullable4 = nullable2.HasValue ? new Decimal?(num2 * nullable2.GetValueOrDefault()) : new Decimal?();
      allocationAccountHistory2.PriorPeriodsAllocAmount = nullable4;
      ((PXSelectBase<GLAllocationAccountHistory>) this.AllocationAccountHistory).Insert(copy3);
    }
    foreach (PXResult<GLAllocationHistory> pxResult in PXSelectBase<GLAllocationHistory, PXSelect<GLAllocationHistory, Where<GLAllocationHistory.module, Equal<Required<Batch.module>>, And<GLAllocationHistory.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) srcBatch.Module,
      (object) srcBatch.BatchNbr
    }))
    {
      GLAllocationHistory copy4 = PXCache<GLAllocationHistory>.CreateCopy(PXResult<GLAllocationHistory>.op_Implicit(pxResult));
      copy4.BatchNbr = (string) null;
      ((PXSelectBase<GLAllocationHistory>) this.AllocationHistory).Insert(copy4);
    }
    PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) null;
    foreach (PXResult<GLTran, PX.Objects.CM.CurrencyInfo> pxResult in (IEnumerable<PXResult<GLTran, PX.Objects.CM.CurrencyInfo>>) transWithCuryInfoForReverse)
    {
      PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResult<GLTran, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      if (currencyInfo1 != null)
      {
        long? curyInfoId3 = currencyInfo2.CuryInfoID;
        long? curyInfoId4 = currencyInfo1.CuryInfoID;
        if (!(curyInfoId3.GetValueOrDefault() == curyInfoId4.GetValueOrDefault() & curyInfoId3.HasValue == curyInfoId4.HasValue) && (!string.Equals(currencyInfo2.CuryID, currencyInfo2.BaseCuryID) || !string.Equals(currencyInfo1.CuryID, currencyInfo1.BaseCuryID)))
          ((PXSelectBase) this.BatchModule).Cache.RaiseExceptionHandling<Batch.origBatchNbr>((object) batch3, (object) null, (Exception) new PXSetPropertyException("Original batch consolidates multiple documents. The currency information is taken from the first document.", (PXErrorLevel) 2));
      }
      currencyInfo1 = currencyInfo2;
      GLTran glTran1 = buildTran((PXGraph) this, PXResult<GLTran, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult), copy2);
      Batch batch4 = batch3;
      Decimal? nullable5 = batch4.CreditTotal;
      Decimal? nullable6 = glTran1.CreditAmt;
      batch4.CreditTotal = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      Batch batch5 = batch3;
      nullable6 = batch5.DebitTotal;
      nullable5 = glTran1.DebitAmt;
      batch5.DebitTotal = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      Batch batch6 = batch3;
      nullable5 = batch6.ControlTotal;
      nullable6 = glTran1.DebitAmt;
      batch6.ControlTotal = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      Batch batch7 = batch3;
      nullable6 = batch7.CuryControlTotal;
      nullable5 = glTran1.CuryDebitAmt;
      batch7.CuryControlTotal = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      nullable5 = glTran1.CuryDebitAmt;
      Decimal num3 = 0M;
      if (nullable5.GetValueOrDefault() == num3 & nullable5.HasValue)
      {
        nullable5 = glTran1.CuryCreditAmt;
        Decimal num4 = 0M;
        if (nullable5.GetValueOrDefault() == num4 & nullable5.HasValue && glTran1.TaxID == null)
          goto label_28;
      }
      glTran1 = ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Insert(glTran1);
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) PXResult<GLTran, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult), ((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) glTran1, (PXNoteAttribute.IPXCopySettings) null);
label_28:
      if (glTran1.TranType == "REV" && this.GetStateController(srcBatch).CanReverseBatch(srcBatch))
      {
        GLTran glTran2 = ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Insert(glTran1);
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) PXResult<GLTran, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult), ((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) glTran2, (PXNoteAttribute.IPXCopySettings) null);
        nullable5 = glTran2.DebitAmt;
        Decimal num5 = 0M;
        if (!(nullable5.GetValueOrDefault() == num5 & nullable5.HasValue))
        {
          Batch batch8 = batch3;
          nullable5 = batch8.DebitTotal;
          nullable6 = glTran2.DebitAmt;
          batch8.DebitTotal = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          nullable6 = glTran2.CreditAmt;
          Decimal num6 = 0M;
          if (!(nullable6.GetValueOrDefault() == num6 & nullable6.HasValue))
          {
            Batch batch9 = batch3;
            nullable6 = batch9.CreditTotal;
            nullable5 = glTran2.CreditAmt;
            batch9.CreditTotal = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
          }
        }
      }
    }
    return batch3;
  }

  protected Batch BuildBatchHeaderBase(Batch srcBatch, PX.Objects.CM.CurrencyInfo curyInfo)
  {
    Batch copy = PXCache<Batch>.CreateCopy(srcBatch);
    copy.BatchNbr = (string) null;
    copy.NoteID = new Guid?();
    copy.ReverseCount = new int?(0);
    copy.CuryInfoID = curyInfo.CuryInfoID;
    copy.Posted = new bool?(false);
    copy.Voided = new bool?(false);
    copy.Scheduled = new bool?(false);
    copy.CuryDebitTotal = new Decimal?(0M);
    copy.CuryCreditTotal = new Decimal?(0M);
    copy.CuryControlTotal = new Decimal?(0M);
    copy.OrigBatchNbr = (string) null;
    copy.OrigModule = (string) null;
    copy.AutoReverseCopy = new bool?(false);
    copy.HasRamainingAmount = new bool?(false);
    ((PXSelectBase) this.BatchModule).Cache.SetDefaultExt<Batch.hold>((object) copy);
    return copy;
  }

  protected Batch BuildReverseBatchHeader(Batch srcBatch, PX.Objects.CM.CurrencyInfo curyInfo)
  {
    Batch batch = this.BuildBatchHeaderBase(srcBatch, curyInfo);
    batch.Module = "GL";
    batch.Released = new bool?(false);
    batch.OrigBatchNbr = srcBatch.BatchNbr;
    batch.OrigModule = srcBatch.Module;
    batch.AutoReverseCopy = new bool?(true);
    return batch;
  }

  public static GLTran BuildReleasableTransaction(
    PXGraph graph,
    GLTran srcTran,
    JournalEntry.TranBuildingModes buildingMode,
    PX.Objects.CM.CurrencyInfo curyInfo = null)
  {
    GLTran copy = PXCache<GLTran>.CreateCopy(srcTran);
    copy.Module = (string) null;
    copy.BatchNbr = (string) null;
    copy.LineNbr = new int?();
    if (buildingMode.HasFlag((Enum) JournalEntry.TranBuildingModes.SetLinkToOriginal))
    {
      copy.OrigBatchNbr = srcTran.BatchNbr;
      copy.OrigModule = srcTran.Module;
      copy.OrigLineNbr = srcTran.LineNbr;
    }
    else
    {
      copy.OrigBatchNbr = (string) null;
      copy.OrigModule = (string) null;
      copy.OrigLineNbr = new int?();
    }
    copy.LedgerID = new int?();
    copy.CATranID = new long?();
    copy.TranID = new int?();
    copy.TranDate = new DateTime?();
    copy.FinPeriodID = (string) null;
    copy.TranPeriodID = (string) null;
    copy.Released = new bool?(false);
    copy.Posted = new bool?(false);
    copy.ReclassSourceTranModule = (string) null;
    copy.ReclassSourceTranBatchNbr = (string) null;
    copy.ReclassSourceTranLineNbr = new int?();
    copy.ReclassBatchNbr = (string) null;
    copy.ReclassBatchModule = (string) null;
    copy.ReclassType = (string) null;
    copy.CuryReclassRemainingAmt = new Decimal?();
    copy.ReclassRemainingAmt = new Decimal?();
    copy.Reclassified = new bool?(false);
    copy.ReclassSeqNbr = new int?();
    copy.IsReclassReverse = new bool?(false);
    copy.ReclassificationProhibited = new bool?(false);
    copy.ReclassOrigTranDate = new DateTime?();
    copy.ReclassTotalCount = new int?();
    copy.ReclassReleasedCount = new int?();
    copy.NoteID = new Guid?();
    copy.PMTranID = new long?();
    copy.OrigPMTranID = new long?();
    if (curyInfo != null)
      copy.CuryInfoID = curyInfo.CuryInfoID;
    return copy;
  }

  public static GLTran BuildReverseTran(
    PXGraph graph,
    GLTran srcTran,
    JournalEntry.TranBuildingModes buildingMode,
    PX.Objects.CM.CurrencyInfo curyInfo = null)
  {
    GLTran glTran1 = JournalEntry.BuildReleasableTransaction(graph, srcTran, buildingMode, curyInfo);
    GLTran glTran2 = glTran1;
    Decimal num = -1M;
    Decimal? qty = glTran1.Qty;
    Decimal? nullable1 = qty.HasValue ? new Decimal?(num * qty.GetValueOrDefault()) : new Decimal?();
    glTran2.Qty = nullable1;
    Decimal? curyCreditAmt = glTran1.CuryCreditAmt;
    glTran1.CuryCreditAmt = glTran1.CuryDebitAmt;
    glTran1.CuryDebitAmt = curyCreditAmt;
    Decimal? creditAmt = glTran1.CreditAmt;
    glTran1.CreditAmt = glTran1.DebitAmt;
    glTran1.DebitAmt = creditAmt;
    int? nullable2 = glTran1.ProjectID;
    if (nullable2.HasValue && ProjectDefaultAttribute.IsNonProject(glTran1.ProjectID))
    {
      GLTran glTran3 = glTran1;
      nullable2 = new int?();
      int? nullable3 = nullable2;
      glTran3.TaskID = nullable3;
    }
    return glTran1;
  }

  /// <summary>
  /// The collection of batches created during the current long-running operation.
  /// The collection serves the following two purposes:
  /// 	1. If the <see cref="P:PX.Objects.CS.FeaturesSet.ConsolidatedPosting" /> feature is activated,
  /// 		the collection stores the list of batches to which transactions can be added during the current operation.
  /// 	2.  If the <see cref="P:PX.Objects.GL.GLSetup.AutoPostOption" /> setting is activated,
  /// 		the collection stores the list of batches that should be posted after release.
  /// 
  /// 						!!!WARNING!!!
  /// If persisting of some <see cref="T:PX.Objects.GL.Batch" /> was interrupted by some reason,
  /// this collection may store inconsistent state of <see cref="T:PX.Objects.GL.Batch" />.
  /// </summary>
  public DocumentList<Batch> created => this._created;

  public bool IsOffLine => this._IsOffline;

  public JournalEntry()
  {
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<Batch.finPeriodID>(((PXSelectBase) this.BatchModule).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetVisible<GLTran.taskID>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, ProjectAttribute.IsPMVisible("GL"));
    PXUIFieldAttribute.SetVisible<GLTran.nonBillable>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, ProjectAttribute.IsPMVisible("GL"));
    PXUIFieldAttribute.SetDisplayName<GLTran.projectID>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, "Project/Contract");
    this._created = new DocumentList<Batch>((PXGraph) this);
    this.SummaryPostingController = new SummaryPostingController(this, PXResultset<PX.Objects.CA.CASetup>.op_Implicit(PXSetup<PX.Objects.CA.CASetup>.Select((PXGraph) this, Array.Empty<object>())));
    ((PXSelectBase) this.GLTranModuleBatNbr).GetAttribute<PXImportAttribute>().MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.MappingPropertiesInit);
    this.ledgersByBranch = new Lazy<IEnumerable<PXResult<Branch, Ledger>>>((Func<IEnumerable<PXResult<Branch, Ledger>>>) (() => ((IEnumerable<PXResult<Branch>>) PXSelectBase<Branch, PXSelectJoin<Branch, LeftJoin<Ledger, On<Ledger.ledgerID, Equal<Branch.ledgerID>>>, Where<Branch.branchID, Equal<Optional2<Branch.branchID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<Branch>>().Cast<PXResult<Branch, Ledger>>()));
  }

  /// <summary>
  /// Removes the last elements of the <see cref="P:PX.Objects.GL.JournalEntry.created" /> collection
  /// corresponding to GL batches that failed to persist. The absence of the batch index
  /// in the <see cref="!:persistedBatchIndices" /> dictionary is used as a criterion.
  /// This method should be called each time the transaction scope is rolled back,
  /// immediately after it is rolled back.
  /// </summary>
  public void CleanupCreated(ICollection<int> persistedBatchIndices)
  {
    for (int index = this.created.Count - 1; index >= 0 && !persistedBatchIndices.Contains(index); --index)
      this.created.RemoveAt(index);
  }

  public virtual void SetOffline()
  {
    ((PXGraph) this).SetOffline();
    this._IsOffline = true;
  }

  public static void SegregateBatch(
    JournalEntry graph,
    string module,
    int? branchID,
    string curyID,
    DateTime? docDate,
    string finPeriodID,
    string description,
    PX.Objects.CM.CurrencyInfo curyInfo,
    Batch consolidatingBatch)
  {
    graph.SegregateBatch(module, branchID, curyID, docDate, finPeriodID, description, curyInfo, consolidatingBatch);
  }

  public virtual void Segregate(
    string module,
    int? branchID,
    string curyID,
    DateTime? effectiveDate,
    DateTime? dateEntered,
    string finPeriodID,
    string descr,
    Decimal? curyRate,
    string curyRateType,
    Batch consolidatingBatch)
  {
    this.SegregateBatch(module, branchID, curyID, dateEntered, finPeriodID, descr, new PX.Objects.CM.CurrencyInfo()
    {
      CuryEffDate = effectiveDate,
      CuryRateTypeID = curyRateType,
      SampleCuryRate = curyRate
    }, consolidatingBatch);
  }

  public virtual void Segregate(
    string Module,
    int? BranchID,
    string CuryID,
    DateTime? DocDate,
    string FinPeriodID,
    string Descr,
    Decimal? curyRate,
    string curyRateType,
    Batch consolidatingBatch)
  {
    this.Segregate(Module, BranchID, CuryID, DocDate, DocDate, FinPeriodID, Descr, curyRate, curyRateType, consolidatingBatch);
  }

  public virtual void SegregateBatch(
    string module,
    int? branchID,
    string curyID,
    DateTime? docDate,
    string finPeriodID,
    string description,
    PX.Objects.CM.CurrencyInfo curyInfo,
    Batch consolidatingBatch)
  {
    this.created.Consolidate = ((PXSelectBase<GLSetup>) this.glsetup).Current.ConsolidatedPosting.GetValueOrDefault();
    if (!this.IsOffLine && ((PXSelectBase) this.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
      ((PXGraph) this).Clear();
    Batch batch1;
    if (consolidatingBatch != null && consolidatingBatch.Module == module)
    {
      int? branchId = consolidatingBatch.BranchID;
      int? nullable = branchID;
      if (branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue && consolidatingBatch.CuryID == curyID && consolidatingBatch.FinPeriodID == finPeriodID)
      {
        batch1 = consolidatingBatch;
        goto label_6;
      }
    }
    batch1 = this.created.Find<Batch.module, Batch.branchID, Batch.curyID, Batch.finPeriodID>((object) module, (object) branchID, (object) curyID, (object) finPeriodID);
label_6:
    if (batch1 != null && batch1.AutoReverseCopy.GetValueOrDefault())
      batch1 = (Batch) null;
    if (batch1 != null)
    {
      if (!((PXSelectBase) this.BatchModule).Cache.ObjectsEqual((object) ((PXSelectBase<Batch>) this.BatchModule).Current, (object) batch1))
        ((PXGraph) this).Clear();
      Batch batch2 = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) this.BatchModule).Search<Batch.batchNbr>((object) batch1.BatchNbr, new object[1]
      {
        (object) batch1.Module
      }));
      if (batch2 != null && batch2.Posted.GetValueOrDefault())
        throw new PXInvalidOperationException("Batch Status invalid for processing.");
      PXCache<Batch>.StoreOriginal((PXGraph) this, batch2);
      if (batch2 != null)
      {
        if (batch2.Description != description)
        {
          batch2.Description = "";
          ((PXSelectBase<Batch>) this.BatchModule).Update(batch2);
        }
        ((PXSelectBase<Batch>) this.BatchModule).Current = batch2;
      }
      else
      {
        this.created.Remove(batch1);
        batch1 = (Batch) null;
      }
    }
    if (batch1 != null)
      return;
    ((PXGraph) this).Clear();
    Ledger ledger = PXResult<Branch, Ledger>.op_Implicit(this.ledgersByBranch.Value.FirstOrDefault<PXResult<Branch, Ledger>>((Func<PXResult<Branch, Ledger>, bool>) (result =>
    {
      int? branchId = PXResult<Branch, Ledger>.op_Implicit(result).BranchID;
      int? nullable = branchID;
      return branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue;
    })));
    if (ledger != null && module != "GL" && module != "CM" && ((PXSelectBase<Company>) this.company).Current.BaseCuryID != ledger.BaseCuryID && !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      throw new PXException("Actual ledger '{0}' must be defined in base currency {1} only.", new object[2]
      {
        (object) ledger.LedgerCD,
        (object) ((PXSelectBase<Company>) this.company).Current.BaseCuryID
      });
    PX.Objects.CM.CurrencyInfo currencyInfo1 = new PX.Objects.CM.CurrencyInfo();
    currencyInfo1.CuryID = curyID;
    PX.Objects.CM.CurrencyInfo currencyInfo2 = currencyInfo1;
    DateTime? curyEffDate = (DateTime?) curyInfo?.CuryEffDate;
    DateTime? nullable1 = curyEffDate ?? docDate;
    currencyInfo2.CuryEffDate = nullable1;
    currencyInfo1.CuryRateTypeID = curyInfo?.CuryRateTypeID ?? currencyInfo1.CuryRateTypeID;
    PXCache cache = ((PXSelectBase) this.currencyinfo).Cache;
    PX.Objects.CM.CurrencyInfo currencyInfo3 = currencyInfo1;
    Decimal? nullable2 = (Decimal?) curyInfo?.SampleCuryRate;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local1 = (ValueType) (nullable2 ?? currencyInfo1.SampleCuryRate);
    cache.SetValuePending<PX.Objects.CM.CurrencyInfo.sampleCuryRate>((object) currencyInfo3, (object) local1);
    PXSelectorAttribute.StoreResult<PX.Objects.CM.CurrencyInfo.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo1, (IBqlTable) CurrencyCollection.GetCurrency(currencyInfo1.CuryID));
    PX.Objects.CM.CurrencyInfo currencyInfo4 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(currencyInfo1) ?? currencyInfo1;
    Batch batch3 = new Batch();
    batch3.BranchID = branchID;
    batch3.Module = module;
    batch3.Released = new bool?(true);
    batch3.Hold = new bool?(false);
    batch3.DateEntered = docDate;
    batch3.FinPeriodID = finPeriodID;
    batch3.CuryID = curyID;
    batch3.CuryInfoID = currencyInfo4.CuryInfoID;
    batch3.CuryDebitTotal = new Decimal?(0M);
    batch3.CuryCreditTotal = new Decimal?(0M);
    batch3.DebitTotal = new Decimal?(0M);
    batch3.CreditTotal = new Decimal?(0M);
    batch3.Description = description;
    ((PXSelectBase<Batch>) this.BatchModule).Insert(batch3);
    PX.Objects.CM.CurrencyInfo currencyInfo5;
    if (!object.Equals((object) (long?) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current?.CuryInfoID, (object) batch3.CuryInfoID))
      currencyInfo5 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Locate(new PX.Objects.CM.CurrencyInfo()
      {
        CuryInfoID = batch3.CuryInfoID
      });
    else
      currencyInfo5 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current;
    PX.Objects.CM.CurrencyInfo currencyInfo6 = currencyInfo5;
    ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>());
    if (currencyInfo6 == null)
      return;
    currencyInfo6.CuryID = curyID;
    PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<Batch.curyInfoID>>>> currencyinfo = this.currencyinfo;
    PX.Objects.CM.CurrencyInfo currencyInfo7 = currencyInfo6;
    curyEffDate = (DateTime?) curyInfo?.CuryEffDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> local2 = (ValueType) (curyEffDate ?? docDate);
    ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) currencyinfo).SetValueExt<PX.Objects.CM.CurrencyInfo.curyEffDate>(currencyInfo7, (object) local2);
    PX.Objects.CM.CurrencyInfo currencyInfo8 = currencyInfo6;
    nullable2 = (Decimal?) curyInfo?.SampleCuryRate;
    Decimal? nullable3 = nullable2 ?? currencyInfo4.SampleCuryRate;
    currencyInfo8.SampleCuryRate = nullable3;
    PX.Objects.CM.CurrencyInfo currencyInfo9 = currencyInfo6;
    nullable2 = (Decimal?) curyInfo?.SampleRecipRate;
    Decimal? nullable4 = nullable2 ?? currencyInfo4.SampleRecipRate;
    currencyInfo9.SampleRecipRate = nullable4;
    currencyInfo6.CuryRateTypeID = curyInfo?.CuryRateTypeID ?? currencyInfo4.CuryRateTypeID;
    ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Update(currencyInfo6);
  }

  public static void ReleaseBatch(IList<Batch> list)
  {
    JournalEntry.ReleaseBatch(list, (IList<Batch>) null);
  }

  public static void ReleaseBatch(IList<Batch> list, IList<Batch> externalPostList)
  {
    JournalEntry.ReleaseBatch(list, externalPostList, false);
  }

  public static void ReleaseBatch(
    IList<Batch> list,
    IList<Batch> externalPostList,
    bool unholdBatch)
  {
    PostGraph instance = PXGraph.CreateInstance<PostGraph>();
    bool flag = externalPostList == null;
    for (int index = 0; index < list.Count; ++index)
    {
      ((PXGraph) instance).Clear((PXClearOption) 0);
      Batch b = list[index];
      instance.ReleaseBatchProc(b, unholdBatch);
      if (b.AutoReverse.Value && ((PXSelectBase<GLSetup>) instance.glsetup).Current.AutoRevOption == "R")
      {
        Batch batch = instance.ReverseBatchProc(b);
        list.Add(batch);
      }
      if (instance.AutoPost)
      {
        if (flag)
          instance.PostBatchProc(b);
        else
          externalPostList.Add(b);
      }
    }
  }

  public static void PostBatch(List<Batch> list)
  {
    PostGraph instance = PXGraph.CreateInstance<PostGraph>();
    for (int index = 0; index < list.Count; ++index)
    {
      ((PXGraph) instance).Clear((PXClearOption) 0);
      Batch b = list[index];
      instance.PostBatchProc(b);
    }
  }

  protected virtual void PopulateSubDescr(PXCache sender, GLTran Row, bool ExternalCall)
  {
    GLTran glTran = PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, NotEqual<Required<GLTran.lineNbr>>>>>, OrderBy<Desc<GLTran.lineNbr>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) Row.Module,
      (object) Row.BatchNbr,
      (object) Row.LineNbr
    }));
    PXResultset<PX.Objects.CA.CashAccount> pxResultset = PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>, And<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.active, Equal<True>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, new object[2]
    {
      (object) Row.BranchID,
      (object) Row.AccountID
    });
    Account account1 = (Account) PXSelectorAttribute.Select<GLTran.accountID>(sender, (object) glTran);
    if (pxResultset != null && pxResultset.Count == 1)
    {
      PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(pxResultset);
      sender.SetValue<GLTran.subID>((object) Row, (object) cashAccount.SubID);
    }
    else if (pxResultset.Count == 0)
    {
      Account account2 = (Account) PXSelectorAttribute.Select<GLTran.accountID>(sender, (object) Row);
      int? nullable;
      if (glTran != null)
      {
        nullable = glTran.SubID;
        if (nullable.HasValue)
        {
          nullable = Row.SubID;
          if (!nullable.HasValue && (EnumerableExtensions.IsIn<string>(account1.Type, "A", "L") && EnumerableExtensions.IsIn<string>(account2.Type, "A", "L") || EnumerableExtensions.IsIn<string>(account1.Type, "I", "E") && EnumerableExtensions.IsIn<string>(account2.Type, "I", "E")))
          {
            Sub sub = (Sub) PXSelectorAttribute.Select<GLTran.subID>(sender, (object) glTran);
            if (sub != null)
            {
              sender.SetValueExt<GLTran.subID>((object) Row, (object) sub.SubCD);
              PXUIFieldAttribute.SetError<GLTran.subID>(sender, (object) Row, (string) null);
            }
          }
        }
      }
      if (account2 != null && account2.NoSubDetail.Value)
      {
        nullable = ((PXSelectBase<GLSetup>) this.glsetup).Current.DefaultSubID;
        if (nullable.HasValue)
        {
          if (((PXGraph) this).IsImport)
          {
            nullable = Row.SubID;
            if (nullable.HasValue)
              goto label_13;
          }
          Row.SubID = ((PXSelectBase<GLSetup>) this.glsetup).Current.DefaultSubID;
        }
      }
    }
label_13:
    if (string.IsNullOrEmpty(Row.TranDesc))
    {
      if (glTran != null)
      {
        Row.TranDesc = glTran.TranDesc;
        Row.RefNbr = glTran.RefNbr;
      }
      else
        Row.TranDesc = ((PXSelectBase<Batch>) this.BatchModule).Current.Description;
    }
    this.BalanceGLTranAmount(Row);
  }

  protected virtual void BalanceGLTranAmount(GLTran Row)
  {
    Decimal? nullable = ((PXSelectBase<Batch>) this.BatchModule).Current.CuryCreditTotal;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = ((PXSelectBase<Batch>) this.BatchModule).Current.CuryDebitTotal;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num = valueOrDefault1 - valueOrDefault2;
    if (!PXCurrencyAttribute.IsNullOrEmpty(Row.CuryDebitAmt) || !PXCurrencyAttribute.IsNullOrEmpty(Row.CuryCreditAmt))
      return;
    if (num < 0M)
      Row.CuryCreditAmt = new Decimal?(Math.Abs(num));
    else
      Row.CuryDebitAmt = new Decimal?(Math.Abs(num));
  }

  public virtual void Persist()
  {
    BranchBaseAttribute.VerifyFieldInPXCache<GLTran, GLTran.branchID>((PXGraph) this, ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()));
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null && ((PXSelectBase) this.BatchModule).Cache.GetStatus((object) ((PXSelectBase<Batch>) this.BatchModule).Current) == 2)
    {
      foreach (GLTran glTran in ((PXSelectBase) this.GLTranModuleBatNbr).Cache.Inserted)
      {
        if (string.Equals(glTran.RefNbr, ((PXSelectBase<Batch>) this.BatchModule).Current.RefNbr))
          PXDBDefaultAttribute.SetDefaultForInsert<GLTran.refNbr>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) glTran, true);
      }
    }
    ((PXGraph) this).Persist();
    if (((PXSelectBase<Batch>) this.BatchModule).Current?.BatchType == "H")
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      GLTran glTran = ((IQueryable<PXResult<GLTran>>) PXSelectBase<GLTran, PXViewOf<GLTran>.BasedOn<SelectFromBase<GLTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<GLTran.curyInfoID, IBqlLong>.IsEqual<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLTran.module, Equal<BqlField<Batch.module, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLTran.batchNbr, Equal<BqlField<Batch.batchNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLTran.curyInfoID, NotEqual<BqlField<Batch.curyInfoID, IBqlLong>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.CM.CurrencyInfo.curyID, IBqlString>.IsNotEqual<BqlField<Batch.curyID, IBqlString>.FromCurrent>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PXResult<GLTran>, GLTran>(Expression.Lambda<Func<PXResult<GLTran>, GLTran>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<GLTran>();
      if (glTran != null)
        throw new PXSetPropertyException((IBqlTable) glTran, "Line #{0} has an unexpected CurrencyInfoID or the wrong CuryID of the linked CurrencyInfo entry and cannot be saved. Please report this situation to your Acumatica support provider.", new object[1]
        {
          (object) glTran.LineNbr
        });
    }
    this.SummaryPostingController.ShouldBeNormalized();
    if (((PXSelectBase<Batch>) this.BatchModule).Current == null)
      return;
    Batch batch = this.created.Find((object) ((PXSelectBase<Batch>) this.BatchModule).Current);
    if (batch == null)
      this.created.Add(((PXSelectBase<Batch>) this.BatchModule).Current);
    else
      ((PXSelectBase) this.BatchModule).Cache.RestoreCopy((object) batch, (object) ((PXSelectBase<Batch>) this.BatchModule).Current);
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    this.SummaryPostingController.ResetState();
  }

  protected virtual void CATran_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_TranPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_ReferenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_CuryID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Ledger.PK.Find((PXGraph) this, this.CurrentLedgerID)?.BaseCuryID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CurrencyInfo_BaseCuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Ledger.PK.Find((PXGraph) this, this.CurrentLedgerID)?.BaseCuryID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current.GLRateTypeDflt;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((PX.Objects.CM.CurrencyInfo) e.Row == null || ((PXSelectBase<Batch>) this.BatchModule).Current == null || !((PXSelectBase<Batch>) this.BatchModule).Current.DateEntered.HasValue)
      return;
    e.NewValue = (object) ((PXSelectBase<Batch>) this.BatchModule).Current.DateEntered;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    PX.Objects.CM.CurrencyInfo row = (PX.Objects.CM.CurrencyInfo) e.Row;
    if (((PXSelectBase<Batch>) this.BatchModule).Current != null && !(((PXSelectBase<Batch>) this.BatchModule).Current.Module != "GL"))
      return;
    BqlCommand bqlCommand = (BqlCommand) new Select<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyID>>, And<PX.Objects.CM.CurrencyInfo.baseCuryID, Equal<Required<PX.Objects.CM.CurrencyInfo.baseCuryID>>, And<PX.Objects.CM.CurrencyInfo.curyRateTypeID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyRateTypeID>>, And<PX.Objects.CM.CurrencyInfo.curyMultDiv, Equal<Required<PX.Objects.CM.CurrencyInfo.curyMultDiv>>, And<PX.Objects.CM.CurrencyInfo.curyRate, Equal<Required<PX.Objects.CM.CurrencyInfo.curyRate>>>>>>>>();
    foreach (PX.Objects.CM.CurrencyInfo currencyInfo in (IEnumerable<PX.Objects.CM.CurrencyInfo>) EnumerableEx.Select<PX.Objects.CM.CurrencyInfo>(sender.Cached).OrderByDescending<PX.Objects.CM.CurrencyInfo, long?>((Func<PX.Objects.CM.CurrencyInfo, long?>) (c => c.CuryInfoID)))
    {
      if (currencyInfo.CuryInfoID.HasValue && sender.GetStatus((object) currencyInfo) != 3 && sender.GetStatus((object) currencyInfo) != 4)
      {
        if (bqlCommand.Meet(sender, (object) currencyInfo, new object[5]
        {
          (object) row.CuryID,
          (object) row.BaseCuryID,
          (object) row.CuryRateTypeID,
          (object) row.CuryMultDiv,
          (object) row.CuryRate
        }))
        {
          row.CuryInfoID = currencyInfo.CuryInfoID;
          sender.Delete((object) currencyInfo);
          break;
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CM.CurrencyInfo> e)
  {
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current == null || e.Row == null || !(current.BatchType == "RCL") && (!(current.OrigModule == "CM") || this.GetStateController(current).CanReverseBatch(current)))
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CM.CurrencyInfo>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CM.CurrencyInfo>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CM.CurrencyInfo>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CM.CurrencyInfo>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void Batch_LedgerID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    Batch row = (Batch) e.Row;
    int? newValue = (int?) e.NewValue;
    int? ledgerId = row.LedgerID;
    int? nullable = newValue;
    if (ledgerId.GetValueOrDefault() == nullable.GetValueOrDefault() & ledgerId.HasValue == nullable.HasValue)
      return;
    string str = (string) null;
    Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this, newValue);
    int?[] array = ((IQueryable<PXResult<GLTran>>) ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Select(Array.Empty<object>())).Select<PXResult<GLTran>, int?>((Expression<Func<PXResult<GLTran>, int?>>) (o => ((GLTran) o).BranchID)).AsEnumerable<int?>().Distinct<int?>().ToArray<int?>();
    if (ledgerById.BalanceType == "A")
    {
      if (((IEnumerable<int?>) array).Any<int?>())
      {
        PXResultset<Branch> source = ((PXSelectBase<Branch>) new PXSelectReadonly<Branch, Where<Branch.branchID, In<Required<Branch.branchID>>, And<Branch.ledgerID, IsNull>>>((PXGraph) this)).Select(new object[1]
        {
          (object) ((IEnumerable<int?>) array).ToArray<int?>()
        });
        if (source.Count > 0)
        {
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          str = PXMessages.LocalizeFormat("No actual ledger has been associated with the following branches: {0}. Use the Ledgers (GL201500) form to associate ledgers with branches.", new object[1]
          {
            (object) ((ICollection<string>) ((IQueryable<PXResult<Branch>>) source).Select<PXResult<Branch>, string>(Expression.Lambda<Func<PXResult<Branch>, string>>((Expression) Expression.Call(((Branch) b).BranchCD, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Trim)), Array.Empty<Expression>()), parameterExpression)).ToArray<string>()).JoinIntoStringForMessage<string>()
          });
        }
      }
    }
    else
    {
      PXSelectBase<Branch> pxSelectBase = (PXSelectBase<Branch>) new PXSelectReadonly2<Branch, LeftJoin<OrganizationLedgerLink, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>, And<OrganizationLedgerLink.ledgerID, Equal<Required<Ledger.ledgerID>>>>>, Where<Branch.branchID, In<Required<Branch.branchID>>, And<OrganizationLedgerLink.ledgerID, IsNull>>>((PXGraph) this);
      if (((IEnumerable<int?>) array).Any<int?>())
      {
        PXResultset<Branch> source = pxSelectBase.Select(new object[2]
        {
          (object) newValue,
          (object) ((IEnumerable<int?>) array).ToArray<int?>()
        });
        if (source.Count > 0)
        {
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          str = PXMessages.LocalizeFormat("The {0} branch or branches have not been associated with the {1} ledger on the Ledgers (GL201500) form.", new object[2]
          {
            (object) ((ICollection<string>) ((IQueryable<PXResult<Branch>>) source).Select<PXResult<Branch>, string>(Expression.Lambda<Func<PXResult<Branch>, string>>((Expression) Expression.Call(((Branch) b).BranchCD, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Trim)), Array.Empty<Expression>()), parameterExpression)).ToArray<string>()).JoinIntoStringForMessage<string>(),
            (object) ledgerById.LedgerCD
          });
        }
      }
    }
    if (!string.IsNullOrEmpty(str))
      throw new PXSetPropertyException(str, (PXErrorLevel) 4)
      {
        ErrorValue = (object) ledgerById.LedgerCD
      };
  }

  protected virtual void Batch_LedgerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Batch row = (Batch) e.Row;
    int? oldValue = (int?) e.OldValue;
    int? ledgerId = row.LedgerID;
    if (oldValue.GetValueOrDefault() == ledgerId.GetValueOrDefault() & oldValue.HasValue == ledgerId.HasValue)
      return;
    ((PXCache) GraphHelper.Caches<Ledger>(sender.Graph)).Current = (object) Ledger.PK.Find((PXGraph) this, row.LedgerID);
    CurrencyInfoAttribute.SetDefaults<Batch.curyInfoID>(sender, e.Row);
    sender.SetDefaultExt<Batch.curyID>((object) row);
    foreach (PXResult<GLTran> pxResult in ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
    {
      GLTran glTran = PXResult<GLTran>.op_Implicit(pxResult);
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.SetDefaultExt<GLTran.ledgerID>((object) glTran);
      GraphHelper.MarkUpdated(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) glTran);
    }
  }

  protected virtual void Batch_CreateTaxTrans_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Batch row))
      return;
    this.GetStateController(row).Batch_CreateTaxTrans_FieldUpdated(sender, e);
  }

  private int? CurrentLedgerID
  {
    get
    {
      int? ledgerId = (int?) ((PXSelectBase<Batch>) this.BatchModule).Current?.LedgerID;
      if (ledgerId.HasValue)
        return ledgerId;
      return !(((PXCache) GraphHelper.Caches<Ledger>((PXGraph) this))?.InternalCurrent is Ledger internalCurrent) ? new int?() : internalCurrent.LedgerID;
    }
  }

  protected virtual void Batch_CuryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Ledger.PK.Find((PXGraph) this, this.CurrentLedgerID)?.BaseCuryID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void Batch_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    Batch row = (Batch) e.Row;
    Batch oldRow = (Batch) e.OldRow;
    bool? createTaxTrans1 = row.CreateTaxTrans;
    bool flag1 = false;
    bool? nullable1;
    if (createTaxTrans1.GetValueOrDefault() == flag1 & createTaxTrans1.HasValue)
    {
      nullable1 = row.SkipTaxValidation;
      if (nullable1.GetValueOrDefault())
        cache.SetValue<Batch.skipTaxValidation>((object) row, (object) false);
    }
    nullable1 = ((PXSelectBase<GLSetup>) this.glsetup).Current.RequireControlTotal;
    if (!nullable1.Value || row.Status == "U")
    {
      Decimal? nullable2 = row.CuryCreditTotal;
      if (nullable2.HasValue)
      {
        nullable2 = row.CuryCreditTotal;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        {
          cache.SetValue<Batch.curyControlTotal>((object) row, (object) row.CuryCreditTotal);
          goto label_11;
        }
      }
      nullable2 = row.CuryDebitTotal;
      if (nullable2.HasValue)
      {
        nullable2 = row.CuryDebitTotal;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        {
          cache.SetValue<Batch.curyControlTotal>((object) row, (object) row.CuryDebitTotal);
          goto label_11;
        }
      }
      cache.SetValue<Batch.curyControlTotal>((object) row, (object) 0M);
label_11:
      nullable2 = row.CreditTotal;
      if (nullable2.HasValue)
      {
        nullable2 = row.CreditTotal;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        {
          cache.SetValue<Batch.controlTotal>((object) row, (object) row.CreditTotal);
          goto label_18;
        }
      }
      nullable2 = row.DebitTotal;
      if (nullable2.HasValue)
      {
        nullable2 = row.DebitTotal;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        {
          cache.SetValue<Batch.controlTotal>((object) row, (object) row.DebitTotal);
          goto label_18;
        }
      }
      cache.SetValue<Batch.controlTotal>((object) row, (object) 0M);
    }
label_18:
    this.CheckBatchBalances(row, cache);
    if (!(row.Status == "B") && !(row.Status == "H") || !(row.Module == "GL") || !PXAccess.FeatureInstalled<FeaturesSet.taxEntryFromGL>())
      return;
    nullable1 = row.CreateTaxTrans;
    bool? createTaxTrans2 = oldRow.CreateTaxTrans;
    if (nullable1.GetValueOrDefault() == createTaxTrans2.GetValueOrDefault() & nullable1.HasValue == createTaxTrans2.HasValue)
      return;
    createTaxTrans2 = row.CreateTaxTrans;
    bool flag2 = false;
    if (createTaxTrans2.GetValueOrDefault() == flag2 & createTaxTrans2.HasValue)
    {
      foreach (PXResult<GLTran> pxResult in ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
      {
        GLTran glTran = PXResult<GLTran>.op_Implicit(pxResult);
        bool flag3 = false;
        if (!string.IsNullOrEmpty(glTran.TaxID))
        {
          glTran.TaxID = (string) null;
          flag3 = true;
        }
        if (!string.IsNullOrEmpty(glTran.TaxCategoryID))
        {
          glTran.TaxCategoryID = (string) null;
          flag3 = true;
        }
        if (flag3)
          ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Update(glTran);
      }
    }
    else
    {
      foreach (PXResult<GLTran> pxResult in ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
      {
        GLTran glTran = PXResult<GLTran>.op_Implicit(pxResult);
        ((PXSelectBase) this.GLTranModuleBatNbr).Cache.SetDefaultExt<GLTran.taxID>((object) glTran);
        ((PXSelectBase) this.GLTranModuleBatNbr).Cache.SetDefaultExt<GLTran.taxCategoryID>((object) glTran);
        ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Update(glTran);
      }
    }
  }

  protected virtual void Batch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is Batch row))
      return;
    ((PXAction) this.createSchedule).SetCaption(row.Status == "S" ? "View Schedule" : "Add to Schedule");
    if (((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current != null && !object.Equals((object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current.CuryInfoID, (object) row.CuryInfoID))
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current = (PX.Objects.CM.CurrencyInfo) null;
    if (((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Current != null && !object.Equals((object) ((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Current.MasterFinPeriodID, (object) row.TranPeriodID))
      ((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Current = (OrganizationFinPeriod) null;
    bool? nullable = row.Released;
    bool isBaseCalc = !nullable.GetValueOrDefault();
    PXNoteAttribute.SetTextFilesActivitiesRequired<GLTran.noteID>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, true, true, false);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyCreditTotal>(cache, (object) row, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyDebitTotal>(cache, (object) row, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyControlTotal>(cache, (object) row, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    nullable = row.Released;
    if (nullable.GetValueOrDefault() && !row.ReverseCount.HasValue && !((PXGraph) this).UnattendedMode)
      row.ReverseCount = new int?(this.GetReversingBatchesCount(row));
    this.GetStateController(row).Batch_RowSelected(cache, e);
    if (row.Module == "PM" && PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
      PXUIFieldAttribute.SetWarning<Batch.curyID>(cache, (object) row, "Journal entries may have individual currency conversion rates. Review the project transaction to view the conversion rate.");
    if (row.Status == "B" && EPApprovalSettings<GLSetupApproval, GLSetupApproval.batchType, BatchTypeCode, BatchStatus.hold, BatchStatus.pendingApproval, BatchStatus.rejected>.ApprovableDocTypes.Contains(row.BatchType))
    {
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.SetAllEditPermissions(false);
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<Batch.module>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<Batch.batchNbr>(cache, (object) row, true);
    }
    if (row.OrigModule == "CM" && !this.GetStateController(row).CanReverseBatch(row))
    {
      PXDBCurrencyAttribute.SetBaseCalc<Batch.curyCreditTotal>(cache, (object) row, false);
      PXDBCurrencyAttribute.SetBaseCalc<Batch.curyDebitTotal>(cache, (object) row, false);
      PXDBCurrencyAttribute.SetBaseCalc<Batch.curyControlTotal>(cache, (object) row, false);
      PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, false);
      PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, false);
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.SetAllEditPermissions(false);
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<Batch.module>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<Batch.batchNbr>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<Batch.description>(cache, (object) row, true);
    }
    PXCache pxCache = cache;
    Batch batch = row;
    nullable = row.CreateTaxTrans;
    int num = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Batch.skipTaxValidation>(pxCache, (object) batch, num != 0);
  }

  protected virtual void Batch_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    Batch row = (Batch) e.Row;
    if (row == null || !row.Released.GetValueOrDefault() || ((PXGraph) this).UnattendedMode)
      return;
    using (new PXConnectionScope())
      row.ReverseCount = new int?(this.GetReversingBatchesCount(row));
  }

  protected virtual void Batch_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    Batch row = (Batch) e.Row;
    if (row.BatchType == "RCL" && e.ExternalCall)
      row.BatchType = "H";
    PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.CM.CurrencyInfo)];
    foreach (PX.Objects.CM.CurrencyInfo currencyInfo in cach.Cached)
    {
      if (cach.GetStatus((object) currencyInfo) == null)
        cach.Remove((object) currencyInfo);
    }
  }

  protected virtual void Batch_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if ((e.Row is Batch row ? (row.Released.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("A released batch cannot be deleted.");
  }

  private bool IsBatchReadonly(Batch batch)
  {
    return batch.Module != "GL" && ((PXSelectBase) this.BatchModule).Cache.GetStatus((object) batch) == 2 || batch.Voided.GetValueOrDefault() || batch.Released.GetValueOrDefault();
  }

  protected virtual void AssertOnDelete(PX.Data.Events.RowPersisting<Batch> e)
  {
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<Batch>>) e).Cache.GetStatus((object) e.Row) == 3 && (e.Row.Released.GetValueOrDefault() || e.Row.Voided.GetValueOrDefault()))
      throw new PXInvalidOperationException("The {0} document with the {1} ref. number is released and cannot be deleted.", new object[2]
      {
        (object) "GL Batch",
        (object) e.Row.RefNbr
      });
  }

  protected virtual void Batch_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is Batch row))
      return;
    this.CheckBatchBalances(row, sender);
    JournalEntry.CheckBatchBranchHasLedger(sender, row);
  }

  /// <summary>
  /// Check balances and raise an exception if the batch is out of the balance
  /// </summary>
  protected virtual void CheckBatchBalances(Batch batch, PXCache cache)
  {
    bool flag = false;
    cache.RaiseExceptionHandling<Batch.curyControlTotal>((object) batch, (object) batch.CuryControlTotal, (Exception) null);
    Ledger ledger = Ledger.PK.Find((PXGraph) this, batch.LedgerID);
    if (batch.Status == "B" || batch.Status == "S" || batch.Status == "U")
    {
      Decimal? curyDebitTotal = batch.CuryDebitTotal;
      Decimal? curyCreditTotal1 = batch.CuryCreditTotal;
      if (!(curyDebitTotal.GetValueOrDefault() == curyCreditTotal1.GetValueOrDefault() & curyDebitTotal.HasValue == curyCreditTotal1.HasValue) && batch.BatchType != "T")
        flag = true;
      if (((PXSelectBase<GLSetup>) this.glsetup).Current.RequireControlTotal.GetValueOrDefault())
      {
        Decimal? curyCreditTotal2 = batch.CuryCreditTotal;
        Decimal? curyControlTotal = batch.CuryControlTotal;
        if (!(curyCreditTotal2.GetValueOrDefault() == curyControlTotal.GetValueOrDefault() & curyCreditTotal2.HasValue == curyControlTotal.HasValue) && ledger?.BalanceType != "S" && batch.BatchType != "T")
          cache.RaiseExceptionHandling<Batch.curyControlTotal>((object) batch, (object) batch.CuryControlTotal, (Exception) new PXSetPropertyException("The batch is not balanced. Review the debit and credit amounts."));
        else
          cache.RaiseExceptionHandling<Batch.curyControlTotal>((object) batch, (object) batch.CuryControlTotal, (Exception) null);
      }
    }
    Decimal? debitTotal = batch.DebitTotal;
    Decimal? creditTotal = batch.CreditTotal;
    if (!(debitTotal.GetValueOrDefault() == creditTotal.GetValueOrDefault() & debitTotal.HasValue == creditTotal.HasValue) && (batch.Status == "U" || batch.Status == "D"))
      flag = true;
    if (flag && ledger?.BalanceType != "S")
      cache.RaiseExceptionHandling<Batch.curyDebitTotal>((object) batch, (object) batch.CuryDebitTotal, (Exception) new PXSetPropertyException("The batch is not balanced. Review the debit and credit amounts."));
    else
      cache.RaiseExceptionHandling<Batch.curyDebitTotal>((object) batch, (object) batch.CuryDebitTotal, (Exception) null);
  }

  internal static void CheckBatchBranchHasLedger(PXCache cache, Batch batch)
  {
    if (!(batch.Module != "GL") || batch.LedgerID.HasValue || !batch.BranchID.HasValue)
      return;
    Branch branch = (Branch) PXSelectorAttribute.Select<Batch.branchID>(cache, (object) batch);
    if (branch != null)
      throw new PXException(PXAccess.FeatureInstalled<FeaturesSet.branch>() ? "The document cannot be released. No posting ledger of the Actual type is specified for the branch. Use the Ledgers (GL201500) form to assign a posting ledger for the company." : "The document cannot be released. A posting ledger of the Actual type does not exist in the system. Use the Ledgers (GL201500) form to create a ledger.", new object[1]
      {
        (object) (branch.BranchCD ?? "").TrimEnd()
      });
  }

  protected virtual void Batch_DateEntered_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(((Batch) e.Row).BatchType != "RCL"))
      return;
    CurrencyInfoAttribute.SetEffectiveDate<Batch.dateEntered>(cache, e);
  }

  public void _(PX.Data.Events.FieldUpdating<Batch, Batch.finPeriodID> e)
  {
    if (e.Row == null || !(e.Row.BatchType == "RCL"))
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<Batch, Batch.finPeriodID>>) e).NewValue = e.OldValue;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<Batch, Batch.finPeriodID>>) e).Cancel = true;
  }

  protected virtual void Batch_Module_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "GL";
  }

  protected virtual void Batch_AutoReverse_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    Batch row = (Batch) e.Row;
    if (!row.AutoReverse.GetValueOrDefault())
      return;
    cache.SetValueExt<Batch.createTaxTrans>((object) row, (object) false);
  }

  protected virtual void Batch_AutoReverseCopy_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Batch row = (Batch) e.Row;
    if (!row.AutoReverseCopy.GetValueOrDefault())
      return;
    cache.SetValueExt<Batch.createTaxTrans>((object) row, (object) false);
  }

  protected virtual void Batch_BatchNbr_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is Batch row))
      return;
    bool isBaseCalc = !row.Released.GetValueOrDefault();
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyCreditTotal>(cache, (object) row, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyDebitTotal>(cache, (object) row, isBaseCalc);
    if (!(row.OrigModule == "CM") || this.GetStateController(row).CanReverseBatch(row))
      return;
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyCreditTotal>(cache, (object) row, false);
    PXDBCurrencyAttribute.SetBaseCalc<Batch.curyDebitTotal>(cache, (object) row, false);
  }

  protected virtual void Batch_BranchID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    Batch row = (Batch) e.Row;
    int? oldValue = (int?) e.OldValue;
    int? branchId = row.BranchID;
    if (oldValue.GetValueOrDefault() == branchId.GetValueOrDefault() & oldValue.HasValue == branchId.HasValue)
      return;
    sender.SetDefaultExt<Batch.ledgerID>(e.Row);
    BranchBaseAttribute.VerifyFieldInPXCache<GLTran, GLTran.branchID>((PXGraph) this, ((PXSelectBase<GLTran>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()));
  }

  [Branch(typeof (Batch.branchID), typeof (Search2<Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where2<MatchWithBranch<Branch.branchID>, And<Match<Current<AccessInfo.userName>>>>>), true, true, true)]
  protected virtual void GLTran_BranchID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (ActiveProjectOrContractForGLAttribute), "DescriptionDisplayName", "Project/Contract Description")]
  protected virtual void _(PX.Data.Events.CacheAttached<GLTran.projectID> e)
  {
  }

  protected virtual bool GLTran_BranchLedgerVerifying(
    PXCache sender,
    GLTran tran,
    int? branchID,
    int? ledgerID)
  {
    if (!branchID.HasValue)
      return false;
    string str = (string) null;
    Branch branch = (Branch) null;
    if (!ledgerID.HasValue)
    {
      branch = BranchMaint.FindBranchByID((PXGraph) this, branchID);
      str = PXMessages.LocalizeFormat("No actual ledger has been associated with the following branches: {0}. Use the Ledgers (GL201500) form to associate ledgers with branches.", new object[1]
      {
        (object) branch.BranchCD
      });
    }
    else if (!((IQueryable<PXResult<Branch>>) PXSelectBase<Branch, PXSelectReadonly2<Branch, InnerJoin<OrganizationLedgerLink, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>, And<OrganizationLedgerLink.ledgerID, Equal<Required<Ledger.ledgerID>>>>>, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ledgerID,
      (object) branchID
    })).Any<PXResult<Branch>>())
    {
      branch = BranchMaint.FindBranchByID((PXGraph) this, branchID);
      Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this, ledgerID);
      str = PXMessages.LocalizeFormat("The {0} branch or branches have not been associated with the {1} ledger on the Ledgers (GL201500) form.", new object[2]
      {
        (object) branch.BranchCD,
        (object) ledgerById.LedgerCD
      });
    }
    if (string.IsNullOrEmpty(str) || branch == null)
      return true;
    sender.RaiseExceptionHandling<GLTran.branchID>((object) tran, (object) branch.BranchCD, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4));
    return false;
  }

  protected virtual void GLTran_BranchID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    GLTran row = (GLTran) e.Row;
    int? branchId = row.BranchID;
    int? newValue = (int?) e.NewValue;
    if (branchId.GetValueOrDefault() == newValue.GetValueOrDefault() & branchId.HasValue == newValue.HasValue)
      return;
    GLTran copy = PXCache<GLTran>.CreateCopy(row);
    copy.BranchID = (int?) e.NewValue;
    object obj = (object) null;
    IBqlCreator ibqlCreator = PXFormulaAttribute.InitFormula(sender.GetAttributesOfType<PXFormulaAttribute>((object) row, "ledgerID").First<PXFormulaAttribute>().Formula);
    bool? nullable = new bool?();
    BqlFormula.Verify(sender, (object) copy, ibqlCreator, ref nullable, ref obj);
    copy.LedgerID = (int?) obj;
    this.GLTran_BranchLedgerVerifying(sender, row, copy.BranchID, copy.LedgerID);
  }

  protected virtual void GLTran_AccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.CheckGLTranAccountIDControlAccount(sender, (EventArgs) e);
  }

  protected virtual void CheckGLTranAccountIDControlAccount(PXCache sender, EventArgs e)
  {
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current == null || current.BatchType == "C" || current.BatchType == "T" || !current.LedgerID.HasValue || current.Module != "GL" || Ledger.PK.Find((PXGraph) this, current.LedgerID)?.BalanceType != "A")
      return;
    Account account = AccountAttribute.GetAccount(sender, typeof (GLTran.accountID).Name, e);
    if (account == null || this.Mode.HasFlag((Enum) JournalEntry.Modes.RecognizingVAT) && account.ControlAccountModule == "TX" || this.Mode.HasFlag((Enum) JournalEntry.Modes.TaxReporting) && account.ControlAccountModule == "TX" || this.Mode.HasFlag((Enum) JournalEntry.Modes.InvoiceReclassification) && account.ControlAccountModule == "AP" || this.Mode.HasFlag((Enum) JournalEntry.Modes.InvoiceReclassification) && account.ControlAccountModule == "TX")
      return;
    AccountAttribute.VerifyAccountIsNotControl<GLTran.accountID>(sender, e, account);
  }

  protected virtual void GLTran_AccountID_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null && !((PXGraph) this).IsExport;
  }

  protected virtual void GLTran_LedgerID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    this.GLTran_BranchLedgerVerifying(sender, row, row.BranchID, (int?) e.NewValue);
  }

  protected virtual void GLTran_LedgerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLTran.projectID>(e.Row);
  }

  protected virtual void GLTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (e.ExternalCall && (e.Row == null || !this._importing) && sender.GetStatus((object) row) == 2 && !((GLTran) e.OldRow).AccountID.HasValue && row.AccountID.HasValue)
    {
      GLTran copy = PXCache<GLTran>.CreateCopy(row);
      this.PopulateSubDescr(sender, row, e.ExternalCall);
      sender.RaiseRowUpdated((object) row, (object) copy);
    }
    this.VerifyCashAccountActiveProperty(row);
  }

  protected virtual void GLTran_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is GLTran newRow))
      return;
    if (!newRow.BranchID.HasValue)
      cache.SetDefaultExt<GLTran.branchID>(e.NewRow);
    if (newRow.RefNbr == null)
      newRow.RefNbr = string.Empty;
    if (newRow.TranDesc == null)
      newRow.TranDesc = string.Empty;
    if (!newRow.Released.GetValueOrDefault() || !(e.Row is GLTran row) || cache.ObjectsEqual<GLTran.branchID, GLTran.finPeriodID, GLTran.released>((object) row, (object) newRow))
      return;
    this.ValidateGLTranFinPeriodByModule(newRow);
  }

  protected virtual void GLTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (e.ExternalCall && (e.Row == null || !this._importing) && sender.GetStatus((object) row) == 2 && row.AccountID.HasValue)
    {
      GLTran copy = PXCache<GLTran>.CreateCopy(row);
      this.PopulateSubDescr(sender, row, e.ExternalCall);
      sender.RaiseRowUpdated((object) row, (object) copy);
    }
    if (row.Module != "GL" && row.SummPost.GetValueOrDefault())
      this.SummaryPostingController.AddSummaryTransaction(row);
    this.VerifyCashAccountActiveProperty(row);
  }

  /// <summary>
  /// Verifies <see cref="T:PX.Objects.GL.GLTran" />'s account is a cash account. If it is a cash account then verifies its active property. Cash account must be
  /// active.
  /// </summary>
  /// <param name="glTran">The transaction to verify.</param>
  /// <param name="calledFromRowPersisting">(Optional) True if method called from row persisting event.</param>
  private void VerifyCashAccountActiveProperty(GLTran glTran, bool calledFromRowPersisting = false)
  {
    if (glTran == null)
      return;
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>, And<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) glTran.BranchID,
      (object) glTran.AccountID,
      (object) glTran.SubID
    }));
    if (cashAccount == null || cashAccount.Active.GetValueOrDefault())
      return;
    string str = $"The cash account {cashAccount.CashAccountCD.Trim()} is deactivated on the Cash Accounts (CA202000) form.";
    if (calledFromRowPersisting)
      throw new PXRowPersistingException(typeof (GLTran).Name, (object) glTran, str);
    ((PXSelectBase) this.GLTranModuleBatNbr).Cache.RaiseExceptionHandling<GLTran.accountID>((object) glTran, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4));
  }

  protected virtual void GLTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    GLTran row = e.Row as GLTran;
    if (row.Module != "GL")
      this.SummaryPostingController.RemoveIfNeeded(row);
    if (row.IsReclassReverse.GetValueOrDefault())
      return;
    GLTran glTran1 = PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, Equal<Required<GLTran.lineNbr>>, And<GLTran.reclassBatchModule, Equal<Required<GLTran.module>>, And<GLTran.reclassBatchNbr, Equal<Required<GLTran.batchNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) row.OrigModule,
      (object) row.OrigBatchNbr,
      (object) row.OrigLineNbr,
      (object) row.Module,
      (object) row.BatchNbr
    }));
    if (glTran1 == null)
      return;
    GLTran glTran2 = PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.origModule, Equal<Required<GLTran.origModule>>, And<GLTran.origBatchNbr, Equal<Required<GLTran.origBatchNbr>>, And<GLTran.origLineNbr, Equal<Required<GLTran.origLineNbr>>, And<GLTran.batchNbr, NotEqual<Required<GLTran.batchNbr>>>>>>, OrderBy<Desc<GLTran.reclassSeqNbr, Desc<GLTran.batchNbr, Desc<GLTran.lineNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[4]
    {
      (object) row.OrigModule,
      (object) row.OrigBatchNbr,
      (object) row.OrigLineNbr,
      (object) row.BatchNbr
    }));
    if (glTran2 == null)
    {
      glTran1.ReclassBatchModule = (string) null;
      glTran1.ReclassBatchNbr = (string) null;
    }
    else
    {
      glTran1.ReclassBatchModule = glTran2.Module;
      glTran1.ReclassBatchNbr = glTran2.BatchNbr;
    }
    GLTran glTran3 = glTran1;
    int? reclassTotalCount = glTran3.ReclassTotalCount;
    glTran3.ReclassTotalCount = reclassTotalCount.HasValue ? new int?(reclassTotalCount.GetValueOrDefault() - 1) : new int?();
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXSelectBase<Batch>) instance.BatchModule).Current = PXParentAttribute.SelectParent<Batch>(((PXSelectBase) instance.GLTranModuleBatNbr).Cache, (object) glTran1);
    ((PXSelectBase) instance.GLTranModuleBatNbr).Cache.SetStatus((object) glTran2, (PXEntryStatus) 1);
    ((PXSelectBase) instance.GLTranModuleBatNbr).Cache.Update((object) glTran1);
    ((PXAction) instance.Save).Press();
  }

  protected virtual void GLTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (row.RefNbr == null)
      row.RefNbr = string.Empty;
    if (row.TranDesc == null)
      row.TranDesc = string.Empty;
    if (!(row.Module != "GL"))
      return;
    bool? nullable1 = row.Released;
    if (nullable1.GetValueOrDefault())
      this.ValidateGLTranFinPeriodByModule(row);
    ((CancelEventArgs) e).Cancel = this.SummaryPostingController.TryAggregateToSummaryTransaction(row);
    if (!((CancelEventArgs) e).Cancel)
      PostGraph.NormalizeAmounts(row);
    if (!((CancelEventArgs) e).Cancel)
    {
      PXRowInsertingEventArgs insertingEventArgs = e;
      Decimal? nullable2 = row.CuryDebitAmt;
      Decimal num1 = 0M;
      int num2;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      {
        nullable2 = row.CuryCreditAmt;
        Decimal num3 = 0M;
        if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
        {
          nullable2 = row.DebitAmt;
          Decimal num4 = 0M;
          if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
          {
            nullable2 = row.CreditAmt;
            Decimal num5 = 0M;
            if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
            {
              nullable1 = row.ZeroPost;
              num2 = !nullable1.GetValueOrDefault() ? 1 : 0;
              goto label_16;
            }
          }
        }
      }
      num2 = 0;
label_16:
      ((CancelEventArgs) insertingEventArgs).Cancel = num2 != 0;
    }
    if (!((CancelEventArgs) e).Cancel && !PostGraph.GetAccountMapping((PXGraph) this, ((PXSelectBase<Batch>) this.BatchModule).Current, row, out BranchAcctMapFrom _, out BranchAcctMapTo _))
    {
      Branch branch1 = (Branch) PXSelectorAttribute.Select<Batch.branchID>(((PXSelectBase) this.BatchModule).Cache, (object) ((PXSelectBase<Batch>) this.BatchModule).Current, (object) ((PXSelectBase<Batch>) this.BatchModule).Current.BranchID);
      Branch branch2 = (Branch) PXSelectorAttribute.Select<GLTran.branchID>(sender, (object) row, (object) row.BranchID);
      throw new PXException("Account mapping is missing for the {0} and {1} branches. Specify account mapping on the Inter-Branch Account Mapping (GL101010) form.", new object[2]
      {
        (object) (branch1?.BranchCD?.Trim() ?? "Undefined"),
        (object) (branch2?.BranchCD?.Trim() ?? "Undefined")
      });
    }
  }

  protected virtual void GLTran_AccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((Account) PXSelectorAttribute.Select<GLTran.accountID>(sender, e.Row) == null)
      return;
    sender.SetDefaultExt<GLTran.projectID>(e.Row);
    sender.SetDefaultExt<GLTran.taxID>(e.Row);
    sender.SetDefaultExt<GLTran.taxCategoryID>(e.Row);
  }

  protected virtual void GLTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((GLTran) e.Row == null)
      return;
    sender.SetDefaultExt<GLTran.costCodeID>(e.Row);
  }

  protected virtual void GLTran_TaskID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((GLTran) e.Row == null)
      return;
    sender.SetDefaultExt<GLTran.costCodeID>(e.Row);
  }

  protected virtual void GLTran_SubID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((GLTran) e.Row == null)
      return;
    sender.SetDefaultExt<GLTran.taxID>(e.Row);
  }

  protected virtual void GLTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    this.CheckGLTranAccountIDControlAccount(sender, (EventArgs) e);
    if (!(e.Row is GLTran row))
      return;
    int? nullable = row.ProjectID;
    if (!nullable.HasValue)
    {
      Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.AccountID
      }));
      if (account != null)
      {
        nullable = account.AccountGroupID;
        if (nullable.HasValue)
          sender.RaiseExceptionHandling<GLTran.projectID>(e.Row, (object) row.ProjectID, (Exception) new PXSetPropertyException((IBqlTable) row, "Project is Required but was not specified. Account '{0}' used in the GL Transaction is mapped to Project Account Group.", new object[1]
          {
            (object) account.AccountCD
          }));
      }
    }
    if ((e.Operation & 3) == 3)
      return;
    if (((PXSelectBase<Batch>) this.BatchModule).Current.BatchType == "RCL" && row.TranClass == "R")
    {
      PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, false);
      PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, false);
    }
    this.VerifyCashAccountActiveProperty(row, true);
  }

  internal static void AssertBatchAndDetailHaveSameMasterPeriod(
    PXSelectBase<GLTran> view,
    Batch batch,
    GLTran gltran)
  {
    if (!(gltran.TranPeriodID != batch.TranPeriodID))
      return;
    ((PXSelectBase) view).Cache.RaiseExceptionHandling<GLTran.tranPeriodID>((object) gltran, (object) gltran.TranPeriodID, (Exception) new PXInvalidOperationException("The period in one of the lines ({0}) differs from the post period specified for the batch ({1}) and the batch cannot be processed further. If this error appears again, please contact the support team.", new object[2]
    {
      (object) gltran.TranPeriodID,
      (object) batch.TranPeriodID
    }));
  }

  protected virtual void GLTran_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is GLTran row) || e.Operation == 3 || e.TranStatus != null)
      return;
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (!(current.BatchType == "RCL") || !(row.TranClass == "R"))
      return;
    bool isBaseCalc = !current.Released.GetValueOrDefault();
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
  }

  protected virtual void GLTran_CuryCreditAmt_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (row == null)
      return;
    Decimal? nullable = row.CuryDebitAmt;
    if (!nullable.HasValue)
      return;
    nullable = row.CuryDebitAmt;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    nullable = row.CuryCreditAmt;
    if (!nullable.HasValue)
      return;
    nullable = row.CuryCreditAmt;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return;
    row.CuryDebitAmt = new Decimal?(0.0M);
    row.DebitAmt = new Decimal?(0.0M);
  }

  protected virtual void GLTran_CuryDebitAmt_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (row == null)
      return;
    Decimal? nullable = row.CuryCreditAmt;
    if (!nullable.HasValue)
      return;
    nullable = row.CuryCreditAmt;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    nullable = row.CuryDebitAmt;
    if (!nullable.HasValue)
      return;
    nullable = row.CuryDebitAmt;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return;
    row.CuryCreditAmt = new Decimal?(0.0M);
    row.CreditAmt = new Decimal?(0.0M);
  }

  protected virtual void GLTran_TaxID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    GLTran row = e.Row as GLTran;
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current == null || !current.CreateTaxTrans.GetValueOrDefault())
      return;
    e.NewValue = (object) null;
    int? nullable = row.AccountID;
    if (nullable.HasValue)
    {
      nullable = row.SubID;
      if (nullable.HasValue)
      {
        PXResultset<PX.Objects.TX.Tax> pxResultset = PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where2<Where<PX.Objects.TX.Tax.purchTaxAcctID, Equal<Required<GLTran.accountID>>, And<PX.Objects.TX.Tax.purchTaxSubID, Equal<Required<GLTran.subID>>>>, Or<Where<PX.Objects.TX.Tax.salesTaxAcctID, Equal<Required<GLTran.accountID>>, And<PX.Objects.TX.Tax.salesTaxSubID, Equal<Required<GLTran.subID>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) row.AccountID,
          (object) row.SubID,
          (object) row.AccountID,
          (object) row.SubID
        });
        if (pxResultset.Count == 1)
          e.NewValue = (object) PXResult<PX.Objects.TX.Tax>.op_Implicit(pxResultset[0]).TaxID;
        else if (pxResultset.Count > 1 && row.TaxID != null && GraphHelper.RowCast<PX.Objects.TX.Tax>((IEnumerable) pxResultset).Any<PX.Objects.TX.Tax>((Func<PX.Objects.TX.Tax, bool>) (t => t.TaxID == row.TaxID)))
          e.NewValue = (object) row.TaxID;
        else if (pxResultset.Count > 1)
          ((PXSelectBase) this.GLTranModuleBatNbr).Cache.RaiseExceptionHandling<GLTran.taxID>((object) row, (object) null, (Exception) new PXSetPropertyException("Account is associated with one or more taxes, but Tax ID is not specified", (PXErrorLevel) 2));
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current == null || e.Row == null)
      return;
    this.GetStateController(current).GLTran_TaxCategoryID_FieldDefaulting(sender, e, current);
  }

  protected virtual void GLTran_TaxID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is GLTran row) || e.NewValue == null)
      return;
    GLTran copy = sender.CreateCopy((object) row) as GLTran;
    copy.TaxID = e.NewValue as string;
    if (copy.TaxID == null || !(PXSelectorAttribute.Select<GLTran.taxID>(sender, (object) copy) is PX.Objects.TX.Tax tax))
      return;
    int? purchTaxAcctId = tax.PurchTaxAcctID;
    int? nullable1 = tax.SalesTaxAcctID;
    int? nullable2;
    if (purchTaxAcctId.GetValueOrDefault() == nullable1.GetValueOrDefault() & purchTaxAcctId.HasValue == nullable1.HasValue)
    {
      nullable1 = tax.PurchTaxSubID;
      nullable2 = tax.SalesTaxSubID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        sender.RaiseExceptionHandling<GLTran.taxID>((object) row, (object) tax.TaxID, (Exception) new PXSetPropertyException("Tax Claimable and Tax Payable accounts and subaccounts for Tax {0} are the same. It's impossible to enter this Tax via GL in this configuration.", new object[1]
        {
          (object) tax.TaxID
        }));
        e.NewValue = (object) row.TaxID;
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    nullable2 = tax.PurchTaxAcctID;
    nullable1 = row.AccountID;
    string str1;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
    {
      nullable1 = tax.PurchTaxSubID;
      nullable2 = row.SubID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        str1 = "P";
        goto label_13;
      }
    }
    nullable2 = tax.SalesTaxAcctID;
    nullable1 = row.AccountID;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
    {
      nullable1 = tax.SalesTaxSubID;
      nullable2 = row.SubID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        str1 = "S";
        goto label_13;
      }
    }
    str1 = (string) null;
label_13:
    string str2 = str1;
    if (str2 == null)
      return;
    if (PXResultset<TaxRev>.op_Implicit(PXSelectBase<TaxRev, PXSelectReadonly<TaxRev, Where<TaxRev.taxID, Equal<Required<TaxRev.taxID>>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<Required<TaxRev.taxType>>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[2]
    {
      (object) tax.TaxID,
      (object) str2
    })) != null)
      return;
    string lower = PXMessages.LocalizeNoPrefix(GetLabel.For<TaxType>(str2)).ToLower();
    sender.RaiseExceptionHandling<GLTran.taxID>((object) row, (object) tax.TaxID, (Exception) new PXSetPropertyException("The {0} tax rate is not specified in the settings of the selected tax.", new object[1]
    {
      (object) lower
    }));
    e.NewValue = (object) row.TaxID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTran_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current == null || e.Row == null)
      return;
    current.HasRamainingAmount = new bool?(ReclassStateController.HasRamainingAmount(current.HasRamainingAmount, e.Row as GLTran));
  }

  protected virtual void GLTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is GLTran row))
      return;
    bool isBaseCalc = row.TranClass != "R" && !row.Released.GetValueOrDefault() && row.TranType != "REV";
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    if (this.Mode.HasFlag((Enum) JournalEntry.Modes.Reclassification))
      return;
    Batch current = ((PXSelectBase<Batch>) this.BatchModule).Current;
    if (current == null || e.Row == null)
      return;
    this.GetStateController(((PXSelectBase<Batch>) this.BatchModule).Current).GLTran_RowSelected(sender, e, current);
    current.HasRamainingAmount = new bool?(ReclassStateController.HasRamainingAmount(current.HasRamainingAmount, e.Row as GLTran));
  }

  protected virtual void GLSetup_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!this.Mode.HasFlag((Enum) JournalEntry.Modes.Reclassification))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<GLTran, GLTran.costCodeID> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<GLTran, GLTran.costCodeID>, GLTran, object>) e).NewValue = (object) CostCodeAttribute.DefaultCostCode;
  }

  protected virtual IEnumerable accounts()
  {
    foreach (PXResult<GLTran, Account> pxResult in PXSelectBase<GLTran, PXSelectJoin<GLTran, InnerJoin<Account, On<GLTran.accountID, Equal<Account.accountID>>>, Where<GLTran.module, Equal<Current<Batch.module>>, And<GLTran.batchNbr, Equal<Current<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      yield return (object) new PXResult<Account, GLTran>(PXResult<GLTran, Account>.op_Implicit(pxResult), PXResult<GLTran, Account>.op_Implicit(pxResult));
  }

  [PXDefault(typeof (Batch.dateEntered))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Batch.description))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (Batch.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Batch.curyDebitTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Batch.debitTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<Batch>) this.BatchModule).Current == null)
      return;
    e.NewValue = (object) PXMessages.LocalizeFormatNoPrefix("Post period: {0}", new object[1]
    {
      (object) FinPeriodIDFormattingAttribute.FormatForError(((PXSelectBase<Batch>) this.BatchModule).Current.FinPeriodID)
    });
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXDefault("Journal Transaction")]
  [PXMergeAttributes]
  protected virtual void EPApproval_SourceItemType_CacheAttached(PXCache sender)
  {
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (((PXGraph) this).Accessinfo.CuryViewState || ((PXSelectBase<Batch>) this.BatchModule).Current?.BatchType == "T")
      return false;
    if (viewName == "GLTranModuleBatNbr")
    {
      string defValue1 = JournalEntry.CorrectImportValue(values, "CreditAmt", "0");
      JournalEntry.CorrectImportValue(values, "CuryCreditAmt", defValue1);
      string defValue2 = JournalEntry.CorrectImportValue(values, "DebitAmt", "0");
      JournalEntry.CorrectImportValue(values, "CuryDebitAmt", defValue2);
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  private static string CorrectImportValue(IDictionary dic, string fieldName, string defValue)
  {
    string str = defValue;
    if (!dic.Contains((object) fieldName))
    {
      dic.Add((object) fieldName, (object) defValue);
    }
    else
    {
      object obj = dic[(object) fieldName];
      string s;
      if (obj == null || string.IsNullOrEmpty(s = obj.ToString()) || !Decimal.TryParse(s, out Decimal _))
        dic[(object) fieldName] = (object) defValue;
      else
        str = s;
    }
    return str;
  }

  private void MappingPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    this.RemoveMappingProperty(e, "CuryViewState");
    this.RemoveMappingProperty(e, "CuryRate");
    this.RemoveMappingProperty(e, "CuryID");
  }

  private void RemoveMappingProperty(
    PXImportAttribute.MappingPropertiesInitEventArgs e,
    string prop)
  {
    int index = e.Names.FindIndex((Predicate<string>) (i => i == prop));
    if (index == -1)
      return;
    e.Names.RemoveAt(index);
    e.DisplayNames.RemoveAt(index);
  }

  public IEnumerable<GLTran> CreateTransBySchedule(DRProcess dr, GLTran templateTran)
  {
    Decimal num1 = (Decimal) (!(templateTran.DebitAmt.Value == 0M) ? 1 : 0);
    Decimal num2 = (Decimal) (!(templateTran.CreditAmt.Value == 0M) ? 1 : 0);
    List<GLTran> transBySchedule = new List<GLTran>();
    foreach (DRScheduleDetail scheduleDetail in (IEnumerable<DRScheduleDetail>) dr.GetScheduleDetails(((PXSelectBase<DRSchedule>) dr.Schedule).Current.ScheduleID))
    {
      GLTran copy = (GLTran) ((PXSelectBase) this.GLTranModuleBatNbr).Cache.CreateCopy((object) templateTran);
      copy.AccountID = scheduleDetail.DefAcctID;
      copy.SubID = scheduleDetail.DefSubID;
      copy.ReclassificationProhibited = new bool?(true);
      GLTran glTran1 = copy;
      Decimal num3 = num1;
      Decimal? totalAmt1 = scheduleDetail.TotalAmt;
      Decimal? nullable1 = totalAmt1.HasValue ? new Decimal?(num3 * totalAmt1.GetValueOrDefault()) : new Decimal?();
      glTran1.DebitAmt = nullable1;
      GLTran glTran2 = copy;
      Decimal num4 = num2;
      Decimal? totalAmt2 = scheduleDetail.TotalAmt;
      Decimal? nullable2 = totalAmt2.HasValue ? new Decimal?(num4 * totalAmt2.GetValueOrDefault()) : new Decimal?();
      glTran2.CreditAmt = nullable2;
      PXCurrencyAttribute.CuryConvCury<GLTran.curyCreditAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) copy);
      PXCurrencyAttribute.CuryConvCury<GLTran.curyDebitAmt>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) copy);
      transBySchedule.Add(copy);
    }
    return (IEnumerable<GLTran>) transBySchedule;
  }

  public IEnumerable<GLTran> CreateTransBySchedule(
    DRProcess dr,
    ARTran artran,
    GLTran templateTran)
  {
    List<GLTran> transBySchedule = new List<GLTran>();
    foreach (PXResult<DRScheduleDetail> pxResult in dr.GetScheduleDetailByOrigLineNbr(((PXSelectBase<DRSchedule>) dr.Schedule).Current.ScheduleID, artran.LineNbr))
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail>.op_Implicit(pxResult);
      GLTran copy = (GLTran) ((PXSelectBase) this.GLTranModuleBatNbr).Cache.CreateCopy((object) templateTran);
      copy.AccountID = drScheduleDetail.DefAcctID;
      copy.SubID = drScheduleDetail.DefSubID;
      copy.ReclassificationProhibited = new bool?(true);
      if (artran.DrCr == "C")
      {
        copy.CuryCreditAmt = drScheduleDetail.CuryTotalAmt;
        copy.CreditAmt = drScheduleDetail.TotalAmt;
      }
      else
      {
        copy.CuryDebitAmt = drScheduleDetail.CuryTotalAmt;
        copy.DebitAmt = drScheduleDetail.TotalAmt;
      }
      transBySchedule.Add(copy);
    }
    return (IEnumerable<GLTran>) transBySchedule;
  }

  public virtual void CorrectCuryAmountsDueToRounding(
    IEnumerable<GLTran> transactions,
    GLTran templateTran,
    Decimal curyExpectedTotal)
  {
    if (!transactions.Any<GLTran>())
      return;
    Decimal? nullable1 = templateTran.DebitAmt;
    Decimal drSign = (Decimal) (!(nullable1.Value == 0M) ? 1 : 0);
    nullable1 = templateTran.CreditAmt;
    Decimal crSign = (Decimal) (!(nullable1.Value == 0M) ? 1 : 0);
    Func<GLTran, Decimal> tranAmount = (Func<GLTran, Decimal>) (t =>
    {
      Decimal? nullable2 = t.CuryDebitAmt;
      Decimal num1 = nullable2.GetValueOrDefault() * drSign;
      nullable2 = t.CuryCreditAmt;
      Decimal num2 = nullable2.GetValueOrDefault() * crSign;
      return num1 + num2;
    });
    Decimal num3 = curyExpectedTotal - transactions.Sum<GLTran>(tranAmount);
    GLTran glTran = transactions.OrderByDescending<GLTran, Decimal>((Func<GLTran, Decimal>) (t => Math.Abs(tranAmount(t)))).First<GLTran>();
    nullable1 = glTran.CuryDebitAmt;
    Decimal num4 = num3 * drSign;
    glTran.CuryDebitAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num4) : new Decimal?();
    nullable1 = glTran.CuryCreditAmt;
    Decimal num5 = num3 * crSign;
    glTran.CuryCreditAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num5) : new Decimal?();
  }

  public static void RedirectToBatch(PXGraph graph, string module, string batchNbr)
  {
    Batch batch = JournalEntry.FindBatch(graph, module, batchNbr);
    if (batch == null)
      return;
    JournalEntry.RedirectToBatch(batch);
  }

  public static void RedirectToBatch(Batch batch)
  {
    if (batch == null)
      throw new ArgumentNullException(nameof (batch));
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXSelectBase<Batch>) instance.BatchModule).Current = batch;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Batch");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  public static void SetReclassTranWarningsIfNeed(PXCache cache, GLTran tran)
  {
    string str = (string) null;
    if (tran.Reclassified.GetValueOrDefault())
      str = "The transaction has been reclassified.";
    if (JournalEntry.HasUnreleasedReclassTran(tran))
      str = "Unreleased reclassification batch exists for this transaction.";
    if (str == null)
      return;
    cache.RaiseExceptionHandling<GLTran.reclassBatchNbr>((object) tran, (object) null, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 3));
  }

  public static bool IsTransactionReclassifiable(
    GLTran tran,
    string batchType,
    string ledgerBalanceType,
    int? nonProjectID)
  {
    return (tran.ReclassBatchNbr == null || tran.ReclassBatchNbr != null && tran.Reclassified.GetValueOrDefault() && tran.CuryReclassRemainingAmt.GetValueOrDefault() != 0M) && !tran.IsReclassReverse.GetValueOrDefault() && tran.Released.GetValueOrDefault() && !tran.ReclassificationProhibited.GetValueOrDefault() && !tran.IsInterCompany.GetValueOrDefault() && JournalEntry.IsModuleReclassifiable(tran.Module) && JournalEntry.IsBatchTypeReclassifiable(batchType) && JournalEntry.LedgerBalanceTypeAllowReclassification(ledgerBalanceType) && !JournalEntry.IsTransactionHasZeroAmount(tran) && !JournalEntry.HasUnreleasedReclassTran(tran);
  }

  public static bool IsTransactionHasZeroAmount(GLTran tran)
  {
    Decimal? debitAmt = tran.DebitAmt;
    Decimal num1 = 0M;
    if (debitAmt.GetValueOrDefault() == num1 & debitAmt.HasValue)
    {
      Decimal? creditAmt = tran.CreditAmt;
      Decimal num2 = 0M;
      if (creditAmt.GetValueOrDefault() == num2 & creditAmt.HasValue)
      {
        Decimal? curyDebitAmt = tran.CuryDebitAmt;
        Decimal num3 = 0M;
        if (curyDebitAmt.GetValueOrDefault() == num3 & curyDebitAmt.HasValue)
        {
          Decimal? curyCreditAmt = tran.CuryCreditAmt;
          Decimal num4 = 0M;
          return curyCreditAmt.GetValueOrDefault() == num4 & curyCreditAmt.HasValue;
        }
      }
    }
    return false;
  }

  public static bool HasUnreleasedReclassTran(GLTran tran)
  {
    return (tran.ReclassBatchModule != null || tran.ReclassBatchNbr != null) && tran.ReclassTotalCount.GetValueOrDefault() != tran.ReclassReleasedCount.GetValueOrDefault();
  }

  public static bool IsBatchTypeReclassifiable(string batchType)
  {
    return batchType != "T" && batchType != "C" && batchType != "A";
  }

  public static bool IsModuleReclassifiable(string module) => module != "CM";

  public static bool LedgerBalanceTypeAllowReclassification(string ledgerType)
  {
    return ledgerType == "A" || ledgerType == null;
  }

  public static bool IsBatchReclassifiable(Batch batch, Ledger ledger)
  {
    return batch.Released.GetValueOrDefault() && JournalEntry.IsBatchTypeReclassifiable(batch.BatchType) && JournalEntry.LedgerBalanceTypeAllowReclassification(ledger.BalanceType) && JournalEntry.IsModuleReclassifiable(batch.Module);
  }

  public static bool IsReclassifacationTran(GLTran tran) => tran.ReclassSourceTranBatchNbr != null;

  public static bool CanShowReclassHistory(GLTran tran, string batchType)
  {
    return batchType == "RCL" || tran.ReclassBatchNbr != null;
  }

  protected virtual StateControllerBase GetStateController(Batch batch)
  {
    if (this.IsBatchReadonly(batch))
      return (StateControllerBase) new ReadonlyStateController(this);
    if (batch.BatchType == "RCL")
      return (StateControllerBase) new ReclassStateController(this);
    return batch.BatchType == "T" ? (StateControllerBase) new TrialBalanceStateController(this) : (StateControllerBase) new CommonTypeStateController(this);
  }

  protected virtual void ValidateGLTranFinPeriodByModule(GLTran tran)
  {
    if (tran.Module == "AP")
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) tran.SingleToArray<GLTran>(), typeof (OrganizationFinPeriod.aPClosed));
    else if (tran.Module == "AR")
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) tran.SingleToArray<GLTran>(), typeof (OrganizationFinPeriod.aRClosed));
    else if (tran.Module == "CA")
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) tran.SingleToArray<GLTran>(), typeof (OrganizationFinPeriod.cAClosed));
    else if (tran.Module == "FA")
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) tran.SingleToArray<GLTran>(), typeof (OrganizationFinPeriod.fAClosed));
    else if (tran.Module == "IN")
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) tran.SingleToArray<GLTran>(), typeof (OrganizationFinPeriod.iNClosed));
    else
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) tran.SingleToArray<GLTran>());
  }

  public bool AskUserApprovalToReverseBatch(Batch origDoc)
  {
    if (this.GetReversingBatchesCount(origDoc) >= 1)
      return ((PXSelectBase) this.BatchModule).View.Ask(PXMessages.LocalizeNoPrefix("One or more reversing batches already exist. To review the reversing entries, click the link in the Reversing Batches box. Do you want to proceed with reversing the batch?"), (MessageButtons) 4) == 6;
    return !origDoc.AutoReverse.GetValueOrDefault() || ((PXSelectBase) this.BatchModule).View.Ask(PXMessages.LocalizeNoPrefix("The batch has the Auto Reversing check box selected and will be reversed by the system on posting or on period closing depending on the configuration on the General Ledger Preferences (GL102000) form. Do you want to continue?"), (MessageButtons) 4) == 6;
  }

  [Flags]
  public enum Modes
  {
    Reclassification = 1,
    RecognizingVAT = 2,
    TaxReporting = 4,
    InvoiceReclassification = 8,
  }

  public class JournalEntryDocumentExtension : DocumentWithLinesGraphExtension<JournalEntry>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>((PXSelectBase) this.Base.BatchModule);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.GLTranModuleBatNbr);
    }

    protected override DocumentMapping GetDocumentMapping()
    {
      return new DocumentMapping(typeof (Batch))
      {
        HeaderTranPeriodID = typeof (Batch.tranPeriodID),
        HeaderDocDate = typeof (Batch.dateEntered)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (GLTran));
    }

    protected override bool ShouldUpdateLinesOnDocumentUpdated(PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document> e)
    {
      return base.ShouldUpdateLinesOnDocumentUpdated(e) || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow);
    }

    protected override void ProcessLineOnDocumentUpdated(
      PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document> e,
      PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine line)
    {
      base.ProcessLineOnDocumentUpdated(e, line);
      if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow) || this.Base.Mode.HasFlag((Enum) JournalEntry.Modes.Reclassification))
        return;
      ((PXSelectBase) this.Lines).Cache.SetDefaultExt<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine.tranDate>((object) line);
    }
  }

  /// <exclude />
  public class ForceGLTranFinPeriodsFromBatch : PXGraphExtension<JournalEntry>
  {
    public void _(PX.Data.Events.RowInserting<GLTran> e)
    {
      Batch current = ((PXSelectBase<Batch>) this.Base.BatchModule).Current;
      GLTran row = e.Row;
      if (row == null || current == null)
        return;
      row.TranPeriodID = current.TranPeriodID;
      FinPeriodIDAttribute.SetPeriodsByMaster<GLTran.finPeriodID>(((PXSelectBase) this.Base.GLTranModuleBatNbr).Cache, (object) row, row.TranPeriodID);
      JournalEntry.AssertBatchAndDetailHaveSameMasterPeriod((PXSelectBase<GLTran>) this.Base.GLTranModuleBatNbr, current, row);
    }

    public void _(PX.Data.Events.RowUpdating<GLTran> e)
    {
      if (((PXSelectBase<Batch>) this.Base.BatchModule).Current == null || e.Row == null)
        return;
      JournalEntry.AssertBatchAndDetailHaveSameMasterPeriod((PXSelectBase<GLTran>) this.Base.GLTranModuleBatNbr, ((PXSelectBase<Batch>) this.Base.BatchModule).Current, e.Row);
    }
  }

  public class JournalEntryGLCATranToExpenseReceiptMatchingGraphExtension : 
    CABankTransactionsMaint.GLCATranToExpenseReceiptMatchingGraphExtension<JournalEntry>
  {
  }

  public class JournalEntryContextExt : GraphContextExtention<JournalEntry>
  {
  }

  public class RedirectToSourceDocumentFromJournalEntryExtension : 
    RedirectToSourceDocumentExtensionBase<JournalEntry>
  {
    public static bool IsActive() => true;
  }

  [Flags]
  public enum TranBuildingModes
  {
    None = 0,
    SetLinkToOriginal = 1,
  }
}
