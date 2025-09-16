// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ReportTaxDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.TX;

[TableAndChartDashboardType]
public class ReportTaxDetail : PXGraph<ReportTaxDetail>
{
  public PXSelect<PX.Objects.CR.BAccount> dummy_baccount;
  public PXCancel<TaxHistoryMaster> Cancel;
  public PXFilter<TaxHistoryMaster> History_Header;
  public PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Current<TaxHistoryMaster.taxReportRevisionID>>, And<TaxReportLine.lineNbr, Equal<Current<TaxHistoryMaster.lineNbr>>>>>> TaxReportLine_Select;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<TaxTranReport, LeftJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxTranReport.vendorID>, And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>, Where<boolFalse, Equal<boolTrue>>> History_Detail;
  public PXSelectJoin<TaxReportLine, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>, InnerJoin<TaxTranReport, On<TaxTranReport.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxTranReport.taxBucketID, Equal<TaxBucketLine.bucketID>, And<Where<TaxReportLine.taxZoneID, IsNull, And<TaxReportLine.tempLine, Equal<boolFalse>, Or<TaxReportLine.taxZoneID, Equal<TaxTranReport.taxZoneID>>>>>>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<TaxTranReport.bAccountID>>>>>, Where<TaxReportLine.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>, And2<Where<TaxReportLine.lineNbr, Equal<Current<TaxHistoryMaster.lineNbr>>, Or<TaxReportLine.tempLineNbr, Equal<Current<TaxHistoryMaster.lineNbr>>>>, And<TaxTranReport.taxPeriodID, Equal<Current<TaxHistoryMaster.taxPeriodID>>, And<TaxTranReport.released, Equal<boolTrue>, And<TaxTranReport.voided, Equal<boolFalse>>>>>>> History_Detail_Expanded;
  public PXAction<TaxHistoryMaster> viewBatch;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<TaxHistoryMaster> viewDocument;
  public PXSetup<PX.Objects.GL.Branch> Company;

  public TaxReportLine taxReportLine
  {
    get
    {
      return PXResultset<TaxReportLine>.op_Implicit(((PXSelectBase<TaxReportLine>) this.TaxReportLine_Select).Select(Array.Empty<object>()));
    }
  }

  public ReportTaxDetail()
  {
    if (!((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
    ((PXSelectBase) this.History_Detail).Cache.AllowInsert = false;
    ((PXSelectBase) this.History_Detail).Cache.AllowUpdate = false;
    ((PXSelectBase) this.History_Detail).Cache.AllowDelete = false;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(ReportTaxDetail.\u003C\u003Ec.\u003C\u003E9__10_0 ?? (ReportTaxDetail.\u003C\u003Ec.\u003C\u003E9__10_0 = new PXFieldDefaulting((object) ReportTaxDetail.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__10_0))));
    ((PXSelectBase) this.History_Header).Cache.IsDirty = false;
    PXUIFieldAttribute.SetRequired<TaxHistoryMaster.branchID>(((PXSelectBase) this.History_Header).Cache, false);
  }

  public virtual IEnumerable history_Detail()
  {
    ReportTaxDetail reportTaxDetail = this;
    TaxHistoryMaster filterRow = ((PXSelectBase<TaxHistoryMaster>) reportTaxDetail.History_Header).Current;
    using (new PXReadBranchRestrictedScope(filterRow.OrganizationID.SingleToArray<int?>(), filterRow.BranchID.SingleToArrayOrNull<int?>(), true, false))
    {
      PXResultset<TaxReportLine> source = ((PXSelectBase<TaxReportLine>) reportTaxDetail.History_Detail_Expanded).Select(new object[2]
      {
        (object) filterRow.OrganizationID,
        (object) filterRow.BranchID
      });
      Expression<Func<PXResult<TaxReportLine>, bool>> predicate = (Expression<Func<PXResult<TaxReportLine>, bool>>) (row => ((TaxReportLine) row).TaxReportRevisionID == filterRow.TaxReportRevisionID);
      foreach (PXResult<TaxReportLine, TaxBucketLine, TaxTranReport, PX.Objects.CR.BAccount> pxResult in (IEnumerable<PXResult<TaxReportLine>>) ((IQueryable<PXResult<TaxReportLine>>) source).Where<PXResult<TaxReportLine>>(predicate))
      {
        TaxBucketLine taxBucketLine = PXResult<TaxReportLine, TaxBucketLine, TaxTranReport, PX.Objects.CR.BAccount>.op_Implicit(pxResult);
        TaxTranReport copy = (TaxTranReport) ((PXGraph) reportTaxDetail).Caches[typeof (TaxTranReport)].CreateCopy((object) PXResult<TaxReportLine, TaxBucketLine, TaxTranReport, PX.Objects.CR.BAccount>.op_Implicit(pxResult));
        PX.Objects.CR.BAccount baccount = PXResult<TaxReportLine, TaxBucketLine, TaxTranReport, PX.Objects.CR.BAccount>.op_Implicit(pxResult);
        Decimal num1 = 0M;
        if (((PXSelectBase<TaxReportLine>) reportTaxDetail.TaxReportLine_Select).Current != null)
          num1 = ReportTaxProcess.GetMult(copy.Module, copy.TranType, copy.TaxType, ((PXSelectBase<TaxReportLine>) reportTaxDetail.TaxReportLine_Select).Current.LineMult);
        TaxTranReport taxTranReport1 = copy;
        Decimal num2 = num1;
        Decimal? reportTaxAmt = copy.ReportTaxAmt;
        Decimal? nullable1 = reportTaxAmt.HasValue ? new Decimal?(num2 * reportTaxAmt.GetValueOrDefault()) : new Decimal?();
        taxTranReport1.ReportTaxAmt = nullable1;
        TaxTranReport taxTranReport2 = copy;
        Decimal num3 = num1;
        Decimal? nullable2 = copy.ReportTaxableAmt;
        Decimal? nullable3 = nullable2.HasValue ? new Decimal?(num3 * nullable2.GetValueOrDefault()) : new Decimal?();
        taxTranReport2.ReportTaxableAmt = nullable3;
        TaxTranReport taxTranReport3 = copy;
        Decimal num4 = num1;
        nullable2 = copy.ReportExemptedAmt;
        Decimal? nullable4 = nullable2.HasValue ? new Decimal?(num4 * nullable2.GetValueOrDefault()) : new Decimal?();
        taxTranReport3.ReportExemptedAmt = nullable4;
        yield return (object) new PXResult<TaxTranReport, TaxBucketLine, PX.Objects.CR.BAccount>(copy, taxBucketLine, baccount);
      }
    }
  }

  protected virtual void TaxHistoryMaster_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    int? nullable1;
    if (((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current != null && ((PXSelectBase<TaxReportLine>) this.TaxReportLine_Select).Current != null)
    {
      int? lineNbr = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.LineNbr;
      nullable1 = ((PXSelectBase<TaxReportLine>) this.TaxReportLine_Select).Current.LineNbr;
      if (!(lineNbr.GetValueOrDefault() == nullable1.GetValueOrDefault() & lineNbr.HasValue == nullable1.HasValue))
        ((PXSelectBase<TaxReportLine>) this.TaxReportLine_Select).Current = (TaxReportLine) null;
    }
    if (((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current != null)
    {
      nullable1 = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.VendorID;
      if (nullable1.HasValue && !string.IsNullOrEmpty(((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.TaxPeriodID))
      {
        TaxPeriod taxPeriod = (TaxPeriod) PXSelectorAttribute.Select<TaxHistoryMaster.taxPeriodID>(((PXSelectBase) this.History_Header).Cache, (object) ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current);
        if (taxPeriod != null)
        {
          ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.StartDate = taxPeriod.StartDate;
          ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.EndDate = taxPeriod.EndDate;
        }
        TaxHistoryMaster current = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current;
        DateTime? endDate = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.EndDate;
        int? nullable2;
        if (endDate.HasValue)
        {
          int? vendorId = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.VendorID;
          endDate = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.EndDate;
          DateTime? searchDate = new DateTime?(endDate.Value.AddDays(-1.0));
          TaxReport reportVersionByDate = TaxReportMaint.GetTaxReportVersionByDate((PXGraph) this, vendorId, searchDate);
          if (reportVersionByDate == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = reportVersionByDate.RevisionID;
        }
        else
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        current.TaxReportRevisionID = nullable2;
      }
    }
    if (((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current == null)
      return;
    nullable1 = ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.VendorID;
    if (!nullable1.HasValue)
      return;
    List<int> intList1 = new List<int>();
    List<string> stringList = new List<string>();
    foreach (PXResult<TaxReportLine> pxResult in PXSelectBase<TaxReportLine, PXSelectReadonly<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>>>, OrderBy<Asc<TaxReportLine.sortOrder, Asc<TaxReportLine.taxZoneID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Current.TaxReportRevisionID
    }))
    {
      TaxReportLine taxReportLine = PXResult<TaxReportLine>.op_Implicit(pxResult);
      List<int> intList2 = intList1;
      nullable1 = taxReportLine.LineNbr;
      int valueOrDefault = nullable1.GetValueOrDefault();
      intList2.Add(valueOrDefault);
      nullable1 = taxReportLine.SortOrder;
      string str = $"{nullable1.GetValueOrDefault().ToString()}-{taxReportLine.Descr}";
      stringList.Add(str);
    }
    if (intList1.Count <= 0)
      return;
    int num = intList1[0];
    PXIntListAttribute.SetList<TaxHistoryMaster.lineNbr>(((PXSelectBase) this.History_Header).Cache, (object) null, intList1.ToArray(), stringList.ToArray());
  }

  protected virtual void TaxHistoryMaster_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<TaxHistoryMaster.taxPeriodID>(e.Row);
    sender.SetDefaultExt<TaxHistoryMaster.startDate>(e.Row);
    sender.SetDefaultExt<TaxHistoryMaster.endDate>(e.Row);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    TaxTranReport current = ((PXSelectBase<TaxTranReport>) this.History_Detail).Current;
    if (current != null)
    {
      string batchNbr = (string) null;
      switch (current.Module)
      {
        case "AP":
          batchNbr = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Current<TaxTranReport.tranType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Current<TaxTranReport.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            (object) current
          }, Array.Empty<object>()))?.BatchNbr;
          break;
        case "AR":
          batchNbr = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Current<TaxTranReport.tranType>>, And<ARRegister.refNbr, Equal<Current<TaxTranReport.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            (object) current
          }, Array.Empty<object>()))?.BatchNbr;
          break;
        case "GL":
          if (current.TranType == "TFW" || current.TranType == "TRV")
          {
            batchNbr = current.RefNbr;
            break;
          }
          if ((current.TranType == "VTO" || current.TranType == "VTI") && !string.IsNullOrEmpty(current.TaxInvoiceNbr) && current.TaxInvoiceDate.HasValue)
          {
            batchNbr = PXResultset<SVATConversionHist>.op_Implicit(PXSelectBase<SVATConversionHist, PXSelect<SVATConversionHist, Where<SVATConversionHist.taxRecordID, Equal<Current<TaxTran.recordID>>, And<SVATConversionHist.processed, Equal<True>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
            {
              (object) current
            }, Array.Empty<object>()))?.AdjBatchNbr;
            break;
          }
          batchNbr = PXResultset<TaxAdjustment>.op_Implicit(((PXSelectBase<TaxAdjustment>) PXGraph.CreateInstance<TaxAdjustmentEntry>().Document).Search<TaxAdjustment.refNbr>((object) current.RefNbr, new object[1]
          {
            (object) current.TranType
          }))?.BatchNbr;
          break;
        case "CA":
          CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
          CAAdj caAdj = PXResultset<CAAdj>.op_Implicit(((PXSelectBase<CAAdj>) instance.CAAdjRecords).Search<CAAdj.adjRefNbr>((object) current.RefNbr, Array.Empty<object>()));
          if (caAdj != null)
          {
            batchNbr = (string) ((PXSelectBase) instance.CAAdjRecords).Cache.GetValue<CAAdj.tranID_CATran_batchNbr>((object) caAdj);
            break;
          }
          break;
      }
      if (!string.IsNullOrEmpty(batchNbr))
      {
        PX.Objects.GL.Batch batch = JournalEntry.FindBatch((PXGraph) this, current.Module, batchNbr);
        if (batch != null)
          JournalEntry.RedirectToBatch(batch);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    TaxTranReport current = ((PXSelectBase<TaxTranReport>) this.History_Detail).Current;
    if (current != null)
    {
      switch (current.Module)
      {
        case "AP":
          PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException(new APDocGraphCreator().Create(current.TranType, current.RefNbr, new int?()), true, "Document");
          ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException1;
        case "AR":
          PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException(new ARDocGraphCreator().Create(current.TranType, current.RefNbr, new int?()), true, "Document");
          ((PXBaseRedirectException) requiredException2).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException2;
        case "GL":
          if (current.TranType == "TFW" || current.TranType == "TRV")
          {
            PX.Objects.GL.Batch batch = JournalEntry.FindBatch((PXGraph) this, current.Module, current.RefNbr);
            if (batch != null)
            {
              JournalEntry.RedirectToBatch(batch);
              break;
            }
            break;
          }
          if ((current.TranType == "VTO" || current.TranType == "VTI") && !string.IsNullOrEmpty(current.TaxInvoiceNbr) && current.TaxInvoiceDate.HasValue)
          {
            ProcessSVATBase instance = PXGraph.CreateInstance<ProcessSVATBase>();
            SVATConversionHist svatConversionHist = PXResultset<SVATConversionHist>.op_Implicit(PXSelectBase<SVATConversionHist, PXSelect<SVATConversionHist, Where<SVATConversionHist.taxRecordID, Equal<Required<SVATConversionHist.taxRecordID>>, And<SVATConversionHist.processed, Equal<True>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, new object[1]
            {
              (object) current.RecordID
            }));
            if (svatConversionHist != null)
            {
              PXRedirectHelper.TryRedirect(((PXSelectBase) instance.SVATDocuments).Cache, (object) svatConversionHist, "Document", (PXRedirectHelper.WindowMode) 3);
              break;
            }
            break;
          }
          TaxAdjustmentEntry instance1 = PXGraph.CreateInstance<TaxAdjustmentEntry>();
          TaxAdjustment taxAdjustment = PXResultset<TaxAdjustment>.op_Implicit(((PXSelectBase<TaxAdjustment>) instance1.Document).Search<TaxAdjustment.refNbr>((object) current.RefNbr, new object[1]
          {
            (object) current.TranType
          }));
          if (taxAdjustment != null)
          {
            ((PXSelectBase<TaxAdjustment>) instance1.Document).Current = taxAdjustment;
            PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException((PXGraph) instance1, true, "Document");
            ((PXBaseRedirectException) requiredException3).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException3;
          }
          break;
        case "CA":
          CATranEntry instance2 = PXGraph.CreateInstance<CATranEntry>();
          CAAdj caAdj = PXResultset<CAAdj>.op_Implicit(((PXSelectBase<CAAdj>) instance2.CAAdjRecords).Search<CAAdj.adjRefNbr>((object) current.RefNbr, Array.Empty<object>()));
          if (caAdj != null)
          {
            ((PXSelectBase<CAAdj>) instance2.CAAdjRecords).Current = caAdj;
            PXRedirectRequiredException requiredException4 = new PXRedirectRequiredException((PXGraph) instance2, true, "Document");
            ((PXBaseRedirectException) requiredException4).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException4;
          }
          break;
      }
    }
    return (IEnumerable) ((PXSelectBase<TaxHistoryMaster>) this.History_Header).Select(Array.Empty<object>());
  }
}
