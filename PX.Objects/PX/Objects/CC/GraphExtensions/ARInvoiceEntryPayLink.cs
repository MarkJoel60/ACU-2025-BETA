// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.ARInvoiceEntryPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.CA;
using PX.Objects.CC.Common;
using PX.Objects.CC.PaymentProcessing.Helpers;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.PayLink;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class ARInvoiceEntryPayLink : PayLinkDocumentGraph<ARInvoiceEntry, PX.Objects.AR.ARInvoice>
{
  public PXSelect<CCPayLink, Where<CCPayLink.payLinkID, Equal<Current<ARInvoicePayLink.payLinkID>>>> PayLink;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  private bool IsSOInvoice
  {
    get => ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.OrigModule == "SO";
  }

  [PXUIField(DisplayName = "Create Payment Link", Visible = true)]
  [PXButton(CommitChanges = true)]
  public override IEnumerable CreateLink(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARInvoiceEntryPayLink.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new ARInvoiceEntryPayLink.\u003C\u003Ec__DisplayClass4_0();
    this.SaveDoc();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.docs = adapter.Get<PX.Objects.AR.ARInvoice>().ToList<PX.Objects.AR.ARInvoice>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CCreateLink\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass40.docs;
  }

  [PXOverride]
  public virtual void ARInvoiceCreated(
    PX.Objects.AR.ARInvoice invoice,
    PX.Objects.AR.ARRegister doc,
    Action<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARRegister> baseMethod)
  {
    baseMethod(invoice, doc);
    if (ARInvoiceEntryPayLink.DocTypePayLinkAllowed(invoice.DocType))
      return;
    PXCache cache = ((PXSelectBase) this.Base.Document).Cache;
    cache.SetValueExt<ARInvoicePayLink.deliveryMethod>((object) invoice, (object) null);
    cache.SetValueExt<ARInvoicePayLink.processingCenterID>((object) invoice, (object) null);
    cache.SetValueExt<ARInvoicePayLink.payLinkID>((object) invoice, (object) null);
  }

  [PXOverride]
  public virtual void Persist(Action baseMethod)
  {
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    if (current != null && ARInvoiceEntryPayLink.DocTypePayLinkAllowed(current.DocType))
    {
      CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
      if (payLink != null && this.CheckPayLinkRelatedToDoc(payLink) && PayLinkHelper.PayLinkOpen(payLink))
      {
        Decimal? nullable1 = payLink.Amount;
        Decimal? curyDocBal1 = current.CuryDocBal;
        DateTime? nullable2;
        int num1;
        if (nullable1.GetValueOrDefault() == curyDocBal1.GetValueOrDefault() & nullable1.HasValue == curyDocBal1.HasValue)
        {
          DateTime? dueDate = payLink.DueDate;
          nullable2 = current.DueDate;
          DateTime dateTime = nullable2.Value;
          num1 = dueDate.HasValue ? (dueDate.GetValueOrDefault() != dateTime ? 1 : 0) : 1;
        }
        else
          num1 = 1;
        bool flag1 = num1 != 0;
        Decimal? valueOriginal1 = ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<PX.Objects.AR.ARInvoice.curyDocBal>((object) current) as Decimal?;
        DateTime? valueOriginal2 = ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<PX.Objects.AR.ARInvoice.dueDate>((object) current) as DateTime?;
        DateTime? nullable3;
        int num2;
        if (flag1)
        {
          Decimal? curyDocBal2 = current.CuryDocBal;
          nullable1 = valueOriginal1;
          if (curyDocBal2.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyDocBal2.HasValue == nullable1.HasValue)
          {
            nullable3 = current.DueDate;
            nullable2 = valueOriginal2;
            num2 = nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1;
          }
          else
            num2 = 1;
        }
        else
          num2 = 0;
        bool flag2 = num2 != 0;
        int num3;
        if (!string.IsNullOrEmpty(payLink.ReportAttachmentID))
        {
          nullable2 = payLink.DueDate;
          nullable3 = current.DueDate;
          DateTime dateTime = nullable3.Value;
          if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() != dateTime ? 1 : 0) : 1) != 0)
          {
            nullable2 = current.DueDate;
            nullable3 = valueOriginal2;
            num3 = nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0) : 0) : 1;
            goto label_14;
          }
        }
        num3 = 0;
label_14:
        bool flag3 = num3 != 0;
        if (flag2)
        {
          payLink.NeedSync = new bool?(flag2);
          CCPayLink ccPayLink = payLink;
          bool? needReportSync = ccPayLink.NeedReportSync;
          ccPayLink.NeedReportSync = flag3 ? new bool?(true) : needReportSync;
          ((PXSelectBase<CCPayLink>) this.PayLink).Update(payLink);
        }
      }
    }
    baseMethod();
  }

  public virtual void UpdatePayLinkAndCreatePayments(PayLinkData payLinkData)
  {
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    PX.Objects.AR.ARInvoice copy = ((PXSelectBase) this.Base.Document).Cache.CreateCopy((object) current) as PX.Objects.AR.ARInvoice;
    payLinkProcessing.UpdatePayLinkByData(payLink, payLinkData);
    if (payLinkData.Transactions != null)
    {
      if (payLinkData.Transactions.Any<TransactionData>())
      {
        try
        {
          this.CreatePayments(copy, payLink, payLinkData);
        }
        catch (Exception ex)
        {
          payLinkProcessing.SetErrorStatus(ex, payLink);
          throw;
        }
      }
    }
    payLinkProcessing.SetLinkStatus(payLink, payLinkData);
  }

  public override void CollectDataAndSyncLink()
  {
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    PX.Objects.AR.ARInvoice copy = ((PXSelectBase) this.Base.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current) as PX.Objects.AR.ARInvoice;
    PX.Objects.Extensions.PayLink.PayLinkDocument payLinkDoc = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    CCPayLink link = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    PayLinkData payments = payLinkProcessing.GetPayments(payLinkDoc, link);
    if (payments.Transactions != null)
    {
      if (payments.Transactions.Any<TransactionData>())
      {
        try
        {
          ARReleaseProcessPayLink.ActivateDoNotSyncFlag();
          this.CreatePayments(copy, link, payments);
        }
        catch (Exception ex)
        {
          payLinkProcessing.SetErrorStatus(ex, link);
          throw;
        }
        finally
        {
          ARReleaseProcessPayLink.ClearDoNotSyncFlag();
        }
      }
    }
    payLinkProcessing.SetLinkStatus(link, payments);
    PX.Objects.AR.ARInvoice invoiceFromDb = this.GetInvoiceFromDB(copy);
    bool? nullable = invoiceFromDb.OpenDoc;
    bool flag1 = false;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
    {
      Decimal? curyDocBal = invoiceFromDb.CuryDocBal;
      Decimal num = 0M;
      if (!(curyDocBal.GetValueOrDefault() == num & curyDocBal.HasValue))
        goto label_9;
    }
    if (payments.StatusCode == null)
    {
      payLinkProcessing.CloseLink(payLinkDoc, link, new PayLinkProcessingParams()
      {
        LinkGuid = link.NoteID,
        ExternalId = link.ExternalID
      });
      return;
    }
label_9:
    nullable = link.NeedSync;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue || payments.StatusCode == 1 || payments.PaymentStatusCode == 1)
      return;
    PayLinkProcessingParams syncLink = this.CollectDataToSyncLink(invoiceFromDb, link);
    this.DoPayLinkAction((Action<PayLinkProcessingParams>) (payLinkData => payLinkProcessing.SyncLink(payLinkDoc, link, payLinkData)), link, syncLink);
  }

  public virtual bool ProcessPayLinkFromRelatedOrders(PX.Objects.AR.ARInvoice inv)
  {
    bool flag = false;
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    foreach (Tuple<CCPayLink, PX.Objects.SO.SOOrder> tuple in this.GetPayLinkOrdersRelatedToInvoice(inv))
    {
      CCPayLink payLink = tuple.Item1;
      PX.Objects.SO.SOOrder order = tuple.Item2;
      SOOrderPayLink extension = ((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOOrder)].GetExtension<SOOrderPayLink>((object) order);
      if (!PayLinkHelper.PayLinkWasProcessed(payLink) && payLink.ExternalID != null)
      {
        PX.Objects.Extensions.PayLink.PayLinkDocument doc = new PX.Objects.Extensions.PayLink.PayLinkDocument();
        doc.OrderType = payLink.OrderType;
        doc.OrderNbr = payLink.OrderNbr;
        doc.DeliveryMethod = payLink.DeliveryMethod;
        doc.ProcessingCenterID = extension.ProcessingCenterID;
        PayLinkData payments = payLinkProcessing.GetPayments(doc, payLink);
        if (payments.Transactions != null)
        {
          if (payments.Transactions.Any<TransactionData>())
          {
            try
            {
              flag = true;
              this.CreatePayments(order, payLink, payments);
            }
            catch (Exception ex)
            {
              payLinkProcessing.SetErrorStatus(ex, payLink);
              throw;
            }
          }
        }
        payLinkProcessing.SetLinkStatus(payLink, payments);
        if (payments.PaymentStatusCode == 2 || payments.PaymentStatusCode == null)
          payLinkProcessing.CloseLink(doc, payLink, new PayLinkProcessingParams()
          {
            LinkGuid = payLink.NoteID,
            ExternalId = payLink.ExternalID
          });
      }
    }
    return flag;
  }

  public override void CollectDataAndCreateLink()
  {
    PX.Objects.AR.ARInvoice current1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    this.CheckDocBeforeLinkCreation(current1);
    PayLinkProcessingParams createLink = this.CollectDataToCreateLink(current1);
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    PX.Objects.Extensions.PayLink.PayLinkDocument current2 = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    CCPayLink payLink;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      payLink = payLinkProcessing.CreateLinkInDB(current2, createLink);
      ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Update(current2);
      ((PXAction) this.Base.Save).Press();
      transactionScope.Complete();
    }
    this.DoPayLinkAction((Action<PayLinkProcessingParams>) (payLinkData => payLinkProcessing.SendCreateLinkRequest(payLink, payLinkData)), payLink, createLink);
    if (!(current2.DeliveryMethod == "E"))
      return;
    List<FileAttachment> attachments = createLink.Attachments;
    this.SendNotification(attachments != null ? attachments.FirstOrDefault<FileAttachment>() : (FileAttachment) null);
  }

  public void CreateStandalonePayments(
    PX.Objects.AR.ARInvoice invoice,
    CCPayLink payLink,
    PayLinkData payLinkData)
  {
    this.CreatePayments(invoice, payLink, payLinkData, true);
  }

  public virtual void CreatePayments(PX.Objects.AR.ARInvoice invoice, CCPayLink payLink, PayLinkData payLinkData)
  {
    this.CreatePayments(invoice, payLink, payLinkData, false);
  }

  public override void SendNotification(FileAttachment attachment)
  {
    if (((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current.DeliveryMethod != "E")
      return;
    CCPayLink ccPayLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    PayLinkReportAttachmentParams attachmentParams = this.GetReportAttachmentParams();
    ARInvoiceEntry_ActivityDetailsExt extension = ((PXGraph) this.Base).GetExtension<ARInvoiceEntry_ActivityDetailsExt>();
    List<string> notificationCDs = new List<string>();
    notificationCDs.Add(this.IsSOInvoice ? "SO INVOICE" : "INVOICE PAY LINK");
    int? branchId = current.BranchID;
    Dictionary<string, string> reportParams = attachmentParams.ReportParams;
    NotificationGenerator notificationProvider = extension.CreateNotificationProvider("Customer", (IList<string>) notificationCDs, branchId, (IDictionary<string, string>) reportParams, (IList<Guid?>) null);
    notificationProvider.MassProcessMode = true;
    if (!notificationProvider.HasAttachments && !string.IsNullOrEmpty(ccPayLink.ReportAttachmentID))
    {
      if (attachment == null)
        attachment = this.GetReportAttachment();
      if (attachment != null)
        notificationProvider.AddAttachment(attachment.Name, attachment.Content);
    }
    if (!notificationProvider.Send().Any<CRSMEmail>())
      throw new PXException("Email send failed. Email isn't created or email recipient list is empty.");
  }

  /// <summary>
  /// Checks if the pay link is related to the current document.
  /// </summary>
  /// <param name="docType">AR Document Type</param>
  /// <returns></returns>
  public static bool DocTypePayLinkAllowed(string docType)
  {
    return docType != null && EnumerableExtensions.IsIn<string>(docType, "INV", "DRM", "FCH");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    PX.Objects.AR.ARInvoice row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache;
    if (row == null)
      return;
    bool flag1 = false;
    bool flag2 = false;
    CustomerClass custClass = ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>());
    if (custClass != null)
    {
      CustomerClassPayLink customerClassExt = this.GetCustomerClassExt(custClass);
      flag1 = customerClassExt.DisablePayLink.GetValueOrDefault();
      flag2 = customerClassExt.AllowOverrideDeliveryMethod.GetValueOrDefault();
    }
    PX.Objects.Extensions.PayLink.PayLinkDocument current = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    bool flag3 = ARInvoiceEntryPayLink.DocTypePayLinkAllowed(row.DocType);
    bool valueOrDefault = row.Released.GetValueOrDefault();
    bool flag4 = payLink == null || payLink.Url == null || PayLinkHelper.PayLinkWasProcessed(payLink);
    bool flag5 = payLink != null && PayLinkHelper.PayLinkWasProcessed(payLink) && payLink.PaymentStatus == "U";
    ((PXAction) this.createLink).SetEnabled(((!(flag3 & valueOrDefault) || !row.OpenDoc.GetValueOrDefault() ? 0 : (current.ProcessingCenterID != null ? 1 : 0)) & (flag4 ? 1 : 0)) != 0 && !flag1);
    ((PXAction) this.createLink).SetVisible(!flag1 & flag3);
    ((PXAction) this.syncLink).SetEnabled(flag3 & valueOrDefault && !flag4 && !flag1 && current.ProcessingCenterID != null);
    ((PXAction) this.syncLink).SetVisible(!flag1 & flag3);
    ((PXAction) this.resendLink).SetEnabled(flag3 & valueOrDefault && !flag4 && current.ProcessingCenterID != null && !flag1 && current.DeliveryMethod == "E");
    ((PXAction) this.resendLink).SetVisible(!flag1 & flag3);
    bool flag6 = flag1 && ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>())?.Url == null;
    bool flag7 = flag3 && !flag6;
    bool flag8 = flag7 && payLink?.Url == null | flag5;
    PXUIFieldAttribute.SetEnabled<ARInvoicePayLink.processingCenterID>(cache, (object) row, flag8);
    PXUIFieldAttribute.SetEnabled<ARInvoicePayLink.deliveryMethod>(cache, (object) row, flag8 & flag2);
    PXUIFieldAttribute.SetVisible<ARInvoicePayLink.processingCenterID>(cache, (object) row, flag7);
    PXUIFieldAttribute.SetVisible<ARInvoicePayLink.deliveryMethod>(cache, (object) row, flag7);
    PXUIFieldAttribute.SetVisible<CCPayLink.url>(((PXSelectBase) this.PayLink).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetVisible<CCPayLink.linkStatus>(((PXSelectBase) this.PayLink).Cache, (object) null, flag7);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CCPayLink> e)
  {
    CCPayLink row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CCPayLink>>) e).Cache;
    if (row == null)
      return;
    this.ShowActionStatusWarningIfNeeded(cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARInvoicePayLink.deliveryMethod> e)
  {
    if (!ARInvoiceEntryPayLink.DocTypePayLinkAllowed(e.Row is PX.Objects.AR.ARInvoice row ? row.DocType : (string) null))
      return;
    CustomerClass custClass = ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>());
    if (custClass == null)
      return;
    CustomerClassPayLink customerClassExt = this.GetCustomerClassExt(custClass);
    if (customerClassExt.DisablePayLink.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARInvoicePayLink.deliveryMethod>, object, object>) e).NewValue = (object) customerClassExt.DeliveryMethod;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice.curyID> e)
  {
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice.curyID>>) e).Cache;
    if (row == null)
      return;
    cache.SetDefaultExt<ARInvoicePayLink.processingCenterID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID> e)
  {
    PX.Objects.AR.ARInvoice row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID>>) e).Cache;
    int? newValue = e.NewValue as int?;
    int? oldValue = ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID>, PX.Objects.AR.ARInvoice, object>) e).OldValue as int?;
    if (row == null)
      return;
    int? nullable1 = newValue;
    int? nullable2 = oldValue;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    if (payLink != null && payLink.Url != null && !PayLinkHelper.PayLinkWasProcessed(payLink))
      return;
    cache.SetDefaultExt<ARInvoicePayLink.processingCenterID>((object) row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice.customerID> e)
  {
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice.customerID>>) e).Cache;
    if (row == null)
      return;
    cache.SetDefaultExt<ARInvoicePayLink.deliveryMethod>((object) row);
    cache.SetDefaultExt<ARInvoicePayLink.processingCenterID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARInvoicePayLink.processingCenterID> e)
  {
    if (!ARInvoiceEntryPayLink.DocTypePayLinkAllowed(e.Row is PX.Objects.AR.ARInvoice row ? row.DocType : (string) null))
      return;
    bool flag = false;
    CustomerClass custClass = ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>());
    if (custClass != null)
      flag = this.GetCustomerClassExt(custClass).DisablePayLink.GetValueOrDefault();
    if (flag)
      return;
    CCProcessingCenter processingCenter = PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, InnerJoin<PX.Objects.CA.CashAccount, On<CCProcessingCenter.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>, InnerJoin<CCProcessingCenterBranch, On<CCProcessingCenterBranch.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>>>, Where<PX.Objects.CA.CashAccount.curyID, Equal<Required<PX.Objects.CA.CashAccount.curyID>>, And<CCProcessingCenter.allowPayLink, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>, And<CCProcessingCenterBranch.defaultForBranch, Equal<True>, And<CCProcessingCenterBranch.branchID, Equal<Required<CCProcessingCenterBranch.branchID>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.CuryID,
      (object) row.BranchID
    }));
    if (processingCenter == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARInvoicePayLink.processingCenterID>, object, object>) e).NewValue = (object) processingCenter.ProcessingCenterID;
  }

  protected virtual IEnumerable<Tuple<CCPayLink, PX.Objects.SO.SOOrder>> GetPayLinkOrdersRelatedToInvoice(
    PX.Objects.AR.ARInvoice inv)
  {
    ARInvoiceEntry arInvoiceEntry = this.Base;
    object[] objArray = new object[2]
    {
      (object) inv.DocType,
      (object) inv.RefNbr
    };
    foreach (PXResult<PX.Objects.SO.SOOrder, CCPayLink, PX.Objects.SO.SOOrderShipment, PX.Objects.AR.ARRegister> pxResult in PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<CCPayLink, On<CCPayLink.payLinkID, Equal<SOOrderPayLink.payLinkID>>, InnerJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderShipment.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.SO.SOOrderShipment.invoiceType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PX.Objects.SO.SOOrderShipment.invoiceNbr>>>>>>, Where<PX.Objects.SO.SOOrderShipment.invoiceType, Equal<Required<PX.Objects.SO.SOOrderShipment.invoiceType>>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.invoiceNbr>>>>, OrderBy<Asc<PX.Objects.AR.ARRegister.createdDateTime>>>.Config>.Select((PXGraph) arInvoiceEntry, objArray))
      yield return new Tuple<CCPayLink, PX.Objects.SO.SOOrder>(PXResult<PX.Objects.SO.SOOrder, CCPayLink, PX.Objects.SO.SOOrderShipment, PX.Objects.AR.ARRegister>.op_Implicit(pxResult), PXResult<PX.Objects.SO.SOOrder, CCPayLink, PX.Objects.SO.SOOrderShipment, PX.Objects.AR.ARRegister>.op_Implicit(pxResult));
  }

  protected virtual PayLinkProcessingParams CollectDataToSyncLink(PX.Objects.AR.ARInvoice doc, CCPayLink payLink)
  {
    PayLinkProcessingParams syncLink = new PayLinkProcessingParams();
    syncLink.DueDate = doc.DueDate.Value;
    syncLink.LinkGuid = payLink.NoteID;
    syncLink.ExternalId = payLink.ExternalID;
    int num = payLink.NeedReportSync.GetValueOrDefault() ? 1 : 0;
    bool totalDetailItemOnly = num != 0 || !string.IsNullOrEmpty(payLink.ReportAttachmentID);
    this.CalculateAndSetLinkAmount(doc, syncLink, totalDetailItemOnly);
    if (num != 0)
      this.AttachReport(syncLink, payLink.ReportAttachmentID);
    return syncLink;
  }

  protected virtual PayLinkProcessingParams CollectDataToCreateLink(PX.Objects.AR.ARInvoice doc)
  {
    ARInvoicePayLink extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARInvoicePayLink>((object) doc);
    PayLinkProcessingParams createLink = new PayLinkProcessingParams();
    string processingCenterId = extension.ProcessingCenterID;
    CCProcessingCenter processingCenterById = this.GetPaymentProcessingRepo().GetProcessingCenterByID(processingCenterId);
    MeansOfPayment meansOfPayment = this.GetMeansOfPayment(((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current, ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>()));
    string str = this.GetCustomerProfileId(doc.CustomerID, extension.ProcessingCenterID) ?? this.GetPayLinkProcessing().CreateCustomerProfileId(doc.CustomerID, extension.ProcessingCenterID);
    createLink.MeansOfPayment = meansOfPayment;
    createLink.DueDate = doc.DueDate.Value;
    createLink.CustomerProfileId = str;
    PayLinkProcessingParams processingParams = createLink;
    bool? nullable = processingCenterById.AllowPartialPayment;
    int num = nullable.GetValueOrDefault() ? 1 : 0;
    processingParams.AllowPartialPayments = num != 0;
    createLink.FormTitle = this.CreateFormTitle(doc);
    nullable = processingCenterById.AttachDetailsToPayLink;
    bool valueOrDefault = nullable.GetValueOrDefault();
    this.CalculateAndSetLinkAmount(doc, createLink, valueOrDefault);
    if (valueOrDefault)
      this.AttachReport(createLink);
    return createLink;
  }

  protected virtual void CalculateAndSetLinkAmount(
    PX.Objects.AR.ARInvoice doc,
    PayLinkProcessingParams payLinkParams,
    bool totalDetailItemOnly)
  {
    Decimal num1 = 0M;
    PX.Objects.Extensions.PayLink.PayLinkDocument current = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    DocumentData documentData1 = new DocumentData();
    documentData1.DocType = doc.DocType;
    documentData1.DocRefNbr = doc.RefNbr;
    documentData1.DocBalance = doc.CuryDocBal.Value;
    payLinkParams.DocumentData = documentData1;
    List<DocumentDetailData> detailData = new List<DocumentDetailData>();
    Decimal num2 = num1 + this.PopulateDocDetailData(detailData, totalDetailItemOnly);
    documentData1.DocumentDetails = detailData;
    Decimal? headerTaxes = this.CalculateHeaderTaxes(doc);
    Decimal? nullable1 = headerTaxes;
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
    {
      documentData1.ExcludedTaxes = headerTaxes.Value;
      num2 += documentData1.ExcludedTaxes;
    }
    Decimal? docDiscounts = this.CalculateDocDiscounts(doc);
    Decimal? nullable2 = docDiscounts;
    Decimal num4 = 0M;
    if (!(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue))
    {
      documentData1.DocDiscounts = docDiscounts.Value;
      num2 += -1M * documentData1.DocDiscounts;
    }
    Decimal? curyFreightTot = doc.CuryFreightTot;
    Decimal num5 = 0M;
    if (!(curyFreightTot.GetValueOrDefault() == num5 & curyFreightTot.HasValue))
    {
      DocumentData documentData2 = documentData1;
      curyFreightTot = doc.CuryFreightTot;
      Decimal num6 = curyFreightTot.Value;
      documentData2.Freight = num6;
      num2 += documentData1.Freight;
    }
    List<AppliedDocumentData> aplDocData = new List<AppliedDocumentData>();
    Decimal num7 = num2 - this.PopulateAppliedDocData(aplDocData, current, payLinkParams);
    documentData1.AppliedDocuments = aplDocData;
    payLinkParams.Amount = num7;
  }

  protected virtual string CreateFormTitle(PX.Objects.AR.ARInvoice invoice)
  {
    PX.Objects.AR.Customer customer = this.GetCustomer(invoice.CustomerID);
    string str = string.Empty;
    if (invoice.InvoiceNbr != null)
      str += invoice.InvoiceNbr;
    if (customer.AcctName != null)
      str = $"{str} {customer.AcctName}";
    if (invoice.DocDesc != null)
      str = $"{str} {invoice.DocDesc}";
    return str.Trim();
  }

  protected override PayLinkReportAttachmentParams GetReportAttachmentParams()
  {
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    List<string> stringList;
    if (!this.IsSOInvoice)
    {
      stringList = new List<string>()
      {
        "INVOICE PAY LINK",
        "INVOICE"
      };
    }
    else
    {
      stringList = new List<string>();
      stringList.Add("SO INVOICE");
    }
    List<string> setupIdentifiers = stringList;
    string defaultReportID = this.IsSOInvoice ? "SO643000" : "AR641000";
    string reportId = this.GetReportID(current.CustomerID, current.BranchID, defaultReportID, setupIdentifiers);
    if (string.IsNullOrEmpty(reportId))
      return (PayLinkReportAttachmentParams) null;
    return new PayLinkReportAttachmentParams()
    {
      ReportID = reportId,
      ReportParams = this.GetReportParams(current),
      FileName = "invoice.pdf"
    };
  }

  private Dictionary<string, string> GetReportParams(PX.Objects.AR.ARInvoice doc)
  {
    return new Dictionary<string, string>()
    {
      {
        "DocType",
        doc.DocType
      },
      {
        "RefNbr",
        doc.RefNbr
      }
    };
  }

  private void CreatePayments(
    PX.Objects.AR.ARInvoice invoice,
    CCPayLink payLink,
    PayLinkData payLinkData,
    bool createStandalonePmt = false)
  {
    if (payLinkData.Transactions == null)
      return;
    PXException pxException = (PXException) null;
    PX.Objects.Extensions.PayLink.PayLinkDocument current1 = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    CCProcessingCenter processingCenterById = this.GetPaymentProcessingRepo().GetProcessingCenterByID(current1.ProcessingCenterID);
    foreach (TransactionData tranData in (IEnumerable<TransactionData>) payLinkData.Transactions.OrderBy<TransactionData, DateTime>((Func<TransactionData, DateTime>) (i => i.SubmitTime)))
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        CCTranType? tranType = tranData.TranType;
        CCTranType ccTranType = (CCTranType) 0;
        if (tranType.GetValueOrDefault() == ccTranType & tranType.HasValue)
        {
          if (tranData.TranStatus == null)
          {
            try
            {
              TranValidationHelper.CheckTranAlreadyRecorded(tranData, new TranValidationHelper.AdditionalParams()
              {
                ProcessingCenter = processingCenterById.ProcessingCenterID,
                Repo = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) this.Base)
              });
            }
            catch (TranValidationHelper.TranValidationException ex)
            {
              continue;
            }
            CCProcessingCenterBranch mappingRow = this.GetMappingRow(current1.BranchID, current1.ProcessingCenterID).Item2;
            try
            {
              this.CheckTranAgainstMapping(mappingRow, tranData);
            }
            catch (PXException ex)
            {
              pxException = ex;
              continue;
            }
            int? nullable1 = new int?();
            MeansOfPayment? paymentMethodType = tranData.PaymentMethodType;
            MeansOfPayment meansOfPayment = (MeansOfPayment) 0;
            string str;
            int? nullable2;
            if (paymentMethodType.GetValueOrDefault() == meansOfPayment & paymentMethodType.HasValue)
            {
              str = mappingRow.CCPaymentMethodID;
              nullable2 = mappingRow.CCCashAccountID;
            }
            else
            {
              str = mappingRow.EFTPaymentMethodID;
              nullable2 = mappingRow.EFTCashAccountID;
            }
            DateTime date = PXTimeZoneInfo.ConvertTimeFromUtc(tranData.SubmitTime, LocaleInfo.GetTimeZone()).Date;
            PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
            arPayment1.AdjDate = new DateTime?(date);
            arPayment1.BranchID = invoice.BranchID;
            arPayment1.DocType = "PMT";
            PX.Objects.AR.ARPayment arPayment2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Insert(arPayment1);
            arPayment2.CustomerID = invoice.CustomerID;
            arPayment2.CustomerLocationID = invoice.CustomerLocationID;
            arPayment2.ARAccountID = invoice.ARAccountID;
            arPayment2.ARSubID = invoice.ARSubID;
            arPayment2.PaymentMethodID = str;
            arPayment2.CuryOrigDocAmt = new Decimal?(tranData.Amount);
            arPayment2.DocDesc = invoice.DocDesc;
            PX.Objects.AR.ARPayment arPayment3 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(arPayment2);
            arPayment3.PMInstanceID = PaymentTranExtConstants.NewPaymentProfile;
            arPayment3.ProcessingCenterID = processingCenterById.ProcessingCenterID;
            arPayment3.CashAccountID = nullable2;
            arPayment3.Hold = new bool?(false);
            arPayment3.DocDesc = PXMessages.LocalizeFormatNoPrefix("Payment Link {0} {1}, External ID {2}", new object[3]
            {
              (object) payLink.DocType,
              (object) payLink.RefNbr,
              (object) payLink.ExternalID
            });
            PX.Objects.AR.ARPayment arPayment4 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(arPayment3);
            ((PXAction) instance.Save).Press();
            if (!createStandalonePmt)
            {
              Decimal? nullable3 = invoice.CuryDocBal;
              Decimal num = 0M;
              if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
              {
                if (invoice.PaymentsByLinesAllowed.GetValueOrDefault())
                {
                  foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<ARTran.curyTranBal, NotEqual<Zero>>>>, OrderBy<Desc<ARTran.curyTranBal>>>.Config>.Select((PXGraph) this.Base, new object[2]
                  {
                    (object) invoice.DocType,
                    (object) invoice.RefNbr
                  }))
                  {
                    ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
                    ((PXSelectBase<ARAdjust>) instance.Adjustments).Insert(new ARAdjust()
                    {
                      AdjdDocType = invoice.DocType,
                      AdjdRefNbr = invoice.RefNbr,
                      AdjdLineNbr = arTran.LineNbr
                    });
                    nullable3 = arPayment4.CuryApplAmt;
                    Decimal amount = tranData.Amount;
                    if (nullable3.GetValueOrDefault() >= amount & nullable3.HasValue)
                      break;
                  }
                }
                else
                  ((PXSelectBase<ARAdjust>) instance.Adjustments).Insert(new ARAdjust()
                  {
                    AdjdDocType = invoice.DocType,
                    AdjdRefNbr = invoice.RefNbr
                  });
                ((PXAction) instance.Save).Press();
              }
            }
            ARPaymentEntryPaymentTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>();
            CCPaymentEntry ccPaymentEntry = new CCPaymentEntry((PXGraph) instance);
            PX.Objects.AR.ARPayment current2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current;
            TransactionData details = tranData;
            CCPaymentEntry paymentEntry = ccPaymentEntry;
            extension.RecordTransaction(current2, details, paymentEntry);
            ((PXGraph) instance).Clear();
            transactionScope.Complete();
          }
        }
      }
    }
    if (pxException != null)
      throw pxException;
  }

  private void CheckDocBeforeLinkCreation(PX.Objects.AR.ARInvoice doc)
  {
    bool? openDoc = doc.OpenDoc;
    bool flag = false;
    if (openDoc.GetValueOrDefault() == flag & openDoc.HasValue)
      throw new PXException("Cannot create a link for the {0} document with the {1} reference number because the document has the invalid status.", new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      });
  }

  private Decimal PopulateAppliedDocData(
    List<AppliedDocumentData> aplDocData,
    PX.Objects.Extensions.PayLink.PayLinkDocument payLinkDoc,
    PayLinkProcessingParams payLinkParams)
  {
    Decimal num1 = 0M;
    bool flag1 = payLinkParams.ExternalId == null;
    IEnumerable<ARAdjust2> arAdjust2s = GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase<ARAdjust2>) this.Base.Adjustments).Select(Array.Empty<object>())).Where<ARAdjust2>((Func<ARAdjust2, bool>) (i =>
    {
      if (!i.Released.GetValueOrDefault())
        return false;
      bool? voided = i.Voided;
      bool flag2 = false;
      return voided.GetValueOrDefault() == flag2 & voided.HasValue;
    }));
    IEnumerable<PX.Objects.AR.ExternalTransaction> source = (IEnumerable<PX.Objects.AR.ExternalTransaction>) null;
    if (payLinkDoc.PayLinkID.HasValue && !flag1)
      source = this.GetPaymentProcessingRepo().GetExternalTransactionsByPayLinkID(payLinkDoc.PayLinkID);
    foreach (ARAdjust2 arAdjust2 in arAdjust2s)
    {
      ARAdjust2 detail = arAdjust2;
      bool flag3 = false;
      if (source != null && source.Any<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i => i.DocType == detail.AdjgDocType && i.RefNbr == detail.AdjgRefNbr)))
        flag3 = true;
      AppliedDocumentData appliedDocumentData = new AppliedDocumentData();
      Decimal? nullable = detail.CuryAdjdDiscAmt;
      Decimal num2 = nullable.Value;
      nullable = detail.CuryAdjdWOAmt;
      Decimal num3 = nullable.Value;
      Decimal num4 = num2 + num3;
      if (!flag3)
      {
        Decimal num5 = num4;
        nullable = detail.CuryAdjdAmt;
        Decimal num6 = nullable.Value;
        num4 = num5 + num6;
      }
      appliedDocumentData.Amount = num4;
      appliedDocumentData.DocRefNbr = detail.AdjgRefNbr;
      appliedDocumentData.DocType = detail.AdjgDocType;
      num1 += num4;
      aplDocData.Add(appliedDocumentData);
    }
    return num1;
  }

  protected override void SaveDoc() => ((PXAction) this.Base.Save).Press();

  public override void SetCurrentDocument(PX.Objects.AR.ARInvoice doc)
  {
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) doc.RefNbr, new object[1]
    {
      (object) doc.DocType
    }));
  }

  protected virtual PX.Objects.AR.ARInvoice GetInvoiceFromDB(PX.Objects.AR.ARInvoice doc)
  {
    return PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectReadonly<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
  }

  protected virtual ARInvoiceEntryPayLink.PayLinkDocumentMapping GetMapping()
  {
    return new ARInvoiceEntryPayLink.PayLinkDocumentMapping(typeof (PX.Objects.AR.ARInvoice));
  }

  private Decimal PopulateDocDetailData(
    List<DocumentDetailData> detailData,
    bool totalDetailItemOnly)
  {
    Decimal price = 0M;
    foreach (ARTran arTran in GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).Where<ARTran>((Func<ARTran, bool>) (i => EnumerableExtensions.IsNotIn<string>(i.LineType, "DS", "FR"))))
    {
      Decimal? nullable = arTran.CuryTranAmt;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      if (!totalDetailItemOnly)
      {
        DocumentDetailData documentDetailData1 = new DocumentDetailData();
        documentDetailData1.ItemName = arTran.TranDesc;
        documentDetailData1.Price = valueOrDefault;
        DocumentDetailData documentDetailData2 = documentDetailData1;
        nullable = arTran.Qty;
        Decimal num = nullable.Value;
        documentDetailData2.Quantity = num;
        documentDetailData1.Uom = arTran.UOM;
        documentDetailData1.LineNbr = arTran.LineNbr.Value;
        detailData.Add(documentDetailData1);
      }
      price += valueOrDefault;
    }
    if (totalDetailItemOnly)
      detailData.Add(this.GetSingleDetailItem(price));
    return price;
  }

  private Decimal? CalculateDocDiscounts(PX.Objects.AR.ARInvoice invoice)
  {
    Decimal num = 0M;
    foreach (ARInvoiceDiscountDetail invoiceDiscountDetail in GraphHelper.RowCast<ARInvoiceDiscountDetail>((IEnumerable) ((PXSelectBase<ARInvoiceDiscountDetail>) this.Base.ARDiscountDetails).Select(Array.Empty<object>())).Where<ARInvoiceDiscountDetail>((Func<ARInvoiceDiscountDetail, bool>) (i =>
    {
      bool? skipDiscount = i.SkipDiscount;
      bool flag = false;
      return skipDiscount.GetValueOrDefault() == flag & skipDiscount.HasValue;
    })))
      num += invoiceDiscountDetail.CuryDiscountAmt.GetValueOrDefault();
    return new Decimal?(num);
  }

  private Decimal? CalculateHeaderTaxes(PX.Objects.AR.ARInvoice invoice)
  {
    Decimal? headerTaxes = invoice.CuryTaxTotal;
    string taxCalcMode = invoice.TaxCalcMode;
    foreach (Tuple<ARTaxTran, PX.Objects.TX.Tax> tax in this.GetTaxes(invoice))
    {
      string taxCalcRule = tax.Item2.TaxCalcRule;
      Decimal? nullable = tax.Item1.CuryTaxAmt;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      string str = "I2";
      if (taxCalcMode == "G" && taxCalcRule != str)
      {
        nullable = headerTaxes;
        Decimal num = valueOrDefault;
        headerTaxes = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num) : new Decimal?();
      }
      else if (tax.Item2.TaxCalcLevel == "0" && taxCalcMode != "N")
      {
        nullable = headerTaxes;
        Decimal num = valueOrDefault;
        headerTaxes = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num) : new Decimal?();
      }
    }
    return headerTaxes;
  }

  private List<Tuple<ARTaxTran, PX.Objects.TX.Tax>> GetTaxes(PX.Objects.AR.ARInvoice invoice)
  {
    PXResultset<ARTaxTran> pxResultset = PXSelectBase<ARTaxTran, PXSelectJoin<ARTaxTran, InnerJoin<PX.Objects.TX.Tax, On<ARTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>, And<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) invoice.RefNbr,
      (object) invoice.DocType
    });
    List<Tuple<ARTaxTran, PX.Objects.TX.Tax>> taxes = new List<Tuple<ARTaxTran, PX.Objects.TX.Tax>>();
    foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax> pxResult in pxResultset)
      taxes.Add(Tuple.Create<ARTaxTran, PX.Objects.TX.Tax>(PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult), PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult)));
    return taxes;
  }

  private CustomerClassPayLink GetCustomerClassExt(CustomerClass custClass)
  {
    return ((PXSelectBase) this.Base.customerclass).Cache.GetExtension<CustomerClassPayLink>((object) custClass);
  }

  protected class PayLinkDocumentMapping : IBqlMapping
  {
    public System.Type DocType = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.docType);
    public System.Type RefNbr = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.refNbr);
    public System.Type BranchID = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.branchID);
    public System.Type ProcessingCenterID = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.processingCenterID);
    public System.Type DeliveryMethod = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.deliveryMethod);
    public System.Type PayLinkID = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.payLinkID);

    public System.Type Table { get; private set; }

    public System.Type Extension => typeof (PX.Objects.Extensions.PayLink.PayLinkDocument);

    public PayLinkDocumentMapping(System.Type table) => this.Table = table;
  }
}
