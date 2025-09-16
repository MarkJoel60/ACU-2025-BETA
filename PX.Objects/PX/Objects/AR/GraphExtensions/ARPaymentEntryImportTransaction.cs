// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARPaymentEntryImportTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR.GraphExtensions;

public class ARPaymentEntryImportTransaction : PXGraphExtension<
#nullable disable
ARPaymentEntry>
{
  public PXFilter<InputCCTransaction> apiInputCCTran;
  public PXAction<PX.Objects.AR.ARPayment> voidCardPayment;
  public PXAction<PX.Objects.AR.ARPayment> cardOperation;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
  }

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable VoidCardPayment(PXAdapter adapter)
  {
    List<PX.Objects.AR.ARPayment> arPaymentList = new List<PX.Objects.AR.ARPayment>();
    if (((PXGraph) this.Base).IsContractBasedAPI)
    {
      foreach (PX.Objects.AR.ARPayment doc in adapter.Get<PX.Objects.AR.ARPayment>())
      {
        InputCCTransaction current = ((PXSelectBase<InputCCTransaction>) this.apiInputCCTran).Current;
        arPaymentList.Add(this.RecordTranAndVoidPayment(doc, current));
      }
    }
    else
      arPaymentList.AddRange(adapter.Get<PX.Objects.AR.ARPayment>());
    return (IEnumerable) arPaymentList;
  }

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable CardOperation(PXAdapter adapter)
  {
    List<PX.Objects.AR.ARPayment> arPaymentList = new List<PX.Objects.AR.ARPayment>();
    if (((PXGraph) this.Base).IsContractBasedAPI)
    {
      foreach (PX.Objects.AR.ARPayment doc in adapter.Get<PX.Objects.AR.ARPayment>())
      {
        InputCCTransaction current = ((PXSelectBase<InputCCTransaction>) this.apiInputCCTran).Current;
        arPaymentList.Add(this.RecordCardOperation(doc, current));
      }
    }
    else
      arPaymentList.AddRange(adapter.Get<PX.Objects.AR.ARPayment>());
    return (IEnumerable) arPaymentList;
  }

  protected virtual void RowSelected(PX.Data.Events.RowSelected<PX.Objects.AR.ARPayment> e)
  {
    ((PXSelectBase) this.apiInputCCTran).Cache.AllowInsert = true;
    ((PXSelectBase) this.apiInputCCTran).Cache.AllowUpdate = true;
  }

  protected virtual void InputCCTransactionRowInserted(PX.Data.Events.RowInserted<InputCCTransaction> e)
  {
    this.InsertImportedCreditCardTransaction(e.Row);
  }

  protected virtual void InputCCTransactionRowUpdated(PX.Data.Events.RowUpdated<InputCCTransaction> e)
  {
    this.InsertImportedCreditCardTransaction(e.Row);
  }

  protected virtual PX.Objects.AR.ARPayment RecordTranAndVoidPayment(
    PX.Objects.AR.ARPayment doc,
    InputCCTransaction inputTran)
  {
    PX.Objects.AR.ARPayment arPayment = doc;
    if (doc == null || inputTran == null)
      return arPayment;
    this.TrySetTranNbr(doc, inputTran);
    this.ValidateBeforeVoiding(doc, inputTran);
    IExternalTransaction extTranDetail = this.GetExtTranDetail(inputTran);
    if (extTranDetail != null)
    {
      ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) this.Base, extTranDetail);
      if (transactionState.IsVoided || transactionState.IsRefunded)
      {
        this.Base.VoidCheck(ARPaymentEntry.CreateAdapterWithDummyView(this.Base, doc));
        return doc;
      }
    }
    CCTranType tranTypeByStrCode = TranTypeList.GetTranTypeByStrCode(inputTran.TranType);
    if (tranTypeByStrCode == CCTranType.Unknown)
      inputTran.NeedValidation = new bool?(true);
    if (!inputTran.TranDate.HasValue)
      inputTran.TranDate = new DateTime?(PXTimeZoneInfo.Now);
    int? nullable = new int?();
    ExternalTransactionState transactionState1 = this.GetActiveTransactionState();
    if (tranTypeByStrCode == CCTranType.Void && inputTran.OrigPCTranNumber != null)
    {
      bool? needValidation = inputTran.NeedValidation;
      bool flag = false;
      if (needValidation.GetValueOrDefault() == flag & needValidation.HasValue)
        nullable = (int?) transactionState1.ExternalTransaction?.TransactionID;
    }
    if (tranTypeByStrCode != CCTranType.Credit)
      inputTran.OrigPCTranNumber = (string) null;
    TranRecordData recordData = this.FormatRecordData(inputTran);
    recordData.RefInnerTranId = nullable;
    return this.CreateVoidDocWithTran(doc, tranTypeByStrCode, recordData);
  }

  protected virtual PX.Objects.AR.ARPayment RecordCardOperation(
    PX.Objects.AR.ARPayment doc,
    InputCCTransaction inputTran)
  {
    PX.Objects.AR.ARPayment arPayment = doc;
    if (doc == null || inputTran == null)
      return arPayment;
    this.TrySetTranNbr(doc, inputTran);
    this.UpdateDocBeforeApiRecording(doc, inputTran);
    this.ValidateBeforeRecordingCardOperation(doc, inputTran);
    CCTranType tranTypeByStrCode = TranTypeList.GetTranTypeByStrCode(inputTran.TranType);
    if (!inputTran.TranDate.HasValue)
      inputTran.TranDate = new DateTime?(PXTimeZoneInfo.Now);
    if (string.IsNullOrEmpty(inputTran.PCTranNumber))
      inputTran.NeedValidation = new bool?(true);
    int? nullable = new int?();
    if (inputTran.OrigPCTranNumber != null)
    {
      bool? needValidation = inputTran.NeedValidation;
      bool flag = false;
      if (needValidation.GetValueOrDefault() == flag & needValidation.HasValue)
        nullable = (int?) this.GetActiveTransactionState().ExternalTransaction?.TransactionID;
    }
    inputTran.OrigPCTranNumber = (string) null;
    TranRecordData tranRecord = this.FormatRecordData(inputTran);
    tranRecord.RefInnerTranId = nullable;
    tranRecord.Amount = inputTran.Amount;
    if (doc.DocType == "PMT" || doc.DocType == "PPM")
    {
      CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
      ARPaymentAfterProcessingManager processingManager = this.GetAfterProcessingManager();
      processingManager.ReleaseDoc = true;
      ccPaymentEntry.AfterProcessingManager = (AfterProcessingManager) processingManager;
      GenericExternalTransactionAdapter<PX.Objects.AR.ExternalTransaction> paymentTransaction = new GenericExternalTransactionAdapter<PX.Objects.AR.ExternalTransaction>((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran);
      if (tranTypeByStrCode == CCTranType.PriorAuthorizedCapture)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          ccPaymentEntry.RecordPriorAuthCapture((ICCPayment) doc, tranRecord, (IExternalTransactionAdapter) paymentTransaction);
          transactionScope.Complete();
        }
      }
    }
    return doc;
  }

  protected virtual PX.Objects.AR.ARPayment CreateVoidDocWithTran(
    PX.Objects.AR.ARPayment doc,
    CCTranType tran,
    TranRecordData recordData)
  {
    CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
    ARPaymentAfterProcessingManager processingManager = this.GetAfterProcessingManager();
    processingManager.ReleaseDoc = true;
    processingManager.Graph = this.Base;
    ccPaymentEntry.AfterProcessingManager = (AfterProcessingManager) processingManager;
    if (doc.DocType == "PMT" || doc.DocType == "PPM")
    {
      ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.HoldEntry = new bool?(false);
      GenericExternalTransactionAdapter<PX.Objects.AR.ExternalTransaction> paymentTransaction = new GenericExternalTransactionAdapter<PX.Objects.AR.ExternalTransaction>((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran);
      if (tran == CCTranType.Void)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXAdapter adapterWithDummyView = ARPaymentEntry.CreateAdapterWithDummyView(this.Base, doc);
          bool? nullable = doc.Released;
          bool flag1 = false;
          if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
          {
            nullable = doc.Voided;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
            {
              ccPaymentEntry.RecordVoid((ICCPayment) doc, recordData, (IExternalTransactionAdapter) paymentTransaction);
              GraphHelper.RowCast<PX.Objects.AR.ARPayment>(this.Base.VoidCheck(adapterWithDummyView)).FirstOrDefault<PX.Objects.AR.ARPayment>();
              goto label_8;
            }
          }
          PX.Objects.AR.ARPayment doc1 = GraphHelper.RowCast<PX.Objects.AR.ARPayment>(this.Base.VoidCheck(adapterWithDummyView)).FirstOrDefault<PX.Objects.AR.ARPayment>();
          if (doc1 != null && doc1.DocType == "RPM")
          {
            ((PXAction) this.Base.Save).Press();
            recordData.AllowFillVoidRef = true;
            recordData.Amount = doc.CuryDocBal;
            ccPaymentEntry.RecordVoid((ICCPayment) doc1, recordData, (IExternalTransactionAdapter) paymentTransaction);
          }
label_8:
          transactionScope.Complete();
        }
      }
      if (tran == CCTranType.Credit)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          this.Base.VoidCheck(ARPaymentEntry.CreateAdapterWithDummyView(this.Base, doc));
          ((PXAction) this.Base.Save).Press();
          ccPaymentEntry.RecordCCCredit((ICCPayment) ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current, recordData, (IExternalTransactionAdapter) paymentTransaction);
          if (this.NeedRelease(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current))
            PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.ReleaseARDocument((IBqlTable) ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current);
          transactionScope.Complete();
        }
      }
      if (tran == CCTranType.Unknown)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          recordData.KeepNewTranDeactivated = true;
          ccPaymentEntry.RecordUnknown((ICCPayment) doc, recordData, (IExternalTransactionAdapter) paymentTransaction);
          transactionScope.Complete();
        }
      }
    }
    return doc;
  }

  protected virtual TranRecordData FormatRecordData(InputCCTransaction inputData)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current;
    TranRecordData tranRecordData = new TranRecordData()
    {
      ExternalTranId = inputData.PCTranNumber,
      ExternalTranApiId = inputData.PCTranApiNumber,
      CommerceTranNumber = inputData.CommerceTranNumber,
      AuthCode = inputData.AuthNumber,
      ResponseText = "Imported External Transaction",
      Imported = true,
      CreateProfile = current.SaveCard.GetValueOrDefault(),
      NeedSync = inputData.NeedValidation.GetValueOrDefault(),
      TransactionDate = inputData.TranDate,
      ProcessingCenterId = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current.ProcessingCenterID,
      ExtProfileId = inputData.ExtProfileId
    };
    tranRecordData.ResponseText = "Imported External Transaction";
    tranRecordData.TranStatus = "APR";
    tranRecordData.RefExternalTranId = inputData.OrigPCTranNumber;
    tranRecordData.CardType = CardType.GetCardTypeEnumByCode(inputData.CardType);
    tranRecordData.ProcCenterCardTypeCode = inputData.CardType;
    tranRecordData.LastDigits = string.Empty;
    return tranRecordData;
  }

  protected virtual void SetSyncLock(PX.Objects.AR.ARPayment doc, InputCCTransaction inputData)
  {
    if (doc == null || inputData == null || ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARPayment)].GetStatus((object) doc) != 2 || inputData == null || inputData.TranType == null)
      return;
    int? pmInstanceId = doc.PMInstanceID;
    CCTranType tranTypeByStrCode = TranTypeList.GetTranTypeByStrCode(inputData.TranType);
    bool? nullable1 = doc.SaveCard;
    if (nullable1.GetValueOrDefault())
    {
      int? nullable2 = pmInstanceId;
      int num = 0;
      if (nullable2.GetValueOrDefault() >= num & nullable2.HasValue)
        goto label_5;
    }
    if (!(doc.DocType == "REF"))
      goto label_6;
label_5:
    doc.SaveCard = new bool?(false);
label_6:
    nullable1 = doc.SaveCard;
    if (nullable1.GetValueOrDefault() || tranTypeByStrCode == CCTranType.Unknown)
      inputData.NeedValidation = new bool?(true);
    nullable1 = inputData.NeedValidation;
    bool flag1 = false;
    if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue && string.IsNullOrEmpty(inputData.PCTranNumber))
      inputData.NeedValidation = new bool?(true);
    nullable1 = inputData.NeedValidation;
    bool flag2 = false;
    if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue && this.CheckNeedValidationForSettledTran(doc, inputData))
      inputData.NeedValidation = new bool?(true);
    nullable1 = doc.SyncLock;
    bool? needValidation = inputData.NeedValidation;
    if (nullable1.GetValueOrDefault() == needValidation.GetValueOrDefault() & nullable1.HasValue == needValidation.HasValue)
      return;
    doc.SyncLock = inputData.NeedValidation;
    doc.SyncLockReason = doc.SyncLock.GetValueOrDefault() ? "V" : (string) null;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Update(doc);
  }

  protected virtual void ProcessDocWithTranOneScope(PX.Objects.AR.ARPayment doc, InputCCTransaction inputData)
  {
    if (((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARPayment)].GetStatus((object) doc) != 2 || !doc.CustomerID.HasValue || ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.Base.ExternalTran).Select(Array.Empty<object>()).Count != 0 || inputData == null || inputData.TranType == null)
      return;
    ARSetup current = ((PXSelectBase<ARSetup>) this.Base.arsetup).Current;
    bool? nullable1;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable1 = current.MigrationMode;
      num = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
      throw new PXException("Migration mode is activated in the Accounts Receivable module.");
    this.UpdateDocBeforeApiRecording(doc, inputData);
    Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> transactionWithPayment = this.GetRefExternalTransactionWithPayment(inputData, doc.ProcessingCenterID);
    CCTranType tranTypeByStrCode = TranTypeList.GetTranTypeByStrCode(inputData.TranType);
    if (!inputData.TranDate.HasValue)
      inputData.TranDate = new DateTime?(PXTimeZoneInfo.Now);
    this.ValidateRecordedInfoBeforeDocCreation(doc, inputData, transactionWithPayment);
    int? nullable2 = new int?();
    if (inputData.OrigPCTranNumber != null && transactionWithPayment != null)
    {
      nullable1 = inputData.NeedValidation;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && tranTypeByStrCode == CCTranType.Void)
        nullable2 = (int?) transactionWithPayment.Item1?.TransactionID;
    }
    if (tranTypeByStrCode != CCTranType.Credit)
      inputData.OrigPCTranNumber = (string) null;
    TranRecordData tranRecord = this.FormatRecordData(inputData);
    tranRecord.RefInnerTranId = nullable2;
    tranRecord.NewDoc = true;
    if (tranTypeByStrCode == CCTranType.AuthorizeOnly)
      tranRecord.ExpirationDate = inputData.ExpirationDate;
    CCPaymentEntry ccPaymentEntry = this.GetCCPaymentEntry((PXGraph) this.Base);
    ccPaymentEntry.NeedPersistAfterRecord = false;
    ARPaymentAfterProcessingManager processingManager = this.GetAfterProcessingManager();
    processingManager.Graph = this.Base;
    ccPaymentEntry.AfterProcessingManager = (AfterProcessingManager) processingManager;
    if (doc.DocType == "PMT" || doc.DocType == "PPM")
    {
      switch (tranTypeByStrCode)
      {
        case CCTranType.AuthorizeAndCapture:
          ccPaymentEntry.RecordAuthCapture((ICCPayment) doc, tranRecord);
          break;
        case CCTranType.AuthorizeOnly:
          ccPaymentEntry.RecordAuthorization((ICCPayment) doc, tranRecord);
          break;
        case CCTranType.PriorAuthorizedCapture:
          ccPaymentEntry.RecordPriorAuthCapture((ICCPayment) doc, tranRecord);
          break;
        case CCTranType.Unknown:
          ccPaymentEntry.RecordUnknown((ICCPayment) doc, tranRecord);
          break;
      }
    }
    if (!(doc.DocType == "REF"))
      return;
    switch (tranTypeByStrCode)
    {
      case CCTranType.Credit:
        ccPaymentEntry.RecordCredit((ICCPayment) doc, tranRecord);
        break;
      case CCTranType.Void:
      case CCTranType.Unknown:
        tranRecord.AllowFillVoidRef = true;
        if (transactionWithPayment != null)
          this.SetVoidDocTypeVoidRefNbrDefaultByDoc();
        if (tranTypeByStrCode == CCTranType.Void)
        {
          ccPaymentEntry.RecordVoid((ICCPayment) doc, tranRecord);
          break;
        }
        ccPaymentEntry.RecordUnknown((ICCPayment) doc, tranRecord);
        break;
    }
  }

  protected virtual void ValidateRecordedInfoBeforeDocCreation(
    PX.Objects.AR.ARPayment doc,
    InputCCTransaction info,
    Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> refExtTran)
  {
    this.ValidateDocBeforeApiRecording(doc, info);
    this.CommonRecordValidation(doc, info);
    CCTranType tranTypeByStrCode = TranTypeList.GetTranTypeByStrCode(info.TranType);
    string centerTranNumber = this.GetNonEmptyProcCenterTranNumber(info);
    if (info.NeedValidation.GetValueOrDefault() && (tranTypeByStrCode == CCTranType.Void || tranTypeByStrCode == CCTranType.Credit))
      throw new PXException("The {0} transaction has not been imported. Set the Need Validation parameter of the API call to False and re-import the transaction.", new object[1]
      {
        (object) centerTranNumber
      });
    if (doc.DocType == "REF" && tranTypeByStrCode != CCTranType.Credit && tranTypeByStrCode != CCTranType.Void && tranTypeByStrCode != CCTranType.Unknown)
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) centerTranNumber
      });
    if ((doc.DocType == "PMT" || doc.DocType == "PPM") && (tranTypeByStrCode == CCTranType.Credit || tranTypeByStrCode == CCTranType.Void))
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) centerTranNumber
      });
    if (refExtTran != null)
    {
      PX.Objects.AR.ARPayment doc1 = refExtTran.Item2;
      if (this.GetVoidedDocForOrigPayment(doc1) != null)
      {
        string documentName = TranValidationHelper.GetDocumentName(doc1.DocType);
        throw new PXException("The {0} transaction has not been imported. The transaction is recorded for the {1} {2} that has already been voided.", new object[3]
        {
          (object) centerTranNumber,
          (object) doc1.RefNbr,
          (object) documentName
        });
      }
    }
    if (tranTypeByStrCode == CCTranType.AuthorizeOnly)
    {
      DateTime? expirationDate = info.ExpirationDate;
      if (expirationDate.HasValue)
      {
        expirationDate = info.ExpirationDate;
        DateTime? tranDate = info.TranDate;
        if ((expirationDate.HasValue & tranDate.HasValue ? (expirationDate.GetValueOrDefault() <= tranDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          throw new PXException("The date specified in the ExpirationDate parameter should be later than the date specified in the TranDate parameter.");
      }
    }
    if (!(doc.DocType == "REF") || tranTypeByStrCode != CCTranType.Void && tranTypeByStrCode != CCTranType.Unknown)
      return;
    if (refExtTran == null)
    {
      if (tranTypeByStrCode == CCTranType.Void)
      {
        bool? needValidation = info.NeedValidation;
        bool flag = false;
        if (needValidation.GetValueOrDefault() == flag & needValidation.HasValue)
          throw new PXException("There is no successful transaction to void.");
      }
    }
    else
    {
      PX.Objects.AR.ExternalTransaction externalTransaction = refExtTran.Item1;
      PX.Objects.AR.ARPayment storedPmt = refExtTran.Item2;
      TranValidationHelper.CheckNewAndStoredPayment(doc, storedPmt, (IExternalTransaction) externalTransaction);
      if (tranTypeByStrCode == CCTranType.Void)
      {
        Decimal? curyDocBal = doc.CuryDocBal;
        Decimal? amount = externalTransaction.Amount;
        if (!(curyDocBal.GetValueOrDefault() == amount.GetValueOrDefault() & curyDocBal.HasValue == amount.HasValue))
          throw new PXException("The {0} transaction amount is not the same as the payment amount.", new object[1]
          {
            (object) centerTranNumber
          });
      }
      ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) this.Base, (IExternalTransaction) externalTransaction);
      if (transactionState.IsRefunded)
        throw new PXException("The {0} transaction is refunded.", new object[1]
        {
          (object) externalTransaction.TranNumber
        });
      if (transactionState.IsVoided)
      {
        string documentName = TranValidationHelper.GetDocumentName(storedPmt.DocType);
        throw new PXException("The {0} transaction has not been imported. The transaction is recorded for the {1} {2} that has already been voided.", new object[3]
        {
          (object) externalTransaction.TranNumber,
          (object) storedPmt.RefNbr,
          (object) documentName
        });
      }
    }
    foreach (PX.Objects.AR.ARRegister payment in GraphHelper.RowCast<ARRegisterAlias>((IEnumerable) ((PXSelectBase<PX.Objects.AR.ARAdjust>) this.Base.Adjustments).Select(Array.Empty<object>())))
      ARReleaseProcess.EnsureNoUnreleasedVoidPaymentExists((PXGraph) this.Base, payment, "refunded");
  }

  protected virtual void CommonRecordValidation(PX.Objects.AR.ARPayment doc, InputCCTransaction info)
  {
    if (string.IsNullOrEmpty(info.PCTranNumber) && string.IsNullOrEmpty(info.PCTranApiNumber))
      throw new PXException("Both '{0}' and '{1}' cannot be empty at the same time.", new object[2]
      {
        (object) "PCTranNumber",
        (object) "PCTranApiNumber"
      });
    if (string.IsNullOrEmpty(info.TranType))
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "TranType"
      });
    if (((IEnumerable<Tuple<string, string>>) TranTypeList.GetCommonInputTypes()).FirstOrDefault<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (i => i.Item1 == info.TranType)) == null)
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) this.GetNonEmptyProcCenterTranNumber(info)
      });
  }

  protected virtual bool CheckNeedValidationForSettledTran(
    PX.Objects.AR.ARPayment doc,
    InputCCTransaction inputData)
  {
    bool flag = false;
    if (inputData.TranType == "AUT")
    {
      string procCenterID = (string) null;
      int? pmInstanceId = doc.PMInstanceID;
      int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
      if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue))
      {
        PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this.Base, doc.PMInstanceID);
        if (customerPaymentMethod != null && customerPaymentMethod.CCProcessingCenterID != null)
          procCenterID = customerPaymentMethod.CCProcessingCenterID;
      }
      else
        procCenterID = doc.ProcessingCenterID;
      if (procCenterID != null && this.GetCCBatchTransaction(procCenterID, inputData.PCTranNumber)?.SettlementStatus == "SSC")
        flag = true;
    }
    return flag;
  }

  protected virtual void ValidateBeforeRecordingCardOperation(
    PX.Objects.AR.ARPayment doc,
    InputCCTransaction inputTran)
  {
    string str = doc.DocType + doc.RefNbr;
    if (((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARPayment)].GetStatus((object) doc) == 2)
      throw new PXException("The document cannot be found in the system.");
    this.CommonRecordValidation(doc, inputTran);
    string centerTranNumber = this.GetNonEmptyProcCenterTranNumber(inputTran);
    bool? nullable = inputTran.NeedValidation;
    if (nullable.GetValueOrDefault())
      throw new PXException("The {0} transaction has not been imported. Set the Need Validation parameter of the API call to False and re-import the transaction.", new object[1]
      {
        (object) centerTranNumber
      });
    if (TranTypeList.GetTranTypeByStrCode(inputTran.TranType) != CCTranType.PriorAuthorizedCapture)
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) centerTranNumber
      });
    nullable = doc.Released;
    if (nullable.GetValueOrDefault())
    {
      string documentName = TranValidationHelper.GetDocumentName(doc.DocType);
      throw new PXException("The transaction cannot be imported for the {0} {1}. The {1} has been released.", new object[2]
      {
        (object) doc.RefNbr,
        (object) documentName
      });
    }
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    if (!transactionState.IsPreAuthorized)
      throw new PXException("Transaction must be authorized before it may be captured");
    if (transactionState.IsCaptured)
      throw new PXException("This payment has been captured already.");
    IExternalTransaction externalTransaction = !transactionState.IsRefunded ? transactionState.ExternalTransaction : throw new PXException("This payment has been refunded already.");
    if (externalTransaction != null)
    {
      nullable = inputTran.NeedValidation;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        if (inputTran.OrigPCTranNumber == null && !this.HaveSameTranNumber(inputTran, externalTransaction))
        {
          string transactionTypeName1 = CCProcessingHelper.GetTransactionTypeName(CCTranType.PriorAuthorizedCapture);
          string transactionTypeName2 = CCProcessingHelper.GetTransactionTypeName(CCTranType.AuthorizeOnly);
          throw new PXException("The {0} {1} transaction is not related to the {2} {3} transaction.", new object[4]
          {
            (object) centerTranNumber,
            (object) transactionTypeName1,
            (object) externalTransaction.TranNumber,
            (object) transactionTypeName2
          });
        }
        if (inputTran.OrigPCTranNumber != null && inputTran.OrigPCTranNumber != externalTransaction.TranNumber)
          throw new PXException("The {0} transaction for the {1} document cannot be found in the system.", new object[2]
          {
            (object) inputTran.OrigPCTranNumber,
            (object) str
          });
      }
    }
    if (inputTran.Amount.HasValue)
    {
      Decimal? amount1 = inputTran.Amount;
      Decimal num = 0M;
      if (amount1.GetValueOrDefault() <= num & amount1.HasValue)
        throw new PXException("The amount must be greater than zero.");
      amount1 = inputTran.Amount;
      Decimal? amount2 = externalTransaction.Amount;
      if (amount1.GetValueOrDefault() > amount2.GetValueOrDefault() & amount1.HasValue & amount2.HasValue)
        throw new PXException("The {0} transaction amount is greater than the payment amount.", new object[1]
        {
          (object) centerTranNumber
        });
    }
    this.CheckInputTranDate(externalTransaction, inputTran);
    this.CheckProcCenterSupportTransactionValidation(doc.ProcessingCenterID, inputTran);
  }

  protected virtual void ValidateBeforeVoiding(PX.Objects.AR.ARPayment doc, InputCCTransaction info)
  {
    string str1 = doc.DocType + doc.RefNbr;
    string documentName = TranValidationHelper.GetDocumentName(doc.DocType);
    if (((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARPayment)].GetStatus((object) doc) == 2)
      throw new PXException("The document cannot be found in the system.");
    this.CommonRecordValidation(doc, info);
    string centerTranNumber = this.GetNonEmptyProcCenterTranNumber(info);
    if (this.GetVoidedDocForOrigPayment(doc) != null)
      throw new PXException("The {0} transaction has not been imported. The transaction is recorded for the {1} {2} that has already been voided.", new object[3]
      {
        (object) centerTranNumber,
        (object) doc.RefNbr,
        (object) documentName
      });
    CCTranType tranTypeByStrCode = TranTypeList.GetTranTypeByStrCode(info.TranType);
    if (info.NeedValidation.GetValueOrDefault() && (tranTypeByStrCode == CCTranType.Void || tranTypeByStrCode == CCTranType.Credit))
      throw new PXException("The {0} transaction has not been imported. Set the Need Validation parameter of the API call to False and re-import the transaction.", new object[1]
      {
        (object) centerTranNumber
      });
    ExternalTransactionState transactionState = this.GetActiveTransactionState();
    IExternalTransaction externalTransaction = transactionState.ExternalTransaction;
    if (string.IsNullOrEmpty(info.PCTranNumber) && tranTypeByStrCode != CCTranType.Unknown)
    {
      if (externalTransaction == null)
        throw new PXException("The {0} transaction cannot be imported for the {1} {2}. The {2} does not have a successful transaction.", new object[3]
        {
          (object) centerTranNumber,
          (object) doc.RefNbr,
          (object) documentName
        });
      throw new PXException("The {0} transaction has not been imported. Set the Tran. Type parameter of the API call to UKN and re-import the transaction.", new object[1]
      {
        (object) info.PCTranApiNumber
      });
    }
    if (tranTypeByStrCode == CCTranType.AuthorizeAndCapture || tranTypeByStrCode == CCTranType.AuthorizeOnly || tranTypeByStrCode == CCTranType.CaptureOnly || tranTypeByStrCode == CCTranType.PriorAuthorizedCapture)
      throw new PXException("The {0} transaction has an invalid transaction type.", new object[1]
      {
        (object) centerTranNumber
      });
    bool? released = doc.Released;
    bool flag1 = false;
    if (released.GetValueOrDefault() == flag1 & released.HasValue && (transactionState.IsCaptured || tranTypeByStrCode == CCTranType.Credit))
      throw new PXException("The {0} payment is not released.", new object[1]
      {
        (object) str1
      });
    if (externalTransaction != null && tranTypeByStrCode == CCTranType.Credit && info.OrigPCTranNumber != null && externalTransaction.TranNumber != info.OrigPCTranNumber)
      throw new PXException("The {0} transaction does not refund the transaction with the {1} transaction number.", new object[2]
      {
        (object) info.PCTranNumber,
        (object) externalTransaction.TranNumber
      });
    if (externalTransaction != null && tranTypeByStrCode == CCTranType.Void)
    {
      bool? needValidation = info.NeedValidation;
      bool flag2 = false;
      if (needValidation.GetValueOrDefault() == flag2 & needValidation.HasValue)
      {
        if (info.OrigPCTranNumber == null && !this.HaveSameTranNumber(info, externalTransaction))
        {
          string transactionTypeName = CCProcessingHelper.GetTransactionTypeName(CCTranType.Void);
          string str2 = transactionState.IsPreAuthorized ? CCProcessingHelper.GetTransactionTypeName(CCTranType.AuthorizeOnly) : CCProcessingHelper.GetTransactionTypeName(CCTranType.AuthorizeAndCapture);
          throw new PXException("The {0} {1} transaction is not related to the {2} {3} transaction.", new object[4]
          {
            (object) centerTranNumber,
            (object) transactionTypeName,
            (object) externalTransaction.TranNumber,
            (object) str2
          });
        }
        if (info.OrigPCTranNumber != null && info.OrigPCTranNumber != externalTransaction.TranNumber)
          throw new PXException("The {0} transaction for the {1} document cannot be found in the system.", new object[2]
          {
            (object) info.OrigPCTranNumber,
            (object) str1
          });
      }
    }
    if ((externalTransaction == null || transactionState.IsPreAuthorized) && tranTypeByStrCode == CCTranType.Credit)
      throw new PXException("The {0} refund transaction cannot be imported for the {1} {2}. The {2} does not have a successful transaction to refund.", new object[3]
      {
        (object) centerTranNumber,
        (object) doc.RefNbr,
        (object) documentName
      });
    if (externalTransaction == null && tranTypeByStrCode == CCTranType.Unknown)
      throw new PXException("The {0} transaction cannot be imported for the {1} {2}. The {2} does not have a successful transaction.", new object[3]
      {
        (object) centerTranNumber,
        (object) doc.RefNbr,
        (object) documentName
      });
    if ((!string.IsNullOrEmpty(info.PCTranNumber) ? this.GetExtTrans().Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranNumber == info.PCTranNumber && i.NeedSync.GetValueOrDefault())).FirstOrDefault<IExternalTransaction>() : this.GetExtTrans().Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranApiNumber == info.PCTranApiNumber && i.NeedSync.GetValueOrDefault())).FirstOrDefault<IExternalTransaction>()) != null)
      throw new PXException("The {0} transaction has already been imported for the {1} {2}.", new object[3]
      {
        (object) centerTranNumber,
        (object) doc.RefNbr,
        (object) documentName
      });
    this.CheckInputTranDate(externalTransaction, info);
    this.CheckProcCenterSupportTransactionValidation(doc.ProcessingCenterID, info);
  }

  protected virtual CCPaymentEntry GetCCPaymentEntry(PXGraph graph) => new CCPaymentEntry(graph);

  protected virtual ARPaymentAfterProcessingManager GetAfterProcessingManager()
  {
    return new ARPaymentAfterProcessingManager();
  }

  protected virtual ICCPaymentProcessingRepository GetPaymentRepository()
  {
    return (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) this.Base);
  }

  protected virtual ExternalTransactionState GetActiveTransactionState()
  {
    return ExternalTranHelper.GetActiveTransactionState((PXGraph) this.Base, this.GetExtTrans());
  }

  protected virtual IEnumerable<IExternalTransaction> GetExtTrans()
  {
    ARPaymentEntryImportTransaction importTransaction = this;
    if (importTransaction.Base.ExternalTran != null)
    {
      foreach (IExternalTransaction extTran in GraphHelper.RowCast<PX.Objects.AR.ExternalTransaction>((IEnumerable) ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) importTransaction.Base.ExternalTran).Select(Array.Empty<object>())))
        yield return extTran;
    }
  }

  protected CCProcessingCenter GetProcessingCenterById(string id)
  {
    return PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) id
    }));
  }

  private bool NeedRelease(PX.Objects.AR.ARPayment doc)
  {
    bool? released = doc.Released;
    bool flag = false;
    return released.GetValueOrDefault() == flag & released.HasValue && ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.IntegratedCCProcessing.GetValueOrDefault();
  }

  private void CheckInputTranDate(IExternalTransaction activeTran, InputCCTransaction inputTran)
  {
    if (activeTran == null || !inputTran.TranDate.HasValue)
      return;
    DateTime? lastActivityDate = activeTran.LastActivityDate;
    DateTime? tranDate = inputTran.TranDate;
    if ((lastActivityDate.HasValue & tranDate.HasValue ? (lastActivityDate.GetValueOrDefault() > tranDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ICCPaymentTransaction lastSuccessful = CCProcTranHelper.FindCCLastSuccessfulTran((IEnumerable<ICCPaymentTransaction>) this.GetPaymentRepository().GetCCProcTranByTranID(activeTran.TransactionID));
    if (lastSuccessful == null)
      return;
    CCProcTran ccProcTran = GraphHelper.RowCast<CCProcTran>((IEnumerable) ((PXSelectBase<CCProcTran>) this.Base.ccProcTran).Select(Array.Empty<object>())).FirstOrDefault<CCProcTran>((Func<CCProcTran, bool>) (i =>
    {
      int? tranNbr1 = i.TranNbr;
      int? tranNbr2 = lastSuccessful.TranNbr;
      return tranNbr1.GetValueOrDefault() == tranNbr2.GetValueOrDefault() & tranNbr1.HasValue == tranNbr2.HasValue;
    }));
    if (ccProcTran != null && ccProcTran.EndTime.Value.Date > inputTran.TranDate.Value.Date)
      throw new PXException("The transaction has not been imported. The transaction date specified in the TranDate parameter is earlier than the date of the original transaction.");
  }

  private void InsertImportedCreditCardTransaction(InputCCTransaction inputTransaction)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Current;
    if (current == null || inputTransaction == null)
      return;
    this.SetSyncLock(current, inputTransaction);
    if (!((PXGraph) this.Base).IsContractBasedAPI)
      return;
    this.ProcessDocWithTranOneScope(current, inputTransaction);
  }

  private CCBatchTransaction GetCCBatchTransaction(string procCenterID, string pcTranNumber)
  {
    return ((PXSelectBase<CCBatchTransaction>) new PXSelectJoin<CCBatchTransaction, InnerJoin<CCBatch, On<CCBatch.batchID, Equal<CCBatchTransaction.batchID>>>, Where<CCBatch.processingCenterID, Equal<Required<CCBatch.processingCenterID>>, And<CCBatchTransaction.pCTranNumber, Equal<Required<CCBatchTransaction.pCTranNumber>>, And<CCBatchTransaction.processingStatus, Equal<CCBatchTranProcessingStatusCode.missing>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) procCenterID,
      (object) pcTranNumber
    });
  }

  private PX.Objects.AR.ARPayment GetVoidedDocForOrigPayment(PX.Objects.AR.ARPayment doc)
  {
    return ((PXSelectBase<PX.Objects.AR.ARPayment>) new PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARRegister.origDocType, Equal<Required<PX.Objects.AR.ARRegister.origDocType>>, And<PX.Objects.AR.ARRegister.origRefNbr, Equal<Required<PX.Objects.AR.ARRegister.origRefNbr>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    });
  }

  private void TrySetTranNbr(PX.Objects.AR.ARPayment doc, InputCCTransaction inputData)
  {
    if (!string.IsNullOrEmpty(inputData.PCTranNumber) || string.IsNullOrEmpty(inputData.PCTranApiNumber))
      return;
    Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> transactionWithPayment = this.GetRefExternalTransactionWithPayment(inputData, doc.ProcessingCenterID);
    if (!(transactionWithPayment?.Item1?.TranApiNumber == inputData.PCTranApiNumber))
      return;
    if (!string.IsNullOrEmpty(transactionWithPayment.Item1.TranNumber))
      inputData.PCTranNumber = transactionWithPayment.Item1.TranNumber;
    else
      throw new PXException("The {0} transaction requires validation.", new object[1]
      {
        (object) inputData.PCTranApiNumber
      });
  }

  private void UpdateDocBeforeApiRecording(PX.Objects.AR.ARPayment doc, InputCCTransaction inputData)
  {
    bool flag = false;
    int? pmInstanceId = doc.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (!(pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue))
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this.Base, doc.PMInstanceID);
      if (customerPaymentMethod != null && customerPaymentMethod.CCProcessingCenterID != null)
      {
        doc.ProcessingCenterID = customerPaymentMethod.CCProcessingCenterID;
        flag = true;
      }
    }
    if (doc.Hold.GetValueOrDefault())
    {
      doc.Hold = new bool?(false);
      flag = true;
    }
    if (flag)
      doc = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Update(doc);
    if (string.IsNullOrEmpty(inputData?.OrigPCTranNumber) || !(doc.DocType == "REF"))
      return;
    this.UpdateOrigTranNumber(doc, inputData);
  }

  private void ValidateDocBeforeApiRecording(PX.Objects.AR.ARPayment doc, InputCCTransaction inputData)
  {
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.Base.paymentmethod).Current;
    string processingCenterId = doc.ProcessingCenterID;
    if (current != null && current.PaymentType == "CCD" && string.IsNullOrEmpty(processingCenterId))
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "processingCenterID"
      });
    if (current != null && current.PaymentType != "CCD" && inputData != null && inputData.TranType != null)
      throw new PXException("The {0} payment method does not support storing credit card transaction information.", new object[1]
      {
        (object) current.PaymentMethodID
      });
    CCProcessingCenter processingCenterById = this.GetProcessingCenterById(processingCenterId);
    if (processingCenterById == null)
      throw new PXException("Processing center can't be found");
    this.CheckProcCenterSupportTransactionValidation(processingCenterById, inputData);
    bool? nullable = processingCenterById.AllowSaveProfile;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    nullable = doc.SaveCard;
    if (nullable.GetValueOrDefault())
      throw new PXException("Saving payment profiles is not allowed for the {0} processing center.", new object[1]
      {
        (object) processingCenterId
      });
  }

  private void UpdateOrigTranNumber(PX.Objects.AR.ARPayment doc, InputCCTransaction inputData)
  {
    string processingCenterId = doc.ProcessingCenterID;
    int? cashAccountId1 = doc.CashAccountID;
    doc.RefTranExtNbr = inputData.OrigPCTranNumber;
    doc = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Update(doc);
    bool flag = false;
    int? pmInstanceId = doc.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue)
    {
      if (processingCenterId != doc.ProcessingCenterID)
      {
        doc.ProcessingCenterID = processingCenterId;
        flag = true;
      }
    }
    else
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this.Base, doc.PMInstanceID);
      if (customerPaymentMethod != null && customerPaymentMethod.CCProcessingCenterID != null && customerPaymentMethod.CCProcessingCenterID != doc.ProcessingCenterID)
      {
        doc.ProcessingCenterID = customerPaymentMethod.CCProcessingCenterID;
        flag = true;
      }
    }
    int? nullable = cashAccountId1;
    int? cashAccountId2 = doc.CashAccountID;
    if (!(nullable.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & nullable.HasValue == cashAccountId2.HasValue))
    {
      doc.CashAccountID = cashAccountId1;
      flag = true;
    }
    if (!flag)
      return;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.Document).Update(doc);
  }

  private void CheckProcCenterSupportTransactionValidation(
    string procCenterId,
    InputCCTransaction info)
  {
    this.CheckProcCenterSupportTransactionValidation(this.GetProcessingCenterById(procCenterId) ?? throw new PXException("Processing center can't be found"), info);
  }

  private void CheckProcCenterSupportTransactionValidation(
    CCProcessingCenter procCenter,
    InputCCTransaction info)
  {
    if ((info.NeedValidation.GetValueOrDefault() || info.TranType == "UKN") && !CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.TransactionGetter))
      throw new PXException("The {0} processing center does not support pulling transaction information from the processing center.", new object[1]
      {
        (object) procCenter.ProcessingCenterID
      });
  }

  private Tuple<PX.Objects.AR.ExternalTransaction, PX.Objects.AR.ARPayment> GetRefExternalTransactionWithPayment(
    InputCCTransaction inputTran,
    string procCenterId)
  {
    int tranTypeByStrCode = (int) TranTypeList.GetTranTypeByStrCode(inputTran.TranType);
    ICCPaymentProcessingRepository paymentRepository = this.GetPaymentRepository();
    bool? needValidation = inputTran.NeedValidation;
    bool flag = false;
    return !(needValidation.GetValueOrDefault() == flag & needValidation.HasValue) || inputTran.OrigPCTranNumber == null ? (string.IsNullOrEmpty(inputTran.PCTranNumber) ? paymentRepository.GetExternalTransactionWithPaymentByApiNumber(inputTran.PCTranApiNumber, procCenterId) : paymentRepository.GetExternalTransactionWithPayment(inputTran.PCTranNumber, procCenterId)) : paymentRepository.GetExternalTransactionWithPayment(inputTran.OrigPCTranNumber, procCenterId);
  }

  private void SetVoidDocTypeVoidRefNbrDefaultByDoc()
  {
    PXDBDefaultAttribute.SetSourceType<PX.Objects.AR.ExternalTransaction.voidDocType>(((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ExternalTransaction)], typeof (PX.Objects.AR.ARRegister.docType));
    PXDBDefaultAttribute.SetSourceType<PX.Objects.AR.ExternalTransaction.voidRefNbr>(((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ExternalTransaction)], typeof (PX.Objects.AR.ARRegister.refNbr));
  }

  private string GetNonEmptyProcCenterTranNumber(InputCCTransaction tran)
  {
    return string.IsNullOrEmpty(tran.PCTranNumber) ? tran.PCTranApiNumber : tran.PCTranNumber;
  }

  private bool HaveSameTranNumber(InputCCTransaction inputTran, IExternalTransaction storedTran)
  {
    if (!string.IsNullOrEmpty(inputTran.PCTranNumber) && !string.IsNullOrEmpty(storedTran.TranNumber))
      return inputTran.PCTranNumber == storedTran.TranNumber;
    return !string.IsNullOrEmpty(inputTran.PCTranApiNumber) && !string.IsNullOrEmpty(storedTran.TranApiNumber) && inputTran.PCTranApiNumber == storedTran.TranApiNumber;
  }

  private IExternalTransaction GetExtTranDetail(InputCCTransaction tran)
  {
    if (!string.IsNullOrEmpty(tran.PCTranNumber))
      return this.GetExtTrans().FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranNumber == tran.PCTranNumber));
    return !string.IsNullOrEmpty(tran.PCTranApiNumber) ? this.GetExtTrans().FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.TranApiNumber == tran.PCTranApiNumber)) : (IExternalTransaction) null;
  }
}
