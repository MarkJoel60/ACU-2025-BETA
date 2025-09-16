// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APReleaseProcessVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class APReleaseProcessVATRecognitionOnPrepayments : PXGraphExtension<APReleaseProcess>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>();
  }

  [PXOverride]
  public virtual void AfterSVATConversionHistoryInserted(
    JournalEntry je,
    APAdjust adj,
    APInvoice adjddoc,
    APRegister adjgdoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    APReleaseProcessVATRecognitionOnPrepayments.AfterSVATConversionHistoryInsertedDelegate baseMethod)
  {
    if (adjddoc.IsPrepaymentInvoiceDocument())
    {
      bool flag = adjddoc.DrCr == "D";
      PX.Objects.TX.Tax tax = PX.Objects.TX.Tax.PK.Find((PXGraph) this.Base, adjSVAT.TaxID);
      int? nullable1;
      if (!adjgdoc.IsPrepaymentInvoiceDocumentReverse())
      {
        nullable1 = tax.OnAPPrepaymentTaxAcctID;
        if (!nullable1.HasValue)
          throw new ReleaseException("The document cannot be released because the Tax on AP Prepayment Account is not specified for the {0} tax. To proceed, specify this account on the Taxes (TX205000) form.", new object[1]
          {
            (object) tax.TaxID
          });
        if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subAccount>())
        {
          nullable1 = tax.OnAPPrepaymentTaxSubID;
          if (!nullable1.HasValue)
            throw new ReleaseException("The document cannot be released because the Tax on AP Prepayment Subaccount is not specified for the {0} tax. To proceed, specify this account on the Taxes (TX205000) form.", new object[1]
            {
              (object) tax.TaxID
            });
        }
      }
      PXCache cach1 = je.Caches[typeof (TaxTran)];
      PXCache cach2 = je.Caches[typeof (SVATTaxTran)];
      je.Views.Caches.Add(typeof (SVATTaxTran));
      SVATConversionHistExt histSVAT = Utilities.Clone<SVATConversionHist, SVATConversionHistExt>((PXGraph) this.Base, docSVAT);
      SVATTaxTran tran1 = (SVATTaxTran) ProcessSVATBase.GetSVATTaxTrans(je, histSVAT, adj.AdjdRefNbr).First<PXResult<SVATTaxTran>>();
      if (adjgdoc.IsPrepaymentInvoiceDocumentReverse())
      {
        adjSVAT.Processed = new bool?(true);
        adjSVAT = (SVATConversionHist) this.Base.SVATConversionHistory.Cache.Update((object) adjSVAT);
        Decimal multByTranType = ReportTaxProcess.GetMultByTranType("AP", adjgdoc.DocType);
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
        je.Caches[typeof (SVATTaxTran)].Update((object) tran1);
        je.Save.Press();
      }
      else
      {
        if (adjddoc.CuryID != adjgdoc.CuryID)
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) this.Base.CurrencyInfo_CuryInfoID.Select((object) adjSVAT.CuryInfoID);
          JournalEntry.SegregateBatch(je, "AP", adjSVAT.AdjdBranchID, adjddoc.CuryID, adjSVAT.AdjdDocDate, adjSVAT.AdjdFinPeriodID, adjddoc.DocDesc, currencyInfo.GetCM(), (Batch) null);
        }
        PX.Objects.GL.GLTran tran2 = new PX.Objects.GL.GLTran();
        tran2.SummPost = new bool?(this.Base.SummPost);
        tran2.BranchID = adjSVAT.AdjdBranchID;
        tran2.CuryInfoID = adjSVAT.CuryInfoID;
        tran2.TranType = adjSVAT.AdjdDocType;
        tran2.TranClass = "T";
        tran2.RefNbr = adjSVAT.AdjdRefNbr;
        tran2.TranDate = adjSVAT.AdjdDocDate;
        tran2.AccountID = flag ? tax.PurchTaxAcctID : tax.OnAPPrepaymentTaxAcctID;
        tran2.SubID = flag ? tax.PurchTaxSubID : tax.OnAPPrepaymentTaxSubID;
        tran2.TranDesc = tax.TaxID;
        tran2.CuryDebitAmt = flag ? adjSVAT.CuryTaxAmt : new Decimal?(0M);
        tran2.DebitAmt = flag ? adjSVAT.TaxAmt : new Decimal?(0M);
        tran2.CuryCreditAmt = flag ? new Decimal?(0M) : adjSVAT.CuryTaxAmt;
        tran2.CreditAmt = flag ? new Decimal?(0M) : adjSVAT.TaxAmt;
        tran2.Released = new bool?(true);
        tran2.ReferenceID = adjddoc.VendorID;
        this.Base.InsertInvoiceTaxTransaction(je, tran2, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = (APRegister) adjddoc
        });
        PX.Objects.GL.GLTran copy1 = (PX.Objects.GL.GLTran) je.GLTranModuleBatNbr.Cache.CreateCopy((object) tran2);
        copy1.AccountID = flag ? tax.OnAPPrepaymentTaxAcctID : tax.PurchTaxAcctID;
        copy1.SubID = flag ? tax.OnAPPrepaymentTaxSubID : tax.PurchTaxSubID;
        copy1.CuryDebitAmt = flag ? new Decimal?(0M) : adjSVAT.CuryTaxAmt;
        copy1.DebitAmt = flag ? new Decimal?(0M) : adjSVAT.TaxAmt;
        copy1.CuryCreditAmt = flag ? adjSVAT.CuryTaxAmt : new Decimal?(0M);
        copy1.CreditAmt = flag ? adjSVAT.TaxAmt : new Decimal?(0M);
        this.Base.InsertInvoiceTaxTransaction(je, copy1, new APReleaseProcess.GLTranInsertionContext()
        {
          APRegisterRecord = (APRegister) adjddoc
        });
        SVATTaxTran copy2 = PXCache<SVATTaxTran>.CreateCopy(tran1);
        SVATTaxTran svatTaxTran5 = copy2;
        nullable1 = new int?();
        int? nullable9 = nullable1;
        svatTaxTran5.RecordID = nullable9;
        copy2.Module = "AP";
        copy2.TaxType = "P";
        copy2.AccountID = tax.PurchTaxAcctID;
        copy2.SubID = tax.PurchTaxSubID;
        copy2.TaxInvoiceNbr = adjgdoc.RefNbr;
        copy2.TaxPeriodID = (string) null;
        copy2.TranDate = adj.AdjgDocDate;
        copy2.FinDate = new System.DateTime?();
        copy2.FinPeriodID = adj.AdjgFinPeriodID;
        Decimal num5 = ReportTaxProcess.GetMult((TaxTran) tran1) * ReportTaxProcess.GetMult((TaxTran) copy2);
        SVATTaxTran svatTaxTran6 = copy2;
        Decimal num6 = num5;
        Decimal? nullable10 = adjSVAT.CuryTaxableAmt;
        Decimal? nullable11 = nullable10.HasValue ? new Decimal?(num6 * nullable10.GetValueOrDefault()) : new Decimal?();
        svatTaxTran6.CuryTaxableAmt = nullable11;
        SVATTaxTran svatTaxTran7 = copy2;
        Decimal num7 = num5;
        nullable10 = adjSVAT.TaxableAmt;
        Decimal? nullable12 = nullable10.HasValue ? new Decimal?(num7 * nullable10.GetValueOrDefault()) : new Decimal?();
        svatTaxTran7.TaxableAmt = nullable12;
        SVATTaxTran svatTaxTran8 = copy2;
        Decimal num8 = num5;
        nullable10 = adjSVAT.CuryTaxAmt;
        Decimal? nullable13 = nullable10.HasValue ? new Decimal?(num8 * nullable10.GetValueOrDefault()) : new Decimal?();
        svatTaxTran8.CuryTaxAmt = nullable13;
        SVATTaxTran svatTaxTran9 = copy2;
        Decimal num9 = num5;
        nullable10 = adjSVAT.TaxAmt;
        Decimal? nullable14 = nullable10.HasValue ? new Decimal?(num9 * nullable10.GetValueOrDefault()) : new Decimal?();
        svatTaxTran9.TaxAmt = nullable14;
        je.Caches[typeof (SVATTaxTran)].Insert((object) copy2);
      }
    }
    else
    {
      if (!adjgdoc.IsPrepaymentInvoiceDocument())
        return;
      if (adjSVAT.ReversalMethod == "Y")
        adjSVAT.Processed = new bool?(true);
      adjSVAT = (SVATConversionHist) this.Base.SVATConversionHistory.Cache.Update((object) adjSVAT);
      SVATConversionHistExt histSVAT = Utilities.Clone<SVATConversionHist, SVATConversionHistExt>((PXGraph) this.Base, adjSVAT);
      PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax> taxTransPurchases = APReleaseProcessVATRecognitionOnPrepayments.GetSVATTaxTransPurchases(je, histSVAT, adj.AdjdRefNbr);
      if (taxTransPurchases == null)
        return;
      SVATTaxTran tran = (SVATTaxTran) taxTransPurchases;
      PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) taxTransPurchases;
      PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) taxTransPurchases;
      PX.Objects.CM.CurrencyInfo copy3 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo1);
      copy3.CuryInfoID = new long?();
      copy3.ModuleCode = "AP";
      copy3.BaseCalc = new bool?(false);
      PX.Objects.CM.CurrencyInfo currencyInfo2 = je.currencyinfo.Insert(copy3) ?? copy3;
      bool flag = ReportTaxProcess.GetMult((TaxTran) tran) == 1M;
      je.GLTranModuleBatNbr.Insert(new PX.Objects.GL.GLTran()
      {
        Module = "AP",
        BranchID = tran.BranchID,
        AccountID = tran.AccountID,
        SubID = tran.SubID,
        CuryDebitAmt = flag ? adjSVAT.CuryTaxAmt : new Decimal?(0M),
        DebitAmt = flag ? adjSVAT.TaxAmt : new Decimal?(0M),
        CuryCreditAmt = flag ? new Decimal?(0M) : adjSVAT.CuryTaxAmt,
        CreditAmt = flag ? new Decimal?(0M) : adjSVAT.TaxAmt,
        TranType = tran.TranType,
        TranClass = "N",
        RefNbr = tran.RefNbr,
        TranDesc = tax.TaxID,
        TranPeriodID = adj.AdjgTranPeriodID,
        FinPeriodID = adj.AdjgFinPeriodID,
        TranDate = tran.TaxInvoiceDate,
        CuryInfoID = currencyInfo2.CuryInfoID,
        ReferenceID = adj.VendorID,
        Released = new bool?(true)
      });
      je.GLTranModuleBatNbr.Insert(new PX.Objects.GL.GLTran()
      {
        Module = "AP",
        BranchID = tran.BranchID,
        AccountID = tax.OnAPPrepaymentTaxAcctID,
        SubID = tax.OnAPPrepaymentTaxSubID,
        CuryDebitAmt = flag ? new Decimal?(0M) : adjSVAT.CuryTaxAmt,
        DebitAmt = flag ? new Decimal?(0M) : adjSVAT.TaxAmt,
        CuryCreditAmt = flag ? adjSVAT.CuryTaxAmt : new Decimal?(0M),
        CreditAmt = flag ? adjSVAT.TaxAmt : new Decimal?(0M),
        TranType = tran.TranType,
        TranClass = "N",
        RefNbr = tran.RefNbr,
        TranDesc = tax.TaxID,
        TranPeriodID = adj.AdjgTranPeriodID,
        FinPeriodID = adj.AdjgFinPeriodID,
        TranDate = tran.TaxInvoiceDate,
        CuryInfoID = currencyInfo2.CuryInfoID,
        ReferenceID = adj.VendorID,
        Released = new bool?(true)
      });
      PXCache cach3 = je.Caches[typeof (TaxTran)];
      PXCache cach4 = je.Caches[typeof (SVATTaxTran)];
      je.Views.Caches.Add(typeof (SVATTaxTran));
      SVATTaxTran copy4 = PXCache<SVATTaxTran>.CreateCopy(tran);
      copy4.RecordID = new int?();
      copy4.Module = "AP";
      copy4.TaxType = "P";
      copy4.AccountID = tax.PurchTaxAcctID;
      copy4.SubID = tax.PurchTaxSubID;
      copy4.TaxInvoiceNbr = adjgdoc.RefNbr;
      copy4.TaxPeriodID = (string) null;
      copy4.TranDate = adj.AdjgDocDate;
      copy4.FinDate = new System.DateTime?();
      copy4.FinPeriodID = adj.AdjgFinPeriodID;
      Decimal num10 = -1M * ReportTaxProcess.GetMult((TaxTran) tran) * ReportTaxProcess.GetMult((TaxTran) copy4);
      SVATTaxTran svatTaxTran10 = copy4;
      Decimal num11 = num10;
      Decimal? nullable15 = adjSVAT.CuryTaxableAmt;
      Decimal? nullable16 = nullable15.HasValue ? new Decimal?(num11 * nullable15.GetValueOrDefault()) : new Decimal?();
      svatTaxTran10.CuryTaxableAmt = nullable16;
      SVATTaxTran svatTaxTran11 = copy4;
      Decimal num12 = num10;
      nullable15 = adjSVAT.TaxableAmt;
      Decimal? nullable17 = nullable15.HasValue ? new Decimal?(num12 * nullable15.GetValueOrDefault()) : new Decimal?();
      svatTaxTran11.TaxableAmt = nullable17;
      SVATTaxTran svatTaxTran12 = copy4;
      Decimal num13 = num10;
      nullable15 = adjSVAT.CuryTaxAmt;
      Decimal? nullable18 = nullable15.HasValue ? new Decimal?(num13 * nullable15.GetValueOrDefault()) : new Decimal?();
      svatTaxTran12.CuryTaxAmt = nullable18;
      SVATTaxTran svatTaxTran13 = copy4;
      Decimal num14 = num10;
      nullable15 = adjSVAT.TaxAmt;
      Decimal? nullable19 = nullable15.HasValue ? new Decimal?(num14 * nullable15.GetValueOrDefault()) : new Decimal?();
      svatTaxTran13.TaxAmt = nullable19;
      je.Caches[typeof (SVATTaxTran)].Insert((object) copy4);
    }
  }

  public static PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax> GetSVATTaxTransPurchases(
    JournalEntry je,
    SVATConversionHistExt histSVAT,
    string masterInvoiceNbr)
  {
    PXSelectBase<SVATTaxTran> commandForSvatTaxTrans = ProcessSVATBase.GetCommandForSVATTaxTrans(je);
    commandForSvatTaxTrans.WhereAnd<Where<SVATTaxTran.taxType, Equal<TaxType.purchase>>>();
    return commandForSvatTaxTrans.Select((object) histSVAT.Module, (object) histSVAT.VendorID, (object) histSVAT.VendorID, (object) histSVAT.AdjdDocType, (object) histSVAT.AdjdRefNbr, (object) masterInvoiceNbr, (object) histSVAT.TaxID).AsEnumerable<PXResult<SVATTaxTran>>().Cast<PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>>().FirstOrDefault<PXResult<SVATTaxTran, PX.Objects.CM.CurrencyInfo, PX.Objects.TX.Tax>>();
  }

  [PXOverride]
  public virtual SVATConversionHist ProcessLastSVATRecord(
    APRegister adjddoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    Decimal percent,
    APReleaseProcessVATRecognitionOnPrepayments.ProcessLastSVATRecordDelegate baseMethod)
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
      BqlCommand command = this.Base.SVATRecognitionRecords.View.BqlSelect;
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

  public delegate void AfterSVATConversionHistoryInsertedDelegate(
    JournalEntry je,
    APAdjust adj,
    APInvoice adjddoc,
    APRegister adjgdoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT);

  public delegate SVATConversionHist ProcessLastSVATRecordDelegate(
    APRegister adjddoc,
    SVATConversionHist docSVAT,
    SVATConversionHist adjSVAT,
    Decimal percent);
}
