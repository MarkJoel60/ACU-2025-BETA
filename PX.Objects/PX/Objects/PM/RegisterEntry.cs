// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RegisterEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class RegisterEntry : PXGraph<
#nullable disable
RegisterEntry, PMRegister>, PXImportAttribute.IPXPrepareItems
{
  [PXHidden]
  public PXSelect<PMProject> Project;
  [PXHidden]
  public PXSetup<PX.Objects.GL.Company> Company;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> dummy;
  [PXHidden]
  public PXSelect<PX.Objects.GL.Account> accountDummy;
  public PXSelect<PMRegister, Where<PMRegister.module, Equal<Optional<PMRegister.module>>>> Document;
  [PXImport(typeof (PMRegister))]
  public FbqlSelect<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMAccountGroup>.On<BqlOperand<
  #nullable enable
  PMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.accountGroupID>>>, FbqlJoins.Left<RegisterReleaseProcess.OffsetPMAccountGroup>.On<BqlOperand<
  #nullable enable
  RegisterReleaseProcess.OffsetPMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.offsetAccountGroupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.tranType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMRegister.module, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PMTran.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMRegister.refNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  RegisterReleaseProcess.OffsetPMAccountGroup.groupID, 
  #nullable disable
  IsNull>>>>.Or<Match<RegisterReleaseProcess.OffsetPMAccountGroup, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMAccountGroup.groupID, 
  #nullable disable
  IsNull>>>>.Or<Match<PMAccountGroup, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  PMTran>.View Transactions;
  public PXFilter<RegisterEntry.PMTranTotal> Totals;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo> CuryInfo;
  public PXSelect<PMAllocationSourceTran, Where<PMAllocationSourceTran.allocationID, Equal<Required<PMAllocationSourceTran.allocationID>>, And<PMAllocationSourceTran.tranID, Equal<Required<PMAllocationSourceTran.tranID>>>>> SourceTran;
  public PXSelect<PMAllocationAuditTran> AuditTrans;
  public PXSelect<PMRecurringItemAccum> RecurringItems;
  public PXSelect<PMTaskAllocTotalAccum> AllocationTotals;
  public PXSetupOptional<PMSetup> Setup;
  public PXSetup<EPSetup> epSetup;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public CMSetupSelect CMSetup;
  public PXSelect<PMTimeActivity> Activities;
  public PXSelect<ContractDetailAcum> ContractDetails;
  public PXAction<PMRegister> curyToggle;
  public PXAction<PMRegister> release;
  public PXAction<PMRegister> reverse;
  public PXAction<PMRegister> viewProject;
  public PXAction<PMRegister> viewTask;
  public PXAction<PMRegister> viewAllocationSorce;
  public PXAction<PMRegister> viewInventory;
  public PXAction<PMRegister> viewCustomer;
  public PXAction<PMRegister> selectProjectRate;
  public PXAction<PMRegister> selectBaseRate;
  protected bool ManualCurrencyInfoCreation;
  private bool configureOnImport;

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMTran.inventoryID>>>>))]
  [PMUnit(typeof (PMTran.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.uOM> e)
  {
  }

  [PXMergeAttributes]
  [PXDimensionSelector("EMPLOYEE", typeof (Search<PX.Objects.CR.Standalone.EPEmployee.bAccountID>), typeof (PX.Objects.CR.Standalone.EPEmployee.acctCD), new System.Type[] {typeof (PX.Objects.CR.Standalone.EPEmployee.bAccountID), typeof (PX.Objects.CR.Standalone.EPEmployee.acctCD), typeof (PX.Objects.CR.Standalone.EPEmployee.acctName), typeof (PX.Objects.CR.Standalone.EPEmployee.departmentID), typeof (PX.Objects.CR.Standalone.EPEmployee.vStatus)}, DescriptionField = typeof (BAccountCRM.acctName), Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.resourceID> e)
  {
  }

  [PXMergeAttributes]
  [BaseProjectTask(typeof (PMTran.projectID), AllowInactive = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.taskID> e)
  {
  }

  public virtual IEnumerable totals()
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
      num1 += pmTran.Qty.GetValueOrDefault();
      num2 += pmTran.BillableQty.GetValueOrDefault();
      num3 += pmTran.Amount.GetValueOrDefault();
    }
    ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current.AmtTotal = new Decimal?(num3);
    ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current.QtyTotal = new Decimal?(num1);
    ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current.BillableQtyTotal = new Decimal?(num2);
    yield return (object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current;
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public RegisterEntry()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      AutoNumberAttribute.SetNumberingId<PMRegister.refNbr>(((PXSelectBase) this.Document).Cache, ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.UsageNumberingID);
    ((PXAction) this.selectBaseRate).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
    ((PXAction) this.selectProjectRate).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
    ((PXAction) this.curyToggle).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.Standalone.EPEmployee.vStatus>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Standalone.EPEmployee)], (object) null, false);
  }

  public virtual void Persist()
  {
    this.FillDataInMigrationMode();
    ((PXGraph) this).Persist();
  }

  private void FillDataInMigrationMode()
  {
    if (!this.MigrationMode)
      return;
    foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
      pmTran.OrigModule = ((PXSelectBase<PMRegister>) this.Document).Current.Module;
      pmTran.OrigTranType = PMOrigDocType.GetOrigDocType(((PXSelectBase<PMRegister>) this.Document).Current.OrigDocType);
    }
  }

  public virtual PMTran InsertTransactionWithManuallyChangedCurrencyInfo(PMTran transaction)
  {
    if (this.ManualCurrencyInfoCreation)
      return ((PXSelectBase<PMTran>) this.Transactions).Insert(transaction);
    this.ManualCurrencyInfoCreation = true;
    try
    {
      return ((PXSelectBase<PMTran>) this.Transactions).Insert(transaction);
    }
    finally
    {
      this.ManualCurrencyInfoCreation = false;
    }
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  /// <summary>Gets the source for the generated PMTran.AccountID</summary>
  public string ExpenseAccountSource
  {
    get
    {
      string expenseAccountSource = "I";
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseAccountSource))
        expenseAccountSource = pmSetup.ExpenseAccountSource;
      return expenseAccountSource;
    }
  }

  public string ExpenseSubMask
  {
    get
    {
      string expenseSubMask = (string) null;
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseSubMask))
        expenseSubMask = pmSetup.ExpenseSubMask;
      return expenseSubMask;
    }
  }

  public string ExpenseAccrualAccountSource
  {
    get
    {
      string accrualAccountSource = "I";
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseAccrualAccountSource))
        accrualAccountSource = pmSetup.ExpenseAccrualAccountSource;
      return accrualAccountSource;
    }
  }

  public string ExpenseAccrualSubMask
  {
    get
    {
      string expenseAccrualSubMask = (string) null;
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseAccrualSubMask))
        expenseAccrualSubMask = pmSetup.ExpenseAccrualSubMask;
      return expenseAccrualSubMask;
    }
  }

  public bool MigrationMode
  {
    get
    {
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return pmSetup != null && pmSetup.MigrationMode.GetValueOrDefault();
    }
  }

  [PXUIField(DisplayName = "View Base")]
  [PXProcessButton]
  public IEnumerable CuryToggle(PXAdapter adapter)
  {
    if (((PXSelectBase<PMRegister>) this.Document).Current != null)
    {
      int num = ((PXSelectBase) this.Document).Cache.IsDirty ? 1 : 0;
      ((PXSelectBase<PMRegister>) this.Document).Current.IsBaseCury = new bool?(!((PXSelectBase<PMRegister>) this.Document).Current.IsBaseCury.GetValueOrDefault());
      ((PXSelectBase<PMRegister>) this.Document).Update(((PXSelectBase<PMRegister>) this.Document).Current);
      if (num == 0 && ((PXSelectBase) this.Document).Cache.IsDirty)
        ((PXSelectBase) this.Document).Cache.IsDirty = false;
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Release")]
  [PXProcessButton]
  public IEnumerable Release(PXAdapter adapter)
  {
    if (!this.IsAllPMTranLinesVisible(((PXSelectBase<PMRegister>) this.Document).Current))
      throw new PXException("The transaction cannot be released because it includes one or multiple lines that are hidden; you do not have permissions to view and edit these lines.");
    this.ReleaseDocument(((PXSelectBase<PMRegister>) this.Document).Current);
    yield return (object) ((PXSelectBase<PMRegister>) this.Document).Current;
  }

  public virtual void ReleaseDocument(PMRegister doc)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RegisterEntry.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new RegisterEntry.\u003C\u003Ec__DisplayClass54_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.doc = doc;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass540.doc == null || cDisplayClass540.doc.Released.GetValueOrDefault())
      return;
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass540, __methodptr(\u003CReleaseDocument\u003Eb__0)));
  }

  [PXUIField(DisplayName = "Reverse")]
  [PXProcessButton(Tooltip = "Reverses Released Transactions")]
  public virtual IEnumerable Reverse(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RegisterEntry.\u003C\u003Ec__DisplayClass56_0 cDisplayClass560 = new RegisterEntry.\u003C\u003Ec__DisplayClass56_0();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass560.registerEntry = PXGraph.CreateInstance<RegisterEntry>();
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase<PMRegister>) cDisplayClass560.registerEntry.Document).Current = ((PXSelectBase<PMRegister>) this.Document).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass560.redirectToResult = !((PXGraph) this).IsImport && !((PXGraph) this).IsContractBasedAPI;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass560, __methodptr(\u003CReverse\u003Eb__0)));
    return adapter.Get();
  }

  protected virtual void ReverseCurrentDocument(bool redirectToResult)
  {
    if (((PXSelectBase<PMRegister>) this.Document).Current == null)
      return;
    PMRegister pmRegister = this.ReverseDocument(((PXSelectBase<PMRegister>) this.Document).Current);
    if (pmRegister != null & redirectToResult)
    {
      RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
      ((PXSelectBase<PMRegister>) instance.Document).Current = pmRegister;
      throw new PXRedirectRequiredException((PXGraph) instance, "Open Reversal");
    }
  }

  public virtual PMRegister ReverseDocument(PMRegister document)
  {
    if (document == null)
      return (PMRegister) null;
    ((PXSelectBase<PMRegister>) this.Document).Current = document;
    if (!this.IsReversableDocument(((PXSelectBase<PMRegister>) this.Document).Current))
      return (PMRegister) null;
    PXSelect<PMRegister, Where<PMRegister.module, Equal<Current<PMRegister.module>>, And<PMRegister.origNoteID, Equal<Current<PMRegister.noteID>>, And<PMRegister.origDocType, Equal<PMOrigDocType.reversal>>>>> pxSelect = new PXSelect<PMRegister, Where<PMRegister.module, Equal<Current<PMRegister.module>>, And<PMRegister.origNoteID, Equal<Current<PMRegister.noteID>>, And<PMRegister.origDocType, Equal<PMOrigDocType.reversal>>>>>((PXGraph) this);
    bool? isAllocation = ((PXSelectBase<PMRegister>) this.Document).Current.IsAllocation;
    if (isAllocation.GetValueOrDefault())
    {
      PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) pxSelect).Select(Array.Empty<object>()));
      if (pmRegister != null)
        throw new PXException("The allocation transaction cannot be reversed because the {0} reversal allocation for this transaction already exists.", new object[1]
        {
          (object) pmRegister.RefNbr
        });
    }
    if (!this.IsAllPMTranLinesVisible(((PXSelectBase<PMRegister>) this.Document).Current))
      throw new PXException("The transaction cannot be reversed because it includes one line or multiple lines that are hidden; you do not have permissions to view and edit these lines.");
    using (new PXConnectionScope())
    {
      List<ProcessInfo<PX.Objects.GL.Batch>> infoList;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
        // ISSUE: method pointer
        ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.inventoryID>(new PXFieldVerifying((object) this, __methodptr(SuppressFieldVerifying)));
        PMRegister pmRegister1 = (PMRegister) ((PXSelectBase) instance.Document).Cache.Insert();
        PMRegister pmRegister2 = pmRegister1;
        isAllocation = ((PXSelectBase<PMRegister>) this.Document).Current.IsAllocation;
        string str = isAllocation.GetValueOrDefault() ? PXMessages.LocalizeNoPrefix("Allocation Reversal") : PXMessages.LocalizeNoPrefix("Project transaction reversal");
        pmRegister2.Description = str;
        pmRegister1.OrigDocType = "RV";
        pmRegister1.OrigNoteID = ((PXSelectBase<PMRegister>) this.Document).Current.NoteID;
        foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.Transactions).Select(Array.Empty<object>()))
        {
          PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
          RegisterEntry registerEntry = instance;
          PMTran tran = pmTran;
          isAllocation = ((PXSelectBase<PMRegister>) this.Document).Current.IsAllocation;
          int num = isAllocation.GetValueOrDefault() ? 1 : 0;
          registerEntry.ReverseTransaction(tran, num != 0);
        }
        ((PXAction) instance.Save).Press();
        if (!RegisterRelease.ReleaseWithoutPost(new List<PMRegister>()
        {
          pmRegister1
        }, false, out infoList))
          throw new PXException("One or more documents could not be released.");
        ((PXSelectBase) this.Transactions).Cache.AllowUpdate = true;
        if (((PXSelectBase<PMRegister>) this.Document).Current.IsAllocation.GetValueOrDefault())
        {
          foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.Transactions).Select(Array.Empty<object>()))
            this.UnallocateTran(PXResult<PMTran>.op_Implicit(pxResult));
        }
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
      }
      if (!RegisterRelease.Post(infoList, false))
        throw new PXException("One or more documents was released but could not be posted.");
    }
    return PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) pxSelect).Select(Array.Empty<object>()));
  }

  protected virtual bool IsReversableDocument(PMRegister document)
  {
    if (!((PXSelectBase<PMRegister>) this.Document).Current.Released.GetValueOrDefault())
      return false;
    if (((PXSelectBase<PMRegister>) this.Document).Current.IsMigratedRecord.GetValueOrDefault())
      return true;
    return ((PXSelectBase<PMRegister>) this.Document).Current.Module == "PM" && ((PXSelectBase<PMRegister>) this.Document).Current.OrigDocType != "TC";
  }

  public virtual void ReverseTransaction(PMTran tran, bool isAllocation)
  {
    PMTran pmTran = PXGraph.CreateInstance<PMBillEngine>().ReverseTran(tran, !isAllocation);
    if (isAllocation)
      this.UpdateReversalTransactionDateOnAllocationSetting(tran, pmTran);
    PMTran reversal = this.InsertTransactionWithManuallyChangedCurrencyInfo(pmTran);
    this.ValidateOffestAccountGroup(tran, reversal);
    tran.ExcludedFromBilling = new bool?(true);
    tran.ExcludedFromBillingReason = PXMessages.LocalizeNoPrefix("Reversed");
    RegisterReleaseProcess.SubtractFromUnbilledSummary((PXGraph) this, tran);
    ((PXSelectBase<PMTran>) this.Transactions).Update(tran);
  }

  public void ValidateOffestAccountGroup(PMTran tran, PMTran reversal)
  {
    RegisterEntry.UpdateOffsetAccountId((PXGraph) this, ((PXSelectBase) this.Transactions).Cache, reversal);
    reversal = ((PXSelectBase) this.Transactions).Cache.Update((object) reversal) as PMTran;
    int? offsetAccountGroupId1 = reversal.OffsetAccountGroupID;
    int? offsetAccountGroupId2 = tran.OffsetAccountGroupID;
    if (!(offsetAccountGroupId1.GetValueOrDefault() == offsetAccountGroupId2.GetValueOrDefault() & offsetAccountGroupId1.HasValue == offsetAccountGroupId2.HasValue) && tran.OffsetAccountID.HasValue)
      throw new PXException("The project transaction cannot be released because the account group has been changed for an account selected in a project transaction line. To be able to release the transaction, assign the {1} account group to the {0} account.", new object[2]
      {
        (object) PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXViewOf<PX.Objects.GL.Account>.BasedOn<SelectFromBase<PX.Objects.GL.Account, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) tran.OffsetAccountID
        })).AccountCD,
        (object) this.GetAccountGroup(tran.OffsetAccountGroupID).GroupCD
      });
  }

  private void UpdateReversalTransactionDateOnAllocationSetting(PMTran original, PMTran reversal)
  {
    if (original.AllocationID == null)
    {
      reversal.Date = new DateTime?();
      reversal.FinPeriodID = (string) null;
    }
    else
    {
      PMAllocationDetail allocationDetail = PXResultset<PMAllocationDetail>.op_Implicit(PXSelectBase<PMAllocationDetail, PXViewOf<PMAllocationDetail>.BasedOn<SelectFromBase<PMAllocationDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAllocationDetail.allocationID, IBqlString>.IsEqual<P.AsString>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) original.AllocationID
      }));
      if (allocationDetail == null)
        return;
      bool? dateFromOriginal = allocationDetail.UseReversalDateFromOriginal;
      bool flag = false;
      if (!(dateFromOriginal.GetValueOrDefault() == flag & dateFromOriginal.HasValue))
        return;
      reversal.Date = new DateTime?();
      reversal.FinPeriodID = (string) null;
    }
  }

  private PMAccountGroup GetAccountGroup(int? groupId)
  {
    return PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXViewOf<PMAccountGroup>.BasedOn<SelectFromBase<PMAccountGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAccountGroup.groupID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) groupId
    }));
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTran>) this.Transactions).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<PMTran>) this.Transactions).Current.ProjectID);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMTran.projectID>>, And<PMTask.taskID, Equal<Current<PMTran.taskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Allocation Source")]
  [PXButton]
  public IEnumerable ViewAllocationSorce(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTran>) this.Transactions).Current != null)
    {
      AllocationAudit instance = PXGraph.CreateInstance<AllocationAudit>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<AllocationPMTran>) instance.destantion).Current.TranID = ((PXSelectBase<PMTran>) this.Transactions).Current.TranID;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Allocation Source");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) ((PXSelectBase<PMTran>) this.Transactions).Current?.InventoryID));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCustomer(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToCustomerScreen(PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PMTran.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  [PXUIField(DisplayName = "Select Project Currency Rate")]
  [PXButton]
  public IEnumerable SelectProjectRate(PXAdapter adapter)
  {
    if (((PXSelectBase) this.Transactions).Cache.Cached.Count() > 0L)
    {
      RegisterEntry.MultiCurrency extension = ((PXGraph) this).GetExtension<RegisterEntry.MultiCurrency>();
      ((PXSelectBase) extension.currencyinfo).Cache.ClearQueryCache();
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.ProjectCuryInfo).AskExt();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Select Base Currency Rate")]
  [PXButton]
  public IEnumerable SelectBaseRate(PXAdapter adapter)
  {
    if (((PXSelectBase) this.Transactions).Cache.Cached.Count() > 0L)
    {
      RegisterEntry.MultiCurrency extension = ((PXGraph) this).GetExtension<RegisterEntry.MultiCurrency>();
      ((PXSelectBase) extension.currencyinfo).Cache.ClearQueryCache();
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.BaseCuryInfo).AskExt();
    }
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRegister, PMRegister.isBaseCury> e)
  {
    if (e.Row == null)
      return;
    ((PXGraph) this).Accessinfo.CuryViewState = e.Row.IsBaseCury.GetValueOrDefault();
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMRegister, PMRegister.hold> e)
  {
    if (e.Row.Released.GetValueOrDefault())
      return;
    if (e.Row.Hold.GetValueOrDefault())
      e.Row.Status = "H";
    else
      e.Row.Status = "B";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocType> e)
  {
    if (e.Row?.OrigDocType == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocType>>) e).Cache.SetValue<PMRegister.origDocNbr>((object) e.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocType>>) e).Cache.SetValue<PMRegister.origNoteID>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocNbr> e)
  {
    if (e.Row == null)
      return;
    string origDocNbr = e.Row.OrigDocNbr;
    string origDocType = PMOrigDocType.GetOrigDocType(e.Row.OrigDocType);
    if (string.IsNullOrEmpty(origDocType) || string.IsNullOrEmpty(origDocNbr))
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocNbr>>) e).Cache.SetValue<PMRegister.origDocNbr>((object) e.Row, (object) null);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocNbr>>) e).Cache.SetValue<PMRegister.origNoteID>((object) e.Row, (object) null);
    }
    else
    {
      PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, origDocType, origDocNbr);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocNbr>>) e).Cache.SetValue<PMRegister.origDocNbr>((object) e.Row, (object) arInvoice?.RefNbr);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRegister, PMRegister.origDocNbr>>) e).Cache.SetValue<PMRegister.origNoteID>((object) e.Row, (object) (Guid?) arInvoice?.NoteID);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRegister, PMRegister.isMigratedRecord> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRegister, PMRegister.isMigratedRecord>, PMRegister, object>) e).NewValue = (object) this.MigrationMode;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMRegister> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.curyToggle).SetCaption(e.Row.IsBaseCury.GetValueOrDefault() ? "View Cury" : "View Base");
    PXUIFieldAttribute.SetEnabled<PMRegister.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, !e.Row.Released.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMRegister.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, !e.Row.Released.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMRegister.status>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, !e.Row.Released.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMRegister.hold>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, !e.Row.Released.GetValueOrDefault());
    ((PXSelectBase) this.Document).Cache.AllowUpdate = !e.Row.Released.GetValueOrDefault() && (this.MigrationMode || e.Row.Module == "PM");
    PXCache cache1 = ((PXSelectBase) this.Document).Cache;
    bool? nullable = e.Row.Released;
    int num1 = nullable.GetValueOrDefault() ? 0 : (this.MigrationMode ? 1 : (e.Row.Module == "PM" ? 1 : 0));
    cache1.AllowDelete = num1 != 0;
    ((PXAction) this.Insert).SetEnabled(this.MigrationMode || e.Row.Module == "PM");
    PXAction<PMRegister> release = this.release;
    nullable = e.Row.Released;
    int num2;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.Hold;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    ((PXAction) release).SetEnabled(num2 != 0);
    PXCache cache2 = ((PXSelectBase) this.Transactions).Cache;
    nullable = e.Row.Released;
    int num3;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.IsAllocation;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    cache2.AllowDelete = num3 != 0;
    PXCache cache3 = ((PXSelectBase) this.Transactions).Cache;
    nullable = e.Row.Released;
    int num4;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.IsAllocation;
      if (!nullable.GetValueOrDefault())
      {
        num4 = this.MigrationMode ? 1 : (e.Row.Module == "PM" ? 1 : 0);
        goto label_11;
      }
    }
    num4 = 0;
label_11:
    cache3.AllowInsert = num4 != 0;
    PXCache cache4 = ((PXSelectBase) this.Transactions).Cache;
    nullable = e.Row.Released;
    int num5 = !nullable.GetValueOrDefault() ? 1 : 0;
    cache4.AllowUpdate = num5 != 0;
    ((PXAction) this.reverse).SetEnabled(this.IsReversableDocument(e.Row));
    ((PXAction) this.viewAllocationSorce).SetEnabled(e.Row.OrigDocType == "AL");
    ((PXAction) this.curyToggle).SetEnabled(true);
    ((PXAction) this.selectProjectRate).SetEnabled(true);
    ((PXAction) this.selectBaseRate).SetEnabled(true);
    bool flag1 = e.Row.Module == "AR";
    bool flag2 = e.Row.Module == "PM";
    nullable = e.Row.Released;
    bool valueOrDefault = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<PMRegister.origDocType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, this.MigrationMode | flag2);
    PXUIFieldAttribute.SetEnabled<PMRegister.origDocType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, this.MigrationMode & flag1 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMRegister.origDocNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, this.MigrationMode & flag1);
    PXUIFieldAttribute.SetEnabled<PMRegister.origDocNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, this.MigrationMode & flag1 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<PMRegister.origNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, this.MigrationMode && !flag1 || !this.MigrationMode & flag2);
    PXUIFieldAttribute.SetEnabled<PMRegister.origNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, false);
    PXStringListAttribute.SetList<PMRegister.origDocType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRegister>>) e).Cache, (object) e.Row, this.MigrationMode & flag1 ? (PXStringListAttribute) new PMOrigDocType.ListARAttribute() : (PXStringListAttribute) new PMOrigDocType.ListAttribute());
    if (this.IsAllPMTranLinesVisible(e.Row))
      return;
    ((PXSelectBase) this.Totals).Cache.RaiseExceptionHandling<RegisterEntry.PMTranTotal.qtyTotal>((object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current, (object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current.QtyTotal, (Exception) new PXSetPropertyException<RegisterEntry.PMTranTotal.qtyTotal>("One or multiple lines are hidden in the document because you do not have permissions to view them. This value is calculated based on the displayed lines.", (PXErrorLevel) 2));
    ((PXSelectBase) this.Totals).Cache.RaiseExceptionHandling<RegisterEntry.PMTranTotal.billableQtyTotal>((object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current, (object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current.BillableQtyTotal, (Exception) new PXSetPropertyException<RegisterEntry.PMTranTotal.billableQtyTotal>("One or multiple lines are hidden in the document because you do not have permissions to view them. This value is calculated based on the displayed lines.", (PXErrorLevel) 2));
    ((PXSelectBase) this.Totals).Cache.RaiseExceptionHandling<RegisterEntry.PMTranTotal.amtTotal>((object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current, (object) ((PXSelectBase<RegisterEntry.PMTranTotal>) this.Totals).Current.AmtTotal, (Exception) new PXSetPropertyException<RegisterEntry.PMTranTotal.amtTotal>("One or multiple lines are hidden in the document because you do not have permissions to view them. This value is calculated based on the displayed lines.", (PXErrorLevel) 2));
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMRegister> e)
  {
    if (e.Row == null || e.Row.Released.GetValueOrDefault() || !(e.Row.OrigDocType == "TC") || !e.Row.OrigNoteID.HasValue)
      return;
    EPTimeCard timeCard = PXResultset<EPTimeCard>.op_Implicit(PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.noteID, Equal<Required<EPTimeCard.noteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.OrigNoteID
    }));
    if (timeCard == null)
      return;
    ((PXGraph) this).Views.Caches.Add(typeof (EPTimeCard));
    this.UnreleaseTimeCard(timeCard);
  }

  protected virtual void UnreleaseTimeCard(EPTimeCard timeCard)
  {
    timeCard.IsReleased = new bool?(false);
    timeCard.Status = "A";
    ((PXGraph) this).Caches[typeof (EPTimeCard)].Update((object) timeCard);
  }

  protected virtual void PMTran_BranchID_FieldUpdated(PX.Data.Events.FieldUpdated<PMTran, PMTran.branchID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.branchID>>) e).Cache.SetDefaultExt<PMTran.finPeriodID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.bAccountID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.bAccountID>>) e).Cache.SetDefaultExt<PMTran.locationID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.inventoryID> e)
  {
    if (e.Row != null && string.IsNullOrEmpty(e.Row.Description))
    {
      int? nullable = e.Row.InventoryID;
      if (nullable.HasValue)
      {
        nullable = e.Row.InventoryID;
        int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
        if (!(nullable.GetValueOrDefault() == emptyInventoryId & nullable.HasValue))
        {
          PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) e.Row.InventoryID
          }));
          if (inventoryItem != null)
          {
            e.Row.Description = inventoryItem.Descr;
            PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMTran.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) e.Row.ProjectID
            }));
            if (pmProject != null)
            {
              nullable = pmProject.CustomerID;
              if (nullable.HasValue)
              {
                PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) pmProject.CustomerID
                }));
                if (customer != null && !string.IsNullOrEmpty(customer.LocaleName))
                  e.Row.Description = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, "Descr", customer.LocaleName);
              }
            }
            ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.inventoryID>>) e).Cache.SetDefaultExt<PMTran.costCodeID>((object) e.Row);
          }
        }
      }
    }
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.inventoryID>>) e).Cache.SetDefaultExt<PMTran.uOM>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.taskID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.taskID>>) e).Cache.SetDefaultExt<PMTran.costCodeID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.qty> e)
  {
    if (e.Row == null || !e.Row.Billable.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.qty>>) e).Cache.SetDefaultExt<PMTran.billableQty>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.billable> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.Billable.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.billable>>) e).Cache.SetDefaultExt<PMTran.billableQty>((object) e.Row);
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.billable>>) e).Cache.SetValueExt<PMTran.billableQty>((object) e.Row, (object) 0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTran, PMTran.billableQty> e)
  {
    if (e.Row == null || !e.Row.Billable.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTran, PMTran.billableQty>, PMTran, object>) e).NewValue = (object) e.Row.Qty;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.billableQty> e)
  {
    if (e.Row == null)
      return;
    Decimal? billableQty = e.Row.BillableQty;
    Decimal num = 0M;
    if (billableQty.GetValueOrDefault() == num & billableQty.HasValue)
      return;
    this.SubtractUsage(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.billableQty>>) e).Cache, e.Row, (Decimal?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMTran, PMTran.billableQty>, PMTran, object>) e).OldValue, e.Row.UOM);
    this.AddUsage(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.billableQty>>) e).Cache, e.Row, e.Row.BillableQty, e.Row.UOM);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.uOM> e)
  {
    if (e.Row == null)
      return;
    Decimal? billableQty = e.Row.BillableQty;
    Decimal num = 0M;
    if (billableQty.GetValueOrDefault() == num & billableQty.HasValue)
      return;
    this.SubtractUsage(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.uOM>>) e).Cache, e.Row, e.Row.BillableQty, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMTran, PMTran.uOM>, PMTran, object>) e).OldValue);
    this.AddUsage(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.uOM>>) e).Cache, e.Row, e.Row.BillableQty, e.Row.UOM);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.date> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.date>>) e).Cache.SetDefaultExt<PMTran.finPeriodID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTran, PMTran.offsetAccountID> e)
  {
    RegisterEntry.UpdateOffsetAccountId((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.offsetAccountID>>) e).Cache, e.Row);
  }

  protected void _(
    PX.Data.Events.FieldUpdated<PMTran, PMTran.accountGroupID> e)
  {
    if (e.Row == null || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMTran, PMTran.accountGroupID>>) e).ExternalCall)
      return;
    this.CheckDebitAccount(e.Row, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.accountGroupID>>) e).Cache);
  }

  public static void UpdateOffsetAccountId(PXGraph graph, PXCache cache, PMTran tran)
  {
    if (tran == null || tran.IsNonGL.GetValueOrDefault())
      return;
    int? offsetAccountId = tran.OffsetAccountID;
    int? nullable = new int?();
    if (offsetAccountId.HasValue)
      nullable = (int?) ((PXResult) ((IQueryable<PXResult<PX.Objects.GL.Account>>) PXSelectBase<PX.Objects.GL.Account, PXViewOf<PX.Objects.GL.Account>.BasedOn<SelectFromBase<PX.Objects.GL.Account, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMAccountGroup>.On<BqlOperand<PX.Objects.GL.Account.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>>.Where<BqlOperand<PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) offsetAccountId
      })).FirstOrDefault<PXResult<PX.Objects.GL.Account>>())?.GetItem<PMAccountGroup>()?.GroupID;
    cache.SetValueExt<PMTran.offsetAccountGroupID>((object) tran, (object) nullable);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PMTran, PMTran.resourceID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran, PMTran.resourceID>, PMTran, object>) e).NewValue == null)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMTran.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.ProjectID
    }));
    if (pmProject == null || !pmProject.RestrictToEmployeeList.GetValueOrDefault())
      return;
    if (PXResultset<EPEmployeeContract>.op_Implicit(PXSelectBase<EPEmployeeContract, PXSelect<EPEmployeeContract, Where<EPEmployeeContract.contractID, Equal<Required<PMTran.projectID>>, And<EPEmployeeContract.employeeID, Equal<Required<EPEmployeeContract.employeeID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) e.Row.ProjectID,
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran, PMTran.resourceID>, PMTran, object>) e).NewValue
    })) == null)
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran, PMTran.resourceID>, PMTran, object>) e).NewValue
      }));
      if (epEmployee != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran, PMTran.resourceID>, PMTran, object>) e).NewValue = (object) epEmployee.AcctCD;
      throw new PXSetPropertyException<PMTran.resourceID>("The specified employee is restricted for the {0} project. Add the {1} employee to the employee list on the Employees tab of the Projects (PM301000) form.", new object[2]
      {
        (object) pmProject.ContractCD.Trim(),
        (object) epEmployee.AcctCD.Trim()
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMTran, PMTran.offsetAccountID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran, PMTran.offsetAccountID>, PMTran, object>) e).NewValue == null)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTran, PMTran.offsetAccountID>, PMTran, object>) e).NewValue
    }));
    int num = ((IQueryable<PXResult<PMAccountGroup>>) PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>, And<Match<PMAccountGroup, Current<AccessInfo.userName>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) account.AccountGroupID
    })).Count<PXResult<PMAccountGroup>>();
    if (account.AccountGroupID.HasValue && num == 0)
      throw new PXSetPropertyException("The account is included in the account group for which you do not have permissions to operate. Select another account.", (PXErrorLevel) 4)
      {
        ErrorValue = (object) account.AccountCD
      };
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.projectID> e)
  {
    if (e.Row == null || !e.Row.ProjectID.HasValue)
      return;
    ((PXGraph) this).GetExtension<RegisterEntry.MultiCurrency>().CalcCuryRatesForProject(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.projectID>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTran> e)
  {
    if (e.Row == null)
      return;
    bool flag = !e.Row.Allocated.GetValueOrDefault() && !e.Row.ExcludedFromAllocation.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PMTran.billableQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) e.Row, e.Row.Billable.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMTran.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PMTran.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PMTran.accountGroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PMTran.accountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PMTran.offsetAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) e.Row, flag);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PMTran> e)
  {
    if (e.Row == null || !((PXGraph) this).IsImportFromExcel && !((PXGraph) this).IsImport && !this.ManualCurrencyInfoCreation)
      return;
    this.configureOnImport = true;
    try
    {
      this.ConfigureCurrencyManually(e.Row);
    }
    finally
    {
      this.configureOnImport = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    if (e.Row == null)
      return;
    e.Cancel = (((PXGraph) this).IsImportFromExcel || ((PXGraph) this).IsImport || this.ManualCurrencyInfoCreation) && !this.configureOnImport;
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMTran> e)
  {
    if (e.Row == null)
      return;
    this.AddAllocatedTotal(e.Row);
    Decimal? billableQty = e.Row.BillableQty;
    Decimal num = 0M;
    if (billableQty.GetValueOrDefault() == num & billableQty.HasValue)
      return;
    this.AddUsage(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMTran>>) e).Cache, e.Row, e.Row.BillableQty, e.Row.UOM);
  }

  protected virtual void ConfigureCurrencyManually(PMTran tran)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Search<PMProject.contractID>((object) tran.ProjectID, Array.Empty<object>()));
    string baseCuryID1 = pmProject.NonProject.GetValueOrDefault() || pmProject.BaseType != "P" ? ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID : pmProject.CuryID;
    string baseCuryID2 = pmProject.NonProject.GetValueOrDefault() || pmProject.BaseType != "P" ? ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID : pmProject.BaseCuryID;
    string curyID = tran.TranCuryID ?? baseCuryID1;
    if (curyID == baseCuryID1)
    {
      PX.Objects.CM.Extensions.CurrencyInfo directRate = this.MultiCurrencyService.CreateDirectRate((PXGraph) this, curyID, tran.Date, "PM");
      tran.ProjectCuryInfoID = directRate.CuryInfoID;
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo rate = this.MultiCurrencyService.CreateRate((PXGraph) this, curyID, baseCuryID1, tran.Date, pmProject.RateTypeID, "PM");
      tran.ProjectCuryInfoID = rate.CuryInfoID;
    }
    if (curyID == baseCuryID2)
    {
      PX.Objects.CM.Extensions.CurrencyInfo directRate = this.MultiCurrencyService.CreateDirectRate((PXGraph) this, curyID, tran.Date, "PM");
      tran.BaseCuryInfoID = directRate.CuryInfoID;
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo rate = this.MultiCurrencyService.CreateRate((PXGraph) this, curyID, baseCuryID2, tran.Date, pmProject.RateTypeID, "PM");
      tran.BaseCuryInfoID = rate.CuryInfoID;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMTran> e)
  {
    if (e.Row != null && e.OldRow != null && !e.Row.Released.GetValueOrDefault())
    {
      Decimal? tranCuryAmount1 = e.Row.TranCuryAmount;
      Decimal? tranCuryAmount2 = e.OldRow.TranCuryAmount;
      if (!(tranCuryAmount1.GetValueOrDefault() == tranCuryAmount2.GetValueOrDefault() & tranCuryAmount1.HasValue == tranCuryAmount2.HasValue))
        goto label_4;
    }
    Decimal? billableQty1 = e.Row.BillableQty;
    Decimal? billableQty2 = e.OldRow.BillableQty;
    if (billableQty1.GetValueOrDefault() == billableQty2.GetValueOrDefault() & billableQty1.HasValue == billableQty2.HasValue)
    {
      Decimal? qty1 = e.Row.Qty;
      Decimal? qty2 = e.OldRow.Qty;
      if (qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue)
        return;
    }
label_4:
    this.SubtractAllocatedTotal(e.OldRow);
    this.AddAllocatedTotal(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMTran> e)
  {
    this.UnallocateTran(e.Row);
    this.UnreleaseActivity(e.Row);
    this.RefreshOriginalTran(e.Row);
  }

  protected virtual void UnallocateTran(PMTran row)
  {
    if (row == null)
      return;
    foreach (PXResult<PMAllocationAuditTran, PMTran> pxResult in ((PXSelectBase<PMAllocationAuditTran>) new PXSelectJoin<PMAllocationAuditTran, InnerJoin<PMTran, On<PMTran.tranID, Equal<PMAllocationAuditTran.sourceTranID>>>, Where<PMAllocationAuditTran.tranID, Equal<Required<PMAllocationAuditTran.tranID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) row.TranID
    }))
    {
      PMAllocationAuditTran allocationAuditTran = PXResult<PMAllocationAuditTran, PMTran>.op_Implicit(pxResult);
      PMTran pmTran = PXResult<PMAllocationAuditTran, PMTran>.op_Implicit(pxResult);
      if (!(pmTran.TranType == row.TranType) || !(pmTran.RefNbr == row.RefNbr))
      {
        pmTran.Allocated = new bool?(false);
        ((PXSelectBase<PMTran>) this.Transactions).Update(pmTran);
      }
      ((PXSelectBase<PMAllocationSourceTran>) this.SourceTran).Delete(PXResultset<PMAllocationSourceTran>.op_Implicit(((PXSelectBase<PMAllocationSourceTran>) this.SourceTran).Select(new object[2]
      {
        (object) allocationAuditTran.AllocationID,
        (object) allocationAuditTran.SourceTranID
      })));
      ((PXSelectBase<PMAllocationAuditTran>) this.AuditTrans).Delete(allocationAuditTran);
    }
    this.SubtractAllocatedTotal(row);
  }

  protected virtual void UnreleaseActivity(PMTran row)
  {
    if (!row.OrigRefID.HasValue || ((PXSelectBase<PMRegister>) this.Document).Current == null || ((PXSelectBase<PMRegister>) this.Document).Current.IsAllocation.GetValueOrDefault())
      return;
    PMTimeActivity pmTimeActivity = PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.noteID, Equal<Required<PMTimeActivity.noteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.OrigRefID
    }));
    if (pmTimeActivity == null)
      return;
    pmTimeActivity.Released = new bool?(false);
    pmTimeActivity.EmployeeRate = new Decimal?();
    ((PXSelectBase<PMTimeActivity>) this.Activities).Update(pmTimeActivity);
  }

  protected virtual void RefreshOriginalTran(PMTran row)
  {
    if (row == null || ((PXSelectBase<PMRegister>) this.Document).Current?.OrigDocType != "WR" || !row.OrigTranID.HasValue)
      return;
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.tranID, Equal<Required<PMTran.origTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.OrigTranID
    }));
    if (pmTran == null)
      return;
    PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.module, Equal<Required<PMTran.tranType>>, And<PMRegister.refNbr, Equal<Required<PMTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) pmTran.TranType,
      (object) pmTran.RefNbr
    }));
    if (pmRegister == null || pmRegister.OrigDocType != "AL" || !pmTran.ExcludedFromBilling.GetValueOrDefault())
      return;
    pmTran.ExcludedFromBilling = new bool?(false);
    ((PXSelectBase<PMTran>) this.Transactions).Update(pmTran);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMTran> e)
  {
    PMTran row = e.Row;
    if (row == null || e.Operation == 3)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
    if (!row.AccountGroupID.HasValue && pmProject?.BaseType == "P" && !ProjectDefaultAttribute.IsNonProject(pmProject.ContractID))
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMTran>>) e).Cache.RaiseExceptionHandling<PMTran.accountGroupID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[accountGroupID]"
      }));
    this.CheckDebitAccount(row, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMTran>>) e).Cache);
  }

  protected void CheckDebitAccount(PMTran tran, PXCache cache)
  {
    int? nullable;
    int num1;
    if (tran == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = tran.AccountID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    int num2;
    if (tran == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = tran.AccountGroupID;
      num2 = !nullable.HasValue ? 1 : 0;
    }
    if (num2 != 0 || cache == null)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, tran.AccountID);
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, tran.AccountGroupID);
    if (account == null || pmAccountGroup == null || !(pmAccountGroup.Type != "O"))
      return;
    nullable = pmAccountGroup.GroupID;
    int? accountGroupId = account.AccountGroupID;
    if (nullable.GetValueOrDefault() == accountGroupId.GetValueOrDefault() & nullable.HasValue == accountGroupId.HasValue)
      return;
    PXFieldState stateExt = (PXFieldState) cache.GetStateExt<PMTran.accountID>((object) tran);
    cache.RaiseExceptionHandling<PMTran.accountID>((object) tran, stateExt.Value, (Exception) new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
    {
      (object) stateExt.DisplayName
    }));
  }

  public virtual void ReverseCreditMemo(
    PX.Objects.AR.ARRegister arDoc,
    List<PXResult<PX.Objects.AR.ARTran, PMTran>> list,
    List<PMTran> remainders,
    bool createBillableTransactions)
  {
    PMBillEngine instance = PXGraph.CreateInstance<PMBillEngine>();
    PMRegister pmRegister = ((PXSelectBase<PMRegister>) this.Document).Insert();
    pmRegister.OrigDocType = "CR";
    pmRegister.OrigNoteID = arDoc.NoteID;
    pmRegister.Description = PXMessages.LocalizeNoPrefix("Credit Memo");
    foreach (PXResult<PX.Objects.AR.ARTran, PMTran> pxResult in list)
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, PMTran>.op_Implicit(pxResult);
      PMTran tran1 = PXResult<PX.Objects.AR.ARTran, PMTran>.op_Implicit(pxResult);
      if (createBillableTransactions)
      {
        PMTran copy = PXCache<PMTran>.CreateCopy(tran1);
        copy.OrigTranID = tran1.TranID;
        copy.Date = tran1.Date;
        copy.FinPeriodID = tran1.FinPeriodID;
        int? nullable1 = copy.AccountGroupID;
        if (nullable1.HasValue)
          this.ValidateAccount(copy);
        PMTran pmTran1 = copy;
        long? nullable2 = new long?();
        long? nullable3 = nullable2;
        pmTran1.TranID = nullable3;
        copy.TranType = (string) null;
        copy.RefNbr = (string) null;
        PMTran pmTran2 = copy;
        nullable1 = new int?();
        int? nullable4 = nullable1;
        pmTran2.RefLineNbr = nullable4;
        copy.ARRefNbr = (string) null;
        copy.ARTranType = (string) null;
        copy.ProformaRefNbr = (string) null;
        PMTran pmTran3 = copy;
        nullable1 = new int?();
        int? nullable5 = nullable1;
        pmTran3.ProformaLineNbr = nullable5;
        copy.BatchNbr = (string) null;
        copy.TranDate = new DateTime?();
        copy.TranPeriodID = (string) null;
        copy.BilledDate = new DateTime?();
        copy.NoteID = new Guid?();
        copy.Released = new bool?(false);
        copy.Billed = new bool?(false);
        copy.Allocated = new bool?(false);
        copy.ExcludedFromBilling = new bool?(false);
        copy.ExcludedFromAllocation = new bool?(true);
        PMTran tran2 = ((PXSelectBase<PMTran>) this.Transactions).Insert(copy);
        if (!this.IsFinPeriodValid(tran2))
        {
          tran2.FinPeriodID = arTran.FinPeriodID;
          ((PXSelectBase<PMTran>) this.Transactions).Update(tran2);
        }
        string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Transactions).Cache, (object) tran1);
        if (note != null)
          PXNoteAttribute.SetNote(((PXSelectBase) this.Transactions).Cache, (object) tran2, note);
        Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) tran1);
        if (fileNotes != null && fileNotes.Length != 0)
          PXNoteAttribute.SetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) tran2, fileNotes);
        if (tran1.Reverse == "N")
        {
          nullable2 = tran1.RemainderOfTranID;
          if (!nullable2.HasValue)
          {
            PMTran tran3 = ((PXSelectBase<PMTran>) this.Transactions).Insert(instance.ReverseTran(tran1).First<PMTran>());
            if (!this.IsFinPeriodValid(tran3))
            {
              tran3.FinPeriodID = arTran.FinPeriodID;
              ((PXSelectBase<PMTran>) this.Transactions).Update(tran3);
            }
          }
        }
      }
      PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.remainderOfTranID, Equal<P.AsInt>>>>>.And<BqlOperand<PMTran.excludedFromBilling, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) tran1.TranID
      }));
      if (pmTran != null && !pmTran.Billed.GetValueOrDefault())
      {
        remainders.Add(pmTran);
        pmTran.ExcludedFromBilling = new bool?(true);
        pmTran.ExcludedFromBillingReason = PXMessages.LocalizeFormatNoPrefix("Written-Off with Credit Memo {0}", new object[1]
        {
          (object) arDoc.RefNbr
        });
        GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) pmTran, true);
      }
    }
  }

  private void ValidateAccount(PMTran tran)
  {
    if (tran == null || !tran.AccountID.HasValue || !tran.ProjectID.HasValue)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) new PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) tran.AccountID
    }));
    if (account == null)
      return;
    int? accountGroupId1 = account.AccountGroupID;
    int? accountGroupId2 = tran.AccountGroupID;
    if (!(accountGroupId1.GetValueOrDefault() == accountGroupId2.GetValueOrDefault() & accountGroupId1.HasValue == accountGroupId2.HasValue))
      throw new Exception(PXMessages.LocalizeFormatNoPrefix("The credit memo related to the {0} project cannot be released. Map the {1} account to an appropriate account group and try again.", new object[2]
      {
        (object) PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) new PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>((PXGraph) this)).Select(new object[1]
        {
          (object) tran.ProjectID
        }))?.ContractCD,
        (object) account.AccountCD
      }));
  }

  public virtual void ValidateContractBaseCurrency(PX.Objects.CT.Contract contract)
  {
  }

  protected virtual bool IsFinPeriodValid(PMTran tran)
  {
    try
    {
      string finPeriodId = tran.FinPeriodID;
      ((PXSelectBase) this.Transactions).Cache.GetAttributes("finPeriodID").OfType<OpenPeriodAttribute>().First<OpenPeriodAttribute>().IsValidPeriod(((PXSelectBase) this.Transactions).Cache, (object) tran, (object) finPeriodId);
    }
    catch (PXSetPropertyException ex)
    {
      return false;
    }
    return true;
  }

  public virtual void BillLater(
    PX.Objects.AR.ARRegister arDoc,
    List<Tuple<PMProformaTransactLine, PMTran>> billLater)
  {
    PMBillEngine instance = PXGraph.CreateInstance<PMBillEngine>();
    PMRegister pmRegister = ((PXSelectBase<PMRegister>) this.Document).Insert();
    pmRegister.OrigDocType = "UR";
    pmRegister.OrigNoteID = arDoc.NoteID;
    pmRegister.Description = PXMessages.LocalizeNoPrefix("Unbilled Remainder");
    foreach (Tuple<PMProformaTransactLine, PMTran> tuple in billLater)
    {
      PMProformaTransactLine proformaTransactLine = tuple.Item1;
      PMTran tran = tuple.Item2;
      PMProject project;
      if (ProjectDefaultAttribute.IsProject((PXGraph) this, tran.ProjectID, out project))
      {
        PMTran copy = PXCache<PMTran>.CreateCopy(tran);
        copy.RemainderOfTranID = tran.TranID;
        if (copy.TranCuryID != arDoc.CuryID)
        {
          copy.TranCuryID = arDoc.CuryID;
          copy.BaseCuryInfoID = new long?();
          copy.ProjectCuryInfoID = new long?();
        }
        copy.Date = arDoc.DocDate;
        copy.FinPeriodID = arDoc.FinPeriodID;
        copy.TranID = new long?();
        copy.TranType = (string) null;
        copy.RefNbr = (string) null;
        copy.ARRefNbr = (string) null;
        copy.ARTranType = (string) null;
        PMTran pmTran1 = copy;
        int? nullable1 = new int?();
        int? nullable2 = nullable1;
        pmTran1.RefLineNbr = nullable2;
        copy.ProformaRefNbr = (string) null;
        PMTran pmTran2 = copy;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        pmTran2.ProformaLineNbr = nullable3;
        copy.BatchNbr = (string) null;
        copy.TranDate = new DateTime?();
        copy.TranPeriodID = (string) null;
        copy.BilledDate = new DateTime?();
        copy.NoteID = new Guid?();
        copy.AllocationID = (string) null;
        copy.Description = proformaTransactLine.Description;
        copy.UOM = proformaTransactLine.UOM;
        PMTran pmTran3 = copy;
        Decimal? nullable4 = proformaTransactLine.BillableQty;
        Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
        nullable4 = proformaTransactLine.Qty;
        Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
        Decimal? nullable5 = new Decimal?(Math.Max(0M, valueOrDefault1 - valueOrDefault2));
        pmTran3.Qty = nullable5;
        copy.BillableQty = copy.Qty;
        copy.Released = new bool?(false);
        copy.Billed = new bool?(false);
        copy.Allocated = new bool?(false);
        copy.ExcludedFromBilling = new bool?(false);
        copy.ExcludedFromAllocation = new bool?(true);
        copy.Reverse = "N";
        PMTran pmTran4 = ((PXSelectBase<PMTran>) this.Transactions).Insert(copy);
        if (!instance.IsNonGL(tran))
        {
          PMSetup current1 = ((PXSelectBase<PMSetup>) this.Setup).Current;
          int num1;
          if (current1 == null)
          {
            num1 = 0;
          }
          else
          {
            nullable1 = current1.UnbilledRemainderAccountID;
            num1 = nullable1.HasValue ? 1 : 0;
          }
          if (num1 != 0)
          {
            PMSetup current2 = ((PXSelectBase<PMSetup>) this.Setup).Current;
            int num2;
            if (current2 == null)
            {
              num2 = 0;
            }
            else
            {
              nullable1 = current2.UnbilledRemainderOffsetAccountID;
              num2 = nullable1.HasValue ? 1 : 0;
            }
            if (num2 != 0)
            {
              PMSetup current3 = ((PXSelectBase<PMSetup>) this.Setup).Current;
              int num3;
              if (current3 == null)
              {
                num3 = 0;
              }
              else
              {
                nullable1 = current3.UnbilledRemainderSubID;
                num3 = nullable1.HasValue ? 1 : 0;
              }
              if (num3 != 0)
              {
                PMSetup current4 = ((PXSelectBase<PMSetup>) this.Setup).Current;
                int num4;
                if (current4 == null)
                {
                  num4 = 0;
                }
                else
                {
                  nullable1 = current4.UnbilledRemainderOffsetSubID;
                  num4 = nullable1.HasValue ? 1 : 0;
                }
                if (num4 != 0)
                {
                  pmTran4.AccountID = ((PXSelectBase<PMSetup>) this.Setup).Current.UnbilledRemainderAccountID;
                  pmTran4.SubID = ((PXSelectBase<PMSetup>) this.Setup).Current.UnbilledRemainderSubID;
                  pmTran4.OffsetAccountID = ((PXSelectBase<PMSetup>) this.Setup).Current.UnbilledRemainderOffsetAccountID;
                  pmTran4.OffsetSubID = ((PXSelectBase<PMSetup>) this.Setup).Current.UnbilledRemainderOffsetSubID;
                  PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, pmTran4.AccountID);
                  pmTran4.AccountGroupID = account.AccountGroupID;
                }
              }
            }
          }
        }
        bool? merged = proformaTransactLine.Merged;
        Decimal valueOrDefault3;
        if (!merged.GetValueOrDefault())
        {
          nullable4 = proformaTransactLine.LineTotal;
          valueOrDefault3 = nullable4.GetValueOrDefault();
        }
        else
        {
          nullable4 = proformaTransactLine.MergedAmount;
          valueOrDefault3 = nullable4.GetValueOrDefault();
        }
        Decimal num5 = valueOrDefault3;
        merged = proformaTransactLine.Merged;
        Decimal valueOrDefault4;
        if (!merged.GetValueOrDefault())
        {
          nullable4 = proformaTransactLine.CuryLineTotal;
          valueOrDefault4 = nullable4.GetValueOrDefault();
        }
        else
        {
          nullable4 = proformaTransactLine.CuryMergedAmount;
          valueOrDefault4 = nullable4.GetValueOrDefault();
        }
        Decimal num6 = valueOrDefault4;
        if (arDoc.CuryID == project.CuryID)
        {
          PMTran pmTran5 = pmTran4;
          nullable4 = proformaTransactLine.BillableAmount;
          Decimal? nullable6 = new Decimal?(Math.Max(0M, nullable4.GetValueOrDefault() - num5));
          pmTran5.Amount = nullable6;
          PMTran pmTran6 = pmTran4;
          nullable4 = proformaTransactLine.CuryBillableAmount;
          Decimal? nullable7 = new Decimal?(Math.Max(0M, nullable4.GetValueOrDefault() - num6));
          pmTran6.TranCuryAmount = nullable7;
          PMTran pmTran7 = pmTran4;
          nullable4 = proformaTransactLine.CuryBillableAmount;
          Decimal? nullable8 = new Decimal?(Math.Max(0M, nullable4.GetValueOrDefault() - num6));
          pmTran7.ProjectCuryAmount = nullable8;
        }
        else
        {
          PMTran pmTran8 = pmTran4;
          nullable4 = proformaTransactLine.BillableAmount;
          Decimal? nullable9 = new Decimal?(Math.Max(0M, nullable4.GetValueOrDefault() - num5));
          pmTran8.Amount = nullable9;
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<RegisterEntry.MultiCurrency>().GetCurrencyInfo(pmTran4.ProjectCuryInfoID);
          nullable4 = pmTran4.Amount;
          Decimal valueOrDefault5 = nullable4.GetValueOrDefault();
          Decimal num7 = currencyInfo.CuryConvCury(valueOrDefault5);
          pmTran4.TranCuryAmount = new Decimal?(num7);
          pmTran4.ProjectCuryAmount = new Decimal?(num7);
        }
        ((PXSelectBase<PMTran>) this.Transactions).Update(pmTran4);
      }
    }
  }

  public virtual List<PMTran> GetRemaindersToReverse(List<PMTran> trans)
  {
    List<PMTran> remaindersToReverse = new List<PMTran>();
    PXSelect<PMTran, Where<PMTran.origTranID, Equal<Required<PMTran.tranID>>>> pxSelect = new PXSelect<PMTran, Where<PMTran.origTranID, Equal<Required<PMTran.tranID>>>>((PXGraph) this);
    foreach (PMTran tran in trans)
    {
      if (!tran.ExcludedFromBalance.GetValueOrDefault())
      {
        if (!((IQueryable<PXResult<PMTran>>) ((PXSelectBase<PMTran>) pxSelect).Select(new object[1]
        {
          (object) tran.TranID
        })).Any<PXResult<PMTran>>())
          remaindersToReverse.Add(tran);
      }
    }
    return remaindersToReverse;
  }

  public virtual void ReverseRemainders(PX.Objects.AR.ARRegister arDoc, List<PMTran> trans)
  {
    PMRegister pmRegister = ((PXSelectBase<PMRegister>) this.Document).Insert();
    pmRegister.OrigDocType = "RR";
    pmRegister.OrigNoteID = arDoc.NoteID;
    pmRegister.Description = PXMessages.LocalizeNoPrefix("Unbilled Remainder Reversal");
    PMBillEngine instance = PXGraph.CreateInstance<PMBillEngine>();
    foreach (PMTran tran in trans)
    {
      PMTran pmTran = instance.ReverseTran(tran).First<PMTran>();
      pmTran.Date = arDoc.DocDate;
      pmTran.FinPeriodID = arDoc.FinPeriodID;
      ((PXSelectBase<PMTran>) this.Transactions).Insert(pmTran);
    }
  }

  public virtual void ReverseAllocations(PX.Objects.AR.ARRegister arDoc, List<PMTran> trans)
  {
    PMRegister pmRegister = ((PXSelectBase<PMRegister>) this.Document).Insert();
    pmRegister.OrigDocType = "AR";
    pmRegister.OrigNoteID = arDoc.NoteID;
    pmRegister.Description = PXMessages.LocalizeNoPrefix("Allocation Reversal on AR Invoice Release");
    PMBillEngine instance = PXGraph.CreateInstance<PMBillEngine>();
    foreach (PMTran tran in trans)
    {
      foreach (PMTran pmTran in (IEnumerable<PMTran>) instance.ReverseTran(tran))
      {
        pmTran.Date = arDoc.DocDate;
        pmTran.FinPeriodID = arDoc.FinPeriodID;
        ((PXSelectBase<PMTran>) this.Transactions).Insert(pmTran);
      }
    }
  }

  protected void SuppressFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  private void AddUsage(PXCache sender, PMTran tran, Decimal? used, string UOM)
  {
    if (!tran.ProjectID.HasValue || !tran.TaskID.HasValue || !tran.InventoryID.HasValue)
      return;
    int? inventoryId = tran.InventoryID;
    int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
    if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
      return;
    if (PXResultset<RegisterEntry.RecurringItemEx>.op_Implicit(PXSelectBase<RegisterEntry.RecurringItemEx, PXSelect<RegisterEntry.RecurringItemEx, Where<RegisterEntry.RecurringItemEx.projectID, Equal<Required<RegisterEntry.RecurringItemEx.projectID>>, And<RegisterEntry.RecurringItemEx.taskID, Equal<Required<RegisterEntry.RecurringItemEx.taskID>>, And<RegisterEntry.RecurringItemEx.inventoryID, Equal<Required<RegisterEntry.RecurringItemEx.inventoryID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) tran.ProjectID,
      (object) tran.TaskID,
      (object) tran.InventoryID
    })) == null)
      return;
    Decimal valueOrDefault = used.GetValueOrDefault();
    if (!string.IsNullOrEmpty(UOM))
      valueOrDefault = INUnitAttribute.ConvertToBase(sender, tran.InventoryID, UOM, used.GetValueOrDefault(), INPrecision.QUANTITY);
    PMRecurringItemAccum recurringItemAccum1 = new PMRecurringItemAccum();
    recurringItemAccum1.ProjectID = tran.ProjectID;
    recurringItemAccum1.TaskID = tran.TaskID;
    recurringItemAccum1.InventoryID = tran.InventoryID;
    PMRecurringItemAccum recurringItemAccum2 = ((PXSelectBase<PMRecurringItemAccum>) this.RecurringItems).Insert(recurringItemAccum1);
    PMRecurringItemAccum recurringItemAccum3 = recurringItemAccum2;
    Decimal? used1 = recurringItemAccum3.Used;
    Decimal num1 = valueOrDefault;
    recurringItemAccum3.Used = used1.HasValue ? new Decimal?(used1.GetValueOrDefault() + num1) : new Decimal?();
    PMRecurringItemAccum recurringItemAccum4 = recurringItemAccum2;
    Decimal? usedTotal = recurringItemAccum4.UsedTotal;
    Decimal num2 = valueOrDefault;
    recurringItemAccum4.UsedTotal = usedTotal.HasValue ? new Decimal?(usedTotal.GetValueOrDefault() + num2) : new Decimal?();
  }

  private void SubtractUsage(PXCache sender, PMTran tran, Decimal? used, string UOM)
  {
    Decimal? nullable1 = used;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    PXCache sender1 = sender;
    PMTran tran1 = tran;
    Decimal? nullable2 = used;
    Decimal? used1 = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
    string UOM1 = UOM;
    this.AddUsage(sender1, tran1, used1, UOM1);
  }

  private void AddAllocatedTotal(PMTran tran)
  {
    if (!tran.OrigProjectID.HasValue || !tran.OrigTaskID.HasValue || !tran.OrigAccountGroupID.HasValue)
      return;
    PMTaskAllocTotalAccum taskAllocTotalAccum1 = new PMTaskAllocTotalAccum();
    taskAllocTotalAccum1.ProjectID = tran.OrigProjectID;
    taskAllocTotalAccum1.TaskID = tran.OrigTaskID;
    taskAllocTotalAccum1.AccountGroupID = tran.OrigAccountGroupID;
    taskAllocTotalAccum1.InventoryID = new int?(tran.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID);
    taskAllocTotalAccum1.CostCodeID = new int?(tran.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode());
    PMTaskAllocTotalAccum taskAllocTotalAccum2 = ((PXSelectBase<PMTaskAllocTotalAccum>) this.AllocationTotals).Insert(taskAllocTotalAccum1);
    PMTaskAllocTotalAccum taskAllocTotalAccum3 = taskAllocTotalAccum2;
    Decimal? amount = taskAllocTotalAccum3.Amount;
    Decimal? nullable1 = tran.ProjectCuryAmount;
    taskAllocTotalAccum3.Amount = amount.HasValue & nullable1.HasValue ? new Decimal?(amount.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    PMTaskAllocTotalAccum taskAllocTotalAccum4 = taskAllocTotalAccum2;
    nullable1 = taskAllocTotalAccum4.Quantity;
    bool? nullable2 = tran.Billable;
    Decimal? nullable3;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = tran.UseBillableQty;
      if (nullable2.GetValueOrDefault())
      {
        nullable3 = tran.BillableQty;
        goto label_5;
      }
    }
    nullable3 = tran.Qty;
label_5:
    Decimal? nullable4 = nullable3;
    taskAllocTotalAccum4.Quantity = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractAllocatedTotal(PMTran tran)
  {
    if (!tran.OrigProjectID.HasValue || !tran.OrigTaskID.HasValue || !tran.OrigAccountGroupID.HasValue || !tran.InventoryID.HasValue)
      return;
    PMTaskAllocTotalAccum taskAllocTotalAccum1 = new PMTaskAllocTotalAccum();
    taskAllocTotalAccum1.ProjectID = tran.OrigProjectID;
    taskAllocTotalAccum1.TaskID = tran.OrigTaskID;
    taskAllocTotalAccum1.AccountGroupID = tran.OrigAccountGroupID;
    taskAllocTotalAccum1.InventoryID = new int?(tran.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID);
    taskAllocTotalAccum1.CostCodeID = new int?(tran.CostCodeID ?? CostCodeAttribute.GetDefaultCostCode());
    PMTaskAllocTotalAccum taskAllocTotalAccum2 = ((PXSelectBase<PMTaskAllocTotalAccum>) this.AllocationTotals).Insert(taskAllocTotalAccum1);
    PMTaskAllocTotalAccum taskAllocTotalAccum3 = taskAllocTotalAccum2;
    Decimal? amount = taskAllocTotalAccum3.Amount;
    Decimal? nullable1 = tran.ProjectCuryAmount;
    taskAllocTotalAccum3.Amount = amount.HasValue & nullable1.HasValue ? new Decimal?(amount.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    PMTaskAllocTotalAccum taskAllocTotalAccum4 = taskAllocTotalAccum2;
    nullable1 = taskAllocTotalAccum4.Quantity;
    bool? nullable2 = tran.Billable;
    Decimal? nullable3;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = tran.UseBillableQty;
      if (nullable2.GetValueOrDefault())
      {
        nullable3 = tran.BillableQty;
        goto label_5;
      }
    }
    nullable3 = tran.Qty;
label_5:
    Decimal? nullable4 = nullable3;
    taskAllocTotalAccum4.Quantity = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
  }

  public virtual PMTran CreateTransaction(RegisterEntry.CreatePMTran createPMTran)
  {
    if (!this.CanCreateTransaction(createPMTran.TimeActivity, createPMTran.TimeSpent, createPMTran.TimeBillable))
      return (PMTran) null;
    bool postToOffbalance = this.GetPostToOffbalance(createPMTran.EmployeeID);
    PX.Objects.IN.InventoryItem laborItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, createPMTran.TimeActivity.LabourItemID);
    PX.Objects.EP.EPEmployee employee = PX.Objects.EP.EPEmployee.PK.Find((PXGraph) this, createPMTran.EmployeeID);
    PMProject project = PMProject.PK.Find((PXGraph) this, createPMTran.TimeActivity.ProjectID);
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, createPMTran.TimeActivity.ProjectID, createPMTran.TimeActivity.ProjectTaskID);
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.parentBAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) employee.ParentBAccountID
    }));
    FinPeriod finPeriodByDate = this.FinPeriodRepository.FindFinPeriodByDate(new DateTime?(createPMTran.Date), PXAccess.GetParentOrganizationID((int?) branch?.BranchID));
    if (finPeriodByDate == null)
      throw new PXException("The financial period that corresponds to the date of at least one activity of a time card does not exist. Please, generate needed financial periods on the Master Financial Calendar (GL201000) form.");
    this.WriteWarningsToTrace(project, dirty);
    string str1 = (string) null;
    string str2 = (string) null;
    bool? nullable1;
    if (!postToOffbalance)
    {
      nullable1 = project.NonProject;
      if (!nullable1.GetValueOrDefault())
      {
        this.ValidateExpenseSubMask(project, dirty, laborItem, employee);
        this.ValidateExpenseAccrualSubMask(project, dirty, laborItem, employee);
        str1 = this.CombineCostSubAccount(project, dirty, laborItem, employee);
        str2 = this.CombineOffsetSubAccount(project, dirty, laborItem, employee);
      }
    }
    PMTran transaction = new PMTran()
    {
      ProjectID = createPMTran.TimeActivity.ProjectID,
      BranchID = (int?) branch?.BranchID
    };
    transaction.ProjectID = createPMTran.TimeActivity.ProjectID;
    transaction.TaskID = createPMTran.TimeActivity.ProjectTaskID;
    transaction.CostCodeID = createPMTran.TimeActivity.CostCodeID;
    transaction.InventoryID = createPMTran.TimeActivity.LabourItemID;
    transaction.UnionID = createPMTran.TimeActivity.UnionID;
    transaction.WorkCodeID = createPMTran.TimeActivity.WorkCodeID;
    transaction.ResourceID = createPMTran.EmployeeID;
    transaction.Date = new DateTime?(PXDateAndTimeWithTimeZoneAttribute.GetTimeZoneAdjustedActivityDate(createPMTran.Date, createPMTran.TimeActivity.ReportedInTimeZoneID));
    transaction.TranCuryID = createPMTran.TranCuryID ?? branch?.BaseCuryID ?? ((PXGraph) this).Accessinfo.BaseCuryID;
    transaction.FinPeriodID = finPeriodByDate.FinPeriodID;
    PMTran pmTran1 = transaction;
    int? nullable2 = createPMTran.TimeSpent;
    Decimal? nullable3 = new Decimal?(this.GetConvertedAndRoundedTime(nullable2.HasValue ? new Decimal?((Decimal) nullable2.GetValueOrDefault()) : new Decimal?(), laborItem.BaseUnit));
    pmTran1.Qty = nullable3;
    transaction.Billable = createPMTran.TimeActivity.IsBillable;
    PMTran pmTran2 = transaction;
    nullable2 = createPMTran.TimeBillable;
    Decimal? nullable4 = new Decimal?(this.GetConvertedAndRoundedTime(nullable2.HasValue ? new Decimal?((Decimal) nullable2.GetValueOrDefault()) : new Decimal?(), laborItem.BaseUnit));
    pmTran2.BillableQty = nullable4;
    transaction.UOM = laborItem.BaseUnit;
    transaction.TranCuryUnitRate = new Decimal?(PXDBPriceCostAttribute.Round(createPMTran.Cost.GetValueOrDefault()));
    transaction.Description = createPMTran.TimeActivity.Summary;
    transaction.StartDate = createPMTran.TimeActivity.Date;
    transaction.EndDate = createPMTran.TimeActivity.Date;
    transaction.OrigRefID = createPMTran.TimeActivity.NoteID;
    transaction.EarningType = createPMTran.TimeActivity.EarningTypeID;
    transaction.OvertimeMultiplier = createPMTran.OvertimeMult;
    transaction.IsNonGL = new bool?(this.IsNonGlTransaction(createPMTran.EmployeeID));
    if (createPMTran.TimeActivity.RefNoteID.HasValue)
    {
      Note note = PXResultset<Note>.op_Implicit(PXSelectBase<Note, PXSelectJoin<Note, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<Note.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) createPMTran.TimeActivity.RefNoteID
      }));
      if (note != null && note.EntityType == typeof (CRCase).FullName)
      {
        CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) createPMTran.TimeActivity.RefNoteID
        }));
        if (crCase != null)
        {
          nullable1 = crCase.IsBillable;
          if (!nullable1.GetValueOrDefault())
            goto label_12;
        }
        transaction.ExcludedFromAllocation = new bool?(true);
        transaction.ExcludedFromBilling = new bool?(true);
        transaction.ExcludedFromBillingReason = PXMessages.LocalizeFormatNoPrefix("Billable with Case {0}", new object[1]
        {
          (object) crCase.CaseCD
        });
      }
    }
label_12:
    if (postToOffbalance)
    {
      transaction.AccountGroupID = EPSetupMaint.GetOffBalancePostingAccount((PXGraph) this, ((PXSelectBase<EPSetup>) this.epSetup).Current, createPMTran.EmployeeID);
    }
    else
    {
      transaction.AccountID = this.GetCostAccount(project, dirty, laborItem, employee);
      transaction.AccountGroupID = this.GetAccountGroupFromAccount(transaction.AccountID);
      transaction.OffsetAccountID = this.GetOffsetAccount(project, dirty, laborItem, employee);
      if (string.IsNullOrEmpty(str1))
        transaction.SubID = laborItem.COGSSubID;
      if (string.IsNullOrEmpty(str2))
        transaction.OffsetSubID = laborItem.InvtSubID;
    }
    if (createPMTran.InsertTransaction)
    {
      try
      {
        transaction = this.InsertTransactionWithManuallyChangedCurrencyInfo(transaction);
      }
      catch (PXFieldValueProcessingException ex)
      {
        if (((Exception) ex).InnerException is PXTaskIsCompletedException)
          throw new PXException("To be able to use project tasks with the Completed, Canceled, or In Planning status for data entry, the user must have the Project Accountant role assigned.");
        throw ex;
      }
      catch (PXException ex)
      {
        throw ex;
      }
      if (!string.IsNullOrEmpty(str1))
        ((PXSelectBase<PMTran>) this.Transactions).SetValueExt<PMTran.subID>(transaction, (object) str1);
      if (!string.IsNullOrEmpty(str2))
        ((PXSelectBase<PMTran>) this.Transactions).SetValueExt<PMTran.offsetSubID>(transaction, (object) str2);
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (PMTimeActivity)], (object) createPMTran.TimeActivity, ((PXSelectBase) this.Transactions).Cache, (object) transaction, ((PXSelectBase<EPSetup>) this.epSetup).Current.GetCopyNoteSettings<PXModule.pm>());
    }
    return transaction;
  }

  protected virtual int? GetCostAccount(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    if (project == null)
      throw new ArgumentNullException(nameof (project));
    return project.NonProject.GetValueOrDefault() ? this.GetCostAccountForNonProject(laborItem) : this.GetCostAccountForProject(project, task, laborItem, employee);
  }

  private int? GetCostAccountForNonProject(PX.Objects.IN.InventoryItem laborItem)
  {
    if (!laborItem.COGSAcctID.HasValue)
      throw new PXException("The COGS account is not defined for the {0} inventory item.", new object[1]
      {
        (object) laborItem.InventoryCD.Trim()
      });
    return laborItem.COGSAcctID;
  }

  protected virtual int? GetCostAccountForProject(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    if (project == null)
      throw new ArgumentNullException(nameof (project));
    if (task == null)
      throw new ArgumentNullException(nameof (task));
    if (laborItem == null)
      throw new ArgumentNullException(nameof (laborItem));
    if (employee == null)
      throw new ArgumentNullException(nameof (employee));
    int? nullable = new int?();
    if (this.ExpenseAccountSource == "P")
    {
      if (project.DefaultExpenseAccountID.HasValue)
        nullable = project.DefaultExpenseAccountID;
      else
        PXTrace.WriteWarning("The time card cannot be released because the default cost account is not specified in the settings of the {0} project on the Defaults tab of the Projects (PM301000) form.", new object[1]
        {
          (object) project.ContractCD.Trim()
        });
    }
    else if (this.ExpenseAccountSource == "T")
    {
      if (task.DefaultExpenseAccountID.HasValue)
        nullable = task.DefaultExpenseAccountID;
      else
        PXTrace.WriteWarning("The time card cannot be released because the default cost account is not specified in the settings of the {0} project task of the {1} project on the Summary tab of the Project Tasks (PM302000) form.", new object[2]
        {
          (object) project.ContractCD.Trim(),
          (object) task.TaskCD.Trim()
        });
    }
    else if (this.ExpenseAccountSource == "E")
    {
      if (employee.ExpenseAcctID.HasValue)
        nullable = employee.ExpenseAcctID;
      else
        PXTrace.WriteWarning("The time card cannot be released because the expense account is not specified in the settings of the {0} employee on the Financial Settings tab of the Employees (EP203000) form.", new object[1]
        {
          (object) employee.AcctCD.Trim()
        });
    }
    else
    {
      if (!laborItem.COGSAcctID.HasValue)
        PXTrace.WriteWarning("The time card cannot be released because the expense account is not specified in the settings of the {0} labor item on the GL Accounts tab of the Non-Stock Items (IN202000) form.", new object[1]
        {
          (object) laborItem.InventoryCD.Trim()
        });
      nullable = laborItem.COGSAcctID;
    }
    return nullable.HasValue ? nullable : throw new PXException("The time card cannot be released because the expense account is not found. For details, see the trace log.");
  }

  protected virtual int? GetOffsetAccount(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    if (project == null)
      throw new ArgumentNullException(nameof (project));
    return project.NonProject.GetValueOrDefault() ? this.GetOffsetAccountForNonProject(laborItem) : this.GetOffsetAccountForProject(project, task, laborItem, employee);
  }

  private int? GetOffsetAccountForNonProject(PX.Objects.IN.InventoryItem laborItem)
  {
    if (!laborItem.InvtAcctID.HasValue)
      throw new PXException("The inventory account is not defined for the {0} inventory item.", new object[1]
      {
        (object) laborItem.InventoryCD.Trim()
      });
    return laborItem.InvtAcctID;
  }

  protected virtual int? GetOffsetAccountForProject(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    int? nullable = new int?();
    if (this.ExpenseAccrualAccountSource == "P")
    {
      if (project.DefaultAccrualAccountID.HasValue)
        nullable = project.DefaultAccrualAccountID;
      else
        PXTrace.WriteWarning("The time card cannot be released because the accrual account is not specified in the settings of the {0} project on the Defaults tab of the Projects (PM301000) form.", new object[1]
        {
          (object) project.ContractCD.Trim()
        });
    }
    else if (this.ExpenseAccrualAccountSource == "T")
    {
      if (task != null && task.DefaultAccrualAccountID.HasValue)
        nullable = task.DefaultAccrualAccountID;
      else
        PXTrace.WriteWarning("The time card cannot be released because the accrual account is not specified in the settings of the {0} project task of the {1} project on the Summary tab of the Project Tasks (PM302000) form.", new object[2]
        {
          (object) project.ContractCD.Trim(),
          (object) task.TaskCD.Trim()
        });
    }
    else if (this.ExpenseAccrualAccountSource == "E")
    {
      if (employee.ExpenseAcctID.HasValue)
        nullable = employee.ExpenseAcctID;
      else
        PXTrace.WriteWarning("Project preferences have been configured to get the expense account from employees, but the expense account is not specified for the {0} employee.", new object[1]
        {
          (object) employee.AcctCD.Trim()
        });
    }
    else
    {
      if (!laborItem.InvtAcctID.HasValue)
        PXTrace.WriteWarning("The time card cannot be released because the expense accrual account is not specified in the settings of the {0} labor item on the GL Accounts tab of the Non-Stock Items (IN202000) form.", new object[1]
        {
          (object) laborItem.InventoryCD.Trim()
        });
      nullable = laborItem.InvtAcctID;
    }
    return nullable.HasValue ? nullable : throw new PXException("The time card cannot be released because the expense accrual account is not found. For details, see the trace log.");
  }

  private int? GetAccountGroupFromAccount(int? accountID)
  {
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID);
    if (!account.AccountGroupID.HasValue)
      throw new PXException("The {0} account is not mapped to any account group.", new object[1]
      {
        (object) account.AccountCD
      });
    return account.AccountGroupID;
  }

  protected virtual string CombineCostSubAccount(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    return SubAccountMaskAttribute.MakeSub<PMSetup.expenseSubMask>((PXGraph) this, this.ExpenseSubMask, new object[4]
    {
      (object) laborItem.COGSSubID,
      (object) project.DefaultExpenseSubID,
      (object) task.DefaultExpenseSubID,
      (object) employee.ExpenseSubID
    }, new System.Type[4]
    {
      typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
      typeof (PX.Objects.CT.Contract.defaultExpenseSubID),
      typeof (PMTask.defaultExpenseSubID),
      typeof (PX.Objects.EP.EPEmployee.expenseSubID)
    });
  }

  protected virtual string CombineOffsetSubAccount(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    return SubAccountMaskAttribute.MakeSub<PMSetup.expenseAccrualSubMask>((PXGraph) this, this.ExpenseAccrualSubMask, new object[4]
    {
      (object) laborItem.InvtSubID,
      (object) project.DefaultAccrualSubID,
      (object) task.DefaultAccrualSubID,
      (object) employee.ExpenseSubID
    }, new System.Type[4]
    {
      typeof (PX.Objects.IN.InventoryItem.invtSubID),
      typeof (PX.Objects.CT.Contract.defaultAccrualSubID),
      typeof (PMTask.defaultAccrualSubID),
      typeof (PX.Objects.EP.EPEmployee.expenseSubID)
    });
  }

  private void ValidateExpenseSubMask(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    if (string.IsNullOrEmpty(this.ExpenseSubMask))
      return;
    int? nullable;
    if (this.ExpenseSubMask.Contains("I"))
    {
      nullable = laborItem.COGSSubID;
      if (!nullable.HasValue)
      {
        PXTrace.WriteError("Project preferences have been configured to combine the expense subaccount from inventory items, but the expense subaccount is not specified for the {0} inventory item.", new object[1]
        {
          (object) laborItem.InventoryCD.Trim()
        });
        throw new PXException("Project preferences have been configured to combine the expense subaccount from inventory items, but the expense subaccount is not specified for the {0} inventory item.", new object[1]
        {
          (object) laborItem.InventoryCD.Trim()
        });
      }
    }
    if (this.ExpenseSubMask.Contains("P"))
    {
      nullable = project.DefaultExpenseSubID;
      if (!nullable.HasValue)
      {
        PXTrace.WriteError("Project preferences have been configured to combine the expense subaccount from projects, but the expense subaccount is not specified for the {0} project.", new object[1]
        {
          (object) project.ContractCD.Trim()
        });
        throw new PXException("Project preferences have been configured to combine the expense subaccount from projects, but the expense subaccount is not specified for the {0} project.", new object[1]
        {
          (object) project.ContractCD.Trim()
        });
      }
    }
    if (this.ExpenseSubMask.Contains("T"))
    {
      nullable = task.DefaultExpenseSubID;
      if (!nullable.HasValue)
      {
        PXTrace.WriteError("Project preferences have been configured to combine the expense subaccount from project tasks, but the expense subaccount is not specified for the {1} task of the {0} project.", new object[2]
        {
          (object) project.ContractCD.Trim(),
          (object) task.TaskCD.Trim()
        });
        throw new PXException("Project preferences have been configured to combine the expense subaccount from project tasks, but the expense subaccount is not specified for the {1} task of the {0} project.", new object[2]
        {
          (object) project.ContractCD.Trim(),
          (object) task.TaskCD.Trim()
        });
      }
    }
    if (!this.ExpenseSubMask.Contains("E"))
      return;
    nullable = employee.ExpenseSubID;
    if (!nullable.HasValue)
    {
      PXTrace.WriteError("Project preferences have been configured to combine the expense subaccount from resources, but the expense subaccount is not specified for the {0} employee.", new object[1]
      {
        (object) employee.AcctCD.Trim()
      });
      throw new PXException("Project preferences have been configured to combine the expense subaccount from resources, but the expense subaccount is not specified for the {0} employee.", new object[1]
      {
        (object) employee.AcctCD.Trim()
      });
    }
  }

  private void ValidateExpenseAccrualSubMask(
    PMProject project,
    PMTask task,
    PX.Objects.IN.InventoryItem laborItem,
    PX.Objects.EP.EPEmployee employee)
  {
    if (string.IsNullOrEmpty(this.ExpenseAccrualSubMask))
      return;
    int? nullable;
    if (this.ExpenseAccrualSubMask.Contains("I"))
    {
      nullable = laborItem.InvtSubID;
      if (!nullable.HasValue)
      {
        PXTrace.WriteError("Project preferences have been configured to combine the expense accrual subaccount from inventory items, but the expense accrual subaccount is not specified for the {0} inventory item.", new object[1]
        {
          (object) laborItem.InventoryCD.Trim()
        });
        throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from inventory items, but the expense accrual subaccount is not specified for the {0} inventory item.", new object[1]
        {
          (object) laborItem.InventoryCD.Trim()
        });
      }
    }
    if (this.ExpenseAccrualSubMask.Contains("P"))
    {
      nullable = project.DefaultAccrualSubID;
      if (!nullable.HasValue)
      {
        PXTrace.WriteError("Project preferences have been configured to combine the expense accrual subaccount from projects, but the expense accrual subaccount is not specified for the {0} project.", new object[1]
        {
          (object) project.ContractCD.Trim()
        });
        throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from projects, but the expense accrual subaccount is not specified for the {0} project.", new object[1]
        {
          (object) project.ContractCD.Trim()
        });
      }
    }
    if (this.ExpenseAccrualSubMask.Contains("T"))
    {
      nullable = task.DefaultAccrualSubID;
      if (!nullable.HasValue)
      {
        PXTrace.WriteError("Project preferences have been configured to combine the expense accrual subaccount from project tasks, but the expense accrual subaccount is not specified for the {1} task of the {0} project.", new object[2]
        {
          (object) project.ContractCD.Trim(),
          (object) task.TaskCD.Trim()
        });
        throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from project tasks, but the expense accrual subaccount is not specified for the {1} task of the {0} project.", new object[2]
        {
          (object) project.ContractCD.Trim(),
          (object) task.TaskCD.Trim()
        });
      }
    }
    if (!this.ExpenseAccrualSubMask.Contains("E"))
      return;
    nullable = employee.ExpenseSubID;
    if (!nullable.HasValue)
    {
      PXTrace.WriteError("Project preferences have been configured to combine the expense subaccount from resources, but the expense subaccount is not specified for the {0} employee.", new object[1]
      {
        (object) employee.AcctCD.Trim()
      });
      throw new PXException("Project preferences have been configured to combine the expense subaccount from resources, but the expense subaccount is not specified for the {0} employee.", new object[1]
      {
        (object) employee.AcctCD.Trim()
      });
    }
  }

  private void WriteWarningsToTrace(PMProject project, PMTask task)
  {
    if (!project.IsActive.GetValueOrDefault())
      PXTrace.WriteWarning("Project is not active. Cannot record cost transaction against inactive project. Project: {0}", new object[1]
      {
        (object) project.ContractCD.Trim()
      });
    if (project.IsCompleted.GetValueOrDefault())
      PXTrace.WriteWarning("Project is completed. Cannot record cost transaction against completed project. Project: {0}", new object[1]
      {
        (object) project.ContractCD.Trim()
      });
    if (task != null && !task.IsActive.GetValueOrDefault())
      PXTrace.WriteWarning("Project Task is not active. Cannot record cost transaction against inactive task. ProjectID: {0} TaskID:{1}", new object[2]
      {
        (object) project.ContractCD.Trim(),
        (object) task.TaskCD.Trim()
      });
    if (task != null && task.IsCompleted.GetValueOrDefault())
      PXTrace.WriteWarning("Project Task is completed. Cannot record cost transaction against completed task. ProjectID: {0} TaskID:{1}", new object[2]
      {
        (object) project.ContractCD.Trim(),
        (object) task.TaskCD.Trim()
      });
    if (task == null || !task.IsCancelled.GetValueOrDefault())
      return;
    PXTrace.WriteWarning("Project Task is cancelled. Cannot record cost transaction against cancelled task. ProjectID: {0} TaskID:{1}", new object[2]
    {
      (object) project.ContractCD.Trim(),
      (object) task.TaskCD.Trim()
    });
  }

  private bool GetPostToOffbalance(int? employeeID)
  {
    return !PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || EPSetupMaint.GetPostToOffBalance((PXGraph) this, ((PXSelectBase<EPSetup>) this.epSetup).Current, employeeID);
  }

  private string GetActivityTimeUOM()
  {
    string activityTimeUom = "MINUTE";
    if (!string.IsNullOrEmpty(((PXSelectBase<EPSetup>) this.epSetup).Current.ActivityTimeUnit))
      activityTimeUom = ((PXSelectBase<EPSetup>) this.epSetup).Current.ActivityTimeUnit;
    return activityTimeUom;
  }

  private bool IsNonGlTransaction(int? employeeID)
  {
    string postingOption = EPSetupMaint.GetPostingOption((PXGraph) this, ((PXSelectBase<EPSetup>) this.epSetup).Current, employeeID);
    return postingOption != "P" && postingOption != "G";
  }

  private bool CanCreateTransaction(PMTimeActivity timeActivity, int? timeSpent, int? timeBillable)
  {
    return !(timeActivity.ApprovalStatus == "CL") && (timeSpent.GetValueOrDefault() != 0 || timeBillable.GetValueOrDefault() != 0);
  }

  private Decimal GetConvertedAndRoundedTime(Decimal? time, string itemBaseUnit)
  {
    string activityTimeUom = this.GetActivityTimeUOM();
    Decimal valueOrDefault = time.GetValueOrDefault();
    if (valueOrDefault > 0M)
    {
      int? minBillableTime = ((PXSelectBase<EPSetup>) this.epSetup).Current.MinBillableTime;
      Decimal? nullable = minBillableTime.HasValue ? new Decimal?((Decimal) minBillableTime.GetValueOrDefault()) : new Decimal?();
      Decimal num = valueOrDefault;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        minBillableTime = ((PXSelectBase<EPSetup>) this.epSetup).Current.MinBillableTime;
        valueOrDefault = (Decimal) minBillableTime.Value;
      }
    }
    try
    {
      return INUnitAttribute.ConvertGlobalUnits((PXGraph) this, activityTimeUom, itemBaseUnit, valueOrDefault, INPrecision.QUANTITY);
    }
    catch (PXException ex)
    {
      PXTrace.WriteError((Exception) ex);
      throw ex;
    }
  }

  public virtual PMTran CreateContractUsage(PMTimeActivity timeActivity, int billableMinutes)
  {
    if (timeActivity.ApprovalStatus == "CL")
      return (PMTran) null;
    if (!timeActivity.RefNoteID.HasValue)
      return (PMTran) null;
    if (!timeActivity.IsBillable.GetValueOrDefault())
      return (PMTran) null;
    CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) timeActivity.RefNoteID
    }));
    CRCaseClass crCaseClass = crCase != null ? PXResultset<CRCaseClass>.op_Implicit(PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Required<CRCaseClass.caseClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) crCase.CaseClassID
    })) : throw new Exception("Case cannot be Found");
    int? nullable1 = crCaseClass.PerItemBilling;
    if (nullable1.GetValueOrDefault() != 1)
      return (PMTran) null;
    PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) crCase.ContractID
    }));
    if (contract == null)
      return (PMTran) null;
    this.ValidateContractBaseCurrency(contract);
    int? nullable2 = CRCaseClassLaborMatrix.GetLaborClassID((PXGraph) this, crCaseClass.CaseClassID, timeActivity.EarningTypeID);
    if (!nullable2.HasValue)
      nullable2 = EPContractRate.GetContractLaborClassID((PXGraph) this, timeActivity);
    if (!nullable2.HasValue)
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<PMTimeActivity.ownerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) timeActivity.OwnerID
      }));
      if (epEmployee != null)
      {
        nullable1 = EPEmployeeClassLaborMatrix.GetLaborClassID((PXGraph) this, epEmployee.BAccountID, timeActivity.EarningTypeID);
        nullable2 = nullable1 ?? epEmployee.LabourItemID;
      }
    }
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) nullable2
    }));
    if (inventoryItem == null)
      throw new PXException("Labor Item cannot be found");
    int num1 = billableMinutes < 0 ? -1 : 1;
    billableMinutes = Math.Abs(billableMinutes);
    nullable1 = crCaseClass.PerItemBilling;
    if (nullable1.GetValueOrDefault() == 1)
    {
      nullable1 = crCaseClass.RoundingInMinutes;
      int num2 = 1;
      if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
      {
        int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(billableMinutes) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
        nullable1 = crCaseClass.RoundingInMinutes;
        int valueOrDefault = nullable1.GetValueOrDefault();
        billableMinutes = int32 * valueOrDefault;
      }
    }
    if (billableMinutes > 0)
    {
      nullable1 = crCaseClass.PerItemBilling;
      if (nullable1.GetValueOrDefault() == 1)
      {
        nullable1 = crCaseClass.MinBillTimeInMinutes;
        int num3 = 0;
        if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
        {
          int val1 = billableMinutes;
          nullable1 = crCaseClass.MinBillTimeInMinutes;
          int val2 = nullable1.Value;
          billableMinutes = Math.Max(val1, val2);
        }
      }
    }
    if (billableMinutes <= 0)
      return (PMTran) null;
    PMTran pmTran = new PMTran()
    {
      ProjectID = crCase.ContractID,
      InventoryID = inventoryItem.InventoryID,
      AccountGroupID = contract.ContractAccountGroup,
      OrigRefID = timeActivity.NoteID,
      BAccountID = crCase.CustomerID,
      LocationID = crCase.LocationID,
      Description = timeActivity.Summary,
      StartDate = timeActivity.Date,
      EndDate = timeActivity.Date,
      Date = timeActivity.Date,
      UOM = inventoryItem.SalesUnit,
      Qty = new Decimal?((Decimal) num1 * Convert.ToDecimal(TimeSpan.FromMinutes((double) billableMinutes).TotalHours))
    };
    pmTran.BillableQty = pmTran.Qty;
    pmTran.Released = new bool?(true);
    pmTran.ExcludedFromAllocation = new bool?(true);
    pmTran.IsQtyOnly = new bool?(true);
    pmTran.BillingID = contract.BillingID;
    pmTran.CaseCD = crCase.CaseCD;
    return ((PXSelectBase<PMTran>) this.Transactions).Insert(pmTran);
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    Command command1 = script.Where<Command>((Func<Command, bool>) (_ => _.FieldName == "UseBillableQty")).SingleOrDefault<Command>();
    int index1 = script.FindIndex((Predicate<Command>) (_ => _.FieldName == "TranCuryAmount"));
    if (command1 != null && index1 >= 0)
    {
      script.Remove(command1);
      script.Insert(index1, command1);
    }
    int index2 = 0;
    for (int index3 = 0; index3 < containers.Count; ++index3)
    {
      Container container = containers[index3];
      Command command2 = script[index3];
      if (containers[index3].ViewName() == "Transactions" || containers[index3].ViewName() == "Document")
      {
        containers.RemoveAt(index3);
        containers.Insert(index2, container);
        script.RemoveAt(index3);
        script.Insert(index2, command2);
        ++index2;
      }
    }
  }

  protected virtual bool IsAllPMTranLinesVisible(PMRegister doc)
  {
    return ((IQueryable<PXResult<PMTran>>) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranType, Equal<P.AsString>>>>>.And<BqlOperand<PMTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.Module,
      (object) doc.RefNbr
    })).Count<PXResult<PMTran>>() == ((IQueryable<PXResult<PMTran>>) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Account>.On<BqlOperand<PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<PMTran.offsetAccountID>>>, FbqlJoins.Left<PMAccountGroup>.On<BqlOperand<PMAccountGroup.groupID, IBqlInt>.IsEqual<PMTran.accountGroupID>>>, FbqlJoins.Left<RegisterReleaseProcess.OffsetPMAccountGroup>.On<BqlOperand<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, IBqlInt>.IsEqual<PX.Objects.GL.Account.accountGroupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranType, Equal<P.AsString>>>>, And<BqlOperand<PMTran.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, IsNull>>>>.Or<Match<RegisterReleaseProcess.OffsetPMAccountGroup, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.groupID, IsNull>>>>.Or<Match<PMAccountGroup, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.Module,
      (object) doc.RefNbr
    })).Count<PXResult<PMTran>>();
  }

  public class MultiCurrency : PMTranMultiCurrencyPM<RegisterEntry>
  {
    public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PMTran.projectCuryInfoID>>>> ProjectCuryInfo;
    public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PMTran.baseCuryInfoID>>>> BaseCuryInfo;

    /// <summary>
    /// I have no idea what actuall conditions should  be here
    /// </summary>
    /// <returns></returns>
    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource != null)
      {
        curySource.AllowOverrideRate = new bool?(true);
        curySource.CuryID = (string) null;
      }
      return curySource;
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Transactions
      };
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PMTran, PMTran.tranCuryID> e)
    {
      PMTran row = e.Row;
      PMProject project;
      if ((row != null ? (row.ProjectID.HasValue ? 1 : 0) : 0) != 0 && PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && ProjectDefaultAttribute.IsProject((PXGraph) this.Base, e.Row.ProjectID, out project))
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTran, PMTran.tranCuryID>, PMTran, object>) e).NewValue = (object) project.CuryID;
      }
      else
      {
        PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, e.Row.BranchID);
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTran, PMTran.tranCuryID>, PMTran, object>) e).NewValue = (object) (branch?.BaseCuryID ?? ((PXSelectBase<PX.Objects.GL.Company>) this.Base.Company).Current.BaseCuryID);
      }
    }

    protected virtual void _(PX.Data.Events.FieldUpdating<PMTran, PMTran.tranCuryID> e)
    {
      if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<PMTran, PMTran.tranCuryID>>) e).NewValue != null)
        return;
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<PMTran, PMTran.tranCuryID>>) e).NewValue = e.OldValue;
    }

    protected virtual void _(PX.Data.Events.RowDeleted<PMTran> e)
    {
      if (e.Row == null)
        return;
      RegisterEntry registerEntry = this.Base;
      object[] objArray = new object[2]
      {
        (object) new long?[2]
        {
          e.Row.BaseCuryInfoID,
          e.Row.ProjectCuryInfoID
        },
        (object) e.Row.TranID
      };
      foreach (PX.Objects.CM.Extensions.CurrencyInfo firstTableItem in PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXViewOf<PX.Objects.CM.Extensions.CurrencyInfo>.BasedOn<SelectFromBase<PX.Objects.CM.Extensions.CurrencyInfo, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, In<P.AsInt>>>>>.And<NotExists<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranID, NotEqual<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.baseCuryInfoID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>>.Or<BqlOperand<PMTran.projectCuryInfoID, IBqlLong>.IsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>>>>>.Config>.Select((PXGraph) registerEntry, objArray).FirstTableItems)
        ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Delete(firstTableItem);
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PMTran, PMTran.tranCuryID> e)
    {
      if (e.Row == null || !PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
        return;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.GetCurrencyInfo(e.Row.ProjectCuryInfoID);
      if (currencyInfo1 != null)
      {
        long? curyInfoId = currencyInfo1.CuryInfoID;
        long num = 0;
        if (curyInfoId.GetValueOrDefault() > num & curyInfoId.HasValue && CurrencyCollection.IsBaseCuryInfo(currencyInfo1))
        {
          PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
          copy.CuryInfoID = new long?();
          PX.Objects.CM.Extensions.CurrencyInfo info = ((PXSelectBase) this.currencyinfo).Cache.Insert((object) copy) as PX.Objects.CM.Extensions.CurrencyInfo;
          e.Row.ProjectCuryInfoID = info.CuryInfoID;
          if (info.CuryRateTypeID == null)
            ((PXSelectBase) this.currencyinfo).Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>((object) info);
          ((PXSelectBase) this.currencyinfo).Cache.SetValueExt<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) info, e.NewValue);
          ((PXSelectBase) this.currencyinfo).Cache.RaiseRowUpdated((object) info, (object) currencyInfo1);
          this.recalculateRowBaseValues(((PXSelectBase) this.Base.Transactions).Cache, (object) ((PXSelectBase<PMTran>) this.Base.Transactions).Current, (IEnumerable<CuryField>) this.TrackedItems[((PXSelectBase) this.Base.Transactions).Cache.GetItemType()]);
          if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) info)))
          {
            ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.tranCuryID>>) e).Cache.RaiseExceptionHandling<PMTran.tranCuryID>((object) e.Row, (object) null, (Exception) this.GetCurrencyRateError(info));
            goto label_10;
          }
          goto label_10;
        }
      }
      ((PXSelectBase) this.currencyinfo).Cache.SetValueExt<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) currencyInfo1, e.NewValue);
      GraphHelper.MarkUpdated(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo1, true);
      this.recalculateRowBaseValues(((PXSelectBase) this.Base.Transactions).Cache, (object) ((PXSelectBase<PMTran>) this.Base.Transactions).Current, (IEnumerable<CuryField>) this.TrackedItems[((PXSelectBase) this.Base.Transactions).Cache.GetItemType()]);
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo1)))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.tranCuryID>>) e).Cache.RaiseExceptionHandling<PMTran.tranCuryID>((object) e.Row, (object) null, (Exception) this.GetCurrencyRateError(currencyInfo1));
label_10:
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.GetCurrencyInfo(e.Row.BaseCuryInfoID);
      ((PXSelectBase) this.currencyinfo).Cache.SetValueExt<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) currencyInfo2, e.NewValue);
      GraphHelper.MarkUpdated(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo2, true);
      if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo2)))
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTran, PMTran.tranCuryID>>) e).Cache.RaiseExceptionHandling<PMTran.tranCuryID>((object) e.Row, (object) null, (Exception) this.GetCurrencyRateError(currencyInfo2));
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
    {
      if (((PXSelectBase<PMTran>) this.Base.Transactions).Current != null)
      {
        long? nullable = e.Row.CuryInfoID;
        long? projectCuryInfoId = ((PXSelectBase<PMTran>) this.Base.Transactions).Current.ProjectCuryInfoID;
        if (!(nullable.GetValueOrDefault() == projectCuryInfoId.GetValueOrDefault() & nullable.HasValue == projectCuryInfoId.HasValue))
        {
          long? curyInfoId = e.Row.CuryInfoID;
          nullable = ((PXSelectBase<PMTran>) this.Base.Transactions).Current.BaseCuryInfoID;
          if (!(curyInfoId.GetValueOrDefault() == nullable.GetValueOrDefault() & curyInfoId.HasValue == nullable.HasValue))
            goto label_4;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PMTran>) this.Base.Transactions).Current.Date;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cancel = true;
        goto label_6;
      }
label_4:
      if (e.Row.CuryEffDate.HasValue)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) e.Row.CuryEffDate;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cancel = true;
      }
label_6:
      base._(e);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
    {
      try
      {
        this.defaultCurrencyRate(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cache, e.Row, true, false);
      }
      catch (PXSetPropertyException ex)
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cache.RaiseExceptionHandling<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) e.Row, (object) e.Row.CuryEffDate, (Exception) this.GetCurrencyRateError(e.Row));
      }
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
    {
      if (e.Row == null)
        return;
      this.defaultEffectiveDate(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cache, e.Row);
      try
      {
        this.defaultCurrencyRate(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cache, e.Row, true, false);
      }
      catch (PXSetPropertyException ex)
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cache.RaiseExceptionHandling<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) e.Row, (object) e.Row.CuryID, (Exception) this.GetCurrencyRateError(e.Row));
      }
      e.Row.CuryPrecision = new short?();
    }

    protected override void _(PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID> e)
    {
      bool resetCuryID = false;
      this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).Cache, (IBqlTable) e.Row, resetCuryID);
    }

    public PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfoGetDefault(long? key)
    {
      return this.GetCurrencyInfo(key) ?? PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.CuryInfo).Search<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>((object) key, Array.Empty<object>()));
    }

    public void CalcCuryRatesForProject(PXCache cache, PMTran tran)
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Base.Project).Search<PMProject.contractID>((object) tran.ProjectID, Array.Empty<object>()));
      bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>();
      bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      long? nullable;
      if (flag1)
      {
        nullable = tran.ProjectCuryInfoID;
        if (nullable.HasValue)
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfoGetDefault = this.GetCurrencyInfoGetDefault(tran.ProjectCuryInfoID);
          currencyInfoGetDefault.BaseCuryID = pmProject.NonProject.GetValueOrDefault() || pmProject.BaseType != "P" ? ((PXSelectBase<PX.Objects.GL.Company>) this.Base.Company).Current.BaseCuryID : pmProject.CuryID;
          if (pmProject.RateTypeID != null)
            currencyInfoGetDefault.CuryRateTypeID = pmProject.RateTypeID;
          currencyInfoGetDefault.CuryEffDate = new DateTime?(DateTime.MinValue);
          ((PXSelectBase) this.currencyinfo).Cache.Update((object) currencyInfoGetDefault);
          currencyInfoGetDefault.CuryEffDate = tran.Date;
          ((PXSelectBase) this.currencyinfo).Cache.Update((object) currencyInfoGetDefault);
          if (!currencyInfoGetDefault.CuryRate.HasValue && !((PXGraph) this.Base).IsCopyPasteContext && !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfoGetDefault)))
            cache.RaiseExceptionHandling<PMTran.projectID>((object) tran, (object) null, (Exception) this.GetCurrencyRateError(currencyInfoGetDefault));
        }
      }
      if (!(flag1 | flag2))
        return;
      nullable = tran.BaseCuryInfoID;
      if (!nullable.HasValue)
        return;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfoGetDefault1 = this.GetCurrencyInfoGetDefault(tran.BaseCuryInfoID);
      if (flag2)
        this.SourceFieldUpdated<PMTran.baseCuryInfoID, PMTran.tranCuryID, PMTran.date>(cache, (IBqlTable) tran, false);
      if (flag1 && pmProject.RateTypeID != null)
        currencyInfoGetDefault1.CuryRateTypeID = pmProject.RateTypeID;
      currencyInfoGetDefault1.CuryEffDate = new DateTime?(DateTime.MinValue);
      ((PXSelectBase) this.currencyinfo).Cache.Update((object) currencyInfoGetDefault1);
      currencyInfoGetDefault1.CuryEffDate = tran.Date;
      ((PXSelectBase) this.currencyinfo).Cache.Update((object) currencyInfoGetDefault1);
      if (((PXGraph) this.Base).IsCopyPasteContext || string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfoGetDefault1)))
        return;
      cache.RaiseExceptionHandling<PMTran.projectID>((object) tran, (object) null, (Exception) this.GetCurrencyRateError(currencyInfoGetDefault1));
    }

    private PXSetPropertyException GetCurrencyRateError(PX.Objects.CM.Extensions.CurrencyInfo info)
    {
      return new PXSetPropertyException("Please define a conversion rate from the {0} to {1} currency within the {2} currency rate type and the {3:d} effective date on the Currency Rates (CM301000) form.", (PXErrorLevel) 2, new object[4]
      {
        (object) info.CuryID,
        (object) info.BaseCuryID,
        (object) info.CuryRateTypeID,
        (object) info.CuryEffDate
      });
    }

    public virtual void ConfigureCurrencyInfoAfterImport(PMTran tran)
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Base.Project).Search<PMProject.contractID>((object) tran.ProjectID, Array.Empty<object>()));
      string tranCuryId = tran.TranCuryID;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        CuryID = tranCuryId,
        BaseCuryID = pmProject.NonProject.GetValueOrDefault() ? ((PXSelectBase<PX.Objects.GL.Company>) this.Base.Company).Current.BaseCuryID : pmProject.CuryID,
        CuryRateTypeID = pmProject.RateTypeID ?? ((PXSelectBase<PX.Objects.CM.CMSetup>) this.Base.CMSetup).Current.PMRateTypeDflt,
        CuryEffDate = tran.Date
      });
      tran.ProjectCuryInfoID = currencyInfo1.CuryInfoID;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        CuryID = tranCuryId,
        CuryEffDate = tran.Date,
        CuryRateTypeID = pmProject.RateTypeID ?? ((PXSelectBase<PX.Objects.CM.CMSetup>) this.Base.CMSetup).Current.PMRateTypeDflt
      });
      tran.BaseCuryInfoID = currencyInfo2.CuryInfoID;
      tran.TranCuryID = tranCuryId ?? tran.TranCuryID;
    }

    protected override string GetBaseCurency()
    {
      PMTran current = ((PXSelectBase<PMTran>) this.Base.Transactions).Current;
      if (current != null && current.ProjectID.HasValue)
      {
        PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Base.Project).Search<PMProject.contractID>((object) current.ProjectID, Array.Empty<object>()));
        if (!pmProject.NonProject.GetValueOrDefault() && pmProject.BaseType == "P")
          return pmProject.BaseCuryID;
      }
      return base.GetBaseCurency();
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMTranTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXQuantity]
    [PXUIField(DisplayName = "Total Quantity", Enabled = false)]
    public virtual Decimal? QtyTotal { get; set; }

    [PXQuantity]
    [PXUIField(DisplayName = "Total Billable Quantity", Enabled = false)]
    public virtual Decimal? BillableQtyTotal { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Total Amount", Enabled = false)]
    public virtual Decimal? AmtTotal { get; set; }

    public abstract class qtyTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      RegisterEntry.PMTranTotal.qtyTotal>
    {
    }

    public abstract class billableQtyTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      RegisterEntry.PMTranTotal.billableQtyTotal>
    {
    }

    public abstract class amtTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      RegisterEntry.PMTranTotal.amtTotal>
    {
    }
  }

  [PXBreakInheritance]
  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class RecurringItemEx : PMRecurringItem
  {
    [PXDBInt(IsKey = true)]
    public override int? ProjectID { get; set; }

    [PXDBInt(IsKey = true)]
    public override int? TaskID { get; set; }

    [PXDBInt(IsKey = true)]
    public override int? InventoryID { get; set; }

    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RegisterEntry.RecurringItemEx.projectID>
    {
    }

    public new abstract class taskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RegisterEntry.RecurringItemEx.taskID>
    {
    }

    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RegisterEntry.RecurringItemEx.inventoryID>
    {
    }
  }

  public class CreatePMTran
  {
    public CreatePMTran(
      PMTimeActivity timeActivity,
      int? employeeID,
      DateTime date,
      int? timeSpent,
      int? timeBillable,
      Decimal? cost,
      Decimal? overtimeMult,
      string tranCuryID,
      bool insertTransaction)
    {
      this.TimeActivity = timeActivity;
      this.EmployeeID = employeeID;
      this.Date = date;
      this.TimeSpent = timeSpent;
      this.TimeBillable = timeBillable;
      this.Cost = cost;
      this.OvertimeMult = overtimeMult;
      this.TranCuryID = tranCuryID;
      this.InsertTransaction = insertTransaction;
    }

    public PMTimeActivity TimeActivity { get; }

    public int? EmployeeID { get; }

    public DateTime Date { get; }

    public int? TimeSpent { get; }

    public int? TimeBillable { get; }

    public Decimal? Cost { get; }

    public Decimal? OvertimeMult { get; }

    public string TranCuryID { get; }

    public bool InsertTransaction { get; }
  }
}
