// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.PaymentTransactionAcceptFormGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Services;
using PX.CCProcessingBase;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.CA;
using PX.Objects.CC.PaymentProcessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.PaymentTransaction;

public abstract class PaymentTransactionAcceptFormGraph<TGraph, TPrimary> : 
  PaymentTransactionGraph<TGraph, TPrimary>
  where TGraph : PXGraph, new()
  where TPrimary : class, IBqlTable, new()
{
  protected bool UseAcceptHostedForm;
  protected Guid? DocNoteId;
  protected bool EnableMobileMode;
  protected bool CheckSyncLockOnPersist;
  private string checkedProcessingCenter;
  private bool checkedProcessingCenterResult;
  private RetryPolicy<IEnumerable<TransactionData>> retryUnsettledTran;
  public PXAction<TPrimary> syncPaymentTransaction;

  [InjectDependency]
  public ICompanyService CompanyService { get; set; }

  [InjectDependency]
  internal PaymentConnectorCallbackService PaymentCallbackService { get; set; }

  [PXUIField(DisplayName = "Authorize", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public override IEnumerable AuthorizeCCPayment(PXAdapter adapter)
  {
    PXTrace.WriteInformation(this.GetClassMethodName() + " started.");
    this.ShowProcessingWarnIfLock(adapter);
    this.CheckProcCenterDisabled();
    IEnumerable enumerable;
    if (!this.UseAcceptHostedForm || this.HasPreviousTransactionId())
    {
      enumerable = base.AuthorizeCCPayment(adapter);
    }
    else
    {
      if (!this.IsSupportPaymentHostedForm(this.SelectedProcessingCenter))
        throw new PXException("The selected processing center does not support acceptance of payments directly from new cards.");
      enumerable = this.AuthorizeThroughForm(adapter);
    }
    return enumerable;
  }

  [PXUIField(DisplayName = "Capture", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public override IEnumerable CaptureCCPayment(PXAdapter adapter)
  {
    PXTrace.WriteInformation(this.GetClassMethodName() + " started.");
    this.ShowProcessingWarnIfLock(adapter);
    this.CheckProcCenterDisabled();
    IEnumerable enumerable;
    if (!this.UseAcceptHostedForm)
    {
      enumerable = base.CaptureCCPayment(adapter);
    }
    else
    {
      if (!this.IsSupportPaymentHostedForm(this.SelectedProcessingCenter))
        throw new PXException("The selected processing center does not support acceptance of payments directly from new cards.");
      enumerable = this.CaptureThroughForm(adapter);
    }
    return enumerable;
  }

  [PXUIField(DisplayName = "Validate Card Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = true)]
  [PXButton]
  public override IEnumerable ValidateCCPayment(PXAdapter adapter)
  {
    PXTrace.WriteInformation(this.GetClassMethodName() + " started.");
    this.ShowProcessingWarnIfLock(adapter);
    this.CheckProcCenterDisabled();
    this.Base.Actions.PressCancel();
    List<TPrimary> list = adapter.Get<TPrimary>().ToList<TPrimary>();
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
    {
      TGraph instance = PXGraph.CreateInstance<TGraph>();
      foreach (TPrimary doc in list)
      {
        PaymentTransactionAcceptFormGraph<TGraph, TPrimary> implementation = instance.FindImplementation<PaymentTransactionAcceptFormGraph<TGraph, TPrimary>>();
        implementation.DoValidateCCPayment(implementation.SetCurrentDocument(instance, doc));
      }
    }));
    return (IEnumerable) list;
  }

  protected override TPrimary DoValidateCCPayment(TPrimary doc)
  {
    if (!this.RunPendingOperations(doc))
      this.CheckPaymentTransaction(doc, true);
    if (this.LockExists(doc))
      this.RemoveSyncLock(doc);
    this.RestoreDocStateByTransactionIfNeeded(doc);
    return doc;
  }

  protected override bool TrySyncByStoredIds(TPrimary doc, IExternalTransaction extTran)
  {
    bool flag1 = false;
    bool? active = extTran.Active;
    bool flag2 = false;
    if (active.GetValueOrDefault() == flag2 & active.HasValue && extTran.ProcStatus == "UKN")
    {
      if (!string.IsNullOrEmpty(extTran.TranNumber))
      {
        try
        {
          PaymentTransactionDetail transactionDetail = this.GetPaymentTranDetails().FirstOrDefault<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
          {
            int? transactionId1 = i.TransactionID;
            int? transactionId2 = extTran.TransactionID;
            return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
          }));
          if (transactionDetail?.ProcStatus == "OPN")
          {
            if (transactionDetail != null)
            {
              bool? imported = transactionDetail.Imported;
              bool flag3 = false;
              if (imported.GetValueOrDefault() == flag3 & imported.HasValue)
              {
                this.SyncPaymentTransactionById(doc, EnumerableExtensions.AsSingleEnumerable<string>(extTran.TranNumber));
                flag1 = true;
              }
            }
          }
        }
        catch (PXException ex) when (ex.InnerException is CCProcessingException innerException && innerException != null && innerException.Reason == 2)
        {
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(extTran.TranApiNumber) && this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionFinder, false))
        {
          TransactionData transaction = this.GetPaymentProcessing().FindTransaction(extTran.TranApiNumber, this.SelectedProcessingCenter);
          ExternalTransactionDetail transactionDetail = this.GetExtTranDetails().FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i =>
          {
            int? transactionId3 = i.TransactionID;
            int? transactionId4 = extTran.TransactionID;
            return transactionId3.GetValueOrDefault() == transactionId4.GetValueOrDefault() & transactionId3.HasValue == transactionId4.HasValue;
          }));
          if (transaction != null && transactionDetail != null)
          {
            transactionDetail.TranNumber = transaction.TranID;
            ExternalTransactionDetail storedExtTran = this.ExternalTransaction.Update(transactionDetail);
            this.CheckAndRecordTransaction(doc, storedExtTran, transaction);
            flag1 = true;
          }
        }
        if (!flag1)
        {
          Guid? noteId = extTran.NoteID;
          if (noteId.HasValue && this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionFinder, false) && this.IsPaymentHostedFormSupported(this.SelectedProcessingCenter))
          {
            PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing paymentProcessing = this.GetPaymentProcessing();
            noteId = extTran.NoteID;
            Guid transactionGuid = noteId.Value;
            string processingCenter = this.SelectedProcessingCenter;
            TransactionData transaction = paymentProcessing.FindTransaction(transactionGuid, processingCenter);
            ExternalTransactionDetail transactionDetail = this.GetExtTranDetails().FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i =>
            {
              int? transactionId5 = i.TransactionID;
              int? transactionId6 = extTran.TransactionID;
              return transactionId5.GetValueOrDefault() == transactionId6.GetValueOrDefault() & transactionId5.HasValue == transactionId6.HasValue;
            }));
            if (transaction != null && transactionDetail != null)
            {
              transactionDetail.TranNumber = transaction.TranID;
              ExternalTransactionDetail storedExtTran = this.ExternalTransaction.Update(transactionDetail);
              this.CheckAndRecordTransaction(doc, storedExtTran, transaction);
              flag1 = true;
            }
          }
        }
      }
    }
    return flag1;
  }

  [PXUIField(MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncPaymentTransaction(PXAdapter adapter)
  {
    PXTrace.WriteInformation(this.GetClassMethodName() + " started.");
    string commandArguments = adapter.CommandArguments;
    PaymentConnectorCallbackParams connectorCallbackParams = !string.IsNullOrEmpty(commandArguments) ? this.PaymentCallbackService.FromCommandArguments(commandArguments) : this.PaymentCallbackService.FromCCPaymentPanelCallback(true, new Func<string, string>(this.GetContextFullKey));
    if (connectorCallbackParams.IsCancelled.GetValueOrDefault())
    {
      IExternalTransaction extTran = this.GetExtTrans().FirstOrDefault<IExternalTransaction>();
      if (extTran != null)
        this.RemoveLockAndFinalizeTran(extTran);
      return adapter.Get();
    }
    if (string.IsNullOrEmpty(connectorCallbackParams.TransactionId))
      throw new PXException("Could not get a response from the hosted form.");
    TPrimary doc = adapter.Get<TPrimary>().First<TPrimary>();
    string tranId;
    if (CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(this.SelectedProcessingCenter), CCProcessingFeature.PaymentForm))
    {
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      tranId = this.GetPaymentProcessing().ProcessPaymentFormResponse(paymentDoc, this.SelectedProcessingCenter, this.SelectedBAccount, connectorCallbackParams.TransactionId)?.TranID;
    }
    else
      tranId = this.GetPaymentProcessing().ParsePaymentFormResponse(connectorCallbackParams.TransactionId, this.SelectedProcessingCenter)?.TranID;
    if (string.IsNullOrEmpty(tranId))
      throw new PXException("Could not get Transaction ID from the hosted form response.");
    if (this.Base.IsMobile)
      SyncImpl(doc, tranId);
    else
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() => SyncImpl(doc, tranId)));
    return adapter.Get();

    void SyncImpl(TPrimary localDoc, string localTranId)
    {
      TGraph instance = PXGraph.CreateInstance<TGraph>();
      PaymentTransactionAcceptFormGraph<TGraph, TPrimary> implementation = instance.FindImplementation<PaymentTransactionAcceptFormGraph<TGraph, TPrimary>>();
      implementation.SyncPaymentTransactionById(implementation.SetCurrentDocument(instance, doc), (IEnumerable<string>) new List<string>()
      {
        localTranId
      });
    }
  }

  protected virtual string GetContextFullKey(string key)
  {
    return "PaymentTransactionAcceptFormGraph$" + key;
  }

  public virtual void SetContextString(string key, string value)
  {
    PXContext.SetSlot<string>(this.GetContextFullKey(key), value);
  }

  private IEnumerable AuthorizeThroughForm(PXAdapter adapter)
  {
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckDocumentUpdatedInDb(doc);
      this.CheckAllowNewCards();
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      Decimal? curyDocBal = paymentDoc.CuryDocBal;
      Decimal num = 0M;
      if (curyDocBal.GetValueOrDefault() <= num & curyDocBal.HasValue)
        throw new PXException("The amount must be greater than zero.");
      bool? released = paymentDoc.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
      {
        this.Base.Actions.PressSave();
        this.BeforeAuthorizePayment(doc);
      }
      primaryList.Add(doc);
      if (!this.CheckForActiveTranBeforeFormOpening(doc))
      {
        ExternalTransactionDetail extTran = this.StartCreditCardTransaction(paymentDoc, CCTranType.AuthorizeOnly);
        if (this.EnableMobileMode)
        {
          this.ProcessMobilePayment(doc, extTran, (CCTranType) 1);
        }
        else
        {
          PXBaseRedirectException redirectEx = (PXBaseRedirectException) null;
          try
          {
            if (CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(this.SelectedProcessingCenter), CCProcessingFeature.PaymentForm))
            {
              string paymentType = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, this.SelectedPaymentMethod)?.PaymentType;
              PXPluginRedirectOptions pluginRedirectOptions = this.GetPaymentProcessing().PreparePaymentForm(paymentDoc, this.SelectedProcessingCenter, this.SelectedBAccount, this.NeedSaveCard(), (CCTranType) 1, extTran.NoteID, paymentType);
              this.UpdateExtTran(extTran, pluginRedirectOptions);
              throw new PXPluginRedirectException<PXPluginRedirectOptions>(pluginRedirectOptions);
            }
            this.ShowAcceptPaymentForm((CCTranType) 1, extTran.NoteID);
          }
          catch (PXBaseRedirectException ex)
          {
            redirectEx = ex;
          }
          if (!this.LockExists(doc))
            this.SetSyncLock(doc);
          TPrimary copy = this.Base.Caches[typeof (TPrimary)].CreateCopy((object) doc) as TPrimary;
          PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
          {
            TGraph instance = PXGraph.CreateInstance<TGraph>();
            PaymentTransactionAcceptFormGraph<TGraph, TPrimary> implementation = instance.FindImplementation<PaymentTransactionAcceptFormGraph<TGraph, TPrimary>>();
            implementation.SetCurrentDocument(instance, copy);
            int? pmInstanceId = implementation.GetPaymentDoc(copy).PMInstanceID;
            int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
            if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue && !implementation.TranHeldForReview() && redirectEx != null)
              throw redirectEx;
            implementation.RemoveSyncLock(copy);
          }));
        }
      }
    }
    return (IEnumerable) primaryList;
  }

  private IEnumerable CaptureThroughForm(PXAdapter adapter)
  {
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary primary in adapter.Get<TPrimary>())
    {
      TPrimary doc = primary;
      this.CheckDocumentUpdatedInDb(doc);
      this.CheckAllowNewCards();
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      Decimal? curyDocBal = paymentDoc.CuryDocBal;
      Decimal num = 0M;
      if (curyDocBal.GetValueOrDefault() <= num & curyDocBal.HasValue)
        throw new PXException("The amount must be greater than zero.");
      bool? released = paymentDoc.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
      {
        this.Base.Actions.PressSave();
        this.BeforeCapturePayment(doc);
      }
      primaryList.Add(doc);
      if (!this.CheckForActiveTranBeforeFormOpening(doc))
      {
        ExternalTransactionDetail extTran = this.StartCreditCardTransaction(paymentDoc, CCTranType.AuthorizeAndCapture);
        if (this.EnableMobileMode)
        {
          if (!this.FindPreAuthorizing())
            this.ProcessMobilePayment(doc, extTran, (CCTranType) 0);
        }
        else
        {
          PXBaseRedirectException redirectEx = (PXBaseRedirectException) null;
          try
          {
            if (CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(this.SelectedProcessingCenter), CCProcessingFeature.PaymentForm))
            {
              string paymentType = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, this.SelectedPaymentMethod)?.PaymentType;
              PXPluginRedirectOptions pluginRedirectOptions = this.GetPaymentProcessing().PreparePaymentForm(paymentDoc, this.SelectedProcessingCenter, this.SelectedBAccount, this.NeedSaveCard(), (CCTranType) 0, extTran.NoteID, paymentType);
              this.UpdateExtTran(extTran, pluginRedirectOptions);
              throw new PXPluginRedirectException<PXPluginRedirectOptions>(pluginRedirectOptions);
            }
            this.ShowAcceptPaymentForm((CCTranType) 0, extTran.NoteID);
          }
          catch (PXBaseRedirectException ex)
          {
            redirectEx = ex;
          }
          if (!this.LockExists(doc))
            this.SetSyncLock(doc);
          TPrimary copy = this.Base.Caches[typeof (TPrimary)].CreateCopy((object) doc) as TPrimary;
          PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
          {
            TGraph instance = PXGraph.CreateInstance<TGraph>();
            PaymentTransactionAcceptFormGraph<TGraph, TPrimary> implementation = instance.FindImplementation<PaymentTransactionAcceptFormGraph<TGraph, TPrimary>>();
            implementation.SetCurrentDocument(instance, copy);
            int? pmInstanceId = implementation.GetPaymentDoc(copy).PMInstanceID;
            int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
            if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue && !implementation.TranHeldForReview() && !implementation.FindPreAuthorizing() && redirectEx != null)
              throw redirectEx;
            implementation.RemoveSyncLock(doc);
          }));
        }
      }
    }
    return (IEnumerable) primaryList;
  }

  private void ProcessMobilePayment(
    TPrimary doc,
    ExternalTransactionDetail extTran,
    CCTranType tranType)
  {
    ICCPayment paymentDoc = this.GetPaymentDoc(doc);
    int? pmInstanceId = paymentDoc.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue && !this.TranHeldForReview())
    {
      this.SetSyncLock(doc);
      string url;
      if (CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(this.SelectedProcessingCenter), CCProcessingFeature.PaymentForm))
      {
        string paymentType = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, this.SelectedPaymentMethod)?.PaymentType;
        PXPluginRedirectOptions options = this.GetPaymentProcessing().PreparePaymentForm(paymentDoc, this.SelectedProcessingCenter, this.SelectedBAccount, this.NeedSaveCard(), tranType, extTran.NoteID, paymentType);
        extTran = this.UpdateExtTran(extTran, options);
        Dictionary<string, string> mobileDict = this.CreateMobileDict(paymentDoc.DocType, paymentDoc.RefNbr, tranType, this.GetCompanyName(), extTran.NoteID);
        mobileDict.Add("ProcCenterId", this.SelectedProcessingCenter);
        url = CCServiceEndpointHelper.GetPaymentUrl("Payment.aspx", options, mobileDict);
      }
      else
        url = CCServiceEndpointHelper.GetUrl((CCServiceAction) 0, this.CreateMobileDict(paymentDoc.DocType, paymentDoc.RefNbr, tranType, this.GetCompanyName(), extTran.NoteID));
      if (url == null)
        throw new PXException("Could not generate redirect URL");
      PXTrace.WriteInformation("Redirect to endpoint. Url: {redirectUrl}", (object) url);
      throw new PXRedirectToUrlException(url, PXBaseRedirectException.WindowMode.New, true, "Redirect:" + url);
    }
    this.RemoveSyncLock(doc);
  }

  private Dictionary<string, string> CreateMobileDict(
    string docType,
    string refNbr,
    CCTranType tranType,
    string companyName,
    Guid? tranUID)
  {
    return new Dictionary<string, string>()
    {
      {
        "NoteId",
        this.DocNoteId.ToString()
      },
      {
        "DocType",
        docType
      },
      {
        "RefNbr",
        refNbr
      },
      {
        "TranType",
        tranType.ToString()
      },
      {
        "CompanyName",
        companyName
      },
      {
        "TranUID",
        tranUID.ToString()
      }
    };
  }

  private void ShowProcessingWarnIfLock(PXAdapter adapter)
  {
    TPrimary doc = adapter.Get<TPrimary>().FirstOrDefault<TPrimary>();
    IExternalTransaction activeTransaction = ExternalTranHelper.GetActiveTransaction(this.GetExtTrans());
    if ((object) doc == null || !adapter.ExternalCall || !this.LockExists(doc) || activeTransaction != null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetLastTransactionState((PXGraph) this.Base, this.GetExtTrans());
    if ((!transactionState.IsVoided || !transactionState.NeedSync) && this.PaymentTransaction.Ask("The payment is already being processed. Proceeding can cause a duplicate credit card transaction.", MessageButtons.OKCancel) == WebDialogResult.No)
      throw new PXException("Operation cancelled.");
  }

  protected virtual void CheckAllowNewCards()
  {
    if (!((bool?) this.GetProcessingCenterById(this.SelectedProcessingCenter)?.UseAcceptPaymentForm).GetValueOrDefault())
      throw new PXException("The {0} processing center does not support payments from new cards. On the Customer Payment Methods (AR303000) form, add this card for the {1} customer and then on the Payments and Applications (AR302000) form, clear the New Card check box and select the card in the Card/Account Nbr. box for the payment.", new object[2]
      {
        (object) this.SelectedProcessingCenter,
        (object) PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, this.SelectedBAccount)?.AcctCD
      });
  }

  protected override bool RunPendingOperations(TPrimary doc)
  {
    if (this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionGetter, false))
    {
      IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
      IExternalTransaction extTran = ExternalTranHelper.GetDeactivatedNeedSyncTransaction(extTrans);
      if (extTran == null)
        extTran = ExternalTranHelper.GetActiveTransaction(extTrans);
      if (extTran != null)
      {
        bool? needSync = extTran.NeedSync;
        bool flag = false;
        if (!(needSync.GetValueOrDefault() == flag & needSync.HasValue))
        {
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            this.IsNeedSyncContext = true;
            ExternalTransactionDetail extTranDetail = this.GetExtTranDetail(extTran.TranNumber, extTran.TranApiNumber);
            try
            {
              TransactionData centerTransaction = this.GetProcCenterTransaction(extTran.TranNumber, extTran.TranApiNumber);
              if (centerTransaction != null)
              {
                this.SetProcCenterTranNumber(centerTransaction, extTranDetail, doc);
                this.ValidateTran(doc, centerTransaction);
                this.RemoveSyncLock(doc);
                this.UpdateExpirationDate(centerTransaction);
                this.UpdateSyncStatus(centerTransaction, extTranDetail);
                this.SyncProfile(doc, centerTransaction);
                this.UpdateNeedSyncDoc(doc, centerTransaction);
              }
              else
                this.HandleTranNotFound(doc, extTranDetail);
              transactionScope.Complete();
              goto label_20;
            }
            catch (TranValidationHelper.TranValidationException ex)
            {
              this.UpdateSyncStatus(extTranDetail, SyncStatus.Error, ex.Message);
              this.DeactivateAndUpdateProcStatus(extTranDetail);
              this.RemoveSyncLock(doc);
              this.PersistChangesIfNeeded();
              CCTranType typeByTranTypeStr = CCTranTypeCode.GetTranTypeByTranTypeStr(this.GetPaymentTranDetails().First<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
              {
                int? transactionId1 = i.TransactionID;
                int? transactionId2 = extTran.TransactionID;
                return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
              })).TranType);
              this.RunCallbacks(doc, typeByTranTypeStr);
              transactionScope.Complete();
              return true;
            }
            catch (PXException ex)
            {
              if ((ex.InnerException is CCProcessingException innerException ? (innerException.Reason == 2 ? 1 : 0) : 0) != 0)
              {
                this.HandleTranNotFound(doc, extTranDetail);
                transactionScope.Complete();
                return true;
              }
              throw;
            }
            finally
            {
              this.IsNeedSyncContext = false;
            }
          }
        }
      }
      return false;
    }
label_20:
    return true;
  }

  private void HandleTranNotFound(TPrimary doc, ExternalTransactionDetail extTranDetail)
  {
    this.DeactivateNotFoundTran(extTranDetail);
    this.RemoveSyncLock(doc);
    this.PersistChangesIfNeeded();
    CCTranType typeByTranTypeStr = CCTranTypeCode.GetTranTypeByTranTypeStr(this.GetPaymentTranDetails().First<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTranDetail.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).TranType);
    this.RunCallbacks(doc, typeByTranTypeStr);
  }

  private string GetCompanyName()
  {
    return !this.CompanyService.IsMultiCompany ? this.CompanyService.GetSingleCompanyLoginName() : this.CompanyService.ExtractCompany(PXContext.PXIdentity.User.Identity.Name);
  }

  private void CheckPaymentTransaction(TPrimary doc, bool tryToRestoreTran)
  {
    if (!this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionGetter, false))
      return;
    ICCPayment pDoc = this.GetPaymentDoc(doc);
    IEnumerable<TransactionData> trans = Enumerable.Empty<TransactionData>();
    IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
    IExternalTransaction activeTransaction = ExternalTranHelper.GetActiveTransaction(extTrans);
    if (activeTransaction != null && activeTransaction.DocType == pDoc.DocType)
    {
      this.SyncPaymentTransactionById(doc, (IEnumerable<string>) new List<string>()
      {
        activeTransaction.TranNumber
      });
    }
    else
    {
      if (!tryToRestoreTran)
        return;
      IExternalTransaction extTran = extTrans.FirstOrDefault<IExternalTransaction>();
      if (extTran != null && this.TrySyncByStoredIds(doc, extTran))
        return;
      this.retryUnsettledTran.HandleError((Func<IEnumerable<TransactionData>, bool>) (i => this.GetTransByDoc(pDoc, i).Count > 0));
      try
      {
        trans = this.retryUnsettledTran.Execute((Func<IEnumerable<TransactionData>>) (() => this.GetPaymentProcessing().GetUnsettledTransactions(this.SelectedProcessingCenter)));
      }
      catch (InvalidOperationException ex)
      {
      }
      IEnumerable<string> tranIds = this.PrepareTransactionIds(this.GetTransByDoc(pDoc, trans));
      this.SyncPaymentTransactionById(doc, tranIds);
    }
  }

  public virtual void SyncPaymentTransactionById(TPrimary doc, IEnumerable<string> tranIds)
  {
    if (!this.IsPaymentHostedFormSupported(this.SelectedProcessingCenter))
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (string tranId1 in tranIds)
      {
        string tranId = tranId1;
        TransactionData tranData = this.GetTranData(tranId);
        bool flag1 = false;
        IList<ExternalTransactionDetail> list = (IList<ExternalTransactionDetail>) this.GetExtTranDetails().ToList<ExternalTransactionDetail>();
        ExternalTransactionDetail storedExtTran;
        if (tranData.TranUID.HasValue)
        {
          ExternalTransactionDetail transactionDetail = list.FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (t =>
          {
            Guid? noteId = t.NoteID;
            Guid? tranUid = tranData.TranUID;
            if (noteId.HasValue != tranUid.HasValue)
              return false;
            return !noteId.HasValue || noteId.GetValueOrDefault() == tranUid.GetValueOrDefault();
          }));
          if (transactionDetail != null)
            flag1 = true;
          if (transactionDetail != null && string.IsNullOrEmpty(transactionDetail.TranNumber))
          {
            transactionDetail.TranNumber = tranData.TranID;
            storedExtTran = this.ExternalTransaction.Update(transactionDetail);
          }
          else
          {
            storedExtTran = list.FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => i.TranNumber == tranId));
            if (storedExtTran != null)
            {
              bool? needSync = storedExtTran.NeedSync;
              bool flag2 = false;
              if (needSync.GetValueOrDefault() == flag2 & needSync.HasValue)
                flag1 = true;
            }
          }
        }
        else
        {
          storedExtTran = list.FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => i.TranNumber == tranId));
          flag1 = true;
        }
        if (flag1)
          this.CheckAndRecordTransaction(doc, storedExtTran, tranData);
      }
      this.FinalizeTransactionsNotFoundInProcCenter();
      transactionScope.Complete();
    }
  }

  public void ShowAcceptPaymentForm(CCTranType tranType, Guid? noteId)
  {
    ICCPayment paymentDoc = this.GetPaymentDoc(this.Base.Caches[typeof (TPrimary)].Current as TPrimary);
    this.GetPaymentProcessing().ShowAcceptPaymentForm(tranType, paymentDoc, this.SelectedProcessingCenter, this.SelectedBAccount, noteId);
  }

  public void CheckAndRecordTransaction(
    ExternalTransactionDetail extTranDetail,
    TransactionData tranData)
  {
    this.CheckAndRecordTransaction(this.Base.Caches[typeof (TPrimary)].Current as TPrimary, extTranDetail, tranData);
  }

  public virtual void RemoveLockAndFinalizeTran(IExternalTransaction extTran)
  {
    this.RemoveSyncLock(this.Base.Caches[typeof (TPrimary)].Current as TPrimary);
    string message = PXMessages.LocalizeNoPrefix("Operation was canceled by user.");
    this.FinalizeTran(extTran, message);
  }

  protected virtual void CheckAndRecordTransaction(
    TPrimary doc,
    ExternalTransactionDetail storedExtTran,
    TransactionData tranData)
  {
    string processingStatus = this.GetProcessingStatus(tranData);
    Decimal? amount1;
    if (storedExtTran != null && storedExtTran.ProcStatus == processingStatus)
    {
      amount1 = storedExtTran.Amount;
      Decimal amount2 = tranData.Amount;
      if (amount1.GetValueOrDefault() == amount2 & amount1.HasValue)
        return;
    }
    if (tranData != null && tranData.CustomerId != null && !this.SuitableCustomerProfileId(tranData?.CustomerId))
      return;
    PXTrace.WriteInformation($"Synchronize tran. TranId = {tranData.TranID}, TranType = {tranData.TranType}, DocNum = {tranData.DocNum}, " + $"SubmitTime = {tranData.SubmitTime}, Amount = {tranData.Amount}, PCCustomerID = {tranData.CustomerId}, PCCustomerPaymentID = {tranData.PaymentId}");
    CCTranType tranType = tranData.TranType.Value;
    if (storedExtTran != null)
      this.UpdateSyncStatus(tranData, storedExtTran);
    ICCPayment paymentDoc = this.GetPaymentDoc(doc);
    if (tranData.TranStatus == null)
    {
      if (!EnumerableExtensions.IsIn<CCTranType>(tranType, (CCTranType) 5, (CCTranType) 9))
      {
        try
        {
          this.GetOrCreatePaymentProfileByTran(tranData, paymentDoc);
        }
        catch (PXException ex)
        {
          PXTrace.WriteError((Exception) ex);
          if (PXLongOperation.IsLongOperationContext())
            PXLongOperation.SetCustomInfoPersistent((object) new PaymentTransactionGraph<TGraph, TPrimary>.LongOperationWarning("PaymentMethodID", (PXSetPropertyException) new PXSetPropertyException<PX.Objects.AR.ARPayment.paymentMethodID>("The customer payment method was not created for this credit card transaction. Processing Center response: {0}", PXErrorLevel.Warning, new object[1]
            {
              (object) PXMessages.LocalizeFormatNoPrefix(ex.InnerException?.Message ?? ex.MessageNoPrefix)
            })));
        }
      }
    }
    this.RemoveSyncLock(doc);
    this.PersistChangesIfNeeded();
    if (storedExtTran != null)
    {
      amount1 = storedExtTran.Amount;
      Decimal amount3 = tranData.Amount;
      if (!(amount1.GetValueOrDefault() == amount3 & amount1.HasValue))
        this.TryToFindIncreaseOrigDocIfNeeded(storedExtTran.TransactionID, paymentDoc, tranData.Amount);
    }
    this.RecordTranData(tranType, paymentDoc, tranData);
  }

  private void TryToFindIncreaseOrigDocIfNeeded(
    int? transactionID,
    ICCPayment pDoc,
    Decimal newAmount)
  {
    Payment extension = ((IBqlTable) pDoc).GetExtension<Payment>();
    if (extension == null || extension.TransactionOrigDocRefNbr != null)
      return;
    PaymentTransactionDetail transactionDetail = this.GetPaymentTranDetails().Where<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId = i.TransactionID;
      int? nullable = transactionID;
      if (transactionId.GetValueOrDefault() == nullable.GetValueOrDefault() & transactionId.HasValue == nullable.HasValue)
      {
        Decimal? amount = i.Amount;
        Decimal num = newAmount;
        if (amount.GetValueOrDefault() == num & amount.HasValue)
          return i.TranType == "IAA";
      }
      return false;
    })).OrderByDescending<PaymentTransactionDetail, int?>((Func<PaymentTransactionDetail, int?>) (i => i.TranNbr)).FirstOrDefault<PaymentTransactionDetail>();
    if (transactionDetail == null || !(transactionDetail.TranStatus != "APR"))
      return;
    extension.TransactionOrigDocType = transactionDetail.OrigDocType;
    extension.TransactionOrigDocRefNbr = transactionDetail.OrigRefNbr;
    extension.CuryDocBalIncrease = new Decimal?(newAmount);
  }

  protected virtual void RecordTranData(
    CCTranType tranType,
    ICCPayment pDoc,
    TransactionData tranData)
  {
    switch ((int) tranType)
    {
      case 0:
      case 2:
      case 3:
        this.RecordCapture(pDoc, tranData);
        break;
      case 1:
      case 8:
        this.RecordAuth(pDoc, tranData);
        break;
      case 4:
        this.RecordCredit(pDoc, tranData);
        break;
      case 5:
      case 9:
        this.RecordVoid(pDoc, tranData);
        break;
    }
  }

  protected override void UpdateSyncStatus(
    TransactionData tranData,
    ExternalTransactionDetail extTranDetail)
  {
    bool flag = true;
    switch (CCProcessingHelper.GetProcessingStatusByTranData(tranData))
    {
      case ProcessingStatus.AuthorizeSuccess:
      case ProcessingStatus.CaptureSuccess:
        Decimal amount1 = tranData.Amount;
        Decimal? amount2 = extTranDetail.Amount;
        Decimal valueOrDefault = amount2.GetValueOrDefault();
        if (!(amount1 == valueOrDefault & amount2.HasValue))
        {
          flag = false;
          string message = PXMessages.LocalizeFormatNoPrefix("The {0} transaction amount has changed.", (object) tranData.TranID);
          this.UpdateSyncStatus(extTranDetail, SyncStatus.Warning, message);
          break;
        }
        break;
    }
    if (!flag || !(extTranDetail.SyncStatus != "W") || !(extTranDetail.SyncStatus != "S"))
      return;
    this.UpdateSyncStatus(extTranDetail, SyncStatus.Success, (string) null);
  }

  protected void UpdateExpirationDate(TransactionData tranData)
  {
    if (!tranData.ExpireAfterDays.HasValue)
      return;
    IExternalTransaction storedTran = this.GetExtTrans().FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranNumber == tranData.TranID));
    if (storedTran == null)
      return;
    System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(tranData.SubmitTime, LocaleInfo.GetTimeZone()).AddDays((double) tranData.ExpireAfterDays.Value);
    storedTran.ExpirationDate = new System.DateTime?(dateTime);
    PaymentTransactionDetail transactionDetail = this.GetPaymentTranDetails().First<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = storedTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    }));
    transactionDetail.ExpirationDate = storedTran.ExpirationDate;
    this.PaymentTransaction.Update(transactionDetail);
  }

  protected void SetProcCenterTranNumber(
    TransactionData tranData,
    ExternalTransactionDetail extTranDetail,
    TPrimary doc)
  {
    if (string.IsNullOrEmpty(extTranDetail.TranNumber))
    {
      extTranDetail.TranNumber = tranData.TranID;
      this.ExternalTransaction.Update(extTranDetail);
    }
    PaymentTransactionDetail transactionDetail = this.GetPaymentTranDetails().First<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTranDetail.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    }));
    if (!string.IsNullOrEmpty(transactionDetail.PCTranNumber))
      return;
    transactionDetail.PCTranNumber = tranData.TranID;
    this.PaymentTransaction.Update(transactionDetail);
  }

  protected virtual bool SuitableCustomerProfileId(string customerId)
  {
    bool flag = true;
    if (customerId != null)
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = new PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.customerCCPID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>>((PXGraph) this.Base).SelectSingle((object) customerId, (object) this.SelectedProcessingCenter);
      if (customerPaymentMethod != null)
      {
        int? baccountId = customerPaymentMethod.BAccountID;
        int? selectedBaccount = this.SelectedBAccount;
        if (!(baccountId.GetValueOrDefault() == selectedBaccount.GetValueOrDefault() & baccountId.HasValue == selectedBaccount.HasValue))
          flag = false;
      }
    }
    return flag;
  }

  protected virtual int? GetOrCreatePaymentProfileByTran(TransactionData tranData, ICCPayment pDoc)
  {
    int? pmInstanceId1 = pDoc.PMInstanceID;
    int? newPaymentProfile1 = PaymentTranExtConstants.NewPaymentProfile;
    if (!(pmInstanceId1.GetValueOrDefault() == newPaymentProfile1.GetValueOrDefault() & pmInstanceId1.HasValue == newPaymentProfile1.HasValue))
      return pDoc.PMInstanceID;
    int? pmInstanceId2 = PaymentTranExtConstants.NewPaymentProfile;
    TranProfile input = (TranProfile) null;
    if (tranData.CustomerId != null && tranData.PaymentId != null)
      input = new TranProfile()
      {
        CustomerProfileId = tranData.CustomerId,
        PaymentProfileId = tranData.PaymentId
      };
    if (!this.NeedSaveCard())
    {
      if (input != null)
        pmInstanceId2 = this.GetInstanceIdForSelectedPM(input);
      int? nullable = pmInstanceId2;
      int? newPaymentProfile2 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable.GetValueOrDefault() == newPaymentProfile2.GetValueOrDefault() & nullable.HasValue == newPaymentProfile2.HasValue))
        this.SetPmInstanceId(pmInstanceId2);
      return pmInstanceId2;
    }
    PaymentProfileCreator paymentProfileCreator = this.GetPaymentProfileCreator();
    try
    {
      PX.Objects.AR.CustomerPaymentMethod cpm = paymentProfileCreator.PrepeareCpmRecord();
      if (input == null)
        input = this.GetOrCreateCustomerProfileByTranId(cpm, tranData.TranID);
      pmInstanceId2 = this.GetInstanceId(input);
      int? nullable1 = pmInstanceId2;
      int? newPaymentProfile3 = PaymentTranExtConstants.NewPaymentProfile;
      if (nullable1.GetValueOrDefault() == newPaymentProfile3.GetValueOrDefault() & nullable1.HasValue == newPaymentProfile3.HasValue)
        pmInstanceId2 = paymentProfileCreator.CreatePaymentProfile(input);
      int? nullable2 = pmInstanceId2;
      int? newPaymentProfile4 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable2.GetValueOrDefault() == newPaymentProfile4.GetValueOrDefault() & nullable2.HasValue == newPaymentProfile4.HasValue))
        paymentProfileCreator.CreateCustomerProcessingCenterRecord(input);
    }
    finally
    {
      paymentProfileCreator.ClearCaches();
    }
    this.SetPmInstanceId(pmInstanceId2);
    return pmInstanceId2;
  }

  protected virtual void FinalizeTransactionsNotFoundInProcCenter()
  {
    string message = PXMessages.LocalizeNoPrefix("Does not exist.");
    foreach (IExternalTransaction extTran in this.GetExtTrans())
      this.FinalizeTran(extTran, message);
  }

  protected virtual void FinalizeTran(IExternalTransaction extTran, string message)
  {
    PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing paymentProcessing = this.GetPaymentProcessing();
    if (!(extTran.ProcStatus == "UKN"))
      return;
    PaymentTransactionDetail transactionDetail = this.PaymentTransaction.Select().RowCast<PaymentTransactionDetail>().Where<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).FirstOrDefault<PaymentTransactionDetail>();
    if (transactionDetail == null || !(transactionDetail.ProcStatus == "OPN"))
      return;
    bool? imported = transactionDetail.Imported;
    bool flag = false;
    if (!(imported.GetValueOrDefault() == flag & imported.HasValue))
      return;
    paymentProcessing.FinalizeTransaction(transactionDetail.TranNbr, message);
  }

  protected virtual bool IsPaymentHostedFormSupported(string procCenterId)
  {
    return CCProcessingFeatureHelper.IsPaymentHostedFormSupported(this.GetProcessingCenterById(procCenterId));
  }

  protected virtual ExternalTransactionDetail StartCreditCardTransaction(
    ICCPayment pDoc,
    CCTranType ccTranType)
  {
    CCProcTran tran = new CCProcTran();
    tran.Copy(pDoc);
    tran.ProcessingCenterID = this.SelectedProcessingCenter;
    PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing paymentProcessing = this.GetPaymentProcessing();
    CCProcessingCenter centerFromTransaction = paymentProcessing.GetAndCheckProcessingCenterFromTransaction(tran);
    tran = paymentProcessing.StartCreditCardTransaction(ccTranType, tran, centerFromTransaction);
    return this.GetExtTranDetails().Where<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = tran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).First<ExternalTransactionDetail>();
  }

  protected virtual ExternalTransactionDetail UpdateExtTran(
    ExternalTransactionDetail extTran,
    PXPluginRedirectOptions options)
  {
    if (options is ICCRedirectOptionsWithTransactionID withTransactionId && !string.IsNullOrEmpty(withTransactionId.TransactionID))
    {
      extTran.TranNumber = withTransactionId.TransactionID;
      extTran = this.ExternalTransaction.Update(extTran);
    }
    return extTran;
  }

  protected virtual bool HasPreviousTransactionId()
  {
    return this.PaymentDoc?.Current?.PreviousExternalTransactionID != null;
  }

  public override void Initialize()
  {
    base.Initialize();
    this.CheckSyncLockOnPersist = true;
    this.retryUnsettledTran = new RetryPolicy<IEnumerable<TransactionData>>();
    this.retryUnsettledTran.RetryCnt = 1;
    this.retryUnsettledTran.StaticSleepDuration = 6000;
  }

  protected void CreateCustomerProcessingCenterRecord(TranProfile input)
  {
    PXCache cach = this.Base.Caches[typeof (CustomerProcessingCenterID)];
    cach.ClearQueryCacheObsolete();
    if (new PXSelectReadonly<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CustomerProcessingCenterID.cCProcessingCenterID>>, And<CustomerProcessingCenterID.bAccountID, Equal<Required<CustomerProcessingCenterID.bAccountID>>, And<CustomerProcessingCenterID.customerCCPID, Equal<Required<CustomerProcessingCenterID.customerCCPID>>>>>>((PXGraph) this.Base).SelectSingle((object) this.SelectedProcessingCenter, (object) this.SelectedBAccount, (object) input.CustomerProfileId) != null)
      return;
    CustomerProcessingCenterID instance = cach.CreateInstance() as CustomerProcessingCenterID;
    instance.BAccountID = this.SelectedBAccount;
    instance.CCProcessingCenterID = this.SelectedProcessingCenter;
    instance.CustomerCCPID = input.CustomerProfileId;
    cach.Insert((object) instance);
    cach.Persist(PXDBOperation.Insert);
  }

  protected int? GetInstanceId(TranProfile input) => this.GetInstanceId(input, (string) null);

  private int? GetInstanceIdForSelectedPM(TranProfile input)
  {
    return this.GetInstanceId(input, this.SelectedPaymentMethod);
  }

  private int? GetInstanceId(TranProfile input, string paymentMethodId)
  {
    int? instanceId = PaymentTranExtConstants.NewPaymentProfile;
    this.Base.Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)].ClearQueryCacheObsolete();
    Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> withProfileDetail = this.GetPaymentProcessing().Repository.GetCustomerPaymentMethodWithProfileDetail(this.SelectedProcessingCenter, input.CustomerProfileId, input.PaymentProfileId);
    if (withProfileDetail != null)
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = withProfileDetail.Item1;
      if (customerPaymentMethod != null)
      {
        int? baccountId = customerPaymentMethod.BAccountID;
        int? selectedBaccount = this.SelectedBAccount;
        if (baccountId.GetValueOrDefault() == selectedBaccount.GetValueOrDefault() & baccountId.HasValue == selectedBaccount.HasValue && customerPaymentMethod.IsActive.GetValueOrDefault() && (string.IsNullOrEmpty(paymentMethodId) || customerPaymentMethod.PaymentMethodID == paymentMethodId))
          instanceId = customerPaymentMethod.PMInstanceID;
      }
    }
    return instanceId;
  }

  protected TranProfile GetOrCreateCustomerProfileByTranId(PX.Objects.AR.CustomerPaymentMethod cpm, string tranId)
  {
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = new PXSelectReadonly<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>, OrderBy<Desc<PX.Objects.AR.CustomerPaymentMethod.createdDateTime>>>((PXGraph) this.Base).Select((object) this.SelectedBAccount, (object) this.SelectedProcessingCenter).RowCast<PX.Objects.AR.CustomerPaymentMethod>().FirstOrDefault<PX.Objects.AR.CustomerPaymentMethod>();
    if (customerPaymentMethod != null)
      cpm.CustomerCCPID = customerPaymentMethod.CustomerCCPID;
    PXSelect<PX.Objects.AR.CustomerPaymentMethod> dataView = new PXSelect<PX.Objects.AR.CustomerPaymentMethod>((PXGraph) this.Base);
    try
    {
      dataView.Insert(cpm);
      CCCustomerInformationManagerGraph instance = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
      GenericCCPaymentProfileAdapter<PX.Objects.AR.CustomerPaymentMethod> paymentProfileAdapter = new GenericCCPaymentProfileAdapter<PX.Objects.AR.CustomerPaymentMethod>((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) dataView);
      // ISSUE: variable of a boxed type
      __Boxed<TGraph> graph = (object) this.Base;
      GenericCCPaymentProfileAdapter<PX.Objects.AR.CustomerPaymentMethod> payment = paymentProfileAdapter;
      string tranId1 = tranId;
      return instance.GetOrCreatePaymentProfileByTran((PXGraph) graph, (ICCPaymentProfileAdapter) payment, tranId1);
    }
    finally
    {
      dataView.Cache.Clear();
    }
  }

  protected TransactionData GetTranData(string tranId)
  {
    return this.GetPaymentProcessing().GetTransactionById(tranId, this.SelectedProcessingCenter);
  }

  protected PX.Objects.AR.Customer GetCustomerByAccountId(int? id)
  {
    return (PX.Objects.AR.Customer) PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, (object) id);
  }

  protected virtual bool IsSupportPaymentHostedForm(string processingCenterId)
  {
    if (processingCenterId != this.checkedProcessingCenter)
    {
      this.checkedProcessingCenterResult = this.IsPaymentHostedFormSupported(processingCenterId);
      this.checkedProcessingCenter = processingCenterId;
    }
    return this.checkedProcessingCenterResult;
  }

  private List<TransactionData> GetTransByDoc(
    ICCPayment payment,
    IEnumerable<TransactionData> trans)
  {
    string searchDocNum = payment.DocType + payment.RefNbr;
    return trans.Where<TransactionData>((Func<TransactionData, bool>) (i => i.DocNum == searchDocNum)).ToList<TransactionData>();
  }

  private IEnumerable<string> PrepareTransactionIds(List<TransactionData> list)
  {
    return list.OrderBy<TransactionData, System.DateTime>((Func<TransactionData, System.DateTime>) (i => i.SubmitTime)).Select<TransactionData, string>((Func<TransactionData, string>) (i => i.TranID));
  }

  private bool FindPreAuthorizing() => this.GetActiveTransactionState().IsPreAuthorized;

  private bool TranHeldForReview() => this.GetActiveTransactionState().IsOpenForReview;

  private bool CheckForActiveTranBeforeFormOpening(TPrimary doc)
  {
    bool flag = false;
    this.CheckPaymentTransaction(doc, this.LockExists(doc));
    if (ExternalTranHelper.GetActiveTransaction(this.GetExtTrans()) != null)
      flag = true;
    return flag;
  }

  protected override void RowSelected(PX.Data.Events.RowSelected<TPrimary> e)
  {
    base.RowSelected(e);
  }

  protected virtual TPrimary GetDocWithoutChanges(TPrimary input) => default (TPrimary);

  protected virtual PaymentTransactionAcceptFormGraph<TGraph, TPrimary> GetPaymentTransactionAcceptFormExt(
    TGraph graph)
  {
    throw new NotImplementedException();
  }

  protected TransactionData GetProcCenterTransaction(string tranNumber, string tranApiNumber)
  {
    TransactionData centerTransaction = (TransactionData) null;
    if (!string.IsNullOrEmpty(tranNumber))
      centerTransaction = this.GetPaymentProcessing().GetTransactionById(tranNumber, this.SelectedProcessingCenter);
    else if (!string.IsNullOrEmpty(tranApiNumber) && this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionFinder, false))
      centerTransaction = this.GetPaymentProcessing().FindTransaction(tranApiNumber, this.SelectedProcessingCenter);
    return centerTransaction;
  }

  protected ExternalTransactionDetail GetExtTranDetail(string tranId, string tranApiId)
  {
    if (!string.IsNullOrEmpty(tranId))
      return this.GetExtTranDetails().FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => i.TranNumber == tranId));
    return !string.IsNullOrEmpty(tranApiId) ? this.GetExtTranDetails().FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => i.TranApiNumber == tranApiId)) : (ExternalTransactionDetail) null;
  }
}
