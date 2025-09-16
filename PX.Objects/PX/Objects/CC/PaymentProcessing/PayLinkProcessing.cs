// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PaymentProcessing.PayLinkProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using PX.Objects.CC.PaymentProcessing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.PaymentProcessing;

public class PayLinkProcessing
{
  private ICCPaymentProcessingRepository _repository;
  private PXGraph _graph;

  public PayLinkProcessing(ICCPaymentProcessingRepository repo)
  {
    this._repository = repo;
    this._graph = repo.Graph;
  }

  public void SetErrorStatus(Exception ex, CCPayLink payLink)
  {
    string message = ex.Message;
    if (ex is PXOuterException pxOuterException && pxOuterException.InnerMessages.Length != 0)
      message = string.Join("\r\n", pxOuterException.InnerMessages);
    payLink.ActionStatus = "E";
    payLink.ErrorMessage = this.ShrinkMessage(message);
    this.SetStatusDate(payLink);
    this.UpdatePayLink(payLink);
    this.Save();
  }

  public string CreateCustomerProfileId(int? baccountID, string procCenterID)
  {
    string customerProfile = CCCustomerInformationManager.CreateCustomerProfile(this._repository.Graph, baccountID, procCenterID);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this._repository.Graph.Caches[typeof (CustomerProcessingCenterID)].Insert((object) new CustomerProcessingCenterID()
      {
        BAccountID = baccountID,
        CCProcessingCenterID = procCenterID,
        CustomerCCPID = customerProfile
      });
      this._repository.Graph.Persist();
      transactionScope.Complete();
    }
    return customerProfile;
  }

  public void CreateWebhook(CCProcessingCenter procCenter, WebHook webHook)
  {
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = procCenter,
      callerGraph = this._repository.Graph
    });
    this.CreatePluginAndGetWebhookProcessor(procCenter, provider).AddWebhook(new Webhook()
    {
      Url = webHook.Url
    });
  }

  public PayLinkData GetPayments(PX.Objects.Extensions.PayLink.PayLinkDocument doc, CCPayLink payLink)
  {
    CCProcessingCenter processingCenter = this.GetProcessingCenter(doc.ProcessingCenterID);
    this.CheckProcessingCenter(processingCenter);
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = processingCenter,
      callerGraph = this._repository.Graph
    });
    ICCPayLinkProcessor processor = this.CreatePluginAndGetPayLinkProcessor(processingCenter, provider);
    payLink.Action = "R";
    payLink.ActionStatus = "O";
    payLink.ErrorMessage = (string) null;
    this.SetStatusDate(payLink);
    payLink = this.UpdatePayLink(payLink);
    this.Save();
    PayLinkSearchParams payLinkParams = new PayLinkSearchParams()
    {
      ExternalId = payLink.ExternalID
    };
    PayLinkData res = this.SendRequestAndHandleError<PayLinkData>((Func<PayLinkData>) (() => processor.GetLinkWithTransactions(payLinkParams)), payLink);
    this.UpdatePayLinkByData(payLink, res);
    return res;
  }

  public void SetLinkStatus(CCPayLink payLink, PayLinkData res)
  {
    if (res.StatusCode != 1)
      return;
    payLink.LinkStatus = "C";
    payLink.NeedSync = new bool?(false);
    payLink.NeedReportSync = new bool?(false);
    this.UpdatePayLink(payLink);
    this.Save();
  }

  public void UpdatePayLinkByData(CCPayLink payLink, PayLinkData res)
  {
    this.SetStatuses(payLink, res);
    if (payLink.LinkStatus == "C")
    {
      payLink.NeedSync = new bool?(false);
      payLink.NeedReportSync = new bool?(false);
    }
    this.UpdatePayLink(payLink);
    this.Save();
  }

  public void CloseLink(
    PX.Objects.Extensions.PayLink.PayLinkDocument doc,
    CCPayLink payLink,
    PayLinkProcessingParams payLinkData)
  {
    CCProcessingCenter processingCenter = this.GetProcessingCenter(doc.ProcessingCenterID);
    this.CheckProcessingCenter(processingCenter);
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = processingCenter,
      callerGraph = this._repository.Graph
    });
    ICCPayLinkProcessor processor = this.CreatePluginAndGetPayLinkProcessor(processingCenter, provider);
    payLink.Action = "C";
    payLink.ActionStatus = "O";
    payLink.ErrorMessage = (string) null;
    this.SetStatusDate(payLink);
    payLink = this.UpdatePayLink(payLink);
    this.Save();
    PayLinkData payLinkData1 = this.SendRequestAndHandleError<PayLinkData>((Func<PayLinkData>) (() => processor.CloseLink(payLinkData)), payLink);
    this.SetStatuses(payLink, payLinkData1);
    payLink.NeedSync = new bool?(false);
    payLink.NeedReportSync = new bool?(false);
    this.UpdatePayLink(payLink);
    this.Save();
  }

  public void SyncLink(PX.Objects.Extensions.PayLink.PayLinkDocument doc, CCPayLink payLink, PayLinkProcessingParams payLinkData)
  {
    CCProcessingCenter processingCenter = this.GetProcessingCenter(doc.ProcessingCenterID);
    this.CheckProcessingCenter(processingCenter);
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = processingCenter,
      callerGraph = this._repository.Graph
    });
    ICCPayLinkProcessor processor = this.CreatePluginAndGetPayLinkProcessor(processingCenter, provider);
    if (!doc.PayLinkID.HasValue)
      return;
    int? payLinkId1 = payLink.PayLinkID;
    int? payLinkId2 = doc.PayLinkID;
    if (!(payLinkId1.GetValueOrDefault() == payLinkId2.GetValueOrDefault() & payLinkId1.HasValue == payLinkId2.HasValue))
      return;
    payLink.Action = "U";
    payLink.ActionStatus = "O";
    payLink.ErrorMessage = (string) null;
    payLink.Amount = new Decimal?(payLinkData.Amount);
    payLink.DeliveryMethod = doc.DeliveryMethod;
    payLink.DueDate = new DateTime?(payLinkData.DueDate);
    this.SetStatusDate(payLink);
    payLink = this.UpdatePayLink(payLink);
    this.Save();
    PayLinkData payLinkData1 = this.SendRequestAndHandleError<PayLinkData>((Func<PayLinkData>) (() => processor.UpdateLink(payLinkData)), payLink);
    this.SetStatuses(payLink, payLinkData1);
    payLink.NeedSync = new bool?(false);
    payLink.NeedReportSync = new bool?(false);
    CCPayLink ccPayLink = payLink;
    IEnumerable<FileData> attachments = payLinkData1.Attachments;
    string fileId = attachments != null ? attachments.ElementAtOrDefault<FileData>(0)?.FileID : (string) null;
    ccPayLink.ReportAttachmentID = fileId;
    this.UpdatePayLink(payLink);
    this.Save();
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public void CreateLink(PX.Objects.Extensions.PayLink.PayLinkDocument doc, PayLinkProcessingParams payLinkData)
  {
    this.CreateLinkInDB(doc, payLinkData);
  }

  public CCPayLink CreateLinkInDB(PX.Objects.Extensions.PayLink.PayLinkDocument doc, PayLinkProcessingParams payLinkData)
  {
    CCPayLink payLink1 = (CCPayLink) null;
    if (doc.PayLinkID.HasValue)
      payLink1 = this.GetPayLinkById(doc.PayLinkID);
    if (payLink1 != null)
      this.CheckPayLinkNotProcessed(payLink1, payLinkData);
    CCPayLink payLink2;
    if (payLink1 != null && !PayLinkHelper.PayLinkCreated(payLink1))
    {
      this.CheckTimeOfAttemptToCreateLink(payLink1);
      payLink2 = payLink1;
    }
    else
      payLink2 = this.InsertPayLink(new CCPayLink());
    if (!PayLinkHelper.PayLinkCreated(payLink2) && payLink2.NoteID.HasValue && payLink2.ActionStatus == "E")
      payLinkData.CheckLinkByGuid = true;
    payLink2.Action = "I";
    payLink2.Amount = new Decimal?(payLinkData.Amount);
    payLink2.DeliveryMethod = doc.DeliveryMethod;
    payLink2.ProcessingCenterID = doc.ProcessingCenterID;
    payLink2.CuryID = doc.CuryID;
    payLink2.DueDate = new DateTime?(payLinkData.DueDate);
    if (doc.OrderType != null)
    {
      payLink2.OrderType = doc.OrderType;
      payLink2.OrderNbr = doc.OrderNbr;
    }
    else
    {
      payLink2.DocType = doc.DocType;
      payLink2.RefNbr = doc.RefNbr;
    }
    payLink2.NeedSync = new bool?(false);
    payLink2.NeedReportSync = new bool?(false);
    payLink2.ActionStatus = "O";
    payLink2.LinkStatus = "N";
    payLink2.PaymentStatus = "N";
    payLink2.ErrorMessage = (string) null;
    this.SetStatusDate(payLink2);
    CCPayLink linkInDb = this.UpdatePayLink(payLink2);
    this.Save();
    payLinkData.LinkGuid = linkInDb.NoteID;
    doc.PayLinkID = linkInDb.PayLinkID;
    return linkInDb;
  }

  public void SendCreateLinkRequest(CCPayLink payLink, PayLinkProcessingParams payLinkData)
  {
    CCProcessingContext context = new CCProcessingContext();
    CCProcessingCenter processingCenter = this.GetProcessingCenter(payLink.ProcessingCenterID);
    this.CheckProcessingCenter(processingCenter);
    context.processingCenter = processingCenter;
    context.callerGraph = this._repository.Graph;
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(context);
    ICCPayLinkProcessor processor = this.CreatePluginAndGetPayLinkProcessor(processingCenter, provider);
    PayLinkData payLinkData1 = this.TryToGetLinkByGuidIfNeeded(processor, payLinkData) ?? this.SendRequestAndHandleError<PayLinkData>((Func<PayLinkData>) (() => processor.CreateLink(payLinkData)), payLink);
    this.SetStatusDate(payLink);
    payLink.ActionStatus = "S";
    payLink.LinkStatus = "O";
    payLink.PaymentStatus = "U";
    payLink.Url = payLinkData1.Url;
    payLink.ExternalID = payLinkData1.Id;
    CCPayLink ccPayLink = payLink;
    IEnumerable<FileData> attachments = payLinkData1.Attachments;
    string fileId = attachments != null ? attachments.ElementAtOrDefault<FileData>(0)?.FileID : (string) null;
    ccPayLink.ReportAttachmentID = fileId;
    this.UpdatePayLink(payLink);
    this.Save();
  }

  protected virtual ICCPayLinkProcessor CreatePluginAndGetPayLinkProcessor(
    CCProcessingCenter processingCenter,
    ICardProcessingReadersProvider provider)
  {
    return processingCenter != null ? this.GetPayLinkProcessor(this.CreatePlugin(processingCenter), provider) : throw new PXException("Processing center can't be found");
  }

  protected ICCWebhookProcessor CreatePluginAndGetWebhookProcessor(
    CCProcessingCenter processingCenter,
    ICardProcessingReadersProvider provider)
  {
    return processingCenter != null ? this.GetWebhookProcessor(this.CreatePlugin(processingCenter), provider) : throw new PXException("Processing center can't be found");
  }

  private PayLinkData TryToGetLinkByGuidIfNeeded(
    ICCPayLinkProcessor processor,
    PayLinkProcessingParams payLinkData)
  {
    PayLinkData linkByGuidIfNeeded = (PayLinkData) null;
    if (payLinkData.CheckLinkByGuid)
    {
      if (payLinkData.LinkGuid.HasValue)
      {
        try
        {
          linkByGuidIfNeeded = processor.GetLink(new PayLinkSearchParams()
          {
            LinkGuid = payLinkData.LinkGuid
          });
        }
        catch
        {
        }
      }
    }
    return linkByGuidIfNeeded;
  }

  private void CheckTimeOfAttemptToCreateLink(CCPayLink payLink)
  {
    if (DateTime.UtcNow.Subtract(payLink.StatusDate.Value).TotalMilliseconds <= 5000.0)
      throw new PXException("The previous operation has not been completed yet.");
  }

  private object CreatePlugin(CCProcessingCenter processingCenter)
  {
    try
    {
      return CCPluginTypeHelper.CreatePluginInstance(processingCenter);
    }
    catch (PXException ex)
    {
      throw;
    }
    catch
    {
      throw new PXException("Cannot instantiate processing object of {0} type for the processing center {1}.", new object[2]
      {
        (object) processingCenter.ProcessingTypeName,
        (object) processingCenter.ProcessingCenterID
      });
    }
  }

  private void SetStatusDate(CCPayLink payLink)
  {
    payLink.StatusDate = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone()));
  }

  private void SetStatuses(CCPayLink payLink, PayLinkData payLinkData)
  {
    if (payLink.Action == "C" && payLinkData.StatusCode == 1)
      payLink.LinkStatus = "C";
    if (payLinkData.PaymentStatusCode == null)
      payLink.PaymentStatus = "U";
    else if (payLinkData.PaymentStatusCode == 1)
      payLink.PaymentStatus = "P";
    else if (payLinkData.PaymentStatusCode == 2)
      payLink.PaymentStatus = "I";
    payLink.ActionStatus = "S";
    this.SetStatusDate(payLink);
  }

  private ICCPayLinkProcessor GetPayLinkProcessor(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    ICCPayLinkProcessor processor = CCProcessingHelper.IsV2ProcessingInterface(pluginObject).CreateProcessor<ICCPayLinkProcessor>(new V2SettingsGenerator(provider).GetSettings());
    if (processor != null)
      return processor;
    this.CreateAndThrowException(CCProcessingFeature.PayLink);
    return processor;
  }

  private ICCWebhookProcessor GetWebhookProcessor(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    ICCWebhookProcessor processor = CCProcessingHelper.IsV2ProcessingInterface(pluginObject).CreateProcessor<ICCWebhookProcessor>(new V2SettingsGenerator(provider).GetSettings());
    if (processor != null)
      return processor;
    this.CreateAndThrowException(CCProcessingFeature.WebhookManagement);
    return processor;
  }

  private void CreateAndThrowException(CCProcessingFeature feature)
  {
    throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
    {
      (object) feature
    }));
  }

  private CCProcessingCenter GetProcessingCenter(string procCenterId)
  {
    return this._repository.GetProcessingCenterByID(procCenterId);
  }

  private void CheckProcessingCenter(CCProcessingCenter procCenter)
  {
    bool? nullable = procCenter != null ? procCenter.IsActive : throw new PXArgumentException(nameof (procCenter), "The argument cannot be null.");
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXException("Processing center {0} is inactive", new object[1]
      {
        (object) procCenter.ProcessingCenterID
      });
  }

  private string ShrinkMessage(string message)
  {
    return message == null || message.Length <= 500 ? message : message.Substring(0, 500);
  }

  private void CheckPayLinkNotProcessed(CCPayLink payLink, PayLinkProcessingParams payLinkData)
  {
    if (!PayLinkHelper.PayLinkWasProcessed(payLink) && PayLinkHelper.PayLinkCreated(payLink))
      throw new PXException("The {0} document with the {1} reference number already has an active payment link.", new object[2]
      {
        (object) payLinkData.DocumentData.DocType,
        (object) payLinkData.DocumentData.DocRefNbr
      });
  }

  private CCPayLink InsertPayLink(CCPayLink payLink)
  {
    return this._graph.Caches[typeof (CCPayLink)].Insert((object) payLink) as CCPayLink;
  }

  private CCPayLink UpdatePayLink(CCPayLink payLink)
  {
    return this._graph.Caches[typeof (CCPayLink)].Update((object) payLink) as CCPayLink;
  }

  private CCPayLink GetPayLinkById(int? payLinkId) => CCPayLink.PK.Find(this._graph, payLinkId);

  private void Save()
  {
    PXAction action = this._graph.Actions[nameof (Save)];
    if (action != null)
      action.Press();
    else
      this._graph.Actions.PressSave();
  }

  private T SendRequestAndHandleError<T>(Func<T> func, CCPayLink payLink)
  {
    try
    {
      return func();
    }
    catch (Exception ex)
    {
      this.SetErrorStatus(ex, payLink);
      throw;
    }
  }
}
