// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARScheduleProcess
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
namespace PX.Objects.AR;

public class ARScheduleProcess : PXGraph<ARScheduleProcess>, IScheduleProcessing
{
  public PXSelect<PX.Objects.GL.Schedule> Running_Schedule;
  public PXSelect<ARTran> Tran_Created;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo> CuryInfo_Created;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public virtual void GenerateProc(PX.Objects.GL.Schedule schedule)
  {
    this.GenerateProc(schedule, (short) 1, ((PXGraph) this).Accessinfo.BusinessDate.Value);
  }

  public virtual void GenerateProc(PX.Objects.GL.Schedule schedule, short times, DateTime runDate)
  {
    IEnumerable<ScheduleDet> scheduleDets = new Scheduler((PXGraph) this).MakeSchedule(schedule, times, new DateTime?(runDate));
    ARInvoiceEntry graph = this.CreateGraph();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (ScheduleDet occurrence in scheduleDets)
      {
        foreach (PXResult<ARInvoice, Customer, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>>>, Where<ARInvoice.scheduleID, Equal<Required<ARInvoice.scheduleID>>, And<ARInvoice.scheduled, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) schedule.ScheduleID
        }))
        {
          ((PXGraph) graph).Clear();
          ((PXSelectBase<Customer>) graph.customer).Current = PXResult<ARInvoice, Customer, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
          ARInvoice scheduledInvoice = PXResult<ARInvoice, Customer, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
          PX.Objects.CM.Extensions.CurrencyInfo scheduledInvoiceCurrencyInfo = PXResult<ARInvoice, Customer, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
          ARInvoice newInvoice = this.InsertDocument(graph, occurrence, PXResult<ARInvoice, Customer, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), PXResult<ARInvoice, Customer, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult), scheduledInvoiceCurrencyInfo);
          this.InsertDetails(graph, scheduledInvoice, newInvoice);
          BalanceCalculation.ForceDocumentControlTotals((PXGraph) graph, (IInvoice) newInvoice);
          try
          {
            ((PXSelectBase) graph.Document).Cache.SetDefaultExt<ARInvoice.hold>((object) newInvoice);
            ((PXSelectBase<ARInvoice>) graph.Document).Update(newInvoice);
            ((PXAction) graph.Save).Press();
          }
          catch
          {
            if (((PXSelectBase) graph.Document).Cache.IsInsertedUpdatedDeleted)
              throw;
          }
        }
        schedule.LastRunDate = occurrence.ScheduledDate;
        ((PXSelectBase) this.Running_Schedule).Cache.Update((object) schedule);
      }
      ((PXSelectBase) this.Running_Schedule).Cache.Persist((PXDBOperation) 1);
      transactionScope.Complete((PXGraph) this);
    }
    ((PXSelectBase) this.Running_Schedule).Cache.Persisted(false);
  }

  protected virtual ARInvoice InsertDocument(
    ARInvoiceEntry invoiceEntry,
    ScheduleDet occurrence,
    Customer customer,
    ARInvoice scheduledInvoice,
    PX.Objects.CM.Extensions.CurrencyInfo scheduledInvoiceCurrencyInfo)
  {
    if (scheduledInvoice.Released.GetValueOrDefault())
      throw new PXException("One of the scheduled documents is already released. Cannot generate new documents.");
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) invoiceEntry).GetExtension<ARInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(scheduledInvoiceCurrencyInfo, occurrence.ScheduledDate);
    ARInvoice copy1 = PXCache<ARInvoice>.CreateCopy(scheduledInvoice);
    copy1.CuryInfoID = currencyInfo.CuryInfoID;
    copy1.DocDate = occurrence.ScheduledDate;
    FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(copy1.BranchID), occurrence.ScheduledPeriod).GetValueOrRaiseError();
    copy1.FinPeriodID = valueOrRaiseError.FinPeriodID;
    copy1.TranPeriodID = (string) null;
    copy1.DueDate = new DateTime?();
    copy1.DiscDate = new DateTime?();
    copy1.CuryOrigDiscAmt = new Decimal?();
    copy1.OrigDiscAmt = new Decimal?();
    copy1.RefNbr = (string) null;
    copy1.Scheduled = new bool?(false);
    copy1.FromSchedule = new bool?(true);
    copy1.StatementDate = new DateTime?();
    copy1.CuryLineTotal = new Decimal?(0M);
    copy1.CuryVatTaxableTotal = new Decimal?(0M);
    copy1.CuryVatExemptTotal = new Decimal?(0M);
    copy1.NoteID = new Guid?();
    copy1.IsTaxValid = new bool?(false);
    copy1.IsTaxPosted = new bool?(false);
    copy1.IsTaxSaved = new bool?(false);
    copy1.OrigDocType = scheduledInvoice.DocType;
    copy1.OrigRefNbr = scheduledInvoice.RefNbr;
    copy1.Hold = new bool?(true);
    copy1.Approved = new bool?();
    copy1.DontApprove = new bool?();
    copy1.CuryDetailExtPriceTotal = new Decimal?(0M);
    copy1.DetailExtPriceTotal = new Decimal?(0M);
    copy1.CuryLineDiscTotal = new Decimal?(0M);
    copy1.LineDiscTotal = new Decimal?(0M);
    copy1.CuryMiscExtPriceTotal = new Decimal?(0M);
    copy1.MiscExtPriceTotal = new Decimal?(0M);
    copy1.CuryGoodsExtPriceTotal = new Decimal?(0M);
    copy1.GoodsExtPriceTotal = new Decimal?(0M);
    ((PXSelectBase) invoiceEntry.Document).Cache.SetDefaultExt<ARInvoice.printed>((object) copy1);
    ((PXSelectBase) invoiceEntry.Document).Cache.SetDefaultExt<ARInvoice.emailed>((object) copy1);
    bool flag1 = false;
    bool flag2 = false;
    if (copy1.PMInstanceID.HasValue)
    {
      PXResult<CustomerPaymentMethod, PX.Objects.CA.PaymentMethod> pxResult = (PXResult<CustomerPaymentMethod, PX.Objects.CA.PaymentMethod>) PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelectJoin<CustomerPaymentMethod, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CustomerPaymentMethod.paymentMethodID>>>, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) invoiceEntry, new object[1]
      {
        (object) copy1.PMInstanceID
      }));
      if (pxResult != null)
      {
        CustomerPaymentMethod customerPaymentMethod = PXResult<CustomerPaymentMethod, PX.Objects.CA.PaymentMethod>.op_Implicit(pxResult);
        PX.Objects.CA.PaymentMethod paymentMethod = PXResult<CustomerPaymentMethod, PX.Objects.CA.PaymentMethod>.op_Implicit(pxResult);
        if (customerPaymentMethod == null || !customerPaymentMethod.IsActive.GetValueOrDefault() || !paymentMethod.IsActive.GetValueOrDefault() || !paymentMethod.UseForAR.GetValueOrDefault())
        {
          flag2 = true;
          flag1 = true;
        }
      }
      else
      {
        flag2 = true;
        flag1 = true;
      }
    }
    else if (!string.IsNullOrEmpty(copy1.PaymentMethodID))
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) invoiceEntry, new object[1]
      {
        (object) copy1.PaymentMethodID
      }));
      if (paymentMethod == null || !paymentMethod.IsActive.GetValueOrDefault() || !paymentMethod.UseForAR.GetValueOrDefault())
      {
        flag2 = true;
        flag1 = true;
      }
    }
    if (flag2)
    {
      copy1.PMInstanceID = new int?();
      copy1.PaymentMethodID = (string) null;
      copy1.CashAccountID = new int?();
    }
    invoiceEntry.ClearRetainageSummary(copy1);
    KeyValueHelper.CopyAttributes(typeof (ARInvoice), ((PXSelectBase) invoiceEntry.Document).Cache, (object) scheduledInvoice, (object) copy1);
    ARInvoice data = ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Insert(copy1);
    ((PXSelectBase<Customer>) invoiceEntry.customer).Current = customer;
    if (flag1)
    {
      ARInvoice copy2 = PXCache<ARInvoice>.CreateCopy(data);
      copy2.PMInstanceID = new int?();
      copy2.PaymentMethodID = (string) null;
      copy2.CashAccountID = new int?();
      data = ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Update(copy2);
    }
    SharedRecordAttribute.CopyRecord<ARInvoice.billAddressID>(((PXSelectBase) invoiceEntry.Document).Cache, (object) data, (object) scheduledInvoice, false);
    SharedRecordAttribute.CopyRecord<ARInvoice.billContactID>(((PXSelectBase) invoiceEntry.Document).Cache, (object) data, (object) scheduledInvoice, false);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (ARInvoice)], (object) scheduledInvoice, ((PXSelectBase) invoiceEntry.Document).Cache, (object) data, (PXNoteAttribute.IPXCopySettings) null);
    return data;
  }

  protected virtual void InsertDetails(
    ARInvoiceEntry invoiceEntry,
    ARInvoice scheduledInvoice,
    ARInvoice newInvoice)
  {
    foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>.Config>.Select((PXGraph) invoiceEntry, new object[2]
    {
      (object) scheduledInvoice.DocType,
      (object) scheduledInvoice.RefNbr
    }))
    {
      ARTran arTran1 = PXResult<ARTran>.op_Implicit(pxResult);
      ARTran copy = PXCache<ARTran>.CreateCopy(arTran1);
      copy.FinPeriodID = (string) null;
      copy.TranPeriodID = (string) null;
      copy.RefNbr = (string) null;
      copy.CuryInfoID = new long?();
      copy.ManualPrice = new bool?(true);
      copy.ManualDisc = new bool?(true);
      copy.NoteID = new Guid?();
      ARTran arTran2 = ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Insert(copy);
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (ARTran)], (object) arTran1, ((PXSelectBase) invoiceEntry.Transactions).Cache, (object) arTran2, (PXNoteAttribute.IPXCopySettings) null);
    }
    foreach (PXResult<ARInvoiceDiscountDetail> pxResult in PXSelectBase<ARInvoiceDiscountDetail, PXSelect<ARInvoiceDiscountDetail, Where<ARInvoiceDiscountDetail.docType, Equal<Required<ARInvoiceDiscountDetail.docType>>, And<ARInvoiceDiscountDetail.refNbr, Equal<Required<ARInvoiceDiscountDetail.refNbr>>>>>.Config>.Select((PXGraph) invoiceEntry, new object[2]
    {
      (object) scheduledInvoice.DocType,
      (object) scheduledInvoice.RefNbr
    }))
    {
      ARInvoiceDiscountDetail copy = PXCache<ARInvoiceDiscountDetail>.CreateCopy(PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult));
      copy.RefNbr = (string) null;
      copy.CuryInfoID = new long?();
      copy.IsManual = new bool?(true);
      DiscountEngineProvider.GetEngineFor<ARTran, ARInvoiceDiscountDetail>().InsertDiscountDetail(((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails, copy);
    }
  }

  public virtual ARInvoiceEntry CreateGraph() => PXGraph.CreateInstance<ARInvoiceEntry>();
}
