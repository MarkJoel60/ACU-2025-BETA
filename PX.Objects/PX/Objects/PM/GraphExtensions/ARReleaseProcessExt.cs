// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ARReleaseProcessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ARReleaseProcessExt : PXGraphExtension<ARReleaseProcess>
{
  public PXSelect<PMTran, Where<PMTran.aRTranType, Equal<Required<PMTran.aRTranType>>, And<PMTran.aRRefNbr, Equal<Required<PMTran.aRRefNbr>>>>> ARDoc_PMTran;
  protected Dictionary<int, List<PMTran>> pmtranByARTranLineNbr = new Dictionary<int, List<PMTran>>();
  protected Dictionary<string, Queue<PMTran>> billedInOriginalInvoice = new Dictionary<string, Queue<PMTran>>();
  protected List<PXResult<PX.Objects.AR.ARTran, PMTran>> creditMemoPMReversal = new List<PXResult<PX.Objects.AR.ARTran, PMTran>>();
  protected List<PMTran> allocationsPMReversal = new List<PMTran>();
  protected List<PMTran> remainders = new List<PMTran>();
  protected List<Tuple<PMProformaTransactLine, PMTran>> billLaterPMList = new List<Tuple<PMProformaTransactLine, PMTran>>();
  protected Dictionary<int, Tuple<PMProformaTransactLine, PMTran>> billLaterLinesWithFirstTransaction = new Dictionary<int, Tuple<PMProformaTransactLine, PMTran>>();
  protected List<PMRegister> ProjectDocuments;
  private bool isProformaCorrectionProcess;
  private bool isCorrectedProformaInvoice;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  [PXOverride]
  public virtual PX.Objects.AR.ARRegister OnBeforeRelease(
    PX.Objects.AR.ARRegister ardoc,
    Func<PX.Objects.AR.ARRegister, PX.Objects.AR.ARRegister> baseMethod)
  {
    return baseMethod(ardoc);
  }

  [PXOverride]
  public virtual List<PX.Objects.AR.ARRegister> ReleaseInvoice(
    JournalEntry je,
    PX.Objects.AR.ARRegister doc,
    PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AR.Customer, PX.Objects.GL.Account> res,
    List<PMRegister> pmDocs,
    Func<JournalEntry, PX.Objects.AR.ARRegister, PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AR.Customer, PX.Objects.GL.Account>, List<PMRegister>, List<PX.Objects.AR.ARRegister>> baseMethod)
  {
    this.ProjectDocuments = pmDocs;
    this.pmtranByARTranLineNbr.Clear();
    this.billedInOriginalInvoice.Clear();
    this.creditMemoPMReversal.Clear();
    this.allocationsPMReversal.Clear();
    this.remainders.Clear();
    this.billLaterPMList.Clear();
    this.billLaterLinesWithFirstTransaction.Clear();
    PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AR.Customer, PX.Objects.GL.Account>.op_Implicit(res);
    if (arInvoice.ProjectID.HasValue && !ProjectDefaultAttribute.IsNonProject(arInvoice.ProjectID))
    {
      foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.ARDoc_PMTran).Select(new object[2]
      {
        (object) arInvoice.DocType,
        (object) arInvoice.RefNbr
      }))
      {
        PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
        if (pmTran.RemainderOfTranID.HasValue)
          this.remainders.Add(pmTran);
        int? refLineNbr = pmTran.RefLineNbr;
        if (refLineNbr.HasValue)
        {
          Dictionary<int, List<PMTran>> pmtranByArTranLineNbr1 = this.pmtranByARTranLineNbr;
          refLineNbr = pmTran.RefLineNbr;
          int key1 = refLineNbr.Value;
          List<PMTran> pmTranList1;
          ref List<PMTran> local = ref pmTranList1;
          if (!pmtranByArTranLineNbr1.TryGetValue(key1, out local))
          {
            pmTranList1 = new List<PMTran>();
            Dictionary<int, List<PMTran>> pmtranByArTranLineNbr2 = this.pmtranByARTranLineNbr;
            refLineNbr = pmTran.RefLineNbr;
            int key2 = refLineNbr.Value;
            List<PMTran> pmTranList2 = pmTranList1;
            pmtranByArTranLineNbr2.Add(key2, pmTranList2);
          }
          pmTranList1.Add(pmTran);
        }
      }
    }
    PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.aRInvoiceDocType, Equal<Required<PMProforma.aRInvoiceDocType>>, And<PMProforma.aRInvoiceRefNbr, Equal<Required<PMProforma.aRInvoiceRefNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arInvoice.DocType,
      (object) arInvoice.RefNbr
    }));
    if (pmProforma != null)
    {
      int? revisionId = pmProforma.RevisionID;
      int num = 1;
      this.isCorrectedProformaInvoice = revisionId.GetValueOrDefault() > num & revisionId.HasValue;
      foreach (PXResult<PMTran, PMProformaTransactLine> pxResult in ((PXSelectBase<PMTran>) new PXSelectJoin<PMTran, InnerJoin<PMProformaTransactLine, On<PMProformaTransactLine.refNbr, Equal<PMTran.proformaRefNbr>, And<PMProformaTransactLine.lineNbr, Equal<PMTran.proformaLineNbr>>>>, Where<PMProformaTransactLine.refNbr, Equal<Required<PMProformaTransactLine.refNbr>>, And<PMProformaTransactLine.revisionID, Equal<Required<PMProformaTransactLine.revisionID>>, And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>, And<PMProformaTransactLine.option, NotEqual<PMProformaLine.option.writeOffRemainder>, And<PMProformaTransactLine.option, NotEqual<PMProformaLine.option.bill>>>>>>>((PXGraph) this.Base)).Select(new object[2]
      {
        (object) pmProforma.RefNbr,
        (object) pmProforma.RevisionID
      }))
      {
        PMTran pmTran = PXResult<PMTran, PMProformaTransactLine>.op_Implicit(pxResult);
        PMProformaTransactLine proformaTransactLine = PXResult<PMTran, PMProformaTransactLine>.op_Implicit(pxResult);
        if (proformaTransactLine.Option == "X")
        {
          if (!string.IsNullOrEmpty(pmTran.AllocationID) && pmTran.Reverse != "B")
            this.allocationsPMReversal.Add(pmTran);
          if (pmTran.RemainderOfTranID.HasValue)
            this.remainders.Add(pmTran);
        }
        else if (proformaTransactLine.Option == "U")
        {
          Decimal? curyLineTotal = proformaTransactLine.CuryLineTotal;
          Decimal? curyBillableAmount = proformaTransactLine.CuryBillableAmount;
          if (curyLineTotal.GetValueOrDefault() < curyBillableAmount.GetValueOrDefault() & curyLineTotal.HasValue & curyBillableAmount.HasValue)
          {
            Dictionary<int, Tuple<PMProformaTransactLine, PMTran>> firstTransaction1 = this.billLaterLinesWithFirstTransaction;
            int? lineNbr = proformaTransactLine.LineNbr;
            int key3 = lineNbr.Value;
            if (!firstTransaction1.ContainsKey(key3))
            {
              Dictionary<int, Tuple<PMProformaTransactLine, PMTran>> firstTransaction2 = this.billLaterLinesWithFirstTransaction;
              lineNbr = proformaTransactLine.LineNbr;
              int key4 = lineNbr.Value;
              Tuple<PMProformaTransactLine, PMTran> tuple = new Tuple<PMProformaTransactLine, PMTran>(proformaTransactLine, pmTran);
              firstTransaction2.Add(key4, tuple);
            }
          }
        }
      }
      this.billLaterPMList.AddRange((IEnumerable<Tuple<PMProformaTransactLine, PMTran>>) this.billLaterLinesWithFirstTransaction.Values);
    }
    if (doc.DocType == "CRM" && doc.OrigDocType == "INV")
    {
      foreach (PXResult<PMTran, PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PMTran>) new PXSelectJoin<PMTran, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.tranType, Equal<PMTran.aRTranType>, And<PX.Objects.AR.ARTran.refNbr, Equal<PMTran.aRRefNbr>, And<PX.Objects.AR.ARTran.lineNbr, Equal<PMTran.refLineNbr>>>>>, Where<PMTran.aRTranType, Equal<Required<PMTran.aRTranType>>, And<PMTran.aRRefNbr, Equal<Required<PMTran.aRRefNbr>>, And<PMTran.taskID, IsNotNull>>>, OrderBy<Asc<PX.Objects.AR.ARTran.lineNbr>>>((PXGraph) this.Base)).Select(new object[2]
      {
        (object) doc.OrigDocType,
        (object) doc.OrigRefNbr
      }))
      {
        PMTran pmTran = PXResult<PMTran, PX.Objects.AR.ARTran>.op_Implicit(pxResult);
        PX.Objects.AR.ARTran arTran = PXResult<PMTran, PX.Objects.AR.ARTran>.op_Implicit(pxResult);
        string key = $"{arTran.TaskID}.{arTran.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID}";
        if (!this.billedInOriginalInvoice.ContainsKey(key))
          this.billedInOriginalInvoice.Add(key, new Queue<PMTran>());
        this.billedInOriginalInvoice[key].Enqueue(pmTran);
      }
    }
    return baseMethod(je, doc, res, pmDocs);
  }

  [PXOverride]
  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    PX.Objects.AR.ARInvoice ardoc,
    PXResult<PX.Objects.AR.ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran> r,
    PX.Objects.GL.GLTran tran,
    Action<JournalEntry, PX.Objects.AR.ARInvoice, PXResult<PX.Objects.AR.ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>, PX.Objects.GL.GLTran> baseMethod)
  {
    baseMethod(je, ardoc, r, tran);
    if (ardoc.IsRetainageDocument.GetValueOrDefault() || ardoc.ProformaExists.GetValueOrDefault())
      return;
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)];
    PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, ARTax, PX.Objects.TX.Tax, DRDeferredCode, PX.Objects.SO.SOOrderType, ARTaxTran>.op_Implicit(r);
    if (arTran.TaskID.HasValue && arTran.AccountID.HasValue && PXSelectorAttribute.Select<PX.Objects.AR.ARTran.accountID>(cach, (object) arTran, (object) arTran.AccountID) is PX.Objects.GL.Account account && !account.AccountGroupID.HasValue)
      throw new PXException("Revenue Account {0} is not mapped to Account Group.", new object[1]
      {
        (object) account.AccountCD
      });
  }

  [PXOverride]
  public virtual void ReleaseInvoiceTransactionPostProcessed(
    JournalEntry je,
    PX.Objects.AR.ARInvoice ardoc,
    PX.Objects.AR.ARTran n)
  {
    int? nullable;
    if (this.billedInOriginalInvoice.Count != 0)
    {
      string key = $"{n.TaskID}.{n.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID}";
      Queue<PMTran> pmTranQueue = (Queue<PMTran>) null;
      if (this.billedInOriginalInvoice.TryGetValue(key, out pmTranQueue) && pmTranQueue.Count > 0)
      {
        while (pmTranQueue.Count > 0)
        {
          PMTran pmTran = pmTranQueue.Dequeue();
          this.creditMemoPMReversal.Add(new PXResult<PX.Objects.AR.ARTran, PMTran>(n, pmTran));
        }
      }
    }
    else
    {
      nullable = n.TaskID;
      if (nullable.HasValue && ardoc.DocType == "CRM" && ardoc.OrigDocType == "INV" && n.OrigInvoiceDate.HasValue)
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) n.AccountID
        }));
        if (PXResultset<PMProformaRevision>.op_Implicit(PXSelectBase<PMProformaRevision, PXSelect<PMProformaRevision, Where<PMProformaRevision.reversedARInvoiceDocType, Equal<Required<PMProformaRevision.reversedARInvoiceDocType>>, And<PMProformaRevision.reversedARInvoiceRefNbr, Equal<Required<PMProformaRevision.reversedARInvoiceRefNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) ardoc.DocType,
          (object) ardoc.RefNbr
        })) == null)
        {
          string curyId = ardoc.CuryID;
          PX.Objects.AR.ARTran line = n;
          int? revenueAccountGroup;
          if (account == null)
          {
            nullable = new int?();
            revenueAccountGroup = nullable;
          }
          else
            revenueAccountGroup = account.AccountGroupID;
          this.RestoreAmountToInvoice(curyId, line, revenueAccountGroup);
        }
      }
    }
    Dictionary<int, List<PMTran>> pmtranByArTranLineNbr = this.pmtranByARTranLineNbr;
    nullable = n.LineNbr;
    int key1 = nullable.Value;
    List<PMTran> pmTranList;
    ref List<PMTran> local = ref pmTranList;
    if (!pmtranByArTranLineNbr.TryGetValue(key1, out local))
      return;
    foreach (PMTran pmTran in pmTranList)
    {
      if (pmTran.Reverse == "I")
        this.allocationsPMReversal.Add(pmTran);
    }
  }

  [PXOverride]
  public virtual void ReleaseInvoiceBatchPostProcessing(
    JournalEntry je,
    PX.Objects.AR.ARInvoice ardoc,
    Batch arbatch)
  {
    if (this.Base.IsIntegrityCheck)
      return;
    PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.origDocType, Equal<PMOrigDocType.allocationReversal>, And<PMRegister.origNoteID, Equal<Required<PX.Objects.AR.ARInvoice.noteID>>, And<PMRegister.released, Equal<False>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ardoc.NoteID
    }));
    if (pmRegister != null)
      this.ProjectDocuments.Add(pmRegister);
    if (this.creditMemoPMReversal.Count <= 0 && this.billLaterPMList.Count <= 0 && this.allocationsPMReversal.Count <= 0 && this.remainders.Count <= 0)
      return;
    RegisterEntry instance1 = PXGraph.CreateInstance<RegisterEntry>();
    ((PXGraph) instance1).GetExtension<RegisterEntry.MultiCurrency>().UseDocumentRowInsertingFromBase = true;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance1).FieldVerifying.AddHandler<PMTran.projectID>(ARReleaseProcessExt.\u003C\u003Ec.\u003C\u003E9__20_0 ?? (ARReleaseProcessExt.\u003C\u003Ec.\u003C\u003E9__20_0 = new PXFieldVerifying((object) ARReleaseProcessExt.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseInvoiceBatchPostProcessing\u003Eb__20_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance1).FieldVerifying.AddHandler<PMTran.taskID>(ARReleaseProcessExt.\u003C\u003Ec.\u003C\u003E9__20_1 ?? (ARReleaseProcessExt.\u003C\u003Ec.\u003C\u003E9__20_1 = new PXFieldVerifying((object) ARReleaseProcessExt.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseInvoiceBatchPostProcessing\u003Eb__20_1))));
    if (this.creditMemoPMReversal.Count > 0)
    {
      ((PXGraph) instance1).Clear();
      if (!PXContext.GetSlot<bool>("ProcessRevision"))
      {
        instance1.ReverseCreditMemo((PX.Objects.AR.ARRegister) ardoc, this.creditMemoPMReversal, this.remainders, true);
        ((PXGraph) instance1).Actions.PressSave();
        this.ProjectDocuments.Add(((PXSelectBase<PMRegister>) instance1.Document).Current);
      }
      else
      {
        int count = this.remainders.Count;
        instance1.ReverseCreditMemo((PX.Objects.AR.ARRegister) ardoc, this.creditMemoPMReversal, this.remainders, false);
        if (this.remainders.Count > count)
        {
          RegisterEntry instance2 = PXGraph.CreateInstance<RegisterEntry>();
          ((PXGraph) instance2).Clear();
          ((PXSelectBase<PMRegister>) instance2.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance2.Document).Search<PMRegister.refNbr>((object) this.remainders[count].RefNbr, new object[1]
          {
            (object) this.remainders[count].TranType
          }));
          foreach (PMTran pmTran1 in ((PXSelectBase) instance1.Transactions).Cache.Updated)
          {
            PMTran pmTran2 = (PMTran) ((PXSelectBase) instance2.Transactions).Cache.Locate((object) pmTran1);
            if (pmTran2 != null)
            {
              pmTran2.ExcludedFromBilling = pmTran1.ExcludedFromBilling;
              pmTran2.ExcludedFromBillingReason = pmTran1.ExcludedFromBillingReason;
              GraphHelper.MarkUpdated(((PXSelectBase) instance2.Transactions).Cache, (object) pmTran2, true);
            }
            else
              GraphHelper.MarkUpdated(((PXSelectBase) instance2.Transactions).Cache, (object) pmTran1, true);
          }
          ((PXGraph) instance2).Persist();
          ((PXGraph) instance1).Clear();
        }
      }
    }
    if (this.billLaterPMList.Count > 0)
    {
      ((PXGraph) instance1).Clear();
      instance1.BillLater((PX.Objects.AR.ARRegister) ardoc, this.billLaterPMList);
      ((PXGraph) instance1).Actions.PressSave();
      this.ProjectDocuments.Add(((PXSelectBase<PMRegister>) instance1.Document).Current);
    }
    if (this.allocationsPMReversal.Count > 0 && !this.isCorrectedProformaInvoice)
    {
      ((PXGraph) instance1).Clear();
      instance1.ReverseAllocations((PX.Objects.AR.ARRegister) ardoc, this.allocationsPMReversal);
      ((PXGraph) instance1).Actions.PressSave();
      this.ProjectDocuments.Add(((PXSelectBase<PMRegister>) instance1.Document).Current);
    }
    if (this.remainders.Count <= 0)
      return;
    List<PMTran> remaindersToReverse = instance1.GetRemaindersToReverse(this.remainders);
    if (remaindersToReverse.Count <= 0)
      return;
    ((PXGraph) instance1).Clear();
    instance1.ReverseRemainders((PX.Objects.AR.ARRegister) ardoc, remaindersToReverse);
    ((PXGraph) instance1).Actions.PressSave();
    this.ProjectDocuments.Add(((PXSelectBase<PMRegister>) instance1.Document).Current);
  }

  [PXOverride]
  public virtual APReleaseProcess.LineBalances AdjustInvoiceDetailsBalanceByLine(
    PX.Objects.AR.ARRegister doc,
    PX.Objects.AR.ARTran tran,
    Func<PX.Objects.AR.ARRegister, PX.Objects.AR.ARTran, APReleaseProcess.LineBalances> baseMethod)
  {
    APReleaseProcess.LineBalances lineBalances = baseMethod(doc, tran);
    this.AdjustProjectBudgetRetainage(doc, tran);
    return lineBalances;
  }

  [PXOverride]
  public virtual void AdjustOriginalRetainageLineBalance(
    PX.Objects.AR.ARRegister document,
    PX.Objects.AR.ARTran tran,
    Decimal? curyAmount,
    Decimal? baseAmount,
    Action<PX.Objects.AR.ARRegister, PX.Objects.AR.ARTran, Decimal?, Decimal?> baseMethod)
  {
    baseMethod(document, tran, curyAmount, baseAmount);
    PX.Objects.AR.ARTran originalRetainageLine = this.Base.GetOriginalRetainageLine(document, tran);
    if (originalRetainageLine == null || this.Base.IsIntegrityCheck)
      return;
    PX.Objects.AR.ARTran tran1 = originalRetainageLine;
    Decimal? nullable = tran.CuryOrigTranAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = document.SignAmount;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num = valueOrDefault1 * valueOrDefault2;
    this.DecreaseRetainedAmount(tran1, num);
  }

  public virtual void RestoreAmountToInvoice(
    string docCuryID,
    PX.Objects.AR.ARTran line,
    int? revenueAccountGroup)
  {
    if (!line.TaskID.HasValue || !revenueAccountGroup.HasValue)
      return;
    PX.Objects.PM.PMProject project = PX.Objects.PM.PMProject.PK.Find((PXGraph) this.Base, line.ProjectID);
    if (project == null || project.NonProject.GetValueOrDefault())
      return;
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this.Base).SelectProjectBalance(PMAccountGroup.PK.Find((PXGraph) this.Base, revenueAccountGroup), project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
    pmBudgetAccum1.Type = pmBudget.Type;
    pmBudgetAccum1.ProjectID = pmBudget.ProjectID;
    pmBudgetAccum1.ProjectTaskID = pmBudget.TaskID;
    pmBudgetAccum1.AccountGroupID = pmBudget.AccountGroupID;
    pmBudgetAccum1.InventoryID = pmBudget.InventoryID;
    pmBudgetAccum1.CostCodeID = pmBudget.CostCodeID;
    pmBudgetAccum1.UOM = pmBudget.UOM;
    pmBudgetAccum1.Description = pmBudget.Description;
    pmBudgetAccum1.CuryInfoID = project.CuryInfoID;
    PMBudgetAccum pmBudgetAccum2 = (PMBudgetAccum) ((PXGraph) this.Base).Caches[typeof (PMBudgetAccum)].Insert((object) pmBudgetAccum1);
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this.Base, line.TranType, line.RefNbr);
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum2;
    Decimal? nullable = pmBudgetAccum3.CuryAmountToInvoice;
    Decimal num1 = this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this.Base, project, arInvoice.CuryID, arInvoice.DocDate, line.CuryTranAmt) + this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this.Base, project, arInvoice.CuryID, arInvoice.DocDate, line.CuryRetainageAmt);
    pmBudgetAccum3.CuryAmountToInvoice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num1) : new Decimal?();
    if (!(pmBudgetAccum2.ProgressBillingBase == "Q"))
      return;
    ARReleaseProcess graph = this.Base;
    string uom1 = line.UOM;
    string uom2 = pmBudgetAccum2.UOM;
    nullable = line.Qty;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    Decimal num2;
    ref Decimal local = ref num2;
    INUnitAttribute.TryConvertGlobalUnits((PXGraph) graph, uom1, uom2, valueOrDefault, INPrecision.QUANTITY, out local);
    PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum2;
    nullable = pmBudgetAccum4.QtyToInvoice;
    Decimal num3 = num2;
    pmBudgetAccum4.QtyToInvoice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
  }

  /// <summary>
  /// Moves Project's Retained Amount out of Draft Retained Amount bucket into Retained Amount
  /// </summary>
  protected virtual void AdjustProjectBudgetRetainage(PX.Objects.AR.ARRegister doc, PX.Objects.AR.ARTran tran)
  {
    if (!tran.TaskID.HasValue)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) tran.AccountID
    }));
    if (account == null || !account.AccountGroupID.HasValue)
      return;
    this.AddRetainedAmount(tran, account.AccountGroupID);
    this.SubtractDraftRetainedAmount(tran, account.AccountGroupID);
  }

  protected virtual void DecreaseRetainedAmount(PX.Objects.AR.ARTran tran, Decimal value)
  {
    if (!tran.TaskID.HasValue || !(value != 0M))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) tran.AccountID
    }));
    if (account == null || !account.AccountGroupID.HasValue)
      return;
    this.AddRetainedAmount(tran, account.AccountGroupID, value * -1M);
  }

  protected virtual void AddRetainedAmount(PX.Objects.AR.ARTran tran, int? accountGroupID, int mult = 1)
  {
    Decimal num = (Decimal) mult * tran.CuryRetainageAmt.GetValueOrDefault() * (ARDocType.SignAmount(tran.TranType) ?? 1M);
    this.AddRetainedAmount(tran, accountGroupID, num);
  }

  protected virtual void AddRetainedAmount(PX.Objects.AR.ARTran tran, int? accountGroupID, Decimal value)
  {
    if (!(value != 0M))
      return;
    PMBudgetAccum pmBudgetAccum1 = (PMBudgetAccum) ((PXGraph) this.Base).Caches[typeof (PMBudgetAccum)].Insert((object) this.GetTargetBudget(accountGroupID, tran));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? curyRetainedAmount = pmBudgetAccum2.CuryRetainedAmount;
    Decimal num1 = value;
    pmBudgetAccum2.CuryRetainedAmount = curyRetainedAmount.HasValue ? new Decimal?(curyRetainedAmount.GetValueOrDefault() + num1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    Decimal? retainedAmount = pmBudgetAccum3.RetainedAmount;
    Decimal num2 = value;
    pmBudgetAccum3.RetainedAmount = retainedAmount.HasValue ? new Decimal?(retainedAmount.GetValueOrDefault() + num2) : new Decimal?();
  }

  protected virtual void SubtractDraftRetainedAmount(PX.Objects.AR.ARTran tran, int? accountGroupID)
  {
    Decimal? nullable = tran.CuryRetainageAmt;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    nullable = ARDocType.SignAmount(tran.TranType);
    Decimal num1 = nullable ?? 1M;
    Decimal num2 = valueOrDefault * num1;
    PMBudgetAccum pmBudgetAccum1 = (PMBudgetAccum) ((PXGraph) this.Base).Caches[typeof (PMBudgetAccum)].Insert((object) this.GetTargetBudget(accountGroupID, tran));
    PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
    Decimal? draftRetainedAmount1 = pmBudgetAccum2.CuryDraftRetainedAmount;
    Decimal num3 = num2;
    pmBudgetAccum2.CuryDraftRetainedAmount = draftRetainedAmount1.HasValue ? new Decimal?(draftRetainedAmount1.GetValueOrDefault() - num3) : new Decimal?();
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
    Decimal? draftRetainedAmount2 = pmBudgetAccum3.DraftRetainedAmount;
    Decimal num4 = num2;
    pmBudgetAccum3.DraftRetainedAmount = draftRetainedAmount2.HasValue ? new Decimal?(draftRetainedAmount2.GetValueOrDefault() - num4) : new Decimal?();
  }

  private PMBudgetAccum GetTargetBudget(int? accountGroupID, PX.Objects.AR.ARTran line)
  {
    PMAccountGroup ag = PMAccountGroup.PK.Find((PXGraph) this.Base, accountGroupID);
    PX.Objects.PM.PMProject project = PX.Objects.PM.PMProject.PK.Find((PXGraph) this.Base, line.ProjectID);
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this.Base).SelectProjectBalance(ag, project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    PMBudgetAccum targetBudget = new PMBudgetAccum();
    targetBudget.Type = pmBudget.Type;
    targetBudget.ProjectID = pmBudget.ProjectID;
    targetBudget.ProjectTaskID = pmBudget.TaskID;
    targetBudget.AccountGroupID = pmBudget.AccountGroupID;
    targetBudget.InventoryID = pmBudget.InventoryID;
    targetBudget.CostCodeID = pmBudget.CostCodeID;
    targetBudget.UOM = pmBudget.UOM;
    targetBudget.Description = pmBudget.Description;
    targetBudget.CuryInfoID = project.CuryInfoID;
    return targetBudget;
  }
}
