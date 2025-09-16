// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.PaymentTransactionGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Objects.Extensions.PaymentTransaction;

public abstract class PaymentTransactionGraph<TGraph, TPrimary> : PXGraphExtension<
#nullable disable
TGraph>
  where TGraph : PXGraph, new()
  where TPrimary : class, IBqlTable, new()
{
  public PXSelectExtension<ExternalTransactionDetail> ExternalTransaction;
  public PXSelectExtension<PaymentTransactionDetail> PaymentTransaction;
  public PXSelectExtension<Payment> PaymentDoc;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXFilter<InputPaymentInfo> InputPmtInfo;
  protected PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing paymentProcessing;
  public PXAction<TPrimary> authorizeCCPayment;
  public PXAction<TPrimary> increazeAuthorizedCCPayment;
  public PXAction<TPrimary> captureCCPayment;
  public PXAction<TPrimary> voidCCPayment;
  public PXAction<TPrimary> creditCCPayment;
  public PXAction<TPrimary> captureOnlyCCPayment;
  public PXAction<TPrimary> validateCCPayment;
  public PXAction<TPrimary> recordCCPayment;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public bool ReleaseDoc { get; set; }

  public string TranHeldwarnMsg { get; set; } = "The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction.";

  public string SelectedProcessingCenter { get; protected set; }

  public string SelectedProcessingCenterType { get; protected set; }

  protected int? SelectedBAccount { get; set; }

  protected string SelectedPaymentMethod { get; set; }

  protected bool IsNeedSyncContext { get; set; }

  [PXUIField(DisplayName = "Authorize", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AuthorizeCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    AccessInfo accessinfo = this.Base.Accessinfo;
    PXTrace.WriteInformation(classMethodName + " started.");
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      PXCache cach = this.Base.Caches[typeof (TPrimary)];
      bool allowUpdate = cach.AllowUpdate;
      cach.AllowUpdate = true;
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      primaryList.Add(doc);
      this.BeforeAuthorizePayment(doc);
      CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
      ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager();
      ccPaymentEntry.AuthorizeCCpayment(paymentDoc, (IExternalTransactionAdapter) new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction));
      cach.AllowUpdate = allowUpdate;
    }
    return (IEnumerable) primaryList;
  }

  [PXUIField(DisplayName = "Increase Authorized Amount", Visible = false, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton(DisplayOnMainToolbar = false)]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable IncreazeAuthorizedCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    AccessInfo accessinfo = this.Base.Accessinfo;
    PXTrace.WriteInformation(classMethodName + " started.");
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      PXCache cach = this.Base.Caches[typeof (TPrimary)];
      bool allowUpdate = cach.AllowUpdate;
      cach.AllowUpdate = true;
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      primaryList.Add(doc);
      CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
      ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager();
      ccPaymentEntry.IncreaseAuthorizedAmountCCpayment(paymentDoc, (IExternalTransactionAdapter) new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction));
      cach.AllowUpdate = allowUpdate;
    }
    return (IEnumerable) primaryList;
  }

  [PXUIField(DisplayName = "Capture", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CaptureCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    AccessInfo accessinfo = this.Base.Accessinfo;
    PXTrace.WriteInformation(classMethodName + " started.");
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      PXCache cach = this.Base.Caches[typeof (TPrimary)];
      bool allowUpdate = cach.AllowUpdate;
      cach.AllowUpdate = true;
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      primaryList.Add(doc);
      this.RunPendingOperations(doc);
      this.BeforeCapturePayment(doc);
      this.CheckHeldForReviewTranStatus(paymentDoc);
      GenericExternalTransactionAdapter<ExternalTransactionDetail> paymentTransaction = new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction);
      CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
      ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager();
      ccPaymentEntry.CaptureCCpayment(paymentDoc, (IExternalTransactionAdapter) paymentTransaction);
      cach.AllowUpdate = allowUpdate;
    }
    return (IEnumerable) primaryList;
  }

  [PXUIField(DisplayName = "Void Card Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable VoidCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    AccessInfo accessinfo = this.Base.Accessinfo;
    PXTrace.WriteInformation(classMethodName + " started.");
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      PXCache cach = this.Base.Caches[typeof (TPrimary)];
      bool allowUpdate = cach.AllowUpdate;
      cach.AllowUpdate = true;
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      primaryList.Add(doc);
      this.RunPendingOperations(doc);
      this.BeforeVoidPayment(doc);
      GenericExternalTransactionAdapter<ExternalTransactionDetail> paymentTransaction = new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction);
      CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
      ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager();
      ccPaymentEntry.VoidCCPayment(paymentDoc, (IExternalTransactionAdapter) paymentTransaction);
      cach.AllowUpdate = allowUpdate;
    }
    return (IEnumerable) primaryList;
  }

  [PXUIField(DisplayName = "Refund Card Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CreditCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    AccessInfo accessinfo = this.Base.Accessinfo;
    PXTrace.WriteInformation(classMethodName + " started.");
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      primaryList.Add(doc);
      this.BeforeCreditPayment(doc);
      GenericExternalTransactionAdapter<ExternalTransactionDetail> paymentTransaction = new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction);
      Payment current = this.PaymentDoc.Current;
      bool flag = current != null && current.CCTransactionRefund.GetValueOrDefault();
      if (string.IsNullOrEmpty(paymentDoc.RefTranExtNbr) & flag)
      {
        this.PaymentDoc.Cache.RaiseExceptionHandling<Payment.refTranExtNbr>((object) paymentDoc, (object) paymentDoc.RefTranExtNbr, (Exception) new PXSetPropertyException("Enter a valid transaction number which was provided by the processing center for the original payment. You can find it in the Proc. Center Tran. Nbr column on the Credit Card Processing Information tab of the Payments and Applications (AR302000) form."));
      }
      else
      {
        CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
        ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager();
        ccPaymentEntry.CreditCCPayment(paymentDoc, (IExternalTransactionAdapter) paymentTransaction, this.SelectedProcessingCenter);
      }
    }
    return (IEnumerable) primaryList;
  }

  [PXUIField(DisplayName = "Record and Capture Preauthorization", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CaptureOnlyCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    PXTrace.WriteInformation(classMethodName + " started.");
    AccessInfo accessinfo = this.Base.Accessinfo;
    InputPaymentInfo current = this.InputPmtInfo.Current;
    if (current == null)
      return adapter.Get();
    if (string.IsNullOrEmpty(current.AuthNumber))
    {
      if (this.InputPmtInfo.Cache.RaiseExceptionHandling<InputPaymentInfo.authNumber>((object) current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[authNumber]"
      })))
        throw new PXRowPersistingException(typeof (InputPaymentInfo.authNumber).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "authNumber"
        });
      return adapter.Get();
    }
    List<TPrimary> primaryList = new List<TPrimary>();
    foreach (TPrimary doc in adapter.Get<TPrimary>())
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      primaryList.Add(doc);
      this.BeforeCaptureOnlyPayment(doc);
      GenericExternalTransactionAdapter<ExternalTransactionDetail> paymentTransaction = new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction);
      CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
      ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager();
      ccPaymentEntry.CaptureOnlyCCPayment(current, paymentDoc, (IExternalTransactionAdapter) paymentTransaction);
    }
    return (IEnumerable) primaryList;
  }

  [PXUIField(DisplayName = "Validate Card Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  public virtual IEnumerable ValidateCCPayment(PXAdapter adapter)
  {
    PXTrace.WriteInformation(this.GetClassMethodName() + " started.");
    List<TPrimary> list = adapter.Get<TPrimary>().ToList<TPrimary>();
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
    {
      TGraph instance = PXGraph.CreateInstance<TGraph>();
      foreach (TPrimary doc1 in list)
      {
        PaymentTransactionGraph<TGraph, TPrimary> implementation = instance.FindImplementation<PaymentTransactionGraph<TGraph, TPrimary>>();
        TPrimary doc2 = implementation.SetCurrentDocument(instance, doc1);
        implementation.CheckProcCenterDisabled();
        implementation.DoValidateCCPayment(doc2);
      }
    }));
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Record Card Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable RecordCCPayment(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName();
    PXTrace.WriteInformation(classMethodName + " started.");
    AccessInfo accessinfo = this.Base.Accessinfo;
    List<TPrimary> list = adapter.Get<TPrimary>().ToList<TPrimary>();
    InputPaymentInfo current = this.InputPmtInfo.Current;
    if (current == null)
      return (IEnumerable) list;
    if (!this.ValidateRecordedInfo(current))
      return (IEnumerable) list;
    this.InputPmtInfo.Cache.Clear();
    TPrimary doc = list.FirstOrDefault<TPrimary>();
    if ((object) doc != null)
    {
      this.CheckProcCenterDisabled();
      this.CheckDocumentUpdatedInDb(doc);
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      PXTrace.WriteInformation($"{classMethodName}. RefNbr:{paymentDoc.RefNbr}; UserName:{accessinfo.UserName}");
      if (!CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(this.SelectedProcessingCenter), CCProcessingFeature.TransactionGetter))
        throw new PXException("The transaction cannot be recorded. The {0} processing center does not support pulling transaction information from the processing center.", new object[1]
        {
          (object) this.SelectedProcessingCenter
        });
      TransactionData details = this.GetTranDetails(current.PCTranNumber.Trim());
      this.ValidateTransactionData(paymentDoc, details);
      if (details.TranType.GetValueOrDefault() == 4 && details.RefTranID != null && paymentDoc.RefTranExtNbr == null)
        this.PaymentDoc.Cache.SetValue<Payment.refTranExtNbr>((object) this.PaymentDoc.Current, (object) details.RefTranID);
      bool? released = paymentDoc.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
        this.Base.Actions.PressSave();
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
      {
        TGraph instance = PXGraph.CreateInstance<TGraph>();
        PaymentTransactionGraph<TGraph, TPrimary> implementation = instance.FindImplementation<PaymentTransactionGraph<TGraph, TPrimary>>();
        TPrimary doc1 = implementation.SetCurrentDocument(instance, doc);
        CCPaymentEntry paymentEntry = new CCPaymentEntry((PXGraph) instance);
        implementation.CheckSaveCardOption(details);
        implementation.RecordTransaction(doc1, details, paymentEntry);
      }));
    }
    return (IEnumerable) list;
  }

  public void CheckSaveCardOption(TransactionData details)
  {
    if (!this.NeedSaveCard() || details.CustomerId == null || details.PaymentId == null)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      int? profileIfNeeded = this.CreateProfileIfNeeded(details);
      int? nullable = profileIfNeeded;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable.HasValue == newPaymentProfile.HasValue))
      {
        this.SetPmInstanceId(profileIfNeeded);
        this.Base.Actions.PressSave();
      }
      transactionScope.Complete();
    }
  }

  protected virtual TPrimary DoValidateCCPayment(TPrimary doc)
  {
    ICCPayment paymentDoc = this.GetPaymentDoc(doc);
    IExternalTransaction activeTransaction = ExternalTranHelper.GetActiveTransaction(this.GetExtTrans());
    if (activeTransaction != null && activeTransaction.DocType == paymentDoc.DocType)
      this.TranStatusChanged(paymentDoc, activeTransaction.TransactionID);
    this.RestoreDocStateByTransactionIfNeeded(doc);
    return doc;
  }

  public virtual void RecordTransaction(
    TPrimary doc,
    TransactionData details,
    CCPaymentEntry paymentEntry)
  {
    paymentEntry.AfterProcessingManager = this.GetAfterProcessingManager(this.Base);
    ICCPayment paymentDoc = this.GetPaymentDoc(doc);
    CCTranType tranType = V2Converter.ConvertTranType(details.TranType.Value);
    GenericExternalTransactionAdapter<ExternalTransactionDetail> paymentTransaction = new GenericExternalTransactionAdapter<ExternalTransactionDetail>((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction);
    TranRecordData tranRecord = this.FormatRecordData(details);
    tranRecord.RefExternalTranId = paymentDoc.RefTranExtNbr;
    this.RaiseBeforeAction(tranType, doc);
    if (this.DocIsPayment(paymentDoc))
    {
      switch (tranType)
      {
        case CCTranType.AuthorizeAndCapture:
          paymentEntry.RecordAuthCapture(paymentDoc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
          break;
        case CCTranType.AuthorizeOnly:
          paymentEntry.RecordAuthorization(paymentDoc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
          break;
        case CCTranType.PriorAuthorizedCapture:
          paymentEntry.RecordPriorAuthCapture(paymentDoc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
          break;
        case CCTranType.CaptureOnly:
          paymentEntry.RecordCaptureOnly(paymentDoc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
          break;
        case CCTranType.Credit:
          paymentEntry.RecordCCCredit(paymentDoc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
          break;
      }
    }
    else
    {
      if (!EnumerableExtensions.IsIn<string>(paymentDoc.DocType, "REF", "RPM") || tranType != CCTranType.Credit)
        return;
      paymentEntry.RecordCCCredit(paymentDoc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
    }
  }

  private TranRecordData FormatRecordData(TransactionData info)
  {
    TranRecordData tranRecordData = new TranRecordData();
    tranRecordData.ExternalTranId = info.TranID;
    tranRecordData.AuthCode = info.AuthCode;
    tranRecordData.TransactionDate = new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(info.SubmitTime, LocaleInfo.GetTimeZone()));
    tranRecordData.ProcessingCenterId = this.SelectedProcessingCenter;
    tranRecordData.TranStatus = CCTranStatusCode.GetCode(V2Converter.ConvertTranStatus(info.TranStatus));
    tranRecordData.CvvVerificationCode = CVVVerificationStatusCode.GetCCVCode(V2Converter.ConvertCvvStatus(info.CcvVerificationStatus));
    tranRecordData.Imported = true;
    tranRecordData.CardType = V2Converter.ConvertCardType(info.CardTypeCode);
    tranRecordData.ProcCenterCardTypeCode = info.CardType;
    tranRecordData.PayLinkExternalId = info.PayLinkExternalID;
    tranRecordData.Tax = info.Tax;
    tranRecordData.Level3Support = info.Level3Support;
    tranRecordData.TerminalID = info.TerminalID;
    if (info.ExpireAfterDays.HasValue)
    {
      System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(info.SubmitTime, LocaleInfo.GetTimeZone()).AddDays((double) info.ExpireAfterDays.Value);
      tranRecordData.ExpirationDate = new System.DateTime?(dateTime);
    }
    tranRecordData.LastDigits = !string.IsNullOrEmpty(info.CardNumber) ? info.CardNumber.Substring(info.CardNumber.Length - 4) : string.Empty;
    return tranRecordData;
  }

  protected virtual bool ValidateRecordedInfo(InputPaymentInfo info)
  {
    bool flag = true;
    if (string.IsNullOrEmpty(info.PCTranNumber))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[PCTranNumber]"
      });
      if (this.InputPmtInfo.Cache.RaiseExceptionHandling<InputPaymentInfo.pCTranNumber>((object) info, (object) null, (Exception) propertyException))
        throw propertyException;
      flag = false;
    }
    return flag;
  }

  protected TransactionData GetTranDetails(string transactionId)
  {
    return this.GetPaymentProcessing().GetTransactionById(transactionId, this.SelectedProcessingCenter);
  }

  protected virtual bool RunPendingOperations(TPrimary doc) => true;

  protected virtual bool TrySyncByStoredIds(TPrimary doc, IExternalTransaction extTran) => false;

  protected virtual void ValidateTransactionData(ICCPayment doc, TransactionData tranData)
  {
    TranValidationHelper.CheckRecordedTranStatus(tranData);
    if (tranData.TranType.GetValueOrDefault() == 5)
      throw new PXException("The {0} transaction is voided.", new object[1]
      {
        (object) tranData.TranID
      });
    if (tranData.TranType.GetValueOrDefault() == 9)
      throw new PXException("The {0} transaction is rejected.", new object[1]
      {
        (object) tranData.TranID
      });
    if (doc.DocType != "REF" && tranData.TranType.GetValueOrDefault() == 4)
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) tranData.TranID
      });
    if (doc.DocType == "REF" && tranData.TranType.GetValueOrDefault() != 4)
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) tranData.TranID
      });
    if (doc.DocType == "REF" && tranData.RefTranID != null && doc.RefTranExtNbr != null && tranData.RefTranID != doc.RefTranExtNbr)
      throw new PXException("The {0} transaction does not refund the transaction with the {1} transaction number that is entered in the Orig. Transaction box.", new object[2]
      {
        (object) tranData.TranID,
        (object) doc.RefTranExtNbr
      });
    this.ValidateCustomerProfile(doc, tranData);
    TranValidationHelper.CheckTranAlreadyRecorded(tranData, new TranValidationHelper.AdditionalParams()
    {
      ProcessingCenter = this.SelectedProcessingCenter,
      PMInstanceId = doc.PMInstanceID,
      Repo = this.GetPaymentRepository()
    });
    Decimal? curyDocBal = this.PaymentDoc.Current.CuryDocBal;
    Decimal amount = tranData.Amount;
    if (!(curyDocBal.GetValueOrDefault() == amount & curyDocBal.HasValue))
      throw new PXException("The {0} transaction amount is not the same as the payment amount.", new object[1]
      {
        (object) tranData.TranID
      });
    PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, this.SelectedPaymentMethod);
    if (!(paymentMethod.PaymentType == "EFT") || tranData.PaymentMethodType.GetValueOrDefault() != 1)
    {
      MeansOfPayment? paymentMethodType;
      if (paymentMethod.PaymentType == "CCD")
      {
        paymentMethodType = tranData.PaymentMethodType;
        MeansOfPayment meansOfPayment = (MeansOfPayment) 0;
        if (paymentMethodType.GetValueOrDefault() == meansOfPayment & paymentMethodType.HasValue)
          return;
        paymentMethodType = tranData.PaymentMethodType;
        if (!paymentMethodType.HasValue)
          return;
      }
      if (paymentMethod.PaymentType == "POS")
      {
        paymentMethodType = tranData.PaymentMethodType;
        MeansOfPayment meansOfPayment = (MeansOfPayment) 0;
        if (paymentMethodType.GetValueOrDefault() == meansOfPayment & paymentMethodType.HasValue)
          return;
      }
      throw new PXException("The payment method with the {0} means of payment is not correct for this transaction. Select a payment method with the {1} means of payment.", new object[2]
      {
        (object) paymentMethod.PaymentType,
        (object) tranData.PaymentMethodType
      });
    }
  }

  protected void ValidateSharedTranSyncStatus(ICCPayment doc, IExternalTransaction extTran)
  {
    if (this.DocIsPayment(doc) && extTran.VoidDocType == "REF" && extTran.NeedSync.GetValueOrDefault())
    {
      string documentName = TranValidationHelper.GetDocumentName(extTran.VoidDocType);
      throw new PXException("The void or refund operation for the {0} transaction has been imported for the {1} {2}. Open the {1} {2} and use the Validate Card Payment command on the More menu.", new object[3]
      {
        (object) extTran.TranNumber,
        (object) extTran.VoidRefNbr,
        (object) documentName
      });
    }
  }

  protected void ValidateCustomerProfile(ICCPayment doc, TransactionData tranData)
  {
    ICCPaymentProcessingRepository paymentRepository = this.GetPaymentRepository();
    TranValidationHelper.CheckPaymentProfile(tranData, new TranValidationHelper.AdditionalParams()
    {
      CustomerID = this.SelectedBAccount,
      PMInstanceId = doc.PMInstanceID,
      ProcessingCenter = this.SelectedProcessingCenter,
      Repo = paymentRepository
    });
  }

  protected string GetClassMethodName()
  {
    StackTrace stackTrace = PXStackTrace.GetStackTrace();
    string name = stackTrace.GetFrame(1).GetMethod().Name;
    string str = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name;
    int length = str.IndexOf('`');
    if (length >= 0)
      str = str.Substring(0, length);
    return $"{str}.{name}";
  }

  public virtual void clearCCInfo()
  {
    InputPaymentInfo current = this.InputPmtInfo.Current;
    // ISSUE: variable of the null type
    __Null local;
    string str = (string) (local = null);
    current.AuthNumber = (string) local;
    current.PCTranNumber = str;
  }

  public virtual void initAuthCCInfo(PXGraph aGraph, string ViewName)
  {
    InputPaymentInfo current = this.InputPmtInfo.Current;
    current.PCTranNumber = current.AuthNumber = (string) null;
    PXUIFieldAttribute.SetVisible<InputPaymentInfo.pCTranNumber>(this.InputPmtInfo.Cache, (object) current, false);
  }

  protected virtual CCPaymentEntry GetCCPaymentEntry(PXGraph graph) => new CCPaymentEntry(graph);

  public override void Initialize()
  {
    base.Initialize();
    this.MapViews(this.Base);
  }

  public virtual PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing GetPaymentProcessing()
  {
    if (this.paymentProcessing == null)
      this.paymentProcessing = new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing((PXGraph) this.Base);
    return this.paymentProcessing;
  }

  public virtual ICCPaymentProcessingRepository GetPaymentRepository()
  {
    if (this.paymentProcessing == null)
      this.paymentProcessing = new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing((PXGraph) this.Base);
    return this.paymentProcessing.Repository;
  }

  protected virtual void CheckDocumentUpdatedInDb(TPrimary doc)
  {
    if (this.Base.Caches[typeof (TPrimary)].GetStatus((object) doc) != PXEntryStatus.Notchanged)
      return;
    EntityHelper entityHelper = new EntityHelper((PXGraph) this.Base);
    object entityRow = entityHelper.GetEntityRow(typeof (TPrimary), entityHelper.GetEntityKey(typeof (TPrimary), (object) doc));
    if (entityRow != null && this.Base.SqlDialect.CompareTimestamps(this.Base.Caches[typeof (TPrimary)].GetValue((object) doc, "tstamp") as byte[], this.Base.Caches[typeof (TPrimary)].GetValue(entityRow, "tstamp") as byte[]) < 0)
      throw new PXException("Another process has updated the '{0}' record. Your changes will be lost.", new object[1]
      {
        (object) typeof (TPrimary).Name
      });
  }

  protected virtual void CheckProcCenterDisabled()
  {
    CCPluginTypeHelper.ThrowIfProcCenterFeatureDisabled(this.SelectedProcessingCenterType);
  }

  protected virtual bool IsProcCenterDisabled(string procCenterType)
  {
    return CCPluginTypeHelper.IsProcCenterFeatureDisabled(procCenterType);
  }

  public virtual ICCPayment GetPaymentDoc(TPrimary doc)
  {
    if (!(doc is ICCPayment ccPayment))
      ccPayment = this.PaymentDoc.View.SelectSingleBound(new object[1]
      {
        (object) doc
      }) as ICCPayment;
    return ccPayment != null ? ccPayment : throw new PXException("Could not get object which implemented ICCPayment interface.");
  }

  public void SyncProfile(TPrimary doc, TransactionData tranData)
  {
    ExternalTransactionDetail transactionDetail = this.GetExtTranDetails().FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => i.TranNumber == tranData.TranID));
    if (transactionDetail == null || this.SelectedProcessingCenter == null || !this.SelectedBAccount.HasValue || !transactionDetail.SaveProfile.GetValueOrDefault())
      return;
    int? pmInstanceId1 = new int?();
    if (this.CheckAllowSavingCards())
    {
      ICCPayment paymentDoc = this.GetPaymentDoc(doc);
      if (tranData.CustomerId != null && tranData.PaymentId != null)
      {
        int? pmInstanceId2 = paymentDoc.PMInstanceID;
        int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
        if (pmInstanceId2.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId2.HasValue == newPaymentProfile.HasValue)
          pmInstanceId1 = this.CreateProfileIfNeeded(tranData);
      }
      int? nullable = pmInstanceId1;
      int? newPaymentProfile1 = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable.GetValueOrDefault() == newPaymentProfile1.GetValueOrDefault() & nullable.HasValue == newPaymentProfile1.HasValue))
      {
        bool? released = paymentDoc.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue)
        {
          this.SetPmInstanceId(pmInstanceId1);
          transactionDetail.PMInstanceID = pmInstanceId1;
          this.ExternalTransaction.Update(transactionDetail);
        }
      }
    }
    transactionDetail.SaveProfile = new bool?(false);
    this.ExternalTransaction.Update(transactionDetail);
  }

  public int? CreateProfileIfNeeded(TransactionData details)
  {
    string customerId = details.CustomerId;
    string paymentId = details.PaymentId;
    Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> withProfileDetail = this.GetPaymentRepository().GetCustomerPaymentMethodWithProfileDetail(this.SelectedProcessingCenter, customerId, paymentId);
    int? profileIfNeeded = PaymentTranExtConstants.NewPaymentProfile;
    if (withProfileDetail != null)
    {
      PX.Objects.AR.CustomerPaymentMethod cpm = withProfileDetail.Item1;
      if (cpm != null && cpm.IsActive.GetValueOrDefault() && this.SelectedPaymentMethod == cpm.PaymentMethodID)
      {
        TranValidationHelper.CheckCustomer(details, this.SelectedBAccount, cpm);
        profileIfNeeded = cpm.PMInstanceID;
      }
    }
    else
    {
      TranProfile input = new TranProfile()
      {
        CustomerProfileId = customerId,
        PaymentProfileId = paymentId
      };
      PaymentProfileCreator paymentProfileCreator = this.GetPaymentProfileCreator();
      paymentProfileCreator.PrepeareCpmRecord();
      profileIfNeeded = paymentProfileCreator.CreatePaymentProfile(input);
      int? nullable = profileIfNeeded;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(nullable.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable.HasValue == newPaymentProfile.HasValue))
        paymentProfileCreator.CreateCustomerProcessingCenterRecord(input);
      paymentProfileCreator.ClearCaches();
    }
    return profileIfNeeded;
  }

  public bool TranStatusChanged(ICCPayment doc, int? tranId)
  {
    bool flag = false;
    ExternalTransactionDetail extTranDet = this.GetExtTranDetails().Where<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i =>
    {
      int? transactionId = i.TransactionID;
      int? nullable = tranId;
      return transactionId.GetValueOrDefault() == nullable.GetValueOrDefault() & transactionId.HasValue == nullable.HasValue;
    })).FirstOrDefault<ExternalTransactionDetail>();
    if (this.SelectedProcessingCenter == null)
    {
      this.SelectedProcessingCenter = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this.Base, doc.PMInstanceID).CCProcessingCenterID;
      this.SelectedProcessingCenterType = this.GetProcessingCenterById(this.SelectedProcessingCenter)?.ProcessingTypeName;
    }
    if (extTranDet != null && this.SelectedProcessingCenter != null && this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionGetter, false))
    {
      TransactionData transactionById = this.GetPaymentProcessing().GetTransactionById(extTranDet.TranNumber, this.SelectedProcessingCenter);
      string processingStatus = this.GetProcessingStatus(transactionById);
      if (extTranDet.ProcStatus != processingStatus)
      {
        this.UpdateSyncStatus(transactionById, extTranDet);
        CCTranType? tranType = transactionById.TranType;
        if (tranType.GetValueOrDefault() == 1)
        {
          this.RecordTran(doc, transactionById, new Action<ICCPayment, TransactionData>(this.RecordAuth));
          flag = true;
        }
        tranType = transactionById.TranType;
        if (tranType.GetValueOrDefault() == 2)
        {
          this.RecordTran(doc, transactionById, new Action<ICCPayment, TransactionData>(this.RecordCapture));
          flag = true;
        }
        tranType = transactionById.TranType;
        CCTranType ccTranType = (CCTranType) 0;
        if (tranType.GetValueOrDefault() == ccTranType & tranType.HasValue)
        {
          this.RecordTran(doc, transactionById, new Action<ICCPayment, TransactionData>(this.RecordCapture));
          flag = true;
        }
        if (EnumerableExtensions.IsIn<CCTranType?>(transactionById.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)))
        {
          this.RecordTran(doc, transactionById, new Action<ICCPayment, TransactionData>(this.RecordVoid));
          flag = true;
        }
        tranType = transactionById.TranType;
        if (tranType.GetValueOrDefault() == 4)
        {
          this.RecordTran(doc, transactionById, new Action<ICCPayment, TransactionData>(this.RecordCredit));
          flag = true;
        }
      }
    }
    return flag;
  }

  private void RaiseBeforeAction(CCTranType tranType, TPrimary doc)
  {
    switch (tranType)
    {
      case CCTranType.AuthorizeAndCapture:
      case CCTranType.PriorAuthorizedCapture:
        this.BeforeCapturePayment(doc);
        break;
      case CCTranType.AuthorizeOnly:
        this.BeforeAuthorizePayment(doc);
        break;
      case CCTranType.CaptureOnly:
        this.BeforeCaptureOnlyPayment(doc);
        break;
      case CCTranType.Credit:
        this.BeforeCreditPayment(doc);
        break;
    }
  }

  private void RecordTran(
    ICCPayment doc,
    TransactionData tranData,
    Action<ICCPayment, TransactionData> action)
  {
    action(doc, tranData);
  }

  public void CheckHeldForReviewTranStatus(ICCPayment doc)
  {
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    if (!transactionState.IsOpenForReview)
      return;
    int? tranID = transactionState.ExternalTransaction.TransactionID;
    if (this.TranStatusChanged(doc, tranID))
    {
      IExternalTransaction externalTransaction = this.GetExtTrans().Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
      {
        int? transactionId = i.TransactionID;
        int? nullable = tranID;
        return transactionId.GetValueOrDefault() == nullable.GetValueOrDefault() & transactionId.HasValue == nullable.HasValue;
      })).FirstOrDefault<IExternalTransaction>();
      if (externalTransaction == null || !(externalTransaction.ProcStatus == "VDS"))
      {
        bool? active = externalTransaction.Active;
        bool flag = false;
        if (!(active.GetValueOrDefault() == flag & active.HasValue))
          return;
      }
      this.PaymentDoc.Cache.Clear();
      this.PaymentDoc.Cache.ClearQueryCache();
      throw new PXException("Cannot capture the pre-authorized credit card payment. The authorization transaction is declined.");
    }
    if (this.IsFeatureSupported(this.SelectedProcessingCenter, CCProcessingFeature.TransactionGetter, true))
      throw new PXException(this.TranHeldwarnMsg);
    throw new PXException("The transaction is held for review. Approval is not supported by the {0} processing center integration plug-in. Please void the transaction.", new object[1]
    {
      (object) this.SelectedProcessingCenter
    });
  }

  public ExternalTransactionState GetActiveTransactionState()
  {
    return ExternalTranHelper.GetActiveTransactionState((PXGraph) this.Base, this.GetExtTrans());
  }

  public string GetLastTransactionDescription()
  {
    string transactionDescription = (string) null;
    IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
    IExternalTransaction targetTran = extTrans.FirstOrDefault<IExternalTransaction>();
    int? nullable1;
    if (this.PaymentDoc.Current?.DocType == "RPM")
    {
      Payment current = this.PaymentDoc.Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        nullable1 = current.CCActualExternalTransactionID;
        num = nullable1.HasValue ? 1 : 0;
      }
      if (num != 0)
        targetTran = extTrans.Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
        {
          int? transactionId = i.TransactionID;
          int? externalTransactionId = (int?) this.PaymentDoc.Current?.CCActualExternalTransactionID;
          return transactionId.GetValueOrDefault() == externalTransactionId.GetValueOrDefault() & transactionId.HasValue == externalTransactionId.HasValue;
        })).FirstOrDefault<IExternalTransaction>();
    }
    if (targetTran != null && targetTran.SyncStatus == "E")
    {
      bool? active = targetTran.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        Payment current = this.PaymentDoc.Current;
        int? nullable2;
        if (current == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = current.CCActualExternalTransactionID;
        int? actualTranId = nullable2;
        if (actualTranId.HasValue)
        {
          IEnumerable<PaymentTransactionDetail> paymentTranDetails = this.GetPaymentTranDetails();
          PaymentTransactionDetail transactionDetail1 = paymentTranDetails.Where<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
          {
            int? transactionId = i.TransactionID;
            int? nullable3 = actualTranId;
            return transactionId.GetValueOrDefault() == nullable3.GetValueOrDefault() & transactionId.HasValue == nullable3.HasValue;
          })).FirstOrDefault<PaymentTransactionDetail>();
          PaymentTransactionDetail transactionDetail2 = paymentTranDetails.Where<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
          {
            int? transactionId1 = i.TransactionID;
            int? transactionId2 = targetTran.TransactionID;
            return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
          })).FirstOrDefault<PaymentTransactionDetail>();
          nullable1 = transactionDetail1.TranNbr;
          int? tranNbr = transactionDetail2.TranNbr;
          if (nullable1.GetValueOrDefault() > tranNbr.GetValueOrDefault() & nullable1.HasValue & tranNbr.HasValue)
            targetTran = extTrans.Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
            {
              int? transactionId = i.TransactionID;
              int? nullable4 = actualTranId;
              return transactionId.GetValueOrDefault() == nullable4.GetValueOrDefault() & transactionId.HasValue == nullable4.HasValue;
            })).FirstOrDefault<IExternalTransaction>();
        }
      }
    }
    if (targetTran != null)
      transactionDescription = this.GetTransactionStateDescription(targetTran);
    return transactionDescription;
  }

  public virtual string GetTransactionStateDescription(IExternalTransaction targetTran)
  {
    return ExternalTranHelper.GetTransactionState((PXGraph) this.Base, targetTran).Description;
  }

  public bool NeedSaveCard()
  {
    PXSelectExtension<Payment> paymentDoc = this.PaymentDoc;
    return (paymentDoc != null ? (paymentDoc.Current.SaveCard.GetValueOrDefault() ? 1 : 0) : 0) != 0 && this.CheckAllowSavingCards();
  }

  public IEnumerable<ICCPaymentTransaction> GetProcTrans()
  {
    foreach (ICCPaymentTransaction paymentTranDetail in this.GetPaymentTranDetails())
      yield return paymentTranDetail;
  }

  public IEnumerable<PaymentTransactionDetail> GetPaymentTranDetails()
  {
    if (this.PaymentTransaction != null)
    {
      foreach (PaymentTransactionDetail paymentTranDetail in this.PaymentTransaction.Select().RowCast<PaymentTransactionDetail>())
        yield return paymentTranDetail;
    }
  }

  public IEnumerable<ExternalTransactionDetail> GetExtTranDetails()
  {
    if (this.ExternalTransaction != null)
    {
      foreach (ExternalTransactionDetail extTranDetail in this.ExternalTransaction.Select().RowCast<ExternalTransactionDetail>())
        yield return extTranDetail;
    }
  }

  public IEnumerable<IExternalTransaction> GetExtTrans()
  {
    foreach (IExternalTransaction extTranDetail in this.GetExtTranDetails())
      yield return extTranDetail;
  }

  protected virtual void ValidateTran(TPrimary doc, TransactionData tranData)
  {
    IExternalTransaction externalTransaction = this.GetExtTrans().FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranNumber == tranData.TranID));
    if (externalTransaction == null || !externalTransaction.NeedSync.GetValueOrDefault())
      return;
    ICCPayment paymentDoc = this.GetPaymentDoc(doc);
    if (paymentDoc.DocType == "REF")
    {
      TranValidationHelper.CheckTranTypeForRefund(tranData);
      if (tranData.TranType.GetValueOrDefault() == 4 && tranData.RefTranID != null && paymentDoc.RefTranExtNbr != null && tranData.RefTranID != paymentDoc.RefTranExtNbr)
        throw new TranValidationHelper.TranValidationException("The {0} transaction does not refund the transaction with the {1} transaction number that is entered in the Orig. Transaction box.", new object[2]
        {
          (object) tranData.TranID,
          (object) paymentDoc.RefTranExtNbr
        });
      if (EnumerableExtensions.IsIn<CCTranType?>(tranData.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)) && this.NeedMergeTransactionForRefund(paymentDoc, externalTransaction, tranData.TranType.Value))
        TranValidationHelper.CheckSharedTranIsSuitableForRefund(paymentDoc, tranData, new TranValidationHelper.AdditionalParams()
        {
          Repo = this.GetPaymentRepository()
        });
    }
    if (this.DocIsPayment(paymentDoc))
    {
      string str = paymentDoc.DocType + paymentDoc.RefNbr;
      bool? released = paymentDoc.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue && tranData.TranType.GetValueOrDefault() == 4)
        throw new TranValidationHelper.TranValidationException("The {0} payment is not released.", new object[1]
        {
          (object) str
        });
      ExternalTransactionState transactionState = this.GetActiveTransactionState();
      TranValidationHelper.CheckActiveTransactionStateForPayment(paymentDoc, tranData, transactionState);
      this.ValidateSharedTranSyncStatus(paymentDoc, externalTransaction);
    }
    string processingStatus = this.GetProcessingStatus(tranData);
    bool flag1 = externalTransaction.ProcStatus == "AUS" && processingStatus == "CAS";
    bool flag2 = processingStatus == "CDS" && this.DocIsPayment(paymentDoc);
    if (processingStatus == externalTransaction.ProcStatus | flag1 | flag2 && processingStatus != "VDS")
      TranValidationHelper.CheckTranAmount(tranData, externalTransaction);
    this.ValidateCustomerProfile(paymentDoc, tranData);
  }

  protected virtual void UpdateNeedSyncDoc(TPrimary doc, TransactionData tranData)
  {
    ExternalTransactionDetail storedTran = this.GetExtTranDetails().FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => i.TranNumber == tranData.TranID));
    if (storedTran == null)
      return;
    ICCPayment paymentDoc1 = this.GetPaymentDoc(doc);
    ICCPayment pmt1 = paymentDoc1;
    ExternalTransactionDetail extTran1 = storedTran;
    CCTranType? tranType1 = tranData.TranType;
    CCTranType tranType2 = tranType1.Value;
    int num1 = this.NeedMergeTransactionForPayment(pmt1, (IExternalTransaction) extTran1, tranType2) ? 1 : 0;
    ICCPayment pmt2 = paymentDoc1;
    ExternalTransactionDetail extTran2 = storedTran;
    tranType1 = tranData.TranType;
    CCTranType tranType3 = tranType1.Value;
    int num2 = this.NeedMergeTransactionForRefund(pmt2, (IExternalTransaction) extTran2, tranType3) ? 1 : 0;
    bool flag = (num1 | num2) != 0;
    if (!storedTran.NeedSync.GetValueOrDefault())
      return;
    if (flag)
    {
      this.PaymentTransaction.Delete(this.GetPaymentTranDetails().First<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
      {
        int? transactionId1 = i.TransactionID;
        int? transactionId2 = storedTran.TransactionID;
        return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
      })));
      this.ExternalTransaction.Delete(storedTran);
    }
    else
    {
      storedTran.NeedSync = new bool?(false);
      if (storedTran.ProcStatus == "VDS")
        storedTran.Active = new bool?(false);
      if (storedTran.ProcStatus == "CDS")
        storedTran.Active = new bool?(true);
      this.ExternalTransaction.Update(storedTran);
    }
    this.PersistChangesIfNeeded();
    ICCPayment paymentDoc2 = this.GetPaymentDoc(doc);
    string processingStatus = this.GetProcessingStatus(tranData);
    if (flag)
    {
      TranRecordData tranRecordData = this.FormatTranRecord(tranData);
      Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> transactionWithPayment = this.GetPaymentRepository().GetExternalTransactionWithPayment(tranData.RefTranID, this.SelectedProcessingCenter);
      tranRecordData.RefInnerTranId = (int?) transactionWithPayment?.Item1.TransactionID;
      if (EnumerableExtensions.IsIn<CCTranType?>(tranData.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)))
      {
        if (paymentDoc2.DocType == "REF")
          tranRecordData.AllowFillVoidRef = true;
        this.RecordVoid(paymentDoc2, tranRecordData);
      }
      tranType1 = tranData.TranType;
      if (tranType1.GetValueOrDefault() != 2)
        return;
      this.RecordCapture(paymentDoc2, (CCTranType) 2, tranRecordData);
    }
    else if (processingStatus == storedTran.ProcStatus)
    {
      TPrimary doc1 = doc;
      tranType1 = tranData.TranType;
      int tranType4 = (int) V2Converter.ConvertTranType(tranType1.Value);
      this.RunCallbacks(doc1, (CCTranType) tranType4);
    }
    else
      this.TranStatusChanged(paymentDoc2, storedTran.TransactionID);
  }

  protected bool DocIsPayment(ICCPayment doc) => doc.DocType == "PMT" || doc.DocType == "PPM";

  protected string GetProcessingStatus(TransactionData tranData)
  {
    return ExtTransactionProcStatusCode.GetProcStatusStrByProcessingStatus(CCProcessingHelper.GetProcessingStatusByTranData(tranData));
  }

  protected CCProcessingCenter GetProcessingCenterById(string id)
  {
    return (CCProcessingCenter) PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select((PXGraph) this.Base, (object) id);
  }

  protected virtual PaymentProfileCreator GetPaymentProfileCreator()
  {
    return new PaymentProfileCreator(PXGraph.CreateInstance<CCPaymentHelperGraph>(), this.SelectedPaymentMethod, this.SelectedProcessingCenter, this.SelectedBAccount);
  }

  protected virtual bool IsFeatureSupported(
    string procCenterId,
    CCProcessingFeature feature,
    bool throwOnError)
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(procCenterId), feature, throwOnError);
  }

  protected virtual void RecordAuth(ICCPayment doc, TransactionData tranData)
  {
    TranRecordData tranRecord = this.FormatTranRecord(tranData);
    if (tranData.ExpireAfterDays.HasValue)
    {
      System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(tranData.SubmitTime, LocaleInfo.GetTimeZone()).AddDays((double) tranData.ExpireAfterDays.Value);
      tranRecord.ExpirationDate = new System.DateTime?(dateTime);
    }
    CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
    ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager(this.Base);
    ccPaymentEntry.RecordAuthorization(doc, tranRecord);
  }

  private void RunAuthCallbacks(IBqlTable doc, CCTranType tranType)
  {
    AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
    if (tranType == CCTranType.IncreaseAuthorizedAmount)
      processingManager.RunIncreaseAuthorizedAmountActions(doc, true);
    else
      processingManager.RunAuthorizeActions(doc, true);
    processingManager.PersistData();
  }

  protected virtual void RecordVoid(ICCPayment doc, TransactionData tranData)
  {
    TranRecordData tranRecordData = this.FormatTranRecord(tranData);
    tranRecordData.AuthCode = tranData.AuthCode;
    if (tranData.RefTranID != null)
    {
      IExternalTransaction externalTransaction = this.GetExtTrans().FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranNumber == tranData.RefTranID && i.Active.GetValueOrDefault()));
      if (externalTransaction != null)
        tranRecordData.RefInnerTranId = externalTransaction.TransactionID;
    }
    this.RecordVoid(doc, tranRecordData);
  }

  private void RunVoidCallbacks(IBqlTable doc)
  {
    AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
    processingManager.RunVoidActions(doc, true);
    processingManager.PersistData();
  }

  private bool NeedMergeTransactionForPayment(
    ICCPayment pmt,
    IExternalTransaction extTran,
    CCTranType tranType)
  {
    bool? active = extTran.Active;
    bool flag = false;
    return active.GetValueOrDefault() == flag & active.HasValue && extTran.NeedSync.GetValueOrDefault() && EnumerableExtensions.IsIn<CCTranType>(tranType, (CCTranType) 2, (CCTranType) 5, (CCTranType) 9) && this.DocIsPayment(pmt);
  }

  private bool NeedMergeTransactionForRefund(
    ICCPayment pmt,
    IExternalTransaction extTran,
    CCTranType tranType)
  {
    return extTran.NeedSync.GetValueOrDefault() && tranType == 5 && extTran.VoidDocType == null && pmt.DocType == "REF";
  }

  protected virtual void RecordCapture(ICCPayment doc, TransactionData tranData)
  {
    TranRecordData tranRecordData1 = this.FormatTranRecord(tranData);
    if (tranData.ExpireAfterDays.HasValue)
    {
      System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(tranData.SubmitTime, LocaleInfo.GetTimeZone()).AddDays((double) tranData.ExpireAfterDays.Value);
      tranRecordData1.ExpirationDate = new System.DateTime?(dateTime);
    }
    CCTranType? tranType1;
    if (tranData.RefTranID != null)
    {
      tranType1 = tranData.TranType;
      if (tranType1.GetValueOrDefault() == 2)
      {
        IExternalTransaction externalTransaction = this.GetExtTrans().FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranNumber == tranData.RefTranID && i.Active.GetValueOrDefault()));
        if (externalTransaction != null)
          tranRecordData1.RefInnerTranId = externalTransaction.TransactionID;
      }
    }
    ICCPayment doc1 = doc;
    tranType1 = tranData.TranType;
    CCTranType tranType2 = tranType1.Value;
    TranRecordData tranRecordData2 = tranRecordData1;
    this.RecordCapture(doc1, tranType2, tranRecordData2);
  }

  protected virtual void RecordCredit(ICCPayment doc, TransactionData tranData)
  {
    CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
    ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager(this.Base);
    TranRecordData tranRecord = this.FormatTranRecord(tranData);
    tranRecord.TransactionDate = new System.DateTime?(tranData.SubmitTime);
    tranRecord.RefExternalTranId = tranData.RefTranID;
    ccPaymentEntry.RecordCredit(doc, tranRecord);
  }

  protected virtual void DeactivateNotFoundTran(ExternalTransactionDetail extTranDet)
  {
    string message = PXMessages.LocalizeFormatNoPrefix("The {0} transaction cannot be found in the {1} processing center.", (object) extTranDet.TranNumber, (object) extTranDet.ProcessingCenterID);
    this.UpdateSyncStatus(extTranDet, SyncStatus.Error, message);
    this.DeactivateAndUpdateProcStatus(extTranDet);
  }

  protected virtual void DeactivateAndUpdateProcStatus(ExternalTransactionDetail extTranDet)
  {
    ProcessingStatus errorStatusForTran = ExternalTranHelper.GetPossibleErrorStatusForTran(ExternalTranHelper.GetTransactionState((PXGraph) this.Base, (IExternalTransaction) extTranDet));
    extTranDet.NeedSync = new bool?(false);
    Payment current = this.PaymentDoc.Current;
    if (extTranDet.DocType == current.DocType && extTranDet.RefNbr == current.RefNbr)
      extTranDet.Active = new bool?(false);
    extTranDet.ProcStatus = ExtTransactionProcStatusCode.GetProcStatusStrByProcessingStatus(errorStatusForTran);
    this.ExternalTransaction.Update(extTranDet);
    PaymentTransactionDetail transactionDetail = this.GetPaymentTranDetails().First<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTranDet.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    }));
    if (CCTranTypeCode.GetTranTypeByTranTypeStr(transactionDetail.TranType) != CCTranType.Unknown)
      transactionDetail.TranStatus = "ERR";
    this.PaymentTransaction.Update(transactionDetail);
  }

  protected virtual void UpdateSyncStatus(
    ExternalTransactionDetail extTranDet,
    SyncStatus syncStatus,
    string message)
  {
    string statusStrBySyncStatus = CCSyncStatusCode.GetSyncStatusStrBySyncStatus(syncStatus);
    extTranDet.SyncStatus = statusStrBySyncStatus;
    if (message != null)
    {
      if (!string.IsNullOrEmpty(extTranDet.SyncMessage))
      {
        if (!extTranDet.SyncMessage.Contains(message))
        {
          ExternalTransactionDetail transactionDetail = extTranDet;
          transactionDetail.SyncMessage = $"{transactionDetail.SyncMessage};{message}";
        }
      }
      else
        extTranDet.SyncMessage = message;
    }
    this.ExternalTransaction.Update(extTranDet);
  }

  protected virtual void UpdateSyncStatus(
    TransactionData tranData,
    ExternalTransactionDetail extTranDet)
  {
    if (!(extTranDet.SyncStatus != "W") || !(extTranDet.SyncStatus != "S"))
      return;
    this.UpdateSyncStatus(extTranDet, SyncStatus.Success, (string) null);
  }

  protected virtual void RestoreDocStateByTransactionIfNeeded(TPrimary doc)
  {
    if (this.PaymentDoc.Current == null)
      return;
    AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
    if (processingManager == null || processingManager.CheckDocStateConsistency((IBqlTable) doc))
      return;
    IEnumerable<IExternalTransaction> extTrans = this.GetExtTrans();
    IEnumerable<ICCPaymentTransaction> procTrans = this.GetProcTrans();
    IExternalTransaction extTran = ExternalTranHelper.GetLastProcessedExtTran(extTrans, procTrans);
    ICCPaymentTransaction paymentTransaction = procTrans.Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue && i.TranStatus != "ERR" && i.TranStatus != "DEC";
    })).FirstOrDefault<ICCPaymentTransaction>();
    if (paymentTransaction == null)
      return;
    CCTranType typeByTranTypeStr = CCTranTypeCode.GetTranTypeByTranTypeStr(paymentTransaction.TranType);
    this.RunCallbacks(doc, typeByTranTypeStr);
  }

  protected virtual void RunCallbacks(TPrimary doc, CCTranType tranType)
  {
    if (tranType == CCTranType.AuthorizeOnly || tranType == CCTranType.IncreaseAuthorizedAmount)
      this.RunAuthCallbacks((IBqlTable) doc, tranType);
    if (tranType == CCTranType.AuthorizeAndCapture || tranType == CCTranType.PriorAuthorizedCapture)
      this.RunCaptureCallbacks((IBqlTable) doc, tranType);
    if (tranType == CCTranType.Credit)
      this.RunCreditCallbacks((IBqlTable) doc);
    if (tranType == CCTranType.Void)
      this.RunVoidCallbacks((IBqlTable) doc);
    if (tranType != CCTranType.Unknown)
      return;
    this.RunUnknownCallbacks((IBqlTable) doc);
  }

  protected virtual void PersistChangesIfNeeded()
  {
    bool flag = false;
    if (this.PaymentDoc.Cache.GetStatus((object) this.PaymentDoc.Current) != PXEntryStatus.Notchanged)
      flag = true;
    else if (this.ExternalTransaction.Cache.Cached.RowCast<ExternalTransactionDetail>().Any<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i => this.ExternalTransaction.Cache.GetStatus((object) i) != 0)))
      flag = true;
    if (!flag)
      return;
    this.Base.Actions["Save"].Press();
  }

  private void RunUnknownCallbacks(IBqlTable doc)
  {
    AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
    processingManager.RunUnknownActions(doc, true);
    processingManager.PersistData();
  }

  private void RunCreditCallbacks(IBqlTable doc)
  {
    AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
    processingManager.RunCreditActions(doc, true);
    processingManager.PersistData();
  }

  private void RunCaptureCallbacks(IBqlTable doc, CCTranType tranType)
  {
    AfterProcessingManager processingManager = this.GetAfterProcessingManager(this.Base);
    if (tranType == CCTranType.PriorAuthorizedCapture)
      processingManager.RunPriorAuthorizedCaptureActions(doc, true);
    else
      processingManager.RunCaptureActions(doc, true);
    processingManager.PersistData();
  }

  private void RecordVoid(ICCPayment doc, TranRecordData tranRecordData)
  {
    CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
    ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager(this.Base);
    ccPaymentEntry.RecordVoid(doc, tranRecordData);
  }

  private void RecordCapture(ICCPayment doc, CCTranType tranType, TranRecordData tranRecordData)
  {
    CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
    ccPaymentEntry.AfterProcessingManager = this.GetAfterProcessingManager(this.Base);
    if (tranType == null)
      ccPaymentEntry.RecordAuthCapture(doc, tranRecordData);
    else if (tranType == 2)
      ccPaymentEntry.RecordPriorAuthCapture(doc, tranRecordData);
    else
      ccPaymentEntry.RecordCaptureOnly(doc, tranRecordData);
  }

  protected bool CheckAllowSavingCards()
  {
    bool? allowSaveProfile = this.GetProcessingCenterById(this.SelectedProcessingCenter).AllowSaveProfile;
    bool flag = false;
    return !(allowSaveProfile.GetValueOrDefault() == flag & allowSaveProfile.HasValue);
  }

  protected TranRecordData FormatTranRecord(TransactionData tranData)
  {
    TranRecordData tranRecordData = new TranRecordData();
    tranRecordData.ExternalTranId = tranData.TranID;
    tranRecordData.Amount = new Decimal?(tranData.Amount);
    tranRecordData.AuthCode = tranData.AuthCode;
    tranRecordData.ResponseCode = tranData.ResponseReasonCode.ToString();
    tranRecordData.ResponseText = tranData.ResponseReasonText;
    tranRecordData.ProcessingCenterId = this.SelectedProcessingCenter;
    tranRecordData.TransactionDate = new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(tranData.SubmitTime, LocaleInfo.GetTimeZone()));
    tranRecordData.ValidateDoc = false;
    tranRecordData.TranStatus = CCTranStatusCode.GetCode(V2Converter.ConvertTranStatus(tranData.TranStatus));
    string ccvCode = CVVVerificationStatusCode.GetCCVCode(V2Converter.ConvertCardVerificationStatus(tranData.CcvVerificationStatus));
    tranRecordData.CvvVerificationCode = ccvCode;
    tranRecordData.TranUID = tranData.TranUID;
    tranRecordData.CardType = V2Converter.ConvertCardType(tranData.CardTypeCode);
    tranRecordData.ProcCenterCardTypeCode = tranData.CardType;
    tranRecordData.Subtotal = tranData.SubtotalAmount;
    tranRecordData.Tax = tranData.Tax;
    tranRecordData.Level3Support = tranData.Level3Support;
    tranRecordData.LastDigits = !string.IsNullOrEmpty(tranData.CardNumber) ? tranData.CardNumber.Substring(tranData.CardNumber.Length - 4) : string.Empty;
    if (tranData != null && tranData.TranType.HasValue)
      tranRecordData.TranType = CCTranTypeCode.GetTypeCode(V2Converter.ConvertTranType(tranData.TranType.Value));
    return tranRecordData;
  }

  protected void ClearTransactionCaches()
  {
    this.PaymentTransaction.Cache.Clear();
    this.PaymentTransaction.Cache.ClearQueryCache();
    this.PaymentTransaction.View.Clear();
    this.ExternalTransaction.Cache.Clear();
    this.ExternalTransaction.Cache.ClearQueryCache();
    this.ExternalTransaction.View.Clear();
  }

  protected void SetPmInstanceId(int? pmInstanceId)
  {
    Payment current = this.PaymentDoc.Current;
    int? cashAccountId = current.CashAccountID;
    current.PMInstanceID = pmInstanceId;
    Payment payment = this.PaymentDoc.Update(current);
    payment.CashAccountID = cashAccountId;
    this.PaymentDoc.Update(payment);
  }

  protected virtual void MapViews(TGraph graph)
  {
  }

  protected virtual void BeforeAuthorizePayment(TPrimary doc)
  {
  }

  protected virtual void BeforeCapturePayment(TPrimary doc)
  {
  }

  protected virtual void BeforeVoidPayment(TPrimary doc)
  {
  }

  protected virtual void BeforeCreditPayment(TPrimary doc)
  {
  }

  protected virtual void BeforeCaptureOnlyPayment(TPrimary doc)
  {
  }

  protected virtual void SetSyncLock(TPrimary doc)
  {
  }

  protected virtual void RemoveSyncLock(TPrimary doc)
  {
  }

  protected virtual bool LockExists(TPrimary doc) => false;

  protected virtual AfterProcessingManager GetAfterProcessingManager()
  {
    return (AfterProcessingManager) null;
  }

  protected virtual AfterProcessingManager GetAfterProcessingManager(TGraph graph)
  {
    return (AfterProcessingManager) null;
  }

  protected virtual void RowSelected(PX.Data.Events.RowSelected<TPrimary> e)
  {
  }

  protected virtual void RowSelected(PX.Data.Events.RowSelected<PaymentTransactionDetail> e)
  {
    e.Cache.AllowInsert = false;
    e.Cache.AllowUpdate = false;
    e.Cache.AllowDelete = false;
    PaymentTransactionDetail row = e?.Row;
    if (row == null)
      return;
    string docType = this.PaymentDoc.Current?.DocType;
    IEnumerable<ExternalTransactionDetail> source = this.ExternalTransaction.Select().RowCast<ExternalTransactionDetail>();
    IEnumerable<PaymentTransactionDetail> transactionDetails = (IEnumerable<PaymentTransactionDetail>) null;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    int? tranNbr1;
    if (row.TranStatus == "HFR" && source.Where<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (ii =>
    {
      int? transactionId1 = ii.TransactionID;
      int? transactionId2 = row.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue && ii.Active.GetValueOrDefault() && !ExternalTranHelper.IsExpired((IExternalTransaction) ii);
    })).Any<ExternalTransactionDetail>())
    {
      transactionDetails = this.PaymentTransaction.Select().RowCast<PaymentTransactionDetail>();
      PaymentTransactionDetail lastSuccessfulTran = this.GetLastSuccessfulTran(transactionDetails, row.TransactionID);
      if (lastSuccessfulTran != null)
      {
        int? tranNbr2 = lastSuccessfulTran.TranNbr;
        tranNbr1 = row.TranNbr;
        if (tranNbr2.GetValueOrDefault() == tranNbr1.GetValueOrDefault() & tranNbr2.HasValue == tranNbr1.HasValue)
          propertyException = new PXSetPropertyException(this.TranHeldwarnMsg, PXErrorLevel.RowWarning);
      }
    }
    int? nullable;
    if (source.Where<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (ii => ii.Active.GetValueOrDefault() && ii.DocType == docType && !ExternalTranHelper.IsExpired((IExternalTransaction) ii))).Count<ExternalTransactionDetail>() > 1 && source.Where<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (ii =>
    {
      int? transactionId3 = ii.TransactionID;
      int? transactionId4 = row.TransactionID;
      return transactionId3.GetValueOrDefault() == transactionId4.GetValueOrDefault() & transactionId3.HasValue == transactionId4.HasValue && ii.Active.GetValueOrDefault() && !ExternalTranHelper.IsExpired((IExternalTransaction) ii);
    })).Any<ExternalTransactionDetail>())
    {
      if (transactionDetails == null)
        transactionDetails = this.PaymentTransaction.Select().RowCast<PaymentTransactionDetail>();
      PaymentTransactionDetail lastSuccessfulTran = this.GetLastSuccessfulTran(transactionDetails, row.TransactionID);
      if (lastSuccessfulTran != null)
      {
        tranNbr1 = lastSuccessfulTran.TranNbr;
        nullable = row.TranNbr;
        if (tranNbr1.GetValueOrDefault() == nullable.GetValueOrDefault() & tranNbr1.HasValue == nullable.HasValue)
          propertyException = new PXSetPropertyException("Multiple active credit card transactions have been recorded for this payment. Use the processing center site to void or refund the duplicates.", PXErrorLevel.RowWarning);
      }
    }
    ExternalTransactionDetail transactionDetail = source.FirstOrDefault<ExternalTransactionDetail>((Func<ExternalTransactionDetail, bool>) (i =>
    {
      int? transactionId5 = i.TransactionID;
      int? transactionId6 = row.TransactionID;
      return transactionId5.GetValueOrDefault() == transactionId6.GetValueOrDefault() & transactionId5.HasValue == transactionId6.HasValue;
    }));
    if (transactionDetail != null && (transactionDetail.SyncStatus == "E" || transactionDetail.SyncStatus == "W") && transactionDetail.SyncMessage != null)
    {
      if (transactionDetails == null)
        transactionDetails = this.PaymentTransaction.Select().RowCast<PaymentTransactionDetail>();
      nullable = transactionDetails.Where<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
      {
        int? transactionId7 = i.TransactionID;
        int? transactionId8 = row.TransactionID;
        return transactionId7.GetValueOrDefault() == transactionId8.GetValueOrDefault() & transactionId7.HasValue == transactionId8.HasValue;
      })).Max<PaymentTransactionDetail>((Func<PaymentTransactionDetail, int?>) (i => i.TranNbr));
      tranNbr1 = row.TranNbr;
      if (nullable.GetValueOrDefault() == tranNbr1.GetValueOrDefault() & nullable.HasValue == tranNbr1.HasValue)
        propertyException = new PXSetPropertyException(transactionDetail.SyncMessage, PXErrorLevel.RowWarning);
    }
    if (propertyException == null)
      return;
    this.PaymentTransaction.Cache.RaiseExceptionHandling<CCProcTran.tranNbr>((object) row, (object) row.TranNbr, (Exception) propertyException);
  }

  private PaymentTransactionDetail GetLastSuccessfulTran(
    IEnumerable<PaymentTransactionDetail> storedExtTrans,
    int? extTranId)
  {
    return storedExtTrans.Where<PaymentTransactionDetail>((Func<PaymentTransactionDetail, bool>) (i =>
    {
      int? transactionId = i.TransactionID;
      int? nullable = extTranId;
      if (!(transactionId.GetValueOrDefault() == nullable.GetValueOrDefault() & transactionId.HasValue == nullable.HasValue))
        return false;
      return i.TranStatus == "APR" || i.TranStatus == "HFR";
    })).FirstOrDefault<PaymentTransactionDetail>();
  }

  protected virtual TGraph GetProcessingGraph() => PXGraph.CreateInstance<TGraph>();

  protected virtual TPrimary SetCurrentDocument(TGraph graph, TPrimary doc)
  {
    throw new NotImplementedException();
  }

  protected virtual PaymentTransactionGraph<TGraph, TPrimary> GetPaymentTransactionExt(TGraph graph)
  {
    throw new NotImplementedException();
  }

  protected virtual void RowPersisting(PX.Data.Events.RowPersisting<TPrimary> e)
  {
  }

  protected virtual void RowUpdating(PX.Data.Events.RowUpdating<TPrimary> e)
  {
  }

  protected abstract PaymentTransactionGraph<TGraph, TPrimary>.ExternalTransactionDetailMapping GetExternalTransactionMapping();

  protected abstract PaymentTransactionGraph<TGraph, TPrimary>.PaymentTransactionDetailMapping GetPaymentTransactionMapping();

  protected abstract PaymentTransactionGraph<TGraph, TPrimary>.PaymentMapping GetPaymentMapping();

  public static void CheckForHeldForReviewStatusAfterProc(
    PXGraph graph,
    IBqlTable aTable,
    CCTranType procTran,
    bool success)
  {
    ICCPayment ccPayment = aTable as ICCPayment;
    if (!(ccPayment != null & success))
      return;
    PXResultset<PX.Objects.AR.ExternalTransaction> resultSet = new PXSelect<PX.Objects.AR.ExternalTransaction, Where<PX.Objects.AR.ExternalTransaction.docType, Equal<Required<PX.Objects.AR.ExternalTransaction.docType>>, And<PX.Objects.AR.ExternalTransaction.refNbr, Equal<Required<PX.Objects.AR.ExternalTransaction.refNbr>>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>>(graph).Select((object) ccPayment.DocType, (object) ccPayment.RefNbr);
    if (ExternalTranHelper.GetActiveTransactionState(graph, (IEnumerable<IExternalTransaction>) resultSet.RowCast<PX.Objects.AR.ExternalTransaction>()).IsOpenForReview)
      throw new PXSetPropertyException("The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction.", PXErrorLevel.RowWarning);
  }

  public static void ReleaseARDocument(IBqlTable aTable)
  {
    ARRegister arRegister = (ARRegister) aTable;
    using (new PXTimeStampScope((byte[]) null))
    {
      if (arRegister.Released.GetValueOrDefault())
        return;
      ARDocumentRelease.ReleaseDoc(new List<ARRegister>(1)
      {
        arRegister
      }, false);
    }
  }

  protected bool IsActualFinPeriodClosedForBranch(int? branchId)
  {
    return !(this.GetFinPeriod(branchId)?.Status == "Open");
  }

  protected bool IsFinPeriodValid(int? branchId, bool? restrictAccessToClosedPeriods)
  {
    FinPeriod finPeriod = this.GetFinPeriod(branchId);
    if (finPeriod != null)
    {
      switch (finPeriod.Status)
      {
        case "Open":
          return true;
        case "Closed":
          bool? nullable = restrictAccessToClosedPeriods;
          bool flag = false;
          return nullable.GetValueOrDefault() == flag & nullable.HasValue;
        case "Inactive":
        case "Locked":
        case null:
          return false;
      }
    }
    return false;
  }

  private FinPeriod GetFinPeriod(int? branchId)
  {
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(branchId);
    return branch != null ? this.FinPeriodRepository.FindFinPeriodByDate(new System.DateTime?(System.DateTime.Today), branch.Organization.OrganizationID) : (FinPeriod) null;
  }

  internal class LongOperationWarning
  {
    public string FieldName { get; }

    public PXSetPropertyException Exception { get; }

    public LongOperationWarning(string fieldName, PXSetPropertyException ex)
    {
      this.FieldName = fieldName;
      this.Exception = ex;
    }
  }

  protected class PaymentMapping : IBqlMapping
  {
    public System.Type PMInstanceID = typeof (Payment.pMInstanceID);
    public System.Type CashAccountID = typeof (Payment.cashAccountID);
    public System.Type ProcessingCenterID = typeof (Payment.processingCenterID);
    public System.Type CuryDocBal = typeof (Payment.curyDocBal);
    public System.Type CuryID = typeof (Payment.curyID);
    public System.Type DocType = typeof (Payment.docType);
    public System.Type RefNbr = typeof (Payment.refNbr);
    public System.Type OrigDocType = typeof (Payment.origDocType);
    public System.Type OrigRefNbr = typeof (Payment.origRefNbr);
    public System.Type RefTranExtNbr = typeof (Payment.refTranExtNbr);
    public System.Type Released = typeof (Payment.released);
    public System.Type SaveCard = typeof (Payment.saveCard);
    public System.Type CCTransactionRefund = typeof (Payment.cCTransactionRefund);
    public System.Type CCPaymentStateDescr = typeof (Payment.cCPaymentStateDescr);
    public System.Type CCActualExternalTransactionID = typeof (Payment.cCActualExternalTransactionID);

    public System.Type Table { get; private set; }

    public System.Type Extension => typeof (Payment);

    public PaymentMapping(System.Type table) => this.Table = table;
  }

  protected class PaymentTransactionDetailMapping : IBqlMapping
  {
    public System.Type TranNbr = typeof (PaymentTransactionDetail.tranNbr);
    public System.Type TransactionID = typeof (PaymentTransactionDetail.transactionID);
    public System.Type PMInstanceID = typeof (PaymentTransactionDetail.pMInstanceID);
    public System.Type ProcessingCenterID = typeof (PaymentTransactionDetail.processingCenterID);
    public System.Type DocType = typeof (PaymentTransactionDetail.docType);
    public System.Type OrigDocType = typeof (PaymentTransactionDetail.origDocType);
    public System.Type OrigRefNbr = typeof (PaymentTransactionDetail.origRefNbr);
    public System.Type RefNbr = typeof (PaymentTransactionDetail.refNbr);
    public System.Type ExpirationDate = typeof (PaymentTransactionDetail.expirationDate);
    public System.Type ProcStatus = typeof (PaymentTransactionDetail.procStatus);
    public System.Type TranStatus = typeof (PaymentTransactionDetail.tranStatus);
    public System.Type TranType = typeof (PaymentTransactionDetail.tranType);
    public System.Type PCTranNumber = typeof (PaymentTransactionDetail.pCTranNumber);
    public System.Type AuthNumber = typeof (PaymentTransactionDetail.authNumber);
    public System.Type PCResponseReasonText = typeof (PaymentTransactionDetail.pCResponseReasonText);
    public System.Type Amount = typeof (PaymentTransactionDetail.amount);
    public System.Type Imported = typeof (PaymentTransactionDetail.imported);

    public System.Type Extension => typeof (PaymentTransactionDetail);

    public System.Type Table { get; private set; }

    public PaymentTransactionDetailMapping(System.Type table) => this.Table = table;
  }

  protected class ExternalTransactionDetailMapping : IBqlMapping
  {
    public System.Type TransactionID = typeof (ExternalTransactionDetail.transactionID);
    public System.Type PMInstanceID = typeof (ExternalTransactionDetail.pMInstanceID);
    public System.Type DocType = typeof (ExternalTransactionDetail.docType);
    public System.Type RefNbr = typeof (ExternalTransactionDetail.refNbr);
    public System.Type OrigDocType = typeof (ExternalTransactionDetail.origDocType);
    public System.Type OrigRefNbr = typeof (ExternalTransactionDetail.origRefNbr);
    public System.Type VoidDocType = typeof (ExternalTransactionDetail.voidDocType);
    public System.Type VoidRefNbr = typeof (ExternalTransactionDetail.voidRefNbr);
    public System.Type TranNumber = typeof (ExternalTransactionDetail.tranNumber);
    public System.Type AuthNumber = typeof (ExternalTransactionDetail.authNumber);
    public System.Type Amount = typeof (ExternalTransactionDetail.amount);
    public System.Type ProcStatus = typeof (ExternalTransactionDetail.procStatus);
    public System.Type LastActivityDate = typeof (ExternalTransactionDetail.lastActivityDate);
    public System.Type Direction = typeof (ExternalTransactionDetail.direction);
    public System.Type Active = typeof (ExternalTransactionDetail.active);
    public System.Type Completed = typeof (ExternalTransactionDetail.completed);
    public System.Type ParentTranID = typeof (ExternalTransactionDetail.parentTranID);
    public System.Type ExpirationDate = typeof (ExternalTransactionDetail.expirationDate);
    public System.Type CVVVerification = typeof (ExternalTransactionDetail.cVVVerification);
    public System.Type NeedSync = typeof (ExternalTransactionDetail.needSync);
    public System.Type SaveProfile = typeof (ExternalTransactionDetail.saveProfile);
    public System.Type SyncStatus = typeof (ExternalTransactionDetail.syncStatus);
    public System.Type SyncMessage = typeof (ExternalTransactionDetail.syncMessage);
    public System.Type NoteID = typeof (ExternalTransactionDetail.noteID);

    public System.Type Extension => typeof (ExternalTransactionDetail);

    public System.Type Table { get; private set; }

    public ExternalTransactionDetailMapping(System.Type table) => this.Table = table;
  }

  protected class InputPaymentInfoMapping : IBqlMapping
  {
    public System.Type AuthNumber = typeof (InputPaymentInfo.authNumber);
    public System.Type PCTranNumber = typeof (InputPaymentInfo.pCTranNumber);

    public System.Type Table { get; private set; }

    public System.Type Extension => typeof (InputPaymentInfo);

    public InputPaymentInfoMapping(System.Type table) => this.Table = table;
  }
}
