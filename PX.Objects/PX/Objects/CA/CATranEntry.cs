// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.Exceptions;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CATranEntry : PXGraph<CATranEntry, CAAdj>, IVoucherEntry
{
  public PXInitializeState<CAAdj> initializeState;
  public PXAction<CAAdj> putOnHold;
  public PXAction<CAAdj> releaseFromHold;
  public PXWorkflowEventHandler<CAAdj> OnReleaseDocument;
  public PXWorkflowEventHandler<CAAdj> OnUpdateStatus;
  public PXAction<CAAdj> Release;
  protected bool reversingContext;
  public PXAction<CAAdj> Reverse;
  public PXAction<CAAdj> caReversingTransactions;
  [PXViewName("Cash Transactions")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CAAdj.cleared), typeof (CAAdj.clearDate), typeof (CAAdj.depositAsBatch), typeof (CAAdj.depositAfter), typeof (CAAdj.depositDate), typeof (CAAdj.deposited), typeof (CAAdj.depositType), typeof (CAAdj.depositNbr)})]
  public PXSelect<CAAdj, Where<CAAdj.draft, Equal<False>>> CAAdjRecords;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CAAdj.cleared), typeof (CAAdj.clearDate), typeof (CAAdj.depositAsBatch), typeof (CAAdj.depositAfter), typeof (CAAdj.depositDate), typeof (CAAdj.deposited), typeof (CAAdj.depositType), typeof (CAAdj.depositNbr)})]
  public PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>> CurrentDocument;
  [PXImport(typeof (CAAdj))]
  [PXViewName("CA Transaction Details")]
  public PXSelect<CASplit, Where<CASplit.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>, And<CASplit.adjTranType, Equal<Current<CAAdj.adjTranType>>>>> CASplitRecords;
  public PXSelect<CATax, Where<CATax.adjTranType, Equal<Current<CAAdj.adjTranType>>, And<CATax.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>>, OrderBy<Asc<CATax.adjTranType, Asc<CATax.adjRefNbr, Asc<CATax.taxID>>>>> Tax_Rows;
  public PXSelectJoin<CATaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CATaxTran.taxID>>>, Where<CATaxTran.module, Equal<BatchModule.moduleCA>, And<CATaxTran.tranType, Equal<Current<CAAdj.adjTranType>>, And<CATaxTran.refNbr, Equal<Current<CAAdj.adjRefNbr>>>>>> Taxes;
  public PXSelectReadonly2<CATaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CATaxTran.taxID>>>, Where<CATaxTran.module, Equal<BatchModule.moduleCA>, And<CATaxTran.tranType, Equal<Current<CAAdj.adjTranType>>, And<CATaxTran.refNbr, Equal<Current<CAAdj.adjRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>>>> UseTaxes;
  public PXSetup<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CAAdj.cashAccountID>>>> cashAccount;
  public PXSelect<CASetupApproval, Where<Current<CAAdj.adjTranType>, Equal<CATranType.cAAdjustment>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomation<CAAdj, CAAdj.approved, CAAdj.rejected, CAAdj.hold, CASetupApproval> Approval;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CAAdj.curyInfoID>>>> currencyinfo;
  public PXSetup<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<CAAdj.finPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<CAAdj.branchID>>>>> finperiod;
  public PXSetup<CASetup> casetup;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<CATran> catran;
  public PXSelect<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<CAAdj.taxZoneID>>>> taxzone;

  private bool IsReverseContext { get; set; }

  public PXAction DeleteButton => (PXAction) this.Delete;

  [Account(typeof (CASplit.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Where<PX.Objects.GL.Account.curyID, Equal<Current<CAAdj.curyID>>, Or<PX.Objects.GL.Account.curyID, IsNull>>, And<Match<Current<AccessInfo.userName>>>>>))]
  [PXDefault]
  protected virtual void CASplit_AccountID_CacheAttached(PXCache sender)
  {
  }

  public PXCache dailycache => ((PXGraph) this).Caches[typeof (CADailySummary)];

  public PXCache catrancache => ((PXGraph) this).Caches[typeof (CATran)];

  public PXCache gltrancache => ((PXGraph) this).Caches[typeof (PX.Objects.GL.GLTran)];

  public CATranEntry()
  {
    CASetup current = ((PXSelectBase<CASetup>) this.casetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<CAAdj.finPeriodID>(((PXSelectBase) this.CAAdjRecords).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.GL.Account.description>(((PXGraph) this).Caches[typeof (PX.Objects.GL.Account)], "Account Description");
    PXGraph.FieldSelectingEvents fieldSelecting = ((PXGraph) this).FieldSelecting;
    CATranEntry caTranEntry = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) caTranEntry, __vmethodptr(caTranEntry, CAAdj_TranID_CATran_BatchNbr_FieldSelecting));
    fieldSelecting.AddHandler<CAAdj.tranID_CATran_batchNbr>(pxFieldSelecting);
    PXUIFieldAttribute.SetVisible<CASplit.projectID>(((PXSelectBase) this.CASplitRecords).Cache, (object) null, ProjectAttribute.IsPMVisible("CA"));
    PXUIFieldAttribute.SetVisible<CASplit.taskID>(((PXSelectBase) this.CASplitRecords).Cache, (object) null, ProjectAttribute.IsPMVisible("CA"));
    PXUIFieldAttribute.SetVisible<CASplit.nonBillable>(((PXSelectBase) this.CASplitRecords).Cache, (object) null, ProjectAttribute.IsPMVisible("CA"));
    ((PXSelectBase) this.Approval).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Approval).Cache.AllowInsert = false;
    ((PXSelectBase) this.Approval).Cache.AllowDelete = false;
  }

  public PX.Objects.TX.TaxZone TAXZONE
  {
    get
    {
      return PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Select(Array.Empty<object>()));
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CATranEntry.\u003C\u003Ec__DisplayClass27_0 cDisplayClass270 = new CATranEntry.\u003C\u003Ec__DisplayClass27_0();
    ((PXAction) this.Save).Press();
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.registerList = new List<CARegister>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.registerList.Add(CATrxRelease.CARegister(current));
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass270, __methodptr(\u003Crelease\u003Eb__0)));
    return (IEnumerable) new List<CAAdj>() { current };
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable reverse(PXAdapter adapter)
  {
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if ((current != null ? (!current.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0 || !this.AskUserApprovalToReverse(current))
      return adapter.Get();
    using (new CATranEntry.ReverseContext(this))
      return this.ReverseTransaction(current);
  }

  private IEnumerable ReverseTransaction(CAAdj transaction)
  {
    CAAdj copy1 = (CAAdj) ((PXSelectBase) this.CAAdjRecords).Cache.CreateCopy((object) ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current);
    this.SetAdjFields(transaction, copy1);
    this.SetCleared(copy1);
    List<Tuple<CASplit, CASplit>> tupleList = new List<Tuple<CASplit, CASplit>>();
    foreach (PXResult<CASplit> pxResult in ((PXSelectBase<CASplit>) this.CASplitRecords).Select(Array.Empty<object>()))
    {
      CASplit caSplit1 = PXResult<CASplit>.op_Implicit(pxResult);
      CASplit copy2 = (CASplit) ((PXSelectBase) this.CASplitRecords).Cache.CreateCopy((object) caSplit1);
      copy2.AdjRefNbr = (string) null;
      copy2.CuryInfoID = new long?();
      copy2.NoteID = new Guid?();
      CASplit caSplit2 = copy2;
      Decimal? nullable = caSplit2.CuryTranAmt;
      Decimal num1 = (Decimal) -1;
      caSplit2.CuryTranAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num1) : new Decimal?();
      CASplit caSplit3 = copy2;
      nullable = caSplit3.CuryUnitPrice;
      Decimal num2 = (Decimal) -1;
      caSplit3.CuryUnitPrice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num2) : new Decimal?();
      CASplit caSplit4 = copy2;
      nullable = caSplit4.CuryTaxAmt;
      Decimal num3 = (Decimal) -1;
      caSplit4.CuryTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num3) : new Decimal?();
      CASplit caSplit5 = copy2;
      nullable = caSplit5.CuryTaxableAmt;
      Decimal num4 = (Decimal) -1;
      caSplit5.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num4) : new Decimal?();
      CASplit caSplit6 = copy2;
      nullable = caSplit6.TranAmt;
      Decimal num5 = (Decimal) -1;
      caSplit6.TranAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num5) : new Decimal?();
      CASplit caSplit7 = copy2;
      nullable = caSplit7.UnitPrice;
      Decimal num6 = (Decimal) -1;
      caSplit7.UnitPrice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num6) : new Decimal?();
      CASplit caSplit8 = copy2;
      nullable = caSplit8.TaxAmt;
      Decimal num7 = (Decimal) -1;
      caSplit8.TaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num7) : new Decimal?();
      CASplit caSplit9 = copy2;
      nullable = caSplit9.TaxableAmt;
      Decimal num8 = (Decimal) -1;
      caSplit9.TaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num8) : new Decimal?();
      copy2.AccountID = caSplit1.AccountID;
      copy2.SubID = caSplit1.SubID;
      tupleList.Add(new Tuple<CASplit, CASplit>(caSplit1, copy2));
    }
    List<CATaxTran> caTaxTranList = new List<CATaxTran>();
    foreach (PXResult<CATaxTran> pxResult in ((PXSelectBase<CATaxTran>) this.Taxes).Select(Array.Empty<object>()))
    {
      CATaxTran caTaxTran1 = PXResult<CATaxTran>.op_Implicit(pxResult);
      CATaxTran caTaxTran2 = new CATaxTran();
      caTaxTran2.AccountID = caTaxTran1.AccountID;
      caTaxTran2.BranchID = caTaxTran1.BranchID;
      caTaxTran2.FinPeriodID = caTaxTran1.FinPeriodID;
      caTaxTran2.SubID = caTaxTran1.SubID;
      caTaxTran2.TaxBucketID = caTaxTran1.TaxBucketID;
      caTaxTran2.TaxID = caTaxTran1.TaxID;
      caTaxTran2.TaxType = caTaxTran1.TaxType;
      caTaxTran2.TaxZoneID = caTaxTran1.TaxZoneID;
      caTaxTran2.TranDate = caTaxTran1.TranDate;
      caTaxTran2.VendorID = caTaxTran1.VendorID;
      caTaxTran2.CuryID = caTaxTran1.CuryID;
      caTaxTran2.Description = caTaxTran1.Description;
      caTaxTran2.NonDeductibleTaxRate = caTaxTran1.NonDeductibleTaxRate;
      caTaxTran2.TaxRate = caTaxTran1.TaxRate;
      CATaxTran caTaxTran3 = caTaxTran2;
      Decimal? nullable1 = caTaxTran1.CuryTaxableAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran3.CuryTaxableAmt = nullable2;
      CATaxTran caTaxTran4 = caTaxTran2;
      nullable1 = caTaxTran1.CuryExemptedAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran4.CuryExemptedAmt = nullable3;
      CATaxTran caTaxTran5 = caTaxTran2;
      nullable1 = caTaxTran1.CuryTaxAmt;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran5.CuryTaxAmt = nullable4;
      CATaxTran caTaxTran6 = caTaxTran2;
      nullable1 = caTaxTran1.CuryTaxAmtSumm;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran6.CuryTaxAmtSumm = nullable5;
      CATaxTran caTaxTran7 = caTaxTran2;
      nullable1 = caTaxTran1.CuryExpenseAmt;
      Decimal? nullable6 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran7.CuryExpenseAmt = nullable6;
      CATaxTran caTaxTran8 = caTaxTran2;
      nullable1 = caTaxTran1.TaxableAmt;
      Decimal? nullable7 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran8.TaxableAmt = nullable7;
      CATaxTran caTaxTran9 = caTaxTran2;
      nullable1 = caTaxTran1.ExemptedAmt;
      Decimal? nullable8 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran9.ExemptedAmt = nullable8;
      CATaxTran caTaxTran10 = caTaxTran2;
      nullable1 = caTaxTran1.TaxAmt;
      Decimal? nullable9 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran10.TaxAmt = nullable9;
      CATaxTran caTaxTran11 = caTaxTran2;
      nullable1 = caTaxTran1.ExpenseAmt;
      Decimal? nullable10 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caTaxTran11.ExpenseAmt = nullable10;
      caTaxTranList.Add(caTaxTran2);
    }
    ((PXSelectBase) this.finperiod).Cache.Current = ((PXSelectBase) this.finperiod).View.SelectSingleBound(new object[1]
    {
      (object) copy1
    }, Array.Empty<object>());
    this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<PX.Objects.GL.Batch.finPeriodID, PX.Objects.GL.Batch.branchID>(((PXSelectBase) this.CAAdjRecords).Cache, (object) copy1, (PXSelectBase<OrganizationFinPeriod>) this.finperiod);
    ((PXGraph) this).Clear();
    this.reversingContext = true;
    CAAdj caAdj = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Insert(copy1);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.CAAdjRecords).Cache, (object) transaction, ((PXSelectBase) this.CAAdjRecords).Cache, (object) caAdj, (PXNoteAttribute.IPXCopySettings) null);
    foreach (Tuple<CASplit, CASplit> tuple in tupleList)
    {
      CASplit caSplit10 = tuple.Item2;
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, caSplit10.InventoryID);
      if (inventoryItem != null)
      {
        bool? nullable = inventoryItem.IsConverted;
        if (nullable.GetValueOrDefault())
        {
          nullable = inventoryItem.StkItem;
          if (nullable.GetValueOrDefault())
            caSplit10.InventoryID = new int?();
        }
      }
      CASplit caSplit11 = ((PXSelectBase<CASplit>) this.CASplitRecords).Insert(caSplit10);
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.CASplitRecords).Cache, (object) tuple.Item1, ((PXSelectBase) this.CASplitRecords).Cache, (object) caSplit11, (PXNoteAttribute.IPXCopySettings) null);
    }
    this.reversingContext = false;
    foreach (CATaxTran caTaxTran in caTaxTranList)
      ((PXSelectBase<CATaxTran>) this.Taxes).Insert(caTaxTran);
    caAdj.ExternalTaxExemptionNumber = transaction.ExternalTaxExemptionNumber;
    caAdj.EntityUsageType = transaction.EntityUsageType;
    ((PXSelectBase) this.CAAdjRecords).Cache.SetValue<CAAdj.taxRoundDiff>((object) caAdj, (object) copy1.TaxRoundDiff);
    ((PXSelectBase) this.CAAdjRecords).Cache.SetValue<CAAdj.curyTaxRoundDiff>((object) caAdj, (object) copy1.CuryTaxRoundDiff);
    ((PXSelectBase) this.CAAdjRecords).Cache.SetValue<CAAdj.taxTotal>((object) caAdj, (object) copy1.TaxAmt);
    ((PXSelectBase) this.CAAdjRecords).Cache.SetValue<CAAdj.curyTaxTotal>((object) caAdj, (object) copy1.CuryTaxAmt);
    ((PXSelectBase) this.CAAdjRecords).Cache.SetValue<CAAdj.tranAmt>((object) caAdj, (object) copy1.TranAmt);
    ((PXSelectBase) this.CAAdjRecords).Cache.SetValue<CAAdj.curyTranAmt>((object) caAdj, (object) copy1.CuryTranAmt);
    CAAdj dest = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Update(caAdj);
    this.FinPeriodUtils.CopyPeriods<CAAdj, CAAdj.finPeriodID, CAAdj.tranPeriodID>(((PXSelectBase) this.CAAdjRecords).Cache, transaction, dest);
    return (IEnumerable) new List<CAAdj>() { dest };
  }

  protected virtual void SetAdjFields(CAAdj current, CAAdj adj)
  {
    adj.AdjRefNbr = (string) null;
    adj.Status = (string) null;
    adj.Approved = new bool?();
    adj.Hold = new bool?();
    adj.Released = new bool?();
    adj.TranID = new long?();
    adj.NoteID = new Guid?();
    adj.CurySplitTotal = new Decimal?();
    adj.CuryVatExemptTotal = new Decimal?();
    adj.CuryVatTaxableTotal = new Decimal?();
    adj.SplitTotal = new Decimal?();
    adj.VatExemptTotal = new Decimal?();
    adj.VatTaxableTotal = new Decimal?();
    CAAdj caAdj1 = adj;
    Decimal? curyTaxRoundDiff = caAdj1.CuryTaxRoundDiff;
    Decimal num1 = (Decimal) -1;
    caAdj1.CuryTaxRoundDiff = curyTaxRoundDiff.HasValue ? new Decimal?(curyTaxRoundDiff.GetValueOrDefault() * num1) : new Decimal?();
    CAAdj caAdj2 = adj;
    Decimal? curyControlAmt = caAdj2.CuryControlAmt;
    Decimal num2 = (Decimal) -1;
    caAdj2.CuryControlAmt = curyControlAmt.HasValue ? new Decimal?(curyControlAmt.GetValueOrDefault() * num2) : new Decimal?();
    CAAdj caAdj3 = adj;
    Decimal? curyTaxAmt = caAdj3.CuryTaxAmt;
    Decimal num3 = (Decimal) -1;
    caAdj3.CuryTaxAmt = curyTaxAmt.HasValue ? new Decimal?(curyTaxAmt.GetValueOrDefault() * num3) : new Decimal?();
    CAAdj caAdj4 = adj;
    Decimal? curyTaxTotal = caAdj4.CuryTaxTotal;
    Decimal num4 = (Decimal) -1;
    caAdj4.CuryTaxTotal = curyTaxTotal.HasValue ? new Decimal?(curyTaxTotal.GetValueOrDefault() * num4) : new Decimal?();
    CAAdj caAdj5 = adj;
    Decimal? curyTranAmt = caAdj5.CuryTranAmt;
    Decimal num5 = (Decimal) -1;
    caAdj5.CuryTranAmt = curyTranAmt.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() * num5) : new Decimal?();
    CAAdj caAdj6 = adj;
    Decimal? taxRoundDiff = caAdj6.TaxRoundDiff;
    Decimal num6 = (Decimal) -1;
    caAdj6.TaxRoundDiff = taxRoundDiff.HasValue ? new Decimal?(taxRoundDiff.GetValueOrDefault() * num6) : new Decimal?();
    CAAdj caAdj7 = adj;
    Decimal? taxAmt = caAdj7.TaxAmt;
    Decimal num7 = (Decimal) -1;
    caAdj7.TaxAmt = taxAmt.HasValue ? new Decimal?(taxAmt.GetValueOrDefault() * num7) : new Decimal?();
    CAAdj caAdj8 = adj;
    Decimal? controlAmt = caAdj8.ControlAmt;
    Decimal num8 = (Decimal) -1;
    caAdj8.ControlAmt = controlAmt.HasValue ? new Decimal?(controlAmt.GetValueOrDefault() * num8) : new Decimal?();
    CAAdj caAdj9 = adj;
    Decimal? taxTotal = caAdj9.TaxTotal;
    Decimal num9 = (Decimal) -1;
    caAdj9.TaxTotal = taxTotal.HasValue ? new Decimal?(taxTotal.GetValueOrDefault() * num9) : new Decimal?();
    CAAdj caAdj10 = adj;
    Decimal? tranAmt = caAdj10.TranAmt;
    Decimal num10 = (Decimal) -1;
    caAdj10.TranAmt = tranAmt.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() * num10) : new Decimal?();
    adj.EmployeeID = new int?();
    adj.OrigAdjTranType = current.AdjTranType;
    adj.OrigAdjRefNbr = current.AdjRefNbr;
    adj.ReverseCount = new int?();
    adj.DepositAsBatch = current.DepositAsBatch;
    adj.Deposited = new bool?(false);
    adj.DepositDate = new DateTime?();
    adj.DepositNbr = (string) null;
    adj.Cleared = new bool?(false);
    adj.ClearDate = new DateTime?();
  }

  protected virtual void SetCleared(CAAdj adj)
  {
    if (((PXSelectBase<CashAccount>) this.cashAccount).Current.Reconcile.GetValueOrDefault())
      return;
    adj.Cleared = new bool?(true);
    adj.ClearDate = adj.TranDate;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CAReversingTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<CAAdj>) this.CAAdjRecords).Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["TranType"] = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current.AdjTranType,
        ["RefNbr"] = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current.AdjRefNbr
      }, "CA659000", "CA Reversing Transactions", (CurrentLocalization) null);
    return adapter.Get();
  }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXDefault(typeof (CAAdj.tranDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<CREmployee.defContactID, Where<BqlOperand<CREmployee.bAccountID, IBqlInt>.IsEqual<BqlField<CAAdj.employeeID, IBqlInt>.FromCurrent>>>))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CAAdj.tranDesc))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (CAAdj.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CAAdj.curyTranAmt))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CAAdj.tranAmt))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  public bool AskUserApprovalToReverse(CAAdj origDoc)
  {
    return CATranEntry.GetReversingCAAdj((PXGraph) this, origDoc.AdjTranType, origDoc.AdjRefNbr).Count<CAAdj>() < 1 || ((PXSelectBase) this.CAAdjRecords).View.Ask(PXMessages.LocalizeNoPrefix("One or more reversing transactions already exist. Do you want to proceed with reversing the transaction?"), (MessageButtons) 4) == 6;
  }

  public static IEnumerable<CAAdj> GetReversingCAAdj(PXGraph graph, string tranType, string refNbr)
  {
    return GraphHelper.RowCast<CAAdj>((IEnumerable) PXSelectBase<CAAdj, PXSelectReadonly<CAAdj, Where<CAAdj.origAdjTranType, Equal<Required<CAAdj.origAdjTranType>>, And<CAAdj.origAdjRefNbr, Equal<Required<CAAdj.origAdjRefNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) tranType,
      (object) refNbr
    }));
  }

  public bool IsApprovalRequired(CAAdj doc, PXCache cache)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() & this.Approval.GetAssignedMaps(doc, cache).Any<ApprovalMap>();
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>>>))]
  public virtual void CATran_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CATran_BatchNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_ReferenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CAAdj_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    bool? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    if (!((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTotal.GetValueOrDefault())
    {
      sender.SetValue<CAAdj.curyControlAmt>((object) row, (object) row.CuryTranAmt);
    }
    else
    {
      nullable1 = row.Hold;
      if (!nullable1.GetValueOrDefault())
      {
        nullable2 = row.CuryControlAmt;
        nullable3 = row.CuryTranAmt;
        if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
          sender.RaiseExceptionHandling<CAAdj.curyControlAmt>((object) row, (object) row.CuryControlAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
        else
          sender.RaiseExceptionHandling<CAAdj.curyControlAmt>((object) row, (object) row.CuryControlAmt, (Exception) null);
      }
    }
    nullable1 = ((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTaxTotal;
    bool flag = nullable1.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>();
    nullable3 = row.CuryTaxTotal;
    nullable2 = row.CuryTaxAmt;
    int num1;
    if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue))
    {
      nullable1 = row.Hold;
      num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    int num2 = flag ? 1 : 0;
    if ((num1 & num2) != 0)
      sender.RaiseExceptionHandling<CAAdj.curyTaxAmt>((object) row, (object) row.CuryTaxAmt, (Exception) new PXSetPropertyException("Tax Amount must be equal to Tax Total."));
    else if (flag)
    {
      sender.RaiseExceptionHandling<CAAdj.curyTaxAmt>((object) row, (object) null, (Exception) null);
    }
    else
    {
      PXCache pxCache = sender;
      CAAdj caAdj = row;
      nullable2 = row.CuryTaxTotal;
      Decimal? nullable4;
      if (nullable2.HasValue)
      {
        nullable2 = row.CuryTaxTotal;
        Decimal num3 = 0M;
        if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
        {
          nullable4 = row.CuryTaxTotal;
          goto label_17;
        }
      }
      nullable4 = new Decimal?(0M);
label_17:
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) nullable4;
      pxCache.SetValueExt<CAAdj.curyTaxAmt>((object) caAdj, (object) local);
    }
    sender.RaiseExceptionHandling<CAAdj.curyTaxRoundDiff>((object) row, (object) null, (Exception) null);
    nullable1 = row.Hold;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = row.Released;
    if (nullable1.GetValueOrDefault())
      return;
    nullable2 = row.TaxRoundDiff;
    Decimal num4 = 0M;
    if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
      return;
    if (flag)
    {
      nullable2 = row.TaxRoundDiff;
      Decimal num5 = Math.Abs(nullable2.Value);
      nullable2 = CurrencyCollection.GetCurrency(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current.BaseCuryID).RoundingLimit;
      Decimal num6 = Math.Abs(nullable2.Value);
      if (!(num5 > num6))
        return;
      sender.RaiseExceptionHandling<CAAdj.curyTaxRoundDiff>((object) row, (object) row.CuryTaxRoundDiff, (Exception) new PXSetPropertyException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current.BaseCuryID,
        (object) PXDBQuantityAttribute.Round(row.TaxRoundDiff),
        (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current.BaseCuryID).RoundingLimit)
      }));
    }
    else if (!PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      sender.RaiseExceptionHandling<CAAdj.curyTaxRoundDiff>((object) row, (object) row.CuryTaxRoundDiff, (Exception) new PXSetPropertyException("Tax Amount cannot be edited because the Net/Gross Entry Mode feature is not enabled."));
    else
      sender.RaiseExceptionHandling<CAAdj.curyTaxRoundDiff>((object) row, (object) row.CuryTaxRoundDiff, (Exception) new PXSetPropertyException("Tax Amount cannot be edited if \"Validate Tax Totals on Entry\" is not selected on the CA Preferences form"));
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    map.Add(typeof (PX.Objects.AP.Vendor), typeof (PX.Objects.AP.Vendor));
  }

  protected virtual void CAAdj_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CAAdj row))
      return;
    bool flag1 = !this.IsApprovalRequired(row, sender);
    bool? nullable1 = row.DontApprove;
    bool flag2 = flag1;
    if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
      sender.SetValueExt<CAAdj.dontApprove>((object) row, (object) flag1);
    if (((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current != null && row.TaxZoneID != ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.TaxZoneID)
      ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current = (PX.Objects.TX.TaxZone) null;
    int? cashAccountId = row.CashAccountID;
    bool flag3 = cashAccountId.HasValue && row.EntryTypeID != null;
    nullable1 = row.Released;
    int num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    int num2;
    if (((PXSelectBase<EPApproval>) this.Approval).Any<EPApproval>())
    {
      nullable1 = row.Approved;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    int num3 = num2 == 0 ? 1 : 0;
    bool flag4 = (num1 & num3) != 0;
    sender.AllowDelete = flag4;
    ((PXSelectBase) this.CASplitRecords).Cache.AllowInsert = flag4 & flag3;
    ((PXSelectBase) this.CASplitRecords).Cache.AllowUpdate = flag4;
    ((PXSelectBase) this.CASplitRecords).Cache.AllowDelete = flag4;
    ((PXSelectBase) this.Taxes).Cache.AllowInsert = flag4 & flag3;
    ((PXSelectBase) this.Taxes).Cache.AllowUpdate = flag4;
    ((PXSelectBase) this.Taxes).Cache.AllowDelete = flag4;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CAAdj.adjRefNbr>(sender, (object) row, true);
    CashAccount cashAccount = (CashAccount) PXSelectorAttribute.Select<CAAdj.cashAccountID>(sender, (object) row);
    nullable1 = ((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTotal;
    bool flag5 = nullable1.Value;
    nullable1 = row.Released;
    int num4;
    if (!nullable1.GetValueOrDefault() && cashAccount != null)
    {
      nullable1 = cashAccount.Reconcile;
      num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    bool flag6 = num4 != 0;
    bool flag7 = !((PXSelectBase<CASplit>) this.CASplitRecords).Any<CASplit>();
    PXCache pxCache1 = sender;
    CAAdj caAdj1 = row;
    nullable1 = row.Released;
    int num5 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CAAdj.externalTaxExemptionNumber>(pxCache1, (object) caAdj1, num5 != 0);
    PXCache pxCache2 = sender;
    CAAdj caAdj2 = row;
    nullable1 = row.Released;
    int num6 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CAAdj.entityUsageType>(pxCache2, (object) caAdj2, num6 != 0);
    if (flag4)
    {
      PXUIFieldAttribute.SetEnabled<CAAdj.hold>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<CAAdj.cashAccountID>(sender, (object) row, flag7);
      PXUIFieldAttribute.SetEnabled<CAAdj.entryTypeID>(sender, (object) row, flag7);
      PXUIFieldAttribute.SetEnabled<CAAdj.extRefNbr>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<CAAdj.tranDate>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<CAAdj.finPeriodID>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<CAAdj.tranDesc>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<CAAdj.taxZoneID>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<CAAdj.curyControlAmt>(sender, (object) row, flag5);
      PXUIFieldAttribute.SetEnabled<CAAdj.cleared>(sender, (object) row, flag6);
      PXCache pxCache3 = sender;
      CAAdj caAdj3 = row;
      int num7;
      if (flag6)
      {
        nullable1 = row.Cleared;
        num7 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num7 = 0;
      PXUIFieldAttribute.SetEnabled<CAAdj.clearDate>(pxCache3, (object) caAdj3, num7 != 0);
      CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.EntryTypeID
      }));
      int num8;
      if (caEntryType != null)
      {
        nullable1 = caEntryType.UseToReclassifyPayments;
        num8 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num8 = 0;
      PXUIFieldAttribute.SetEnabled<CASplit.inventoryID>(((PXSelectBase) this.CASplitRecords).Cache, (object) null, num8 == 0);
    }
    PXUIFieldAttribute.SetVisible<CAAdj.curyControlAmt>(sender, (object) null, flag5);
    PXUIFieldAttribute.SetVisible<CAAdj.curyID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetRequired<CAAdj.curyControlAmt>(sender, flag5);
    nullable1 = row.PaymentsReclassification;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<CASplit.cashAccountID>(((PXSelectBase) this.CASplitRecords).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<CASplit.cashAccountID>(((PXSelectBase) this.CASplitRecords).Cache, (object) null, valueOrDefault);
    PXCache pxCache4 = sender;
    nullable1 = ((PXSelectBase<CASetup>) this.casetup).Current.RequireExtRefNbr;
    int num9 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<CAAdj.extRefNbr>(pxCache4, num9 != 0);
    int num10;
    if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
    {
      nullable1 = ((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTaxTotal;
      num10 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num10 = 0;
    bool flag8 = num10 != 0;
    PXUIFieldAttribute.SetVisible<CAAdj.curyTaxAmt>(sender, (object) row, flag8);
    PXUIFieldAttribute.SetEnabled<CAAdj.curyTaxAmt>(sender, (object) row, flag8);
    PXUIFieldAttribute.SetRequired<CAAdj.curyTaxAmt>(sender, flag8);
    PXCache pxCache5 = sender;
    CAAdj caAdj4 = row;
    nullable1 = row.Released;
    int num11 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CAAdj.taxCalcMode>(pxCache5, (object) caAdj4, num11 != 0);
    Decimal? curyTaxRoundDiff = row.CuryTaxRoundDiff;
    Decimal num12 = 0M;
    bool flag9 = !(curyTaxRoundDiff.GetValueOrDefault() == num12 & curyTaxRoundDiff.HasValue);
    PXUIFieldAttribute.SetVisible<CAAdj.curyTaxRoundDiff>(sender, (object) row, flag9);
    if (((PXSelectBase<CATaxTran>) this.UseTaxes).Select(Array.Empty<object>()).Count != 0)
      sender.RaiseExceptionHandling<CAAdj.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", (PXErrorLevel) 2));
    PXUIFieldAttribute.SetEnabled<CAAdj.depositAfter>(sender, (object) row, false);
    PXCache pxCache6 = sender;
    nullable1 = row.DepositAsBatch;
    int num13 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<CAAdj.depositAfter>(pxCache6, num13 != 0);
    nullable1 = row.DepositAsBatch;
    PXPersistingCheck pxPersistingCheck = nullable1.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXDefaultAttribute.SetPersistingCheck<CAAdj.depositAfter>(sender, (object) row, pxPersistingCheck);
    PXUIFieldAttribute.SetEnabled<CAAdj.depositDate>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CAAdj.depositAsBatch>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CAAdj.deposited>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CAAdj.depositNbr>(sender, (object) null, false);
    CashAccount current = ((PXSelectBase<CashAccount>) this.cashAccount).Current;
    int? nullable2;
    int num14;
    if (current != null)
    {
      cashAccountId = current.CashAccountID;
      nullable2 = row.CashAccountID;
      if (cashAccountId.GetValueOrDefault() == nullable2.GetValueOrDefault() & cashAccountId.HasValue == nullable2.HasValue)
      {
        nullable1 = current.ClearingAccount;
        num14 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_28;
      }
    }
    num14 = 0;
label_28:
    bool flag10 = num14 != 0;
    bool flag11 = !string.IsNullOrEmpty(row.DepositNbr) && !string.IsNullOrEmpty(row.DepositType);
    int num15;
    if (!flag11 && current != null)
    {
      if (!flag10)
      {
        nullable1 = row.DepositAsBatch;
        bool flag12 = flag10;
        num15 = !(nullable1.GetValueOrDefault() == flag12 & nullable1.HasValue) ? 1 : 0;
      }
      else
        num15 = 1;
    }
    else
      num15 = 0;
    bool flag13 = num15 != 0;
    if (flag13)
    {
      nullable1 = row.DepositAsBatch;
      bool flag14 = flag10;
      PXSetPropertyException propertyException = !(nullable1.GetValueOrDefault() == flag14 & nullable1.HasValue) ? new PXSetPropertyException("'Batch deposit' setting does not match 'Clearing Account' flag of the Cash Account", (PXErrorLevel) 2) : (PXSetPropertyException) null;
      sender.RaiseExceptionHandling<CAAdj.depositAsBatch>((object) row, (object) row.DepositAsBatch, (Exception) propertyException);
    }
    PXUIFieldAttribute.SetEnabled<CAAdj.depositAsBatch>(sender, (object) row, flag13);
    PXCache pxCache7 = sender;
    CAAdj caAdj5 = row;
    int num16;
    if (!flag11 & flag10)
    {
      nullable1 = row.DepositAsBatch;
      num16 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num16 = 0;
    PXUIFieldAttribute.SetEnabled<CAAdj.depositAfter>(pxCache7, (object) caAdj5, num16 != 0);
    PXCache pxCache8 = sender;
    CAAdj caAdj6 = row;
    nullable2 = row.ReverseCount;
    int num17 = 0;
    int num18 = nullable2.GetValueOrDefault() > num17 & nullable2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CAAdj.reverseCount>(pxCache8, (object) caAdj6, num18 != 0);
    PXUIFieldAttribute.SetVisible<CAAdj.origAdjRefNbr>(sender, (object) row, row.OrigAdjRefNbr != null);
  }

  protected virtual void CAAdj_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is CAAdj row) || !row.Released.GetValueOrDefault() || row.ReverseCount.HasValue)
      return;
    using (new PXConnectionScope())
      row.ReverseCount = new int?(CATranEntry.GetReversingCAAdj((PXGraph) this, row.AdjTranType, row.AdjRefNbr).Count<CAAdj>());
  }

  protected virtual void CAAdj_EntryTypeId_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CAAdj row))
      return;
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EntryTypeID
    }));
    if (caEntryType != null)
    {
      row.DrCr = caEntryType.DrCr;
      CAAdj caAdj = row;
      bool? reclassifyPayments = caEntryType.UseToReclassifyPayments;
      bool? nullable = new bool?(reclassifyPayments.GetValueOrDefault());
      caAdj.PaymentsReclassification = nullable;
      reclassifyPayments = caEntryType.UseToReclassifyPayments;
      if (reclassifyPayments.GetValueOrDefault() && row.CashAccountID.HasValue)
      {
        if (PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, NotEqual<Required<CashAccount.cashAccountID>>, And<CashAccount.curyID, Equal<Required<CashAccount.curyID>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[2]
        {
          (object) row.CashAccountID,
          (object) row.CuryID
        })) == null)
          sender.RaiseExceptionHandling<CAAdj.entryTypeID>((object) row, (object) null, (Exception) new PXSetPropertyException("This Entry Type requires to set a Cash Account with currency {0} as an Offset Account. Currently, there is no such a Cash Account defined in the system", (PXErrorLevel) 2, new object[1]
          {
            (object) row.CuryID
          }));
      }
    }
    sender.SetDefaultExt<CAAdj.taxZoneID>((object) row);
    sender.SetDefaultExt<CAAdj.taxCalcMode>((object) row);
  }

  protected virtual void CAAdj_Hold_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXSelectBase<CASetup>) this.casetup).Current.HoldEntry.GetValueOrDefault();
  }

  protected virtual void CAAdj_Status_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = ((PXSelectBase<CASetup>) this.casetup).Current.HoldEntry.GetValueOrDefault() ? (object) "H" : (object) "B";
  }

  protected virtual void CAAdj_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CAAdj row = e.Row as CAAdj;
    if (row.Cleared.GetValueOrDefault())
    {
      if (row.ClearDate.HasValue)
        return;
      row.ClearDate = row.TranDate;
    }
    else
      row.ClearDate = new DateTime?();
  }

  protected virtual void CAAdj_TranDesc_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      bool? released = row.Released;
      bool flag = false;
      num = !(released.GetValueOrDefault() == flag & released.HasValue) ? 1 : 0;
    }
    if (num != 0)
      return;
    foreach (PXResult<CATaxTran> pxResult in ((PXSelectBase<CATaxTran>) this.Taxes).Select(Array.Empty<object>()))
    {
      CATaxTran caTaxTran = PXResult<CATaxTran>.op_Implicit(pxResult);
      caTaxTran.Description = row.DocDesc;
      ((PXSelectBase) this.Taxes).Cache.Update((object) caTaxTran);
    }
  }

  protected virtual void CAAdj_CashAccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    row.Cleared = new bool?(false);
    row.ClearDate = new DateTime?();
    if (!row.CashAccountID.HasValue)
      return;
    if (((PXSelectBase<CashAccount>) this.cashAccount).Current != null)
    {
      int? cashAccountId1 = ((PXSelectBase<CashAccount>) this.cashAccount).Current.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
        goto label_4;
    }
    ((PXSelectBase<CashAccount>) this.cashAccount).Current = (CashAccount) PXSelectorAttribute.Select<CAAdj.cashAccountID>(sender, (object) row);
label_4:
    this.SetCleared(row);
    sender.SetDefaultExt<CAAdj.entryTypeID>(e.Row);
    sender.SetDefaultExt<CAAdj.depositAsBatch>(e.Row);
    sender.SetDefaultExt<CAAdj.depositAfter>(e.Row);
    sender.SetDefaultExt<CAAdj.externalTaxExemptionNumber>((object) row);
    sender.SetDefaultExt<CAAdj.entityUsageType>((object) row);
  }

  protected virtual void CAAdj_DepositAsBatch_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<CAAdj.depositAfter>(e.Row);
  }

  protected virtual void CAAdj_DepositAfter_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    if (!row.DepositAsBatch.GetValueOrDefault())
      return;
    e.NewValue = (object) row.TranDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<CAAdj.tranDate> e)
  {
    bool? released = ((CAAdj) e.Row).Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CAAdj.tranDate>>) e).Cache.SetDefaultExt<CAAdj.depositAfter>(e.Row);
  }

  protected virtual void CAAdj_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    PXPersistingCheck pxPersistingCheck = row.DepositAsBatch.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXDefaultAttribute.SetPersistingCheck<CAAdj.depositAfter>(sender, (object) row, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<CAAdj.extRefNbr>(sender, (object) row, ((PXSelectBase<CASetup>) this.casetup).Current.RequireExtRefNbr.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if ((e.Operation & 3) == 3 && row.Status == "R")
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("Released document cannot be deleted.");
    }
  }

  protected virtual void CAAdj_Rejected_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    if (!row.Rejected.GetValueOrDefault())
      return;
    row.Approved = new bool?(false);
    row.Hold = new bool?(false);
    row.Status = "J";
    cache.RaiseFieldUpdated<CAAdj.hold>(e.Row, (object) null);
  }

  protected virtual void CAAdj_TranID_CATran_BatchNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row != null && !e.IsAltered)
      return;
    string str = (string) null;
    if (sender.Graph.Caches[typeof (CATran)].GetStateExt<CATran.batchNbr>((object) null) is PXFieldState stateExt)
      str = stateExt.ViewName;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, new bool?(false), new bool?(false), new int?(0), new int?(0), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(true), new bool?(true), (PXUIVisibility) 3, str, (string[]) null, (string[]) null);
  }

  [PXDBCurrency(typeof (CATran.curyInfoID), typeof (CATran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void CATran_CuryTranAmt_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CASplit_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.IsReverseContext)
      return;
    CASplit row = e.Row as CASplit;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if (current == null || current.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, current.CashAccountID, current.EntryTypeID).AccountID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CASplit_AccountID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    CATranDetailHelper.OnAccountIdFieldUpdatedEvent(cache, e);
    CASplit row = (CASplit) e.Row;
    if (!row.InventoryID.HasValue)
      cache.SetDefaultExt<CASplit.taxCategoryID>(e.Row);
    int? projectId = row.ProjectID;
    if (projectId.HasValue)
    {
      projectId = row.ProjectID;
      int? nullable = ProjectDefaultAttribute.NonProject();
      if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue))
        return;
    }
    cache.SetDefaultExt<CASplit.projectID>(e.Row);
  }

  protected virtual void CASplit_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.IsReverseContext)
      return;
    CASplit row = e.Row as CASplit;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if (current == null || current.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, current.CashAccountID, current.EntryTypeID).SubID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CASplit_BranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.IsReverseContext)
      return;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    CASplit row = e.Row as CASplit;
    if (current == null || current.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, current.CashAccountID, current.EntryTypeID).BranchID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  private CASplit GetDefaultAccountValues(PXGraph graph, int? cashAccountID, string entryTypeID)
  {
    return CATranDetailHelper.CreateCATransactionDetailWithDefaultAccountValues<CASplit>(graph, cashAccountID, entryTypeID);
  }

  protected virtual void CASplit_CashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CATranDetailHelper.OnCashAccountIdFieldDefaultingEvent(sender, e);
  }

  protected virtual void CASplit_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATranDetailHelper.OnCashAccountIdFieldUpdatedEvent(sender, e);
  }

  protected virtual void CASplit_TranDesc_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if (current == null || current.EntryTypeID == null)
      return;
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.EntryTypeID
    }));
    if (caEntryType == null)
      return;
    e.NewValue = (object) caEntryType.Descr;
  }

  protected virtual void CASplit_CuryTranAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CASplit row1) || !((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTotal.GetValueOrDefault() || !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      return;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if (current == null)
      return;
    Decimal? nullable1 = new Decimal?(0M);
    if (string.IsNullOrEmpty(row1.TaxCategoryID))
      sender.SetDefaultExt<CASplit.taxCategoryID>((object) row1);
    ref Decimal? local1 = ref nullable1;
    PXCache cache = sender;
    CASplit row2 = row1;
    string taxZoneId = current.TaxZoneID;
    string taxCategoryId = row1.TaxCategoryID;
    DateTime aDocDate = current.TranDate.Value;
    string taxCalcMode = current.TaxCalcMode;
    Decimal ControlTotalAmt = current.CuryControlAmt.Value;
    Decimal? nullable2 = current.CurySplitTotal;
    Decimal LinesTotal = nullable2.Value;
    nullable2 = current.CuryTaxTotal;
    Decimal TaxTotal = nullable2.Value;
    Decimal num1 = TaxAttribute.CalcResidualAmt(cache, (object) row2, taxZoneId, taxCategoryId, aDocDate, taxCalcMode, ControlTotalAmt, LinesTotal, TaxTotal);
    local1 = new Decimal?(num1);
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    int num2 = Math.Sign(nullable1.Value);
    nullable2 = current.CuryControlAmt;
    int num3 = Math.Sign(nullable2.Value);
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local2 = (ValueType) (num2 == num3 ? nullable1 : new Decimal?(0M));
    defaultingEventArgs.NewValue = (object) local2;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CASplit_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CASplit row = e.Row as CASplit;
    if (this.reversingContext || row == null || TaxBaseAttribute.GetTaxCalc<CASplit.taxCategoryID>(sender, (object) row) != TaxCalc.Calc)
      return;
    int? nullable = row.InventoryID;
    if (nullable.HasValue)
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) null;
    nullable = row.AccountID;
    if (nullable.HasValue)
      account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.AccountID
      }));
    if (account != null && account.TaxCategoryID != null)
      e.NewValue = (object) account.TaxCategoryID;
    else if (((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID))
      e.NewValue = (object) ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID;
    else
      e.NewValue = (object) row.TaxCategoryID;
  }

  protected virtual void CASplit_TaxCategoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CASplit row = e.Row as CASplit;
    if (this.reversingContext || row == null)
      return;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if (((PXGraph) this).IsCopyPasteContext || !((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTotal.GetValueOrDefault() || !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      return;
    Decimal? nullable1 = row.CuryTranAmt;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.CuryTranAmt;
    if (!(nullable1.Value != 0M))
      return;
    nullable1 = row.Qty;
    Decimal num1 = 1M;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue) || !(current.TaxCalcMode == "N"))
      return;
    PXResultset<CATax> pxResultset = PXSelectBase<CATax, PXSelect<CATax, Where<CATax.adjTranType, Equal<Required<CATax.adjTranType>>, And<CATax.adjRefNbr, Equal<Required<CATax.adjRefNbr>>, And<CATax.lineNbr, Equal<Required<CATax.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.AdjTranType,
      (object) row.AdjRefNbr,
      (object) row.LineNbr
    });
    Decimal num2 = 0M;
    foreach (PXResult<CATax> pxResult in pxResultset)
    {
      CATax caTax = PXResult<CATax>.op_Implicit(pxResult);
      Decimal num3 = num2;
      nullable1 = caTax.CuryTaxAmt;
      Decimal num4 = nullable1.Value;
      num2 = num3 + num4;
    }
    Decimal? nullable2 = new Decimal?(TaxAttribute.CalcTaxableFromTotalAmount(sender, (object) row, current.TaxZoneID, row.TaxCategoryID, current.TranDate.Value, row.CuryTranAmt.Value + num2, false, TaxAttribute.TaxCalcLevelEnforcing.EnforceCalcOnItemAmount));
    sender.SetValueExt<CASplit.curyTranAmt>((object) row, (object) nullable2);
  }

  protected virtual void CASplit_InventoryId_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CASplit row = e.Row as CASplit;
    CAAdj current = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current;
    if (row != null)
    {
      int? nullable = row.InventoryID;
      if (nullable.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<CASplit.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.InventoryID
        }));
        nullable = row.AccountID;
        bool flag1 = !nullable.HasValue;
        bool flag2 = !((PXGraph) this).IsImportFromExcel && !((PXGraph) this).IsContractBasedAPI;
        if (inventoryItem != null && current != null && flag1 | flag2 && !this.IsReverseContext)
        {
          if (current.DrCr == "D")
          {
            row.AccountID = inventoryItem.SalesAcctID;
            row.SubID = inventoryItem.SalesSubID;
          }
          else
          {
            row.AccountID = inventoryItem.COGSAcctID;
            row.SubID = inventoryItem.COGSSubID;
          }
          row.TranDesc = inventoryItem.Descr;
        }
      }
    }
    sender.SetDefaultExt<CASplit.taxCategoryID>((object) row);
    sender.SetDefaultExt<CASplit.uOM>((object) row);
  }

  protected virtual void CASplit_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CASplit row = (CASplit) e.Row;
    if (row == null)
      return;
    bool valueOrDefault = ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current.PaymentsReclassification.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<CASplit.accountID>(sender, (object) row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<CASplit.subID>(sender, (object) row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<CASplit.branchID>(sender, (object) row, !valueOrDefault);
  }

  protected virtual void CASplit_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CATranDetailHelper.OnCATranDetailRowUpdatingEvent(sender, e);
    if (CATranDetailHelper.VerifyOffsetCashAccount(sender, (ICATranDetail) (e.NewRow as CASplit), (int?) ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current?.CashAccountID))
      ((CancelEventArgs) e).Cancel = true;
    sender.SetValueExt<CASplit.curyTranAmt>(e.NewRow, (object) (e.NewRow as CASplit).CuryTranAmt);
  }

  protected virtual void CASplit_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    CATranDetailHelper.VerifyOffsetCashAccount(sender, (ICATranDetail) (e.Row as CASplit), (int?) ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current?.CashAccountID);
    sender.SetValueExt<CASplit.curyTranAmt>(e.Row, (object) (e.Row as CASplit).CuryTranAmt);
  }

  protected virtual void CASplit_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    TaxBaseAttribute.Calculate<CASplit.taxCategoryID>(sender, e);
  }

  protected virtual void CASplit_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    CASplit row = (CASplit) e.Row;
    int? nullable;
    if (((PXGraph) this).IsImport && row != null)
    {
      nullable = row.AccountID;
      if (!nullable.HasValue)
        sender.SetDefaultExt<CASplit.accountID>((object) row);
    }
    if (!((PXGraph) this).IsImport || row == null)
      return;
    nullable = row.SubID;
    if (nullable.HasValue)
      return;
    sender.SetDefaultExt<CASplit.subID>((object) row);
  }

  protected virtual void CASplit_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CASplit row = e.Row as CASplit;
    object projectId = (object) row.ProjectID;
    try
    {
      sender.RaiseFieldVerifying<CASplit.projectID>((object) row, ref projectId);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling<CASplit.projectID>((object) row, projectId, (Exception) ex);
    }
  }

  protected virtual void CASplit_Qty_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    e.NewValue = (object) 1M;
  }

  protected virtual void CATaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CATaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
  }

  protected virtual void CATaxTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (this.reversingContext)
      ((CancelEventArgs) e).Cancel = true;
    PXParentAttribute.SetParent(sender, e.Row, typeof (CAAdj), (object) ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current);
  }

  protected virtual void CATaxTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    ((PXSelectBase) this.Taxes).View.RequestRefresh();
  }

  protected virtual void CATaxTran_TaxType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current == null)
      return;
    if (((PXSelectBase<CAAdj>) this.CAAdjRecords).Current.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((TaxDetail) e.Row).TaxID
      }));
      if (purchaseTax == null)
        return;
      e.NewValue = (object) purchaseTax.TranTaxType;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((TaxDetail) e.Row).TaxID
      }));
      if (salesTax == null)
        return;
      e.NewValue = (object) salesTax.TranTaxType;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CATaxTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current == null)
      return;
    if (((PXSelectBase<CAAdj>) this.CAAdjRecords).Current.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((TaxDetail) e.Row).TaxID
      }));
      if (purchaseTax == null)
        return;
      e.NewValue = (object) purchaseTax.HistTaxAcctID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((TaxDetail) e.Row).TaxID
      }));
      if (salesTax == null)
        return;
      e.NewValue = (object) salesTax.HistTaxAcctID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CATaxTran_SubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current == null)
      return;
    if (((PXSelectBase<CAAdj>) this.CAAdjRecords).Current.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((TaxDetail) e.Row).TaxID
      }));
      if (purchaseTax == null)
        return;
      e.NewValue = (object) purchaseTax.HistTaxSubID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((TaxDetail) e.Row).TaxID
      }));
      if (salesTax == null)
        return;
      e.NewValue = (object) salesTax.HistTaxSubID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CATaxTran_TaxBucketID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAAdj>) this.CAAdjRecords).Current == null)
      return;
    PX.Objects.TX.Tax tax = PXResultset<PX.Objects.TX.Tax>.op_Implicit(PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) ((TaxDetail) e.Row).TaxID
    }));
    if ((tax != null ? (tax.IsExternal.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    e.NewValue = (object) 0;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<CATaxTran, CATaxTran.taxID> e)
  {
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CATaxTran, CATaxTran.taxID>, CATaxTran, object>) e).OldValue == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CATaxTran, CATaxTran.taxID>, CATaxTran, object>) e).OldValue == e.NewValue)
      return;
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<CATaxTran.accountID>((object) e.Row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<CATaxTran.taxBucketID>((object) e.Row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<CATaxTran.vendorID>((object) e.Row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<CATaxTran.subID>((object) e.Row);
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual CAAdj CalculateExternalTax(CAAdj invoice) => invoice;

  internal class ReverseContext : IDisposable
  {
    private readonly CATranEntry _graph;
    private bool suppressPeriodValidation;

    public ReverseContext(CATranEntry graph)
    {
      this._graph = graph;
      try
      {
        graph.FinPeriodRepository.GetFinPeriodByDate(((PXSelectBase<CAAdj>) graph.CAAdjRecords).Current.TranDate, PXAccess.GetParentOrganizationID(((PXGraph) graph).Accessinfo.BranchID));
      }
      catch (FinancialPeriodNotDefinedForDateException ex)
      {
        this.suppressPeriodValidation = true;
        OpenPeriodAttribute.SetValidatePeriod<CAAdj.finPeriodID>(((PXSelectBase) graph.CAAdjRecords).Cache, (object) null, PeriodValidation.Nothing);
      }
      this._graph.IsReverseContext = true;
    }

    public void Dispose()
    {
      if (this.suppressPeriodValidation)
        OpenPeriodAttribute.SetValidatePeriod<CAAdj.finPeriodID>(((PXSelectBase) this._graph.CAAdjRecords).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
      this._graph.IsReverseContext = false;
    }
  }

  public class CATranEntryDocumentExtension : DocumentWithLinesGraphExtension<CATranEntry>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>((PXSelectBase) this.Base.CAAdjRecords);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.CASplitRecords);
    }

    protected override DocumentMapping GetDocumentMapping()
    {
      return new DocumentMapping(typeof (CAAdj))
      {
        HeaderTranPeriodID = typeof (CAAdj.tranPeriodID),
        HeaderDocDate = typeof (CAAdj.tranDate)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (CASplit));
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
      if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow))
        return;
      ((PXSelectBase) this.Lines).Cache.SetDefaultExt<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine.tranDate>((object) line);
    }
  }

  public class CATranEntry_ActivityDetailsExt : ActivityDetailsExt<CATranEntry, CAAdj, CAAdj.noteID>
  {
  }
}
