// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.SOOrderEntryPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CC.Common;
using PX.Objects.CC.PaymentProcessing.Helpers;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.PayLink;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class SOOrderEntryPayLink : PayLinkDocumentGraph<SOOrderEntry, PX.Objects.SO.SOOrder>
{
  public PXSelect<CCPayLink, Where<CCPayLink.payLinkID, Equal<Current<SOOrderPayLink.payLinkID>>>> PayLink;
  public PXAction<PX.Objects.SO.SOOrder> deactivateLink;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) this.Base).Actions.Move("ResendLink", "DeactivateLink");
  }

  [PXUIField(DisplayName = "Close Payment Link", Visible = true)]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable DeactivateLink(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOOrderEntryPayLink.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new SOOrderEntryPayLink.\u003C\u003Ec__DisplayClass4_0();
    this.SaveDoc();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.docs = adapter.Get<PX.Objects.SO.SOOrder>().ToList<PX.Objects.SO.SOOrder>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CDeactivateLink\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass40.docs;
  }

  private void CheckDocBeforeLinkCreation(PX.Objects.SO.SOOrder doc)
  {
    Tuple<PX.Objects.AR.ARInvoice, CCPayLink> tuple = this.AllowedOrderStatus(doc) ? this.GetRelatedInvoicesWithPayLink(doc).FirstOrDefault<Tuple<PX.Objects.AR.ARInvoice, CCPayLink>>() : throw new PXException("Use the invoices related to the {0} sales order to create a payment link.", new object[1]
    {
      (object) doc.OrderNbr
    });
    if (tuple != null)
      throw new PXException("Use the {0} invoice that is related to the {1} sales order to process the payment link.", new object[2]
      {
        (object) tuple.Item1.RefNbr,
        (object) doc.OrderNbr
      });
  }

  public override void CollectDataAndCreateLink()
  {
    PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
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

  public override void SendNotification(FileAttachment attachment)
  {
    PX.Objects.Extensions.PayLink.PayLinkDocument current1 = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    if (current1.DeliveryMethod != "E")
      return;
    CCPayLink ccPayLink = CCPayLink.PK.Find((PXGraph) this.Base, current1.PayLinkID);
    PX.Objects.SO.SOOrder current2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PayLinkReportAttachmentParams attachmentParams = this.GetReportAttachmentParams();
    SOOrderEntry_ActivityDetailsExt extension = ((PXGraph) this.Base).GetExtension<SOOrderEntry_ActivityDetailsExt>();
    List<string> notificationCDs = new List<string>();
    notificationCDs.Add("SALES ORDER PAY LINK");
    int? branchId = current2.BranchID;
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

  public virtual void GetPaymentsAndCloseLink()
  {
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PX.Objects.Extensions.PayLink.PayLinkDocument current2 = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    PayLinkData payments = payLinkProcessing.GetPayments(current2, payLink);
    if (payments.Transactions != null)
    {
      if (payments.Transactions.Count<TransactionData>() > 0)
      {
        try
        {
          this.CreatePayments(current1, payLink, payments);
        }
        catch (Exception ex)
        {
          payLinkProcessing.SetErrorStatus(ex, payLink);
          throw;
        }
      }
    }
    payLinkProcessing.SetLinkStatus(payLink, payments);
    if (payments.StatusCode == 1)
      return;
    payLinkProcessing.CloseLink(current2, payLink, new PayLinkProcessingParams()
    {
      LinkGuid = payLink.NoteID,
      ExternalId = payLink.ExternalID
    });
  }

  public override void CollectDataAndSyncLink()
  {
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PX.Objects.Extensions.PayLink.PayLinkDocument payLinkDoc = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    CCPayLink link = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    PayLinkData payments = payLinkProcessing.GetPayments(payLinkDoc, link);
    if (payments.Transactions != null)
    {
      if (payments.Transactions.Count<TransactionData>() > 0)
      {
        try
        {
          this.CreatePayments(current, link, payments);
        }
        catch (Exception ex)
        {
          payLinkProcessing.SetErrorStatus(ex, link);
          throw;
        }
      }
    }
    payLinkProcessing.SetLinkStatus(link, payments);
    if (!current.Completed.GetValueOrDefault())
    {
      Decimal? curyUnpaidBalance = current.CuryUnpaidBalance;
      Decimal num = 0M;
      if (!(curyUnpaidBalance.GetValueOrDefault() == num & curyUnpaidBalance.HasValue))
        goto label_8;
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
label_8:
    bool? needSync = link.NeedSync;
    bool flag = false;
    if (needSync.GetValueOrDefault() == flag & needSync.HasValue || payments.StatusCode == 1 || payments.PaymentStatusCode == 1)
      return;
    PayLinkProcessingParams syncLink = this.CollectDataToSyncLink(current, link);
    this.DoPayLinkAction((Action<PayLinkProcessingParams>) (payLinkData => payLinkProcessing.SyncLink(payLinkDoc, link, payLinkData)), link, syncLink);
  }

  public virtual void UpdatePayLinkAndCreatePayments(PayLinkData payLinkData)
  {
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PX.Objects.CC.PaymentProcessing.PayLinkProcessing payLinkProcessing = this.GetPayLinkProcessing();
    PX.Objects.SO.SOOrder copy = ((PXSelectBase) this.Base.Document).Cache.CreateCopy((object) current) as PX.Objects.SO.SOOrder;
    payLinkProcessing.UpdatePayLinkByData(payLink, payLinkData);
    if (payLinkData.Transactions != null)
    {
      if (payLinkData.Transactions.Count<TransactionData>() > 0)
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

  [PXOverride]
  public virtual void Persist(Action baseMethod)
  {
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    if (payLink != null && this.CheckPayLinkRelatedToDoc(payLink) && PayLinkHelper.PayLinkOpen(payLink))
    {
      PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      Decimal? valueOriginal1 = ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<PX.Objects.SO.SOOrder.curyUnpaidBalance>((object) current) as Decimal?;
      Decimal? amount = payLink.Amount;
      Decimal? nullable1 = current.CuryUnpaidBalance;
      int num;
      if (!(amount.GetValueOrDefault() == nullable1.GetValueOrDefault() & amount.HasValue == nullable1.HasValue))
      {
        nullable1 = valueOriginal1;
        Decimal? curyUnpaidBalance = current.CuryUnpaidBalance;
        num = !(nullable1.GetValueOrDefault() == curyUnpaidBalance.GetValueOrDefault() & nullable1.HasValue == curyUnpaidBalance.HasValue) ? 1 : 0;
      }
      else
        num = 0;
      bool flag1 = num != 0;
      bool? nullable2 = payLink.NeedReportSync;
      bool flag2 = nullable2.GetValueOrDefault();
      if (!flag2 && !string.IsNullOrEmpty(payLink.ReportAttachmentID))
      {
        Decimal? valueOriginal2 = ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<PX.Objects.SO.SOOrder.curyDocBal>((object) current) as Decimal?;
        nullable1 = current.CuryDocBal;
        flag2 = !(valueOriginal2.GetValueOrDefault() == nullable1.GetValueOrDefault() & valueOriginal2.HasValue == nullable1.HasValue);
        if (!flag2)
        {
          foreach (SOLine soLine in GraphHelper.RowCast<SOLine>((IEnumerable) ((PXSelectBase<SOLine>) this.Base.Transactions).Select(Array.Empty<object>())))
          {
            nullable1 = ((PXSelectBase) this.Base.Transactions).Cache.GetValueOriginal<SOLine.curyLineAmt>((object) soLine) as Decimal?;
            Decimal? curyLineAmt = soLine.CuryLineAmt;
            flag2 = !(nullable1.GetValueOrDefault() == curyLineAmt.GetValueOrDefault() & nullable1.HasValue == curyLineAmt.HasValue);
            if (flag2)
              break;
          }
        }
        flag1 |= flag2;
      }
      if (flag1)
      {
        CCPayLink actualPayLink = this.GetActualPayLink(payLink);
        nullable2 = actualPayLink.NeedSync;
        bool flag3 = flag1;
        if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
        {
          nullable2 = actualPayLink.NeedReportSync;
          bool flag4 = flag2;
          if (nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue)
            goto label_18;
        }
        actualPayLink.NeedSync = new bool?(flag1);
        actualPayLink.NeedReportSync = new bool?(flag2);
        ((PXSelectBase<CCPayLink>) this.PayLink).Update(actualPayLink);
      }
    }
label_18:
    baseMethod();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache;
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
    bool flag3 = this.AllowedOrderType();
    bool flag4 = this.AllowedOrderStatus(row);
    bool flag5 = payLink == null || payLink.Url == null || PayLinkHelper.PayLinkWasProcessed(payLink);
    bool flag6 = payLink != null && PayLinkHelper.PayLinkOpen(payLink);
    bool flag7 = payLink != null && PayLinkHelper.PayLinkWasProcessed(payLink) && payLink.PaymentStatus == "U";
    bool flag8 = cache.GetStatus((object) row) != 2;
    ((PXAction) this.createLink).SetEnabled(((((!(flag3 & flag4) ? 0 : (current.ProcessingCenterID != null ? 1 : 0)) & (flag5 ? 1 : 0)) == 0 ? 0 : (!flag1 ? 1 : 0)) & (flag8 ? 1 : 0)) != 0);
    ((PXAction) this.createLink).SetVisible(!flag1 & flag3);
    ((PXAction) this.syncLink).SetEnabled(flag3 && !flag5 && !flag1 && current.ProcessingCenterID != null);
    ((PXAction) this.syncLink).SetVisible(!flag1 & flag3);
    ((PXAction) this.deactivateLink).SetEnabled(flag3 & flag6 && !flag5 && !flag1 && current.ProcessingCenterID != null);
    ((PXAction) this.deactivateLink).SetVisible(!flag1 & flag3);
    ((PXAction) this.resendLink).SetEnabled(flag3 && current.ProcessingCenterID != null && !flag5 && !flag1 && current.DeliveryMethod == "E");
    ((PXAction) this.resendLink).SetVisible(!flag1 & flag3);
    bool flag9 = (!flag1 ? 0 : (((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>())?.Url == null ? 1 : 0)) == 0 & flag3;
    bool flag10 = flag9 && payLink?.Url == null | flag7;
    PXUIFieldAttribute.SetEnabled<SOOrderPayLink.processingCenterID>(cache, (object) row, flag10);
    PXUIFieldAttribute.SetEnabled<SOOrderPayLink.deliveryMethod>(cache, (object) row, flag10 & flag2);
    PXUIFieldAttribute.SetVisible<SOOrderPayLink.processingCenterID>(cache, (object) row, flag9);
    PXUIFieldAttribute.SetVisible<SOOrderPayLink.deliveryMethod>(cache, (object) row, flag9);
    PXUIFieldAttribute.SetVisible<CCPayLink.url>(((PXSelectBase) this.PayLink).Cache, (object) null, flag9);
    PXUIFieldAttribute.SetVisible<CCPayLink.needSync>(((PXSelectBase) this.PayLink).Cache, (object) null, flag9);
    PXUIFieldAttribute.SetVisible<CCPayLink.linkStatus>(((PXSelectBase) this.PayLink).Cache, (object) null, flag9);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CCPayLink> e)
  {
    CCPayLink row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CCPayLink>>) e).Cache;
    if (row == null)
      return;
    this.ShowActionStatusWarningIfNeeded(cache, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if (row == null || e.Operation != 3)
      return;
    SOOrderPayLink extension = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder>>) e).Cache.GetExtension<SOOrderPayLink>((object) row);
    if (extension == null || !extension.PayLinkID.HasValue)
      return;
    CCPayLink payLink = CCPayLink.PK.Find((PXGraph) this.Base, extension.PayLinkID);
    if (payLink != null && payLink.Url != null && !PayLinkHelper.PayLinkWasProcessed(payLink))
      throw new PXRowPersistingException("linkStatus", (object) payLink.Url, "The {0} document with the {1} reference number has an active payment link. Close the payment link to cancel the order.", new object[2]
      {
        (object) payLink.OrderType,
        (object) payLink.OrderNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, SOOrderPayLink.deliveryMethod> e)
  {
    if (e.Row == null || !this.AllowedOrderType())
      return;
    CustomerClass custClass = ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>());
    if (custClass == null)
      return;
    CustomerClassPayLink customerClassExt = this.GetCustomerClassExt(custClass);
    if (customerClassExt.DisablePayLink.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, SOOrderPayLink.deliveryMethod>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) customerClassExt.DeliveryMethod;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyID> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if (row == null)
      return;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    if (payLink != null && payLink.Url != null && !PayLinkHelper.PayLinkWasProcessed(payLink))
      throw new PXSetPropertyException("The {0} document with the {1} reference number has an active payment link. Close the payment link to change the currency.", new object[2]
      {
        (object) row.OrderType,
        (object) row.OrderNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if (row == null)
      return;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    if (payLink == null || payLink.Url == null || PayLinkHelper.PayLinkWasProcessed(payLink))
      return;
    string str = PXMessages.LocalizeFormatNoPrefix("The {0} document with the {1} reference number has an active payment link. Close the payment link to change the customer.", new object[2]
    {
      (object) payLink.OrderType,
      (object) payLink.OrderNbr
    });
    this.Base.RaiseCustomerIDSetPropertyException(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>>) e).Cache, row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>, PX.Objects.SO.SOOrder, object>) e).NewValue, PXMessages.LocalizeFormatNoPrefix(str, Array.Empty<object>()));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    bool? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) e).NewValue as bool?;
    if (row == null)
      return;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    if (newValue.GetValueOrDefault() && payLink != null && payLink.Url != null && !PayLinkHelper.PayLinkWasProcessed(payLink))
      throw new PXException("The {0} document with the {1} reference number has an active payment link. Close the payment link to cancel the order.", new object[2]
      {
        (object) payLink.OrderType,
        (object) payLink.OrderNbr
      });
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyID> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyID>>) e).Cache.SetDefaultExt<SOOrderPayLink.processingCenterID>((object) row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.branchID> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.branchID>>) e).Cache;
    int? newValue = e.NewValue as int?;
    int? oldValue = ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.branchID>, PX.Objects.SO.SOOrder, object>) e).OldValue as int?;
    if (row == null)
      return;
    int? nullable1 = newValue;
    int? nullable2 = oldValue;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    CCPayLink payLink = ((PXSelectBase<CCPayLink>) this.PayLink).SelectSingle(Array.Empty<object>());
    if (payLink != null && payLink.Url != null && !PayLinkHelper.PayLinkWasProcessed(payLink))
      return;
    cache.SetDefaultExt<SOOrderPayLink.processingCenterID>((object) row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>>) e).Cache.SetDefaultExt<SOOrderPayLink.deliveryMethod>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>>) e).Cache.SetDefaultExt<SOOrderPayLink.processingCenterID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, SOOrderPayLink.processingCenterID> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if (row == null || !this.AllowedOrderType())
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
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, SOOrderPayLink.processingCenterID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) processingCenter.ProcessingCenterID;
  }

  protected virtual string CreateFormTitle(PX.Objects.SO.SOOrder doc)
  {
    PX.Objects.AR.Customer customer = this.GetCustomer(doc.CustomerID);
    string str = string.Empty;
    if (doc.CustomerOrderNbr != null)
      str += doc.CustomerOrderNbr;
    if (customer.AcctName != null)
      str = $"{str} {customer.AcctName}";
    if (doc.OrderDesc != null)
      str = $"{str} {doc.OrderDesc}";
    return str.Trim();
  }

  protected virtual PayLinkProcessingParams CollectDataToCreateLink(PX.Objects.SO.SOOrder doc)
  {
    PX.Objects.Extensions.PayLink.PayLinkDocument current = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    PayLinkProcessingParams createLink = new PayLinkProcessingParams();
    string processingCenterId = current.ProcessingCenterID;
    CCProcessingCenter processingCenterById = this.GetPaymentProcessingRepo().GetProcessingCenterByID(processingCenterId);
    MeansOfPayment meansOfPayment = this.GetMeansOfPayment(current, ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>()));
    string str = this.GetCustomerProfileId(doc.CustomerID, current.ProcessingCenterID) ?? this.GetPayLinkProcessing().CreateCustomerProfileId(doc.CustomerID, current.ProcessingCenterID);
    createLink.MeansOfPayment = meansOfPayment;
    createLink.DueDate = doc.RequestDate.Value;
    createLink.CustomerProfileId = str;
    createLink.AllowPartialPayments = processingCenterById.AllowPartialPayment.GetValueOrDefault();
    createLink.FormTitle = this.CreateFormTitle(doc);
    bool valueOrDefault = processingCenterById.AttachDetailsToPayLink.GetValueOrDefault();
    this.CalculateAndSetLinkAmount(doc, createLink, valueOrDefault);
    if (valueOrDefault)
      this.AttachReport(createLink);
    return createLink;
  }

  protected virtual PayLinkProcessingParams CollectDataToSyncLink(PX.Objects.SO.SOOrder doc, CCPayLink payLink)
  {
    PayLinkProcessingParams syncLink = new PayLinkProcessingParams();
    syncLink.DueDate = doc.RequestDate.Value;
    syncLink.LinkGuid = payLink.NoteID;
    syncLink.ExternalId = payLink.ExternalID;
    int num = payLink.NeedReportSync.GetValueOrDefault() ? 1 : 0;
    bool totalDetailItemOnly = num != 0 || !string.IsNullOrEmpty(payLink.ReportAttachmentID);
    this.CalculateAndSetLinkAmount(doc, syncLink, totalDetailItemOnly);
    if (num != 0)
      this.AttachReport(syncLink, payLink.ReportAttachmentID);
    return syncLink;
  }

  protected virtual void CalculateAndSetLinkAmount(
    PX.Objects.SO.SOOrder doc,
    PayLinkProcessingParams payLinkParams,
    bool totalDetailItemOnly)
  {
    Decimal num1 = 0M;
    PX.Objects.Extensions.PayLink.PayLinkDocument current = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    DocumentData documentData1 = new DocumentData();
    documentData1.DocType = doc.OrderType;
    documentData1.DocRefNbr = doc.OrderNbr;
    documentData1.DocBalance = doc.CuryUnpaidBalance.Value;
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

  [PXOverride]
  public virtual void OrderCreated(
    PX.Objects.SO.SOOrder document,
    PX.Objects.SO.SOOrder source,
    SOOrderEntry.OrderCreatedDelegate baseMethod)
  {
    baseMethod(document, source);
    PX.Objects.Extensions.PayLink.PayLinkDocument current = ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Current;
    if (current == null)
      return;
    int? nullable1 = current.PayLinkID;
    if (!nullable1.HasValue)
      return;
    PX.Objects.Extensions.PayLink.PayLinkDocument payLinkDocument = current;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    payLinkDocument.PayLinkID = nullable2;
    ((PXSelectBase<PX.Objects.Extensions.PayLink.PayLinkDocument>) this.PayLinkDocument).Update(current);
  }

  protected virtual CCPayLink GetActualPayLink(CCPayLink payLink)
  {
    ((PXGraph) this.Base).Caches[typeof (CCPayLink)].ClearQueryCache();
    return PXResultset<CCPayLink>.op_Implicit(PXSelectBase<CCPayLink, PXSelect<CCPayLink, Where<CCPayLink.payLinkID, Equal<Required<CCPayLink.payLinkID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) payLink.PayLinkID
    }));
  }

  private Decimal PopulateDocDetailData(
    List<DocumentDetailData> detailData,
    bool totalDetailItemOnly)
  {
    Decimal price = 0M;
    foreach (SOLine soLine in GraphHelper.RowCast<SOLine>((IEnumerable) ((PXSelectBase<SOLine>) this.Base.Transactions).Select(Array.Empty<object>())))
    {
      Decimal? nullable = soLine.CuryLineAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      if (!totalDetailItemOnly)
      {
        DocumentDetailData documentDetailData1 = new DocumentDetailData();
        documentDetailData1.ItemName = soLine.TranDesc;
        DocumentDetailData documentDetailData2 = documentDetailData1;
        nullable = soLine.CuryLineAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        documentDetailData2.Price = valueOrDefault2;
        DocumentDetailData documentDetailData3 = documentDetailData1;
        nullable = soLine.Qty;
        Decimal num = nullable.Value;
        documentDetailData3.Quantity = num;
        documentDetailData1.Uom = soLine.UOM;
        documentDetailData1.LineNbr = soLine.LineNbr.Value;
        detailData.Add(documentDetailData1);
      }
      price += valueOrDefault1;
    }
    if (totalDetailItemOnly)
      detailData.Add(this.GetSingleDetailItem(price));
    return price;
  }

  protected virtual SOOrderEntryPayLink.PayLinkDocumentMapping GetMapping()
  {
    return new SOOrderEntryPayLink.PayLinkDocumentMapping(typeof (PX.Objects.SO.SOOrder));
  }

  public override void SetCurrentDocument(PX.Objects.SO.SOOrder doc)
  {
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) doc.OrderNbr, new object[1]
    {
      (object) doc.OrderType
    }));
  }

  protected override void SaveDoc() => ((PXAction) this.Base.Save).Press();

  protected virtual IEnumerable<Tuple<PX.Objects.AR.ARInvoice, CCPayLink>> GetRelatedInvoicesWithPayLink(
    PX.Objects.SO.SOOrder order)
  {
    SOOrderEntry soOrderEntry = this.Base;
    object[] objArray = new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    };
    foreach (PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOOrderShipment, PX.Objects.AR.ARRegister, CCPayLink> pxResult in PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderShipment.invoiceType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, InnerJoin<CCPayLink, On<CCPayLink.payLinkID, Equal<ARInvoicePayLink.payLinkID>>>>>, Where<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>, And<PX.Objects.AR.ARRegister.openDoc, Equal<True>, And<PX.Objects.AR.ARRegister.curyDocBal, Greater<Zero>>>>>>, OrderBy<Asc<PX.Objects.AR.ARRegister.createdDateTime>>>.Config>.Select((PXGraph) soOrderEntry, objArray))
    {
      CCPayLink ccPayLink = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOOrderShipment, PX.Objects.AR.ARRegister, CCPayLink>.op_Implicit(pxResult);
      yield return new Tuple<PX.Objects.AR.ARInvoice, CCPayLink>(PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOOrderShipment, PX.Objects.AR.ARRegister, CCPayLink>.op_Implicit(pxResult), ccPayLink);
    }
  }

  private Decimal PopulateAppliedDocData(
    List<AppliedDocumentData> aplDocData,
    PX.Objects.Extensions.PayLink.PayLinkDocument payLinkDoc,
    PayLinkProcessingParams payLinkParams)
  {
    Decimal num1 = 0M;
    bool flag1 = payLinkParams.ExternalId == null;
    IEnumerable<SOAdjust> soAdjusts = GraphHelper.RowCast<SOAdjust>((IEnumerable) ((PXSelectBase<SOAdjust>) this.Base.Adjustments).Select(Array.Empty<object>())).Where<SOAdjust>((Func<SOAdjust, bool>) (i =>
    {
      bool? voided = i.Voided;
      bool flag2 = false;
      return voided.GetValueOrDefault() == flag2 & voided.HasValue;
    }));
    IEnumerable<PX.Objects.AR.ExternalTransaction> source = (IEnumerable<PX.Objects.AR.ExternalTransaction>) null;
    if (payLinkDoc.PayLinkID.HasValue && !flag1)
      source = this.GetPaymentProcessingRepo().GetExternalTransactionsByPayLinkID(payLinkDoc.PayLinkID);
    foreach (SOAdjust soAdjust in soAdjusts)
    {
      SOAdjust detail = soAdjust;
      if (source == null || !source.Any<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i => i.DocType == detail.AdjgDocType && i.RefNbr == detail.AdjgRefNbr)))
      {
        AppliedDocumentData appliedDocumentData1 = new AppliedDocumentData();
        AppliedDocumentData appliedDocumentData2 = appliedDocumentData1;
        Decimal? nullable = detail.CuryAdjdAmt;
        Decimal num2 = nullable.Value;
        nullable = detail.CuryAdjdBilledAmt;
        Decimal num3 = nullable.Value;
        Decimal num4 = num2 + num3;
        nullable = detail.CuryAdjdDiscAmt;
        Decimal num5 = nullable.Value;
        Decimal num6 = num4 + num5;
        appliedDocumentData2.Amount = num6;
        appliedDocumentData1.DocRefNbr = detail.AdjgRefNbr;
        appliedDocumentData1.DocType = detail.AdjgDocType;
        num1 += appliedDocumentData1.Amount;
        aplDocData.Add(appliedDocumentData1);
      }
    }
    return num1;
  }

  protected override PayLinkReportAttachmentParams GetReportAttachmentParams()
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    List<string> setupIdentifiers = new List<string>()
    {
      "SALES ORDER PAY LINK",
      "SALES ORDER"
    };
    string defaultReportID = "SO641010";
    string reportId = this.GetReportID(current.CustomerID, current.BranchID, defaultReportID, setupIdentifiers);
    if (string.IsNullOrEmpty(reportId))
      return (PayLinkReportAttachmentParams) null;
    return new PayLinkReportAttachmentParams()
    {
      ReportID = reportId,
      ReportParams = this.GetReportParams(current),
      FileName = "sales_order.pdf"
    };
  }

  private Dictionary<string, string> GetReportParams(PX.Objects.SO.SOOrder doc)
  {
    return new Dictionary<string, string>()
    {
      {
        "OrderType",
        doc.OrderType
      },
      {
        "OrderNbr",
        doc.OrderNbr
      }
    };
  }

  private Decimal? CalculateDocDiscounts(PX.Objects.SO.SOOrder order)
  {
    Decimal num = 0M;
    foreach (SOOrderDiscountDetail orderDiscountDetail in GraphHelper.RowCast<SOOrderDiscountDetail>((IEnumerable) ((PXSelectBase<SOOrderDiscountDetail>) this.Base.DiscountDetails).Select(Array.Empty<object>())).Where<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>) (i =>
    {
      bool? skipDiscount = i.SkipDiscount;
      bool flag = false;
      return skipDiscount.GetValueOrDefault() == flag & skipDiscount.HasValue;
    })))
      num += orderDiscountDetail.CuryDiscountAmt.GetValueOrDefault();
    return new Decimal?(num);
  }

  private Decimal? CalculateHeaderTaxes(PX.Objects.SO.SOOrder order)
  {
    Decimal? headerTaxes = order.CuryTaxTotal;
    string taxCalcMode = order.TaxCalcMode;
    foreach (Tuple<SOTaxTran, PX.Objects.TX.Tax> tax in this.GetTaxes(order))
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

  private List<Tuple<SOTaxTran, PX.Objects.TX.Tax>> GetTaxes(PX.Objects.SO.SOOrder order)
  {
    PXResultset<SOTaxTran> pxResultset = PXSelectBase<SOTaxTran, PXSelectJoin<SOTaxTran, InnerJoin<PX.Objects.TX.Tax, On<SOTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<SOTaxTran.orderNbr, Equal<Required<SOTaxTran.orderNbr>>, And<SOTaxTran.orderType, Equal<Required<SOTaxTran.orderType>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) order.OrderNbr,
      (object) order.OrderType
    });
    List<Tuple<SOTaxTran, PX.Objects.TX.Tax>> taxes = new List<Tuple<SOTaxTran, PX.Objects.TX.Tax>>();
    foreach (PXResult<SOTaxTran, PX.Objects.TX.Tax> pxResult in pxResultset)
      taxes.Add(Tuple.Create<SOTaxTran, PX.Objects.TX.Tax>(PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult), PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult)));
    return taxes;
  }

  private CustomerClassPayLink GetCustomerClassExt(CustomerClass custClass)
  {
    return ((PXSelectBase) this.Base.customerclass).Cache.GetExtension<CustomerClassPayLink>((object) custClass);
  }

  private bool AllowedOrderStatus(PX.Objects.SO.SOOrder doc)
  {
    bool? completed = doc.Completed;
    bool flag1 = false;
    if (completed.GetValueOrDefault() == flag1 & completed.HasValue)
    {
      bool? cancelled = doc.Cancelled;
      bool flag2 = false;
      if (cancelled.GetValueOrDefault() == flag2 & cancelled.HasValue)
      {
        bool? isExpired = doc.IsExpired;
        bool flag3 = false;
        if (isExpired.GetValueOrDefault() == flag3 & isExpired.HasValue && (doc.Approved.GetValueOrDefault() || doc.Hold.GetValueOrDefault()))
        {
          Decimal? curyUnpaidBalance = doc.CuryUnpaidBalance;
          Decimal num1 = 0M;
          if (curyUnpaidBalance.GetValueOrDefault() > num1 & curyUnpaidBalance.HasValue)
          {
            if (EnumerableExtensions.IsNotIn<string>(doc.Behavior, "IN", "MO"))
              return true;
            int? billedCntr = doc.BilledCntr;
            int num2 = 0;
            return billedCntr.GetValueOrDefault() == num2 & billedCntr.HasValue;
          }
        }
      }
    }
    return false;
  }

  private bool AllowedOrderType()
  {
    return ((bool?) ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current?.CanHavePayments).GetValueOrDefault();
  }

  protected class PayLinkDocumentMapping : IBqlMapping
  {
    public System.Type OrderType = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.orderType);
    public System.Type OrderNbr = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.orderNbr);
    public System.Type BranchID = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.branchID);
    public System.Type ProcessingCenterID = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.processingCenterID);
    public System.Type DeliveryMethod = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.deliveryMethod);
    public System.Type PayLinkID = typeof (PX.Objects.Extensions.PayLink.PayLinkDocument.payLinkID);

    public System.Type Table { get; private set; }

    public System.Type Extension => typeof (PX.Objects.Extensions.PayLink.PayLinkDocument);

    public PayLinkDocumentMapping(System.Type table) => this.Table = table;
  }
}
