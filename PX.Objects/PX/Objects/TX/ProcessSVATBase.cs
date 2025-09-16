// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ProcessSVATBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

[TableAndChartDashboardType]
public class ProcessSVATBase : PXGraph<ProcessSVATBase>
{
  public PXCancel<SVATTaxFilter> Cancel;
  public PXFilter<SVATTaxFilter> Filter;
  public PXAction<SVATTaxFilter> viewDocument;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingOrderBy<SVATConversionHistExt, SVATTaxFilter, OrderBy<Asc<SVATConversionHistExt.module, Asc<SVATConversionHistExt.adjdRefNbr, Asc<SVATConversionHistExt.adjdDocType, Asc<SVATConversionHistExt.adjdLineNbr, Asc<SVATConversionHistExt.adjgRefNbr, Asc<SVATConversionHistExt.adjgDocType>>>>>>>> SVATDocuments;
  private Dictionary<object, object> _copies = new Dictionary<object, object>();

  public virtual bool IsDirty => false;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public virtual void Clear()
  {
    ((PXSelectBase<SVATTaxFilter>) this.Filter).Current.TotalTaxAmount = new Decimal?(0M);
    ((PXGraph) this).Clear();
  }

  public ProcessSVATBase()
  {
    PXUIFieldAttribute.SetEnabled<SVATConversionHist.taxInvoiceDate>(((PXSelectBase) this.SVATDocuments).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SVATConversionHist.taxInvoiceNbr>(((PXSelectBase) this.SVATDocuments).Cache, (object) null, true);
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(((PXSelectBase<SVATConversionHistExt>) this.SVATDocuments).Current?.AdjdDocType) && !string.IsNullOrEmpty(((PXSelectBase<SVATConversionHistExt>) this.SVATDocuments).Current?.AdjdRefNbr))
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.SVATDocuments).Cache, (object) ((PXSelectBase<SVATConversionHistExt>) this.SVATDocuments).Current, "Document", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  public virtual IEnumerable sVATDocuments()
  {
    ProcessSVATBase processSvatBase = this;
    SVATTaxFilter current = ((PXSelectBase<SVATTaxFilter>) processSvatBase.Filter).Current;
    if (current != null)
    {
      PXSelectBase<SVATConversionHistExt> sel = (PXSelectBase<SVATConversionHistExt>) new PXSelect<SVATConversionHistExt, Where<SVATConversionHist.processed, NotEqual<True>>>((PXGraph) processSvatBase);
      if (current.ReversalMethod != null)
      {
        switch (current.ReversalMethod)
        {
          case "P":
            sel.WhereAnd<Where<SVATConversionHistExt.module, Equal<BatchModule.moduleCA>, Or<SVATConversionHistExt.adjdDocType, Equal<ARDocType.cashSale>, Or<SVATConversionHistExt.adjdDocType, Equal<ARDocType.cashReturn>, Or<SVATConversionHistExt.adjdDocType, Equal<APDocType.quickCheck>, Or<SVATConversionHistExt.adjdDocType, Equal<APDocType.voidQuickCheck>, Or<Where<SVATConversionHist.reversalMethod, Equal<SVATTaxReversalMethods.onPayments>, And<Where<SVATConversionHistExt.adjdDocType, NotEqual<SVATConversionHistExt.adjgDocType>, Or<SVATConversionHistExt.adjdRefNbr, NotEqual<SVATConversionHistExt.adjgRefNbr>>>>>>>>>>>>();
            break;
          case "D":
            sel.WhereAnd<Where<SVATConversionHist.reversalMethod, Equal<SVATTaxReversalMethods.onDocuments>, And<Where<SVATConversionHistExt.adjdDocType, Equal<SVATConversionHistExt.adjgDocType>, And<SVATConversionHistExt.adjdRefNbr, Equal<SVATConversionHistExt.adjgRefNbr>>>>>>();
            break;
        }
      }
      if (current.TaxAgencyID.HasValue)
        sel.WhereAnd<Where<SVATConversionHist.vendorID, Equal<Current<SVATTaxFilter.taxAgencyID>>>>();
      else
        sel.WhereAnd<Where<SVATConversionHist.vendorID, IsNull>>();
      if (current.Date.HasValue)
        sel.WhereAnd<Where<SVATConversionHist.adjdDocDate, LessEqual<Current<SVATTaxFilter.date>>>>();
      if (current.OrgBAccountID.HasValue)
        sel.WhereAnd<Where<SVATConversionHist.adjdBranchID, InsideBranchesOf<Current<SVATTaxFilter.orgBAccountID>>>>();
      else if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
        yield break;
      if (current.TaxID != null)
        sel.WhereAnd<Where<SVATConversionHistExt.taxID, Equal<Current<SVATTaxFilter.taxID>>>>();
      processSvatBase.FillSVATDocumentsQuery(sel);
      foreach (PXResult<SVATConversionHistExt> pxResult in sel.Select(Array.Empty<object>()))
      {
        SVATConversionHistExt hist = PXResult<SVATConversionHistExt>.op_Implicit(pxResult);
        yield return (object) hist;
        if (processSvatBase._copies.ContainsKey((object) hist))
          processSvatBase._copies.Remove((object) hist);
        processSvatBase._copies.Add((object) hist, (object) PXCache<SVATConversionHistExt>.CreateCopy(hist));
        hist = (SVATConversionHistExt) null;
      }
      ((PXSelectBase) processSvatBase.SVATDocuments).Cache.IsDirty = false;
    }
  }

  public virtual void FillSVATDocumentsQuery(PXSelectBase<SVATConversionHistExt> sel)
  {
  }

  protected virtual void SVATTaxFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProcessSVATBase.\u003C\u003Ec__DisplayClass20_0 cDisplayClass200 = new ProcessSVATBase.\u003C\u003Ec__DisplayClass20_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass200.filter = (SVATTaxFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass200.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<SVATConversionHistExt>) this.SVATDocuments).SetProcessDelegate(new PXProcessingBase<SVATConversionHistExt>.ProcessListDelegate((object) cDisplayClass200, __methodptr(\u003CSVATTaxFilter_RowSelected\u003Eb__0)));
  }

  protected virtual void SVATTaxFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<SVATTaxFilter>) this.Filter).Current.TotalTaxAmount = new Decimal?(0M);
    ((PXSelectBase) this.SVATDocuments).Cache.Clear();
    ((PXSelectBase) this.SVATDocuments).Cache.ClearQueryCacheObsolete();
  }

  protected virtual void SVATConversionHistExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SVATTaxFilter current = ((PXSelectBase<SVATTaxFilter>) this.Filter).Current;
    SVATConversionHistExt row = e.Row as SVATConversionHistExt;
    if (current == null || row == null)
      return;
    if (string.IsNullOrEmpty(row.TaxInvoiceNbr))
    {
      switch (row.DisplayTaxEntryRefNbr)
      {
        case "D":
          row.TaxInvoiceNbr = row.AdjdRefNbr;
          break;
        case "P":
          row.TaxInvoiceNbr = row.AdjgRefNbr;
          break;
      }
    }
    if (!row.TaxInvoiceDate.HasValue)
      row.TaxInvoiceDate = current.ReversalMethod == "D" ? current.Date : row.AdjdDocDate;
    PXUIFieldAttribute.SetEnabled<SVATConversionHist.taxInvoiceNbr>(sender, (object) null, row.DisplayTaxEntryRefNbr != "T");
  }

  protected virtual void SVATConversionHistExt_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SVATTaxFilter current = ((PXSelectBase<SVATTaxFilter>) this.Filter).Current;
    if (current == null)
      return;
    object oldRow = e.OldRow;
    if (e.Row == e.OldRow && !this._copies.TryGetValue(e.Row, out oldRow))
    {
      Decimal? nullable1 = new Decimal?(0M);
      foreach (PXResult<SVATConversionHistExt> pxResult in ((PXSelectBase<SVATConversionHistExt>) this.SVATDocuments).Select(Array.Empty<object>()))
      {
        SVATConversionHistExt conversionHistExt = PXResult<SVATConversionHistExt>.op_Implicit(pxResult);
        if (conversionHistExt.Selected.GetValueOrDefault())
        {
          Decimal? nullable2 = nullable1;
          Decimal? nullable3 = conversionHistExt.TaxAmt;
          Decimal valueOrDefault = nullable3.GetValueOrDefault();
          Decimal? nullable4;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable4 = nullable3;
          }
          else
            nullable4 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault);
          nullable1 = nullable4;
        }
      }
      current.TotalTaxAmount = nullable1;
    }
    else
    {
      SVATConversionHistExt conversionHistExt = oldRow as SVATConversionHistExt;
      SVATConversionHistExt row = e.Row as SVATConversionHistExt;
      SVATTaxFilter svatTaxFilter1 = current;
      Decimal? totalTaxAmount = svatTaxFilter1.TotalTaxAmount;
      bool? selected;
      Decimal? nullable5;
      if (conversionHistExt != null)
      {
        selected = conversionHistExt.Selected;
        if (selected.GetValueOrDefault())
        {
          nullable5 = conversionHistExt?.TaxAmt;
          goto label_19;
        }
      }
      nullable5 = new Decimal?(0M);
label_19:
      Decimal? nullable6 = nullable5;
      svatTaxFilter1.TotalTaxAmount = totalTaxAmount.HasValue & nullable6.HasValue ? new Decimal?(totalTaxAmount.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      SVATTaxFilter svatTaxFilter2 = current;
      nullable6 = svatTaxFilter2.TotalTaxAmount;
      Decimal? nullable7;
      if (row != null)
      {
        selected = row.Selected;
        if (selected.GetValueOrDefault())
        {
          nullable7 = row?.TaxAmt;
          goto label_23;
        }
      }
      nullable7 = new Decimal?(0M);
label_23:
      Decimal? nullable8 = nullable7;
      svatTaxFilter2.TotalTaxAmount = nullable6.HasValue & nullable8.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
    }
  }

  public static void ProcessPendingVATProc(List<SVATConversionHistExt> list, SVATTaxFilter filter)
  {
    ProcessSVATBase svat = PXGraph.CreateInstance<ProcessSVATBase>();
    JournalEntry instance1 = PXGraph.CreateInstance<JournalEntry>();
    instance1.Mode |= JournalEntry.Modes.RecognizingVAT;
    PXCache cach1 = ((PXGraph) instance1).Caches[typeof (TaxTran)];
    PXCache cach2 = ((PXGraph) instance1).Caches[typeof (SVATTaxTran)];
    ((PXGraph) instance1).Views.Caches.Add(typeof (SVATTaxTran));
    int num1 = 0;
    bool flag1 = false;
    Dictionary<AdjKey, string> dictionary1 = new Dictionary<AdjKey, string>();
    Dictionary<string, Tuple<Batch, int>> dictionary2 = new Dictionary<string, Tuple<Batch, int>>();
    foreach (SVATConversionHistExt conversionHistExt in list)
    {
      SVATConversionHistExt histSVAT = conversionHistExt;
      AdjKey key = new AdjKey()
      {
        Module = histSVAT.Module,
        AdjdDocType = histSVAT.AdjdDocType,
        AdjdRefNbr = histSVAT.AdjdRefNbr,
        AdjdLineNbr = histSVAT.AdjdLineNbr.GetValueOrDefault(),
        AdjgDocType = histSVAT.AdjgDocType,
        AdjgRefNbr = histSVAT.AdjgRefNbr,
        AdjNbr = histSVAT.AdjNbr.GetValueOrDefault()
      };
      try
      {
        ((PXGraph) instance1).Clear((PXClearOption) 3);
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXProcessing<SVATConversionHistExt>.SetCurrentItem((object) histSVAT);
          PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) svat, (object[]) null, new object[1]
          {
            (object) histSVAT.VendorID
          }));
          string str;
          if (dictionary1.TryGetValue(key, out str))
          {
            histSVAT.TaxInvoiceNbr = str;
          }
          else
          {
            if (histSVAT.TaxType == "A" && vendor?.SVATOutputTaxEntryRefNbr == "T")
              histSVAT.TaxInvoiceNbr = AutoNumberAttribute.GetNextNumber(((PXSelectBase) svat.SVATDocuments).Cache, (object) null, vendor.SVATTaxInvoiceNumberingID, ((PXGraph) svat).Accessinfo.BusinessDate);
            dictionary1.Add(key, histSVAT.TaxInvoiceNbr);
          }
          if (!string.IsNullOrEmpty(histSVAT.TaxInvoiceNbr))
          {
            DateTime? nullable1 = histSVAT.TaxInvoiceDate;
            if (nullable1.HasValue)
            {
              TaxTran taxTran = (TaxTran) null;
              FinPeriod finPeriodByDate = svat.FinPeriodRepository.GetFinPeriodByDate(histSVAT.TaxInvoiceDate, PXAccess.GetParentOrganizationID(histSVAT.AdjdBranchID));
              string masterInvoiceNbr = string.Empty;
              if (histSVAT.Module == "AR")
              {
                PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>> pxSelect = new PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>((PXGraph) instance1);
                PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelect).Select(new object[1]
                {
                  (object) histSVAT.AdjdRefNbr
                }));
                masterInvoiceNbr = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelect).Select(new object[1]
                {
                  (object) arInvoice?.MasterRefNbr
                }))?.RefNbr;
              }
              else if (histSVAT.Module == "AP")
              {
                PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>> pxSelect = new PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>((PXGraph) instance1);
                PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) pxSelect).Select(new object[1]
                {
                  (object) histSVAT.AdjdRefNbr
                }));
                masterInvoiceNbr = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) pxSelect).Select(new object[1]
                {
                  (object) apInvoice?.MasterRefNbr
                }))?.RefNbr;
              }
              IEnumerable<PXResult<SVATTaxTran>> svatTaxTrans = ProcessSVATBase.GetSVATTaxTrans(instance1, histSVAT, masterInvoiceNbr);
              try
              {
                svat.FinPeriodUtils.ValidateFinPeriod<SVATTaxTran>(GraphHelper.RowCast<SVATTaxTran>((IEnumerable) svatTaxTrans), (Func<SVATTaxTran, string>) (m => svat.FinPeriodRepository.GetFinPeriodByDate(histSVAT.TaxInvoiceDate, PXAccess.GetParentOrganizationID(m.BranchID)).FinPeriodID), (Func<SVATTaxTran, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
              }
              catch (PXException ex)
              {
                PXProcessing<SVATConversionHistExt>.SetError((Exception) ex);
                flag1 = true;
                ++num1;
                continue;
              }
              foreach (PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, Tax> pxResult in svatTaxTrans)
              {
                SVATTaxTran svatTaxTran1 = PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, Tax>.op_Implicit(pxResult);
                PX.Objects.CM.CurrencyInfo info = PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, Tax>.op_Implicit(pxResult);
                Tax tax = PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, Tax>.op_Implicit(pxResult);
                svat.SegregateBatch(instance1, histSVAT, finPeriodByDate, svatTaxTran1, info);
                svatTaxTran1.TaxInvoiceNbr = histSVAT.TaxInvoiceNbr;
                svatTaxTran1.TaxInvoiceDate = histSVAT.TaxInvoiceDate;
                PXCache cach3 = ((PXGraph) instance1).Caches[typeof (SVATTaxTran)];
                cach3.Update((object) svatTaxTran1);
                PX.Objects.CM.CurrencyInfo copy1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(info);
                copy1.CuryInfoID = new long?();
                copy1.ModuleCode = "GL";
                copy1.BaseCalc = new bool?(false);
                PX.Objects.CM.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance1.currencyinfo).Insert(copy1) ?? copy1;
                bool flag2 = ReportTaxProcess.GetMult((TaxTran) svatTaxTran1) == 1M;
                Decimal multByTranType = ReportTaxProcess.GetMultByTranType(svatTaxTran1.Module, svatTaxTran1.TranType);
                Decimal num2 = histSVAT.CuryTaxableAmt.GetValueOrDefault() * multByTranType;
                Decimal num3 = histSVAT.TaxableAmt.GetValueOrDefault() * multByTranType;
                Decimal? nullable2 = histSVAT.CuryTaxAmt;
                Decimal num4 = nullable2.GetValueOrDefault() * multByTranType;
                nullable2 = histSVAT.TaxAmt;
                Decimal num5 = nullable2.GetValueOrDefault() * multByTranType;
                svat.InsertReverseTransaction(instance1, new PX.Objects.GL.GLTran()
                {
                  BranchID = svatTaxTran1.BranchID,
                  AccountID = svatTaxTran1.AccountID,
                  SubID = svatTaxTran1.SubID,
                  CuryDebitAmt = new Decimal?(flag2 ? num4 : 0M),
                  DebitAmt = new Decimal?(flag2 ? num5 : 0M),
                  CuryCreditAmt = new Decimal?(flag2 ? 0M : num4),
                  CreditAmt = new Decimal?(flag2 ? 0M : num5),
                  TranType = svatTaxTran1.TranType,
                  TranClass = "N",
                  RefNbr = svatTaxTran1.RefNbr,
                  TranDesc = svatTaxTran1.TaxInvoiceNbr,
                  TranPeriodID = finPeriodByDate.FinPeriodID,
                  FinPeriodID = finPeriodByDate.FinPeriodID,
                  TranDate = svatTaxTran1.TaxInvoiceDate,
                  CuryInfoID = currencyInfo.CuryInfoID,
                  Released = new bool?(true)
                }, new GLTranInsertContext()
                {
                  SVATTaxTranRecord = svatTaxTran1,
                  TaxRecord = tax
                });
                SVATTaxTran copy2 = PXCache<SVATTaxTran>.CreateCopy(svatTaxTran1);
                copy2.RecordID = new int?();
                copy2.Module = "GL";
                copy2.TranType = svatTaxTran1.TaxType == "A" ? "REO" : "REI";
                copy2.RefNbr = copy2.TaxInvoiceNbr;
                copy2.TranDate = copy2.TaxInvoiceDate;
                copy2.FinPeriodID = finPeriodByDate.FinPeriodID;
                SVATTaxTran svatTaxTran2 = copy2;
                nullable1 = new DateTime?();
                DateTime? nullable3 = nullable1;
                svatTaxTran2.FinDate = nullable3;
                Decimal num6 = -1M * ReportTaxProcess.GetMult((TaxTran) svatTaxTran1) * ReportTaxProcess.GetMult((TaxTran) copy2);
                copy2.CuryTaxableAmt = new Decimal?(num6 * num2);
                copy2.TaxableAmt = new Decimal?(num6 * num3);
                copy2.CuryTaxAmt = new Decimal?(num6 * num4);
                copy2.TaxAmt = new Decimal?(num6 * num5);
                cach3.Insert((object) copy2);
                svat.InsertTransactionWithReclassifyToVATAccount(instance1, new PX.Objects.GL.GLTran()
                {
                  BranchID = svatTaxTran1.BranchID,
                  AccountID = svatTaxTran1.TaxType == "A" ? tax.SalesTaxAcctID : tax.PurchTaxAcctID,
                  SubID = svatTaxTran1.TaxType == "A" ? tax.SalesTaxSubID : tax.PurchTaxSubID,
                  CuryDebitAmt = new Decimal?(flag2 ? 0M : num4),
                  DebitAmt = new Decimal?(flag2 ? 0M : num5),
                  CuryCreditAmt = new Decimal?(flag2 ? num4 : 0M),
                  CreditAmt = new Decimal?(flag2 ? num5 : 0M),
                  TranType = svatTaxTran1.TranType,
                  TranClass = "N",
                  RefNbr = svatTaxTran1.RefNbr,
                  TranDesc = svatTaxTran1.TaxInvoiceNbr,
                  TranPeriodID = finPeriodByDate.FinPeriodID,
                  FinPeriodID = finPeriodByDate.FinPeriodID,
                  TranDate = svatTaxTran1.TaxInvoiceDate,
                  CuryInfoID = currencyInfo.CuryInfoID,
                  Released = new bool?(true)
                }, new GLTranInsertContext()
                {
                  SVATTaxTranRecord = svatTaxTran1,
                  TaxRecord = tax
                });
                SVATTaxTran copy3 = PXCache<SVATTaxTran>.CreateCopy(svatTaxTran1);
                copy3.RecordID = new int?();
                copy3.Module = "GL";
                copy3.TranType = svatTaxTran1.TaxType == "A" ? "VTO" : "VTI";
                copy3.TaxType = svatTaxTran1.TaxType == "A" ? "S" : "P";
                copy3.AccountID = svatTaxTran1.TaxType == "A" ? tax.SalesTaxAcctID : tax.PurchTaxAcctID;
                copy3.SubID = svatTaxTran1.TaxType == "A" ? tax.SalesTaxSubID : tax.PurchTaxSubID;
                copy3.RefNbr = copy3.TaxInvoiceNbr;
                copy3.TranDate = copy3.TaxInvoiceDate;
                copy3.FinPeriodID = finPeriodByDate.FinPeriodID;
                SVATTaxTran svatTaxTran3 = copy3;
                nullable1 = new DateTime?();
                DateTime? nullable4 = nullable1;
                svatTaxTran3.FinDate = nullable4;
                Decimal num7 = ReportTaxProcess.GetMult((TaxTran) svatTaxTran1) * ReportTaxProcess.GetMult((TaxTran) copy3);
                copy3.CuryTaxableAmt = new Decimal?(num7 * num2);
                copy3.TaxableAmt = new Decimal?(num7 * num3);
                copy3.CuryTaxAmt = new Decimal?(num7 * num4);
                copy3.TaxAmt = new Decimal?(num7 * num5);
                taxTran = (TaxTran) cach3.Insert((object) copy3);
              }
              if (histSVAT.ReversalMethod == "P")
              {
                SVATConversionHist svatConversionHist1 = PXResultset<SVATConversionHist>.op_Implicit(PXSelectBase<SVATConversionHist, PXSelect<SVATConversionHist, Where<SVATConversionHist.module, Equal<Current<SVATConversionHist.module>>, And<SVATConversionHist.adjdDocType, Equal<Current<SVATConversionHist.adjdDocType>>, And<SVATConversionHist.adjdRefNbr, Equal<Current<SVATConversionHist.adjdRefNbr>>, And<SVATConversionHist.adjgDocType, Equal<SVATConversionHist.adjdDocType>, And<SVATConversionHist.adjgRefNbr, Equal<SVATConversionHist.adjdRefNbr>, And<SVATConversionHist.adjNbr, Equal<decimal_1>, And<SVATConversionHist.taxID, Equal<Current<SVATConversionHist.taxID>>>>>>>>>>.Config>.SelectSingleBound((PXGraph) svat, new object[1]
                {
                  (object) histSVAT
                }, Array.Empty<object>()));
                SVATConversionHist svatConversionHist2 = svatConversionHist1;
                Decimal? unrecognizedTaxAmt1 = svatConversionHist2.CuryUnrecognizedTaxAmt;
                Decimal? nullable5 = histSVAT.CuryTaxAmt;
                svatConversionHist2.CuryUnrecognizedTaxAmt = unrecognizedTaxAmt1.HasValue & nullable5.HasValue ? new Decimal?(unrecognizedTaxAmt1.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
                SVATConversionHist svatConversionHist3 = svatConversionHist1;
                nullable5 = svatConversionHist3.UnrecognizedTaxAmt;
                Decimal? taxAmt = histSVAT.TaxAmt;
                svatConversionHist3.UnrecognizedTaxAmt = nullable5.HasValue & taxAmt.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - taxAmt.GetValueOrDefault()) : new Decimal?();
                Decimal? unrecognizedTaxAmt2 = svatConversionHist1.CuryUnrecognizedTaxAmt;
                Decimal num8 = 0M;
                if (unrecognizedTaxAmt2.GetValueOrDefault() == num8 & unrecognizedTaxAmt2.HasValue)
                {
                  svatConversionHist1.Processed = new bool?(true);
                  svatConversionHist1.AdjgFinPeriodID = finPeriodByDate.FinPeriodID;
                }
                ((PXSelectBase) svat.SVATDocuments).Cache.Update((object) svatConversionHist1);
              }
              ((PXAction) instance1.Save).Press();
              histSVAT.Processed = new bool?(true);
              histSVAT.CuryUnrecognizedTaxAmt = new Decimal?(0M);
              histSVAT.UnrecognizedTaxAmt = new Decimal?(0M);
              histSVAT.TaxRecordID = (int?) taxTran?.RecordID;
              histSVAT.AdjBatchNbr = ((PXSelectBase<Batch>) instance1.BatchModule).Current?.BatchNbr;
              histSVAT.AdjgFinPeriodID = finPeriodByDate.FinPeriodID;
              ((PXSelectBase) svat.SVATDocuments).Cache.Update((object) histSVAT);
              ((PXGraph) svat).Persist();
              transactionScope.Complete();
              if (((PXSelectBase<Batch>) instance1.BatchModule).Current != null)
              {
                if (!dictionary2.ContainsKey(histSVAT.AdjBatchNbr))
                {
                  dictionary2.Add(histSVAT.AdjBatchNbr, new Tuple<Batch, int>(((PXSelectBase<Batch>) instance1.BatchModule).Current, num1));
                  goto label_36;
                }
                goto label_36;
              }
              goto label_36;
            }
          }
          PXProcessing<SVATConversionHistExt>.SetError("The record cannot be processed because the Tax Doc. Nbr box is empty. To proceed, fill in the box.");
          flag1 = true;
          ++num1;
          continue;
        }
label_36:
        PXProcessing<SVATConversionHistExt>.SetInfo(num1, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        PXProcessing<SVATConversionHistExt>.SetError(num1, ex);
        flag1 = true;
        dictionary1.Remove(key);
      }
      ++num1;
    }
    if (((PXSelectBase<GLSetup>) instance1.glsetup).Current.AutoPostOption.GetValueOrDefault())
    {
      PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
      foreach (Tuple<Batch, int> tuple in dictionary2.Values)
      {
        try
        {
          ((PXGraph) instance2).Clear();
          instance2.PostBatchProc(tuple.Item1);
        }
        catch (Exception ex)
        {
          PXProcessing<SVATConversionHistExt>.SetError(tuple.Item2, ex);
          flag1 = true;
        }
      }
    }
    if (filter.ReversalMethod == "P")
    {
      foreach (AdjKey key in dictionary1.Keys)
      {
        string str = dictionary1[key];
        switch (key.Module)
        {
          case "AP":
            PXUpdate<Set<APAdjust.taxInvoiceNbr, Required<APAdjust.taxInvoiceNbr>>, APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjdLineNbr, Equal<Required<APAdjust.adjdLineNbr>>, And<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.adjNbr, Equal<Required<APAdjust.adjNbr>>>>>>>>>.Update((PXGraph) svat, new object[7]
            {
              (object) str,
              (object) key.AdjdDocType,
              (object) key.AdjdRefNbr,
              (object) key.AdjdLineNbr,
              (object) key.AdjgDocType,
              (object) key.AdjgRefNbr,
              (object) key.AdjNbr
            });
            continue;
          case "AR":
            PXUpdate<Set<ARAdjust.taxInvoiceNbr, Required<ARAdjust.taxInvoiceNbr>>, ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjdLineNbr, Equal<Required<ARAdjust.adjdLineNbr>>, And<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.adjNbr, Equal<Required<ARAdjust.adjNbr>>>>>>>>>.Update((PXGraph) svat, new object[7]
            {
              (object) str,
              (object) key.AdjdDocType,
              (object) key.AdjdRefNbr,
              (object) key.AdjdLineNbr,
              (object) key.AdjgDocType,
              (object) key.AdjgRefNbr,
              (object) key.AdjNbr
            });
            continue;
          default:
            continue;
        }
      }
    }
    if (flag1)
    {
      PXProcessing<PX.Objects.AP.APPayment>.SetCurrentItem((object) null);
      throw new PXException("One or more documents could not be released.");
    }
  }

  protected virtual void SegregateBatch(
    JournalEntry je,
    SVATConversionHistExt histSVAT,
    FinPeriod finPeriod,
    SVATTaxTran taxtran,
    PX.Objects.CM.CurrencyInfo info)
  {
    je.SegregateBatch("GL", taxtran.BranchID, info.CuryID, histSVAT.TaxInvoiceDate, finPeriod.FinPeriodID, (string) null, info, (Batch) null);
  }

  protected virtual PX.Objects.GL.GLTran InsertReverseTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    GLTranInsertContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  protected virtual PX.Objects.GL.GLTran InsertTransactionWithReclassifyToVATAccount(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    GLTranInsertContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public static IEnumerable<PXResult<SVATTaxTran>> GetSVATTaxTrans(
    JournalEntry je,
    SVATConversionHistExt histSVAT,
    string masterInvoiceNbr)
  {
    return ((IEnumerable<PXResult<SVATTaxTran>>) ProcessSVATBase.GetCommandForSVATTaxTrans(je).Select(new object[7]
    {
      (object) histSVAT.Module,
      (object) histSVAT.VendorID,
      (object) histSVAT.VendorID,
      (object) histSVAT.AdjdDocType,
      (object) histSVAT.AdjdRefNbr,
      (object) masterInvoiceNbr,
      (object) histSVAT.TaxID
    })).AsEnumerable<PXResult<SVATTaxTran>>();
  }

  public static PXSelectBase<SVATTaxTran> GetCommandForSVATTaxTrans(JournalEntry je)
  {
    return (PXSelectBase<SVATTaxTran>) new PXSelectJoin<SVATTaxTran, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<SVATTaxTran.curyInfoID>>, InnerJoin<Tax, On<Tax.taxID, Equal<SVATTaxTran.taxID>>>>, Where<SVATTaxTran.module, Equal<Required<SVATConversionHistExt.module>>, And2<Where<SVATTaxTran.vendorID, Equal<Required<SVATConversionHist.vendorID>>, Or<SVATTaxTran.vendorID, IsNull, And<Required<SVATConversionHist.vendorID>, IsNull>>>, And<SVATTaxTran.tranType, Equal<Required<SVATConversionHistExt.adjdDocType>>, And2<Where<SVATTaxTran.refNbr, Equal<Required<SVATConversionHistExt.adjdRefNbr>>, Or<SVATTaxTran.refNbr, Equal<Required<SVATTaxTran.refNbr>>>>, And<SVATTaxTran.taxID, Equal<Required<SVATConversionHistExt.taxID>>>>>>>>((PXGraph) je);
  }
}
