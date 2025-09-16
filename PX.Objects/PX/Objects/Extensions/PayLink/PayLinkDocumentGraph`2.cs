// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PayLink.PayLinkDocumentGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Services;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CA;
using PX.Objects.CC;
using PX.Objects.CC.Common;
using PX.Objects.CC.GraphExtensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.PayLink;

public abstract class PayLinkDocumentGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TPrimary : class, IBqlTable, new()
{
  public PXSelectExtension<PX.Objects.Extensions.PayLink.PayLinkDocument> PayLinkDocument;
  private ICCPaymentProcessingRepository _paymentProcRepo;
  public PXAction<TPrimary> createLink;
  public PXAction<TPrimary> syncLink;
  public PXAction<TPrimary> resendLink;

  [InjectDependency]
  public ICompanyService CompanyService { get; set; }

  [InjectDependency]
  private IReportLoaderService ReportLoader { get; set; }

  [InjectDependency]
  private IReportRenderer ReportRenderer { get; set; }

  [PXUIField(DisplayName = "Create Payment Link", Visible = true)]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable CreateLink(PXAdapter adapter)
  {
    this.SaveDoc();
    List<TPrimary> docs = adapter.Get<TPrimary>().ToList<TPrimary>();
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
    {
      foreach (TPrimary doc in docs)
      {
        PayLinkDocumentGraph<TGraph, TPrimary> implementation = PXGraph.CreateInstance<TGraph>().FindImplementation<PayLinkDocumentGraph<TGraph, TPrimary>>();
        implementation.SetCurrentDocument(doc);
        implementation.CollectDataAndCreateLink();
      }
    }));
    return (IEnumerable) docs;
  }

  [PXUIField(DisplayName = "Sync Payment Link", Visible = true)]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable SyncLink(PXAdapter adapter)
  {
    this.SaveDoc();
    List<TPrimary> docs = adapter.Get<TPrimary>().ToList<TPrimary>();
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
    {
      foreach (TPrimary doc in docs)
      {
        PayLinkDocumentGraph<TGraph, TPrimary> implementation = PXGraph.CreateInstance<TGraph>().FindImplementation<PayLinkDocumentGraph<TGraph, TPrimary>>();
        implementation.SetCurrentDocument(doc);
        implementation.CollectDataAndSyncLink();
      }
    }));
    return (IEnumerable) docs;
  }

  [PXUIField(DisplayName = "Resend Payment Link", Visible = true)]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable ResendLink(PXAdapter adapter)
  {
    this.SaveDoc();
    List<TPrimary> docs = adapter.Get<TPrimary>().ToList<TPrimary>();
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
    {
      foreach (TPrimary doc in docs)
      {
        PayLinkDocumentGraph<TGraph, TPrimary> implementation = PXGraph.CreateInstance<TGraph>().FindImplementation<PayLinkDocumentGraph<TGraph, TPrimary>>();
        implementation.SetCurrentDocument(doc);
        implementation.SendNotification();
      }
    }));
    return (IEnumerable) docs;
  }

  public void CreateStandalonePayments(PX.Objects.SO.SOOrder order, CCPayLink payLink, PayLinkData payLinkData)
  {
    this.CreatePayments(order, payLink, payLinkData, true);
  }

  public virtual void CreatePayments(PX.Objects.SO.SOOrder order, CCPayLink payLink, PayLinkData payLinkData)
  {
    this.CreatePayments(order, payLink, payLinkData, false);
  }

  protected virtual void ApplyPaymentToDocuments(ARPaymentEntry paymentGraph, PX.Objects.SO.SOOrder order)
  {
    SOAdjust soAdjust1 = (SOAdjust) null;
    bool flag1 = false;
    OrdersToApplyTab applyTabExtension = paymentGraph.GetOrdersToApplyTabExtension(true);
    Decimal? curyOrderTotal = order.CuryOrderTotal;
    Decimal? curyPaidAmt = order.CuryPaidAmt;
    Decimal? nullable1 = curyOrderTotal.HasValue & curyPaidAmt.HasValue ? new Decimal?(curyOrderTotal.GetValueOrDefault() - curyPaidAmt.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
    {
      bool? cancelled = order.Cancelled;
      bool flag2 = false;
      if (cancelled.GetValueOrDefault() == flag2 & cancelled.HasValue)
      {
        bool? completed = order.Completed;
        bool flag3 = false;
        if (completed.GetValueOrDefault() == flag3 & completed.HasValue)
        {
          SOAdjust soAdjust2 = new SOAdjust()
          {
            AdjdOrderType = order.OrderType,
            AdjdOrderNbr = order.OrderNbr
          };
          soAdjust1 = applyTabExtension.SOAdjustments.Insert(soAdjust2);
          paymentGraph.Save.Press();
          flag1 = true;
        }
      }
    }
    bool flag4 = false;
    foreach (PX.Objects.AR.ARInvoice arInvoice in EnumerableExtensions.Distinct<PX.Objects.AR.ARInvoice, string>(this.GetInvoicesRelatedToOrder(order).RowCast<PX.Objects.AR.ARInvoice>(), (Func<PX.Objects.AR.ARInvoice, string>) (i => i.DocType + i.RefNbr)))
    {
      PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
      Decimal? curyUnappliedBal1 = current.CuryUnappliedBal;
      Decimal? nullable2 = current.CurySOApplAmt;
      nullable1 = curyUnappliedBal1.HasValue & nullable2.HasValue ? new Decimal?(curyUnappliedBal1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? curyUnpaidBalance = arInvoice.CuryUnpaidBalance;
      Decimal? nullable3;
      if (!(nullable1.GetValueOrDefault() > curyUnpaidBalance.GetValueOrDefault() & nullable1.HasValue & curyUnpaidBalance.HasValue))
      {
        Decimal? curyUnappliedBal2 = current.CuryUnappliedBal;
        nullable1 = current.CurySOApplAmt;
        if (!(curyUnappliedBal2.HasValue & nullable1.HasValue))
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(curyUnappliedBal2.GetValueOrDefault() + nullable1.GetValueOrDefault());
      }
      else
        nullable3 = arInvoice.CuryUnpaidBalance;
      Decimal? nullable4 = nullable3;
      nullable1 = nullable4;
      Decimal num2 = 0M;
      if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
      {
        ARAdjust arAdjust1 = new ARAdjust()
        {
          AdjdDocType = arInvoice.DocType,
          AdjdRefNbr = arInvoice.RefNbr
        };
        if (flag1)
        {
          arAdjust1.AdjdOrderNbr = order.OrderNbr;
          arAdjust1.AdjdOrderType = order.OrderType;
        }
        ARAdjust arAdjust2 = paymentGraph.Adjustments.Insert(arAdjust1);
        arAdjust2.CuryAdjdAmt = nullable4;
        arAdjust2.CuryAdjgAmt = nullable4;
        paymentGraph.Adjustments.Update(arAdjust2);
        flag4 = true;
      }
    }
    if (!flag4)
      return;
    if (flag1)
    {
      PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
      Decimal? curyUnappliedBal3 = current.CuryUnappliedBal;
      Decimal? curySoApplAmt1 = current.CurySOApplAmt;
      Decimal? nullable5 = curyUnappliedBal3.HasValue & curySoApplAmt1.HasValue ? new Decimal?(curyUnappliedBal3.GetValueOrDefault() + curySoApplAmt1.GetValueOrDefault()) : new Decimal?();
      Decimal? curyUnpaidBalance = order.CuryUnpaidBalance;
      Decimal? nullable6;
      if (!(nullable5.GetValueOrDefault() > curyUnpaidBalance.GetValueOrDefault() & nullable5.HasValue & curyUnpaidBalance.HasValue))
      {
        Decimal? curyUnappliedBal4 = current.CuryUnappliedBal;
        Decimal? curySoApplAmt2 = current.CurySOApplAmt;
        nullable6 = curyUnappliedBal4.HasValue & curySoApplAmt2.HasValue ? new Decimal?(curyUnappliedBal4.GetValueOrDefault() + curySoApplAmt2.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable6 = order.CuryUnpaidBalance;
      Decimal? nullable7 = nullable6;
      SOAdjust soAdjust3 = applyTabExtension.SOAdjustments.Locate(soAdjust1);
      soAdjust3.CuryAdjgAmt = nullable7;
      soAdjust3.CuryAdjdAmt = nullable7;
      applyTabExtension.SOAdjustments.Update(soAdjust3);
    }
    paymentGraph.Save.Press();
  }

  protected virtual PX.Objects.CC.PaymentProcessing.PayLinkProcessing GetPayLinkProcessing()
  {
    return new PX.Objects.CC.PaymentProcessing.PayLinkProcessing(this.GetPaymentProcessingRepo());
  }

  protected virtual ICCPaymentProcessingRepository GetPaymentProcessingRepo()
  {
    if (this._paymentProcRepo == null)
      this._paymentProcRepo = CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
    return this._paymentProcRepo;
  }

  protected (CCProcessingCenter, CCProcessingCenterBranch) GetMappingRow(
    int? branchId,
    string procCenter)
  {
    (CCProcessingCenter, CCProcessingCenterBranch) andProcCenterIds = this.GetPaymentProcessingRepo().GetProcessingCenterBranchByBranchAndProcCenterIDs(branchId, procCenter);
    return andProcCenterIds.Item2 != null ? andProcCenterIds : throw new PXException("The mapping row for the {0} branch and the {1} processing center has not been found.", new object[2]
    {
      (object) PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, branchId).BranchCD,
      (object) procCenter
    });
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  protected virtual string[] GetKeysAsString(List<TPrimary> docs)
  {
    int count = docs.Count;
    string[] keysAsString = new string[count];
    List<System.Type> bqlKeys = this.Base.Caches[typeof (TPrimary)].BqlKeys;
    for (int index = 0; index < count; ++index)
    {
      string empty = string.Empty;
      foreach (System.Type type in bqlKeys)
        empty += this.Base.Caches[typeof (TPrimary)].GetValue((object) docs[index], type.Name)?.ToString();
      keysAsString[index] = empty;
    }
    return keysAsString;
  }

  protected string GetCompanyName(string userName)
  {
    return !this.CompanyService.IsMultiCompany ? this.CompanyService.GetSingleCompanyLoginName() : this.CompanyService.ExtractCompany(userName);
  }

  protected void CheckTranAgainstMapping(
    CCProcessingCenterBranch mappingRow,
    TransactionData tranData)
  {
    bool flag = true;
    MeansOfPayment? paymentMethodType = tranData.PaymentMethodType;
    MeansOfPayment meansOfPayment = (MeansOfPayment) 0;
    if (paymentMethodType.GetValueOrDefault() == meansOfPayment & paymentMethodType.HasValue && (mappingRow == null || mappingRow.CCPaymentMethodID == null || (mappingRow != null ? (!mappingRow.CCCashAccountID.HasValue ? 1 : 0) : 1) != 0))
      flag = false;
    if (tranData.PaymentMethodType.GetValueOrDefault() == 1 && (mappingRow == null || mappingRow.EFTPaymentMethodID == null || (mappingRow != null ? (!mappingRow.EFTCashAccountID.HasValue ? 1 : 0) : 1) != 0))
      flag = false;
    if (!flag)
      throw new PXException("There is no suitable payment method for the payment being created.");
  }

  protected MeansOfPayment GetMeansOfPayment(PX.Objects.Extensions.PayLink.PayLinkDocument doc, CustomerClass customerClass)
  {
    MeansOfPayment meansOfPayment = (MeansOfPayment) 2;
    if (doc == null)
      throw new PXArgumentException(nameof (doc), "The argument cannot be null.");
    if (customerClass == null)
      throw new PXArgumentException(nameof (customerClass), "The argument cannot be null.");
    CustomerClassPayLink extension = this.Base.Caches[typeof (CustomerClass)].GetExtension<CustomerClassPayLink>((object) customerClass);
    if (extension.PayLinkPaymentMethod == "C")
      meansOfPayment = (MeansOfPayment) 0;
    else if (extension.PayLinkPaymentMethod == "E")
      meansOfPayment = (MeansOfPayment) 1;
    CCProcessingCenterBranch row = this.GetMappingRow(doc.BranchID, doc.ProcessingCenterID).Item2;
    if (meansOfPayment == 2 && !this.CCMeansOfPmtIsReady(row) && this.EFTMeansOfPmtIsReady(row))
      meansOfPayment = (MeansOfPayment) 1;
    if (meansOfPayment == 2 && !this.EFTMeansOfPmtIsReady(row) && this.CCMeansOfPmtIsReady(row))
      meansOfPayment = (MeansOfPayment) 0;
    if (meansOfPayment == null && !this.CCMeansOfPmtIsReady(row))
      throw new PXException("There is no suitable payment method and cash account for the {0} means of payment.", new object[1]
      {
        (object) (MeansOfPayment) 0
      });
    return meansOfPayment != 1 || this.EFTMeansOfPmtIsReady(row) ? meansOfPayment : throw new PXException("There is no suitable payment method and cash account for the {0} means of payment.", new object[1]
    {
      (object) (MeansOfPayment) 1
    });
  }

  protected virtual void ShowActionStatusWarningIfNeeded(PXCache cache, CCPayLink payLink)
  {
    if (!(payLink.ActionStatus == "E") || payLink.ErrorMessage == null)
      return;
    cache.RaiseExceptionHandling<CCPayLink.linkStatus>((object) payLink, (object) payLink.Url, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("Payment Link processing error: {0}", (object) payLink.ErrorMessage), PXErrorLevel.Warning));
  }

  protected string GetCustomerProfileId(int? baccountID, string procCenterID)
  {
    return this.GetPaymentProcessingRepo().GetCustomerProcessingCenterByAccountAndProcCenterIDs(baccountID, procCenterID)?.CustomerCCPID;
  }

  protected virtual bool CheckPayLinkRelatedToDoc(CCPayLink payLink)
  {
    bool doc = true;
    PX.Objects.Extensions.PayLink.PayLinkDocument current = this.PayLinkDocument.Current;
    if (current != null && current.PayLinkID.HasValue)
    {
      int? valueOriginal = this.PayLinkDocument.Cache.GetValueOriginal<PX.Objects.Extensions.PayLink.PayLinkDocument.payLinkID>((object) current) as int?;
      int? payLinkId = current.PayLinkID;
      if (valueOriginal.GetValueOrDefault() == payLinkId.GetValueOrDefault() & valueOriginal.HasValue == payLinkId.HasValue)
        return doc;
      int status = (int) this.PayLinkDocument.Cache.GetStatus((object) current);
      if (status == 2)
        doc = false;
      bool flag = current.OrderType != null;
      if (status == 1)
      {
        if (flag && (current.OrderType != payLink.OrderType || current.OrderNbr != payLink.OrderNbr))
          doc = false;
        if (!flag && (current.DocType != payLink.DocType || current.RefNbr != payLink.RefNbr))
          doc = false;
      }
      if (!doc)
        this.PayLinkDocument.Cache.SetValue<PX.Objects.Extensions.PayLink.PayLinkDocument.payLinkID>((object) current, (object) null);
    }
    return doc;
  }

  protected virtual IEnumerable<PX.Objects.AR.ARInvoice> GetInvoicesRelatedToOrder(PX.Objects.SO.SOOrder order)
  {
    // ISSUE: variable of a boxed type
    __Boxed<TGraph> graph = (object) this.Base;
    object[] objArray = new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    };
    foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectReadonly2<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderShipment.invoiceType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>>>, Where<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>, And<PX.Objects.AR.ARRegister.openDoc, Equal<True>, And<PX.Objects.AR.ARRegister.curyDocBal, Greater<Zero>>>>>>, OrderBy<Asc<PX.Objects.AR.ARRegister.createdDateTime>>>.Config>.Select((PXGraph) graph, objArray))
      yield return (PX.Objects.AR.ARInvoice) pxResult;
  }

  protected virtual PX.Objects.AR.Customer GetCustomer(int? customerId)
  {
    return (PX.Objects.AR.Customer) PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, (object) customerId);
  }

  protected string GetReportID(
    int? customerID,
    int? branchID,
    string defaultReportID,
    List<string> setupIdentifiers)
  {
    PX.Objects.AR.Customer row = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, customerID);
    foreach (string setupIdentifier in setupIdentifiers)
    {
      (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) tuple = new NotificationUtility((PXGraph) this.Base).SearchSetup("Customer", setupIdentifier, branchID);
      NotificationSource source = new NotificationUtility((PXGraph) this.Base).GetSource("Customer", (object) row, (IList<Guid?>) new Guid?[2]
      {
        (Guid?) tuple.SetupWithBranch?.SetupID,
        (Guid?) tuple.SetupWithoutBranch?.SetupID
      }, branchID);
      if (!string.IsNullOrEmpty(source?.ReportID))
        return source.ReportID;
    }
    return defaultReportID;
  }

  protected FileAttachment GetReportAttachment()
  {
    PayLinkReportAttachmentParams attachmentParams = this.GetReportAttachmentParams();
    if (attachmentParams == null)
      return (FileAttachment) null;
    using (Report report = this.ReportLoader.LoadReport(attachmentParams.ReportID, (IPXResultset) null))
    {
      if (report == null)
        throw new PXException("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
        {
          (object) attachmentParams.ReportID
        });
      this.ReportLoader.InitDefaultReportParameters(report, (IDictionary<string, string>) attachmentParams.ReportParams);
      using (StreamManager streamManager = new StreamManager())
      {
        this.ReportRenderer.Render("PDF", report, (Hashtable) null, streamManager);
        return new FileAttachment()
        {
          Content = streamManager.MainStream.GetBytes(),
          Name = attachmentParams.FileName
        };
      }
    }
  }

  protected void DoPayLinkAction(
    System.Action<PayLinkProcessingParams> payLinkAction,
    CCPayLink payLink,
    PayLinkProcessingParams payLinkData)
  {
    try
    {
      payLinkAction(payLinkData);
    }
    catch (CCProcessingException ex) when (ex.Reason == 3)
    {
      throw new PXException("The {0} contact does not exist or is inactive in the {1} processing center.", new object[2]
      {
        (object) payLinkData.CustomerProfileId,
        (object) payLink.ProcessingCenterID
      });
    }
    catch (CCProcessingException ex) when (ex.Reason == 5)
    {
      if (payLinkData.Attachments == null)
      {
        if (payLinkData.DocumentData.DocumentDetails != null)
          payLinkData.DocumentData.DocumentDetails = new List<DocumentDetailData>()
          {
            this.GetSingleDetailItem(payLinkData.DocumentData.DocumentDetails.Sum<DocumentDetailData>((Func<DocumentDetailData, Decimal>) (d => d.Price)))
          };
        this.AttachReport(payLinkData, payLink.ReportAttachmentID);
        payLinkData.CheckLinkByGuid = false;
        payLinkAction(payLinkData);
      }
      else
        throw;
    }
  }

  protected DocumentDetailData GetSingleDetailItem(Decimal price)
  {
    return new DocumentDetailData()
    {
      LineNbr = 1,
      Quantity = 1M,
      ItemName = "Line Item Total",
      Price = price
    };
  }

  protected void AttachReport(PayLinkProcessingParams payLinkData, string attachmentID = null)
  {
    FileAttachment reportAttachment = this.GetReportAttachment();
    if (reportAttachment == null)
      return;
    reportAttachment.FileID = attachmentID;
    reportAttachment.OwnerID = payLinkData.ExternalId;
    payLinkData.Attachments = new List<FileAttachment>()
    {
      reportAttachment
    };
  }

  private void ApplyPaymentWithoutNeedSync(ARPaymentEntry paymentGraph, PX.Objects.SO.SOOrder order)
  {
    ARPaymentEntryPayLink extension = paymentGraph.GetExtension<ARPaymentEntryPayLink>();
    try
    {
      extension.DoNotSetNeedSync = true;
      this.ApplyPaymentToDocuments(paymentGraph, order);
    }
    finally
    {
      extension.DoNotSetNeedSync = false;
    }
  }

  private void CreatePayments(
    PX.Objects.SO.SOOrder order,
    CCPayLink payLink,
    PayLinkData payLinkData,
    bool createStandalonePmt)
  {
    // ISSUE: unable to decompile the method.
  }

  private bool CCMeansOfPmtIsReady(CCProcessingCenterBranch row)
  {
    return row.CCPaymentMethodID != null && row.CCCashAccountID.HasValue;
  }

  private bool EFTMeansOfPmtIsReady(CCProcessingCenterBranch row)
  {
    return row.EFTPaymentMethodID != null && row.EFTCashAccountID.HasValue;
  }

  protected abstract void SaveDoc();

  protected abstract PayLinkReportAttachmentParams GetReportAttachmentParams();

  public abstract void SetCurrentDocument(TPrimary doc);

  public abstract void CollectDataAndSyncLink();

  public abstract void CollectDataAndCreateLink();

  public abstract void SendNotification(FileAttachment attachment = null);
}
