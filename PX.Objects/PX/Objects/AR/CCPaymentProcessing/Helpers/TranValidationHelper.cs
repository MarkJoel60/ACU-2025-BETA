// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Helpers.TranValidationHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.Common;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Helpers;

public static class TranValidationHelper
{
  public static void CheckRecordedTranStatus(TransactionData tranData)
  {
    if (tranData.TranStatus == 1)
      throw new TranValidationHelper.TranValidationException("The {0} transaction is declined.", new object[1]
      {
        (object) tranData.TranID
      });
    if (tranData.TranStatus == 4)
      throw new TranValidationHelper.TranValidationException("The {0} transaction has expired.", new object[1]
      {
        (object) tranData.TranID
      });
    if (tranData.TranStatus == 2 || tranData.TranStatus == 5)
      throw new TranValidationHelper.TranValidationException("The {0} transaction has an invalid status.", new object[1]
      {
        (object) tranData.TranID
      });
  }

  public static void CheckTranTypeForRefund(TransactionData tranData)
  {
    if (tranData.TranType.HasValue && (tranData.TranType.GetValueOrDefault() == 1 || CCTranTypeCode.IsCaptured(V2Converter.ConvertTranType(tranData.TranType.Value))))
    {
      CCTranTypeCode.GetTypeLabel(V2Converter.ConvertTranType(tranData.TranType.Value));
      throw new TranValidationHelper.TranValidationException("Refund documents allow recording only voided and refunded transactions.", Array.Empty<object>());
    }
  }

  public static void CheckTranAmount(TransactionData tranData, IExternalTransaction storedTran)
  {
    string processingStatus = ExtTransactionProcStatusCode.GetProcStatusStrByProcessingStatus(CCProcessingHelper.GetProcessingStatusByTranData(tranData));
    if ((storedTran.ProcStatus == "AUS" || storedTran.ProcStatus == "AUH") && processingStatus == "CAS")
    {
      Decimal amount1 = tranData.Amount;
      Decimal? amount2 = storedTran.Amount;
      Decimal valueOrDefault = amount2.GetValueOrDefault();
      if (amount1 > valueOrDefault & amount2.HasValue)
        throw new TranValidationHelper.TranValidationException("The {0} transaction amount is not the same as the payment amount.", new object[1]
        {
          (object) tranData.TranID
        });
    }
    else
    {
      Decimal amount3 = tranData.Amount;
      Decimal? amount4 = storedTran.Amount;
      Decimal valueOrDefault = amount4.GetValueOrDefault();
      if (!(amount3 == valueOrDefault & amount4.HasValue))
        throw new TranValidationHelper.TranValidationException("The {0} transaction amount is not the same as the payment amount.", new object[1]
        {
          (object) tranData.TranID
        });
    }
  }

  public static void CheckCustomer(
    TransactionData tranData,
    int? customer,
    CustomerPaymentMethod cpm)
  {
    if (cpm == null || !customer.HasValue)
      return;
    int? baccountId = cpm.BAccountID;
    int? nullable = customer;
    if (!(baccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & baccountId.HasValue == nullable.HasValue))
      throw new TranValidationHelper.TranValidationException("The {0} transaction has been processed for a different customer.", new object[1]
      {
        (object) tranData.TranID
      });
  }

  public static void CheckPmInstance(
    TransactionData tranData,
    Tuple<CustomerPaymentMethod, CustomerPaymentMethodDetail> cpmData)
  {
    if (tranData == null || cpmData == null)
      return;
    CustomerPaymentMethod customerPaymentMethod = cpmData.Item1;
    CustomerPaymentMethodDetail paymentMethodDetail = cpmData.Item2;
    if (tranData.CustomerId != null && tranData.PaymentId != null && (tranData.CustomerId != customerPaymentMethod.CustomerCCPID || tranData.PaymentId != paymentMethodDetail.Value))
      throw new TranValidationHelper.TranValidationException("The {0} transaction was processed with a different payment profile.", new object[1]
      {
        (object) tranData.TranID
      });
  }

  public static void CheckActiveTransactionStateForPayment(
    ICCPayment checkedPayment,
    TransactionData tranData,
    ExternalTransactionState activeState)
  {
    IExternalTransaction externalTransaction = activeState?.ExternalTransaction;
    string str1 = checkedPayment.DocType + checkedPayment.RefNbr;
    string documentName = TranValidationHelper.GetDocumentName(checkedPayment.DocType);
    CCTranType? tranType;
    if (externalTransaction == null || activeState.IsPreAuthorized)
    {
      tranType = tranData.TranType;
      if (tranType.GetValueOrDefault() == 4)
        throw new TranValidationHelper.TranValidationException("The {0} refund transaction cannot be imported for the {1} {2}. The {2} does not have a successful transaction to refund.", new object[3]
        {
          (object) tranData.TranID,
          (object) checkedPayment.RefNbr,
          (object) TranValidationHelper.GetDocumentName(checkedPayment.DocType)
        });
    }
    if (externalTransaction == null && EnumerableExtensions.IsIn<CCTranType?>(tranData.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)))
      throw new TranValidationHelper.TranValidationException("The {0} refund transaction cannot be imported for the {1} {2}. The {2} does not have a successful transaction to void.", new object[3]
      {
        (object) tranData.TranID,
        (object) checkedPayment.RefNbr,
        (object) documentName
      });
    if (externalTransaction == null)
      return;
    tranType = tranData.TranType;
    if (tranType.GetValueOrDefault() == 4 && tranData.RefTranID != null && externalTransaction.TranNumber != tranData.RefTranID)
      throw new TranValidationHelper.TranValidationException("The {0} transaction does not refund the transaction with the {1} transaction number.", new object[2]
      {
        (object) tranData.TranID,
        (object) externalTransaction.TranNumber
      });
    if (tranData.TranID != externalTransaction.TranNumber && tranData.RefTranID != externalTransaction.TranNumber)
    {
      if ((activeState.IsPreAuthorized || activeState.IsCaptured) && EnumerableExtensions.IsIn<CCTranType?>(tranData.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)))
      {
        string transactionTypeName = CCProcessingHelper.GetTransactionTypeName(CCTranType.Void);
        string str2 = activeState.IsPreAuthorized ? CCProcessingHelper.GetTransactionTypeName(CCTranType.AuthorizeOnly) : CCProcessingHelper.GetTransactionTypeName(CCTranType.AuthorizeAndCapture);
        throw new TranValidationHelper.TranValidationException("The {0} {1} transaction is not related to the {2} {3} transaction.", new object[4]
        {
          (object) tranData.TranID,
          (object) transactionTypeName,
          (object) externalTransaction.TranNumber,
          (object) str2
        });
      }
      if (activeState.IsPreAuthorized)
      {
        tranType = tranData.TranType;
        if (tranType.GetValueOrDefault() == 2)
        {
          string transactionTypeName1 = CCProcessingHelper.GetTransactionTypeName(CCTranType.PriorAuthorizedCapture);
          string transactionTypeName2 = CCProcessingHelper.GetTransactionTypeName(CCTranType.AuthorizeOnly);
          throw new TranValidationHelper.TranValidationException("The {0} {1} transaction is not related to the {2} {3} transaction.", new object[4]
          {
            (object) tranData.TranID,
            (object) transactionTypeName1,
            (object) externalTransaction.TranNumber,
            (object) transactionTypeName2
          });
        }
      }
    }
    if (!(tranData.TranID != externalTransaction.TranNumber))
      return;
    if (activeState.IsRefunded)
    {
      tranType = tranData.TranType;
      if (tranType.GetValueOrDefault() == 4)
        throw new TranValidationHelper.TranValidationException("This payment has been refunded already.", Array.Empty<object>());
    }
    if (activeState.IsCaptured)
    {
      tranType = tranData.TranType;
      if (tranType.HasValue)
      {
        tranType = tranData.TranType;
        if (CCTranTypeCode.IsCaptured(V2Converter.ConvertTranType(tranType.Value)))
          throw new TranValidationHelper.TranValidationException("This payment has been captured already.", Array.Empty<object>());
      }
    }
    if (!activeState.IsPreAuthorized)
      return;
    tranType = tranData.TranType;
    if (tranType.GetValueOrDefault() == 1)
      throw new TranValidationHelper.TranValidationException("This payment has been pre-authorized already.", Array.Empty<object>());
  }

  public static void CheckSharedTranIsSuitableForRefund(
    ICCPayment checkedPayment,
    TransactionData tranData,
    TranValidationHelper.AdditionalParams prms)
  {
    if (!EnumerableExtensions.IsIn<CCTranType?>(tranData.TranType, new CCTranType?((CCTranType) 5), new CCTranType?((CCTranType) 9)))
      return;
    PXGraph graph = prms.Repo.Graph;
    ARPayment checkedPmt = ((PXSelectBase<ARPayment>) new PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) checkedPayment.DocType,
      (object) checkedPayment.RefNbr
    });
    Tuple<ExternalTransaction, ARPayment> transactionWithPayment = prms.Repo.GetExternalTransactionWithPayment(tranData.RefTranID ?? throw new TranValidationHelper.TranValidationException("Validation of the {0} transaction has failed. The {0} transaction cannot void the {1} transaction. To refund the original transaction, use the Refund Card Payment command on the More menu. To void the original transaction, delete the Refund document and void the original payment.", new object[2]
    {
      (object) tranData.TranID,
      (object) checkedPayment.RefTranExtNbr
    }), checkedPmt.ProcessingCenterID);
    ARPayment storedPmt = transactionWithPayment != null ? transactionWithPayment.Item2 : throw new TranValidationHelper.TranValidationException("There is no successful transaction to void.", Array.Empty<object>());
    ExternalTransaction externalTransaction = transactionWithPayment.Item1;
    TranValidationHelper.CheckNewAndStoredPayment(checkedPmt, storedPmt, (IExternalTransaction) externalTransaction);
    Decimal? curyDocBal = checkedPmt.CuryDocBal;
    Decimal? amount = externalTransaction.Amount;
    if (!(curyDocBal.GetValueOrDefault() == amount.GetValueOrDefault() & curyDocBal.HasValue == amount.HasValue))
      throw new TranValidationHelper.TranValidationException("The {0} transaction amount is not the same as the payment amount.", new object[1]
      {
        (object) externalTransaction.TransactionID
      });
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState(graph, (IExternalTransaction) externalTransaction);
    if (transactionState.IsRefunded)
      throw new TranValidationHelper.TranValidationException("The {0} transaction is refunded.", new object[1]
      {
        (object) externalTransaction.TranNumber
      });
    if (transactionState.IsVoided)
      throw new TranValidationHelper.TranValidationException("The {0} transaction is voided.", new object[1]
      {
        (object) externalTransaction.TranNumber
      });
  }

  public static void CheckNewAndStoredPayment(
    ARPayment checkedPmt,
    ARPayment storedPmt,
    IExternalTransaction storedExtTran)
  {
    if (checkedPmt == null || storedPmt == null || storedExtTran == null)
      return;
    string centerTranNumber = TranValidationHelper.GetNonEmptyProcCenterTranNumber(storedExtTran);
    int? nullable1 = checkedPmt.CustomerID;
    int? nullable2 = storedPmt.CustomerID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      throw new TranValidationHelper.TranValidationException("The {0} transaction has been processed for a different customer.", new object[1]
      {
        (object) centerTranNumber
      });
    nullable2 = checkedPmt.PMInstanceID;
    nullable1 = PaymentTranExtConstants.NewPaymentProfile;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      nullable1 = storedPmt.PMInstanceID;
      nullable2 = checkedPmt.PMInstanceID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        throw new TranValidationHelper.TranValidationException("The {0} transaction was processed with a different payment profile.", new object[1]
        {
          (object) centerTranNumber
        });
    }
    else
    {
      nullable2 = storedPmt.CustomerID;
      nullable1 = checkedPmt.CustomerID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        throw new TranValidationHelper.TranValidationException("The {0} transaction has been processed for a different customer.", new object[1]
        {
          (object) centerTranNumber
        });
      if (storedPmt.PaymentMethodID != checkedPmt.PaymentMethodID)
        throw new TranValidationHelper.TranValidationException("The {0} transaction was processed with a different payment method.", new object[1]
        {
          (object) centerTranNumber
        });
      if (storedPmt.ProcessingCenterID != checkedPmt.ProcessingCenterID)
        throw new TranValidationHelper.TranValidationException("The {0} transaction was processed with a different processing center.", new object[1]
        {
          (object) centerTranNumber
        });
    }
    nullable1 = storedPmt.CashAccountID;
    nullable2 = checkedPmt.CashAccountID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      throw new TranValidationHelper.TranValidationException("The {0} transaction was processed for a document associated with a different cash account.", new object[1]
      {
        (object) centerTranNumber
      });
    if (storedExtTran.NeedSync.GetValueOrDefault())
      throw new TranValidationHelper.TranValidationException("The {0} transaction requires validation.", new object[1]
      {
        (object) centerTranNumber
      });
    Decimal? curyDocBal = checkedPmt.CuryDocBal;
    Decimal? amount = storedExtTran.Amount;
    if (curyDocBal.GetValueOrDefault() > amount.GetValueOrDefault() & curyDocBal.HasValue & amount.HasValue)
      throw new TranValidationHelper.TranValidationException("The {0} transaction amount is not the same as the payment amount.", new object[1]
      {
        (object) storedExtTran.TransactionID
      });
  }

  public static void CheckPaymentProfile(
    TransactionData tranData,
    TranValidationHelper.AdditionalParams prms)
  {
    CustomerPaymentMethod customerPaymentMethod = (CustomerPaymentMethod) null;
    ICCPaymentProcessingRepository repo = prms.Repo;
    int? pmInstanceId = prms.PMInstanceId;
    int num = 0;
    if (pmInstanceId.GetValueOrDefault() > num & pmInstanceId.HasValue)
    {
      Tuple<CustomerPaymentMethod, CustomerPaymentMethodDetail> withProfileDetail = repo.GetCustomerPaymentMethodWithProfileDetail(prms.PMInstanceId);
      if (withProfileDetail == null)
        throw new TranValidationHelper.TranValidationException("Credit Card with ID {0} is not defined", new object[1]
        {
          (object) prms.PMInstanceId
        });
      TranValidationHelper.CheckPmInstance(tranData, withProfileDetail);
      TranValidationHelper.CheckCustomer(tranData, prms.CustomerID, withProfileDetail.Item1);
    }
    else
    {
      Tuple<CustomerPaymentMethod, CustomerPaymentMethodDetail> withProfileDetail = repo.GetCustomerPaymentMethodWithProfileDetail(prms.ProcessingCenter, tranData.CustomerId, tranData.PaymentId);
      if (withProfileDetail != null)
      {
        CustomerPaymentMethod cpm = withProfileDetail.Item1;
        TranValidationHelper.CheckCustomer(tranData, prms.CustomerID, cpm);
      }
      else
      {
        if (customerPaymentMethod != null || tranData.CustomerId == null)
          return;
        CustomerData customerProfile = CCCustomerInformationManager.GetCustomerProfile(repo.Graph, prms.ProcessingCenter, tranData.CustomerId);
        if (string.IsNullOrEmpty(customerProfile?.CustomerCD))
          return;
        string acctCD = CCProcessingHelper.DeleteCustomerPrefix(customerProfile.CustomerCD.Trim());
        if (!Customer.PK.Find(prms.Repo.Graph, prms.CustomerID).AcctCD.Trim().Equals(acctCD) && CCProcessingHelper.GetCustomer(prms.Repo.Graph, acctCD) != null)
          throw new TranValidationHelper.TranValidationException("The {0} transaction has been processed for a different customer.", new object[1]
          {
            (object) tranData.TranID
          });
      }
    }
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public static void CheckTransactionByNoteId(
    TransactionData tranData,
    TranValidationHelper.AdditionalParams inputParams)
  {
    ExternalTransaction externalTransaction = ((PXSelectBase<ExternalTransaction>) new PXSelect<ExternalTransaction, Where<ExternalTransaction.noteID, Equal<Required<ExternalTransaction.noteID>>>>(inputParams.Repo.Graph)).SelectSingle(new object[1]
    {
      (object) tranData.TranUID
    });
    if (externalTransaction == null)
      throw new TranValidationHelper.TranValidationException("The {0} transaction has been processed on another Acumatica ERP instance or tenant and cannot be recorded.", new object[1]
      {
        (object) tranData.TranID
      });
    if (!string.IsNullOrEmpty(externalTransaction.TranNumber) && !externalTransaction.TranNumber.Equals(tranData.TranID) || !externalTransaction.ProcessingCenterID.Equals(inputParams.ProcessingCenter))
      throw new TranValidationHelper.TranValidationException("The {0} transaction has been processed on another Acumatica ERP instance and cannot be recorded.", new object[1]
      {
        (object) tranData.TranID
      });
  }

  public static void CheckTranAlreadyRecorded(
    TransactionData tranData,
    TranValidationHelper.AdditionalParams inputParams)
  {
    ExternalTransaction extTran = ((PXSelectBase<ExternalTransaction>) new PXSelect<ExternalTransaction, Where<ExternalTransaction.tranNumber, Equal<Required<ExternalTransaction.tranNumber>>, And<ExternalTransaction.processingCenterID, Equal<Required<ExternalTransaction.processingCenterID>>, And<Not<ExternalTransaction.syncStatus, Equal<CCSyncStatusCode.error>, And<ExternalTransaction.active, Equal<False>>>>>>>(inputParams.Repo.Graph)).SelectSingle(new object[2]
    {
      (object) tranData.TranID,
      (object) inputParams.ProcessingCenter
    });
    if (extTran != null)
      throw new TranValidationHelper.TranValidationException(TranValidationHelper.GenerateTranAlreadyRecordedErrMsg(tranData.TranID, extTran, inputParams), Array.Empty<object>());
  }

  public static string GenerateTranAlreadyRecordedErrMsg(
    string tranId,
    ExternalTransaction extTran,
    TranValidationHelper.AdditionalParams inputParams)
  {
    return TranValidationHelper.GenerateTranAlreadyRecordedErrMsg(tranId, extTran.DocType, extTran.RefNbr, inputParams);
  }

  public static string GenerateTranAlreadyRecordedErrMsg(
    string tranId,
    CCProcTran procTran,
    TranValidationHelper.AdditionalParams inputParams)
  {
    return TranValidationHelper.GenerateTranAlreadyRecordedErrMsg(tranId, procTran.DocType, procTran.RefNbr, inputParams);
  }

  public static string GetDocumentName(string docType)
  {
    string documentName = PXMessages.LocalizeNoPrefix("Document");
    if (docType != null)
    {
      ValueLabelPair valueLabelPair = new ARDocType().ValueLabelPairs.FirstOrDefault<ValueLabelPair>((Func<ValueLabelPair, bool>) (i => i.Value == docType));
      if (valueLabelPair.Label != null)
        documentName = PXMessages.LocalizeNoPrefix(valueLabelPair.Label).ToLower();
    }
    return documentName;
  }

  public static string GenerateTranAlreadyRecordedErrMsg(
    string tranId,
    string docType,
    string refNbr,
    TranValidationHelper.AdditionalParams inputParams)
  {
    string documentName = TranValidationHelper.GetDocumentName(docType);
    string str = (string) null;
    if (docType != null)
      str = docType + refNbr;
    int? pmInstanceId = inputParams.PMInstanceId;
    int num = 0;
    string alreadyRecordedErrMsg;
    if (pmInstanceId.GetValueOrDefault() > num & pmInstanceId.HasValue)
    {
      ICCPaymentProcessingRepository repo = inputParams.Repo;
      CustomerPaymentMethod customerPaymentMethod = CustomerPaymentMethod.PK.Find(inputParams.Repo.Graph, inputParams.PMInstanceId);
      alreadyRecordedErrMsg = PXMessages.LocalizeFormatNoPrefix("The {0} transaction that was processed with the {1} customer payment method has already been recorded for the {2} {3}.", new object[4]
      {
        (object) tranId,
        (object) customerPaymentMethod.Descr,
        (object) str,
        (object) documentName
      });
    }
    else
      alreadyRecordedErrMsg = PXMessages.LocalizeFormatNoPrefix("The {0} transaction that was processed with the {1} processing center has already been recorded for the {2} {3}.", new object[4]
      {
        (object) tranId,
        (object) inputParams.ProcessingCenter,
        (object) str,
        (object) documentName
      });
    return alreadyRecordedErrMsg;
  }

  private static string GetNonEmptyProcCenterTranNumber(IExternalTransaction tran)
  {
    return string.IsNullOrEmpty(tran.TranNumber) ? tran.TranApiNumber : tran.TranNumber;
  }

  public class TranValidationException : PXException
  {
    public TranValidationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public TranValidationException(string format, params object[] args)
      : base(format, args)
    {
      this._MessagePrefix = (string) null;
    }
  }

  public class AdditionalParams
  {
    public int? PMInstanceId { get; set; }

    public int? CustomerID { get; set; }

    public string ProcessingCenter { get; set; }

    public ICCPaymentProcessingRepository Repo { get; set; }
  }
}
