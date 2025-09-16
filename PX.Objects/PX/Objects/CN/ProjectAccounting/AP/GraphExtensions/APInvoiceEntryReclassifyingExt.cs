// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.GraphExtensions.APInvoiceEntryReclassifyingExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.CN.Subcontracts.AP.CacheExtensions;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.APReleaseProcessExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;

public class APInvoiceEntryReclassifyingExt : PXGraphExtension<APInvoiceEntry>
{
  public PXSelect<PMTran, Where<PMTran.origModule, Equal<BatchModule.moduleAP>, And<PMTran.origTranType, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<PMTran.origRefNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>, And<PMTran.billed, NotEqual<True>, And<PMTran.proformaRefNbr, IsNotNull>>>>>> LinkedProgressiveTransactions;
  public PXAction<PX.Objects.AP.APInvoice> reclassify;

  public static bool IsActive() => true;

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.lineNbr> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line", Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POLine.lineNbr, Where<PX.Objects.PO.POLine.orderType, Equal<Current<PX.Objects.AP.APTran.pOOrderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Current<PX.Objects.AP.APTran.pONbr>>>>>), new Type[] {typeof (PX.Objects.PO.POLine.lineNbr), typeof (PX.Objects.PO.POLine.projectID), typeof (PX.Objects.PO.POLine.taskID), typeof (PX.Objects.PO.POLine.costCodeID), typeof (PX.Objects.PO.POLine.inventoryID), typeof (PX.Objects.PO.POLine.lineType), typeof (PX.Objects.PO.POLine.tranDesc), typeof (PX.Objects.PO.POLine.uOM), typeof (PX.Objects.PO.POLine.orderQty), typeof (PX.Objects.PO.POLine.curyUnitCost), typeof (PX.Objects.PO.POLine.curyExtCost)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APTran.pOLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [APOpenPeriod(typeof (PX.Objects.AP.APRegister.docDate), typeof (PX.Objects.AP.APRegister.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (PX.Objects.AP.APRegister.tranPeriodID), IsHeader = true, ValidatePeriod = PeriodValidation.DefaultUpdate)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APInvoice.finPeriodID> e)
  {
  }

  [PXOverride]
  public virtual IEnumerable Release(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseHandler)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    APInvoiceEntryReclassifyingExt.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new APInvoiceEntryReclassifyingExt.\u003C\u003Ec__DisplayClass5_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.\u003C\u003E4__this = this;
    if (!(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.Status == "X"))
      return baseHandler(adapter);
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.list = adapter.Get<PX.Objects.AP.APInvoice>().ToList<PX.Objects.AP.APInvoice>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass50, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass50.list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual void Reclassify()
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.IsMigratedRecord.GetValueOrDefault())
      throw new PXException("The bill cannot be reclassified because it has been created in migration mode.");
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.OrigDocType == "ECL")
      throw new PXException("The bill cannot be reclassified because it is linked to an expense claim.");
    if (((IQueryable<PXResult<PMTran>>) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.batchNbr, Equal<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PMTran.origModule, IBqlString>.IsEqual<BatchModule.moduleAP>>>>.And<BqlOperand<PMTran.billed, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Count<PXResult<PMTran>>() > 0)
      throw new PXException("The bill cannot be reclassified because the related project transaction has been billed.");
    if (((IQueryable<PXResult<PMTran>>) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.batchNbr, Equal<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PMTran.origModule, IBqlString>.IsEqual<BatchModule.moduleAP>>>>.And<BqlOperand<PMTran.allocated, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Count<PXResult<PMTran>>() > 0)
      throw new PXException("The bill cannot be reclassified because the related project transaction has been allocated.");
    if (((IQueryable<PXResult<PX.Objects.GL.GLTran>>) PXSelectBase<PX.Objects.GL.GLTran, PXViewOf<PX.Objects.GL.GLTran>.BasedOn<SelectFromBase<PX.Objects.GL.GLTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.GLTran.batchNbr, Equal<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.GL.GLTran.module, IBqlString>.IsEqual<BatchModule.moduleAP>>>>.And<BqlOperand<PX.Objects.GL.GLTran.refNbr, IBqlString>.IsNotNull>>.Aggregate<To<GroupBy<PX.Objects.GL.GLTran.refNbr>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Count<PXResult<PX.Objects.GL.GLTran>>() > 1)
      throw new PXException("The bill cannot be reclassified because the general ledger transaction created on release of the bill was included to the {0} consolidated batch.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.BatchNbr
      });
    if (((IQueryable<PXResult<PX.Objects.AP.APInvoice>>) PXSelectBase<PX.Objects.AP.APInvoice, PXViewOf<PX.Objects.AP.APInvoice>.BasedOn<SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APInvoice.origDocType, Equal<BqlField<PX.Objects.AP.APInvoice.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AP.APInvoice.origRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AP.APInvoice.refNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AP.APInvoice.isRetainageDocument, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Count<PXResult<PX.Objects.AP.APInvoice>>() > 0)
      throw new PXException("The bill cannot be reclassified because it includes the released retainage.");
    if (((IQueryable<PXResult<PX.Objects.GL.GLTran>>) PXSelectBase<PX.Objects.GL.GLTran, PXViewOf<PX.Objects.GL.GLTran>.BasedOn<SelectFromBase<PX.Objects.GL.GLTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.GLTran.batchNbr, Equal<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.GL.GLTran.module, IBqlString>.IsEqual<BatchModule.moduleAP>>>>.And<BqlOperand<PX.Objects.GL.GLTran.reclassBatchNbr, IBqlString>.IsNotNull>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Count<PXResult<PX.Objects.GL.GLTran>>() > 0)
      throw new PXException("The bill cannot be reclassified because the corresponding GL transaction has been reclassified.");
    if (((IQueryable<PXResult<PX.Objects.GL.GLTran>>) PXSelectBase<PX.Objects.GL.GLTran, PXViewOf<PX.Objects.GL.GLTran>.BasedOn<SelectFromBase<PX.Objects.GL.GLTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.GLTran.batchNbr, Equal<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.GL.GLTran.module, IBqlString>.IsEqual<BatchModule.moduleAP>>>>.And<BqlOperand<PX.Objects.GL.GLTran.summPost, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Count<PXResult<PX.Objects.GL.GLTran>>() == 0)
      throw new PXException("The bill cannot be reclassified because the corresponding GL transaction has been generated with the Post Summary on Updating GL setting selected on the Accounts Payable Preferences (AP101000) form.");
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.TermsID != null && ((PX.Objects.CS.Terms) PXSelectorAttribute.Select<PX.Objects.AP.APInvoice.termsID>(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current)).InstallmentType == "M")
      throw new PXException("The bill line cannot be reclassified because the multiple installment credit terms are specified in the bill.");
    foreach (PXResult<PX.Objects.AP.APTran> pxResult in ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AP.APTran apTran = PXResult<PX.Objects.AP.APTran>.op_Implicit(pxResult);
      ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<APTranExt>((object) apTran).PrevPOLineNbr = apTran.POLineNbr;
      ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Update(apTran);
    }
    try
    {
      APOpenPeriodAttribute openPeriodAttribute = ((PXSelectBase) this.Base.Document).Cache.GetAttributesReadonly<PX.Objects.AP.APInvoice.finPeriodID>().OfType<APOpenPeriodAttribute>().FirstOrDefault<APOpenPeriodAttribute>();
      PeriodValidation validatePeriod = openPeriodAttribute.ValidatePeriod;
      openPeriodAttribute.ValidatePeriod = PeriodValidation.DefaultSelectUpdate;
      openPeriodAttribute?.IsValidPeriod(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.FinPeriodID);
      openPeriodAttribute.ValidatePeriod = validatePeriod;
    }
    catch (PXSetPropertyException ex)
    {
      ((PXCache) GraphHelper.Caches<PX.Objects.AP.APInvoice>((PXGraph) this.Base)).RaiseExceptionHandling<PX.Objects.AP.APInvoice.finPeriodID>((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) 0M, (Exception) ex);
    }
  }

  public virtual void ReleaseDoc(List<PX.Objects.AP.APInvoice> list)
  {
    APInvoiceEntry instance1 = PXGraph.CreateInstance<APInvoiceEntry>();
    APReleaseProcess instance2 = PXGraph.CreateInstance<APReleaseProcess>();
    PostGraph instance3 = PXGraph.CreateInstance<PostGraph>();
    JournalEntry instance4 = PXGraph.CreateInstance<JournalEntry>();
    instance4.Mode |= JournalEntry.Modes.InvoiceReclassification;
    foreach (PX.Objects.AP.APInvoice apDoc in list)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance1.Document).Current = apDoc;
        Batch batch = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXViewOf<Batch>.BasedOn<SelectFromBase<Batch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Batch.batchNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) instance1, Array.Empty<object>()));
        if (this.MarkChangedLines(instance1))
        {
          this.ReverseOrigBatch(instance4, instance3, instance2, batch);
          ((PXGraph) instance4).Clear();
          ((PXGraph) instance3).Clear();
          this.CreateNewBatch(instance4, instance3, instance1, instance2, apDoc, batch);
          ((PXGraph) instance4).Clear();
          ((PXGraph) instance3).Clear();
        }
        this.UpdatePOLines(instance1, instance2, apDoc);
        this.UnlinkReclassified(instance1);
        ((PXGraph) instance2).Persist();
        ((PXGraph) instance2).Clear();
        ((SelectedEntityEvent<PX.Objects.AP.APInvoice>) PXEntityEventBase<PX.Objects.AP.APInvoice>.Container<PX.Objects.AP.APInvoice.Events>.Select((Expression<Func<PX.Objects.AP.APInvoice.Events, PXEntityEvent<PX.Objects.AP.APInvoice.Events>>>) (e => e.ReleaseDocument))).FireOn((PXGraph) instance1, apDoc);
        ((PXGraph) instance1).Persist();
        ((PXGraph) instance1).Clear();
        transactionScope.Complete();
      }
    }
  }

  private bool MarkChangedLines(APInvoiceEntry graph)
  {
    bool flag1 = false;
    foreach (PXResult<PX.Objects.GL.GLTran> pxResult in PXSelectBase<PX.Objects.GL.GLTran, PXViewOf<PX.Objects.GL.GLTran>.BasedOn<SelectFromBase<PX.Objects.GL.GLTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.APTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.lineNbr, Equal<PX.Objects.GL.GLTran.tranLineNbr>>>>, And<BqlOperand<PX.Objects.AP.APTran.tranType, IBqlString>.IsEqual<BqlField<PX.Objects.AP.APInvoice.docType, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AP.APTran.refNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AP.APInvoice.refNbr, IBqlString>.FromCurrent>>>>>.Where<BqlOperand<PX.Objects.GL.GLTran.batchNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AP.APInvoice.batchNbr, IBqlString>.FromCurrent>>.Order<By<BqlField<PX.Objects.GL.GLTran.lineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) graph, Array.Empty<object>()))
    {
      PX.Objects.AP.APTran apTran = PXResult.Unwrap<PX.Objects.AP.APTran>((object) pxResult);
      PX.Objects.GL.GLTran glTran = PXResult.Unwrap<PX.Objects.GL.GLTran>((object) pxResult);
      APTranExt extension = ((PXSelectBase) graph.Transactions).Cache.GetExtension<APTranExt>((object) apTran);
      if (apTran.RefNbr != null)
      {
        int? nullable1 = apTran.CostCodeID;
        int? nullable2 = glTran.CostCodeID;
        int num1;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          nullable2 = apTran.ProjectID;
          nullable1 = ProjectDefaultAttribute.NonProject();
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = apTran.CostCodeID;
            if (!nullable1.HasValue)
            {
              nullable1 = glTran.ProjectID;
              nullable2 = ProjectDefaultAttribute.NonProject();
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = glTran.CostCodeID;
                nullable1 = CostCodeAttribute.DefaultCostCode;
                num1 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) ? 1 : 0;
                goto label_10;
              }
            }
          }
          num1 = 1;
        }
        else
          num1 = 0;
label_10:
        bool flag2 = num1 != 0;
        nullable1 = apTran.ProjectID;
        nullable2 = glTran.ProjectID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          ((PXSelectBase) graph.Transactions).Cache.SetValue<APTranExt.projectReclassified>((object) apTran, (object) true);
        nullable2 = apTran.ProjectID;
        nullable1 = glTran.ProjectID;
        int num2;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = apTran.TaskID;
          nullable2 = glTran.TaskID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = apTran.AccountID;
            nullable1 = glTran.AccountID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = apTran.SubID;
              nullable2 = glTran.SubID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = extension.PrevPOLineNbr;
                nullable1 = apTran.POLineNbr;
                num2 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) ? 1 : 0;
                goto label_18;
              }
            }
          }
        }
        num2 = 1;
label_18:
        int num3 = flag2 ? 1 : 0;
        if ((num2 | num3) != 0)
        {
          extension.Reclassified = new bool?(true);
          ((PXSelectBase<PX.Objects.AP.APTran>) graph.Transactions).Update(apTran);
          flag1 = true;
        }
      }
    }
    return flag1;
  }

  private void ReverseOrigBatch(
    JournalEntry je,
    PostGraph pg,
    APReleaseProcess release,
    Batch batch)
  {
    je.ReverseBatchProc(batch);
    ((PXSelectBase<Batch>) je.BatchModule).SetValueExt<Batch.module>(((PXSelectBase<Batch>) je.BatchModule).Current, (object) batch.Module);
    PXCache cach = ((PXGraph) je).Caches[typeof (PX.Objects.GL.GLTran)];
    foreach (object obj in cach.Inserted)
      cach.SetValueExt<PX.Objects.GL.GLTran.module>(obj, (object) batch.Module);
    if (((PXSelectBase<Batch>) je.BatchModule).Current.Status == "H")
    {
      ((PXSelectBase<Batch>) je.BatchModule).Current.Approved = new bool?(true);
      ((PXAction) je.releaseFromHold).Press();
    }
    ((PXAction) je.Save).Press();
    pg.ReleaseBatchProc(((PXSelectBase<Batch>) je.BatchModule).Current, true);
    if (!release.AutoPost)
      return;
    pg.PostBatchProc(((PXSelectBase<Batch>) je.BatchModule).Current);
  }

  private void CreateNewBatch(
    JournalEntry je,
    PostGraph pg,
    APInvoiceEntry graph,
    APReleaseProcess release,
    PX.Objects.AP.APInvoice apDoc,
    Batch batch)
  {
    release.IsInvoiceReclassification = true;
    apDoc.Released = new bool?(false);
    foreach (PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AP.Vendor> res in ((PXSelectBase<PX.Objects.AP.APInvoice>) release.APInvoice_DocType_RefNbr).Select(new object[2]
    {
      (object) apDoc.DocType,
      (object) apDoc.RefNbr
    }))
    {
      JournalEntry.SegregateBatch(je, "AP", apDoc.BranchID, apDoc.CuryID, apDoc.DocDate, apDoc.FinPeriodID, apDoc.DocDesc, PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AP.Vendor>.op_Implicit(res).GetCM(), (Batch) null);
      PX.Objects.AP.APRegister doc = (PX.Objects.AP.APRegister) apDoc;
      release.ReleaseInvoice(je, ref doc, res, false, out List<PX.Objects.IN.INRegister> _);
      ((PXSelectBase<Batch>) je.BatchModule).Current.OrigBatchNbr = batch.BatchNbr;
      ((PXSelectBase<Batch>) je.BatchModule).Current.OrigModule = batch.Module;
      ((PXAction) je.Save).Press();
    }
    apDoc.Released = new bool?(true);
    apDoc.BatchNbr = ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr;
    ((PXSelectBase) graph.Document).Cache.GetExtension<APInvoiceExt>((object) apDoc).Reclassified = new bool?(true);
    if (!release.AutoPost)
      return;
    pg.PostBatchProc(((PXSelectBase<Batch>) je.BatchModule).Current);
  }

  private void UpdatePOLines(APInvoiceEntry graph, APReleaseProcess release, PX.Objects.AP.APInvoice apDoc)
  {
    foreach (PXResult<PX.Objects.AP.APTran> pxResult in ((PXSelectBase<PX.Objects.AP.APTran>) graph.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AP.APTran apTran1 = PXResult<PX.Objects.AP.APTran>.op_Implicit(pxResult);
      APTranExt extension = ((PXSelectBase) graph.Transactions).Cache.GetExtension<APTranExt>((object) apTran1);
      int? prevPoLineNbr = extension.PrevPOLineNbr;
      int? nullable1 = apTran1.POLineNbr;
      if (!(prevPoLineNbr.GetValueOrDefault() == nullable1.GetValueOrDefault() & prevPoLineNbr.HasValue == nullable1.HasValue))
      {
        PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(((IQueryable<PXResult<PX.Objects.PO.POReceiptLine>>) PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.pOType, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.pOLineNbr, Equal<P.AsInt>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) graph, new object[4]
        {
          (object) apTran1.POOrderType,
          (object) apTran1.PONbr,
          (object) apTran1.POLineNbr,
          (object) extension.PrevPOLineNbr
        })).FirstOrDefault<PXResult<PX.Objects.PO.POReceiptLine>>());
        if (poReceiptLine != null)
          throw new PXException("The bill cannot be released because the purchase receipt {0} is linked to the purchase order selected in the {1} bill line.", new object[2]
          {
            (object) poReceiptLine.ReceiptNbr,
            (object) apTran1.LineNbr
          });
        release.IsInvoiceReclassification = false;
        apTran1.POAccrualLineNbr = apTran1.POLineNbr;
        APReleaseProcess release1 = release;
        PX.Objects.AP.APInvoice apDoc1 = apDoc;
        PX.Objects.AP.APTran apTran2 = apTran1;
        nullable1 = apTran1.POLineNbr;
        int poLineNbr1 = nullable1.Value;
        this.UpdatePOLine(release1, apDoc1, apTran2, poLineNbr1, false);
        PX.Objects.AP.APTran copy = (PX.Objects.AP.APTran) ((PXSelectBase) graph.Transactions).Cache.CreateCopy((object) apTran1);
        PX.Objects.AP.APTran apTran3 = copy;
        nullable1 = extension.PrevPOLineNbr;
        int? nullable2 = new int?(nullable1.Value);
        apTran3.POAccrualLineNbr = nullable2;
        PX.Objects.AP.APTran apTran4 = copy;
        Decimal? nullable3 = apTran4.CuryLineAmt;
        Decimal num1 = (Decimal) -1;
        apTran4.CuryLineAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num1) : new Decimal?();
        PX.Objects.AP.APTran apTran5 = copy;
        nullable3 = apTran5.LineAmt;
        Decimal num2 = (Decimal) -1;
        apTran5.LineAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num2) : new Decimal?();
        PX.Objects.AP.APTran apTran6 = copy;
        nullable3 = apTran6.CuryTranAmt;
        Decimal num3 = (Decimal) -1;
        apTran6.CuryTranAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num3) : new Decimal?();
        PX.Objects.AP.APTran apTran7 = copy;
        nullable3 = apTran7.TranAmt;
        Decimal num4 = (Decimal) -1;
        apTran7.TranAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num4) : new Decimal?();
        PX.Objects.AP.APTran apTran8 = copy;
        nullable3 = apTran8.CuryRetainageAmt;
        Decimal num5 = (Decimal) -1;
        apTran8.CuryRetainageAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num5) : new Decimal?();
        PX.Objects.AP.APTran apTran9 = copy;
        nullable3 = apTran9.RetainageAmt;
        Decimal num6 = (Decimal) -1;
        apTran9.RetainageAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num6) : new Decimal?();
        PX.Objects.AP.APTran apTran10 = copy;
        nullable3 = apTran10.Qty;
        Decimal num7 = (Decimal) -1;
        apTran10.Qty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num7) : new Decimal?();
        PX.Objects.AP.APTran apTran11 = copy;
        nullable3 = apTran11.BaseQty;
        Decimal num8 = (Decimal) -1;
        apTran11.BaseQty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num8) : new Decimal?();
        APReleaseProcess release2 = release;
        PX.Objects.AP.APInvoice apDoc2 = apDoc;
        PX.Objects.AP.APTran apTran12 = copy;
        nullable1 = extension.PrevPOLineNbr;
        int poLineNbr2 = nullable1.Value;
        this.UpdatePOLine(release2, apDoc2, apTran12, poLineNbr2, true);
        APReleaseProcess release3 = release;
        PX.Objects.AP.APInvoice apDoc3 = apDoc;
        PX.Objects.AP.APTran apTran13 = copy;
        nullable1 = extension.PrevPOLineNbr;
        int poLineNbr3 = nullable1.Value;
        this.DeletePOLineRevision(release3, apDoc3, apTran13, poLineNbr3);
      }
    }
  }

  private void UpdatePOLine(
    APReleaseProcess release,
    PX.Objects.AP.APInvoice apDoc,
    PX.Objects.AP.APTran apTran,
    int poLineNbr,
    bool open)
  {
    POAccrualStatus origRow = PXResultset<POAccrualStatus>.op_Implicit(PXSelectBase<POAccrualStatus, PXViewOf<POAccrualStatus>.BasedOn<SelectFromBase<POAccrualStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.refNoteID, Equal<P.AsGuid>>>>>.And<BqlOperand<POAccrualStatus.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) release, new object[2]
    {
      (object) apTran.POAccrualRefNoteID,
      (object) apTran.POAccrualLineNbr
    }));
    UpdatePOOnRelease implementation = ((PXGraph) release).FindImplementation<UpdatePOOnRelease>();
    PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder> pxResult = (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>) PXResultset<PX.Objects.PO.POLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POLine>) implementation.poOrderLineUPD).Select(new object[3]
    {
      (object) apTran.POOrderType,
      (object) apTran.PONbr,
      (object) poLineNbr
    }));
    implementation.UpdatePOAccrualStatus(origRow, apTran, apDoc, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), (PX.Objects.PO.POReceiptLine) null);
    if (pxResult == null)
      return;
    PX.Objects.PO.POLine updLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
    PX.Objects.PO.POOrder poOrder = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
    if (open)
    {
      updLine.Completed = new bool?(false);
      updLine.Closed = new bool?(false);
      ((PXSelectBase<PX.Objects.PO.POOrder>) implementation.poOrderUPD).Update(poOrder);
    }
    implementation.UpdatePOLine(apTran, apDoc, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), updLine, false);
  }

  private void DeletePOLineRevision(
    APReleaseProcess release,
    PX.Objects.AP.APInvoice apDoc,
    PX.Objects.AP.APTran apTran,
    int poLineNbr)
  {
    PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder> pxResult = (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>) PXResultset<PX.Objects.PO.POLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POLine>) ((PXGraph) release).FindImplementation<UpdatePOOnRelease>().poOrderLineUPD).Select(new object[3]
    {
      (object) apTran.POOrderType,
      (object) apTran.PONbr,
      (object) poLineNbr
    }));
    if (pxResult == null)
      return;
    PX.Objects.PO.POLine srcLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
    ((PXGraph) release).GetExtension<APInvoicePOValidation>()?.DeletePOLineRevision(apDoc, srcLine, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult));
  }

  private void UnlinkReclassified(APInvoiceEntry graph)
  {
    foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.LinkedProgressiveTransactions).Select(Array.Empty<object>()))
    {
      PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
      bool flag = false;
      if (pmTran.ProformaRefNbr != null)
      {
        PX.Objects.AP.APTran apTran = ((PXSelectBase<PX.Objects.AP.APTran>) graph.Transactions).Locate(new PX.Objects.AP.APTran()
        {
          TranType = pmTran.OrigTranType,
          RefNbr = pmTran.OrigRefNbr,
          LineNbr = pmTran.OrigLineNbr
        });
        if (apTran != null)
        {
          APTranExt extension = ((PXSelectBase) graph.Transactions).Cache.GetExtension<APTranExt>((object) apTran);
          if (extension != null && extension.ProjectReclassified.GetValueOrDefault())
            flag = true;
        }
      }
      if (flag)
      {
        pmTran.ProformaRefNbr = (string) null;
        pmTran.ProformaLineNbr = new int?();
        ((PXGraph) graph).Caches[typeof (PMTran)].Update((object) pmTran);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = !string.IsNullOrEmpty(PredefinedRoles.ProjectAccountant) && PXContext.PXIdentity.User.IsInRole(PredefinedRoles.ProjectAccountant);
    bool flag2 = !string.IsNullOrEmpty(PredefinedRoles.FinancialSupervisor) && PXContext.PXIdentity.User.IsInRole(PredefinedRoles.FinancialSupervisor);
    if (!flag1 && !flag2)
      ((PXAction) this.reclassify).SetEnabled(false);
    if (!(e.Row.Status == "X"))
      return;
    if (!flag1 && !flag2)
      ((PXAction) this.Base.release).SetEnabled(false);
    ((PXSelectBase) this.Base.Transactions).Cache.AllowUpdate = true;
    APInvoiceState documentState = this.Base.GetDocumentState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice>>) e).Cache, e.Row);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice>>) e).Cache, (object) e.Row, !documentState.HasPOLink && !documentState.IsFromExpenseClaims);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APTran> e)
  {
    if (e.Row != null && ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document)?.Current?.Status == "X")
    {
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.branchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.inventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.tranDesc>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.qty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyUnitCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyLineAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyDiscAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.nonBillable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.taxCategoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.box1099>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.discountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.discPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.manualPrice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.manualDisc>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyRetainageAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.retainagePct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, true);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.accountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, true);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.subID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, true);
      if (e.Row.PONbr != null || e.Row.DeferredCode != null)
      {
        PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
        PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
        PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
        PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.accountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
        PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.subID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      }
      APTranExt extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<APTranExt>((object) e.Row);
      bool flag = true;
      if (extension.PrevPOLineNbr.HasValue)
        flag = this.SelectPOLine(e.Row.POOrderType, e.Row.PONbr, extension.PrevPOLineNbr).LineType == "SV";
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.pOLineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, ((e.Row.PONbr == null ? 0 : (e.Row.POOrderType != "RS" ? 1 : 0)) & (flag ? 1 : 0)) != 0 && e.Row.DeferredCode == null && e.Row.ReceiptNbr == null);
      PXUIFieldAttribute.SetEnabled<ApTranExt.subcontractLineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, e.Row.PONbr != null && e.Row.POOrderType == "RS" && e.Row.DeferredCode == null);
      if (e.Row.DeferredCode == null)
        return;
      PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.deferredCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, "The bill line cannot be reclassified because the deferral code is specified in this line.");
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.pOLineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<ApTranExt.subcontractLineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, false);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APTran> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current == null || !(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.Status == "X") || e.Row.PONbr == null)
      return;
    int? nullable = e.Row.POLineNbr;
    if (!nullable.HasValue)
    {
      if (e.Row.POOrderType == "RS")
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<ApTranExt.subcontractLineNbr>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
      else
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.pOLineNbr>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    }
    else
    {
      APTranExt extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<APTranExt>((object) e.Row);
      nullable = e.Row.POLineNbr;
      int? prevPoLineNbr = extension.PrevPOLineNbr;
      if (nullable.GetValueOrDefault() == prevPoLineNbr.GetValueOrDefault() & nullable.HasValue == prevPoLineNbr.HasValue)
        return;
      PX.Objects.PO.POLine poLine = this.SelectPOLine(e.Row.POOrderType, e.Row.PONbr, extension.PrevPOLineNbr);
      int? inventoryId = this.SelectPOLine(e.Row.POOrderType, e.Row.PONbr, e.Row.POLineNbr).InventoryID;
      nullable = poLine.InventoryID;
      if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
        return;
      nullable = poLine.InventoryID;
      PXSetPropertyException propertyException;
      if (!nullable.HasValue)
        propertyException = new PXSetPropertyException("The inventory item cannot be changed on reclassification. Select a commitment line with empty inventory item.");
      else
        propertyException = new PXSetPropertyException("The inventory item cannot be changed on reclassification. Select a commitment line with the {0} inventory item.", new object[1]
        {
          (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, poLine.InventoryID).InventoryCD
        });
      if (e.Row.POOrderType == "RS")
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<ApTranExt.subcontractLineNbr>((object) e.Row, (object) null, (Exception) propertyException);
      else
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.pOLineNbr>((object) e.Row, (object) null, (Exception) propertyException);
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOLineNbr> e)
  {
    if (e.NewValue == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOLineNbr>, PX.Objects.AP.APTran, object>) e).OldValue == null)
      return;
    if (this.SelectPOLine(e.Row.POOrderType, e.Row.PONbr, e.Row.POLineNbr).LineType != "SV")
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOLineNbr>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.pOLineNbr>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("The line cannot be adjusted because it is linked to a commitment line with the line type different from the Service type."));
    this.UpdateAPTran(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, ApTranExt.subcontractLineNbr> e)
  {
    if (e.NewValue == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, ApTranExt.subcontractLineNbr>, PX.Objects.AP.APTran, object>) e).OldValue == null)
      return;
    this.UpdateAPTran(e.Row);
  }

  private void UpdateAPTran(PX.Objects.AP.APTran row)
  {
    PX.Objects.PO.POLine poLine = this.SelectPOLine(row.POOrderType, row.PONbr, row.POLineNbr);
    if (poLine == null)
      return;
    row.ProjectID = poLine.ProjectID;
    row.TaskID = poLine.TaskID;
    row.CostCodeID = poLine.CostCodeID;
    row.AccountID = poLine.ExpenseAcctID;
    row.SubID = poLine.ExpenseSubID;
  }

  private PX.Objects.PO.POLine SelectPOLine(string orderType, string orderNbr, int? lineNbr)
  {
    return PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderNbr, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POLine.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) orderType,
      (object) orderNbr,
      (object) lineNbr
    }));
  }

  [PXOverride]
  public virtual void Persist(System.Action persist)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current != null && ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.Status == "X")
    {
      Dictionary<Tuple<string, string, int>, PX.Objects.AP.APTran> dictionary = new Dictionary<Tuple<string, string, int>, PX.Objects.AP.APTran>();
      foreach (PXResult<PX.Objects.AP.APTran> pxResult in ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Select(Array.Empty<object>()))
      {
        PX.Objects.AP.APTran apTran1 = PXResult<PX.Objects.AP.APTran>.op_Implicit(pxResult);
        if (apTran1.PONbr != null)
        {
          Tuple<string, string, int> key = new Tuple<string, string, int>(apTran1.POOrderType, apTran1.PONbr, apTran1.POLineNbr.Value);
          PX.Objects.AP.APTran apTran2;
          if (dictionary.TryGetValue(key, out apTran2))
          {
            int? prevPoLineNbr = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<APTranExt>((object) apTran1).PrevPOLineNbr;
            int? poLineNbr = apTran1.POLineNbr;
            PX.Objects.AP.APTran apTran3 = prevPoLineNbr.GetValueOrDefault() == poLineNbr.GetValueOrDefault() & prevPoLineNbr.HasValue == poLineNbr.HasValue ? apTran2 : apTran1;
            PXSetPropertyException propertyException = new PXSetPropertyException("The commitment line cannot be selected because it is already linked to the {0} bill line.", new object[1]
            {
              (object) apTran3.POLineNbr.Value
            });
            if (apTran3.POOrderType == "RS")
              ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<ApTranExt.subcontractLineNbr>((object) apTran3, (object) apTran3.POLineNbr, (Exception) propertyException);
            else
              ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.pOLineNbr>((object) apTran3, (object) apTran3.POLineNbr, (Exception) propertyException);
            throw new PXException();
          }
          dictionary.Add(key, apTran1);
        }
      }
    }
    persist();
  }
}
