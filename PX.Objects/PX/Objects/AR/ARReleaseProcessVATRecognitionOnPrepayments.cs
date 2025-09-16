// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARReleaseProcessVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARReleaseProcessVATRecognitionOnPrepayments : PXGraphExtension<ARReleaseProcess>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  [PXOverride]
  public void PerformBasicReleaseChecks(
    PXGraph selectGraph,
    ARRegister document,
    ARReleaseProcessVATRecognitionOnPrepayments.PerformBasicReleaseChecksDelegate baseMethod)
  {
    baseMethod(selectGraph, document);
    if (!((document is ARInvoice arInvoice ? arInvoice.DocType : (string) null) == "PPI"))
      return;
    if (((IQueryable<PXResult<PX.Objects.CS.Terms>>) PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Required<ARInvoice.termsID>>, And<PX.Objects.CS.Terms.installmentType, Equal<TermsInstallmentType.multiple>>>>.Config>.SelectSingleBound(selectGraph, (object[]) null, new object[1]
    {
      (object) arInvoice.TermsID
    })).Any<PXResult<PX.Objects.CS.Terms>>())
      throw new ReleaseException("The prepayment invoice cannot be released, because a prepayment invoice cannot have terms with the Multiple installment type.", Array.Empty<object>());
    int? nullable = arInvoice.PrepaymentAccountID;
    if (!nullable.HasValue)
      throw new ReleaseException("The document cannot be released because the Prepayment Account box is empty on the Financial tab of the Invoices and Memos (AR301000) form. To proceed, fill in this box.", Array.Empty<object>());
    if (!PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
      return;
    nullable = arInvoice.PrepaymentSubID;
    if (!nullable.HasValue)
      throw new ReleaseException("The document cannot be released because the Prepayment Subaccount box is empty on the Financial tab of the Invoices and Memos (AR301000) form. To proceed, fill in this box.", Array.Empty<object>());
  }

  [PXOverride]
  public void SetClosedPeriodsFromLatestApplication(
    ARRegister doc,
    ARReleaseProcessVATRecognitionOnPrepayments.SetClosedPeriodsFromLatestApplicationDelegate baseMethod)
  {
    if (!doc.IsPrepaymentInvoiceDocument())
    {
      baseMethod(doc);
    }
    else
    {
      ARTranPost arTranPost1 = PXResultset<ARTranPost>.op_Implicit(PXSelectBase<ARTranPost, PXSelect<ARTranPost, Where<ARTranPost.docType, Equal<Required<ARTranPost.docType>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTranPost.voidAdjNbr, IsNull, And<NotExists<Select<ARTranPostAlias, Where<ARTranPostAlias.voidAdjNbr, Equal<ARTranPost.adjNbr>, And<ARTranPostAlias.refNbr, Equal<ARTranPost.refNbr>, And<ARTranPostAlias.docType, Equal<ARTranPost.docType>, And<ARTranPostAlias.lineNbr, Equal<ARTranPost.lineNbr>, And<ARTranPostAlias.sourceRefNbr, Equal<ARTranPost.sourceRefNbr>, And<ARTranPostAlias.sourceDocType, Equal<ARTranPost.sourceDocType>>>>>>>>>>>>>, OrderBy<Desc<ARTranPost.tranPeriodID, Desc<ARTranPost.iD>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[0], new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }));
      ARTranPost arTranPost2 = PXResultset<ARTranPost>.op_Implicit(PXSelectBase<ARTranPost, PXSelect<ARTranPost, Where<ARTranPost.docType, Equal<Required<ARTranPost.docType>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTranPost.voidAdjNbr, IsNull, And<NotExists<Select<ARTranPostAlias, Where<ARTranPostAlias.voidAdjNbr, Equal<ARTranPost.adjNbr>, And<ARTranPostAlias.refNbr, Equal<ARTranPost.refNbr>, And<ARTranPostAlias.docType, Equal<ARTranPost.docType>, And<ARTranPostAlias.lineNbr, Equal<ARTranPost.lineNbr>, And<ARTranPostAlias.sourceRefNbr, Equal<ARTranPost.sourceRefNbr>, And<ARTranPostAlias.sourceDocType, Equal<ARTranPost.sourceDocType>>>>>>>>>>>>>, OrderBy<Desc<ARTranPost.docDate, Desc<ARTranPost.iD>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[0], new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }));
      doc.ClosedTranPeriodID = FinPeriodUtils.Max(arTranPost1?.TranPeriodID, doc.TranPeriodID);
      FinPeriodIDAttribute.SetPeriodsByMaster<ARRegister.closedFinPeriodID>(((PXSelectBase) this.Base.ARDocument).Cache, (object) doc, doc.ClosedTranPeriodID);
      doc.ClosedDate = FinPeriodUtils.Max((DateTime?) arTranPost2?.DocDate, doc.DocDate);
    }
  }

  private void SegregateBatch(
    JournalEntry je,
    int? branchID,
    string curyID,
    DateTime? docDate,
    string finPeriodID,
    string description,
    PX.Objects.CM.CurrencyInfo curyInfo,
    Batch paymentBatch)
  {
    if (((PXGraph) je).Caches[typeof (PX.Objects.GL.GLTran)].IsInsertedUpdatedDeleted)
      ((PXAction) je.Save).Press();
    JournalEntry.SegregateBatch(je, "AR", branchID, curyID, docDate, finPeriodID, description, curyInfo, paymentBatch);
  }

  [PXOverride]
  public virtual void AfterSVATConversionHistoryInserted(
    JournalEntry je,
    ARAdjust adj,
    ARInvoice adjddoc,
    ARRegister adjgdoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    ARReleaseProcessVATRecognitionOnPrepayments.AfterSVATConversionHistoryInsertedDelegate baseMethod)
  {
    if (adjddoc.IsPrepaymentInvoiceDocument())
    {
      bool flag1 = adjgdoc.DocType == "REF" || adjgdoc.DocType == "VRF";
      bool flag2 = adjddoc.DrCr == "D";
      PX.Objects.TX.Tax tax = PX.Objects.TX.Tax.PK.Find((PXGraph) this.Base, adjSVAT.TaxID);
      int? nullable1;
      if (!adjgdoc.IsPrepaymentInvoiceDocumentReverse())
      {
        nullable1 = tax.OnARPrepaymentTaxAcctID;
        if (!nullable1.HasValue)
          throw new ReleaseException("The document cannot be released because the Tax on AR Prepayment Account box is empty for the {0} tax. To proceed, fill in this box on the Taxes (TX205000) form.", new object[1]
          {
            (object) tax.TaxID
          });
        if (PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
        {
          nullable1 = tax.OnARPrepaymentTaxSubID;
          if (!nullable1.HasValue)
            throw new ReleaseException("The document cannot be released because the Tax on AR Prepayment Subaccount box is empty for the {0} tax. To proceed, fill in this box on the Taxes (TX205000) form.", new object[1]
            {
              (object) tax.TaxID
            });
        }
      }
      PXCache cach1 = ((PXGraph) je).Caches[typeof (TaxTran)];
      PXCache cach2 = ((PXGraph) je).Caches[typeof (SVATTaxTran)];
      ((PXGraph) je).Views.Caches.Add(typeof (SVATTaxTran));
      SVATConversionHistExt histSVAT = PX.Objects.Common.Utilities.Clone<SVATConversionHist, SVATConversionHistExt>((PXGraph) this.Base, docSVAT);
      SVATTaxTran tran1 = PXResult<SVATTaxTran>.op_Implicit(ProcessSVATBase.GetSVATTaxTrans(je, histSVAT, adj.AdjdRefNbr).First<PXResult<SVATTaxTran>>());
      if (flag1)
        adjSVAT.Processed = new bool?(true);
      if (adjgdoc.IsPrepaymentInvoiceDocumentReverse())
      {
        adjSVAT.Processed = new bool?(true);
        adjSVAT = (SVATConversionHist) ((PXSelectBase) this.Base.SVATConversionHistory).Cache.Update((object) adjSVAT);
        Decimal multByTranType = ReportTaxProcess.GetMultByTranType("AR", adjgdoc.DocType);
        SVATTaxTran svatTaxTran1 = tran1;
        Decimal? adjustedTaxableAmt = svatTaxTran1.CuryAdjustedTaxableAmt;
        Decimal num1 = multByTranType;
        Decimal? nullable2 = adjSVAT.CuryTaxableAmt;
        Decimal? nullable3 = nullable2.HasValue ? new Decimal?(num1 * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4;
        if (!(adjustedTaxableAmt.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(adjustedTaxableAmt.GetValueOrDefault() + nullable3.GetValueOrDefault());
        svatTaxTran1.CuryAdjustedTaxableAmt = nullable4;
        SVATTaxTran svatTaxTran2 = tran1;
        nullable3 = svatTaxTran2.AdjustedTaxableAmt;
        Decimal num2 = multByTranType;
        nullable2 = adjSVAT.TaxableAmt;
        Decimal? nullable5 = nullable2.HasValue ? new Decimal?(num2 * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6;
        if (!(nullable3.HasValue & nullable5.HasValue))
        {
          nullable2 = new Decimal?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new Decimal?(nullable3.GetValueOrDefault() + nullable5.GetValueOrDefault());
        svatTaxTran2.AdjustedTaxableAmt = nullable6;
        SVATTaxTran svatTaxTran3 = tran1;
        nullable5 = svatTaxTran3.CuryAdjustedTaxAmt;
        Decimal num3 = multByTranType;
        nullable2 = adjSVAT.CuryTaxAmt;
        nullable3 = nullable2.HasValue ? new Decimal?(num3 * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable7;
        if (!(nullable5.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable5.GetValueOrDefault() + nullable3.GetValueOrDefault());
        svatTaxTran3.CuryAdjustedTaxAmt = nullable7;
        SVATTaxTran svatTaxTran4 = tran1;
        nullable3 = svatTaxTran4.AdjustedTaxAmt;
        Decimal num4 = multByTranType;
        nullable2 = adjSVAT.TaxAmt;
        nullable5 = nullable2.HasValue ? new Decimal?(num4 * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable8;
        if (!(nullable3.HasValue & nullable5.HasValue))
        {
          nullable2 = new Decimal?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new Decimal?(nullable3.GetValueOrDefault() + nullable5.GetValueOrDefault());
        svatTaxTran4.AdjustedTaxAmt = nullable8;
        ((PXGraph) je).Caches[typeof (SVATTaxTran)].Update((object) tran1);
        ((PXAction) je.Save).Press();
      }
      else
      {
        Batch current = ((PXGraph) je).Caches[typeof (Batch)].Current as Batch;
        PX.Objects.CM.CurrencyInfo curyInfo = (PX.Objects.CM.CurrencyInfo) null;
        bool flag3 = adjddoc.CuryID != adjgdoc.CuryID;
        if (flag3)
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.CurrencyInfo_CuryInfoID).Select(new object[1]
          {
            (object) adjSVAT.CuryInfoID
          }));
          curyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Select(Array.Empty<object>()));
          this.SegregateBatch(je, adjSVAT.AdjdBranchID, adjddoc.CuryID, adjSVAT.AdjdDocDate, adjSVAT.AdjdFinPeriodID, adjddoc.DocDesc, currencyInfo?.GetCM(), (Batch) null);
        }
        PX.Objects.GL.GLTran tran2 = new PX.Objects.GL.GLTran();
        tran2.SummPost = new bool?(this.Base.SummPost);
        tran2.BranchID = adjSVAT.AdjdBranchID;
        tran2.CuryInfoID = adjSVAT.CuryInfoID;
        tran2.TranType = adjSVAT.AdjdDocType;
        tran2.TranClass = "T";
        tran2.RefNbr = adjSVAT.AdjdRefNbr;
        tran2.TranDate = adjSVAT.AdjdDocDate;
        tran2.AccountID = tax.SalesTaxAcctID;
        tran2.SubID = tax.SalesTaxSubID;
        tran2.TranDesc = tax.TaxID;
        tran2.CuryDebitAmt = flag2 ? adjSVAT.CuryTaxAmt : new Decimal?(0M);
        tran2.DebitAmt = flag2 ? adjSVAT.TaxAmt : new Decimal?(0M);
        tran2.CuryCreditAmt = flag2 ? new Decimal?(0M) : adjSVAT.CuryTaxAmt;
        tran2.CreditAmt = flag2 ? new Decimal?(0M) : adjSVAT.TaxAmt;
        tran2.Released = new bool?(true);
        tran2.ReferenceID = adjddoc.CustomerID;
        PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, tax.OnARPrepaymentTaxAcctID);
        this.Base.SetProjectAndTaxID(tran2, account, adjddoc);
        this.Base.InsertInvoiceTaxTransaction(je, tran2, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = (ARRegister) adjddoc
        });
        PX.Objects.GL.GLTran copy1 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) tran2);
        copy1.AccountID = tax.OnARPrepaymentTaxAcctID;
        copy1.SubID = tax.OnARPrepaymentTaxSubID;
        copy1.CuryDebitAmt = flag2 ? new Decimal?(0M) : adjSVAT.CuryTaxAmt;
        copy1.DebitAmt = flag2 ? new Decimal?(0M) : adjSVAT.TaxAmt;
        copy1.CuryCreditAmt = flag2 ? adjSVAT.CuryTaxAmt : new Decimal?(0M);
        copy1.CreditAmt = flag2 ? adjSVAT.TaxAmt : new Decimal?(0M);
        this.Base.InsertInvoiceTaxTransaction(je, copy1, new ARReleaseProcess.GLTranInsertionContext()
        {
          ARRegisterRecord = (ARRegister) adjddoc
        });
        SVATTaxTran copy2 = PXCache<SVATTaxTran>.CreateCopy(tran1);
        SVATTaxTran svatTaxTran5 = copy2;
        nullable1 = new int?();
        int? nullable9 = nullable1;
        svatTaxTran5.RecordID = nullable9;
        copy2.Module = "AR";
        copy2.TaxType = "S";
        copy2.AccountID = tax.SalesTaxAcctID;
        copy2.SubID = tax.SalesTaxSubID;
        copy2.TaxInvoiceNbr = adjgdoc.RefNbr;
        copy2.TaxPeriodID = (string) null;
        copy2.TranDate = adj.AdjgDocDate;
        copy2.FinDate = new DateTime?();
        copy2.FinPeriodID = adj.AdjgFinPeriodID;
        Decimal num5 = ReportTaxProcess.GetMult((TaxTran) tran1) * ReportTaxProcess.GetMult((TaxTran) copy2);
        if (flag1)
          num5 = -1M * num5;
        SVATTaxTran svatTaxTran6 = copy2;
        Decimal num6 = num5;
        Decimal? curyTaxableAmt = adjSVAT.CuryTaxableAmt;
        Decimal? nullable10 = curyTaxableAmt.HasValue ? new Decimal?(num6 * curyTaxableAmt.GetValueOrDefault()) : new Decimal?();
        svatTaxTran6.CuryTaxableAmt = nullable10;
        SVATTaxTran svatTaxTran7 = copy2;
        Decimal num7 = num5;
        Decimal? nullable11 = adjSVAT.TaxableAmt;
        Decimal? nullable12 = nullable11.HasValue ? new Decimal?(num7 * nullable11.GetValueOrDefault()) : new Decimal?();
        svatTaxTran7.TaxableAmt = nullable12;
        SVATTaxTran svatTaxTran8 = copy2;
        Decimal num8 = num5;
        nullable11 = adjSVAT.CuryTaxAmt;
        Decimal? nullable13 = nullable11.HasValue ? new Decimal?(num8 * nullable11.GetValueOrDefault()) : new Decimal?();
        svatTaxTran8.CuryTaxAmt = nullable13;
        SVATTaxTran svatTaxTran9 = copy2;
        Decimal num9 = num5;
        nullable11 = adjSVAT.TaxAmt;
        Decimal? nullable14 = nullable11.HasValue ? new Decimal?(num9 * nullable11.GetValueOrDefault()) : new Decimal?();
        svatTaxTran9.TaxAmt = nullable14;
        ((PXGraph) je).Caches[typeof (SVATTaxTran)].Insert((object) copy2);
        if (!flag3)
          return;
        this.SegregateBatch(je, current.BranchID, current.CuryID, (DateTime?) curyInfo?.CuryEffDate, current.FinPeriodID, current.Description, curyInfo, current);
      }
    }
    else
    {
      if (!adjgdoc.IsPrepaymentInvoiceDocument())
        return;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.CurrencyInfo_CuryInfoID).Select(new object[1]
      {
        (object) adjSVAT.CuryInfoID
      }));
      Batch current = ((PXGraph) je).Caches[typeof (Batch)].Current as Batch;
      PX.Objects.CM.CurrencyInfo curyInfo = (PX.Objects.CM.CurrencyInfo) null;
      bool flag4 = current?.CuryID != currencyInfo1.CuryID;
      if (flag4)
      {
        curyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Select(Array.Empty<object>()));
        this.SegregateBatch(je, current.BranchID, currencyInfo1.CuryID, adjSVAT.AdjdDocDate, current.FinPeriodID, adjddoc.DocDesc, currencyInfo1.GetCM(), (Batch) null);
      }
      if (adjSVAT.ReversalMethod == "Y")
        adjSVAT.Processed = new bool?(true);
      adjSVAT = (SVATConversionHist) ((PXSelectBase) this.Base.SVATConversionHistory).Cache.Update((object) adjSVAT);
      SVATConversionHistExt histSVAT = PX.Objects.Common.Utilities.Clone<SVATConversionHist, SVATConversionHistExt>((PXGraph) this.Base, adjSVAT);
      PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax> svatTaxTransSales = ARReleaseProcessVATRecognitionOnPrepayments.GetSVATTaxTransSales(je, histSVAT, adj.AdjdRefNbr);
      if (svatTaxTransSales == null)
        return;
      SVATTaxTran tran = PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>.op_Implicit(svatTaxTransSales);
      PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>.op_Implicit(svatTaxTransSales);
      PX.Objects.TX.Tax tax = PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>.op_Implicit(svatTaxTransSales);
      PX.Objects.CM.CurrencyInfo copy3 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo2);
      copy3.CuryInfoID = new long?();
      copy3.ModuleCode = "AR";
      copy3.BaseCalc = new bool?(false);
      PX.Objects.CM.CurrencyInfo currencyInfo3 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(copy3) ?? copy3;
      bool flag5 = ReportTaxProcess.GetMult((TaxTran) tran) == 1M;
      ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(new PX.Objects.GL.GLTran()
      {
        Module = "AR",
        BranchID = tran.BranchID,
        AccountID = tran.AccountID,
        SubID = tran.SubID,
        CuryDebitAmt = flag5 ? adjSVAT.CuryTaxAmt : new Decimal?(0M),
        DebitAmt = flag5 ? adjSVAT.TaxAmt : new Decimal?(0M),
        CuryCreditAmt = flag5 ? new Decimal?(0M) : adjSVAT.CuryTaxAmt,
        CreditAmt = flag5 ? new Decimal?(0M) : adjSVAT.TaxAmt,
        TranType = tran.TranType,
        TranClass = "N",
        RefNbr = tran.RefNbr,
        TranDesc = tax.TaxID,
        TranPeriodID = adj.AdjgTranPeriodID,
        FinPeriodID = adj.AdjgFinPeriodID,
        TranDate = tran.TaxInvoiceDate,
        CuryInfoID = currencyInfo3.CuryInfoID,
        ReferenceID = adj.CustomerID,
        Released = new bool?(true)
      });
      ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(new PX.Objects.GL.GLTran()
      {
        Module = "AR",
        BranchID = tran.BranchID,
        AccountID = tax.OnARPrepaymentTaxAcctID,
        SubID = tax.OnARPrepaymentTaxSubID,
        CuryDebitAmt = flag5 ? new Decimal?(0M) : adjSVAT.CuryTaxAmt,
        DebitAmt = flag5 ? new Decimal?(0M) : adjSVAT.TaxAmt,
        CuryCreditAmt = flag5 ? adjSVAT.CuryTaxAmt : new Decimal?(0M),
        CreditAmt = flag5 ? adjSVAT.TaxAmt : new Decimal?(0M),
        TranType = tran.TranType,
        TranClass = "N",
        RefNbr = tran.RefNbr,
        TranDesc = tax.TaxID,
        TranPeriodID = adj.AdjgTranPeriodID,
        FinPeriodID = adj.AdjgFinPeriodID,
        TranDate = tran.TaxInvoiceDate,
        CuryInfoID = currencyInfo3.CuryInfoID,
        ReferenceID = adj.CustomerID,
        Released = new bool?(true)
      });
      PXCache cach3 = ((PXGraph) je).Caches[typeof (TaxTran)];
      PXCache cach4 = ((PXGraph) je).Caches[typeof (SVATTaxTran)];
      ((PXGraph) je).Views.Caches.Add(typeof (SVATTaxTran));
      SVATTaxTran copy4 = PXCache<SVATTaxTran>.CreateCopy(tran);
      copy4.RecordID = new int?();
      copy4.Module = "AR";
      copy4.TaxType = "S";
      copy4.AccountID = tax.SalesTaxAcctID;
      copy4.SubID = tax.SalesTaxSubID;
      copy4.TaxInvoiceNbr = adjddoc.RefNbr;
      copy4.TaxPeriodID = (string) null;
      copy4.TranDate = adj.AdjgDocDate;
      copy4.FinDate = new DateTime?();
      copy4.FinPeriodID = adj.AdjgFinPeriodID;
      Decimal num10 = -1M * ReportTaxProcess.GetMult((TaxTran) tran) * ReportTaxProcess.GetMult((TaxTran) copy4);
      SVATTaxTran svatTaxTran10 = copy4;
      Decimal num11 = num10;
      Decimal? curyTaxableAmt = adjSVAT.CuryTaxableAmt;
      Decimal? nullable15 = curyTaxableAmt.HasValue ? new Decimal?(num11 * curyTaxableAmt.GetValueOrDefault()) : new Decimal?();
      svatTaxTran10.CuryTaxableAmt = nullable15;
      SVATTaxTran svatTaxTran11 = copy4;
      Decimal num12 = num10;
      Decimal? nullable16 = adjSVAT.TaxableAmt;
      Decimal? nullable17 = nullable16.HasValue ? new Decimal?(num12 * nullable16.GetValueOrDefault()) : new Decimal?();
      svatTaxTran11.TaxableAmt = nullable17;
      SVATTaxTran svatTaxTran12 = copy4;
      Decimal num13 = num10;
      nullable16 = adjSVAT.CuryTaxAmt;
      Decimal? nullable18 = nullable16.HasValue ? new Decimal?(num13 * nullable16.GetValueOrDefault()) : new Decimal?();
      svatTaxTran12.CuryTaxAmt = nullable18;
      SVATTaxTran svatTaxTran13 = copy4;
      Decimal num14 = num10;
      nullable16 = adjSVAT.TaxAmt;
      Decimal? nullable19 = nullable16.HasValue ? new Decimal?(num14 * nullable16.GetValueOrDefault()) : new Decimal?();
      svatTaxTran13.TaxAmt = nullable19;
      ((PXGraph) je).Caches[typeof (SVATTaxTran)].Insert((object) copy4);
      if (!flag4)
        return;
      this.SegregateBatch(je, current.BranchID, current.CuryID, (DateTime?) curyInfo?.CuryEffDate, current.FinPeriodID, current.Description, curyInfo, current);
    }
  }

  public static PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax> GetSVATTaxTransSales(
    JournalEntry je,
    SVATConversionHistExt histSVAT,
    string masterInvoiceNbr)
  {
    PXSelectBase<SVATTaxTran> commandForSvatTaxTrans = ProcessSVATBase.GetCommandForSVATTaxTrans(je);
    commandForSvatTaxTrans.WhereAnd<Where<SVATTaxTran.taxType, Equal<TaxType.sales>>>();
    return ((IEnumerable<PXResult<SVATTaxTran>>) commandForSvatTaxTrans.Select(new object[7]
    {
      (object) histSVAT.Module,
      (object) histSVAT.VendorID,
      (object) histSVAT.VendorID,
      (object) histSVAT.AdjdDocType,
      (object) histSVAT.AdjdRefNbr,
      (object) masterInvoiceNbr,
      (object) histSVAT.TaxID
    })).AsEnumerable<PXResult<SVATTaxTran>>().Cast<PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>>().FirstOrDefault<PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>>();
  }

  [PXOverride]
  public virtual SVATConversionHist ProcessLastSVATRecord(
    ARRegister adjddoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    Decimal percent,
    ARReleaseProcessVATRecognitionOnPrepayments.ProcessLastSVATRecordDelegate baseMethod)
  {
    int num1 = percent != 1M ? 1 : 0;
    adjSVAT.CuryTaxableAmt = docSVAT.CuryTaxableAmt;
    adjSVAT.TaxableAmt = docSVAT.TaxableAmt;
    adjSVAT.CuryTaxAmt = docSVAT.CuryTaxAmt;
    adjSVAT.TaxAmt = docSVAT.TaxAmt;
    if (num1 != 0)
    {
      List<object> objectList = new List<object>()
      {
        (object) docSVAT.AdjdDocType,
        (object) docSVAT.AdjdRefNbr,
        (object) docSVAT.TaxID
      };
      BqlCommand command = ((PXSelectBase) this.Base.SVATRecognitionRecords).View.BqlSelect;
      bool? nullable1;
      ref bool? local = ref nullable1;
      int num2;
      if (adjddoc.IsPrepaymentInvoiceDocument())
      {
        bool? pendingPayment = adjddoc.PendingPayment;
        bool flag = false;
        num2 = pendingPayment.GetValueOrDefault() == flag & pendingPayment.HasValue ? 1 : 0;
      }
      else
        num2 = 0;
      local = new bool?(num2 != 0);
      if (nullable1.GetValueOrDefault())
      {
        command = command.WhereAnd<Where<BqlOperand<SVATConversionHist.processed, IBqlBool>.IsEqual<P.AsBool>>>();
        objectList.Add((object) true);
      }
      foreach (SVATConversionHist svatConversionHist1 in command.Select<SVATConversionHist>((PXGraph) this.Base, false, objectList.ToArray()))
      {
        SVATConversionHist svatConversionHist2 = adjSVAT;
        Decimal? nullable2 = svatConversionHist2.CuryTaxableAmt;
        Decimal? nullable3 = svatConversionHist1.CuryTaxableAmt;
        Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
        Decimal? nullable4;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault1);
        svatConversionHist2.CuryTaxableAmt = nullable4;
        SVATConversionHist svatConversionHist3 = adjSVAT;
        nullable2 = svatConversionHist3.TaxableAmt;
        nullable3 = svatConversionHist1.TaxableAmt;
        Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable5 = nullable3;
        }
        else
          nullable5 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault2);
        svatConversionHist3.TaxableAmt = nullable5;
        SVATConversionHist svatConversionHist4 = adjSVAT;
        nullable2 = svatConversionHist4.CuryTaxAmt;
        nullable3 = svatConversionHist1.CuryTaxAmt;
        Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
        Decimal? nullable6;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable6 = nullable3;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault3);
        svatConversionHist4.CuryTaxAmt = nullable6;
        SVATConversionHist svatConversionHist5 = adjSVAT;
        nullable2 = svatConversionHist5.TaxAmt;
        nullable3 = svatConversionHist1.TaxAmt;
        Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
        Decimal? nullable7;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable7 = nullable3;
        }
        else
          nullable7 = new Decimal?(nullable2.GetValueOrDefault() - valueOrDefault4);
        svatConversionHist5.TaxAmt = nullable7;
      }
    }
    adjSVAT.CuryUnrecognizedTaxAmt = adjSVAT.CuryTaxAmt;
    adjSVAT.UnrecognizedTaxAmt = adjSVAT.TaxAmt;
    return adjSVAT;
  }

  [PXOverride]
  public virtual void ProcessPostponedFlags(System.Action baseProcessPostponedFlags)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (ARAdjust arAdjust in ((PXSelectBase) this.Base.ARAdjust_AdjgDocType_RefNbr_CustomerID).Cache.Cached.Cast<ARAdjust>())
    {
      if (!(arAdjust.AdjdDocType != "PPI"))
      {
        ARPayment row = ARPayment.PK.Find((PXGraph) this.Base, arAdjust.AdjdDocType, arAdjust.AdjdRefNbr);
        ARRegister arRegister = ((PXSelectBase<ARRegister>) this.Base.ARDocument).Locate(new ARRegister()
        {
          DocType = arAdjust.AdjdDocType,
          RefNbr = arAdjust.AdjdRefNbr
        });
        if (row != null && arRegister != null)
        {
          ((PXSelectBase) this.Base.pe.Document).Cache.RaiseEventsOnFieldChanging<ARPayment.pendingPayment>((object) row, (object) arRegister.PendingPayment);
          flag1 = true;
        }
      }
    }
    foreach (ARRegister row in ((PXGraph) this.Base).Caches[typeof (ARRegister)].Cached.Cast<ARRegister>().Where<ARRegister>((Func<ARRegister, bool>) (p => p.PostponePendingPaymentFlag.GetValueOrDefault())))
    {
      PXCache cache = ((PXSelectBase) this.Base.soAdjust).Cache;
      row.PendingPayment = new bool?(false);
      ((PXSelectBase) this.Base.InvoiceEntryGraph.Document).Cache.RaiseEventsOnFieldChanging<ARRegister.pendingPayment>((object) row, (object) true);
      row.PostponePendingPaymentFlag = new bool?(false);
      flag2 = true;
    }
    if (flag1)
      ((PXAction) this.Base.pe.Save).Press();
    if (flag2)
      ((PXAction) this.Base.InvoiceEntryGraph.Save).Press();
    baseProcessPostponedFlags();
  }

  [PXOverride]
  public virtual void CreditMemoProcessingBeforeSave(ARRegister ardoc)
  {
    if (!(ardoc is ARInvoice doc) || !doc.IsPrepaymentInvoiceDocumentReverse())
      return;
    ARInvoiceEntryVATRecognitionOnPrepayments extension = ((PXGraph) this.Base.InvoiceEntryGraph).GetExtension<ARInvoiceEntryVATRecognitionOnPrepayments>();
    Decimal? nullable = doc.CuryDocBal;
    Decimal num1 = nullable.Value;
    ARPayment arPayment = ARPayment.PK.Find((PXGraph) this.Base, doc.OrigDocType, doc.OrigRefNbr);
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    foreach (PXResult<SOAdjust, PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<SOAdjust, PXViewOf<SOAdjust>.BasedOn<SelectFromBase<SOAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<PX.Objects.CM.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<SOAdjust.adjdCuryInfoID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<SOAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>.Order<PX.Data.BQL.Fluent.By<BqlField<SOAdjust.recordID, IBqlInt>.Desc>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) doc.OrigDocType,
      (object) doc.OrigRefNbr
    }))
    {
      SOAdjust adj = PXResult<SOAdjust, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo inv_info = PXResult<SOAdjust, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      ARPayment copy = PXCache<ARPayment>.CreateCopy(arPayment);
      copy.CuryDocBal = arPayment.CuryOrigDocAmt;
      copy.DocBal = arPayment.OrigDocAmt;
      Decimal paymentBalance = instance.GetPaymentBalance(inv_info, copy, adj);
      Decimal num2 = num1;
      Decimal num3 = paymentBalance;
      nullable = adj.CuryAdjdAmt;
      Decimal num4 = nullable.Value;
      Decimal num5 = num3 - num4;
      if (!(num2 < num5))
      {
        Decimal num6 = num1;
        nullable = adj.CuryAdjdAmt;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        if (num6 < valueOrDefault & nullable.HasValue)
        {
          adj.AdjAmt = new Decimal?(paymentBalance - num1);
          adj.CuryAdjdAmt = adj.AdjAmt;
          adj.CuryAdjgAmt = adj.AdjAmt;
          ((PXSelectBase) extension.SOAdjustments).Cache.Update((object) adj);
          break;
        }
        num1 -= paymentBalance;
        adj.CuryAdjdAmt = new Decimal?(0M);
        adj.AdjAmt = new Decimal?(0M);
        ((PXSelectBase) extension.SOAdjustments).Cache.SetValueExt<SOAdjust.curyAdjgAmt>((object) adj, (object) 0M);
        ((PXSelectBase) extension.SOAdjustments).Cache.Update((object) adj);
      }
      else
        break;
    }
    ((PXSelectBase) extension.SOAdjustments).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) extension.SOOrder_CustomerID_OrderType_RefNbr).Cache.Persist((PXDBOperation) 1);
  }

  public delegate void PerformBasicReleaseChecksDelegate(PXGraph selectGraph, ARRegister document);

  public delegate void SetClosedPeriodsFromLatestApplicationDelegate(ARRegister doc);

  public delegate void AfterSVATConversionHistoryInsertedDelegate(
    JournalEntry je,
    ARAdjust adj,
    ARInvoice adjddoc,
    ARRegister adjgdoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT);

  public delegate SVATConversionHist ProcessLastSVATRecordDelegate(
    ARRegister adjddoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    Decimal percent);
}
