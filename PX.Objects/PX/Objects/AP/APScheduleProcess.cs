// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APScheduleProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APScheduleProcess : PXGraph<APScheduleProcess>, IScheduleProcessing
{
  public PXSelect<PX.Objects.GL.Schedule> Running_Schedule;
  public PXSelect<APTran> Tran_Created;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo> CuryInfo_Created;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public GLSetup GLSetup
  {
    get => (GLSetup) PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select((PXGraph) this);
  }

  public virtual void GenerateProc(PX.Objects.GL.Schedule schedule)
  {
    this.GenerateProc(schedule, (short) 1, this.Accessinfo.BusinessDate.Value);
  }

  public virtual void GenerateProc(PX.Objects.GL.Schedule schedule, short times, System.DateTime runDate)
  {
    IEnumerable<ScheduleDet> scheduleDets = new Scheduler((PXGraph) this).MakeSchedule(schedule, times, new System.DateTime?(runDate));
    APInvoiceEntry graph = this.CreateGraph();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (ScheduleDet scheduleDet in scheduleDets)
      {
        foreach (PXResult<APInvoice, Vendor, PX.Objects.CM.Extensions.CurrencyInfo> pxResult1 in PXSelectBase<APInvoice, PXSelectJoin<APInvoice, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>>>, Where<APInvoice.scheduleID, Equal<Required<APInvoice.scheduleID>>, And<APInvoice.scheduled, Equal<boolTrue>>>>.Config>.Select((PXGraph) this, (object) schedule.ScheduleID))
        {
          graph.Clear();
          graph.vendor.Current = (Vendor) pxResult1;
          APInvoice apInvoice1 = (APInvoice) pxResult1;
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult1;
          bool? nullable = apInvoice1.Released;
          if (nullable.GetValueOrDefault())
            throw new PXException("One of the scheduled documents is already released. Cannot generate new documents.");
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = graph.GetExtension<APInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo1, scheduleDet.ScheduledDate);
          APInvoice copy1 = PXCache<APInvoice>.CreateCopy(apInvoice1);
          copy1.CuryInfoID = currencyInfo2.CuryInfoID;
          copy1.DocDate = scheduleDet.ScheduledDate;
          FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(copy1.BranchID), scheduleDet.ScheduledPeriod).GetValueOrRaiseError();
          copy1.FinPeriodID = valueOrRaiseError.FinPeriodID;
          copy1.TranPeriodID = (string) null;
          copy1.DueDate = new System.DateTime?();
          copy1.DiscDate = new System.DateTime?();
          copy1.PayDate = new System.DateTime?();
          copy1.CuryOrigDiscAmt = new Decimal?();
          copy1.OrigDiscAmt = new Decimal?();
          copy1.RefNbr = (string) null;
          copy1.Scheduled = new bool?(false);
          copy1.CuryLineTotal = new Decimal?(0M);
          copy1.CuryVatTaxableTotal = new Decimal?(0M);
          copy1.CuryVatExemptTotal = new Decimal?(0M);
          copy1.NoteID = new Guid?();
          copy1.PaySel = new bool?(false);
          copy1.IsTaxValid = new bool?(false);
          copy1.IsTaxPosted = new bool?(false);
          copy1.IsTaxSaved = new bool?(false);
          copy1.OrigDocType = apInvoice1.DocType;
          copy1.OrigRefNbr = apInvoice1.RefNbr;
          copy1.CuryDetailExtPriceTotal = new Decimal?(0M);
          copy1.DetailExtPriceTotal = new Decimal?(0M);
          copy1.CuryLineDiscTotal = new Decimal?(0M);
          copy1.LineDiscTotal = new Decimal?(0M);
          KeyValueHelper.CopyAttributes(typeof (APInvoice), graph.Document.Cache, (object) apInvoice1, (object) copy1);
          APInvoice apInvoice2 = graph.Document.Insert(copy1);
          nullable = apInvoice2.DontApprove;
          if (!nullable.GetValueOrDefault())
            apInvoice2.Hold = new bool?(true);
          APInvoice dst_row1 = graph.Document.Update(apInvoice2);
          PXNoteAttribute.CopyNoteAndFiles(this.Caches[typeof (APInvoice)], (object) apInvoice1, graph.Document.Cache, (object) dst_row1);
          foreach (PXResult<APTran> pxResult2 in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<Where<APTran.lineType, IsNull, Or<APTran.lineType, NotEqual<SOLineType.discount>>>>>>>.Config>.Select((PXGraph) graph, (object) apInvoice1.DocType, (object) apInvoice1.RefNbr))
          {
            APTran src_row = (APTran) pxResult2;
            APTran copy2 = PXCache<APTran>.CreateCopy(src_row);
            copy2.FinPeriodID = (string) null;
            copy2.TranPeriodID = (string) null;
            copy2.RefNbr = (string) null;
            copy2.CuryInfoID = new long?();
            copy2.ManualPrice = new bool?(true);
            copy2.ManualDisc = new bool?(true);
            copy2.NoteID = new Guid?();
            APTran dst_row2 = graph.Transactions.Insert(copy2);
            dst_row2.Box1099 = src_row.Box1099;
            PXNoteAttribute.CopyNoteAndFiles(this.Caches[typeof (APTran)], (object) src_row, graph.Transactions.Cache, (object) dst_row2);
          }
          foreach (PXResult<APInvoiceDiscountDetail> pxResult3 in PXSelectBase<APInvoiceDiscountDetail, PXSelect<APInvoiceDiscountDetail, Where<APInvoiceDiscountDetail.docType, Equal<Required<APInvoiceDiscountDetail.docType>>, And<APInvoiceDiscountDetail.refNbr, Equal<Required<APInvoiceDiscountDetail.refNbr>>>>>.Config>.Select((PXGraph) graph, (object) apInvoice1.DocType, (object) apInvoice1.RefNbr))
          {
            APInvoiceDiscountDetail copy3 = PXCache<APInvoiceDiscountDetail>.CreateCopy((APInvoiceDiscountDetail) pxResult3);
            copy3.RefNbr = (string) null;
            copy3.CuryInfoID = new long?();
            copy3.IsManual = new bool?(true);
            DiscountEngineProvider.GetEngineFor<APTran, APInvoiceDiscountDetail>().InsertDiscountDetail(graph.DiscountDetails.Cache, (PXSelectBase<APInvoiceDiscountDetail>) graph.DiscountDetails, copy3);
          }
          BalanceCalculation.ForceDocumentControlTotals((PXGraph) graph, (IInvoice) dst_row1);
          try
          {
            graph.Save.Press();
          }
          catch
          {
            if (graph.Document.Cache.IsInsertedUpdatedDeleted)
              throw;
          }
        }
        schedule.LastRunDate = scheduleDet.ScheduledDate;
        this.Running_Schedule.Cache.Update((object) schedule);
      }
      this.Running_Schedule.Cache.Persist(PXDBOperation.Update);
      transactionScope.Complete((PXGraph) this);
    }
    this.Running_Schedule.Cache.Persisted(false);
  }

  public virtual APInvoiceEntry CreateGraph() => PXGraph.CreateInstance<APInvoiceEntry>();
}
