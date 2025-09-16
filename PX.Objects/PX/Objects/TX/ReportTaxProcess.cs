// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ReportTaxProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.TX;

[PXHidden]
public class ReportTaxProcess : PXGraph<ReportTaxProcess>
{
  public PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<TaxHistory.branchID, Equal<Required<TaxHistory.branchID>>, And<TaxHistory.accountID, Equal<Required<TaxHistory.accountID>>, And<TaxHistory.subID, Equal<Required<TaxHistory.subID>>, And<TaxHistory.taxID, Equal<Required<TaxHistory.taxID>>, And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, And<TaxHistory.lineNbr, Equal<Required<TaxHistory.lineNbr>>, And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>>>>>> TaxHistory_Current;
  public PXSelectJoin<TaxReportLine, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>, InnerJoin<TaxTranReport, On<TaxTranReport.vendorID, Equal<TaxReportLine.vendorID>, And<TaxTranReport.taxBucketID, Equal<TaxBucketLine.bucketID>, And<TaxTranReport.revisionID, Equal<Required<TaxTranReport.revisionID>>, And<TaxTranReport.released, Equal<boolTrue>, And<TaxTranReport.voided, Equal<boolFalse>, And<TaxTranReport.taxPeriodID, Equal<Required<TaxTranReport.taxPeriodID>>, And<Where<TaxReportLine.taxZoneID, IsNull, And<TaxReportLine.tempLine, Equal<boolFalse>, Or<TaxReportLine.taxZoneID, Equal<TaxTranReport.taxZoneID>>>>>>>>>>>>>, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>>>, OrderBy<Asc<TaxReportLine.vendorID, Asc<TaxTranReport.branchID, Asc<TaxReportLine.sortOrder, Asc<TaxTranReport.accountID, Asc<TaxTranReport.subID, Asc<TaxTranReport.taxID, Asc<TaxTranReport.taxPeriodID, Asc<TaxTranReport.module, Asc<TaxTranReport.tranType, Asc<TaxTranReport.refNbr>>>>>>>>>>>> Period_Details_Expanded;
  public PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxPeriodID, Equal<Required<TaxPeriod.taxPeriodID>>>>>> TaxPeriod_Current;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>> Vendor_Current;
  public PXSelectJoin<TaxAdjustment, LeftJoin<PX.Objects.GL.Branch, On<TaxAdjustment.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<TaxPeriod, On<TaxPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>, And<TaxPeriod.vendorID, Equal<TaxAdjustment.vendorID>, And<TaxPeriod.taxPeriodID, Equal<TaxAdjustment.taxPeriod>>>>, LeftJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxAdjustment.vendorID>, And<TaxReportLine.netTax, Equal<boolTrue>, And<TaxReportLine.tempLine, Equal<boolTrue>>>>, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<TaxAdjustment.vendorID>>>>>>, Where<TaxAdjustment.docType, Equal<Required<TaxAdjustment.docType>>, And<TaxAdjustment.refNbr, Equal<Required<TaxAdjustment.refNbr>>>>> TaxAdjustment_Select;
  public PXSelectJoin<TaxTran, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxTran.vendorID>, And<TaxBucketLine.bucketID, Equal<TaxTran.taxBucketID>>>, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxReportLine.lineNbr, Equal<TaxBucketLine.lineNbr>, And<TaxReportLine.taxReportRevisionID, Equal<TaxBucketLine.taxReportRevisionID>>>>, InnerJoin<TaxReport, On<TaxReport.vendorID, Equal<TaxReportLine.vendorID>, And<TaxReport.revisionID, Equal<TaxReportLine.taxReportRevisionID>>>>>>, Where<TaxTran.tranType, Equal<Required<TaxTran.tranType>>, And<TaxTran.refNbr, Equal<Required<TaxTran.refNbr>>, And<TaxTran.tranDate, Between<TaxReport.validFrom, TaxReport.validTo>, And2<Where<TaxReportLine.taxZoneID, IsNull, Or<TaxReportLine.taxZoneID, Equal<TaxTran.taxZoneID>>>, And<TaxReportLine.tempLine, Equal<boolFalse>>>>>>> TaxTran_Select;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  protected IFinPeriodUtils FinPeriodUtils { get; set; }

  public static int? CurrentRevisionId(
    PXGraph graph,
    int? organizationID,
    int? branchID,
    int? vendorId,
    string taxPeriodID)
  {
    int?[] branchIDs = branchID.HasValue ? branchID.SingleToArray<int?>() : ReportTax.GetBranchesForProcessing(graph, organizationID, new int?(), vendorId, taxPeriodID);
    return ReportTaxProcess.CurrentRevisionId(graph, organizationID, branchIDs, vendorId, taxPeriodID);
  }

  public static int? CurrentRevisionId(
    PXGraph graph,
    int? organizationID,
    int?[] branchIDs,
    int? vendorId,
    string taxPeriodID)
  {
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArray<int?>(), branchIDs, true, true))
    {
      PXResult<TaxPeriod, TaxHistory> pxResult = (PXResult<TaxPeriod, TaxHistory>) PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelectJoin<TaxPeriod, LeftJoin<TaxHistory, On<TaxHistory.vendorID, Equal<TaxPeriod.vendorID>, And<TaxHistory.taxPeriodID, Equal<TaxPeriod.taxPeriodID>>>>, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxPeriodID, Equal<Required<TaxPeriod.taxPeriodID>>>>>, OrderBy<Desc<TaxHistory.revisionID>>>.Config>.Select(graph, new object[3]
      {
        (object) organizationID,
        (object) vendorId,
        (object) taxPeriodID
      }));
      if (pxResult == null)
        return new int?();
      TaxPeriod taxPeriod = PXResult<TaxPeriod, TaxHistory>.op_Implicit(pxResult);
      return PXResult<TaxPeriod, TaxHistory>.op_Implicit(pxResult).RevisionID ?? (taxPeriod.Status != "N" ? new int?(1) : new int?());
    }
  }

  public static int[] GetBranchesToProcess(PXGraph graph, int? organizationID, int? branchID)
  {
    if (!branchID.HasValue)
      return BranchMaint.GetChildBranches(graph, organizationID).Select<PX.Objects.GL.Branch, int>((Func<PX.Objects.GL.Branch, int>) (b => b.BranchID.Value)).ToArray<int>();
    return new int[1]{ branchID.Value };
  }

  [PXDate]
  [PXDBScalar(typeof (Search<PX.Objects.CM.CurrencyRate.curyEffDate, Where<PX.Objects.CM.CurrencyRate.fromCuryID, Equal<TaxTran.curyID>, And<PX.Objects.CM.CurrencyRate.toCuryID, Equal<CurrentValue<PX.Objects.AP.Vendor.curyID>>, And<PX.Objects.CM.CurrencyRate.curyRateType, Equal<CurrentValue<PX.Objects.AP.Vendor.curyRateTypeID>>, And<PX.Objects.CM.CurrencyRate.curyEffDate, LessEqual<TaxTran.tranDate>>>>>, OrderBy<Desc<PX.Objects.CM.CurrencyRate.curyEffDate>>>))]
  protected virtual void TaxTran_CuryEffDate_CacheAttached(PXCache sender)
  {
  }

  public static Decimal GetMult(TaxTran tran)
  {
    return ReportTaxProcess.GetMult(tran.Module, tran.TranType, tran.TaxType, new short?((short) 1));
  }

  public static Decimal GetMultByTranType(string module, string tranType)
  {
    return (!(module == "AP") || !(APDocType.TaxDrCr(tranType) == "D")) && (!(module == "AR") || !(ARDocType.TaxDrCr(tranType) == "C")) && (!(module == "GL") || !(tranType != "TRV")) && !(module == "CA") ? -1M : 1M;
  }

  public static Decimal GetMult(
    string module,
    string tranType,
    string tranTaxType,
    short? reportLineMult)
  {
    short? nullable1 = reportLineMult;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    Decimal num1;
    if (nullable2.GetValueOrDefault() != 1 || !(tranTaxType == "S"))
    {
      short? nullable3 = reportLineMult;
      int? nullable4;
      if (!nullable3.HasValue)
      {
        nullable2 = new int?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new int?((int) nullable3.GetValueOrDefault());
      nullable2 = nullable4;
      if (nullable2.GetValueOrDefault() != 1 || !(tranTaxType == "A"))
      {
        short? nullable5 = reportLineMult;
        int? nullable6;
        if (!nullable5.HasValue)
        {
          nullable2 = new int?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new int?((int) nullable5.GetValueOrDefault());
        nullable2 = nullable6;
        if (nullable2.GetValueOrDefault() != -1 || !(tranTaxType == "P"))
        {
          short? nullable7 = reportLineMult;
          int? nullable8;
          if (!nullable7.HasValue)
          {
            nullable2 = new int?();
            nullable8 = nullable2;
          }
          else
            nullable8 = new int?((int) nullable7.GetValueOrDefault());
          nullable2 = nullable8;
          if (nullable2.GetValueOrDefault() != -1 || !(tranTaxType == "B"))
          {
            num1 = -1M;
            goto label_15;
          }
        }
      }
    }
    num1 = 1M;
label_15:
    Decimal num2 = num1;
    return ReportTaxProcess.GetMultByTranType(module, tranType) * num2;
  }

  private void SegregateBatch(
    JournalEntry je,
    int? BranchID,
    string CuryID,
    DateTime? DocDate,
    string FinPeriodID,
    string description)
  {
    Batch current = ((PXSelectBase<Batch>) je.BatchModule).Current;
    if (current != null && object.Equals((object) current.BranchID, (object) BranchID) && object.Equals((object) current.CuryID, (object) CuryID) && object.Equals((object) current.FinPeriodID, (object) FinPeriodID))
      return;
    ((PXGraph) je).Clear();
    PX.Objects.CM.CurrencyInfo currencyInfo1 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(new PX.Objects.CM.CurrencyInfo()
    {
      CuryID = CuryID,
      CuryEffDate = DocDate
    });
    ((PXSelectBase<Batch>) je.BatchModule).Insert(new Batch()
    {
      BranchID = BranchID,
      Module = "GL",
      Status = "U",
      Released = new bool?(true),
      Hold = new bool?(false),
      DateEntered = DocDate,
      FinPeriodID = FinPeriodID,
      TranPeriodID = FinPeriodID,
      CuryID = CuryID,
      CuryInfoID = currencyInfo1.CuryInfoID,
      Description = description
    });
    PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<Batch.curyInfoID>>>>.Config>.Select((PXGraph) je, (object[]) null));
    currencyInfo2.CuryID = CuryID;
    currencyInfo2.CuryEffDate = DocDate;
    ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Update(currencyInfo2);
  }

  private void UpdateHistory(TaxTran tran, TaxReportLine line)
  {
    TaxHistory taxHistory1 = PXResultset<TaxHistory>.op_Implicit(((PXSelectBase<TaxHistory>) this.TaxHistory_Current).Select(new object[8]
    {
      (object) tran.VendorID,
      (object) tran.BranchID,
      (object) tran.AccountID,
      (object) tran.SubID,
      (object) tran.TaxID,
      (object) tran.TaxPeriodID,
      (object) line.LineNbr,
      (object) tran.RevisionID
    }));
    if (taxHistory1 == null)
      taxHistory1 = (TaxHistory) ((PXSelectBase) this.TaxHistory_Current).Cache.Insert((object) new TaxHistory()
      {
        RevisionID = tran.RevisionID,
        VendorID = tran.VendorID,
        BranchID = tran.BranchID,
        AccountID = tran.AccountID,
        SubID = tran.SubID,
        TaxID = tran.TaxID,
        TaxPeriodID = tran.TaxPeriodID,
        LineNbr = line.LineNbr,
        TaxReportRevisionID = line.TaxReportRevisionID
      });
    Decimal mult = ReportTaxProcess.GetMult(tran.Module, tran.TranType, tran.TaxType, line.LineMult);
    switch (line.LineType)
    {
      case "P":
        TaxHistory taxHistory2 = taxHistory1;
        Decimal? nullable1 = taxHistory2.ReportFiledAmt;
        Decimal num1 = mult * tran.ReportTaxAmt.GetValueOrDefault();
        taxHistory2.ReportFiledAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
        TaxHistory taxHistory3 = taxHistory1;
        nullable1 = taxHistory3.FiledAmt;
        Decimal num2 = mult;
        Decimal? nullable2 = tran.TaxAmt;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        Decimal num3 = num2 * valueOrDefault1;
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num3);
        taxHistory3.FiledAmt = nullable3;
        break;
      case "A":
        TaxHistory taxHistory4 = taxHistory1;
        Decimal? nullable4 = taxHistory4.ReportFiledAmt;
        Decimal num4 = mult * tran.ReportTaxableAmt.GetValueOrDefault();
        taxHistory4.ReportFiledAmt = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num4) : new Decimal?();
        TaxHistory taxHistory5 = taxHistory1;
        nullable4 = taxHistory5.FiledAmt;
        Decimal num5 = mult;
        Decimal? nullable5 = tran.TaxableAmt;
        Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
        Decimal num6 = num5 * valueOrDefault2;
        Decimal? nullable6;
        if (!nullable4.HasValue)
        {
          nullable5 = new Decimal?();
          nullable6 = nullable5;
        }
        else
          nullable6 = new Decimal?(nullable4.GetValueOrDefault() + num6);
        taxHistory5.FiledAmt = nullable6;
        break;
      case "E":
        TaxHistory taxHistory6 = taxHistory1;
        Decimal? nullable7 = taxHistory6.ReportFiledAmt;
        Decimal num7 = mult * tran.ReportExemptedAmt.GetValueOrDefault();
        taxHistory6.ReportFiledAmt = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + num7) : new Decimal?();
        TaxHistory taxHistory7 = taxHistory1;
        nullable7 = taxHistory7.FiledAmt;
        Decimal num8 = mult;
        Decimal? nullable8 = tran.ExemptedAmt;
        Decimal valueOrDefault3 = nullable8.GetValueOrDefault();
        Decimal num9 = num8 * valueOrDefault3;
        Decimal? nullable9;
        if (!nullable7.HasValue)
        {
          nullable8 = new Decimal?();
          nullable9 = nullable8;
        }
        else
          nullable9 = new Decimal?(nullable7.GetValueOrDefault() + num9);
        taxHistory7.FiledAmt = nullable9;
        break;
    }
    ((PXSelectBase) this.TaxHistory_Current).Cache.Update((object) taxHistory1);
  }

  public virtual void ReleaseDocProc(JournalEntry je, TaxAdjustment doc)
  {
    if (doc.Hold.GetValueOrDefault())
      throw new PXException("Document is On Hold and cannot be released.");
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        int? vendorId = doc.VendorID;
        TaxHistory current = ((PXSelectBase<TaxHistory>) this.TaxHistory_Current).Current;
        int taxReportRevisionID = (current != null ? (!current.TaxReportRevisionID.HasValue ? 1 : 0) : 1) != 0 ? 0 : ((PXSelectBase<TaxHistory>) this.TaxHistory_Current).Current.TaxReportRevisionID.Value;
        RoundingManager rmanager = new RoundingManager((PXGraph) this, vendorId, taxReportRevisionID);
        PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, PXAccess.GetParentOrganizationID(doc.BranchID));
        int? branchID = organizationById.FileTaxesByBranches.GetValueOrDefault() ? doc.BranchID : new int?();
        int? revisionId = new int?();
        if (doc.TaxPeriod != null)
          revisionId = new int?(ReportTaxProcess.CurrentRevisionId((PXGraph) this, organizationById.OrganizationID, branchID, doc.VendorID, doc.TaxPeriod) ?? 1);
        foreach (PXResult<TaxAdjustment, PX.Objects.GL.Branch, TaxPeriod, TaxReportLine, PX.Objects.AP.Vendor> pxResult in ((PXSelectBase<TaxAdjustment>) this.TaxAdjustment_Select).Select(new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        }))
        {
          TaxAdjustment taxAdjustment = PXResult<TaxAdjustment, PX.Objects.GL.Branch, TaxPeriod, TaxReportLine, PX.Objects.AP.Vendor>.op_Implicit(pxResult);
          TaxPeriod taxPeriod = PXResult<TaxAdjustment, PX.Objects.GL.Branch, TaxPeriod, TaxReportLine, PX.Objects.AP.Vendor>.op_Implicit(pxResult);
          TaxReportLine taxReportLine = PXResult<TaxAdjustment, PX.Objects.GL.Branch, TaxPeriod, TaxReportLine, PX.Objects.AP.Vendor>.op_Implicit(pxResult);
          if (taxPeriod.TaxPeriodID != null)
          {
            if (taxPeriod.Status == "C" && taxReportLine.NetTax.HasValue)
              throw new PXException("You can adjust a tax report for a tax period with the Open or Prepared status only.");
            ((PXGraph) this).Caches[typeof (TaxPeriod)].SetStatus((object) taxPeriod, (PXEntryStatus) 1);
          }
          this.SegregateBatch(je, taxAdjustment.BranchID, taxAdjustment.CuryID, taxAdjustment.DocDate, taxAdjustment.FinPeriodID, taxAdjustment.DocDesc);
          bool flag = (doc.DocType == "INT" ? 1 : -1) * Math.Sign(taxAdjustment.OrigDocAmt.GetValueOrDefault()) > 0;
          PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran()
          {
            AccountID = taxAdjustment.AdjAccountID,
            SubID = taxAdjustment.AdjSubID,
            CuryDebitAmt = new Decimal?(flag ? Math.Abs(taxAdjustment.CuryOrigDocAmt.GetValueOrDefault()) : 0M),
            DebitAmt = new Decimal?(flag ? Math.Abs(taxAdjustment.OrigDocAmt.GetValueOrDefault()) : 0M),
            CuryCreditAmt = new Decimal?(flag ? 0M : Math.Abs(taxAdjustment.CuryOrigDocAmt.GetValueOrDefault())),
            CreditAmt = new Decimal?(flag ? 0M : Math.Abs(taxAdjustment.OrigDocAmt.GetValueOrDefault())),
            TranType = taxAdjustment.DocType,
            TranClass = "N",
            RefNbr = taxAdjustment.RefNbr,
            TranDesc = taxAdjustment.DocDesc,
            TranPeriodID = taxAdjustment.FinPeriodID,
            FinPeriodID = taxAdjustment.FinPeriodID,
            TranDate = taxAdjustment.DocDate,
            CuryInfoID = ((PXSelectBase<Batch>) je.BatchModule).Current.CuryInfoID,
            Released = new bool?(true)
          };
          ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
        }
        TaxTran objB = (TaxTran) null;
        foreach (PXResult<TaxTran, TaxBucketLine, TaxReportLine> pxResult in ((PXSelectBase<TaxTran>) this.TaxTran_Select).Select(new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        }))
        {
          TaxTran taxTran = PXResult<TaxTran, TaxBucketLine, TaxReportLine>.op_Implicit(pxResult);
          TaxReportLine line = PXResult<TaxTran, TaxBucketLine, TaxReportLine>.op_Implicit(pxResult);
          bool flag = (doc.DocType == "INT" ? 1 : -1) * Math.Sign(taxTran.TaxAmt.GetValueOrDefault()) > 0;
          if (!object.Equals((object) taxTran, (object) objB))
          {
            PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran()
            {
              AccountID = taxTran.AccountID,
              SubID = taxTran.SubID,
              CuryDebitAmt = new Decimal?(flag ? 0M : Math.Abs(taxTran.CuryTaxAmt.GetValueOrDefault())),
              DebitAmt = new Decimal?(flag ? 0M : Math.Abs(taxTran.TaxAmt.GetValueOrDefault())),
              CuryCreditAmt = new Decimal?(flag ? Math.Abs(taxTran.CuryTaxAmt.GetValueOrDefault()) : 0M),
              CreditAmt = new Decimal?(flag ? Math.Abs(taxTran.TaxAmt.GetValueOrDefault()) : 0M),
              TranType = doc.DocType,
              TranClass = "N",
              RefNbr = doc.RefNbr,
              TranDesc = taxTran.Description,
              TranPeriodID = doc.FinPeriodID,
              FinPeriodID = doc.FinPeriodID,
              TranDate = doc.DocDate,
              CuryInfoID = ((PXSelectBase<Batch>) je.BatchModule).Current.CuryInfoID,
              Released = new bool?(true)
            };
            ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
            taxTran.Released = new bool?(true);
            if (doc.TaxPeriod != null)
              taxTran.RevisionID = revisionId;
            ((PXSelectBase) this.TaxTran_Select).Cache.Update((object) taxTran);
          }
          objB = taxTran;
          if (doc.TaxPeriod != null)
            this.UpdateHistory(taxTran, line);
        }
        ((PXAction) je.Save).Press();
        doc.Released = new bool?(true);
        doc.BatchNbr = ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr;
        doc = ((PXSelectBase<TaxAdjustment>) this.TaxAdjustment_Select).Update(doc);
        if (doc.TaxPeriod != null)
        {
          foreach (PXResult<TaxHistory> pxResult in PXSelectBase<TaxHistory, PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>, And<TaxHistory.taxID, Equal<StringEmpty>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) doc.VendorID,
            (object) doc.TaxPeriod,
            (object) revisionId
          }))
            ((PXSelectBase) this.TaxHistory_Current).Cache.Delete((object) PXResult<TaxHistory>.op_Implicit(pxResult));
        }
        ((PXGraph) this).Persist();
        if (doc.TaxPeriod != null)
          TaxHistorySumManager.UpdateTaxHistorySums((PXGraph) this, rmanager, doc.TaxPeriod, revisionId, new int?(), doc.BranchID, (Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool>) (line => this.ShowTaxReportLine(PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>.op_Implicit(line), doc.TaxPeriod)));
        transactionScope.Complete((PXGraph) this);
      }
      ((PXSelectBase) this.TaxAdjustment_Select).Cache.Persisted(false);
      ((PXSelectBase) this.TaxTran_Select).Cache.Persisted(false);
      ((PXSelectBase) this.TaxHistory_Current).Cache.Persisted(false);
    }
  }

  public virtual void VoidReportProc(TaxPeriodFilter taxPeriod)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing((PXGraph) this, taxPeriod.OrganizationID, taxPeriod.BranchID, taxPeriod.VendorID, taxPeriod.TaxPeriodID);
        using (new PXReadBranchRestrictedScope(taxPeriod.OrganizationID.SingleToArray<int?>(), branchesForProcessing, true, true))
        {
          PXUpdateJoin<Set<TaxAdjustment.taxPeriod, Null>, TaxAdjustment, InnerJoin<TaxTran, On<TaxAdjustment.docType, Equal<TaxTran.tranType>, And<TaxAdjustment.refNbr, Equal<TaxTran.refNbr>>>>, Where<TaxAdjustment.vendorID, Equal<Required<TaxAdjustment.vendorID>>, And<TaxAdjustment.taxPeriod, Equal<Required<TaxAdjustment.taxPeriod>>, And<TaxTran.revisionID, Equal<Required<TaxTran.revisionID>>, And<TaxAdjustment.released, Equal<True>, And<TaxTran.voided, Equal<False>>>>>>>.Update((PXGraph) this, new object[3]
          {
            (object) taxPeriod.VendorID,
            (object) taxPeriod.TaxPeriodID,
            (object) taxPeriod.RevisionId
          });
          PXUpdate<Set<TaxTran.taxPeriodID, Null, Set<TaxTran.revisionID, Null>>, TaxTran, Where<TaxTran.vendorID, Equal<Required<TaxTran.vendorID>>, And<TaxTran.taxPeriodID, Equal<Required<TaxTran.taxPeriodID>>, And<TaxTran.revisionID, Equal<Required<TaxTran.revisionID>>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>>>>>>>.Update((PXGraph) this, new object[3]
          {
            (object) taxPeriod.VendorID,
            (object) taxPeriod.TaxPeriodID,
            (object) taxPeriod.RevisionId
          });
          foreach (PXResult<TaxHistory> pxResult in PXSelectBase<TaxHistory, PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) taxPeriod.VendorID,
            (object) taxPeriod.TaxPeriodID,
            (object) taxPeriod.RevisionId
          }))
            ((PXSelectBase) this.TaxHistory_Current).Cache.Delete((object) PXResult<TaxHistory>.op_Implicit(pxResult));
        }
        ((PXSelectBase) this.TaxHistory_Current).Cache.Persist((PXDBOperation) 3);
        ((PXSelectBase) this.TaxHistory_Current).Cache.Persisted(false);
        bool flag1;
        using (new PXReadBranchRestrictedScope(taxPeriod.OrganizationID.SingleToArray<int?>(), branchesForProcessing, true, true))
          flag1 = PXResultset<TaxHistory>.op_Implicit(PXSelectBase<TaxHistory, PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
          {
            (object) taxPeriod.VendorID,
            (object) taxPeriod.TaxPeriodID,
            (object) taxPeriod.RevisionId
          })) == null;
        if (flag1)
        {
          foreach (PXResult<TaxPeriod> pxResult in ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).Select(new object[3]
          {
            (object) taxPeriod.OrganizationID,
            (object) taxPeriod.VendorID,
            (object) taxPeriod.TaxPeriodID
          }))
          {
            TaxPeriod taxPeriod1 = PXResult<TaxPeriod>.op_Implicit(pxResult);
            TaxPeriod taxPeriod2 = !(taxPeriod1.Status != "P") ? taxPeriod1 : throw new PXException();
            int? revisionId = taxPeriod.RevisionId;
            int num = 1;
            string str = revisionId.GetValueOrDefault() > num & revisionId.HasValue ? "C" : "N";
            taxPeriod2.Status = str;
            ((PXSelectBase) this.TaxPeriod_Current).Cache.Update((object) taxPeriod1);
          }
          ((PXSelectBase) this.TaxPeriod_Current).Cache.Persist((PXDBOperation) 1);
          ((PXSelectBase) this.TaxPeriod_Current).Cache.Persisted(false);
          bool flag2;
          using (new PXReadBranchRestrictedScope(taxPeriod.OrganizationID.SingleToArray<int?>(), branchesForProcessing, true, true))
            flag2 = PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelectReadonly2<TaxPeriod, InnerJoin<TaxHistory, On<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<TaxHistory.vendorID>, And<TaxPeriod.taxPeriodID, Equal<TaxHistory.taxPeriodID>>>>>, Where<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxYear, Equal<Required<TaxPeriod.taxYear>>>>>.Config>.Select((PXGraph) this, new object[3]
            {
              (object) taxPeriod.OrganizationID,
              (object) taxPeriod.VendorID,
              (object) taxPeriod.TaxPeriodID.Substring(0, 4)
            })) == null;
          if (flag2)
            PXDatabase.Delete<TaxPeriod>(new PXDataFieldRestrict[3]
            {
              (PXDataFieldRestrict) new PXDataFieldRestrict<TaxPeriod.organizationID>((object) taxPeriod.OrganizationID),
              (PXDataFieldRestrict) new PXDataFieldRestrict<TaxPeriod.vendorID>((object) taxPeriod.VendorID),
              (PXDataFieldRestrict) new PXDataFieldRestrict<TaxPeriod.taxYear>((object) taxPeriod.TaxPeriodID.Substring(0, 4))
            });
        }
        transactionScope.Complete((PXGraph) this);
      }
    }
  }

  public virtual void ClosePeriodProc(TaxPeriodFilter p)
  {
    List<PX.Objects.AP.APRegister> list = new List<PX.Objects.AP.APRegister>();
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing((PXGraph) this, p.OrganizationID, new int?(), p.VendorID, p.TaxPeriodID);
        this.CheckNetTaxReportLinesInMigrationMode(p.VendorID);
        this.CheckUnreleasedTaxAdjustmentsDoNotExist(p.TaxPeriodID, p.OrganizationID, branchesForProcessing, p.VendorID);
        bool arPPDExist;
        bool apPPDExist;
        ReportTaxProcess.CheckForUnprocessedPPD((PXGraph) this, p.OrganizationID, branchesForProcessing, p.VendorID, p.EndDate, out arPPDExist, out apPPDExist);
        string str1 = string.Empty + (arPPDExist ? PXMessages.Localize("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AR Tax Adjustments (AR504500) form and appropriate VAT credit memos are released on the Release AR Documents (AR501000) form.") : string.Empty) + (apPPDExist ? " " + PXMessages.Localize("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AP Tax Adjustments (AP504500) form and appropriate VAT debit adjustments are released on the Release AP Documents (AP501000) form.") : string.Empty);
        if (!string.IsNullOrEmpty(str1))
          throw new PXSetPropertyException(str1, (PXErrorLevel) 4);
        TaxPeriod taxPeriod = PXResultset<TaxPeriod>.op_Implicit(((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).Select(new object[3]
        {
          (object) p.OrganizationID,
          (object) p.VendorID,
          (object) p.TaxPeriodID
        }));
        taxPeriod.Status = !(taxPeriod?.Status != "P") ? "C" : throw new PXException("Cannot close tax report for Closed or Open period.");
        ((PXSelectBase) this.TaxPeriod_Current).Cache.Update((object) taxPeriod);
        PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor_Current).Select(new object[1]
        {
          (object) p.VendorID
        }));
        DateTime dateTime = taxPeriod.EndDate.Value.AddDays(-1.0);
        this.VerifyTaxConfigurationErrors(p, taxPeriod, vendor).RaiseIfHasError();
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        instance.Approval.SuppressApproval = true;
        Dictionary<int?, KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>>> dictionary1 = new Dictionary<int?, KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>>>();
        PXResult<TaxHistory, TaxReportLine>[] array;
        using (new PXReadBranchRestrictedScope(p.OrganizationID.SingleToArray<int?>(), branchesForProcessing, true, true))
          array = ((IEnumerable<PXResult<TaxHistory>>) PXSelectBase<TaxHistory, PXSelectJoinGroupBy<TaxHistory, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxHistory.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxHistory.taxReportRevisionID>, And<TaxReportLine.lineNbr, Equal<TaxHistory.lineNbr>>>>>, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>, And<TaxHistory.taxReportRevisionID, Equal<Required<TaxHistory.taxReportRevisionID>>>>>>, Aggregate<GroupBy<TaxHistory.vendorID, GroupBy<TaxHistory.branchID, GroupBy<TaxHistory.lineNbr, GroupBy<TaxHistory.accountID, GroupBy<TaxHistory.subID, GroupBy<TaxHistory.taxID, GroupBy<TaxReportLine.netTax, Sum<TaxHistory.filedAmt>>>>>>>>>>.Config>.Select((PXGraph) instance, new object[4]
          {
            (object) p.VendorID,
            (object) p.TaxPeriodID,
            (object) p.RevisionId,
            (object) p.TaxReportRevisionID
          })).AsEnumerable<PXResult<TaxHistory>>().Cast<PXResult<TaxHistory, TaxReportLine>>().ToArray<PXResult<TaxHistory, TaxReportLine>>();
        Decimal? nullable1;
        foreach (PXResult<TaxHistory, TaxReportLine> pxResult in array)
        {
          TaxHistory taxHistory = PXResult<TaxHistory, TaxReportLine>.op_Implicit(pxResult);
          TaxReportLine taxReportLine = PXResult<TaxHistory, TaxReportLine>.op_Implicit(pxResult);
          bool? nullable2 = taxReportLine.NetTax;
          if (nullable2.GetValueOrDefault() && taxHistory.BranchID.HasValue)
          {
            nullable2 = vendor.AutoGenerateTaxBill;
            if (nullable2.GetValueOrDefault())
            {
              KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>> keyValuePair1;
              if (!dictionary1.TryGetValue(taxHistory.BranchID, out keyValuePair1))
              {
                Dictionary<int?, KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>>> dictionary2 = dictionary1;
                int? branchId = taxHistory.BranchID;
                keyValuePair1 = new KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>>(new PX.Objects.AP.APInvoice(), new List<APTran>());
                KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>> keyValuePair2 = keyValuePair1;
                dictionary2[branchId] = keyValuePair2;
                keyValuePair1.Key.CuryLineTotal = new Decimal?(0M);
                keyValuePair1.Key.LineTotal = new Decimal?(0M);
              }
              APTran apTran = new APTran()
              {
                BranchID = taxHistory.BranchID,
                AccountID = taxHistory.AccountID,
                SubID = taxHistory.SubID,
                LineAmt = taxHistory.FiledAmt,
                CuryLineAmt = taxHistory.ReportFiledAmt,
                TranAmt = taxHistory.FiledAmt,
                CuryTranAmt = taxHistory.ReportFiledAmt,
                TranDesc = string.IsNullOrEmpty(taxReportLine.TaxZoneID) ? $"{taxHistory.TaxID}" : $"{taxHistory.TaxID},{taxReportLine.TaxZoneID}"
              };
              keyValuePair1.Value.Add(apTran);
              PX.Objects.AP.APInvoice key1 = keyValuePair1.Key;
              nullable1 = key1.CuryLineTotal;
              Decimal valueOrDefault1 = apTran.CuryLineAmt.GetValueOrDefault();
              key1.CuryLineTotal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
              PX.Objects.AP.APInvoice key2 = keyValuePair1.Key;
              nullable1 = key2.LineTotal;
              Decimal valueOrDefault2 = apTran.LineAmt.GetValueOrDefault();
              key2.LineTotal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
            }
          }
        }
        if (dictionary1.Count > 0)
        {
          foreach (KeyValuePair<int?, KeyValuePair<PX.Objects.AP.APInvoice, List<APTran>>> keyValuePair in dictionary1)
          {
            int? key = keyValuePair.Key;
            Decimal? lineTotal = keyValuePair.Value.Key.LineTotal;
            Decimal? curyLineTotal = keyValuePair.Value.Key.CuryLineTotal;
            ((PXGraph) instance).Clear();
            ((PXSelectBase<PX.Objects.AP.Vendor>) instance.vendor).Current = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) taxPeriod.VendorID
            }));
            PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
            if (this.FinPeriodRepository.FindFinPeriodByDate(new DateTime?(dateTime), p.OrganizationID) == null)
              throw new PXException("The tax report cannot be released, and the tax period cannot be closed because the financial period related to the {0} end date and {2} tax period is not defined for the {1} company. To proceed, create and activate the necessary financial periods on the Company Financial Calendar (GL201010) form.", new object[3]
              {
                (object) dateTime.ToShortDateString(),
                (object) PXAccess.GetOrganizationCD(p.OrganizationID),
                (object) PeriodIDAttribute.FormatForError(taxPeriod.TaxPeriodID)
              });
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = new PX.Objects.CM.Extensions.CurrencyInfo();
            if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
            {
              currencyInfo1.CuryID = ((PXSelectBase<PX.Objects.AP.Vendor>) instance.vendor).Current.CuryID;
              if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
              {
                currencyInfo1.BaseCuryID = PXOrgAccess.GetBaseCuryID(p.OrgBAccountID);
                currencyInfo1.BaseCalc = new bool?(object.Equals((object) currencyInfo1.CuryID, (object) currencyInfo1.BaseCuryID));
              }
              else
              {
                Company company = PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this, Array.Empty<object>()));
                currencyInfo1.BaseCalc = new bool?(object.Equals((object) currencyInfo1.CuryID, (object) company.BaseCuryID));
              }
              currencyInfo1.ModuleCode = "AP";
              if (!string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) instance.vendor).Current.CuryRateTypeID))
                currencyInfo1.CuryRateTypeID = ((PXSelectBase<PX.Objects.AP.Vendor>) instance.vendor).Current.CuryRateTypeID;
            }
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo).Insert(currencyInfo1);
            PX.Objects.AP.APInvoice apInvoice1 = new PX.Objects.AP.APInvoice();
            nullable1 = curyLineTotal;
            Decimal num1 = 0M;
            Decimal num2 = nullable1.GetValueOrDefault() >= num1 & nullable1.HasValue ? 1M : -1M;
            PX.Objects.AP.APInvoice apInvoice2 = apInvoice1;
            nullable1 = curyLineTotal;
            Decimal num3 = 0M;
            string str2 = nullable1.GetValueOrDefault() >= num3 & nullable1.HasValue ? "INV" : "ADR";
            apInvoice2.DocType = str2;
            apInvoice1.CuryInfoID = currencyInfo2.CuryInfoID;
            PX.Objects.AP.APInvoice apInvoice3 = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Insert(apInvoice1);
            apInvoice3.TaxCalcMode = "T";
            apInvoice3.VendorID = taxPeriod.VendorID;
            PX.Objects.AP.APInvoice row = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Update(apInvoice3);
            row.BranchID = key;
            row.FinPeriodID = ((PXSelectBase<PX.Objects.AP.Vendor>) instance.vendor).Current.TaxPeriodType == "F" ? taxPeriod.TaxPeriodID : this.FinPeriodRepository.FindFinPeriodByDate(new DateTime?(dateTime), p.OrganizationID)?.FinPeriodID;
            this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<PX.Objects.AP.APInvoice.finPeriodID, PX.Objects.AP.APInvoice.branchID>(((PXSelectBase) instance.Document).Cache, (object) row, (PXSelectBase<OrganizationFinPeriod>) instance.finperiod, typeof (OrganizationFinPeriod.aPClosed));
            FinPeriod byId = this.FinPeriodRepository.GetByID(row.FinPeriodID, p.OrganizationID);
            row.DocDate = new DateTime?(byId.EndDate.Value.AddDays(-1.0));
            row.Released = new bool?(false);
            row.Hold = new bool?(false);
            row.CuryID = currencyInfo2.CuryID;
            row.CuryInfoID = currencyInfo2.CuryInfoID;
            row.IsTaxDocument = new bool?(true);
            row.TaxZoneID = (string) null;
            PX.Objects.AP.APInvoice apInvoice4 = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Update(row);
            ((PXSelectBase<APSetup>) instance.APSetup).Current.RequireControlTotal = new bool?(false);
            ((PXSelectBase<APSetup>) instance.APSetup).Current.RequireControlTaxTotal = new bool?(false);
            foreach (APTran apTran1 in keyValuePair.Value.Value)
            {
              APTran apTran2 = apTran1;
              Decimal num4 = num2;
              nullable1 = apTran1.LineAmt;
              Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num4 * nullable1.GetValueOrDefault()) : new Decimal?();
              apTran2.LineAmt = nullable3;
              APTran apTran3 = apTran1;
              Decimal num5 = num2;
              nullable1 = apTran1.CuryLineAmt;
              Decimal? nullable4 = nullable1.HasValue ? new Decimal?(num5 * nullable1.GetValueOrDefault()) : new Decimal?();
              apTran3.CuryLineAmt = nullable4;
              APTran apTran4 = apTran1;
              Decimal num6 = num2;
              nullable1 = apTran1.TranAmt;
              Decimal? nullable5 = nullable1.HasValue ? new Decimal?(num6 * nullable1.GetValueOrDefault()) : new Decimal?();
              apTran4.TranAmt = nullable5;
              APTran apTran5 = apTran1;
              Decimal num7 = num2;
              nullable1 = apTran1.CuryTranAmt;
              Decimal? nullable6 = nullable1.HasValue ? new Decimal?(num7 * nullable1.GetValueOrDefault()) : new Decimal?();
              apTran5.CuryTranAmt = nullable6;
              ((PXSelectBase<APTran>) instance.Transactions).Insert(apTran1);
            }
            PX.Objects.AP.APInvoice apInvoice5 = apInvoice4;
            Decimal num8 = num2;
            nullable1 = lineTotal;
            Decimal? nullable7 = nullable1.HasValue ? new Decimal?(num8 * nullable1.GetValueOrDefault()) : new Decimal?();
            apInvoice5.LineTotal = nullable7;
            PX.Objects.AP.APInvoice apInvoice6 = apInvoice4;
            Decimal num9 = num2;
            nullable1 = curyLineTotal;
            Decimal? nullable8 = nullable1.HasValue ? new Decimal?(num9 * nullable1.GetValueOrDefault()) : new Decimal?();
            apInvoice6.CuryLineTotal = nullable8;
            PX.Objects.AP.APInvoice apInvoice7 = apInvoice4;
            Decimal num10 = num2;
            nullable1 = lineTotal;
            Decimal? nullable9 = nullable1.HasValue ? new Decimal?(num10 * nullable1.GetValueOrDefault()) : new Decimal?();
            apInvoice7.OrigDocAmt = nullable9;
            PX.Objects.AP.APInvoice apInvoice8 = apInvoice4;
            Decimal num11 = num2;
            nullable1 = curyLineTotal;
            Decimal? nullable10 = nullable1.HasValue ? new Decimal?(num11 * nullable1.GetValueOrDefault()) : new Decimal?();
            apInvoice8.CuryOrigDocAmt = nullable10;
            PX.Objects.AP.APInvoice apInvoice9 = apInvoice4;
            Decimal num12 = num2;
            nullable1 = lineTotal;
            Decimal? nullable11 = nullable1.HasValue ? new Decimal?(num12 * nullable1.GetValueOrDefault()) : new Decimal?();
            apInvoice9.DocBal = nullable11;
            PX.Objects.AP.APInvoice apInvoice10 = apInvoice4;
            Decimal num13 = num2;
            nullable1 = curyLineTotal;
            Decimal? nullable12 = nullable1.HasValue ? new Decimal?(num13 * nullable1.GetValueOrDefault()) : new Decimal?();
            apInvoice10.CuryDocBal = nullable12;
            currencyInfo2.CuryMultDiv = "M";
            nullable1 = apInvoice4.CuryOrigDocAmt;
            Decimal num14 = 0M;
            if (!(nullable1.GetValueOrDefault() == num14 & nullable1.HasValue))
            {
              nullable1 = apInvoice4.OrigDocAmt;
              Decimal num15 = 0M;
              if (!(nullable1.GetValueOrDefault() == num15 & nullable1.HasValue))
              {
                PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
                nullable1 = apInvoice4.OrigDocAmt;
                Decimal num16 = nullable1.Value;
                nullable1 = apInvoice4.CuryOrigDocAmt;
                Decimal num17 = nullable1.Value;
                Decimal? nullable13 = new Decimal?(Math.Round(num16 / num17, 8));
                currencyInfo3.CuryRate = nullable13;
                PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo2;
                nullable1 = apInvoice4.CuryOrigDocAmt;
                Decimal num18 = nullable1.Value;
                nullable1 = apInvoice4.OrigDocAmt;
                Decimal num19 = nullable1.Value;
                Decimal? nullable14 = new Decimal?(Math.Round(num18 / num19, 8));
                currencyInfo4.RecipRate = nullable14;
                goto label_39;
              }
            }
            currencyInfo2.CuryRate = new Decimal?((Decimal) 1);
            currencyInfo2.RecipRate = new Decimal?((Decimal) 1);
label_39:
            ((PXAction) instance.Save).Press();
            list.Add((PX.Objects.AP.APRegister) ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current);
          }
        }
        ((PXSelectBase) this.TaxPeriod_Current).Cache.Persist((PXDBOperation) 1);
        ((PXSelectBase) this.TaxPeriod_Current).Cache.Persisted(false);
        transactionScope.Complete((PXGraph) this);
      }
    }
    APDocumentRelease.ReleaseDoc(list, false);
  }

  private void CheckNetTaxReportLinesInMigrationMode(int? vendorID)
  {
    if (((PXSelectBase<TaxReportLine>) new PXSelectJoin<TaxReportLine, CrossJoin<APSetup>, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.netTax, Equal<True>, And<APSetup.migrationMode, Equal<True>>>>>((PXGraph) this)).Any<TaxReportLine>((object) vendorID))
      throw new PXException("The tax report cannot be released and the tax period cannot be closed because Net Tax lines are configured for the tax agency and migration mode is activated in the Accounts Payable module.");
  }

  private void CheckUnreleasedTaxAdjustmentsDoNotExist(
    string taxPeriodID,
    int? organizationID,
    int?[] branchIDs,
    int? taxAgencyID)
  {
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArray<int?>(), branchIDs, true, true))
    {
      if (PXResultset<TaxAdjustment>.op_Implicit(PXSelectBase<TaxAdjustment, PXSelect<TaxAdjustment, Where<TaxAdjustment.taxPeriod, Equal<Required<TaxAdjustment.taxPeriod>>, And<TaxAdjustment.released, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) taxPeriodID
      })) != null)
        throw new PXException("The tax report cannot be released and the tax period cannot be closed because unreleased tax adjustments exist in the selected tax period.");
    }
  }

  protected virtual ProcessingResult VerifyTaxConfigurationErrors(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor)
  {
    HashSet<int> col = new HashSet<int>();
    IEnumerable<int?> source1 = (IEnumerable<int?>) null;
    if (filter.OrganizationID.HasValue)
    {
      col.Add<int>((IEnumerable<int>) PXAccess.GetChildBranchIDs(filter.OrganizationID, false));
      source1 = (IEnumerable<int?>) ((IQueryable<PXResult<TaxHistory>>) PXSelectBase<TaxHistory, PXSelectGroupBy<TaxHistory, Where<TaxHistory.branchID, In<Required<TaxHistory.branchID>>>, Aggregate<GroupBy<TaxHistory.branchID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) col
      })).Select<PXResult<TaxHistory>, int?>((Expression<Func<PXResult<TaxHistory>, int?>>) (t => ((TaxHistory) t).BranchID));
    }
    using (new PXReadBranchRestrictedScope(filter.OrganizationID.SingleToArray<int?>(), source1.ToArray<int?>(), true, true))
    {
      Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
      HashSet<string> source2 = new HashSet<string>();
      ProcessingResult processingResult = new ProcessingResult();
      foreach (PXResult<PX.Objects.GL.Branch, TaxHistory, TaxTranReport, TaxBucketLine> pxResult in (PXResultset<PX.Objects.GL.Branch>) this.SelectTaxTransForVerifying(filter, taxPeriod, vendor))
      {
        PX.Objects.GL.Branch branch = PXResult<PX.Objects.GL.Branch, TaxHistory, TaxTranReport, TaxBucketLine>.op_Implicit(pxResult);
        TaxTranReport taxTranReport = PXResult<PX.Objects.GL.Branch, TaxHistory, TaxTranReport, TaxBucketLine>.op_Implicit(pxResult);
        if (PXResult<PX.Objects.GL.Branch, TaxHistory, TaxTranReport, TaxBucketLine>.op_Implicit(pxResult).VendorID.HasValue)
        {
          HashSet<string> stringSet;
          if (!dictionary.TryGetValue(branch.BranchCD, out stringSet))
          {
            stringSet = new HashSet<string>();
            dictionary[branch.BranchCD] = stringSet;
          }
          stringSet.Add(taxTranReport.TaxID);
        }
        else
          source2.Add(taxTranReport.TaxID);
      }
      if (dictionary.Count != 0 || source2.Count != 0)
      {
        processingResult.AddErrorMessage("Cannot close tax period.");
        foreach (KeyValuePair<string, HashSet<string>> keyValuePair in dictionary)
        {
          string str = string.Join(", ", keyValuePair.Value.ToArray<string>());
          processingResult.AddErrorMessage("There are unreported transactions for tax(es) {0} in branch {1}. Please void this tax period and prepare it again.", (object) str, (object) keyValuePair.Key.Trim());
        }
        if (source2.Count != 0)
        {
          string str = string.Join(", ", source2.ToArray<string>());
          processingResult.AddErrorMessage("There are transactions for tax(es) {0} that could not be included into report.", (object) str);
          processingResult.AddErrorMessage("Please check tax configuration.");
        }
      }
      return processingResult;
    }
  }

  protected virtual PXResultset<PX.Objects.GL.Branch, TaxHistory, TaxTranReport, TaxBucketLine> SelectTaxTransForVerifying(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor)
  {
    return PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, LeftJoin<TaxHistory, On<TaxHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>, And<TaxHistory.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>, And<TaxHistory.revisionID, Equal<Current<TaxPeriodFilter.revisionId>>>>>>, LeftJoin<TaxTranReport, On<TaxTranReport.branchID, Equal<PX.Objects.GL.Branch.branchID>, And<TaxTranReport.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxTranReport.released, Equal<True>, And<TaxTranReport.voided, Equal<False>, And<TaxTranReport.taxPeriodID, IsNull, And<TaxTranReport.origRefNbr, Equal<Empty>, And<TaxTranReport.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTranReport.taxType, NotEqual<TaxType.pendingSales>, And2<Where<Current<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<True>, Or<TaxTranReport.tranDate, Less<Current<TaxPeriod.endDate>>>>, And<Where<Current<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<False>, Or<TaxTranReport.finDate, Less<Current<TaxPeriod.endDate>>>>>>>>>>>>>>, LeftJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxTranReport.vendorID>, And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>>>, Where<TaxHistory.taxPeriodID, IsNull, And<TaxTranReport.refNbr, IsNotNull>>>.Config>.SelectMultiBound<PXResultset<PX.Objects.GL.Branch, TaxHistory, TaxTranReport, TaxBucketLine>>((PXGraph) this, new object[3]
    {
      (object) filter,
      (object) taxPeriod,
      (object) vendor
    }, Array.Empty<object>());
  }

  public virtual string FileTaxProc(TaxPeriodFilter filter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ReportTaxProcess.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new ReportTaxProcess.\u003C\u003Ec__DisplayClass31_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.\u003C\u003E4__this = this;
    ProcessingResult processingResult = new ProcessingResult();
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing((PXGraph) this, filter.OrganizationID, filter.BranchID, filter.VendorID, filter.TaxPeriodID);
        bool arPPDExist;
        bool apPPDExist;
        ReportTaxProcess.CheckForUnprocessedPPD((PXGraph) this, filter.OrganizationID, branchesForProcessing, filter.VendorID, filter.EndDate, out arPPDExist, out apPPDExist);
        string str1 = string.Empty + (arPPDExist ? PXMessages.Localize("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AR Tax Adjustments (AR504500) form and appropriate VAT credit memos are released on the Release AR Documents (AR501000) form.") : string.Empty) + (apPPDExist ? " " + PXMessages.Localize("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AP Tax Adjustments (AP504500) form and appropriate VAT debit adjustments are released on the Release AP Documents (AP501000) form.") : string.Empty);
        if (!string.IsNullOrEmpty(str1))
          throw new PXSetPropertyException(str1, (PXErrorLevel) 4);
        BranchMaint.FindBranchByID((PXGraph) this, filter.BranchID);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass310.taxPeriod = ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).SelectSingle(new object[3]
        {
          (object) filter.OrganizationID,
          (object) filter.VendorID,
          (object) filter.TaxPeriodID
        });
        // ISSUE: reference to a compiler-generated field
        RoundingManager rmanager = new RoundingManager((PXGraph) this, cDisplayClass310.taxPeriod.VendorID, filter.TaxReportRevisionID.Value);
        PXAccess.MasterCollection.Organization organizationById = PXAccess.GetOrganizationByID(filter.OrganizationID);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        int revisionId = ReportTaxProcess.CurrentRevisionId((PXGraph) this, filter.OrganizationID, branchesForProcessing, cDisplayClass310.taxPeriod.VendorID, cDisplayClass310.taxPeriod.TaxPeriodID) ?? 1;
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass310.taxPeriod.Status == "C")
          ++revisionId;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass310.vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<TaxPeriod.vendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) cDisplayClass310.taxPeriod
        }, Array.Empty<object>()));
        using (new PXReadBranchRestrictedScope(filter.OrganizationID.SingleToArray<int?>(), branchesForProcessing, true, true))
        {
          // ISSUE: method pointer
          ((PXGraph) this).Defaults[typeof (PX.Objects.AP.Vendor)] = new PXGraph.GetDefaultDelegate((object) cDisplayClass310, __methodptr(\u003CFileTaxProc\u003Eb__1));
          string reportCuryId = ReportTaxProcess.GetReportCuryID(rmanager, organizationById);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          PXDatabase.Update<TaxPeriod>(new PXDataFieldParam[5]
          {
            (PXDataFieldParam) new PXDataFieldAssign(typeof (TaxPeriod.status).Name, (object) "C"),
            (PXDataFieldParam) new PXDataFieldRestrict(typeof (TaxPeriod.organizationID).Name, (PXDbType) 8, new int?(4), (object) cDisplayClass310.taxPeriod.OrganizationID, (PXComp) 0),
            (PXDataFieldParam) new PXDataFieldRestrict(typeof (TaxPeriod.vendorID).Name, (PXDbType) 8, new int?(4), (object) cDisplayClass310.taxPeriod.VendorID, (PXComp) 0),
            (PXDataFieldParam) new PXDataFieldRestrict(typeof (TaxPeriod.taxYear).Name, (PXDbType) 3, new int?(4), (object) cDisplayClass310.taxPeriod.TaxYear, (PXComp) 0),
            (PXDataFieldParam) new PXDataFieldRestrict(typeof (TaxPeriod.taxPeriodID).Name, (PXDbType) 3, new int?(6), (object) cDisplayClass310.taxPeriod.TaxPeriodID, (PXComp) 4)
          });
          // ISSUE: reference to a compiler-generated field
          this.UpdateTaxTranBucketIDsFromTaxRev(cDisplayClass310.taxPeriod);
          // ISSUE: reference to a compiler-generated field
          PXResultset<TaxTran> pxResultset1 = this.SelectTaxTransWOTaxRevs(cDisplayClass310.taxPeriod);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.CalculateAndUpdateTaxTranTaxableAmounts(cDisplayClass310.taxPeriod, organizationById, cDisplayClass310.vendor, revisionId);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          PXResultset<TaxTran> pxResultset2 = this.SelectUnreportedTaxTrans(cDisplayClass310.taxPeriod, cDisplayClass310.vendor);
          this.UpdateTaxPeriodInTaxAdjustments();
          // ISSUE: reference to a compiler-generated field
          TaxTran taxTran1 = PXResultset<TaxTran>.op_Implicit(this.SelectTaxTranRateNotFound(cDisplayClass310.taxPeriod, revisionId));
          if (taxTran1 != null)
            throw new PXException("Tax report preparing failed. There is no currency rate to convert tax from currency '{0}' to report currency '{1}' for date '{2}'", new object[3]
            {
              (object) taxTran1.CuryID,
              (object) reportCuryId,
              (object) taxTran1.TranDate.Value.ToShortDateString()
            });
          if (pxResultset1.Count == 0)
          {
            if (pxResultset2.Count == 0)
              goto label_36;
          }
          processingResult.AddErrorMessage("Warning");
          processingResult.AddErrorMessage(":");
          if (pxResultset1.Count != 0)
          {
            Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
            foreach (PXResult<TaxTran> pxResult in pxResultset1)
            {
              TaxTran taxTran2 = PXResult<TaxTran>.op_Implicit(pxResult);
              HashSet<string> stringSet;
              if (!dictionary.TryGetValue(taxTran2.TaxID, out stringSet))
              {
                stringSet = new HashSet<string>();
                dictionary[taxTran2.TaxID] = stringSet;
              }
              stringSet.Add(taxTran2.TaxType);
            }
            foreach (KeyValuePair<string, HashSet<string>> keyValuePair in dictionary)
            {
              foreach (string str2 in keyValuePair.Value)
                processingResult.AddErrorMessage("Tax {0} has no configuration for {1} tax type, but there are tax transactions of this type.", (object) keyValuePair.Key, (object) PXMessages.LocalizeNoPrefix(GetLabel.For<TaxType>(str2)));
            }
          }
          if (pxResultset2.Count != 0)
          {
            string str3 = string.Join(",", GraphHelper.RowCast<TaxTran>((IEnumerable) pxResultset2).Select<TaxTran, string>((Func<TaxTran, string>) (tran => tran.TaxID)).Distinct<string>().OrderBy<string, string>((Func<string, string>) (x => x)).ToArray<string>());
            processingResult.AddErrorMessage("There are transactions for tax(es) {0} that could not be included into report.", (object) str3);
          }
          processingResult.AddErrorMessage("Please check tax configuration.");
        }
label_36:
        TaxHistory prev_hist = (TaxHistory) null;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        foreach (PXResult<TaxPeriod> pxResult in ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).Select(new object[3]
        {
          (object) filter.OrganizationID,
          (object) cDisplayClass310.taxPeriod.VendorID,
          (object) cDisplayClass310.taxPeriod.TaxPeriodID
        }))
        {
          TaxPeriod taxPeriod = PXResult<TaxPeriod>.op_Implicit(pxResult);
          taxPeriod.Status = "P";
          ((PXSelectBase) this.TaxPeriod_Current).Cache.Update((object) taxPeriod);
        }
        // ISSUE: reference to a compiler-generated field
        this.UpdateTaxHistory(filter, cDisplayClass310.taxPeriod, prev_hist, branchesForProcessing, revisionId);
        ((PXSelectBase) this.TaxPeriod_Current).Cache.Persist((PXDBOperation) 2);
        ((PXSelectBase) this.TaxPeriod_Current).Cache.Persist((PXDBOperation) 1);
        ((PXSelectBase) this.TaxHistory_Current).Cache.Persist((PXDBOperation) 2);
        ((PXSelectBase) this.TaxHistory_Current).Cache.Persist((PXDBOperation) 1);
        ((PXSelectBase) this.TaxPeriod_Current).Cache.Persisted(false);
        ((PXSelectBase) this.TaxHistory_Current).Cache.Persisted(false);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        TaxHistorySumManager.UpdateTaxHistorySums((PXGraph) this, rmanager, cDisplayClass310.taxPeriod.TaxPeriodID, new int?(revisionId), filter.OrganizationID, filter.BranchID, new Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool>(cDisplayClass310.\u003CFileTaxProc\u003Eb__0));
        transactionScope.Complete((PXGraph) this);
      }
    }
    return processingResult.GetGeneralMessage();
  }

  protected virtual void UpdateTaxTranBucketIDsFromTaxRev(TaxPeriod taxPeriod)
  {
    PXUpdateJoin<Set<TaxTran.taxBucketID, TaxRev.taxBucketID, Set<TaxTran.vendorID, TaxRev.taxVendorID>>, TaxTran, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<TaxTran.taxID>, And<TaxRev.taxType, Equal<TaxTran.taxType>, And<TaxTran.tranDate, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>>>>>>, Where<TaxRev.taxVendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<StringEmpty>, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>>>>>>>>>.Update((PXGraph) this, new object[1]
    {
      (object) taxPeriod.VendorID
    });
  }

  protected virtual TaxHistory UpdateTaxHistory(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    TaxHistory prev_hist,
    int?[] _branches,
    int revisionId)
  {
    RoundingManager roundingManager = new RoundingManager((PXGraph) this, taxPeriod.VendorID, filter.TaxReportRevisionID.Value);
    using (new PXReadBranchRestrictedScope(filter.OrganizationID.SingleToArray<int?>(), _branches, true, true))
    {
      foreach (PXResult<TaxReportLine, TaxBucketLine, TaxTranReport> pxResult in ((IEnumerable<PXResult<TaxReportLine>>) ((PXSelectBase<TaxReportLine>) this.Period_Details_Expanded).Select(new object[4]
      {
        (object) revisionId,
        (object) taxPeriod.TaxPeriodID,
        (object) taxPeriod.VendorID,
        (object) filter.TaxReportRevisionID
      })).AsEnumerable<PXResult<TaxReportLine>>().Where<PXResult<TaxReportLine>>((Func<PXResult<TaxReportLine>, bool>) (line => this.ShowTaxReportLine(((PXResult) line).GetItem<TaxReportLine>(), taxPeriod.TaxPeriodID))))
      {
        TaxReportLine taxReportLine = PXResult<TaxReportLine, TaxBucketLine, TaxTranReport>.op_Implicit(pxResult);
        TaxTranReport taxTranReport = PXResult<TaxReportLine, TaxBucketLine, TaxTranReport>.op_Implicit(pxResult);
        if (prev_hist == null || !object.Equals((object) prev_hist.BranchID, (object) taxTranReport.BranchID) || !object.Equals((object) prev_hist.AccountID, (object) taxTranReport.AccountID) || !object.Equals((object) prev_hist.SubID, (object) taxTranReport.SubID) || !object.Equals((object) prev_hist.TaxID, (object) taxTranReport.TaxID) || !object.Equals((object) prev_hist.LineNbr, (object) taxReportLine.LineNbr))
        {
          if (prev_hist != null)
            ((PXSelectBase) this.TaxHistory_Current).Cache.Update((object) prev_hist);
          prev_hist = PXResultset<TaxHistory>.op_Implicit(((PXSelectBase<TaxHistory>) this.TaxHistory_Current).Select(new object[8]
          {
            (object) taxTranReport.VendorID,
            (object) taxTranReport.BranchID,
            (object) taxTranReport.AccountID,
            (object) taxTranReport.SubID,
            (object) taxTranReport.TaxID,
            (object) taxTranReport.TaxPeriodID,
            (object) taxReportLine.LineNbr,
            (object) revisionId
          }));
          if (prev_hist == null)
          {
            prev_hist = new TaxHistory()
            {
              VendorID = taxTranReport.VendorID,
              BranchID = taxTranReport.BranchID,
              TaxPeriodID = taxTranReport.TaxPeriodID,
              AccountID = taxTranReport.AccountID,
              SubID = taxTranReport.SubID,
              TaxID = taxTranReport.TaxID,
              LineNbr = taxReportLine.LineNbr,
              CuryID = roundingManager.CurrentVendor.CuryID ?? PXAccess.GetBranch(taxTranReport.BranchID).BaseCuryID,
              FiledAmt = new Decimal?(0M),
              UnfiledAmt = new Decimal?(0M),
              ReportFiledAmt = new Decimal?(0M),
              ReportUnfiledAmt = new Decimal?(0M),
              RevisionID = new int?(revisionId),
              TaxReportRevisionID = taxReportLine.TaxReportRevisionID
            };
            prev_hist = (TaxHistory) ((PXSelectBase) this.TaxHistory_Current).Cache.Insert((object) prev_hist);
          }
        }
        Decimal mult = ReportTaxProcess.GetMult(taxTranReport.Module, taxTranReport.TranType, taxTranReport.TaxType, taxReportLine.LineMult);
        Decimal? nullable1;
        switch (taxReportLine.LineType)
        {
          case "P":
            TaxHistory taxHistory1 = prev_hist;
            Decimal? filedAmt1 = taxHistory1.FiledAmt;
            Decimal num1 = mult;
            nullable1 = taxTranReport.TaxAmt;
            Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
            Decimal num2 = num1 * valueOrDefault1;
            Decimal? nullable2;
            if (!filedAmt1.HasValue)
            {
              nullable1 = new Decimal?();
              nullable2 = nullable1;
            }
            else
              nullable2 = new Decimal?(filedAmt1.GetValueOrDefault() + num2);
            taxHistory1.FiledAmt = nullable2;
            TaxHistory taxHistory2 = prev_hist;
            Decimal? reportFiledAmt1 = taxHistory2.ReportFiledAmt;
            Decimal num3 = mult;
            nullable1 = taxTranReport.ReportTaxAmt;
            Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
            Decimal num4 = num3 * valueOrDefault2;
            Decimal? nullable3;
            if (!reportFiledAmt1.HasValue)
            {
              nullable1 = new Decimal?();
              nullable3 = nullable1;
            }
            else
              nullable3 = new Decimal?(reportFiledAmt1.GetValueOrDefault() + num4);
            taxHistory2.ReportFiledAmt = nullable3;
            break;
          case "A":
            TaxHistory taxHistory3 = prev_hist;
            Decimal? filedAmt2 = taxHistory3.FiledAmt;
            Decimal num5 = mult;
            nullable1 = taxTranReport.TaxableAmt;
            Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
            Decimal num6 = num5 * valueOrDefault3;
            Decimal? nullable4;
            if (!filedAmt2.HasValue)
            {
              nullable1 = new Decimal?();
              nullable4 = nullable1;
            }
            else
              nullable4 = new Decimal?(filedAmt2.GetValueOrDefault() + num6);
            taxHistory3.FiledAmt = nullable4;
            TaxHistory taxHistory4 = prev_hist;
            Decimal? reportFiledAmt2 = taxHistory4.ReportFiledAmt;
            Decimal num7 = mult;
            nullable1 = taxTranReport.ReportTaxableAmt;
            Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
            Decimal num8 = num7 * valueOrDefault4;
            Decimal? nullable5;
            if (!reportFiledAmt2.HasValue)
            {
              nullable1 = new Decimal?();
              nullable5 = nullable1;
            }
            else
              nullable5 = new Decimal?(reportFiledAmt2.GetValueOrDefault() + num8);
            taxHistory4.ReportFiledAmt = nullable5;
            break;
          case "E":
            TaxHistory taxHistory5 = prev_hist;
            Decimal? filedAmt3 = taxHistory5.FiledAmt;
            Decimal num9 = mult;
            nullable1 = taxTranReport.ExemptedAmt;
            Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
            Decimal num10 = num9 * valueOrDefault5;
            Decimal? nullable6;
            if (!filedAmt3.HasValue)
            {
              nullable1 = new Decimal?();
              nullable6 = nullable1;
            }
            else
              nullable6 = new Decimal?(filedAmt3.GetValueOrDefault() + num10);
            taxHistory5.FiledAmt = nullable6;
            TaxHistory taxHistory6 = prev_hist;
            Decimal? reportFiledAmt3 = taxHistory6.ReportFiledAmt;
            Decimal num11 = mult;
            nullable1 = taxTranReport.ReportExemptedAmt;
            Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
            Decimal num12 = num11 * valueOrDefault6;
            Decimal? nullable7;
            if (!reportFiledAmt3.HasValue)
            {
              nullable1 = new Decimal?();
              nullable7 = nullable1;
            }
            else
              nullable7 = new Decimal?(reportFiledAmt3.GetValueOrDefault() + num12);
            taxHistory6.ReportFiledAmt = nullable7;
            break;
        }
        if (taxReportLine.TempLine.GetValueOrDefault() || taxReportLine.TempLineNbr.HasValue && prev_hist.FiledAmt.GetValueOrDefault() == 0M)
        {
          ((PXSelectBase) this.TaxHistory_Current).Cache.Delete((object) prev_hist);
          prev_hist = (TaxHistory) null;
        }
      }
      if (prev_hist != null)
        ((PXSelectBase) this.TaxHistory_Current).Cache.Update((object) prev_hist);
    }
    return prev_hist;
  }

  protected virtual PXResultset<TaxTran> SelectTaxTransWOTaxRevs(TaxPeriod taxPeriod)
  {
    return PXSelectBase<TaxTran, PXSelectJoin<TaxTran, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<TaxTran.taxID>, And<TaxRev.taxType, Equal<TaxTran.taxType>, And<TaxTran.tranDate, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>>>>>>, Where<TaxRev.revisionID, IsNull, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, Equal<Required<TaxPeriod.taxPeriodID>>, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) taxPeriod.TaxPeriodID
    });
  }

  protected virtual PXResultset<TaxTran> SelectTaxTranRateNotFound(
    TaxPeriod taxPeriod,
    int revisionId)
  {
    return PXSelectBase<TaxTran, PXSelect<TaxTran, Where<TaxTran.taxPeriodID, Equal<Required<TaxTran.taxPeriodID>>, And<TaxTran.vendorID, Equal<Required<TaxTran.vendorID>>, And<TaxTran.revisionID, Equal<Required<TaxTran.revisionID>>, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>, And<Where<TaxTran.reportTaxAmt, IsNull, Or<TaxTran.reportTaxableAmt, IsNull, Or<TaxTran.reportExemptedAmt, IsNull>>>>>>>>>, OrderBy<Asc<TaxTran.tranDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) taxPeriod.TaxPeriodID,
      (object) taxPeriod.VendorID,
      (object) revisionId
    });
  }

  protected virtual void UpdateTaxPeriodInTaxAdjustments()
  {
    PXUpdateJoin<Set<TaxAdjustment.taxPeriod, TaxTran.taxPeriodID>, TaxAdjustment, InnerJoin<TaxTran, On<TaxAdjustment.docType, Equal<TaxTran.tranType>, And<TaxAdjustment.refNbr, Equal<TaxTran.refNbr>>>>, Where2<Where<TaxAdjustment.taxPeriod, NotEqual<TaxTran.taxPeriodID>, Or<Where<TaxAdjustment.taxPeriod, IsNull, And<TaxTran.taxPeriodID, IsNotNull>>>>, And<TaxAdjustment.released, Equal<True>>>>.Update((PXGraph) this, Array.Empty<object>());
  }

  protected virtual PXResultset<TaxTran> SelectUnreportedTaxTrans(
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor)
  {
    return PXSelectBase<TaxTran, PXSelectJoin<TaxTran, InnerJoin<PX.Objects.GL.Branch, On<TaxTran.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.finPeriodID, Equal<TaxTran.finPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>>>, Where<TaxTran.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<StringEmpty>, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>, And2<Where<Required<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<True>, Or<TaxTran.tranDate, Less<Required<TaxPeriod.endDate>>>>, And2<Where<Required<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<False>, Or<Sub<OrganizationFinPeriod.endDate, int1>, Less<Required<TaxPeriod.endDate>>>>, And<TaxTran.taxPeriodID, IsNull>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) taxPeriod.VendorID,
      (object) vendor.TaxReportFinPeriod,
      (object) taxPeriod.EndDate,
      (object) vendor.TaxReportFinPeriod,
      (object) taxPeriod.EndDate
    });
  }

  protected virtual void CalculateAndUpdateTaxTranTaxableAmounts(
    TaxPeriod taxPeriod,
    PXAccess.MasterCollection.Organization organization,
    PX.Objects.AP.Vendor vendor,
    int revisionId)
  {
    RoundingManager rmanager = new RoundingManager((PXGraph) this, taxPeriod.VendorID, revisionId);
    string reportCuryId = ReportTaxProcess.GetReportCuryID(rmanager, organization);
    PXUpdateJoin<Set<TaxTran.taxPeriodID, Required<TaxPeriod.taxPeriodID>, Set<TaxTran.revisionID, Required<TaxTran.revisionID>, Set<TaxTran.reportTaxAmt, Switch<Case<Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<TaxTran.curyID>>>, TaxTran.taxAmt, Case<Where<TaxTran.curyID, Equal<PX.Objects.CM.Extensions.Currency.curyID>>, TaxTran.curyTaxAmt, Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, Equal<PX.Objects.CM.Extensions.CuryMultDivType.mult>>, Round<Mult<TaxTran.curyTaxAmt, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, PX.Objects.CM.Extensions.Currency.decimalPlaces>, Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, Equal<PX.Objects.CM.Extensions.CuryMultDivType.div>>, Round<Div<TaxTran.curyTaxAmt, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, PX.Objects.CM.Extensions.Currency.decimalPlaces>>>>>>, Set<TaxTran.reportTaxableAmt, Switch<Case<Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<TaxTran.curyID>>>, TaxTran.taxableAmt, Case<Where<TaxTran.curyID, Equal<PX.Objects.CM.Extensions.Currency.curyID>>, TaxTran.curyTaxableAmt, Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, Equal<PX.Objects.CM.Extensions.CuryMultDivType.mult>>, Round<Mult<TaxTran.curyTaxableAmt, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, PX.Objects.CM.Extensions.Currency.decimalPlaces>, Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, Equal<PX.Objects.CM.Extensions.CuryMultDivType.div>>, Round<Div<TaxTran.curyTaxableAmt, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, PX.Objects.CM.Extensions.Currency.decimalPlaces>>>>>>, Set<TaxTran.reportExemptedAmt, Switch<Case<Where<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<TaxTran.curyID>>>, TaxTran.exemptedAmt, Case<Where<TaxTran.curyID, Equal<PX.Objects.CM.Extensions.Currency.curyID>>, TaxTran.curyExemptedAmt, Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, Equal<PX.Objects.CM.Extensions.CuryMultDivType.mult>>, Round<Mult<TaxTran.curyExemptedAmt, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, PX.Objects.CM.Extensions.Currency.decimalPlaces>, Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, Equal<PX.Objects.CM.Extensions.CuryMultDivType.div>>, Round<Div<TaxTran.curyExemptedAmt, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, PX.Objects.CM.Extensions.Currency.decimalPlaces>>>>>>, Set<TaxTran.reportCuryID, PX.Objects.CM.Extensions.Currency.curyID, Set<TaxTran.reportCuryRateTypeID, PX.Objects.CM.Extensions.CurrencyRate.curyRateType, Set<TaxTran.reportCuryEffDate, PX.Objects.CM.Extensions.CurrencyRate.curyEffDate, Set<TaxTran.reportCuryRate, Switch<Case<Where<PX.Objects.CM.CurrencyRate.curyRate, IsNotNull>, PX.Objects.CM.Extensions.CurrencyRate.curyRate>, decimal1>, Set<TaxTran.reportCuryMultDiv, Switch<Case<Where<PX.Objects.CM.CurrencyRate.curyMultDiv, IsNotNull>, PX.Objects.CM.Extensions.CurrencyRate.curyMultDiv>, PX.Objects.CM.Extensions.CuryMultDivType.mult>>>>>>>>>>>, TaxTran, InnerJoin<PX.Objects.GL.Branch, On<TaxTran.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.finPeriodID, Equal<TaxTran.finPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, InnerJoin<TaxBucketLine, On<TaxTran.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxTran.taxBucketID, Equal<TaxBucketLine.bucketID>>>, LeftJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxBucketLine.taxReportRevisionID>, And<TaxReportLine.lineNbr, Equal<TaxBucketLine.lineNbr>>>>, LeftJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<Required<PX.Objects.CM.Extensions.Currency.curyID>>>, LeftJoin<PX.Objects.CM.CurrencyRate, On<PX.Objects.CM.CurrencyRate.fromCuryID, Equal<TaxTran.curyID>, And<PX.Objects.CM.CurrencyRate.toCuryID, Equal<Required<PX.Objects.CM.CurrencyRate.toCuryID>>, And<PX.Objects.CM.CurrencyRate.curyRateType, Equal<Required<PX.Objects.CM.CurrencyRate.curyRateType>>, And<PX.Objects.CM.CurrencyRate.curyEffDate, Equal<TaxTran.curyEffDate>>>>>>>>>>>, Where<TaxTran.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<Empty>, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>, And2<Where<TaxReportLine.taxZoneID, IsNull, Or<TaxReportLine.taxZoneID, Equal<TaxTran.taxZoneID>>>, And2<Where<Required<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<True>, Or<TaxTran.tranDate, Less<Required<TaxPeriod.endDate>>>>, And<Where<Required<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<False>, Or<Sub<OrganizationFinPeriod.endDate, int1>, Less<Required<TaxPeriod.endDate>>>>>>>>>>>>>>>.Update((PXGraph) this, new object[13]
    {
      (object) taxPeriod.TaxPeriodID,
      (object) revisionId,
      (object) ((PXAccess.Organization) organization).BaseCuryID,
      (object) ((PXAccess.Organization) organization).BaseCuryID,
      (object) reportCuryId,
      (object) reportCuryId,
      (object) reportCuryId,
      (object) rmanager.CurrentVendor.CuryRateTypeID,
      (object) taxPeriod.VendorID,
      (object) vendor.TaxReportFinPeriod,
      (object) taxPeriod.EndDate,
      (object) vendor.TaxReportFinPeriod,
      (object) taxPeriod.EndDate
    });
  }

  private static string GetReportCuryID(
    RoundingManager rmanager,
    PXAccess.MasterCollection.Organization organization)
  {
    return rmanager.CurrentVendor.CuryID ?? ((PXAccess.Organization) organization).BaseCuryID;
  }

  public virtual bool ShowTaxReportLine(TaxReportLine taxReportLine, string taxPeriodID) => true;

  public static void CheckForUnprocessedPPD(
    PXGraph graph,
    int? organizationID,
    int? branchID,
    int? vendorID,
    DateTime? endDate,
    out bool arPPDExist,
    out bool apPPDExist)
  {
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing(graph, organizationID, branchID, vendorID);
    ReportTaxProcess.CheckForUnprocessedPPD(graph, organizationID, branchesForProcessing, vendorID, endDate, out arPPDExist, out apPPDExist);
  }

  public static void CheckForUnprocessedPPD(
    PXGraph graph,
    int? organizationID,
    int?[] branchIDs,
    int? vendorID,
    DateTime? endDate,
    out bool arPPDExist,
    out bool apPPDExist)
  {
    arPPDExist = ReportTaxProcess.CheckForUnprocessedPPD(graph, organizationID, branchIDs, vendorID, endDate);
    apPPDExist = ReportTaxProcess.CheckAPInvoicesForUnprocessedPPD(graph, organizationID, branchIDs, vendorID, endDate);
  }

  public static bool CheckForUnprocessedPPD(
    PXGraph graph,
    int? organizationID,
    int? branchID,
    int? vendorID,
    DateTime? endDate)
  {
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing(graph, organizationID, branchID, vendorID);
    return ReportTaxProcess.CheckForUnprocessedPPD(graph, organizationID, branchesForProcessing, vendorID, endDate);
  }

  public static bool CheckForUnprocessedPPD(
    PXGraph graph,
    int? organizationID,
    int?[] branchIDs,
    int? vendorID,
    DateTime? endDate)
  {
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArray<int?>(), branchIDs, true, true))
    {
      bool flag = false;
      Tax tax = PXResultset<Tax>.op_Implicit(PXSelectBase<Tax, PXSelect<Tax, Where<Tax.taxType, Equal<CSTaxType.vat>, And<Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>, And<Tax.taxVendorID, Equal<Required<Tax.taxVendorID>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) vendorID
      }));
      if (tax != null)
        flag = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.pendingPPD, Equal<True>, And<ARAdjust.adjgDocDate, LessEqual<Required<ARAdjust.adjgDocDate>>>>>>>>, InnerJoin<ARTaxTran, On<ARTaxTran.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<ARTaxTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>, Where<PX.Objects.AR.ARInvoice.pendingPPD, Equal<True>, And<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.openDoc, Equal<True>, And<ARTaxTran.taxID, Equal<Required<ARTaxTran.taxID>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
        {
          (object) endDate,
          (object) tax.TaxID
        })) != null;
      return flag;
    }
  }

  public static bool CheckAPInvoicesForUnprocessedPPD(
    PXGraph graph,
    int? organizationID,
    int? branchID,
    int? vendorID,
    DateTime? endDate)
  {
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing(graph, organizationID, branchID, vendorID);
    return ReportTaxProcess.CheckAPInvoicesForUnprocessedPPD(graph, organizationID, branchesForProcessing, vendorID, endDate);
  }

  public static bool CheckAPInvoicesForUnprocessedPPD(
    PXGraph graph,
    int? organizationID,
    int?[] branchIDs,
    int? vendorID,
    DateTime? endDate)
  {
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArray<int?>(), branchIDs, true, true))
    {
      bool flag = false;
      Tax tax = PXResultset<Tax>.op_Implicit(PXSelectBase<Tax, PXSelect<Tax, Where<Tax.taxType, Equal<CSTaxType.vat>, And<Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>, And<Tax.taxVendorID, Equal<Required<Tax.taxVendorID>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) vendorID
      }));
      if (tax != null)
        flag = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoin<PX.Objects.AP.APInvoice, InnerJoin<APAdjust, On<APAdjust.adjdDocType, Equal<PX.Objects.AP.APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<APAdjust.released, Equal<True>, And<APAdjust.voided, NotEqual<True>, And<APAdjust.pendingPPD, Equal<True>, And<APAdjust.adjgDocDate, LessEqual<Required<APAdjust.adjgDocDate>>>>>>>>, InnerJoin<APTaxTran, On<APTaxTran.refNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<APTaxTran.tranType, Equal<PX.Objects.AP.APInvoice.docType>>>>>, Where<PX.Objects.AP.APInvoice.pendingPPD, Equal<True>, And<PX.Objects.AP.APInvoice.released, Equal<True>, And<PX.Objects.AP.APInvoice.openDoc, Equal<True>, And<APTaxTran.taxID, Equal<Required<APTaxTran.taxID>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
        {
          (object) endDate,
          (object) tax.TaxID
        })) != null;
      return flag;
    }
  }

  public static bool CheckForUnprocessedSVAT(
    PXGraph graph,
    int? organizationID,
    int? branchID,
    PX.Objects.AP.Vendor vendor,
    DateTime? endDate)
  {
    bool flag = false;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID(graph, organizationID);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATReporting>() && organizationById != null && (branchID.HasValue && organizationById.FileTaxesByBranches.GetValueOrDefault() || !organizationById.FileTaxesByBranches.GetValueOrDefault()) && vendor != null && vendor.BAccountID.HasValue && endDate.HasValue)
    {
      object obj = vendor.TaxReportFinPeriod.GetValueOrDefault() ? (object) new PXSelectJoin<SVATConversionHist, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<SVATConversionHist.adjdBranchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.startDate, LessEqual<SVATConversionHist.adjdDocDate>, And<OrganizationFinPeriod.endDate, Greater<SVATConversionHist.adjdDocDate>, And<OrganizationFinPeriod.startDate, NotEqual<OrganizationFinPeriod.endDate>, And<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>>>>>, Where<OrganizationFinPeriod.endDate, LessEqual<Required<OrganizationFinPeriod.endDate>>>>(graph) : (object) new PXSelect<SVATConversionHist, Where<SVATConversionHist.adjdDocDate, LessEqual<Required<OrganizationFinPeriod.endDate>>>>(graph);
      ((PXSelectBase<SVATConversionHist>) obj).WhereAnd<Where<SVATConversionHist.processed, NotEqual<True>, And<SVATConversionHist.adjdBranchID, In<Required<SVATConversionHist.adjdBranchID>>, And<SVATConversionHist.vendorID, Equal<Required<SVATConversionHist.vendorID>>, And<SVATConversionHist.reversalMethod, Equal<SVATTaxReversalMethods.onPayments>, And<Where<SVATConversionHist.adjdDocType, NotEqual<SVATConversionHist.adjgDocType>, Or<SVATConversionHist.adjdRefNbr, NotEqual<SVATConversionHist.adjgRefNbr>>>>>>>>>();
      int[] branchesToProcess = ReportTaxProcess.GetBranchesToProcess(graph, organizationID, branchID);
      flag = ((PXSelectBase<SVATConversionHist>) obj).SelectSingle(new object[3]
      {
        (object) endDate.Value.AddDays(-1.0),
        (object) branchesToProcess,
        (object) vendor.BAccountID
      }) != null;
    }
    return flag;
  }

  public class ErrorNotifications
  {
    private List<string> _errorMessages = new List<string>();

    public void AddMessage(string message, params object[] args)
    {
      this._errorMessages.Add(PXMessages.LocalizeFormatNoPrefix(message, args));
    }

    public string Message => string.Join(" ", this._errorMessages.ToArray());

    public void RaiseIfAny()
    {
      if (this._errorMessages.Any<string>())
        throw new PXException(this.Message);
    }
  }
}
